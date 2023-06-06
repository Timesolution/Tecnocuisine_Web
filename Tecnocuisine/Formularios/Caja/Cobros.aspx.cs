using Gestion_Api.Controladores;
using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Controladores;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine.Caja
{
    public partial class Cobros : Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorCuentaCorrienteVentas controladorCuentaCorriente = new ControladorCuentaCorrienteVentas();
        ControladorProveedores cp = new ControladorProveedores();
        ControladorCliente ControladorCliente = new ControladorCliente();
        ControladorVentas controladorVentas = new ControladorVentas();

        int idCliente = -1;
        string FechaD = "";
        string FechaH = "";
        int mensaje = 0;




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            if (Request.QueryString["m"] != null)
            {
                mensaje = Convert.ToInt32(Request.QueryString["m"]);
            }

            if (Request.QueryString["FechaD"] != null)
            {
            this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idCliente = Convert.ToInt32(Request.QueryString["p"]);
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }
            if (mensaje == 5)
            {
                this.m.ShowToastr(this.Page, "Se imputaron correctamente las facturas seleccionadas!", "Exito");
            }
          

            if (!IsPostBack)
            {



            }

            if (idCliente != -1)
            {
                FiltrarVentas(this.idCliente, ConvertDateFormat(this.FechaD), ConvertDateFormat(this.FechaH));
            }
            CargarClientes();
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


        public void FiltrarVentas(int idCli, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes clientes = ControladorCliente.ObtenerClienteId(idCli);
                AliasCliente.Value = clientes == null ? "No se encontro cliente" : clientes.id + " - " + clientes.alias;
                string FechaDesde = ConvertirFecha(FechaD);
                string FechaHasta = ConvertirFecha(FechaH);
                var dt = controladorCuentaCorriente.FiltrarCuentaCorrienteVentas(FechaD, FechaH, idCli);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.CuentaCorrienteVentas cc = new Tecnocuisine_API.Entitys.CuentaCorrienteVentas();
                    cc.id = Convert.ToInt32(row["id"]);
                    cc.fecha = ((DateTime)row["fecha"]);
                    cc.idCliente = Convert.ToInt32(row["idCliente"]);
                    cc.Descripcion = row["Descripcion"].ToString();
                    cc.Debe = Convert.ToDecimal(row["Debe"]);
                    cc.Haber = Convert.ToDecimal(row["Haber"]);
                    cc.idVenta = Convert.ToInt32(row["idVenta"]);
                    cc.Saldo = Convert.ToDecimal(row["Saldo"]);


                    total += (decimal)(cc.Saldo);

                    RellenarTabla(cc);


                }
                SaldoTotal.Value = FormatearNumero(total);
            }
            catch (Exception ex) { }
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
        


        public void RellenarTabla(Tecnocuisine_API.Entitys.CuentaCorrienteVentas cc)
        {
            try
            {
                //fila
                TableRow tr = new TableRow();
                tr.ID = cc.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text =  cc.id.ToString();
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


                TableCell celDebe = new TableCell();
                celDebe.Text =  FormatearNumero((decimal)cc.Debe);
                celDebe.VerticalAlign = VerticalAlign.Middle;
                celDebe.HorizontalAlign = HorizontalAlign.Left;
                celDebe.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celDebe);

                TableCell celHaber = new TableCell();
                celHaber.Width = Unit.Percentage(10);
                celHaber.Text =  FormatearNumero((decimal)cc.Haber);
                celHaber.VerticalAlign = VerticalAlign.Middle;
                celHaber.HorizontalAlign = HorizontalAlign.Left;
                celHaber.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celHaber);

                TableCell celImporte = new TableCell();
                celImporte.Width = Unit.Percentage(10);
                celImporte.Text = FormatearNumero((decimal)cc.Saldo);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);




                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                HtmlInputCheckBox checkBox = new HtmlInputCheckBox();
                checkBox.Value = cc.id.ToString();
                checkBox.ID = "chk" + cc.id.ToString();
                checkBox.Attributes.Add("onclick", "calcularImporte(this);"); // Agregar evento onclick
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                celAccion.Controls.Add(checkBox);


                tr.Cells.Add(celAccion);

                phCuentaCorriente.Controls.Add(tr);
            }
            catch(Exception ex) { }
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