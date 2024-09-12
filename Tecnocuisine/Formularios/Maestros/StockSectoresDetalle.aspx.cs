using System;
using System.Linq;
using System.Web.Services;
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
        private ControladorStockReceta cStockReceta = new ControladorStockReceta();

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            ObtenerParametros();
            MostrarDescripcionSector();
            CargarTabla_General();
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

        public void CargarTabla_General()
        {
            CargarProductos();
            CargarRecetas();
        }

        private void CargarProductos()
        {
            try
            {
                var stocks = cStockProducto.ObtenerStockSectoresByIdSector(sectorId);

                foreach (var stock in stocks)
                {
                    //fila
                    TableRow tr = new TableRow();
                    //tr.ID = sector.id.ToString();

                    TableCell cellTipo = new TableCell();
                    cellTipo.Text = "Producto";
                    cellTipo.VerticalAlign = VerticalAlign.Middle;
                    cellTipo.HorizontalAlign = HorizontalAlign.Left;
                    cellTipo.Style.Add("display","none");

                    TableCell cellDescripcion = new TableCell();
                    cellDescripcion.Text = stock.Productos.descripcion;
                    cellDescripcion.VerticalAlign = VerticalAlign.Middle;
                    cellDescripcion.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellStock = new TableCell();
                    cellStock.Text = stock.stock.ToString();
                    cellStock.VerticalAlign = VerticalAlign.Middle;
                    cellStock.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellUnidad = new TableCell();
                    cellUnidad.Text = stock.Productos.Unidades.abreviacion;
                    cellUnidad.VerticalAlign = VerticalAlign.Middle;
                    cellUnidad.HorizontalAlign = HorizontalAlign.Left;


                    LinkButton btnDetalle = new LinkButton();
                    btnDetalle.ID = "btnDetalle_" + sector.id;
                    btnDetalle.CssClass = "btn btn-xs";
                    //btnDetalle.Click += new EventHandler(this.verEvolucionReceta);
                    btnDetalle.Text = "<span><i style='color:black' class='fa fa-list'></i></span>";
                    btnDetalle.Attributes.Add("title", "Ver Detalle");

                    TableCell celAccion = new TableCell();
                    celAccion.Controls.Add(btnDetalle);
                    celAccion.Attributes.Add("style", "padding-bottom: 1px !important; text-align:end");

                    tr.Cells.Add(cellTipo);
                    tr.Cells.Add(cellDescripcion);
                    tr.Cells.Add(cellStock);
                    tr.Cells.Add(cellUnidad);
                    tr.Cells.Add(celAccion);

                    phItems.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarRecetas()
        {
            try
            {
                var stocks = cStockReceta.ObtenerStockSectoresByIdSector(sectorId);

                foreach (var stock in stocks)
                {
                    //fila
                    TableRow tr = new TableRow();
                    //tr.ID = sector.id.ToString();

                    TableCell cellTipo = new TableCell();
                    cellTipo.Text = "Receta";
                    cellTipo.VerticalAlign = VerticalAlign.Middle;
                    cellTipo.HorizontalAlign = HorizontalAlign.Left;
                    cellTipo.Style.Add("display", "none");

                    TableCell cellDescripcion = new TableCell();
                    cellDescripcion.Text = stock.Recetas.descripcion;
                    cellDescripcion.VerticalAlign = VerticalAlign.Middle;
                    cellDescripcion.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellStock = new TableCell();
                    cellStock.Text = stock.stock.ToString();
                    cellStock.VerticalAlign = VerticalAlign.Middle;
                    cellStock.HorizontalAlign = HorizontalAlign.Left;

                    TableCell cellUnidad = new TableCell();
                    cellUnidad.Text = stock.Recetas.Unidades.abreviacion;
                    cellUnidad.VerticalAlign = VerticalAlign.Middle;
                    cellUnidad.HorizontalAlign = HorizontalAlign.Left;

                    LinkButton btnDetalle = new LinkButton();
                    btnDetalle.ID = "btnDetalle_" + sector.id;
                    btnDetalle.CssClass = "btn btn-xs";
                    //btnDetalle.Click += new EventHandler(this.verEvolucionReceta);
                    btnDetalle.Text = "<span><i style='color:black' class='fa fa-list'></i></span>";
                    btnDetalle.Attributes.Add("title", "Ver Detalle");

                    TableCell celAccion = new TableCell();
                    celAccion.Controls.Add(btnDetalle);
                    celAccion.Attributes.Add("style", "padding-bottom: 1px !important; text-align:end");

                    tr.Cells.Add(cellTipo);
                    tr.Cells.Add(cellDescripcion);
                    tr.Cells.Add(cellStock);
                    tr.Cells.Add(cellUnidad);
                    tr.Cells.Add(celAccion);

                    phItems.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}