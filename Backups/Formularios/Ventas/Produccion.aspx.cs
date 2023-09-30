using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Modelo;
using Microsoft.Ajax.Utilities;
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
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class Produccion : System.Web.UI.Page
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
            //ObtenerProductos();
            //ObtenerRecetas();
            ObtenerTodasLasProduciones();
        }


        private void ObtenerTodasLasProduciones()
        {
            try
            {
                ControladorVentas controladorVentas = new ControladorVentas();
                var TodasLasVentas = controladorVentas.ObtenerTodasLasVentasProducion();
                if (TodasLasVentas.Count > 0)
                {
                    foreach (var item in TodasLasVentas)
                    {
                        CargarVentasPH(item);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void CargarVentasPH(VentaProducion item)
        {
            try
            {
                ControladorReceta controladorReceta = new ControladorReceta();
                string descp = controladorReceta.ObtenerRecetaId(item.idReceta).descripcion;
                //fila
                TableRow tr = new TableRow();
                tr.ID = item.idReceta.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = item.idReceta.ToString();
                celNumero.Width = Unit.Percentage(5);
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Width = Unit.Percentage(10);
                celDescripcion.Text = descp;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celDescripcion);



                TableCell NumProduc = new TableCell();
                NumProduc.Text = item.NumeroProduccion;
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");
                tr.Cells.Add(NumProduc);

                TableCell FechaProd = new TableCell();
                FechaProd.Text = item.FechaProduccion.ToString();
                FechaProd.VerticalAlign = VerticalAlign.Middle;
                FechaProd.HorizontalAlign = HorizontalAlign.Right;
                FechaProd.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(FechaProd);


                TableCell CantProdu = new TableCell();
                CantProdu.Text = item.CantidadProducida.ToString();
                CantProdu.VerticalAlign = VerticalAlign.Middle;
                CantProdu.HorizontalAlign = HorizontalAlign.Right;
                CantProdu.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");
                tr.Cells.Add(CantProdu);

                TableCell Lote = new TableCell();
                Lote.Text = item.Lote;
                Lote.VerticalAlign = VerticalAlign.Middle;
                Lote.HorizontalAlign = HorizontalAlign.Right;
                Lote.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: center;");
                tr.Cells.Add(Lote);

                    decimal CostoTotal3 = 0;
                if (item.CostoTotal != null)
                {
                    CostoTotal3 = (decimal)item.CostoTotal;
                }
                TableCell CostoTotal = new TableCell();
                CostoTotal.Text = FormatearNumero(CostoTotal3);
                CostoTotal.VerticalAlign = VerticalAlign.Middle;
                CostoTotal.HorizontalAlign = HorizontalAlign.Right;
                CostoTotal.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");
                tr.Cells.Add(CostoTotal);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnEliminar = new LinkButton();
                btnEliminar.CssClass = "btn  btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "../Ventas/ProduccionDetallada.aspx?i=" + item.id);

                //btnEliminar.Attributes.Add("href", "../Compras/CrearVenta.aspx?t=1&i=" + producto.id);
                btnEliminar.Text = "<span data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=\"Ver Detalle Produccion\"><i class='fa fa-search' style=\"color: black\"></i></span>";
                btnEliminar.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; background-color: transparent; padding-top: 12px;");

                btnEliminar.Click += new EventHandler(this.RedirecCrearVenta);
                celAccion.Controls.Add(btnEliminar);

                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);
            }
            catch (Exception)
            {

            }
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


    
        private void RedirecCrearVenta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("CrearVenta.aspx?t=1&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        decimal RevertirNumero(string numeroFormateado)
        {
            string numeroSinComas = numeroFormateado.Replace(",", "");
            decimal numero = decimal.Parse(numeroSinComas, CultureInfo.InvariantCulture);
            return numero;
        }

        string FormatearNumero(decimal numero)
        {
            return numero.ToString("N2", new CultureInfo("en-US"));
        }


    }
}