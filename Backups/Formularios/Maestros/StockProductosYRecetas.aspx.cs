using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class StockProductosYRecetas : System.Web.UI.Page
    {
        ControladorReceta ControladorReceta = new ControladorReceta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ObtenerProductos();
            ObtenerRecetas();
        }
        public void ObtenerProductos()
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                var productos = controladorProducto.ObtenerTodosProductos();

                if (productos.Count > 0)
                {
                    

                    foreach (var item in productos)
                    {
                        CargarProductosPH(item);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void CargarProductosPH(Tecnocuisine_API.Entitys.Productos producto)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "Productos_" + producto.id.ToString() + "_" + producto.unidadMedida.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = producto.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = producto.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

               

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(producto.unidadMedida).descripcion;
                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celUM);

                TableCell celCosto = new TableCell();
                celCosto.Text = producto.costo.ToString().Replace(',', '.');
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                TableCell celStock = new TableCell();

                var stockR = controladorStockProducto.ObtenerStockProducto(producto.id);
                if (stockR != null)
                    celStock.Text = stockR.stock.Value.ToString();
                else
                    celStock.Text = "0";
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celStock);



                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("data-placement", "top");
                btnDetalles.Attributes.Add("href", "StockDetallado.aspx?t=1&i=" +producto.id);
                btnDetalles.Attributes.Add(" data-original-title", "Visualizar Stock");
                btnDetalles.ID = "btnSelecProducto_" + producto.id ;
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-clipboard'></i></span>";
                btnDetalles.Click += new EventHandler(this.RedirecProducto);
                celAccion.Controls.Add(btnDetalles);
                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");



                tr.Cells.Add(celAccion);

                phProductosyRecetas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        public void ObtenerRecetas()
        {
            try
            {
                var Recetas = ControladorReceta.ObtenerTodosRecetas();

                if (Recetas.Count > 0)
                {
                    foreach (var item in Recetas)
                    {

                        CargarRecetasPHModal(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void CargarRecetasPHModal(Tecnocuisine_API.Entitys.Recetas Receta)
        {

            try
            {

                //fila


                TableRow tr = new TableRow();
                tr.ID = "Recetas_" + Receta.id.ToString() + "_" + Receta.UnidadMedida;

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Receta.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = Receta.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);
                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(Receta.UnidadMedida.Value).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celUM);

                TableCell celCosto = new TableCell();
                celCosto.Text = FormatearNumero((decimal)Receta.Costo);
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                ControladorStockReceta controladorStockProducto = new ControladorStockReceta();
                TableCell celStock = new TableCell();
                var stockR = controladorStockProducto.ObtenerStockReceta(Receta.id);
                if (stockR != null)
                    celStock.Text = FormatearNumero(stockR.stock.Value);
                else
                    celStock.Text = "0.00";

                //celStock.Text = stockR.stock.Value.ToString();
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celStock);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("href", "StockDetallado.aspx?t=2&i=" + Receta.id);
                btnDetalles.Attributes.Add("data-placement", "top");
                btnDetalles.Attributes.Add(" data-original-title", "Visualizar Stock");
                btnDetalles.ID = "btnSelecReceta_" + Receta.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-clipboard'></i></span>";
                btnDetalles.Click += new EventHandler(this.RedirecReceta);
                celAccion.Controls.Add(btnDetalles);
                tr.Cells.Add(celAccion);

                phProductosyRecetas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        private void RedirecReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("StockDetallado.aspx?t=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        private void RedirecProducto(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("StockDetallado.aspx?t=1&i=" + id[1]);
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