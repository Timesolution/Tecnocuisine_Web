using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
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
        string SectorProductivo = "";
        DataTable dt;
        DataTable dt2;
        protected void Page_Load(object sender, EventArgs e)
        {

            CargarSectoresProductivodDDL();
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();

                this.FechaH = (Request.QueryString["FechaH"]).ToString();

                this.SectorProductivo = (Request.QueryString["SP"]).ToString();
            }
            if (FechaD != "")
            {
                FiltrarIngredientesDeOrdenesDeProduccion(this.FechaD, this.FechaH, this.SectorProductivo);
            }
        }

        public void FiltrarIngredientesDeOrdenesDeProduccion(string FechaD, string FechaH, string SectorProductivo)
        {
            try
            {
                this.FechaDesde.Value = this.FechaD;
                this.FechaHasta.Value = this.FechaH;

                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                //ControladorEstadoDeResultado controladorEstadoDeResultado = new ControladorEstadoDeResultado();
                //var dtRecaudacionRubroMensual = controladorEstadoDeResultado.getRecaudacionMensualRubroPorRangoDeFecha(FechaD, FechaH);


                ControladorPreProduccion cPreProduccion = new ControladorPreProduccion();
                dt = cPreProduccion.GetAllIngredientesOrdenesProduccion(SectorProductivo, FechaDesde, FechaHasta);
                dt2 = cPreProduccion.ObtenerTodosLosIngredienteDeTodasLasOrdenes_recetas_recetas(SectorProductivo, FechaDesde, FechaHasta);


                DataTable dtCombined = dt.Clone();
                dtCombined.Merge(dt);
                dtCombined.Merge(dt2);

                // Ordena la DataTable combinada por la columna "Fecha" de mayor a menor
                dtCombined.DefaultView.Sort = "Fecha ASC";
                dtCombined = dtCombined.DefaultView.ToTable(); ;
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
                    celReceta.Text = dr["Producto"].ToString();
                    celReceta.VerticalAlign = VerticalAlign.Middle;
                    celReceta.HorizontalAlign = HorizontalAlign.Left;
                    celReceta.Width = Unit.Percentage(40);
                    celReceta.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celReceta);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = dr["Kilogramos"].ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Right;
                    celCantidad.Width = Unit.Percentage(40);
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);


                    TableCell celAccion = new TableCell();
                    Literal l3 = new Literal();
                    l3.Text = "&nbsp";
                    celAccion.Controls.Add(l3);


                    HyperLink lnkDetalles = new HyperLink();
                    lnkDetalles.CssClass = "btn btn-xs";
                    lnkDetalles.Style.Add("background-color", "transparent");
                    lnkDetalles.Attributes.Add("data-toggle", "tooltip");
                    lnkDetalles.Attributes.Add("title", "Producir");
                    lnkDetalles.ID = "lnk_Receta" + Cont + "_";
                    lnkDetalles.Text = "<span><i style='color:black;' class='fa fa-utensils'></i></span>";
                    lnkDetalles.NavigateUrl = "GenerarProduccion.aspx?PPro=2&PR=" + dr["Producto"].ToString() + "&C=" + dr["Kilogramos"].ToString();
                    lnkDetalles.Target = "_blank"; // Esta línea abre el enlace en una nueva pestaña
                    celAccion.Controls.Add(lnkDetalles);

                
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

        protected void DetalleIngredientes_Click(object sender, EventArgs e)
        {
            Session["dtProductoOrdenes"] = dt;
            Session["dtProductoRecetas"] = dt2;
            //Response.Redirect("DetalleIngredientes.aspx", false);


            string url = "DetalleIngredientes.aspx";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, GetType(), "openNewTab", script, true);
        }
    }
}