using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class EntregaDetallada : System.Web.UI.Page
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
                if (id != 0)
                {
                CargarTablaEntregasTotal();
                }

            }
        }

     

        private void CargarTablaEntregasTotal()
        {
            try
            {
                ControladorEntregas controladorEntregas = new ControladorEntregas();
                var list = controladorEntregas.ObtenerEntregasProductoByidEntrega(id);
                foreach(var item in list)
                {
                    CargarPhEntregas(item);
                }
            }
            catch (Exception)
            {

            }
        }
        private void CargarPhEntregas(Tecnocuisine_API.Entitys.Entregas_Productos i)
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

                    NomProd = controladorProducto.ObtenerProductoId((int)i.idProductos).descripcion;

                    idprod = i.idProductos.ToString();

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

                TableCell celCantidad = new TableCell();
                celCantidad.Text = i.Cantidad.ToString();
                celCantidad.Font.Bold = true;
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: end;");
                tr.Cells.Add(celCantidad);


                ControladorMarca cm = new ControladorMarca();
              

                TableCell celMarca = new TableCell();
                celMarca.Text = cm.ObtenerMarcaId((int)i.idMarca).descripcion;
                celMarca.VerticalAlign = VerticalAlign.Middle;
                celMarca.HorizontalAlign = HorizontalAlign.Left;
                celMarca.Width = Unit.Percentage(5);
                celMarca.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celMarca);


                ControladorPresentacion cp = new ControladorPresentacion();
                TableCell CelPresentacion = new TableCell();
                CelPresentacion.Text = cp.ObtenerPresentacionId((int)i.idPresentacion).descripcion;
                CelPresentacion.VerticalAlign = VerticalAlign.Middle;
                CelPresentacion.HorizontalAlign = HorizontalAlign.Left;
                CelPresentacion.Width = Unit.Percentage(5);
                CelPresentacion.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(CelPresentacion);



                TableCell Lote = new TableCell();
                Lote.Text = i.Lote;
                Lote.VerticalAlign = VerticalAlign.Middle;
                Lote.HorizontalAlign = HorizontalAlign.Left;
                Lote.Width = Unit.Percentage(5);
                Lote.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(Lote);




                TableCell CelFecha = new TableCell();
                CelFecha.Text =  i.FechaVencimiento;
                CelFecha.VerticalAlign = VerticalAlign.Middle;
                CelFecha.HorizontalAlign = HorizontalAlign.Left;
                CelFecha.Width = Unit.Percentage(5);
                CelFecha.Attributes.Add("style", "text-align:center");
                tr.Cells.Add(CelFecha);
                //agrego fila a tabla

                phTablaProductos.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }
    }
}