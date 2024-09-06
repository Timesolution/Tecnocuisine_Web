using System;
using System.Linq;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class StockSectoresDetalle : System.Web.UI.Page
    {
        private int sectorId;
        private ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
        private Tecnocuisine_API.Entitys.SectorProductivo sector = new Tecnocuisine_API.Entitys.SectorProductivo();
        private ControladorStockProducto cStockProducto = new ControladorStockProducto();

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            ObtenerParametros();                
            MostrarDescripcionSector();
            CargarTabla();
        }

        private void MostrarDescripcionSector()
        {
            sector = cSectorProductivo.ObtenerSectorProductivoId(sectorId);
            lblSector.Text = sector.descripcion;
        }

        private void ObtenerParametros()
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("StockSectores.aspx");

            sectorId = Convert.ToInt32(Request.QueryString["id"]);
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
                    if (this.VerificarAcceso() != 1)
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

        private int VerificarAcceso()
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

        public void CargarTabla()
        {
            try
            {
                var stocks = cStockProducto.ObtenerStockSectoresByIdSector(sectorId);

                foreach (var stock in stocks)
                {
                    //fila
                    TableRow tr = new TableRow();
                    //tr.ID = sector.id.ToString();

                    TableCell cellDescripcion = new TableCell();
                    cellDescripcion.Text = stock.Productos.descripcion;
                    cellDescripcion.VerticalAlign = VerticalAlign.Middle;
                    cellDescripcion.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellStock = new TableCell();
                    cellStock.Text = stock.stock.ToString();
                    cellStock.VerticalAlign = VerticalAlign.Middle;
                    cellStock.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellUnidad = new TableCell();
                    cellUnidad.Text = stock.Productos.Unidades.descripcion;
                    cellUnidad.VerticalAlign = VerticalAlign.Middle;
                    cellUnidad.HorizontalAlign = HorizontalAlign.Left;

                    tr.Cells.Add(cellDescripcion);
                    tr.Cells.Add(cellStock);
                    tr.Cells.Add(cellUnidad);

                    phItems.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}