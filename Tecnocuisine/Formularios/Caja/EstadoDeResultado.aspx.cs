using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Caja
{
    public partial class EstadoDeResutaldo : System.Web.UI.Page
    {
        string FechaD = "";
        string FechaH = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();

                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }

            if (FechaD != "")
            {
                FiltrarVentas(this.FechaD, this.FechaH);
            }
        }


        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Usuario/Login.aspx");
                }
                else
                {
                    if (this.verificarAcceso() != 1)
                    {
                        Response.Redirect("/Default.aspx?m=1", false);
                    }
                }
            }
            catch
            {
                Response.Redirect("../../Account/Login.aspx");
            }
        }

        private int verificarAcceso()
        {
            try
            {
                int valor = 1;
                string permisos = Session["Login_Permisos"] as string;
                string[] listPermisos = permisos.Split(';');

                string permiso = listPermisos.Where(x => x == "215").FirstOrDefault();

                if (!string.IsNullOrEmpty(permiso))
                    valor = 1;

                return valor;
            }
            catch
            {
                return -1;
            }
        }

        public void FiltrarVentas(string FechaD, string FechaH)
        {
            try
            {
                this.FechaDesde.Value = this.FechaD;
                this.FechaHasta.Value = this.FechaH;

                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                ControladorEstadoDeResultado controladorEstadoDeResultado = new ControladorEstadoDeResultado();
                var dtRecaudacionRubroMensual = controladorEstadoDeResultado.getRecaudacionMensualRubroPorRangoDeFecha(FechaD, FechaH);

                CargarRecaudacionMensualRubroEnPH2(dtRecaudacionRubroMensual);






            }
            catch (Exception ex) { }
        }

        public void CargarRecaudacionMensualRubroEnPH2(DataTable dtRecaudacionRubroMensual)
        {
            try
            {
                foreach (DataRow dr in dtRecaudacionRubroMensual.Rows)
                {

                    //fila
                    TableRow tr = new TableRow();
                    tr.ID = dr["id"].ToString();

                    TableCell celTipoRubro = new TableCell();
                    celTipoRubro.Text = dr["Rubro"].ToString();
                    celTipoRubro.VerticalAlign = VerticalAlign.Middle;
                    celTipoRubro.HorizontalAlign = HorizontalAlign.Left;
                    celTipoRubro.Width = Unit.Percentage(40);   
                    celTipoRubro.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celTipoRubro);

                    TableCell celRecaudacionMensualRubro = new TableCell();
                    celRecaudacionMensualRubro.Text = dr["RecaudacionMensual"].ToString();
                    celRecaudacionMensualRubro.VerticalAlign = VerticalAlign.Middle;
                    celRecaudacionMensualRubro.HorizontalAlign = HorizontalAlign.Right;
                    celRecaudacionMensualRubro.Width = Unit.Percentage(40);
                    celRecaudacionMensualRubro.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celRecaudacionMensualRubro);


                    TableCell celReal = new TableCell();
                    celReal.Text = "0";
                    celReal.VerticalAlign = VerticalAlign.Middle;
                    celReal.HorizontalAlign = HorizontalAlign.Right;
                    celReal.Width = Unit.Percentage(40);
                    celReal.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celReal);


                    TableCell celPlan = new TableCell();
                    celPlan.Text = "0";
                    celPlan.VerticalAlign = VerticalAlign.Middle;
                    celPlan.HorizontalAlign = HorizontalAlign.Right;
                    celPlan.Width = Unit.Percentage(40);
                    celPlan.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celPlan);

                    phRubroRecaudacionMensual.Controls.Add(tr);
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
    }
}