using Gestion_Api.Controladores;
using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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


namespace Tecnocuisine.Compras
{
    public partial class CuentaCorriente : Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorCuentaCorriente controladorCuentaCorriente = new ControladorCuentaCorriente();
        ControladorProveedores cp = new ControladorProveedores();
        int idProveedor = -1;
        string FechaD = "";
        string FechaH = "";




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();


            if (Request.QueryString["FechaD"] != null)
            {
            this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idProveedor = Convert.ToInt32(Request.QueryString["p"]);
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }
          

            if (!IsPostBack)
            {



            }

            if (idProveedor != -1)
            {
                FiltrarCuentaCorriente(this.idProveedor, this.FechaD, this.FechaH);
            }
            ObtenerTodosLosProveedores();
        }
        public void FiltrarCuentaCorriente(int idProveedor, string FechaD, string FechaH) {
            try
            {
                Tecnocuisine_API.Entitys.Proveedores provedor = cp.ObtenerProveedorByID(idProveedor);
                AliasProveedor.Value = provedor != null ? provedor.Alias : "Todos";
            string FechaDesde = ConvertDateFormat(FechaD);
            string FechaHasta = ConvertDateFormat(FechaH);
            var dt = controladorCuentaCorriente.FiltrarCuentaCorriente(FechaD, FechaH, idProveedor);
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                Tecnocuisine_API.Entitys.CuentaCorriente cc = new Tecnocuisine_API.Entitys.CuentaCorriente();
                cc.id = Convert.ToInt32(row["id"]);
                    cc.fecha = ((DateTime)row["fecha"]);
                    cc.fechaVTO = ((DateTime)row["fechaVTO"]);
                cc.Descripcion = (row["Descripcion"]).ToString();
                cc.Debe = Convert.ToDecimal(row["Debe"]);
                cc.Haber = Convert.ToDecimal(row["Haber"]);
                    cc.Saldo = Convert.ToDecimal(row["Saldo"]);
                total += (decimal)(cc.Debe - cc.Haber);
                
                        RellenarTabla(cc);
               

            }
                SaldoTotal.Value = FormatearNumero(total);
            } catch(Exception ex) { }
        }


        public void RellenarTabla(Tecnocuisine_API.Entitys.CuentaCorriente cc)
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


                TableCell NumProduc = new TableCell();
                NumProduc.Text =  FormatearNumero((decimal)cc.Debe);
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(NumProduc);

                TableCell celImporte = new TableCell();
                celImporte.Width = Unit.Percentage(10);
                celImporte.Text =  FormatearNumero((decimal)cc.Haber);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);

                TableCell celImporteSaldo = new TableCell();
                celImporteSaldo.Width = Unit.Percentage(10);
                celImporteSaldo.Text =  cc.Saldo != null ? FormatearNumero((decimal)cc.Saldo) : "Error";
                celImporteSaldo.VerticalAlign = VerticalAlign.Middle;
                celImporteSaldo.HorizontalAlign = HorizontalAlign.Left;
                celImporteSaldo.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporteSaldo);





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
            catch(Exception ex) { }
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



        public void ObtenerTodosLosProveedores()
        {
            try
            {

                var allTipos = cp.ObtenerProveedoresAll();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in allTipos)
                {

                    builder.Append(String.Format("<option value='{0}' id='" + rec.Id + "'>", rec.Id + " - " + rec.Alias));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaProveedores.InnerHtml = builder.ToString();

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
    }
}