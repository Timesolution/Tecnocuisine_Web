using Gestion_Api.Controladores;
using Gestor_Solution.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Ventas;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class ChequesCobros : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        ControladorTarjetaDeCreditoVenta controladorTarjetaDeCreditoVenta = new ControladorTarjetaDeCreditoVenta();
        ControladorProveedores controladorProveedores = new ControladorProveedores();
        ControladorCheques controladorCheques = new ControladorCheques();
        ControladorCliente controladorCliente = new ControladorCliente();
        ControladorTarjetas controladorTarjetas = new ControladorTarjetas();
        int accion;
        int Mensaje;
        int idClientes = -1;
        string FechaD = "";
        string FechaH = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idClientes = Convert.ToInt32(Request.QueryString["c"]);
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }
            if (!IsPostBack)
            {

                CargarClientes();


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
                FiltrarVentas(this.idClientes, this.FechaD, this.FechaH);
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

        public void FiltrarVentas(int idCli, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes clientes = controladorCliente.ObtenerClienteId(idCli);
                AliasCliente.Value = clientes == null ? "Todos" : clientes.alias;
                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                var dt = controladorCheques.FiltrarCheques(FechaDesde, FechaH, idCli);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.Cheques cheque = new Tecnocuisine_API.Entitys.Cheques();
                    cheque.id = Convert.ToInt32(row["id"]);
                    cheque.Descripcion = row["Descripcion"].ToString();
                    cheque.NumeroCheque = row["NumeroCheque"].ToString();
                    cheque.Importe = Convert.ToDecimal(row["Importe"]);
                    cheque.Numero = row["Numero"].ToString();
                    cheque.idBanco = Convert.ToInt32(row["idBanco"]);
                    cheque.Cuenta = row["Cuenta"].ToString();
                    cheque.cuit = row["cuit"].ToString();
                    cheque.Librador = row["Librador"].ToString();
                    cheque.fecha = ((DateTime)row["fecha"]);
                   
                    if (!row.IsNull("idClientes"))
                    {
                        cheque.idClientes = Convert.ToInt32(row["idClientes"]);
                        // Resto de tu lógica aquí
                    }
                    else
                    {
                        cheque.idProveedor = Convert.ToInt32(row["idProveedor"]);

                    }



                    total += (decimal)(cheque.Importe);

                    CargarInsumosPH2(cheque);


                }
                SaldoTotal.Value = FormatearNumero(total);
            }
            catch (Exception ex) { }
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

        public static string ConvertirFecha(string fecha)
        {
            // Convertir a objeto DateTime
            DateTime fechaDateTime = DateTime.ParseExact(fecha, "MM/dd/yyyy", null);

            // Obtener la fecha en formato "anio-dia-mes"
            string fechaNueva = fechaDateTime.ToString("yyyy-dd-MM");

            // Retornar la fecha nueva
            return fechaNueva;
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

        public void CargarInsumosPH2(Cheques cheque)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = cheque.id.ToString();

                //Celdas

                TableCell celNumero = new TableCell();
                celNumero.Text = cheque.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celFecha = new TableCell();
                celFecha.Text = cheque.fecha.ToString().Split(' ')[0];
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Width = Unit.Percentage(40);
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);

                if (cheque.idClientes != null)
                {

                var cliente = controladorCliente.ObtenerClienteId((int)cheque.idClientes);
                TableCell celEntidad = new TableCell();
                celEntidad.Text = cliente.alias;
                celEntidad.VerticalAlign = VerticalAlign.Middle;
                celEntidad.HorizontalAlign = HorizontalAlign.Left;
                celEntidad.Width = Unit.Percentage(40);
                celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celEntidad);
                } else
                {
                    var prov = controladorProveedores.ObtenerProveedorByID((int)cheque.idProveedor);
                    TableCell celEntidad = new TableCell();
                    celEntidad.Text = prov.Alias;
                    celEntidad.VerticalAlign = VerticalAlign.Middle;
                    celEntidad.HorizontalAlign = HorizontalAlign.Left;
                    celEntidad.Width = Unit.Percentage(40);
                    celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celEntidad);
                }


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = cheque.Descripcion.ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Width = Unit.Percentage(40);
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

               
                TableCell celNumeroCheque = new TableCell();
                celNumeroCheque.Text = cheque.NumeroCheque == null ? "Numero no encontrado" : cheque.NumeroCheque;
                celNumeroCheque.VerticalAlign = VerticalAlign.Middle;
                celNumeroCheque.HorizontalAlign = HorizontalAlign.Left;
                celNumeroCheque.Width = Unit.Percentage(40);
                celNumeroCheque.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNumeroCheque);



                ControladorEntidadesBancarias controladorEntidadesBancarias = new ControladorEntidadesBancarias();
                var banco = controladorEntidadesBancarias.ObtenerBancariasByID((int)cheque.idBanco);


               TableCell celBanco = new TableCell();
                celBanco.Text = banco.Nombre.ToString();
                celBanco.VerticalAlign = VerticalAlign.Middle;
                celBanco.HorizontalAlign = HorizontalAlign.Left;
                celBanco.Width = Unit.Percentage(40);
                celBanco.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celBanco);
             

                TableCell celCuenta = new TableCell();
                celCuenta.Text = cheque.Cuenta.ToString();
                celCuenta.VerticalAlign = VerticalAlign.Middle;
                celCuenta.HorizontalAlign = HorizontalAlign.Left;
                celCuenta.Width = Unit.Percentage(40);
                celCuenta.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCuenta);

                TableCell celCuit = new TableCell();
                celCuit.Text = cheque.cuit;
                celCuit.VerticalAlign = VerticalAlign.Middle;
                celCuit.HorizontalAlign = HorizontalAlign.Left;
                celCuit.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celCuit);

                TableCell celLibrador = new TableCell();
                celLibrador.Text = cheque.Librador;
                celLibrador.VerticalAlign = VerticalAlign.Middle;
                celLibrador.HorizontalAlign = HorizontalAlign.Left;
                celLibrador.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celLibrador);

                TableCell celImporte = new TableCell();
                celImporte.Text = FormatearNumero((decimal)cheque.Importe);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + cheque.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarInsumo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + cheque.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + cheque.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        public void CargarInsumosPH(Cheques cheque)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = cheque.id.ToString();

                //Celdas

                TableCell celNumero = new TableCell();
                celNumero.Text = cheque.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celFecha = new TableCell();
                celFecha.Text = cheque.fecha.ToString().Split(' ')[0];
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Width = Unit.Percentage(40);
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);


                var cliente = controladorCliente.ObtenerClienteId((int)cheque.idClientes);
                TableCell celEntidad = new TableCell();
                celEntidad.Text = cliente.alias;
                celEntidad.VerticalAlign = VerticalAlign.Middle;
                celEntidad.HorizontalAlign = HorizontalAlign.Left;
                celEntidad.Width = Unit.Percentage(40);
                celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celEntidad);


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = cheque.Descripcion.ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Width = Unit.Percentage(40);
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);


                TableCell celNumeroCheque = new TableCell();
                celNumeroCheque.Text = cheque.NumeroCheque == null ? "Numero no encontrado" : cheque.NumeroCheque;
                celNumeroCheque.VerticalAlign = VerticalAlign.Middle;
                celNumeroCheque.HorizontalAlign = HorizontalAlign.Left;
                celNumeroCheque.Width = Unit.Percentage(40);
                celNumeroCheque.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNumeroCheque);



                ControladorEntidadesBancarias controladorEntidadesBancarias = new ControladorEntidadesBancarias();
                var banco = controladorEntidadesBancarias.ObtenerBancariasByID((int)cheque.idBanco);


                TableCell celBanco = new TableCell();
                celBanco.Text = banco.Nombre.ToString();
                celBanco.VerticalAlign = VerticalAlign.Middle;
                celBanco.HorizontalAlign = HorizontalAlign.Left;
                celBanco.Width = Unit.Percentage(40);
                celBanco.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celBanco);


                TableCell celCuenta = new TableCell();
                celCuenta.Text = cheque.Cuenta.ToString();
                celCuenta.VerticalAlign = VerticalAlign.Middle;
                celCuenta.HorizontalAlign = HorizontalAlign.Left;
                celCuenta.Width = Unit.Percentage(40);
                celCuenta.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCuenta);

                TableCell celCuit = new TableCell();
                celCuit.Text = cheque.cuit;
                celCuit.VerticalAlign = VerticalAlign.Middle;
                celCuit.HorizontalAlign = HorizontalAlign.Left;
                celCuit.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celCuit);

                TableCell celLibrador = new TableCell();
                celLibrador.Text = cheque.Librador;
                celLibrador.VerticalAlign = VerticalAlign.Middle;
                celLibrador.HorizontalAlign = HorizontalAlign.Left;
                celLibrador.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celLibrador);

                TableCell celImporte = new TableCell();
                celImporte.Text = FormatearNumero((decimal)cheque.Importe);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + cheque.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarInsumo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + cheque.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + cheque.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        public static double RevertirNumero(string numeroFormateado)
        {
            double numero = double.Parse(numeroFormateado.Replace(",", ""));
            return numero;
        }

        public static string FormatearNumero(decimal numero)
        {
            return numero.ToString("N2", new CultureInfo("en-US"));
        }
        protected void editarInsumo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("InsumosF.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }


        public void LimpiarCampos()
        {
            try
            {
                //txtDescripcionInsumo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

      
    }
}