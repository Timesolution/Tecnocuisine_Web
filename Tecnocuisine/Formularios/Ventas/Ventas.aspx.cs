using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Compras;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static System.Net.Mime.MediaTypeNames;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class Ventas : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        Gestion_Api.Controladores.controladorArticulo controladorArticulo = new Gestion_Api.Controladores.controladorArticulo();
        Gestion_Api.Controladores.ControladorArticulosEntity contArtEnt = new Gestion_Api.Controladores.ControladorArticulosEntity();
        int accion;
        int idProducto;
        int Mensaje;




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);


            if (!IsPostBack)
            {


                //txtDescripcionAtributo.Attributes.Add("disabled", "disabled");
                //txtDescripcionCategoria.Attributes.Add("disabled", "disabled");

                //CargarUnidadesMedida();
                //CargarAlicuotasIVA();

                //cargarNestedListAtributos();
                if (accion == 2)
                {
                    //btnPresentacion.Attributes.Add("data-action", "editPresentation");
                    //CargarProducto();
                }

                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 2)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 3)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

            }
            ObtenerTodasVentas();
            //ObtenerRecetas();
        }

        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Account/Login.aspx");
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


        public void ObtenerTodasVentas()
        {
            try
            {
                ControladorVentas controladorventas = new ControladorVentas();
                var ListVentas = controladorventas.ObtenerTodasLasVentas();
                foreach (var ventas in ListVentas)
                {
                    CargarVentasPH(ventas);
                }

            }


            catch (Exception)
            {

            }
        }
        public void CargarVentasPH(VentasDetalle venta)
        {
            try
            {
                 // < th > ID </ th > LISTO
                 // < th > Fecha </ th > LISTO
                 // < th > Numero </ th > LISTO
                 // < th > Costo </ th > LISTO
                 // < th > Venta </ th >
                TableRow tr = new TableRow();
                tr.ID = venta.id.ToString();
                TableCell celID = new TableCell();
                celID.Text = "<span> " + venta.id.ToString() + "</span>";
                celID.VerticalAlign = VerticalAlign.Middle;
                celID.Style.Add("text-align", "right");
                tr.Cells.Add(celID);

                TableCell celFechaVenta = new TableCell();
                celFechaVenta.Text = "<span> " + venta.FechaVenta.ToString() + "</span>";
                celFechaVenta.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaVenta);

                TableCell celNumero = new TableCell();
                celNumero.Text = venta.NumeroVenta;
                celNumero.Style.Add("text-align", "right");
                celNumero.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celNumero);

                TableCell celCosto = new TableCell();
                celCosto.Text = "$" + venta.CostoTotal.ToString();
                celCosto.Style.Add("text-align", "right");
                celCosto.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCosto);

                TableCell celPrecioVenta = new TableCell();
                celPrecioVenta.Text = "$" + venta.PrecioVentaTotal.ToString();
                celPrecioVenta.VerticalAlign = VerticalAlign.Middle;
                celPrecioVenta.Style.Add("text-align", "right");
                tr.Cells.Add(celPrecioVenta);



                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = "BtnEdit_" + venta.id.ToString() + "_";

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("href", "VentaDetallada.aspx?t=1&i=" + venta.id);
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                celAction.Controls.Add(btnEditar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phProductosyRecetas.Controls.Add(tr);

            }
            catch (Exception)
            {

            }

        }




    }
}