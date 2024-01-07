using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class Pre_Produccion : System.Web.UI.Page
    {

        ControladorSectorProductivo sectorProductivo = new ControladorSectorProductivo();
        string FechaD = "";
        string FechaH = "";
        int idSectorProductivo = -1;
        DataTable dt;
        DataTable dt2;
        protected void Page_Load(object sender, EventArgs e)
        {

            CargarSectoresProductivodDDL();
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();

                this.FechaH = (Request.QueryString["FechaH"]).ToString();

                this.idSectorProductivo = Convert.ToInt32(Request.QueryString["SP"]);
            }
            if (FechaD != "")
            {
                FiltrarIngredientesDeOrdenesDeProduccion(this.FechaD, this.FechaH, this.idSectorProductivo);
            }
        }

        public void FiltrarIngredientesDeOrdenesDeProduccion(string FechaD, string FechaH, int idSectorProductivo)
        {
            try
            {
                this.FechaDesde.Value = this.FechaD;
                this.FechaHasta.Value = this.FechaH;
                this.ddlSectorSelecionado.Value = idSectorProductivo.ToString();

                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                //ControladorEstadoDeResultado controladorEstadoDeResultado = new ControladorEstadoDeResultado();
                //var dtRecaudacionRubroMensual = controladorEstadoDeResultado.getRecaudacionMensualRubroPorRangoDeFecha(FechaD, FechaH);


                ControladorPreProduccion cPreProduccion = new ControladorPreProduccion();
                dt = cPreProduccion.GetAllIngredientesOrdenesProduccion(idSectorProductivo, FechaDesde, FechaHasta);
                //dt = cPreProduccion.ObtenerTodosLosIngredienteDeTodasLasOrdenes_recetas_recetas(idSectorProductivo, FechaDesde, FechaHasta);


                DataTable dtCombined = dt.Clone();
                dtCombined.Merge(dt);
                // dtCombined.Merge(dt2);

                // Ordena la DataTable combinada por la columna "Fecha" de mayor a menor
                dtCombined.DefaultView.Sort = "Fecha ASC";
                dtCombined = dtCombined.DefaultView.ToTable();
                CargarIngredientesFiltradosEnPH2(dtCombined);

            }
            catch (Exception ex) { }
        }

        public void CargarIngredientesFiltradosEnPH2(DataTable dt)
        {
            try
            {

                int Cont = 0; //Esta variable contadora la uso para los ids de las filas 
                foreach (DataRow dr in dt.Rows)
                {
                    Cont++;
                    //fila
                    TableRow tr = new TableRow();
                    tr.ID = Cont.ToString();


                    TableCell celFecha = new TableCell();
                    celFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy");
                    celFecha.VerticalAlign = VerticalAlign.Middle;
                    celFecha.HorizontalAlign = HorizontalAlign.Left;
                    celFecha.Width = Unit.Percentage(40);
                    celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celFecha);


                    TableCell celReceta = new TableCell();
                    celReceta.Text = dr["descripcion"].ToString();
                    celReceta.VerticalAlign = VerticalAlign.Middle;
                    celReceta.HorizontalAlign = HorizontalAlign.Left;
                    celReceta.Width = Unit.Percentage(40);
                    celReceta.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celReceta);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = dr["cantidad"].ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Right;
                    celCantidad.Width = Unit.Percentage(40);
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);


                    TableCell celEstado = new TableCell();
                    celEstado.Text = "Pendiente";
                    celEstado.VerticalAlign = VerticalAlign.Middle;
                    celEstado.HorizontalAlign = HorizontalAlign.Left;
                    celEstado.Width = Unit.Percentage(40);
                    celEstado.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celEstado);


                    TableCell celSectorProductivo = new TableCell();
                    celSectorProductivo.Text = dr["sectorProductivo"].ToString();
                    celSectorProductivo.VerticalAlign = VerticalAlign.Middle;
                    celSectorProductivo.HorizontalAlign = HorizontalAlign.Left;
                    celSectorProductivo.Width = Unit.Percentage(40);
                    celSectorProductivo.Attributes.Add("style", "display: none; padding-bottom: 1px !important;");
                    tr.Cells.Add(celSectorProductivo);



                    TableCell celidReceta = new TableCell();
                    celidReceta.Text = dr["id"].ToString();
                    celidReceta.VerticalAlign = VerticalAlign.Middle;
                    celidReceta.HorizontalAlign = HorizontalAlign.Left;
                    celidReceta.Width = Unit.Percentage(40);
                    celidReceta.Attributes.Add("style", "display: none; padding-bottom: 1px !important;");
                    tr.Cells.Add(celidReceta);


                    TableCell celAccion = new TableCell();
                    Literal l3 = new Literal();
                    l3.Text = "&nbsp";
                    celAccion.Controls.Add(l3);

                    if (dr["ProducoOreceta"].ToString() != "Producto")
                    {
                        CheckBox chkSeleccionar = new CheckBox();
                        chkSeleccionar.ID = "chkSeleccionar_" + Cont.ToString();
                        chkSeleccionar.Attributes.Add("onchange", "javascript:return verIngredientesReceta('ContentPlaceHolder1_" + chkSeleccionar.ID + "', '" + dr["id"].ToString() + "', '" + dr["cantidad"].ToString() + "', '" + dr["descripcion"].ToString() + "', '" + dr["sectorProductivo"].ToString() + "');");
                        celAccion.Controls.Add(chkSeleccionar);
                    }


                    HyperLink lnkDetalles = new HyperLink();
                    lnkDetalles.CssClass = "btn btn-xs";
                    lnkDetalles.Style.Add("background-color", "transparent");
                    lnkDetalles.Attributes.Add("data-toggle", "tooltip");
                    lnkDetalles.Attributes.Add("title", "Producir");
                    lnkDetalles.ID = "btn_Producir_" + Cont + "_";
                    lnkDetalles.Text = "<span><i style='color:black;' class='fa fa-cutlery'></i></span>";
                    lnkDetalles.NavigateUrl = "GenerarProduccion.aspx?PPro=2&PR=" + dr["descripcion"].ToString() + "&C=" + dr["cantidad"].ToString() + "&i=" + dr["id"].ToString();
                    lnkDetalles.Target = "_blank"; // Esta línea abre el enlace en una nueva pestaña
                    celAccion.Controls.Add(lnkDetalles);


                    HyperLink btnStock = new HyperLink();
                    btnStock.CssClass = "btn btn-xs";
                    btnStock.Style.Add("background-color", "transparent");
                    btnStock.Attributes.Add("data-toggle", "tooltip");
                    btnStock.Attributes.Add("title", "Ver stock");
                    btnStock.ID = "lnk_Receta" + Cont + "_";
                    btnStock.Text = "<span><i style='color:black;' class='fa fa-list'></i></span>";
                    btnStock.NavigateUrl = "/Formularios/Maestros/StockDetallado.aspx?t=1&i=" + dr["id"].ToString();
                    btnStock.Target = "_blank"; // Esta línea abre el enlace en una nueva pestaña
                    celAccion.Controls.Add(btnStock);



                    LinkButton btnIngrediente = new LinkButton();
                    btnIngrediente.ID = "btnVerStock_" + Cont;
                    btnIngrediente.CssClass = "btn  btn-xs";
                    btnIngrediente.Text = $"<a data-toggle=\"tooltip\" data-placement=\"top\" title=\"Ver ingredientes\" onclick=\"verStock('{dr["id"].ToString()}', {Convert.ToInt32(dr["cantidad"])})\"><i class='fa fa-arrows-h'></i></a>";
                    btnIngrediente.Style.Add("background-color", "transparent");
                    // btnEliminar.Style.Add("background-color", "transparent");
                    celAccion.Controls.Add(btnIngrediente);

                    //< button id = "Recepcion" type = "button" class="icon-button" style="float: right; margin-left: 10px;"
                    //                  title="Recepción" onclick="MostrarRecepcion('<%=Cantidad %>', <%=id %>, <%=Nivel %>, <%=DiasMasNivel %>, '<%=descripcionReceta %>', <%=idReceta %>, <%=dt %>)">
                    //                  <i class="fa fa-exchange"></i>
                    //              </button>

                    ControladorReceta cReceta = new ControladorReceta();

                    tr.Cells.Add(celAccion);
                    phIngredientesFiltrados.Controls.Add(tr);

                }
            }
            catch (Exception ex)
            {


            }
        }

        private string ConvertDateFormat(string fecha)
        {

            DateTime fechaConvertida;
            try
            {

                if (DateTime.TryParseExact(fecha, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaConvertida))
                {
                    return fechaConvertida.ToString("MM/dd/yyyy");
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {


                return "";
                throw new ArgumentException("El formato de fecha proporcionado es inválido.");
            }
        }

        protected void CargarSectoresProductivodDDL()
        {
            try
            {
                DataTable dt = sectorProductivo.GetAllSectoresProductivosDT();
                DataRow drSeleccione = dt.NewRow();
                drSeleccione["descripcion"] = "Seleccione...";
                drSeleccione["id"] = -1;
                dt.Rows.InsertAt(drSeleccione, 0);

                // Ordenar los datos alfabéticamente por la descripción (ignorando la primera fila)
                var sortedData = dt.AsEnumerable()
                                   .Skip(1) // Ignorar la primera fila ("Seleccione...")
                                   .OrderBy(row => row.Field<string>("descripcion"));

                // Crear una nueva tabla con los elementos ordenados y el elemento inicial
                DataTable sortedTable = dt.Clone();
                sortedTable.Rows.Add(drSeleccione.ItemArray);
                foreach (DataRow row in sortedData)
                {
                    sortedTable.ImportRow(row);
                }

                this.ddlSector.DataSource = sortedTable;
                this.ddlSector.DataValueField = "id";
                this.ddlSector.DataTextField = "descripcion";

                this.ddlSector.DataBind();

                this.ddlSector.SelectedValue = "-1";

            }
            catch (Exception ex)
            {


            }
        }

        //protected void DetalleIngredientes_Click(object sender, EventArgs e)
        //{
        //    Session["dtProductoOrdenes"] = dt;
        //    Session["dtProductoRecetas"] = dt2;
        //    //Response.Redirect("DetalleIngredientes.aspx", false);


        //    string url = "DetalleIngredientes.aspx";
        //    string script = "window.open('" + url + "', '_blank');";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "openNewTab", script, true);
        //}



        [WebMethod]
        public static string getIngredientesRecetaByIdReceta(int idReceta, decimal cantidad)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.obtenerIngredientePorIdReceta(idReceta, cantidad);
                string ingredientesDeLaReceta = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string Cantidad = dr["Cantidad"].ToString();
                    Cantidad = Cantidad.Replace(",", ".");

                    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + ";";
                }
                return ingredientesDeLaReceta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Esta funcion obtiene los ingredientes de una receta y el sector al que pertenece cada ingrediente
        [WebMethod]
        public static string getIngredientesRecetaByIdRecetaSectorProductivo(int idReceta, decimal cantidad)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.obtenerIngredientePorIdRecetaSectorProductivo(idReceta, cantidad);
                string ingredientesDeLaReceta = "";

                string hola = HttpContext.Current.Session["DatosReceta"] as string;

                foreach (DataRow dr in dt.Rows)
                {
                    string Cantidad = dr["Cantidad"].ToString();
                    Cantidad = Cantidad.Replace(",", ".");

                    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + "," + dr["SectorProductivo"].ToString() + ";";
                }
                return ingredientesDeLaReceta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static string GetProductosEnRecetas(string idsRecetas)
        {
            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idsRecetas));

                List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idsRecetas));
                if (listProd.Count > 0)
                {
                    foreach (var item in listProd)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(item.idProducto);
                        if (stock != null)
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + "," + item.Productos.costo.ToString().Replace(',', '.') + "; ";
                        }
                        else
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + "," + item.Productos.costo.ToString().Replace(',', '.') + ";";

                        }
                    }
                }
                if (listRecetas.Count > 0)
                {
                    foreach (var item in listRecetas)
                    {
                        var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                        if (receta != null)
                        {
                            //item.Recetas.id
                            //item.Recetas.descripcion
                            //item.cantidad
                            //item.Recetas.CostoU


                            var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                            decimal stock;
                            if (CantStock == null)
                            {
                                stock = 0;
                            }
                            else
                            {

                                stock = (decimal)CantStock.stock;
                            }
                            if (item.Recetas.UnidadMedida == null)
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + stock.ToString().Replace(",", ".") + "," + item.cantidad.ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";

                            }
                            else
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                            }
                        }
                    }
                }
                return ListFinal;

            }
            catch (Exception ex)
            {
                return "";
            }
        }


        [WebMethod]
        public static string getRecetasEntrega(string idsRecetas, string recetasSeleccionadas, int idReceta)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getIdrecetasEntrega(idsRecetas, idReceta);
                //string ingredientesDeLaReceta = "";
                string valorCelda = dt.Rows[0][0].ToString();
                valorCelda = valorCelda.TrimEnd(',');
                //idsRecetas = idsRecetas.TrimEnd(',');

                string[] vectorIdsRecetas = valorCelda.Split(',');
                //string[] elementosSeparadosPuntoComa = recetasSeleccionadas.Split(';');

                string[] elementosSeparadosPuntoComa = recetasSeleccionadas.Split(';')
                .Where(elemento => !string.IsNullOrWhiteSpace(elemento))
                .ToArray();


                string nuevaCedena = "";



                //foreach (string idRecetaStr in vectorIdsRecetas)
                //{
                //    int idRecetaInt = Convert.ToInt32(idRecetaStr);
                //    // Ahora puedes trabajar con idRecetaInt

                foreach (string elementoPuntoComa in elementosSeparadosPuntoComa)
                {
                    string[] elementosSeparadosComa = elementoPuntoComa.Split(',');
                    bool existeReceta = false;
                    for (int i = 0; i < vectorIdsRecetas.Length; i++)
                    {
                        if (elementosSeparadosComa[1].ToString() == vectorIdsRecetas[i])
                        {
                            existeReceta = true;
                        }
                    }

                    if (existeReceta == true)
                    {
                        nuevaCedena += elementoPuntoComa + ";";
                    }

                }

                //}

                //string hola = HttpContext.Current.Session["DatosReceta"] as string;

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string Cantidad = dr["Cantidad"].ToString();
                //    Cantidad = Cantidad.Replace(",", ".");

                //    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + "," + dr["SectorProductivo"].ToString() + ";";
                //}
                return nuevaCedena;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static string GetProductosEnRecetas2(string idsRecetas)
        {
            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorReceta cReceta = new ControladorReceta();

                DataTable dt = cReceta.ObtenerProductosByRecetaDT(idsRecetas);
                DataTable dt2 = cReceta.obtenerRecetasbyRecetaDT(idsRecetas);

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                //List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idProd));

                HttpContext.Current.Session["DatosReceta"] = "hola";

                //List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idsRecetas));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(Convert.ToInt32(dr["idProducto"].ToString()));
                        if (stock != null)
                        {
                            //ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["cantidad"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                        }
                        else
                        {
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                        }
                    }
                }
                //if (listRecetas.Count > 0)
                //{
                foreach (DataRow dr in dt2.Rows)
                {
                    //var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                    //if (receta != null)
                    //{
                    //var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                    var CantStock = controladorStockReceta.ObtenerStockReceta(Convert.ToInt32((dr["id"].ToString())));
                    decimal stock;
                    if (CantStock == null)
                    {
                        stock = 0;
                    }
                    else
                    {
                        stock = (decimal)CantStock.stock;
                    }
                    if (dr["UnidadMedida"].ToString() == null)
                    {
                        //  ListFinal += receta.id + "," + receta.descripcion + "," + stock.ToString().Replace(",", ".") + "," + item.cantidad.ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + stock.ToString().Replace(",", ".") + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                    }
                    else
                    {
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32((dr["UnidadMedida"].ToString()))).descripcion + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";
                        //ListFinal += item.Recetas.id + "," + item.Recetas.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                    }
                    //}
                    //}
                }
                return ListFinal;

                //return "hola";

            }
            catch (Exception ex)
            {
                return "";
            }
        }



        [WebMethod]
        public static string getRecepcionEntregaAgrupadoPorSector(string idsRecetas)
        {

            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getIngredientesAndRecetasGruopBySector(idsRecetas);
                  

                dt.DefaultView.Sort = "SectorProductivo ASC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                string listaProductos = "";


                foreach (DataRow dr in sortedDt.Rows)
                {

                    listaProductos += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + dr["unidadMedida"].ToString() + "," + dr["costo"].ToString().Replace(',', '.') + "," + dr["SectorProductivo"].ToString() + "," + dr["id"].ToString() + ";";
                   // dr[].ToString()

                }

                return listaProductos;
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}