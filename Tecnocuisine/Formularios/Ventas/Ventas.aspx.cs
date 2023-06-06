using Gestion_Api.Entitys;
using Gestion_Api.Entitys.ModeloImportacion;
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
        ControladorCliente controladorCliente = new ControladorCliente();
        ControladorVentas controladorVentas = new ControladorVentas();
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
                this.idClientes = Convert.ToInt32(Request.QueryString["c"]);
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
            ObtenerTodasVentas();
            }
            CargarClientes();
            //ObtenerRecetas();
        }

        private string ConvertDateFormat(string fecha)
        {
            DateTime fechaConvertida;

            if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaConvertida))
            {
                return fechaConvertida.ToString("MM/dd/yyyy");
            }

            throw new ArgumentException("El formato de fecha proporcionado es inválido.");
        }

        private void CargarClientes()
        {
            try
            {
                ControladorCliente controladorCliente = new ControladorCliente();
                var clientes = controladorCliente.ObtenerTodosClientes();


                if (clientes.Count > 0)
                {
                    CargarClientesOptions(clientes);
                    //foreach (var item in Recetas)
                    //{

                    //    CargarRecetasPHModal(item);

                    //}
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarClientesOptions(List<Tecnocuisine_API.Entitys.Clientes> clientes)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();
                builder.Append("<option value='0 - Todos'>0 - Todos</option>");
                foreach (var cli in clientes)
                {
                    builder.Append(String.Format("<option value='{0}' id='c_r_" + cli.id + "_" + cli.alias + "_" + cli.cuit + "'>", cli.id + " - " + cli.alias));
                }
               
                    

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListClientes.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }
        public void FiltrarVentas(int idCli, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes clientes = controladorCliente.ObtenerClienteId(idCli);
                if (clientes == null) {
                    AliasCliente.Value = "Todos";
                } else
                {
                AliasCliente.Value = clientes.alias;
                }
                string FechaDesde = ConvertirFecha(FechaD);
                string FechaHasta = ConvertirFecha(FechaH);
                var dt = controladorVentas.FiltrarVentas(FechaD, FechaH, idCli);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.VentasDetalle vd = new Tecnocuisine_API.Entitys.VentasDetalle();
                    vd.id = Convert.ToInt32(row["id"]);
                    vd.NumeroVenta = row["NumeroVenta"].ToString();
                    vd.FechaVenta = ((DateTime)row["FechaVenta"]);
                    vd.CostoTotal = Convert.ToDecimal(row["CostoTotal"]);
                    vd.PrecioVentaTotal = Convert.ToDecimal(row["PrecioVentaTotal"]);
                    vd.CantidadTotal = Convert.ToInt32(row["CantidadTotal"]);
                    if (!Convert.IsDBNull(row["idCliente"]))
                    {
                    vd.idCliente = Convert.ToInt32(row["idCliente"]);
                    }
                    if (!Convert.IsDBNull(row["idTipoFactura"]))
                    {
                    vd.idTipoFactura = Convert.ToInt32(row["idTipoFactura"]);
                    }
                    vd.NumeroFactura = row["NumeroFactura"].ToString();
                    vd.FormaPago = row["FormaPago"].ToString();



                    total += (decimal)(vd.PrecioVentaTotal);

                    CargarVentasPH2(vd, clientes);


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


        public void ObtenerTodasVentas()
        {
            try
            {
                ControladorVentas controladorventas = new ControladorVentas();
                var ListVentas = controladorventas.ObtenerTodasLasVentas();
                decimal total = 0;
                foreach (var ventas in ListVentas)
                {
                    DateTime date = (DateTime)ventas.FechaVenta;
                    if (ventas.FechaVenta != null && DateTime.Today == date.Date)
                    {
                    CargarVentasPH(ventas);
                    total += (decimal)(ventas.PrecioVentaTotal);
                    }
                    
                }
                SaldoTotal.Value = FormatearNumero(total);

            }


            catch (Exception)
            {

            }
        }


        public void CargarVentasPH2(VentasDetalle venta, Tecnocuisine_API.Entitys.Clientes cliente)
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
                if (cliente == null && venta.idCliente != null)
                {
                 Tecnocuisine_API.Entitys.Clientes cliente2 = controladorCliente.ObtenerClienteId((int)venta.idCliente);
                    string cli = cliente2.alias;
                    celCliente.Text = "<span> " + cli + "</span>";
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celCliente);
                } else if (cliente == null && venta.idCliente == null)
                {
                string cli = (venta.idCliente == null ? "ClienteNoDisp" : cliente.alias);
                celCliente.Text = "<span> " + cli + "</span>";
                celCliente.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCliente);
                } else
                {
                  
                    celCliente.Text = "<span> " + cliente.alias + "</span>";
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celCliente);
                }


                TableCell celFormaDePago = new TableCell();
                string formapago = (venta.FormaPago == null ? "Forma de Pago No Disp" : venta.FormaPago);
                celFormaDePago.Text = "<span> " + formapago + "</span>";
                celFormaDePago.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFormaDePago);

                ControladorTipoDocumento controladorTipoDocumento = new ControladorTipoDocumento();
                var doc = controladorTipoDocumento.ObtenerTipoDocumentoByID((int)venta.idTipoFactura);
                TableCell celTipoFactura = new TableCell();
                string tipofac = (venta.idTipoFactura == null ? "TipoDoc No Disp" : doc.Descripcion);
                celTipoFactura.Text = "<span> " + tipofac + "</span>";
                celTipoFactura.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoFactura);

                TableCell celNumero = new TableCell();
                celNumero.Text = "#" + venta.NumeroVenta;
                celNumero.Style.Add("text-align", "right");
                celNumero.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celNumero);

                TableCell celCosto = new TableCell();
                celCosto.Text = "$" + venta.CostoTotal.ToString().Replace(',', '.');
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
                string cli = (venta.idCliente == null ? "ClienteNoDisp" : venta.Clientes.alias);
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