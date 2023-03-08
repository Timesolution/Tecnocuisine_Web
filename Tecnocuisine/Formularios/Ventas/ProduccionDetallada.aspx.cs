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
    public partial class ProduccionDetallada : System.Web.UI.Page
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
                var list = controladorVentas.obtenerVentaProducionRecetaProductoIdVenta(id);
                int cont = 1;
                foreach(var item in list)
                {
                    
                    CargarPhVenta(item,cont);
                    cont += 1;
                }
            }
            catch (Exception)
            {

            }
        }
        private void CargarPhVenta(Tecnocuisine_API.Entitys.VentaProducionRecetaProducto i,int cont)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorReceta controladorReceta = new ControladorReceta();

                string idprod = "";
                string NomProd = "";
                if (i.idProducto == null)
                {
                    NomProd = controladorReceta.ObtenerRecetaId((int)i.idReceta).descripcion;
                    idprod = i.idReceta.ToString() + " - Receta";
                } else
                {
                    NomProd = controladorProducto.ObtenerProductoId((int)i.idProducto).descripcion;

                    idprod = i.idProducto.ToString() + " - Producto";

                }

                TableRow tr = new TableRow();
                tr.ID = i.id.ToString();

                TableCell celProductoCod = new TableCell();
                celProductoCod.Text = cont.ToString();
                celProductoCod.Font.Bold = true;
                celProductoCod.VerticalAlign = VerticalAlign.Middle;
                celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important; vertical-align: middle;");
                tr.Cells.Add(celProductoCod);

                TableCell celidProd = new TableCell();
                celidProd.Text = idprod;
                celidProd.Font.Bold = true;
                celidProd.VerticalAlign = VerticalAlign.Middle;
                celidProd.HorizontalAlign = HorizontalAlign.Left;
                celidProd.Attributes.Add("style", "padding-bottom: 1px !important;vertical-align: middle;");
                tr.Cells.Add(celidProd);

                TableCell celNombreProd = new TableCell();
                celNombreProd.Text = NomProd;
                celNombreProd.Font.Bold = true;
                celNombreProd.VerticalAlign = VerticalAlign.Middle;
                celNombreProd.HorizontalAlign = HorizontalAlign.Left;
                celNombreProd.Attributes.Add("style", "padding-bottom: 1px !important;vertical-align: middle;");
                tr.Cells.Add(celNombreProd);

                TableCell CantProd = new TableCell();
                CantProd.Text = i.CantidadProducida.ToString();
                CantProd.Font.Bold = true;
                CantProd.VerticalAlign = VerticalAlign.Middle;
                CantProd.HorizontalAlign = HorizontalAlign.Left;
                CantProd.Attributes.Add("style", "padding-bottom: 1px !important; text-align: end;vertical-align: middle;");
                tr.Cells.Add(CantProd);


                phTablaProductos.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }
    }
}