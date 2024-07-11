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
using Tecnocuisine.Formularios.Ventas;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static System.Net.Mime.MediaTypeNames;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class PagosRealizados : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        ControladorCliente controladorCliente = new ControladorCliente();
        ControladorProveedores controladorProveedores = new ControladorProveedores();
        ControladorVentas controladorVentas = new ControladorVentas();
        ControladorCobrosRealizados controladorCobrosRealizados = new Tecnocuisine_API.Controladores.ControladorCobrosRealizados();
        ControladorPagoRealizados controladorPagoRealizados = new ControladorPagoRealizados();
        Gestion_Api.Controladores.controladorArticulo controladorArticulo = new Gestion_Api.Controladores.controladorArticulo();
        Gestion_Api.Controladores.ControladorArticulosEntity contArtEnt = new Gestion_Api.Controladores.ControladorArticulosEntity();
        int accion;
        int idProducto;
        int Mensaje;
        int idClientes = -1;
        string FechaD = "";
        string FechaH = "";




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idClientes = Convert.ToInt32(Request.QueryString["p"]);
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }

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
            if (idClientes != -1)
            {
                FiltrarVentas(this.idClientes, ConvertDateFormat(this.FechaD), ConvertDateFormat(this.FechaH));
            }else
            {
                DateTime fechaConvertida = DateTime.Now;
                int idcli = 0;
                string fechafinal = fechaConvertida.ToString("MM/dd/yyyy"); ;

                FiltrarVentas(idcli, fechafinal, fechafinal);
            }
            CargarProveedores();
            //ObtenerRecetas();
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
        private void CargarProveedores()
        {
            try
            {

                var allTipos = controladorProveedores.ObtenerProveedoresAll();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in allTipos)
                {

                    builder.Append(String.Format("<option value='{0}' id='" + rec.Id + "'>", rec.Id + " - " + rec.Alias));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListProveedores.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        public void FiltrarVentas(int idCli, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Proveedores prov =  controladorProveedores.ObtenerProveedorByID(idCli);
                if (prov == null)
                {
                    AliasCliente.Value = "Todos";
                }
                else
                {
                    AliasCliente.Value = prov.Alias;

                }
                string FechaDesde = ConvertirFecha(FechaD);
                string FechaHasta = ConvertirFecha(FechaH);

                this.FechaDesde.Value = this.FechaD;
                this.FechaHasta.Value = this.FechaH;

                var dt = controladorPagoRealizados.FiltrarPagosRealizados(FechaD, FechaH, idCli);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.PagosRealizados vd = new Tecnocuisine_API.Entitys.PagosRealizados();
                    vd.id = Convert.ToInt32(row["id"]);
                    vd.fecha = ((DateTime)row["fecha"]);
                    vd.idProveedor = Convert.ToInt32(row["idProveedor"]);
                    vd.numeroRecibo = row["numeroRecibo"].ToString(); ;
                    vd.importe = Convert.ToDecimal(row["importe"]);

                    total += (decimal)(vd.importe);

                    CargarVentasPH2(vd, prov);


                }
                SaldoTotal.Value = FormatearNumero(total);
            }
            catch (Exception ex) { }
        }
        public static string ConvertirFecha(string fecha)
        {
            // Convertir a objeto DateTime
            DateTime fechaDateTime = DateTime.ParseExact(fecha, "MM/dd/yyyy", null);

            // Obtener la fecha en formato "anio-dia-mes"
            string fechaNueva = fechaDateTime.ToString("yyyy-dd-MM");

            // Retornar la fecha nueva
            return fechaNueva;
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


        //public void ObtenerTodosCobros()
        //{
        //    try
        //    {
                
        //        var ListVentas = controladorCobrosRealizados.();
        //        decimal total = 0;
        //        foreach (var ventas in ListVentas)
        //        {
        //            DateTime date = (DateTime)ventas.FechaVenta;
        //            if (ventas.FechaVenta != null && DateTime.Today == date.Date)
        //            CargarVentasPH(ventas);
        //            total += (decimal)(ventas.PrecioVentaTotal);
        //        }
        //        SaldoTotal.Value = FormatearNumero(total);

        //    }


        //    catch (Exception)
        //    {

        //    }
        //}


        public void CargarVentasPH2(Tecnocuisine_API.Entitys.PagosRealizados cobro, Tecnocuisine_API.Entitys.Proveedores prov)
        {
            try
            {
                // < th > ID </ th > LISTO
                // < th > Fecha </ th > LISTO
                // < th > Numero </ th > LISTO
                // < th > Costo </ th > LISTO
                // < th > Venta </ th >
                TableRow tr = new TableRow();
                tr.ID = cobro.id.ToString();
                TableCell celID = new TableCell();
                celID.Text = "<span> " + cobro.id.ToString() + "</span>";
                celID.VerticalAlign = VerticalAlign.Middle;
                celID.Style.Add("text-align", "right");
                tr.Cells.Add(celID);

                TableCell celFechaVenta = new TableCell();
                celFechaVenta.Text = "<span> " + cobro.fecha.ToString().Split(' ')[0] + "</span>";
                celFechaVenta.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaVenta);

                TableCell celCliente = new TableCell();
                if (prov == null && cobro.idProveedor != null)
                {
                    Tecnocuisine_API.Entitys.Proveedores prov2 = controladorProveedores.ObtenerProveedorByID((int)cobro.idProveedor);
                    string cli = prov2.Alias;
                    celCliente.Text = "<span> " + cli + "</span>";
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celCliente);
                }
                else if (prov == null && cobro.idProveedor == null)
                {
                    string cli = (cobro.idProveedor == null ? "ProveedorNoDisp" : prov.Alias);
                    celCliente.Text = "<span> " + cli + "</span>";
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celCliente);
                }
                else
                {

                    celCliente.Text = "<span> " + prov.Alias + "</span>";
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celCliente);
                }



                TableCell celFormaDePago = new TableCell();
                string formapago = (cobro.numeroRecibo == null ? "Numero Recibo No Disp" : cobro.numeroRecibo);
                celFormaDePago.Text = "<span> " + formapago + "</span>";
                celFormaDePago.VerticalAlign = VerticalAlign.Middle;
                celFormaDePago.Attributes.Add("style", "text-align:right;");
                tr.Cells.Add(celFormaDePago);

                TableCell celTipoFactura = new TableCell();
                string tipofac = (cobro.importe == null ? "TipoDoc No Disp" : FormatearNumero((decimal)cobro.importe));
                celTipoFactura.Text = "<span> " + tipofac + "</span>";
                celTipoFactura.Attributes.Add("style", "text-align:right;");
                celTipoFactura.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoFactura);



                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = "BtnEdit_" + cobro.id.ToString() + "_";

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("href", "../../Formularios/Ventas/VentaDetallada.aspx?t=1&i=" + cobro.id);
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
                celFechaVenta.Text = "<span> " + venta.FechaVenta.ToString().Split(' ')[0] + "</span>";
                celFechaVenta.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaVenta);

                TableCell celCliente = new TableCell();
                string cli = (venta.idCliente == null ? "ProvNoDisp" : venta.Clientes.alias);
                celCliente.Text = "<span> " + cli + "</span>";
                celCliente.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCliente);

 
                TableCell celFormaDePago = new TableCell();
                string formapago = (venta.FormaPago == null ? "Forma de Pago No Disp" : venta.FormaPago);
                celFormaDePago.Text = "<span> " + formapago + "</span>";
                celFormaDePago.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFormaDePago);

                TableCell celTipoFactura = new TableCell();
                string tipofac = (venta.TipoDocumento == null ? "TipoDoc No Disp" : venta.TipoDocumento.Descripcion);
                celTipoFactura.Text = "<span> " + tipofac + "</span>";
                celTipoFactura.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoFactura);

                TableCell celNumero = new TableCell();
                celNumero.Text = "#"+venta.NumeroVenta;
                celNumero.Style.Add("text-align", "right");
                celNumero.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celNumero);

                TableCell celCosto = new TableCell();
                celCosto.Text = "$" + venta.CostoTotal.ToString().Replace(',','.');
                celCosto.Style.Add("text-align", "right");
                celCosto.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCosto);

                TableCell celPrecioVenta = new TableCell();
                celPrecioVenta.Text = "$" + FormatearNumero((decimal)venta.PrecioVentaTotal);
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