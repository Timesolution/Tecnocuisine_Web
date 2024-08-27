using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Gestion
{
    public partial class AlertasFoodCost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            CargarTabla();
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
                int valor = 0;
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

        public void CargarTabla()
        {
            try
            {
                var alertas = new ControladorAlertasFoodCost().GetAll();

                foreach (var alerta in alertas)
                {
                    //fila
                    TableRow tr = new TableRow();
                    tr.ID = "Alerta" + alerta.Id.ToString();

                    //Celdas
                    //TableCell celNumero = new TableCell();
                    //celNumero.Text = evolucion.Id.ToString();
                    //celNumero.VerticalAlign = VerticalAlign.Middle;
                    //celNumero.HorizontalAlign = HorizontalAlign.Right;
                    //celNumero.Attributes.Add("style", " text-align: right");
                    //tr.Cells.Add(celNumero);

                    TableCell celFecha = new TableCell();
                    celFecha.Text = alerta.FechaCreacion.ToString();
                    celFecha.VerticalAlign = VerticalAlign.Middle;
                    celFecha.HorizontalAlign = HorizontalAlign.Left;
                    tr.Cells.Add(celFecha);

                    TableCell celDescripcion = new TableCell();
                    celDescripcion.Text = alerta.Recetas.descripcion;
                    celDescripcion.VerticalAlign = VerticalAlign.Middle;
                    celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                    tr.Cells.Add(celDescripcion);

                    TableCell celCostoUnitario = new TableCell();
                    celCostoUnitario.Text = alerta.Recetas.CostoU?.ToString("C");
                    celCostoUnitario.VerticalAlign = VerticalAlign.Middle;
                    celCostoUnitario.HorizontalAlign = HorizontalAlign.Left;
                    celCostoUnitario.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                    tr.Cells.Add(celCostoUnitario);

                    TableCell celPrecioVenta = new TableCell();
                    celPrecioVenta.Text = alerta.Recetas.PrVenta?.ToString("C");
                    celPrecioVenta.VerticalAlign = VerticalAlign.Middle;
                    celPrecioVenta.HorizontalAlign = HorizontalAlign.Left;
                    celPrecioVenta.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                    tr.Cells.Add(celPrecioVenta);

                    TableCell celContMarginal = new TableCell();
                    celContMarginal.Text = alerta.Recetas.CostMarginal?.ToString("C");
                    celContMarginal.VerticalAlign = VerticalAlign.Middle;
                    celContMarginal.HorizontalAlign = HorizontalAlign.Left;
                    celContMarginal.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                    tr.Cells.Add(celContMarginal);

                    TableCell celFoodCost = new TableCell();
                    celFoodCost.Text = alerta.Recetas.PorcFoodCost?.ToString() + " %";
                    celFoodCost.VerticalAlign = VerticalAlign.Middle;
                    celFoodCost.HorizontalAlign = HorizontalAlign.Left;
                    celFoodCost.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                    tr.Cells.Add(celFoodCost);

                    phAlertas.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}