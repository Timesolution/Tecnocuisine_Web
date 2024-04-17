using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                string idsQueryString = Request.QueryString["ids"]; //Esta variable obtiene todos los ids de todas las ordenes de produccion seleccionadas
                int Nivel = 1;
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();

                //Esta funcion obtiene todas los ingredientes de todas las ordenes de produccion de nivel 1
                dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccionGroupBySector(idsQueryString);


                DataTable dtCopia = cOrdenDeProduccion.getRecipesFromSelectedOrders(idsQueryString);

                foreach (DataRow dr in dtCopia.Rows)
                {
                    DataTable dtingredietesNivel1 = obtenerIngredientesReceta(Convert.ToInt32(dr["id"].ToString()), dr["OPNumero"].ToString(), dr["Producto"].ToString(), Convert.ToInt32(dr["idReceta"].ToString()),
                        dr["cantidad"].ToString(), dr["fechaEntrega"].ToString(), dr["descripcion1"].ToString());
                    //aca tengo que obtener el set de datos necesito 
                    DataTable dtSubRecetas = obtenerSubRecetasOrdenes(Convert.ToInt32(dr["id"].ToString()), dr["OPNumero"].ToString(), dr["Producto"].ToString(), Convert.ToInt32(dr["idReceta"].ToString()),
                          dr["cantidad"].ToString(), dr["fechaEntrega"].ToString(), dr["descripcion1"].ToString());


                    foreach (DataRow drNivel1 in dtSubRecetas.Rows)
                    {
                        DataTable dtingredietesNivel2 = obtenerIngredientesReceta(Convert.ToInt32(drNivel1["idOrdenProduccion"].ToString()), drNivel1["OPNumero"].ToString(), drNivel1["descripcion"].ToString(), Convert.ToInt32(drNivel1["idProductoOReceta"].ToString()),
                        drNivel1["cantidad"].ToString(), drNivel1["FechaEntregaOrden"].ToString(), drNivel1["sectorProductivo"].ToString());

                        DataTable dtSubrecetasNivel2 = obtenerSubRecetasOrdenes(Convert.ToInt32(drNivel1["idOrdenProduccion"].ToString()), drNivel1["OPNumero"].ToString(), drNivel1["descripcion"].ToString(), Convert.ToInt32(drNivel1["idProductoOReceta"].ToString()),
                        drNivel1["cantidad"].ToString(), drNivel1["FechaEntregaOrden"].ToString(), drNivel1["sectorProductivo"].ToString());

                        foreach (DataRow drNivel2 in dtSubrecetasNivel2.Rows)
                        {
                            DataTable dtingredietesNivel3 = obtenerIngredientesReceta(Convert.ToInt32(drNivel2["idOrdenProduccion"].ToString()), drNivel2["OPNumero"].ToString(), drNivel2["descripcion"].ToString(), Convert.ToInt32(drNivel2["idProductoOReceta"].ToString()),
                            drNivel2["cantidad"].ToString(), drNivel2["FechaEntregaOrden"].ToString(), drNivel2["sectorProductivo"].ToString());

                            DataTable dtSubrecetasNivel3 = obtenerSubRecetasOrdenes(Convert.ToInt32(drNivel2["idOrdenProduccion"].ToString()), drNivel2["OPNumero"].ToString(), drNivel2["descripcion"].ToString(), Convert.ToInt32(drNivel2["idProductoOReceta"].ToString()),
                            drNivel2["cantidad"].ToString(), drNivel2["FechaEntregaOrden"].ToString(), drNivel2["sectorProductivo"].ToString());
                        }
                    }
                }




                //dtOrdenDeProduccionRecetas_recetas = cOrdenDeProduccion.GetAllIngredientesRecetasOrdenesProduccionGroupBySector(idsQueryString);
                //dtGlobal.DefaultView.Sort = "SectorProductivo ASC";
                //dtGlobal = dt.DefaultView.ToTable();


                DataView dv = dtGlobal.DefaultView;
                dv.Sort = "sectorProductivo ASC, fechaProducto ASC";

                // Asignar la vista ordenada de nuevo a la DataTable
                dtGlobal = dv.ToTable();
                //Esta funcion obtiene las fechas de entrega de todas las ordenes de produccion


                separarSectoresEnTablas();


                // fechaOrdenDeProduccion = obtenerFechaOrden(idsQueryString);
                // dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);

                obtenerOPNumeros(idsQueryString);

            }
            catch (Exception ex)
            {
            }

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




            DataTable cocinaCalienteTable = sectorTables["COCINA CALIENTE"];
            DataTable carniceriaTable = sectorTables["CARNICERIA"];
            DataTable verduleriaTable = sectorTables["VERDULERIA"];
            DataTable ALMACENTable = sectorTables["ALMACEN"];



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


        public void obtenerIngredientesReceta()
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();

        }


        public DataTable obtenerSubRecetasOrdenes(int id, string OPNumero, string Producto, int idReceta, string cantidad, string fechaEntrega, string sectorPadre)
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                DataTable dt = cOrdenDeProduccion.getRecipesFromRecipes(id, OPNumero, Producto, idReceta, cantidad, fechaEntrega, sectorPadre);
                dtGlobal.Merge(dt);
                return dt;

            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public DataTable obtenerIngredientesReceta(int id, string OPNumero, string Producto, int idReceta, string cantidad, string fechaEntrega, string SectorPadre)
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                DataTable dt = cOrdenDeProduccion.getIngredientsRecipes(id, OPNumero, Producto, idReceta, cantidad, fechaEntrega, SectorPadre);
                dtGlobal.Merge(dt);
                return dt;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //public void cambiarEstadoDeLaOrdenSeleccionada()
        //{
        //    ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
        //    string idOrdenDeProduccion = Request.QueryString["ids"];
        //    cOrdenDeProduccion.cambiarEstadoOrden(idOrdenDeProduccion);
        //}

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
        public static int cambiarEstadoDeLaOrden(string id, int estadoOrden)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            cOrdenDeProduccion.cambiarEstadoOrden(id, estadoOrden);

            return 1;
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