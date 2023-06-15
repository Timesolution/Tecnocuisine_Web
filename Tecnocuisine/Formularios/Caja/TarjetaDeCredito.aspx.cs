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
    public partial class TarjetaDeCredito : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        ControladorTarjetaDeCreditoVenta controladorTarjetaDeCreditoVenta = new ControladorTarjetaDeCreditoVenta();
        ControladorEntidad controladorEntidad = new ControladorEntidad();
        ControladorTarjetas controladorTarjetas = new ControladorTarjetas();
        int accion;
        int Mensaje;
        int idClientes = -1;
        string FechaD = "";
        string FechaH = "";
        int Option = 0;

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
                if (Request.QueryString["Op"] != null)
                {
                   this.Option = Convert.ToInt32(Request.QueryString["Op"]);
                }
            }
            if (!IsPostBack)
            {

                cargarEntidades();
              

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
                FiltrarVentas(this.idClientes, this.FechaD, this.FechaH, this.Option);
            }
            else
            {
                ObtenerInsumos();
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

        public void FiltrarVentas(int idCli, string FechaD, string FechaH,int option = 0)
        {
            try
            {
                Tecnocuisine_API.Entitys.Entidades clientes = controladorEntidad.ObtenerEntidadPorID(idCli);
                AliasCliente.Value = clientes == null ? "Todos" : clientes.descripcion;
                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                var dt = controladorTarjetaDeCreditoVenta.FiltrarCuentaCorrienteVentas(FechaD, FechaH, idCli, option);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.CuentaTarjetaCreditoVentas vd = new Tecnocuisine_API.Entitys.CuentaTarjetaCreditoVentas();
                    vd.id = Convert.ToInt32(row["id"]);
                    vd.idCliente = Convert.ToInt32(row["idCliente"]);
                    vd.fecha = ((DateTime)row["fecha"]);
                    vd.Descripcion = row["Descripcion"].ToString();
                    vd.Debe = Convert.ToDecimal(row["Debe"]);
                    vd.Haber = Convert.ToDecimal(row["Haber"]);

                    //vd.idVenta = Convert.ToInt32(row["idVenta"]);
                    vd.idTarjeta = Convert.ToInt32(row["idTarjeta"]);
                    vd.idEntidad = Convert.ToInt32(row["idEntidad"]);


                    total += (decimal)(vd.Debe + vd.Haber);

                    CargarInsumosPH2(vd);


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
        public void cargarEntidades()
        {
            try
            {
                var ListEntidades = controladorEntidad.ObtenerTodasLasEntidades();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("id", typeof(int));
                dataTable.Columns.Add("descripcion", typeof(string));

                // Recorrer la lista de entidades y agregar filas a la DataTable
                foreach (var entidad in ListEntidades)
                {
                    DataRow row = dataTable.NewRow();
                    row["id"] = entidad.id;
                    row["descripcion"] = entidad.descripcion;
                    dataTable.Rows.Add(row);
                }


                DataRow dr = dataTable.NewRow();
                dr["descripcion"] = "Todos";
                dr["id"] = 0;
                dataTable.Rows.InsertAt(dr, 0);

                this.txtEntidad.DataSource = dataTable;
                this.txtEntidad.DataValueField = "id";
                this.txtEntidad.DataTextField = "descripcion";

                this.txtEntidad.DataBind();



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

        public void ObtenerInsumos()
        {
            try
            {
                var insumos = controladorTarjetaDeCreditoVenta.ObtenerTodasLasVentasTarjeta();

                if (insumos.Count > 0)
                {
                    decimal total = 0;
                    foreach (var item in insumos)
                    {
                        CargarInsumosPH(item);
                        total += (decimal)(item.Haber + item.Debe);
                    }
                    SaldoTotal.Value = FormatearNumero(total);
                }

            }
            catch (Exception ex)
            {

            }
        }

    
        public void CargarInsumosPH2(CuentaTarjetaCreditoVentas insumo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = insumo.id.ToString();

                //Celdas

                TableCell celNumero = new TableCell();
                celNumero.Text = insumo.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                var entidad = controladorEntidad.ObtenerEntidadPorID((int)insumo.idEntidad);
                TableCell celEntidad = new TableCell();
                celEntidad.Text = entidad.descripcion;
                celEntidad.VerticalAlign = VerticalAlign.Middle;
                celEntidad.HorizontalAlign = HorizontalAlign.Left;
                celEntidad.Width = Unit.Percentage(40);
                celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celEntidad);

                TableCell celFecha = new TableCell();
                celFecha.Text = insumo.fecha.ToString().Split(' ')[0];
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Width = Unit.Percentage(40);
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = insumo.Descripcion.ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Width = Unit.Percentage(40);
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                ControladorCliente controladorCliente = new ControladorCliente();
                var cliente = controladorCliente.ObtenerClienteId((int)insumo.idCliente);
                TableCell celCliente = new TableCell();
                celCliente.Text = cliente == null ? "Cliente no encontrado" : cliente.alias;
                celCliente.VerticalAlign = VerticalAlign.Middle;
                celCliente.HorizontalAlign = HorizontalAlign.Left;
                celCliente.Width = Unit.Percentage(40);
                celCliente.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCliente);
                ControladorTarjetas controladorTarjetas2 = new ControladorTarjetas();
                var tarjeta = controladorTarjetas2.ObtenerTarjetaPorID((int)insumo.idTarjeta);


               TableCell celAcredita = new TableCell();
                celAcredita.Text = tarjeta != null ? tarjeta.AcreditaEn.ToString() + " Dias" : "No se encontro acreditacion";
                celAcredita.VerticalAlign = VerticalAlign.Middle;
                celAcredita.HorizontalAlign = HorizontalAlign.Left;
                celAcredita.Width = Unit.Percentage(40);
                celAcredita.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcredita);
                DateTime fecha = (DateTime)insumo.fecha;
                DateTime FechaFinal  = fecha.AddDays((double)tarjeta.AcreditaEn);

                TableCell celAcreditaEl = new TableCell();
                celAcreditaEl.Text = FechaFinal.ToString().Split(' ')[0];
                celAcreditaEl.VerticalAlign = VerticalAlign.Middle;
                celAcreditaEl.HorizontalAlign = HorizontalAlign.Left;
                celAcreditaEl.Width = Unit.Percentage(40);
                celAcreditaEl.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcreditaEl);

                TableCell NumProduc = new TableCell();
                NumProduc.Text = FormatearNumero((decimal)insumo.Debe + (decimal)insumo.Haber);
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(NumProduc);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + insumo.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarInsumo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + insumo.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + insumo.id + ");";
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
        public void CargarInsumosPH(CuentaTarjetaCreditoVentas insumo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = insumo.id.ToString();

                //Celdas

                TableCell celNumero = new TableCell();
                celNumero.Text = insumo.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                var entidad = controladorEntidad.ObtenerEntidadPorID((int)insumo.Tarjetas.idEntidad);
                TableCell celEntidad = new TableCell();
                celEntidad.Text = entidad.descripcion;
                celEntidad.VerticalAlign = VerticalAlign.Middle;
                celEntidad.HorizontalAlign = HorizontalAlign.Left;
                celEntidad.Width = Unit.Percentage(40);
                celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celEntidad);

                TableCell celFecha = new TableCell();
                celFecha.Text = insumo.fecha.ToString().Split(' ')[0];
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Width = Unit.Percentage(40);
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = insumo.Descripcion.ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Width = Unit.Percentage(40);
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);


                TableCell celCliente = new TableCell();
                celCliente.Text = insumo.Clientes.alias;
                celCliente.VerticalAlign = VerticalAlign.Middle;
                celCliente.HorizontalAlign = HorizontalAlign.Left;
                celCliente.Width = Unit.Percentage(40);
                celCliente.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCliente);


                TableCell celAcredita = new TableCell();
                celAcredita.Text = insumo.Tarjetas.AcreditaEn.ToString() + " Dias";
                celAcredita.VerticalAlign = VerticalAlign.Middle;
                celAcredita.HorizontalAlign = HorizontalAlign.Left;
                celAcredita.Width = Unit.Percentage(40);
                celAcredita.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcredita);

                DateTime fecha = (DateTime)insumo.fecha;
                DateTime FechaFinal = fecha.AddDays((double)insumo.Tarjetas.AcreditaEn);

                TableCell celAcreditaEl = new TableCell();
                celAcreditaEl.Text = FechaFinal.ToString().Split(' ')[0];
                celAcreditaEl.VerticalAlign = VerticalAlign.Middle;
                celAcreditaEl.HorizontalAlign = HorizontalAlign.Left;
                celAcreditaEl.Width = Unit.Percentage(40);
                celAcreditaEl.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcreditaEl);

                TableCell NumProduc = new TableCell();
                NumProduc.Text = FormatearNumero((decimal)insumo.Debe + (decimal)insumo.Haber);
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(NumProduc);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + insumo.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarInsumo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + insumo.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + insumo.id + ");";
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