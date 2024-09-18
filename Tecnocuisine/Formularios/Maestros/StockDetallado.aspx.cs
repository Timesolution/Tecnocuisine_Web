using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class StockDetallado : System.Web.UI.Page
    {
        ControladorStockReceta controladorStockRecetas = new ControladorStockReceta();
        ControladorStockProducto ControladorStockProducto = new ControladorStockProducto();
        ControladorProducto ControladorProducto = new ControladorProducto();
        ControladorReceta ControladorReceta = new ControladorReceta();
        ControladorUnidad cu = new ControladorUnidad();
        int id;
        int tipo;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["i"]);
                tipo = Convert.ToInt32(Request.QueryString["t"]);
                CargarTablaStockTotal();

            }
        }


        /// <summary>
        /// Verifica el tipo de item (1-producto, 2-receta), obtiene sus stocks correspondientes y los dibuja en tablas
        /// </summary>
        private void CargarTablaStockTotal()
        {
            try
            {
                // Es Producto
                if (tipo == 1)
                {
                    var ListStockTotal = ControladorStockProducto.ObtenerStockProducto(id);

                    var prod = ControladorProducto.ObtenerProductoId(id);

                    //NombreProd.Value = prod.descripcion;


                    //var listStockPresentaciones = ControladorStockProducto.ObtenerStockPresentacionesByIdProducto(id);

                    var listStockSector = ControladorStockProducto.ObtenerStockSectoresByIdProducto(id);

                    //var listStockLotes = ControladorStockProducto.ObtenerStockLotesByIdProducto(id);

                    // Son las cantidades que fueron enviadas entre sectores y que aun no han sido recepcionadas pero siguen siendo parte del stock
                    var stockEnTransito = ControladorStockProducto.ObtenerStockEnTransitoByDescripcion(prod.descripcion);

                    //var ListStockMarcas = ControladorStockProducto.ObtenerStockMarcaByIDProducto(id);

                    if (ListStockTotal != null)
                    {
                        CargarPhFinal(ListStockTotal);
                    }

                    CargarStockEnTransito(stockEnTransito, prod.unidadMedida);

                    //if (listStockPresentaciones != null)
                    //{
                    //    foreach (var i in listStockPresentaciones)
                    //    {

                    //    CargarPhPresentaciones(i);
                    //    }
                    //}
                    if (listStockSector != null)
                    {
                        foreach (var i in listStockSector)
                        {
                            CargarPhSectores(i);
                        }
                    }
                    //if (listStockLotes != null)
                    //{
                    //    foreach (var i in listStockLotes)
                    //    {
                    //        CargarPhLotes(i);
                    //    }
                    //}
                    //if (ListStockMarcas != null)
                    //{
                    //      foreach (var i in ListStockMarcas)
                    //    {
                    //        CargarPhMarcas(i);
                    //    }
                    //}
                }
                // Es Receta
                else
                {
                    var receta = ControladorReceta.ObtenerRecetaId(id);

                    var ListStockTotal = controladorStockRecetas.ObtenerStockReceta(id);
                    //var listStockPresentaciones = controladorStockRecetas.ObtenerStockPresentacionesByIdReceta(id);
                    var listStockSector = controladorStockRecetas.ObtenerStockSectoresRecetaByIdReceta(id);
                    //var listStockLotes = controladorStockRecetas.ObtenerStockLotesRecetaByIdReceta(id);
                    //var listStockMarcas = controladorStockRecetas.ObtenerStockMarcasRecetasByIdReceta(id);

                    // Son las cantidades que fueron enviadas entre sectores y que aun no han sido recepcionadas pero siguen siendo parte del stock
                    var stockEnTransito = controladorStockRecetas.ObtenerStockEnTransitoByDescripcion(receta.descripcion);

                    //NombreProd.Value = receta.descripcion;
                    if (ListStockTotal != null)
                    {
                        CargarPhFinal2(ListStockTotal);
                    }

                    CargarStockEnTransito(stockEnTransito, (int)receta.UnidadMedida);

                    //if (listStockPresentaciones != null)
                    //{
                    //    foreach (var i in listStockPresentaciones)
                    //    {

                    //        CargarPhPresentaciones2(i);
                    //    }
                    //}
                    if (listStockSector != null)
                    {
                        foreach (var i in listStockSector)
                        {
                            CargarPhSectores2(i);
                        }
                    }
                    //if (listStockLotes != null)
                    //{
                    //    foreach (var i in listStockLotes)
                    //    {
                    //        CargarPhLotes2(i);
                    //    }
                    //}
                    //if (listStockMarcas != null)
                    //{
                    //    foreach (var i in listStockMarcas)
                    //    {
                    //        CargarPhMarcas2(i);
                    //    }
                    //}
                }
            }
            catch (Exception)
            {

            }
        }

        private void CargarStockEnTransito(decimal stockEnTransito, int unidadId)
        {
            try
            {
                TableRow tr = new TableRow();
                //tr.ID = listStockTotal.id.ToString();

                TableCell celUM = new TableCell();
                celUM.Text = cu.ObtenerUnidadId(unidadId).descripcion;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);

                TableCell celStock = new TableCell();
                celStock.Text = stockEnTransito.ToString("N", culture);
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Width = Unit.Percentage(5);
                celStock.Attributes.Add("style", "text-align:end");

                tr.Cells.Add(celStock);

                PHTransito.Controls.Add(tr);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //private void CargarPhLotes2(Tecnocuisine_API.Entitys.StockLotesReceta i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);


        //        TableCell celPresentaciones = new TableCell();
        //        celPresentaciones.Text = i.Presentaciones.descripcion;
        //        celPresentaciones.Font.Bold = true;
        //        celPresentaciones.VerticalAlign = VerticalAlign.Middle;
        //        celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
        //        celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celPresentaciones);

        //        TableCell celLotes = new TableCell();
        //        celLotes.Text = i.Lote;
        //        celLotes.Font.Bold = true;
        //        celLotes.VerticalAlign = VerticalAlign.Middle;
        //        celLotes.HorizontalAlign = HorizontalAlign.Left;
        //        celLotes.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celLotes);

        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHLotes.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private void CargarPhLotes(Tecnocuisine_API.Entitys.StockLotes i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);


        //        TableCell celPresentaciones = new TableCell();
        //        celPresentaciones.Text = i.Presentaciones.descripcion;
        //        celPresentaciones.Font.Bold = true;
        //        celPresentaciones.VerticalAlign = VerticalAlign.Middle;
        //        celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
        //        celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celPresentaciones);

        //        TableCell celLotes = new TableCell();
        //        celLotes.Text = i.Lote;
        //        celLotes.Font.Bold = true;
        //        celLotes.VerticalAlign = VerticalAlign.Middle;
        //        celLotes.HorizontalAlign = HorizontalAlign.Left;
        //        celLotes.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celLotes);

        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHLotes.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void CargarPhSectores2(Tecnocuisine_API.Entitys.stockSectoresReceta i)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = i.id.ToString();

                //TableCell celProductoCod = new TableCell();
                //celProductoCod.Text = "";
                //celProductoCod.Font.Bold = true;
                //celProductoCod.VerticalAlign = VerticalAlign.Middle;
                //celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                //celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProductoCod);


                TableCell celPresentaciones = new TableCell();
                celPresentaciones.Text = i.SectorProductivo.descripcion;
                //celPresentaciones.Font.Bold = true;
                celPresentaciones.VerticalAlign = VerticalAlign.Middle;
                celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
                celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPresentaciones);

                string UnidadMedida = cu.ObtenerUnidadId(i.Recetas.UnidadMedida.Value).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                //celUM.Font.Bold = true;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);

                TableCell celStock = new TableCell();
                celStock.Text = i.stock.Value.ToString("N", culture);
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Width = Unit.Percentage(5);
                celStock.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celStock);
                //agrego fila a tabla

                PHSector.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }

        //private void CargarPhMarcas2(Tecnocuisine_API.Entitys.StockMarcaReceta i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);



        //        TableCell celMarcas = new TableCell();
        //        celMarcas.Text = i.Articulos_Marcas.descripcion;
        //        celMarcas.Font.Bold = true;
        //        celMarcas.VerticalAlign = VerticalAlign.Middle;
        //        celMarcas.HorizontalAlign = HorizontalAlign.Left;
        //        celMarcas.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celMarcas);

        //        string UnidadMedida = cu.ObtenerUnidadId((int)i.Recetas.UnidadMedida).descripcion;

        //        TableCell celUM = new TableCell();
        //        celUM.Text = UnidadMedida;
        //        celUM.Font.Bold = true;
        //        celUM.VerticalAlign = VerticalAlign.Middle;
        //        celUM.HorizontalAlign = HorizontalAlign.Left;
        //        celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celUM);


        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHMarcas.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private void CargarPhMarcas(Tecnocuisine_API.Entitys.StockMarca i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);

        //        TableCell celMarcas = new TableCell();
        //        celMarcas.Text = i.Articulos_Marcas.descripcion;
        //        celMarcas.Font.Bold = true;
        //        celMarcas.VerticalAlign = VerticalAlign.Middle;
        //        celMarcas.HorizontalAlign = HorizontalAlign.Left;
        //        celMarcas.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celMarcas);

        //        string UnidadMedida = cu.ObtenerUnidadId(i.Productos.unidadMedida).descripcion;

        //        TableCell celUM = new TableCell();
        //        celUM.Text = UnidadMedida;
        //        celUM.Font.Bold = true;
        //        celUM.VerticalAlign = VerticalAlign.Middle;
        //        celUM.HorizontalAlign = HorizontalAlign.Left;
        //        celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celUM);


        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHMarcas.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void CargarPhSectores(Tecnocuisine_API.Entitys.StockSectores i)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = i.id.ToString();

                //TableCell celProductoCod = new TableCell();
                //celProductoCod.Text = "";
                //celProductoCod.Font.Bold = true;
                //celProductoCod.VerticalAlign = VerticalAlign.Middle;
                //celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                //celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProductoCod);

                TableCell celPresentaciones = new TableCell();
                celPresentaciones.Text = i.SectorProductivo.descripcion;
                //celPresentaciones.Font.Bold = true;
                celPresentaciones.VerticalAlign = VerticalAlign.Middle;
                celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
                celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPresentaciones);

                string UnidadMedida = cu.ObtenerUnidadId(i.Productos.unidadMedida).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                //celUM.Font.Bold = true;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);


                TableCell celStock = new TableCell();
                celStock.Text = i.stock.Value.ToString("N", culture);
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Width = Unit.Percentage(5);
                celStock.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celStock);
                //agrego fila a tabla

                PHSector.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }
        //private void CargarPhPresentaciones2(Tecnocuisine_API.Entitys.stockpresentacionesReceta i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);


        //        TableCell celPresentaciones = new TableCell();
        //        celPresentaciones.Text = i.Presentaciones.descripcion;
        //        celPresentaciones.Font.Bold = true;
        //        celPresentaciones.VerticalAlign = VerticalAlign.Middle;
        //        celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
        //        celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celPresentaciones);

        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHPresentaciones.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void CargarPhPresentaciones(Tecnocuisine_API.Entitys.StockPresentaciones i)
        //{
        //    try
        //    {
        //        TableRow tr = new TableRow();
        //        tr.ID = i.id.ToString();

        //        TableCell celProductoCod = new TableCell();
        //        celProductoCod.Text = "";
        //        celProductoCod.Font.Bold = true;
        //        celProductoCod.VerticalAlign = VerticalAlign.Middle;
        //        celProductoCod.HorizontalAlign = HorizontalAlign.Left;
        //        celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celProductoCod);


        //        TableCell celPresentaciones = new TableCell();
        //        celPresentaciones.Text = i.Presentaciones.descripcion;
        //        celPresentaciones.Font.Bold = true;
        //        celPresentaciones.VerticalAlign = VerticalAlign.Middle;
        //        celPresentaciones.HorizontalAlign = HorizontalAlign.Left;
        //        celPresentaciones.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celPresentaciones);

        //        TableCell celStock = new TableCell();
        //        celStock.Text = i.stock.Value.ToString("N", culture);
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Left;
        //        celStock.Width = Unit.Percentage(5);
        //        celStock.Attributes.Add("style", "text-align:end");
        //        tr.Cells.Add(celStock);
        //        //agrego fila a tabla

        //        PHPresentaciones.Controls.Add(tr);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void CargarPhFinal2(Tecnocuisine_API.Entitys.StockReceta listStockTotal)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = listStockTotal.id.ToString();

                //TableCell celProductoCod = new TableCell();
                //celProductoCod.Text = listStockTotal.Recetas.id.ToString();
                //celProductoCod.Font.Bold = true;
                //celProductoCod.VerticalAlign = VerticalAlign.Middle;
                //celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                //celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProductoCod);


                string UnidadMedida = cu.ObtenerUnidadId(listStockTotal.Recetas.UnidadMedida.Value).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                //celUM.Font.Bold = true;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);


                TableCell celStock = new TableCell();
                celStock.Text = listStockTotal.stock.Value.ToString("N", culture);
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Width = Unit.Percentage(5);
                celStock.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celStock);
                //agrego fila a tabla

                PHStockFinal.Controls.Add(tr);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarPhFinal(Tecnocuisine_API.Entitys.StockProducto listStockTotal)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = listStockTotal.id.ToString();

                //TableCell celProductoCod = new TableCell();
                //celProductoCod.Text = listStockTotal.Productos.id.ToString();
                //celProductoCod.Font.Bold = true;
                //celProductoCod.VerticalAlign = VerticalAlign.Middle;
                //celProductoCod.HorizontalAlign = HorizontalAlign.Left;
                //celProductoCod.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProductoCod);


                string UnidadMedida = cu.ObtenerUnidadId(listStockTotal.Productos.unidadMedida).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                //celUM.Font.Bold = true;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);

                TableCell celStock = new TableCell();
                celStock.Text = listStockTotal.stock.Value.ToString("N", culture);
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Left;
                celStock.Width = Unit.Percentage(5);
                celStock.Attributes.Add("style", "text-align:end");
                tr.Cells.Add(celStock);
                //agrego fila a tabla

                PHStockFinal.Controls.Add(tr);
            }
            catch (Exception ex)
            {

                throw;
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