using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class VentaDetallada : System.Web.UI.Page
    {
        ControladorStockReceta controladorStockRecetas = new ControladorStockReceta();
        ControladorStockProducto ControladorStockProducto = new ControladorStockProducto();
        ControladorProducto ControladorProducto = new ControladorProducto();
        ControladorReceta ControladorReceta = new ControladorReceta();
        ControladorUnidad cu = new ControladorUnidad();
            int id  ;
        int tipo;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id= Convert.ToInt32(Request.QueryString["i"]);
                CargarTablaVentasTotal();

            }
        }

     

        private void CargarTablaVentasTotal()
        {
            try
            {
                ControladorVentas controladorVentas = new ControladorVentas();
                var list = controladorVentas.obtenerVentaDetalleRecetaProductoIdVenta(id);
                foreach(var item in list)
                {
                    CargarPhVenta(item);
                }
            }
            catch (Exception)
            {

            }
        }
        private void CargarPhVenta(Tecnocuisine_API.Entitys.VentaDetalleRecetaProducto i)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorReceta controladorReceta = new ControladorReceta();

                //ID ---- LISTO
                // NOMBRE PRODUCTO ---- LISTO
                // COSTO ----- LISTO
                // CANTIDAD ---- Listo
                //PRECIO VENTA

                string idprod = "";
                string NomProd = "";
                if (i.idProducto == null)
                {
                    NomProd = controladorReceta.ObtenerRecetaId((int)i.idReceta).descripcion;
                    idprod = i.idReceta.ToString();
                } else
                {
                    NomProd = controladorProducto.ObtenerProductoId((int)i.idProducto).descripcion;

                    idprod = i.idProducto.ToString();

                }

                TableRow tr = new TableRow();
                tr.ID = i.id.ToString();

                TableCell celProductoCod = new TableCell();
                celProductoCod.Text = idprod;
                celProductoCod.Font.Bold = true;
                celProductoCod.VerticalAlign = VerticalAlign.Middle;
                celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celProductoCod);



                TableCell celNombreProd = new TableCell();
                celNombreProd.Text = NomProd;
                celNombreProd.Font.Bold = true;
                celNombreProd.VerticalAlign = VerticalAlign.Middle;
                celNombreProd.HorizontalAlign = HorizontalAlign.Left;
                celNombreProd.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombreProd);

                TableCell CelCosto = new TableCell();
                CelCosto.Text = "$" + FormatearNumero((decimal)i.Costo);
                CelCosto.Font.Bold = true;
                CelCosto.VerticalAlign = VerticalAlign.Middle;
                CelCosto.HorizontalAlign = HorizontalAlign.Left;
                CelCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: end;");
                tr.Cells.Add(CelCosto);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = i.Cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Width = Unit.Percentage(5);
                celCantidad.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celCantidad);

                TableCell celPrecioVenta = new TableCell();
                celPrecioVenta.Text = "$" + i.PrecioVenta.ToString();
                celPrecioVenta.VerticalAlign = VerticalAlign.Middle;
                celPrecioVenta.HorizontalAlign = HorizontalAlign.Left;
                celPrecioVenta.Width = Unit.Percentage(5);
                celPrecioVenta.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celPrecioVenta);
                //agrego fila a tabla

                phTablaProductos.Controls.Add(tr);
            }
            catch (Exception ex)
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