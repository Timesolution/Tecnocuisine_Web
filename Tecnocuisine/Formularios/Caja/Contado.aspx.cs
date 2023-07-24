using Gestion_Api.Controladores;
using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Controladores;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine.Caja
{
    public partial class Contado : Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorCuentaContado controladorCuentaContado = new ControladorCuentaContado();
        ControladorProveedores cp = new ControladorProveedores();
        ControladorCliente ControladorCliente = new ControladorCliente();
        ControladorVentas controladorVentas = new ControladorVentas();

        int idCliente = -1;
        string FechaD = "";
        string FechaH = "";




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();


            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idCliente = Convert.ToInt32(Request.QueryString["p"]);
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }


            if (!IsPostBack)
            {
            }

            if (idCliente != -1)
            {
                FiltrarVentas(this.idCliente, this.FechaD, this.FechaH);
            } else
            {
                ObtenerTodosLasCuentaContado();
            }
            CargarClientes();
        }
        public void ObtenerTodosLasCuentaContado()
        {
            try
            {
                var cuenta = controladorCuentaContado.ObtenerTodosCuentaContado();
                if (cuenta != null)
                {
                    foreach (CuentaContado item in cuenta)
                    {
                        RellenarTabla(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void FiltrarVentas(int idCli, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes clientes = ControladorCliente.ObtenerClienteId(idCli);
                AliasCliente.Value = clientes == null ? "Todos" : clientes.alias  ;
                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                var dt = controladorCuentaContado.FiltrarCuentaContado(FechaD, FechaH, idCli);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.CuentaContado cc = new Tecnocuisine_API.Entitys.CuentaContado();
                    cc.id = Convert.ToInt32(row["id"]);
                    cc.fecha = ((DateTime)row["fecha"]);
                    cc.Descripcion = row["Descripcion"].ToString();
                    cc.Importe = Convert.ToDecimal(row["Importe"]);
                    if (!row.IsNull("idCliente"))
                    { 
                        cc.idCliente = Convert.ToInt32(row["idCliente"]);
                    } else
                    {
                        cc.idProveedor = Convert.ToInt32(row["idProveedor"]);
                    }


                    total += (decimal)(cc.Importe);

                    RellenarTabla(cc);


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

        private void CargarClientes()
        {
            try
            {

                var clientes = ControladorCliente.ObtenerTodosClientes();


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

                ListaProveedores.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }



        public void RellenarTabla(Tecnocuisine_API.Entitys.CuentaContado cc)
        {
            try
            {
                //fila
                TableRow tr = new TableRow();
                tr.ID = cc.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = cc.id.ToString();
                celNumero.Width = Unit.Percentage(5);
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");

                tr.Cells.Add(celNumero);

                TableCell celFecha = new TableCell();
                celFecha.Width = Unit.Percentage(10);
                celFecha.Text = cc.fecha.ToString().Split(' ')[0];
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celFecha);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Width = Unit.Percentage(10);
                celDescripcion.Text = cc.Descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celDescripcion);

                ControladorCliente controladorCliente = new ControladorCliente();
                ControladorProveedores controladorProveedores = new ControladorProveedores();
                if (cc.idCliente != null)
                {

                var cliente = controladorCliente.ObtenerClienteId((int)cc.idCliente);
                TableCell celCliente = new TableCell();
                celCliente.Width = Unit.Percentage(10);
                celCliente.Text = cliente == null ? "No se encontro cliente" : cliente.alias;
                celCliente.VerticalAlign = VerticalAlign.Middle;
                celCliente.HorizontalAlign = HorizontalAlign.Left;
                celCliente.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celCliente);
                } else
                {
                    var prov = controladorProveedores.ObtenerProveedorByID((int)cc.idProveedor);
                    TableCell celCliente = new TableCell();
                    celCliente.Width = Unit.Percentage(10);
                    celCliente.Text = prov.Alias;
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    celCliente.HorizontalAlign = HorizontalAlign.Left;
                    celCliente.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                    tr.Cells.Add(celCliente);
                }

                TableCell celImporte = new TableCell();
                celImporte.Width = Unit.Percentage(10);
                celImporte.Text = FormatearNumero((decimal)cc.Importe);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);




                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnEliminar = new LinkButton();
                btnEliminar.CssClass = "btn  btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#" + cc.id);

                btnEliminar.Attributes.Add("href", "#");
                btnEliminar.Text = "<span data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=\"Ver Detalle Factura\"><i class='fa fa-search' style=\"color: black\"></i></span>";
                btnEliminar.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; background-color: transparent; padding-top: 12px;");
                celAccion.Controls.Add(btnEliminar);

                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phCuentaCorriente.Controls.Add(tr);
            }
            catch (Exception ex) { }
        }



        public static string ConvertirFecha(string fecha)
        {
            // Convertir a objeto DateTime
            DateTime fechaDateTime = DateTime.ParseExact(fecha, "yyyy/MM/dd", null);

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



        public static double RevertirNumero(string numeroFormateado)
        {
            double numero = double.Parse(numeroFormateado.Replace(",", ""));
            return numero;
        }

        public static string FormatearNumero(decimal numero)
        {
            return numero.ToString("N2", new CultureInfo("en-US"));
        }
    }
}