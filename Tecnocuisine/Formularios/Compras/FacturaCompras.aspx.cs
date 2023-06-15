using Gestion_Api.Controladores;
using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using System.Xml.Linq;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static Gestion_Api.Auxiliares.PushNotification.AppCenterPush;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class FacturaCompras : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorSectorProductivo ControladorSector = new ControladorSectorProductivo();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        ControladorEntregas ControladorEntregas = new ControladorEntregas();
        ControladorReceta ControladorReceta = new ControladorReceta();
        ControladorMarca controladorMarca = new ControladorMarca();
        ControladorPresentacion controladorPresentacion1 = new ControladorPresentacion();
        ControladorFacturas controladorFacturas = new ControladorFacturas();
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        int idProveedor = -1;
        string FechaD = "";
        string FechaH = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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
            ObtenerTodasLasFacturas();
            ObtenerTodosLosProveedores();
        }


        public void ObtenerTodosLosProveedores()
        {
            try
            {

                var allTipos = ControladorProveedores.ObtenerProveedoresAll();
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

        public void FiltrarCuentaCorriente(int idProveedor, string FechaD, string FechaH)
        {
            try
            {
                Tecnocuisine_API.Entitys.Proveedores provedor = ControladorProveedores.ObtenerProveedorByID(idProveedor);
                AliasProveedor.Value = provedor.Alias;
                string FechaDesde = ConvertirFecha(FechaD);
                string FechaHasta = ConvertirFecha(FechaH);
                var dt = controladorFacturas.FiltrarFacturas(FechaD, FechaH, idProveedor);
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Tecnocuisine_API.Entitys.Facturas f = new Tecnocuisine_API.Entitys.Facturas();
                    f.id = Convert.ToInt32(row["id"]);
                    f.FechaEmitido = ((DateTime)row["FechaEmitido"]);
                    f.FechaVencimiento = ((DateTime)row["FechaVencimiento"]);
                    f.TipoDocumentoID = Convert.ToInt32(row["TipoDocumentoID"]);
                    f.ImporteTotal = Convert.ToDecimal(row["ImporteTotal"]);
                    f.NumeroFactura = (row["Haber"]).ToString();
                    f.idProveedor = Convert.ToInt32(row["idProveedor"]);

                    total += (decimal)(f.ImporteTotal);

                    CargarFacturasPH(f);


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
        private void ObtenerTodasLasFacturas()
        {
            try
            {
                var TodasLasFac = controladorFacturas.ObtenerTodasFactura();
                if (TodasLasFac.Count > 0)
                {
                    decimal total = 0;
                    foreach (var item in TodasLasFac)
                    {
                        CargarFacturasPH(item);
                        total += (decimal)(item.ImporteTotal);
                    }
                    SaldoTotal.Value = FormatearNumero(total);
                }
            }
            catch (Exception)
            {

            }
        }

        [WebMethod]
        public static void EliminarFac(string id)
        {
            int idfa = Convert.ToInt32(id);
            ControladorFacturas cf = new ControladorFacturas();
            cf.EliminarFac(idfa);
        } 

        public void CargarFacturasPH(Facturas item)
        {
            try
            {
                
                //fila
                TableRow tr = new TableRow();
                tr.ID = item.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = item.id.ToString();
                celNumero.Width = Unit.Percentage(5);
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: right;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Width = Unit.Percentage(10);
                celDescripcion.Text = item.FechaEmitido.ToString().Split(' ')[0];
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celDescripcion);

                TableCell celVto = new TableCell();
                celVto.Width = Unit.Percentage(10);
                celVto.Text = item.FechaVencimiento.ToString().Split(' ')[0];
                celVto.VerticalAlign = VerticalAlign.Middle;
                celVto.HorizontalAlign = HorizontalAlign.Left;
                celVto.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celVto);


                TableCell NumProduc = new TableCell();
                NumProduc.Text = item.Proveedores.Alias;
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle; text-align: left;");
                tr.Cells.Add(NumProduc);

                TableCell celImporte = new TableCell();
                celImporte.Width = Unit.Percentage(10);
                celImporte.Text = FormatearNumero((decimal)item.ImporteTotal);
                celImporte.VerticalAlign = VerticalAlign.Middle;
                celImporte.HorizontalAlign = HorizontalAlign.Left;
                celImporte.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(celImporte);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnEliminar1 = new LinkButton();
                btnEliminar1.CssClass = "btn  btn-xs";
                btnEliminar1.Attributes.Add("data-toggle", "modal");
                btnEliminar1.Attributes.Add("href", "#" + item.id);

                //btnEliminar.Attributes.Add("href", "../Compras/CrearVenta.aspx?t=1&i=" + producto.id);
                btnEliminar1.Text = "<span data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=\"Ver Detalle Factura\"><i class='fa fa-search' style=\"color: black\"></i></span>";
                btnEliminar1.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; background-color: transparent; padding-top: 12px;");
                celAccion.Controls.Add(btnEliminar1);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + item.id;
                btnEliminar.CssClass = "btn  btn-xs";
                btnEliminar.Text = "<a data-toggle=\"tooltip\" onclick =\"EliminarFac("+ item.id + ")\" data-placement=\"top\" data -original-title=\"Eliminar Producto\"><i style='color:black' class='fa fa-trash - o'></i></a>";
                btnEliminar.Style.Add("background-color", "transparent");
                celAccion.Controls.Add(btnEliminar);


                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);
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
