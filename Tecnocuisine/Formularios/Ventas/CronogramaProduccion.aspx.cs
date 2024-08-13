using Gestion_Api.Controladores.APP;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class CronogramaProduccion : System.Web.UI.Page
    {
        public DataTable dtGlobal = new DataTable();
        public DataTable dtIngredientes = new DataTable();
        public DataTable dt;
        public DataTable dtOrdenDeProduccionRecetas_recetas;
        public DataTable dtCantidadRecetasPorCadaOrden;
        //Esta datatable contiene una combinacion de los ingredientes normales
        //y las recetas que se usan como ingredientes
        public DataTable dtCombined;
        public Dictionary<string, DataTable> sectorTables;
        public Dictionary<string, DataTable> sectorTablesGroupByFechas = new Dictionary<string, DataTable>();
        public int cantMax = 0;
        public List<int> numeros = new List<int>();
        public int cont = 0;
        public DateTime fechaOrdenDeProduccion;
        //public List<string> fechasHead = new List<string>();
        public HashSet<string> fechasHead = new HashSet<string>();

        private string idsQueryString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                idsQueryString = Request.QueryString["ids"]; //Esta variable obtiene todos los ids de todas las ordenes de produccion seleccionadas

                if (string.IsNullOrWhiteSpace(idsQueryString))
                    Response.Redirect("OrdenesDeProduccion.aspx", false);

                GenerarCronograma();

                //dtOrdenDeProduccionRecetas_recetas = cOrdenDeProduccion.GetAllIngredientesRecetasOrdenesProduccionGroupBySector(idsQueryString);
                //dtGlobal.DefaultView.Sort = "SectorProductivo ASC";
                //dtGlobal = dt.DefaultView.ToTable();

                DataView dv = dtGlobal.DefaultView;
                dv.Sort = "sectorProductivo ASC, fechaProducto ASC";

                // Asignar la vista ordenada de nuevo a la DataTable
                dtGlobal = dv.ToTable();
                //Esta funcion obtiene las fechas de entrega de todas las ordenes de produccion
                Session["DatosOrdenesSeleccionas"] = dtGlobal;

                separarSectoresEnTablas();

                // fechaOrdenDeProduccion = obtenerFechaOrden(idsQueryString);
                // dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);

                obtenerOPNumeros(idsQueryString);
            }
            catch (Exception ex)
            {
                Response.Redirect("OrdenesDeProduccion.aspx", false);
            }
        }

        private void GenerarCronograma()
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            DataTable dtRecetasFinalesOrdenes = cOrdenDeProduccion.GetRecetasDeOrdenesSeleccionadas(idsQueryString);

            foreach (DataRow dr in dtRecetasFinalesOrdenes.Rows)
            {
                int id = Convert.ToInt32(dr["id"].ToString());
                string OPNumero = dr["OPNumero"].ToString();
                string producto = dr["Producto"].ToString();
                int idReceta = Convert.ToInt32(dr["idReceta"].ToString());
                string cantidad = dr["cantidad"].ToString();
                string fechaEntrega = dr["fechaEntrega"].ToString();
                int idSector = Convert.ToInt32(dr["id5"].ToString());
                string sector = dr["descripcion1"].ToString();
                string razonSocial = dr["razonSocial"].ToString();

                /// Obtener los ingredientes de la receta recorrida 
                DataTable dtingredietesNivel1 = obtenerIngredientesReceta(id, OPNumero, producto, idReceta, cantidad, fechaEntrega, idSector, sector, razonSocial);
                dtGlobal.Merge(dtingredietesNivel1); // no mover de aca!!

                /// Insertar en la tabla la receta final que se esta recorriendo
                DataRow rowRecetaOrden = CrearRowRecetaOrden(dr);
                dtGlobal.Rows.Add(rowRecetaOrden);

                /// Obtener las subrecetas de la receta recorrida
                DataTable dtSubRecetas = obtenerSubRecetasOrdenes(id, OPNumero, producto, idReceta, cantidad, fechaEntrega, idSector, sector, razonSocial);
                dtGlobal.Merge(dtSubRecetas);

                /// Recorrer los ingredientes y recetas de cada subreceta
                RecorrerSubrecetas(dtSubRecetas);
            }
        }

        private void RecorrerSubrecetas(DataTable dtRecetas)
        {
            DataTable dtRecetasOriginal; // Para mantener la fuente original

            // Cada subreceta puede tener infinitas subrecetas (por eso se usa while preguntando si la receta tiene mas subrecetas dentro)
            while (dtRecetas.Rows.Count > 0)
            {
                // Guardar la fuente original de subrecetas
                dtRecetasOriginal = dtRecetas.Copy();

                foreach (DataRow dr in dtRecetas.Rows)
                {
                    DateTime fechaSubrecetaPadre = Convert.ToDateTime(dr["fechaProducto"].ToString());

                    int idOrdenProduccion = Convert.ToInt32(dr["idOrdenProduccion"].ToString());
                    string OPNumero = dr["OPNumero"].ToString();
                    string descripcion = dr["descripcion"].ToString();
                    int idProductoReceta = Convert.ToInt32(dr["idProductoOReceta"].ToString());
                    string cantidad = dr["cantidad"].ToString();
                    string fechaEntregaOrden = dr["FechaEntregaOrden"].ToString();
                    int idSector = Convert.ToInt32(dr["idSector"].ToString());
                    string sector = dr["sectorProductivo"].ToString();
                    string razonSocial = dr["razonSocial"].ToString();

                    // Ingredientes: Aceite,oregano,cebolla...
                    DataTable dtingredientes = obtenerIngredientesReceta(idOrdenProduccion, OPNumero, descripcion, idProductoReceta, cantidad, fechaEntregaOrden, idSector, sector, razonSocial);
                    // Recorrer cada ingrediente y cambiarle la fecha teniendo en cuenta su tiempo de elaboracion y la fecha del padre
                    SetFechasElaboracion(dtingredientes, fechaSubrecetaPadre);

                    //Subrecetas
                    DataTable dtSubrecetas = obtenerSubRecetasOrdenes(idOrdenProduccion, OPNumero, descripcion, idProductoReceta, cantidad, fechaEntregaOrden, idSector, sector, razonSocial);
                    // Recorrer cada subreceta y cambiarle la fecha teniendo en cuenta su tiempo de elaboracion y la fecha del padre:
                    SetFechasElaboracion(dtSubrecetas, fechaSubrecetaPadre);

                    dtGlobal.Merge(dtingredientes);
                    dtGlobal.Merge(dtSubrecetas);

                    if (dtSubrecetas.Rows.Count > 0)
                    {
                        dtRecetas = dtSubrecetas; // Cambiar la fuente a las nuevas subrecetas encontradas
                        break; // Romper el foreach actual para comenzar de nuevo con la nueva fuente
                    }
                    //Cuando la receta procesada ya no tenga subrecetas, seguimos con la siguiente receta
                    else
                    {
                        dtRecetas.Rows.RemoveAt(0);
                        break;
                    }
                }

                //Cuando la receta procesada ya no tenga subrecetas, seguimos con la siguiente receta
                //Esto se deberia hacer solo en el caso de que la receta procesada anteriormente haya tenido subrecetas, porque si no tuvo, este segundo registro ya se habria procesado en el for
                //if (entroEnSubreceta)
                //{
                //    entroEnSubreceta = false;
                //    dtRecetasOriginal.Rows.RemoveAt(0);
                //    dtRecetas = dtRecetasOriginal;
                //}
            }

        }

        private void SetFechasElaboracion(DataTable dt, DateTime fechaSubrecetaPadre)
        {
            // Calculo: fecha del padre - tiempo elaboracion = fecha del ingrediente
            foreach (DataRow dr in dt.Rows)
            {
                dr["fechaProducto"] = fechaSubrecetaPadre.AddDays(-(int)dr["Tiempo"]);
            }
        }

        /// <summary>
        /// Devuelve una fila para la datatable "dtGlobal" con la informacion de una receta de la orden de produccion (producto final)
        /// </summary>
        /// <param name="rowRecetasOrden">
        /// </param>
        /// <returns></returns>
        private DataRow CrearRowRecetaOrden(DataRow rowRecetasOrden)
        {
            DataRow newRow = dtGlobal.NewRow();
            newRow["idOrdenProduccion"] = rowRecetasOrden["idOrdenDeProduccion"];
            newRow["OPNumero"] = rowRecetasOrden["OPNumero"];
            newRow["Column1"] = rowRecetasOrden["Producto"];
            newRow["idRecetaPadre"] = rowRecetasOrden["idReceta"];
            newRow["CantidadPadre"] = rowRecetasOrden["cantidad"];
            newRow["FechaEntregaOrden"] = rowRecetasOrden["fechaEntrega"];
            newRow["idSectorPadre"] = rowRecetasOrden["id5"];
            newRow["sectorPadre"] = rowRecetasOrden["descripcion1"];
            newRow["idProductoOReceta"] = rowRecetasOrden["idReceta"];
            newRow["descripcion"] = rowRecetasOrden["descripcion"];
            newRow["cantidad"] = rowRecetasOrden["cantidad"];
            newRow["idSector"] = rowRecetasOrden["id5"];
            newRow["sectorProductivo"] = rowRecetasOrden["descripcion1"];
            newRow["fechaProducto"] = rowRecetasOrden["fechaEntrega"];
            newRow["ingredienteOreceta"] = "Receta";
            newRow["RazonSocial"] = rowRecetasOrden["razonSocial"];
            //newRow["unidadMedida"] = "Litro";
            newRow["SectorPadre"] = rowRecetasOrden["descripcion1"];

            return newRow;
        }

        public void separarSectoresEnTablas()
        {
            sectorTables = new Dictionary<string, DataTable>();

            foreach (DataRow row in dtGlobal.Rows)
            {
                string sector = row["sectorProductivo"].ToString();

                if (!sectorTables.ContainsKey(sector))
                {
                    DataTable newTable = dtGlobal.Clone();
                    newTable.TableName = sector;
                    sectorTables.Add(sector, newTable);
                }

                sectorTables[sector].ImportRow(row);
            }

            fusionarFilas();

        }

        public void fusionarFilas()
        {
            foreach (var kvp in sectorTables)
            {
                DataColumn nuevaColumna = new DataColumn("index", typeof(int));
                nuevaColumna.DefaultValue = 0;
                kvp.Value.Columns.Add(nuevaColumna);

                DataColumn nuevaColumna2 = new DataColumn("delete", typeof(int));
                nuevaColumna2.DefaultValue = 0;
                kvp.Value.Columns.Add(nuevaColumna2);

                int cont = 0;
                kvp.Value.Columns["idOrdenProduccion"].DataType = typeof(string);
                // int rowIndex = kvp.Value.IndexOf(col);
                List<DataRow> filasParaEliminar = new List<DataRow>();

                foreach (DataRow col in kvp.Value.DefaultView.Table.Rows)
                {
                    cont++;
                    col["index"] = cont;

                    foreach (DataRow row in kvp.Value.DefaultView.Table.Rows)
                    {
                        if (Convert.ToInt32((col["index"])) != Convert.ToInt32(row["index"]))
                        {

                            if (col["descripcion"].ToString() == row["descripcion"].ToString() && col["fechaProducto"].ToString() == row["fechaProducto"].ToString() && col["delete"].ToString() == "0")
                            {
                                col["idOrdenProduccion"] = col["idOrdenProduccion"] + ";" + row["idOrdenProduccion"];
                                col["OPNumero"] = col["OPNumero"] + ";" + row["OPNumero"];
                                col["cantidad"] = col["cantidad"] + ";" + row["cantidad"];
                                col["column1"] = col["column1"] + ";" + row["column1"];
                                col["sectorPadre"] = col["sectorPadre"] + ";" + row["sectorPadre"];
                                col["RazonSocial"] = col["RazonSocial"] + ";" + row["RazonSocial"];
                                row["delete"] = 1;
                                filasParaEliminar.Add(row);
                            }
                        }
                    }
                }


                foreach (DataRow fila in filasParaEliminar)
                {
                    kvp.Value.Rows.Remove(fila);
                }
            }


        }

        public string GenerarHTMLWidget()
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div class=\"row\">");
            htmlBuilder.Append("<div class=\"col-lg-12\">");
            htmlBuilder.Append("<div class=\"ibox float-e-margins\">");
            htmlBuilder.Append("<div class=\"ibox-title\">");
            htmlBuilder.Append("<div class=\"ibox-tools\">");
            htmlBuilder.Append("<a class=\"collapse-link\">");
            htmlBuilder.Append("<i class=\"fa fa-chevron-up\"></i>");
            htmlBuilder.Append("</a>");
            htmlBuilder.Append("<ul class=\"dropdown-menu dropdown-user\">");
            htmlBuilder.Append("<li><a href=\"#\">Config option 1</a></li>");
            htmlBuilder.Append("<li><a href=\"#\">Config option 2</a></li>");
            htmlBuilder.Append("</ul>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("<div class=\"ibox-content\">");
            htmlBuilder.Append("<table class=\"table table-hover no-margins table-bordered\">");
            htmlBuilder.Append("<thead><tr></tr></thead>");
            htmlBuilder.Append("<tbody></tbody>");
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }



        public DataTable obtenerSubRecetasOrdenes(int id, string OPNumero, string Producto, int idReceta, string cantidad, string fechaEntrega, int idSector, string sectorPadre, string RazonSocialCliente)
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                DataTable dt = cOrdenDeProduccion.getRecipesFromRecipes(id, OPNumero, Producto, idReceta, cantidad, fechaEntrega, idSector, sectorPadre, RazonSocialCliente);

                return dt;

            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public DataTable obtenerIngredientesReceta(int id, string OPNumero, string Producto, int idReceta, string cantidad, string fechaEntrega, int idSector, string SectorPadre, string RazonSocialCliente)
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                DataTable dt = cOrdenDeProduccion.getIngredientsRecipes(id, OPNumero, Producto, idReceta, cantidad, fechaEntrega, idSector, SectorPadre, RazonSocialCliente);

                return dt;

            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public DateTime obtenerFechaOrden(string idOrdenDeProduccion)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            DataTable fechaOrdenProduccion = cOrdenDeProduccion.getFechaEntregaOrdenProduccion(idOrdenDeProduccion);

            DateTime FechaOrdenDeProduccion = Convert.ToDateTime(fechaOrdenProduccion.Rows[0][0]);
            return FechaOrdenDeProduccion;
        }

        public void ObtenerProductoDeLasRecetas(DataTable dtRecetas, int Nivel)
        {
            try
            {
                foreach (DataRow dr in dtRecetas.Rows)
                {
                    if (dr["descripcion"].ToString() == "Receta")
                    {
                        int idReceta = Convert.ToInt32(dr["id"].ToString());
                        DataTable productosDeLaReceta = ObtenerProductosPorIdReceta(idReceta, Nivel);

                        if (productosDeLaReceta != null)
                        {
                            dt.Merge(productosDeLaReceta);
                        }
                    }

                }
            }
            catch (Exception ex)
            {


            }

        }


        public DataTable ObtenerRecetasDeLasRecetas(DataTable dtRecetas, int Nivel)
        {
            DataTable RecetasDeLaReceta = null;

            foreach (DataRow dr in dtRecetas.Rows)
            {
                if (dr["descripcion"].ToString() == "Receta")
                {

                    //int idReceta = obterneridRecetaByDescripcion(dr["ProductoOReceta"].ToString());
                    int idReceta = Convert.ToInt32(dr["id"].ToString());
                    RecetasDeLaReceta = ObtenerRecetasPorIdReceta(idReceta, Nivel);

                    if (RecetasDeLaReceta != null)
                    {
                        dt.Merge(RecetasDeLaReceta);
                    }
                }

            }

            return RecetasDeLaReceta;
        }

        public int obterneridRecetaByDescripcion(string Descripcion)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtidReceta = cReceta.obtenerIdRecetaByDescripcionReceta(Descripcion);


            int idReceta = 0;
            idReceta = Convert.ToInt32(dtidReceta.Rows[0][0]);
            return idReceta;
        }

        public DataTable ObtenerProductosPorIdReceta(int idReceta, int Nivel)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtProductos = cReceta.ObtenerProductosPorIdReceta(idReceta, Nivel);
            return dtProductos;
        }


        public DataTable ObtenerRecetasPorIdReceta(int idReceta, int Nivel)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtRecetas = cReceta.ObtenerRecetaPorIdReceta(idReceta, Nivel);
            return dtRecetas;
        }


        //Esta funcion busca y obtiene todos los numeros de produccion en base a sus id
        public void obtenerOPNumeros(string idsQueryString)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            DataTable dt = cOrdenDeProduccion.GetallOPNumeros(idsQueryString);

            string OPNumeros = string.Empty;
            OPNumeros = dt.Rows[0][0].ToString();
            MostrarOPNumeros(OPNumeros);
        }
        public void MostrarOPNumeros(string OPNumeros)
        {
            //string idsQueryString = Request.QueryString["ids"];
            OPNumeros = OPNumeros.TrimStart(',');
            lblIdsQueryString.Text = OPNumeros;
        }


        [WebMethod]
        public static string getProductoByidReceta(int id, int nivel)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dtProductos = cReceta.ObtenerProductosPorIdReceta(id, nivel);
                DataTable dtRecetas = cReceta.ObtenerRecetaPorIdReceta(id, nivel);
                string ProductosDeLaReceta = "";

                foreach (DataRow dr in dtProductos.Rows)
                {
                    string cantidad = dr["Cantidad"].ToString();
                    cantidad = cantidad.Replace(",", ".");

                    ProductosDeLaReceta += dr["id"].ToString() + "," + dr["SectorProductivo"].ToString() + "," + dr["ProductoOReceta"].ToString() + "," + dr["Dias"].ToString() +
                    "," + dr["Nivel"].ToString() + "," + dr["DiasMasNivel"].ToString() + "," + dr["Descripcion"].ToString() + "," + cantidad + ";";
                }

                foreach (DataRow drRecetas in dtRecetas.Rows)
                {
                    string cantidad = drRecetas["Cantidad"].ToString();
                    cantidad = cantidad.Replace(",", ".");

                    ProductosDeLaReceta += drRecetas["id"].ToString() + "," + drRecetas["SectorProductivo"].ToString() + "," + drRecetas["ProductoOReceta"].ToString() + "," + drRecetas["Dias"].ToString() +
                    "," + drRecetas["Nivel"].ToString() + "," + drRecetas["DiasMasNivel"].ToString() + "," + drRecetas["Descripcion"].ToString() + "," + cantidad + ";";
                }

                return ProductosDeLaReceta;

            }
            catch (Exception ex)
            {
                return null;
            }
        }




        [WebMethod]
        public static string getSectorProductivoByIdReceta(int idReceta)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getSectorProductivoByIdReceta(idReceta);
                string SectorProductivo = dt.Rows[0][9].ToString();
                string Cantidad = dt.Rows[0][5].ToString();
                return SectorProductivo + ";" + Cantidad;

            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [WebMethod]
        public static int cambiarEstadoDeLaOrden(string ids, int estadoOrden)
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                cOrdenDeProduccion.cambiarEstadoOrden(ids, estadoOrden);

                DataTable dt = HttpContext.Current.Session["DatosOrdenesSeleccionas"] as DataTable;
                Dictionary<string, DataTable> dtDiccionario = new Dictionary<string, DataTable>();
                Dictionary<string, DataTable> sumaCantidadProductos = new Dictionary<string, DataTable>();

                foreach (DataRow row in dt.Rows)
                {
                    string sector = row["sectorPadre"].ToString();
                    string sectorOrigen = row["sectorProductivo"].ToString();
                    string fechaProducto = row["fechaProducto"].ToString();

                    string clave = $"{sector}_{sectorOrigen}_{fechaProducto}";

                    if (!dtDiccionario.ContainsKey(clave))
                    {
                        DataTable newTable = dt.Clone();
                        newTable.TableName = clave;
                        dtDiccionario.Add(clave, newTable);
                    }

                    dtDiccionario[clave].ImportRow(row);
                }

                //sumaCantidadProductos = sumarCantidadAgrupadaPorProducto(dt);

                //controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
                //foreach (var key in sumaCantidadProductos)
                //{
                //    datosTransferencias datosTransferencias = new datosTransferencias();
                //    var row = key.Value.DefaultView.Table.Rows[0];
                //    datosTransferencias.sectorOrigen = row[12].ToString();
                //    datosTransferencias.sectorDestino = row[7].ToString();
                //    datosTransferencias.producto = row[9].ToString();
                //    datosTransferencias.cantidad = 0;
                //    datosTransferencias.estado = 1;
                //    foreach (DataRow column in key.Value.DefaultView.Table.Rows)
                //    {
                //        datosTransferencias.cantidad += decimal.Parse(row[10].ToString(), CultureInfo.InvariantCulture);
                //    }

                //    cDatosTransferencias.addDatosTrasferencias(datosTransferencias);

                //}

                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                controladorPedidosTranferencia cPedidosTranferencia = new controladorPedidosTranferencia();

                foreach (var kvp in dtDiccionario)
                {
                    Transferencia transferencia = new Transferencia();
                    string[] partesClave = kvp.Key.Split('_');
                    string origenString = partesClave[1];
                    string destinoString = partesClave[0];
                    string fechaString = partesClave[2];

                    transferencia.fecha = Convert.ToDateTime(fechaString);
                    transferencia.origen = origenString;
                    transferencia.destino = destinoString;
                    transferencia.estadoTransferencia = 2;

                    var listaTransferencias = cTransferencia.getListTransferencias();

                    string fac1 = "";
                    if (listaTransferencias.Count != 0)
                    {
                        string codigo = (listaTransferencias.Count + 1).ToString();
                        fac1 = cOrdenDeProduccion.GenerarCodigoPedido(codigo);
                        transferencia.orden = "#" + fac1;
                    }
                    else
                    {
                        transferencia.orden = "#000001";
                    }

                    transferencia.estato = 1;

                    int id = cTransferencia.addTransferencia(transferencia);

                    if (id != -1)
                    {
                        Dictionary<string, DataTable> sumaCantidadPorGrupo = new Dictionary<string, DataTable>();

                        foreach (DataRow col1 in kvp.Value.DefaultView.Table.Rows)
                        {
                            PedidosInternos pedidosInternos = new PedidosInternos();
                            pedidosInternos.sectorOrigen = col1["sectorProductivo"].ToString();
                            pedidosInternos.productoOrigen = col1["descripcion"].ToString();
                            pedidosInternos.cantidadOrigen = col1["cantidad"].ToString();
                            pedidosInternos.ProductoDestinodestino = col1["Column1"].ToString();
                            pedidosInternos.SectorDestinodestino = col1["sectorPadre"].ToString();
                            pedidosInternos.orden = col1["OPNumero"].ToString();
                            pedidosInternos.cliente = col1["RazonSocial"].ToString();
                            pedidosInternos.idTransferencia = id;
                            pedidosInternos.CantidadAConfirmar = col1["cantidad"].ToString();
                            pedidosInternos.estado = 1;
                            cPedidosTranferencia.addPedidosInternos(pedidosInternos);


                            string key = $"{col1["sectorProductivo"]}-{col1["descripcion"]}";

                            if (!sumaCantidadPorGrupo.ContainsKey(key))
                            {
                                DataTable newTable = kvp.Value.Clone(); // Clonamos la estructura de la tabla original
                                sumaCantidadPorGrupo[key] = newTable;
                            }


                            sumaCantidadPorGrupo[key].ImportRow(col1);
                        }

                        //aca quiero convertirlo en una datatable
                        controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
                        foreach (var key in sumaCantidadPorGrupo)
                        {
                            datosTransferencias datosTransferencias = new datosTransferencias();
                            foreach (DataRow col1 in key.Value.DefaultView.Table.Rows)
                            {
                                datosTransferencias.sectorOrigen = col1["sectorProductivo"].ToString();
                                datosTransferencias.sectorDestino = col1["sectorPadre"].ToString();
                                datosTransferencias.producto = col1["descripcion"].ToString();
                                datosTransferencias.cantidad += Convert.ToDecimal(col1["cantidad"].ToString(), CultureInfo.InvariantCulture);
                                datosTransferencias.cantidadConfirmada += Convert.ToDecimal(col1["cantidad"].ToString(), CultureInfo.InvariantCulture);
                                datosTransferencias.idTransferencia = id;
                                datosTransferencias.estado = true;

                            }
                            cDatosTransferencias.addDatosTrasferencias(datosTransferencias);

                        }

                        //sumaCantidadProductos = sumarCantidadAgrupadaPorProducto(dt);

                        //controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
                        //foreach (var key in sumaCantidadProductos)
                        //{
                        //    datosTransferencias datosTransferencias = new datosTransferencias();
                        //    var row = key.Value.DefaultView.Table.Rows[0];
                        //    datosTransferencias.sectorOrigen = row[12].ToString();
                        //    datosTransferencias.sectorDestino = row[7].ToString();
                        //    datosTransferencias.producto = row[9].ToString();
                        //    datosTransferencias.cantidad = 0;
                        //    datosTransferencias.estado = 1;
                        //    foreach (DataRow column in key.Value.DefaultView.Table.Rows)
                        //    {
                        //        datosTransferencias.cantidad += decimal.Parse(row[10].ToString(), CultureInfo.InvariantCulture);
                        //    }

                        //    cDatosTransferencias.addDatosTrasferencias(datosTransferencias);

                        //}
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static Dictionary<string, DataTable> sumarCantidadAgrupadaPorProducto(DataTable dt)
        {
            Dictionary<string, DataTable> dtDiccionario = new Dictionary<string, DataTable>();
            foreach (DataRow row in dt.Rows)
            {
                string descripcionProducto = row["descripcion"].ToString();
                //string sectorOrigen = row["sectorProductivo"].ToString();
                //string fechaProducto = row["fechaProducto"].ToString();

                string clave = $"{descripcionProducto}";

                if (!dtDiccionario.ContainsKey(clave))
                {
                    DataTable newTable = dt.Clone();
                    newTable.TableName = clave;
                    dtDiccionario.Add(clave, newTable);
                }

                dtDiccionario[clave].ImportRow(row);
            }

            return dtDiccionario;
        }

        public static void saveDatosTransferencia(Dictionary<string, decimal> sumaCantidadPorGrupo, int idTransferencia)
        {
            datosTransferencias datosTransferencias = new datosTransferencias();
            controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();



            //foreach (var kvp in sumaCantidadPorGrupo)
            //{
            //    string[] partes = kvp.Key.Split('_');
            //    string sectorOrigen = partes[0];
            //    string sectorDestino = partes[1];
            //    string producto = partes[2];
            //    decimal cantidad = kvp.Value;
            //    int estado = 1;

            //    datosTransferencias.sectorOrigen = sectorOrigen;
            //    datosTransferencias.sectorDestino = sectorDestino;
            //    datosTransferencias.cantidad = cantidad;
            //    datosTransferencias.producto = producto;
            //    datosTransferencias.estado = estado;
            //    datosTransferencias.idTransferencia = idTransferencia;

            //    int id = cDatosTransferencias.addDatosTrasferencias(datosTransferencias);
            //    //saveDetalleDatosTransferencias(datosTransferencias, id);

            //}
        }

        public static void saveDetalleDatosTransferencias(datosTransferencias datosTransferencias, int id)
        {
            controladorPedidosTranferencia cPedidosTranferencia = new controladorPedidosTranferencia();
            // cPedidosTranferencia.addPedidosInternos(pedidosInternos);



        }


        [WebMethod]
        public static string getIngredientesRecetaByid(int idReceta)
        {

            try
            {
                Tecnocuisine_API.Controladores.ControladorReceta cReceta = new Tecnocuisine_API.Controladores.ControladorReceta();
                DataTable dtIngredientes = cReceta.getIngredientesRecetaByid(idReceta);
                string ingredientes = "";
                foreach (DataRow dr in dtIngredientes.Rows)
                {
                    string cantidad = dr["cantidad"].ToString();
                    cantidad = cantidad.Replace(",", ".");
                    ingredientes += dr["descripcion"].ToString() + "," + dr["sectorProductivo"].ToString() + "," + cantidad + ";";
                }
                return ingredientes;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenesDeProduccion.aspx", false);
        }
    }
}