using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class StockSectores : System.Web.UI.Page
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
                var sectores = new ControladorSectorProductivo().ObtenerTodosSectorProductivo();

                foreach (var sector in sectores)
                {
                    //fila
                    TableRow tr = new TableRow();
                    //tr.ID = sector.id.ToString();

                    TableCell celCodigo = new TableCell();
                    celCodigo.Text = sector.codigo;
                    celCodigo.VerticalAlign = VerticalAlign.Middle;
                    celCodigo.HorizontalAlign = HorizontalAlign.Left;
                    
                    TableCell celDescripcion = new TableCell();
                    celDescripcion.Text = sector.descripcion;
                    celDescripcion.VerticalAlign = VerticalAlign.Middle;
                    celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                   
                    TableCell celAccion = new TableCell();

                    LinkButton btnDetalle = new LinkButton();
                    btnDetalle.ID = sector.id.ToString();
                    //btnDetalle.CssClass = "btn btn-xs";
                    btnDetalle.Click += new EventHandler(this.VerDetalle);
                    btnDetalle.Text = "<span><i style='color:black' class='fa fa-list'></i></span>";
                    btnDetalle.Attributes.Add("title", "Ver Detalle");
                    //btnDetalle.Attributes.Add("style", "padding-bottom: 1px !important; text-align:end");
                    celAccion.Controls.Add(btnDetalle);

                    tr.Cells.Add(celCodigo);
                    tr.Cells.Add(celDescripcion);
                    tr.Cells.Add(celAccion);

                    phSectores.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void VerDetalle(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                Response.Redirect("StockSectoresDetalle.aspx?id=" + lb.ID);
            }
            catch (Exception Ex)
            {

            }
        }
    }
}