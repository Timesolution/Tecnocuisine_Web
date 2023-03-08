﻿using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
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

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class GenerarProduccion : System.Web.UI.Page
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
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        int accion;
        int id;
        int Mensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            if (!IsPostBack)
            {
                ObtenerRecetas();
                CargarDLL();
                if (accion == 2)
                {

                }

                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Produccion Generada con Exito!", "Exito");
                }
            }
            CargarNumeroVenta();
            CargarProducto();
        }



        private void CargarNumeroVenta()
        {
            try
            {
                ControladorVentas controladorVentas = new ControladorVentas();
                GenerarVenta generar = new GenerarVenta();
                var listaVentas = controladorVentas.ObtenerTodasLasVentasProducion();
                string fac1;
                if (listaVentas.Count == 0)
                {
                    fac1 = "000001";
                }
                else
                {
                    string codigo = (listaVentas.Count + 1).ToString();
                    fac1 = generar.GenerarCodigoPedido(codigo);


                    var h3 = new HtmlGenericControl("h3");
                    h3.InnerText = "#" + fac1;
                    lblProdNum.Controls.Add(h3);
                }
            }
            catch (Exception ex)
            {
            }

        }
        private void CargarProducto()
        {
            var receta = ControladorReceta.ObtenerRecetaId(this.id);
            if (receta != null)
            {
                //phTablaProductos

                GetProductosEnRecetasConIDQuery(this.id);
                string Add = receta.id + " - " + receta.descripcion + " - " + "Receta";
                txtDescripcionProductos.Text = Add;
                NRinde.Text = receta.rinde.ToString();
            }
        }
        public void GetProductosEnRecetasConIDQuery(int idProd)
        {
            try
            {

                ControladorReceta controladorReceta = new ControladorReceta();
                List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idProd));

                List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idProd));
                if (listProd.Count > 0)
                {
                    foreach (var item in listProd)
                    {
                        CargarTablaPHProductos(item);
                    }
                }
                if (listRecetas.Count > 0)
                {
                    foreach (var item in listRecetas)
                    {
                        CargarTablaPHRecetas(item);
                    }
                }




            }
            catch (Exception ex)
            {

            }
        }


        public void CargarTablaPHRecetas(Recetas_Receta item)
        {
            try
            {
                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                var unidad = controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida);
                var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                //receta.id + "," + receta.descripcion + "," + item.cantidad + "," + stock + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + ";";

                //fila
                TableRow tr = new TableRow();
                //tr.ID = "presentacion_" + presentacion.id.ToString();
                tr.Attributes.Add("id", "Receta," + receta.id + "," + item.cantidad);
                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = receta.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "vertical-align: middle; text-align: right;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = receta.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(celNombre);

                TableCell CelCantNecesaria = new TableCell();
                CelCantNecesaria.Text = item.cantidad.ToString();
                CelCantNecesaria.VerticalAlign = VerticalAlign.Middle;
                CelCantNecesaria.HorizontalAlign = HorizontalAlign.Left;
                CelCantNecesaria.Attributes.Add("style", "vertical-align: middle; text-align: right;");
                tr.Cells.Add(CelCantNecesaria);

                if (CantStock != null)
                {
                    TableCell Stock = new TableCell();
                    Stock.Text = CantStock.stock.ToString();
                    Stock.VerticalAlign = VerticalAlign.Middle;
                    Stock.HorizontalAlign = HorizontalAlign.Left;
                    Stock.Attributes.Add("style", "vertical-align: middle; text-align: right;");
                    tr.Cells.Add(Stock);
                }
                else
                {
                    TableCell Stock = new TableCell();
                    Stock.Text = "0";
                    Stock.VerticalAlign = VerticalAlign.Middle;
                    Stock.HorizontalAlign = HorizontalAlign.Left;
                    Stock.Attributes.Add("style", "color:red;vertical-align: middle; text-align: right;");
                    tr.Cells.Add(Stock);
                }

                if (unidad != null)
                {

                    TableCell UnidadMedida = new TableCell();
                    UnidadMedida.Text = unidad.descripcion;
                    UnidadMedida.VerticalAlign = VerticalAlign.Middle;
                    UnidadMedida.HorizontalAlign = HorizontalAlign.Left;
                    UnidadMedida.Attributes.Add("style", "vertical-align: middle;");
                    tr.Cells.Add(UnidadMedida);
                }
                else
                {

                    TableCell UnidadMedida = new TableCell();
                    UnidadMedida.Text = controladorUnidad.ObtenerUnidadId(1).descripcion;
                    UnidadMedida.VerticalAlign = VerticalAlign.Middle;
                    UnidadMedida.HorizontalAlign = HorizontalAlign.Left;
                    UnidadMedida.Attributes.Add("style", "vertical-align: middle;");
                    tr.Cells.Add(UnidadMedida);
                }

                TableCell CantReal = new TableCell();
                CantReal.Text = "<input type=\"number\" style=\"withd 100%;\" placeholder=\"Ingresa la cantidad real que utilizaste\" />";
                CantReal.VerticalAlign = VerticalAlign.Middle;
                CantReal.HorizontalAlign = HorizontalAlign.Left;
                CantReal.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(CantReal);

                //agrego fila a tabla
                TableCell Celld = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("a");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar.Attributes.Add("data-original-title", "Producir Esta Receta");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar.Attributes.Add("href", "GenerarProduccion.aspx?i=" + receta.id);
                cbxAgregar.Attributes.Add("target", "_blank");
                cbxAgregar.Style.Add("background-color", "transparent");
                cbxAgregar.InnerHtml = " <i class=\"fa fa-cutlery\"></i>";
                // <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>
                Celld.Controls.Add(cbxAgregar);


                HtmlGenericControl cbxAgregar2 = new HtmlGenericControl("a");
                cbxAgregar2.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar2.Attributes.Add("data-original-title", "Ver Receta");
                cbxAgregar2.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar2.Attributes.Add("href", "/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + receta.id + "&b=1");
                cbxAgregar2.Attributes.Add("target", "_blank");
                cbxAgregar2.Style.Add("background-color", "transparent");
                cbxAgregar2.InnerHtml = "<i class=\"fa fa-search-plus\"></i>";
                Celld.Controls.Add(cbxAgregar2);

                Celld.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(Celld);

                phTablaProductos.Controls.Add(tr);

            }

            catch (Exception)
            {

            }
        }


        public void CargarTablaPHProductos(Recetas_Producto item)
        {
            try
            {
                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                var stock = controladorStockProducto.ObtenerStockProducto(item.idProducto);
                var unidad = controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida);
                //    string faReceta = "<td></td>";
                //string STOCK = "";
                //string Nombre = "<td> " + item.Productos.descripcion + "</td>";
                //string ID = "<td style=\" text-align: right\"> " + item.Productos.id + "</td>";
                //if (stock.stock > item.cantidad)
                //{
                //    STOCK = "<td id=" + 0 + "-" + item.Productos.descripcion + "-" + item.Productos.id + " style=\" text-align: right;\"> " + 0 + "</td>";
                //}
                //else
                //{
                //    STOCK = "<td id=" + 0 + "-" + item.Productos.descripcion + "-" + item.Productos.id + " style=\" text-align: right; color: red;\"> " + 0 + "</td>";
                //}
                //string CantNecesaria = "<td style=\" text-align: right\"> " + item.cantidad + "</td>";
                //string Unidad = "<td style=\" text-align: right\">" + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "</td>";
                //}


                //fila
                TableRow tr = new TableRow();
                //tr.ID = "presentacion_" + presentacion.id.ToString();
                tr.Attributes.Add("id", "Producto," + item.Productos.id + "," + item.cantidad);
                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = item.Productos.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "vertical-align: middle; text-align: right;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = item.Productos.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(celNombre);

                TableCell CelCantNecesaria = new TableCell();
                CelCantNecesaria.Text = item.cantidad.ToString();
                CelCantNecesaria.VerticalAlign = VerticalAlign.Middle;
                CelCantNecesaria.HorizontalAlign = HorizontalAlign.Left;
                CelCantNecesaria.Attributes.Add("style", "vertical-align: middle; text-align: right;");
                tr.Cells.Add(CelCantNecesaria);

                if (stock != null)
                {
                    TableCell Stock = new TableCell();
                    Stock.Text = stock.stock.ToString();
                    Stock.VerticalAlign = VerticalAlign.Middle;
                    Stock.HorizontalAlign = HorizontalAlign.Left;
                    Stock.Attributes.Add("style", "vertical-align: middle;text-align: right;");
                    tr.Cells.Add(Stock);
                }
                else
                {
                    TableCell Stock = new TableCell();
                    Stock.Text = "0";
                    Stock.VerticalAlign = VerticalAlign.Middle;
                    Stock.HorizontalAlign = HorizontalAlign.Left;
                    Stock.Attributes.Add("style", "color:red;vertical-align: middle;text-align: right;");
                    tr.Cells.Add(Stock);
                }
                TableCell UnidadMedida = new TableCell();
                UnidadMedida.Text = unidad.descripcion;
                UnidadMedida.VerticalAlign = VerticalAlign.Middle;
                UnidadMedida.HorizontalAlign = HorizontalAlign.Left;
                UnidadMedida.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(UnidadMedida);

                TableCell CantReal = new TableCell();
                CantReal.Text = "<input type=\"number\" style=\"withd 100%;\" placeholder=\"Ingresa la cantidad real que utilizaste\" />";
                CantReal.VerticalAlign = VerticalAlign.Middle;
                CantReal.HorizontalAlign = HorizontalAlign.Left;
                CantReal.Attributes.Add("style", "vertical-align: middle;");
                tr.Cells.Add(CantReal);

                //agrego fila a tabla
                TableCell Celld = new TableCell();


                tr.Cells.Add(Celld);

                phTablaProductos.Controls.Add(tr);

            }

            catch (Exception)
            {

            }
        }



        private void CargarDLL()
        {
            var presentacion = controladorPresentacion1.ObtenerTodosPresentaciones();
            var marca = controladorMarca.ObtenerTodasMarcas();
            var sector = ControladorSector.ObtenerTodosSectorProductivo();
            var Unidades = controladorUnidad.ObtenerTodosUnidades();
            CargaMarcasOptions(marca);
            CargaPresentacionOptions(presentacion);
            CargaSectorOptions(sector);
            CargarUnidadOptions(Unidades);
        }
        private void ObtenerRecetas()
        {
            try
            {
                var Recetas = ControladorReceta.ObtenerTodosRecetas();


                if (Recetas.Count > 0)
                {
                    CargarRecetasOptions(Recetas);
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

        private void CargarUnidadOptions(List<Tecnocuisine_API.Entitys.Unidades> unidad)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var rec in unidad)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + "'>", rec.id + " - " + rec.descripcion));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaDLLUnidadMedida.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }
        private void CargaMarcasOptions(List<Tecnocuisine_API.Entitys.Articulos_Marcas> marca)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in marca)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + "'>", rec.id + " - " + rec.descripcion));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaDLLMarca.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void CargaSectorOptions(List<Tecnocuisine_API.Entitys.SectorProductivo> sector)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in sector)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + "'>", rec.id + " - " + rec.descripcion));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaDLLSector.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }
        private void CargaPresentacionOptions(List<Tecnocuisine_API.Entitys.Presentaciones> Presentacion)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in Presentacion)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + "'>", rec.id + " - " + rec.descripcion));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaDLLPresentaciones.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void CargarRecetasOptions(List<Tecnocuisine_API.Entitys.Recetas> recetas)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in recetas)
                {
                    string UnidadMedida = "";
                    if (rec.UnidadMedida != null)
                    {
                        if (rec.UnidadMedida.Value == -1)
                            rec.UnidadMedida = 1;

                        UnidadMedida = cu.ObtenerUnidadId(rec.UnidadMedida.Value).descripcion;
                        builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + UnidadMedida + "_" + rec.Costo.ToString().Replace(',', '.') + "'>", rec.id + " - " + rec.descripcion + " - " + "Receta"));
                    }
                    else
                    {

                    }
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaNombreProd.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        [WebMethod]
        public static string GetStockRecetas(int idProd)
        {
            try
            {
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();

                var stock = controladorStockReceta.ObtenerStockReceta(idProd);
                if (stock != null)
                {
                    GenerarProduccion gp = new GenerarProduccion();
                    string stocktotal = ((double)stock.stock).ToString();
                    return stocktotal;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }


        [WebMethod]
        public static string GetReceta(int idProd)
        {
            try
            {
                ControladorReceta controladorReceta = new ControladorReceta();

                Tecnocuisine_API.Entitys.Recetas receta = controladorReceta.ObtenerRecetaId(idProd);
                string All = "";
                if (receta != null)
                {
                    All = receta.Costo.ToString() + " - " + receta.PrVenta.ToString() + " - " + receta.rinde.ToString();
                }
                return All;
            }
            catch (Exception)
            {
                return "";
            }
        }




        [WebMethod]
        public static string GetProducto(int idProd)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();

                Tecnocuisine_API.Entitys.Productos productos = controladorProducto.ObtenerProductoId(idProd);
                string All = "";
                if (productos != null)
                {
                    ControladorUnidad controladorUnidad = new ControladorUnidad();
                    var medida = controladorUnidad.ObtenerUnidadId(productos.unidadMedida);
                    All = productos.costo.ToString() + " - " + "0" + " - " + medida.descripcion.ToString();
                    return All;
                }
                return All;

            }
            catch (Exception)
            {
                return "";
            }
        }


        public void ObtenerProductos()
        {
            try
            {
                var productos = controladorProducto.ObtenerProductosFinales();


                if (productos.Count > 0)
                {
                    CargarProductosOptions(productos);

                    //foreach (var item in productos)
                    //{
                    //    CargarProductosPH(item);
                    //}
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void CargarProductosOptions(List<Tecnocuisine_API.Entitys.Productos> productos)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var prod in productos)
                {
                    string UnidadMedida = "";
                    UnidadMedida = cu.ObtenerUnidadId(prod.unidadMedida).descripcion;
                    builder.Append(String.Format("<option value='{0}' id='c_p_" + prod.id + "_" + prod.descripcion + "_" + UnidadMedida + "_" + prod.costo.ToString().Replace(',', '.') + "'>", prod.id + " - " + prod.descripcion + " - " + "Producto"));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListaNombreProd.InnerHtml += builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }



        public void AgregarNuevoProductoVenta(string prod, int idsector, string fecha)
        {
            Tecnocuisine_API.Entitys.ProductoVentas newProductoVentas = new Tecnocuisine_API.Entitys.ProductoVentas();
            ControladorProductoVenta controladorProductoVenta = new ControladorProductoVenta();
            newProductoVentas.idSector = idsector;
            string[] producto = prod.Split(',');
            string id_Marca = producto[2];
            string id_Producto = producto[0];
            string Tipo = producto[1];
            string Cantidad = producto[3];
            string Presentaciones = producto[5];
            string LoteEnviado = producto[6];
            newProductoVentas.idMarca = Convert.ToInt32(id_Marca);
            newProductoVentas.idProducto = Convert.ToInt32(id_Producto);
            newProductoVentas.idPresentacion = Convert.ToInt32(Presentaciones);
            newProductoVentas.Stock = Convert.ToInt32(Cantidad);
            newProductoVentas.Lote = LoteEnviado;
            newProductoVentas.FechaVencimiento = Convert.ToDateTime(fecha);
            controladorProductoVenta.AgregarProductosVentas(newProductoVentas);
        }


        [WebMethod]
        public static List<PresentacionClass> GetPresentaciones(int idProd, int tipo)
        {
            try
            {
                if (tipo == 1)
                {

                    ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                    var lista = controladorPresentacion.ObtenerPresentacionByIdProducto(idProd);
                    List<Tecnocuisine_API.Entitys.Presentaciones> ListaDDL = new List<Tecnocuisine_API.Entitys.Presentaciones>();
                    foreach (var pres in lista)
                    {
                        ListaDDL.Add(pres.Presentaciones);
                    }
                    List<PresentacionClass> listaFInal = new List<PresentacionClass>();
                    foreach (var pres in ListaDDL)
                    {
                        PresentacionClass pc = new PresentacionClass();
                        pc.id = pres.id;
                        pc.descripcion = pres.descripcion;

                        listaFInal.Add(pc);
                    }

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //javaScript.MaxJsonLength = 5000000;
                    //string resultadoJSON = javaScript.Serialize("Exito al editar el Producto.");
                    //return resultadoJSON;
                    return listaFInal.ToList();
                }
                else if (tipo == 2)
                {
                    ControladorReceta controladorReceta = new ControladorReceta();
                    var lista = controladorReceta.ObtenerPresentacionByIdReceta(idProd);
                    List<Tecnocuisine_API.Entitys.Presentaciones> ListaDDL = new List<Tecnocuisine_API.Entitys.Presentaciones>();
                    foreach (var pres in lista)
                    {
                        ListaDDL.Add(pres.Presentaciones);
                    }
                    List<PresentacionClass> listaFInal = new List<PresentacionClass>();
                    foreach (var pres in ListaDDL)
                    {
                        PresentacionClass pc = new PresentacionClass();
                        pc.id = pres.id;
                        pc.descripcion = pres.descripcion;

                        listaFInal.Add(pc);
                    }

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //javaScript.MaxJsonLength = 5000000;
                    //string resultadoJSON = javaScript.Serialize("Exito al editar el Producto.");
                    //return resultadoJSON;
                    return listaFInal.ToList();
                }
                return new List<PresentacionClass>();
            }
            catch (Exception)
            {
                return new List<PresentacionClass>();

            }
        }


        [WebMethod]
        public static string GetProductosEnRecetas(string idProd)
        {
            try
            {
                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idProd));

                List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idProd));
                if (listProd.Count > 0)
                {
                    foreach (var item in listProd)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(item.idProducto);
                        if (stock != null)
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad + "," + stock.stock + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + ";";
                        }
                        else
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + ";";

                        }
                    }
                }
                if (listRecetas.Count > 0)
                {
                    foreach (var item in listRecetas)
                    {
                        var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                        if (receta != null)
                        {
                            var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                            decimal stock;
                            if (CantStock == null)
                            {
                                stock = 0;
                            }
                            else
                            {
                                stock = (decimal)CantStock.stock;
                            }
                            if (item.Recetas.UnidadMedida == null)
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + stock + "," + item.cantidad + "," + "1" + "," + "Receta" + ";";

                            }
                            else
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + item.cantidad + "," + stock + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + ";";
                            }
                        }
                    }
                }
                return ListFinal;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public static void GenerarProduccionFinal(string List, string Marca, string Presentacion, string UnidadMedida, string Sector, string Lote, string CantidadProducida, string idReceta)
        {
            try
            {
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                ControladorUnidad cu = new ControladorUnidad();
                ControladorReceta cr = new ControladorReceta();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                var item = List.Split(';');
                int marcaid = Convert.ToInt16(Marca.Split('-')[0].Trim());
                int presentacionid = Convert.ToInt16(Presentacion.Split('-')[0].Trim());
                int sectorid = Convert.ToInt16(Sector.Split('-')[0].Trim());
                int unidadid = Convert.ToInt16(UnidadMedida.Split('-')[0].Trim());
                int idreceta = Convert.ToInt16(idReceta);
                decimal cantProducida = Convert.ToDecimal(CantidadProducida);
                foreach (var i in item)
                {
                    if (i != "")
                    {
                        VaciarStockProductos(i.Split(','));
                    }
                }
                var receta = controladorReceta.ObtenerRecetaId(Convert.ToInt16(idReceta));
                if (receta != null)
                {

                    // Stock Receta General
                    var sr = controladorStockReceta.ObtenerStockReceta(receta.id);
                    string fecha = DateTime.Now.ToString();
                    if (sr == null)
                    {
                        Entregas_Recetas p = new Entregas_Recetas();
                        p.idRecetas = receta.id;
                        p.Cantidad = Convert.ToDecimal(CantidadProducida);
                        p.idEntregas = null;

                        int i = controladorStockReceta.AgregarStockAll_Receta(p, sectorid, Lote, fecha, presentacionid, marcaid);



                    }
                    else
                    {

                        // STOCK FINAL
                        StockReceta stockreceta = new StockReceta();
                        stockreceta.idReceta = sr.idReceta;
                        stockreceta.stock = sr.stock + Convert.ToDecimal(CantidadProducida);
                        stockreceta.id = sr.id;
                        controladorStockReceta.EditarStockReceta(stockreceta);


                        // STOCK PRESENTACIONES
                        var SP = controladorStockReceta.ObtenerStockPresentacionesReceta(receta.id, presentacionid);
                        if (SP == null)
                        {
                            var pres = controladorPresentacion.ObtenerPresentacionId(presentacionid);
                            stockpresentacionesReceta spr = new stockpresentacionesReceta();
                            spr.idReceta = receta.id;
                            spr.idPresentacion = presentacionid;
                            spr.stock = pres.cantidad * Convert.ToDecimal(CantidadProducida);
                            controladorStockReceta.AgregarStockPresentacionReceta(spr);
                        }
                        else
                        {
                            var pres = controladorPresentacion.ObtenerPresentacionId(SP.idPresentacion);

                            stockpresentacionesReceta spr = new stockpresentacionesReceta();
                            spr.idReceta = SP.idReceta;
                            spr.id = SP.id;
                            spr.idPresentacion = SP.idPresentacion;
                            spr.stock = pres.cantidad * Convert.ToDecimal(CantidadProducida) + SP.stock;
                            controladorStockReceta.EditarStockPresentacionesReceta(spr);

                        }


                        // STOCK MARCAS 

                        var SM = controladorStockReceta.ObtenerStockMarcasRecetas(receta.id, marcaid);
                        if (SM == null)
                        {
                            StockMarcaReceta smr = new StockMarcaReceta();
                            smr.idReceta = receta.id;
                            smr.idMarca = marcaid;
                            smr.stock = Convert.ToDecimal(CantidadProducida);
                            controladorStockReceta.AgregarStockMarcasReceta(smr);
                        }
                        else
                        {
                            StockMarcaReceta smr = new StockMarcaReceta();
                            smr.idReceta = SM.idReceta;
                            smr.id = SM.id;
                            smr.idMarca = SM.idMarca;
                            smr.stock = Convert.ToDecimal(CantidadProducida) + SM.stock;
                            controladorStockReceta.EditarStockMarcasReceta(smr);
                        }


                        // STOCK LOTES


                        var SL = controladorStockReceta.ObtenerStockLotesReceta(receta.id, Lote);
                        if (SL == null)
                        {
                            StockLotesReceta slr = new StockLotesReceta();
                            slr.idReceta = receta.id;
                            slr.Lote = Lote;
                            slr.stock = Convert.ToDecimal(CantidadProducida);
                            slr.idPresentacion = presentacionid;
                            slr.FechaVencimiento = DateTime.Now;
                            controladorStockReceta.AgregarStockLotesReceta(slr);


                        }
                        else
                        {
                            StockLotesReceta slr = new StockLotesReceta();
                            slr.idReceta = SL.idReceta;
                            slr.id = SL.id;
                            slr.Lote = SL.Lote;
                            slr.stock = Convert.ToDecimal(CantidadProducida) + SL.stock;
                            slr.idPresentacion = SL.idPresentacion;
                            controladorStockReceta.EditarStockLotesReceta(slr);
                        }


                        // STOCK SECTOR


                        var SS = controladorStockReceta.ObtenerStockSectoresReceta(receta.id, sectorid);
                        if (SS == null)
                        {
                            stockSectoresReceta ssr = new stockSectoresReceta();
                            ssr.idReceta = receta.id;
                            ssr.idSector = sectorid;
                            ssr.stock = Convert.ToDecimal(CantidadProducida);
                            controladorStockReceta.AgregarStockSectoresReceta(ssr);


                        }
                        else
                        {
                            stockSectoresReceta ssr = new stockSectoresReceta();
                            ssr.idReceta = SL.idReceta;
                            ssr.id = SS.id;
                            ssr.idSector = SS.idSector;
                            ssr.stock = Convert.ToDecimal(CantidadProducida) + SS.stock;
                            controladorStockReceta.EditarStockSectoresReceta(ssr);
                        }

                    }



                }



                CrearHistoricoProduccion(item, marcaid, presentacionid, sectorid, Lote, cantProducida, unidadid, idreceta);
            }
            catch (Exception)
            {

            }

        }

        public static void CrearHistoricoProduccion(string[] list, int marca, int presentaciones, int sector, string lote, decimal cantProducida, int unidad, int idreceta)
        {
            try
            {


                ControladorVentas controladorVentas = new ControladorVentas();
                VentaProducion VD = new VentaProducion();
                var listaVentas = controladorVentas.ObtenerTodasLasVentasProducion();
                GenerarProduccion generar = new GenerarProduccion();
                string fac1 = "000000";
                if (listaVentas.Count == 0)
                {
                    fac1 = "000001";
                }
                else
                {
                    string codigo = (listaVentas.Count + 1).ToString();
                    fac1 = generar.GenerarCodigoPedido(codigo);
                }
                VD.NumeroProduccion = fac1;
                VD.FechaProduccion = DateTime.Now;
                VD.CantidadProducida = cantProducida;
                VD.idMarca = marca;
                VD.idPresentacion = presentaciones;
                VD.Lote = lote;
                VD.idUnidadMedida = unidad;
                VD.idSector = sector;
                VD.idReceta = idreceta;



                int idVentaProduccion = controladorVentas.AgregarVentaProducion(VD);
                foreach (var item in list)
                {
                    if (item != "")
                    {

                        var arr = item.Split(',');
                        string type = arr[0];
                        int id = Convert.ToInt16(arr[1]);
                        decimal CantProdu = Convert.ToDecimal(arr[3]);
                        VentaProducionRecetaProducto VDRP = new VentaProducionRecetaProducto();


                        if (type == "Receta")
                        {
                            VDRP.idProducto = null;
                            VDRP.idReceta = id;
                            VDRP.CantidadProducida = CantProdu;
                            VDRP.idProducion = idVentaProduccion;
                        }
                        else
                        {
                            VDRP.idProducto = id;
                            VDRP.idReceta = null;
                            VDRP.CantidadProducida = CantProdu;
                            VDRP.idProducion = idVentaProduccion;
                        }

                        controladorVentas.AgregarVentaProducionRecetaProducto(VDRP);

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string GenerarCodigoPedido(string value)
        {
            var value2 = Convert.ToInt64(value);
            int length = value2.ToString().Length;
            int calc = 6 - length;
            int decimalLength = value2.ToString("D").Length + calc;
            var result2 = value2.ToString("D" + decimalLength.ToString());
            return result2;
        }
        public static int VaciarStockProductos(string[] list)
        {
            try
            {
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorUnidad cu = new ControladorUnidad();
                ControladorProducto cp = new ControladorProducto();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockReceta controladorStockRecetas = new ControladorStockReceta();
                ControladorEntregas ControladorEntregas = new ControladorEntregas();
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();

                string type = list[0];
                string id = list[1];

                if (type == "Producto")
                {

                    ReducirStockProducto(cp.ObtenerProductoId(Convert.ToInt32(id)), list);
                }
                else
                {

                    ReducirStockReceta(controladorReceta.ObtenerRecetaId(Convert.ToInt32(id)), list);
                }



                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }




        public static void ReducirStockReceta(Tecnocuisine_API.Entitys.Recetas receta, string[] array)
        {
            ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
            ControladorUnidad cu = new ControladorUnidad();
            ControladorReceta cr = new ControladorReceta();
            ControladorReceta controladorReceta = new ControladorReceta();

            ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
            try
            {
                string type = array[0];
                string id = array[1];
                decimal CantVendida = Convert.ToDecimal(array[3]);


                // StockFinal 

                var StockFinal = controladorStockReceta.ObtenerStockReceta(receta.id);

                if (StockFinal == null)
                {

                    Entregas_Recetas p = new Entregas_Recetas();
                    p.idRecetas = Convert.ToInt16(id);
                    p.Cantidad = -CantVendida;
                    p.idEntregas = null;
                    string fecha = DateTime.Now.ToString();
                    int i = controladorStockReceta.AgregarStockAll_Receta(p, 1, "No Existe Lote", fecha, 1, 1);

                }
                else
                {


                    var sf = new StockReceta();
                    sf.id = StockFinal.id;
                    sf.idReceta = StockFinal.idReceta;
                    sf.stock = StockFinal.stock - CantVendida;

                    controladorStockReceta.EditarStockReceta(sf);

                    stockpresentacionesReceta Pres = new stockpresentacionesReceta();
                    var RP = controladorReceta.ObtenerUnaPresentacionByIdReceta(receta.id);
                    var ListStockPresentaciones = controladorStockReceta.ObtenerStockPresentacionesByIdReceta(receta.id);
                    decimal totalCantidadPresentacion = CantVendida;
                    // Presentaciones //
                    foreach (var item in ListStockPresentaciones)
                    {
                        if (totalCantidadPresentacion > 0)
                        {


                            var presentaciones = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                            decimal divisor = presentaciones.cantidad;
                            if (presentaciones.cantidad < 1)
                            {
                                divisor = 1 / presentaciones.cantidad;
                            }

                            Pres.id = item.id;
                            Pres.idPresentacion = item.idPresentacion;
                            Pres.idReceta = item.idReceta;

                            if (presentaciones.cantidad > 1)
                            {
                                decimal totalkg = (decimal)(presentaciones.cantidad * item.stock);
                                if (totalkg > CantVendida && totalCantidadPresentacion > 0)
                                {
                                    totalkg = (totalkg - CantVendida) / presentaciones.cantidad;
                                    totalCantidadPresentacion = 0;
                                    Pres.stock = totalkg;
                                }
                                else
                                {
                                    totalkg = 0;
                                    totalCantidadPresentacion = CantVendida - totalkg;
                                    Pres.stock = 0;
                                }
                            }
                            else if (presentaciones.cantidad < 1 && totalCantidadPresentacion > 0)
                            {
                                decimal totalKG = (decimal)(presentaciones.cantidad * item.stock);
                                if (totalKG > CantVendida && totalCantidadPresentacion > 0)
                                {
                                    totalKG = (totalKG - CantVendida) * divisor;
                                    totalCantidadPresentacion = 0;
                                    Pres.stock = totalKG;
                                }
                                else
                                {
                                    totalKG = 0;
                                    totalCantidadPresentacion = CantVendida - totalKG;
                                    Pres.stock = 0;
                                }
                            }
                            else if (totalCantidadPresentacion > item.stock)
                            {
                                totalCantidadPresentacion = (decimal)(totalCantidadPresentacion - item.stock);
                                Pres.stock = 0;
                            }
                            else
                            {
                                if (presentaciones.cantidad > 1)
                                {
                                    Pres.stock = (decimal)(item.stock - totalCantidadPresentacion) / divisor;
                                }
                                else
                                {
                                    Pres.stock = (decimal)(item.stock - totalCantidadPresentacion) * divisor;

                                }
                                totalCantidadPresentacion = 0;
                            }

                            controladorStockReceta.EditarStockPresentacionesReceta(Pres);

                        }

                    }
                    // Si TotalCantidadPresentaciones es mayor a 0 osea que no se saldo todo el stock hay que crear stock negativo
                    if (totalCantidadPresentacion > 0)
                    {
                        stockpresentacionesReceta Pres2 = new stockpresentacionesReceta();
                        Pres2.idReceta = Convert.ToInt16(id);
                        Pres2.idPresentacion = 1;
                        Pres2.stock = totalCantidadPresentacion * -1;
                        totalCantidadPresentacion = 0;
                        controladorStockReceta.AgregarStockPresentacionReceta(Pres);


                    }

                    var ListStockMarcas = controladorStockReceta.ObtenerStockMarcasRecetasByIdReceta(receta.id);
                    // Marcas //
                    StockMarcaReceta Marc = new StockMarcaReceta();
                    decimal totalCantidadMarca = CantVendida;
                    foreach (var item in ListStockMarcas)
                    {
                        if (totalCantidadMarca >= 0)
                        {

                            Marc.idMarca = item.idMarca;
                            Marc.id = item.id;
                            Marc.idReceta = item.idReceta;
                            if (item.stock >= CantVendida)
                            {
                                Marc.stock = item.stock - CantVendida;
                                totalCantidadMarca = 0;
                            }
                            else
                            {
                                if (item.stock < 0)
                                {
                                    decimal totalNegativo = totalCantidadMarca * -1;
                                    totalCantidadMarca = (decimal)(totalNegativo + item.stock);
                                    Marc.stock = totalCantidadMarca;
                                }
                                else
                                {
                                    totalCantidadMarca = (decimal)(CantVendida - item.stock);
                                    Marc.stock = 0;
                                }
                            }
                            controladorStockReceta.EditarStockMarcasReceta(Marc);
                        }

                    }

                    if (totalCantidadMarca > 0)
                    {
                        StockMarcaReceta Marc2 = new StockMarcaReceta();
                        Marc2.idMarca = 1;
                        Marc2.idReceta = Convert.ToInt16(id);
                        Marc2.stock = totalCantidadMarca * -1;

                        controladorStockReceta.AgregarStockMarcasReceta(Marc2);
                    }


                    // Lotes // 
                    var ListStockLotes = controladorStockReceta.ObtenerStockLotesRecetaByIdReceta(receta.id);
                    StockLotesReceta Lotes = new StockLotesReceta();
                    decimal TotalCantLotes = CantVendida;
                    foreach (var item in ListStockLotes)
                    {
                        if (TotalCantLotes > 0)
                        {
                            var presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                            Lotes.Lote = item.Lote;
                            Lotes.id = item.id;
                            Lotes.idReceta = item.idReceta;
                            Lotes.idPresentacion = item.idPresentacion;
                            decimal totalKg = (decimal)(item.stock * presentacion.cantidad);
                            decimal divisor = presentacion.cantidad;
                            if (presentacion.cantidad < 1)
                            {
                                divisor = 1 / presentacion.cantidad;
                            }
                            if (totalKg >= TotalCantLotes)
                            {
                                if (presentacion.cantidad > 1)
                                {

                                    Lotes.stock = (totalKg - TotalCantLotes) / divisor;
                                }
                                else
                                {
                                    Lotes.stock = (totalKg - TotalCantLotes) * divisor;
                                }
                                TotalCantLotes = 0;
                            }
                            else
                            {
                                TotalCantLotes = (decimal)(TotalCantLotes - totalKg);
                                Lotes.stock = 0;
                            }
                            controladorStockReceta.EditarStockLotesReceta(Lotes);
                        }

                    }


                    if (TotalCantLotes > 0)
                    {
                        StockLotesReceta Lotes2 = new StockLotesReceta();
                        Lotes.idPresentacion = 1;
                        Lotes.idReceta = Convert.ToInt16(id);
                        Lotes.stock = TotalCantLotes * -1;
                        Lotes.Lote = "No Existe Lote";
                        controladorStockReceta.AgregarStockLotesReceta(Lotes2);
                    }


                    // Sector //
                    var ListStockSector = controladorStockReceta.ObtenerStockSectoresRecetaByIdReceta(receta.id);
                    stockSectoresReceta Sector = new stockSectoresReceta();
                    decimal TotalCantSector = CantVendida;
                    foreach (var item in ListStockSector)
                    {
                        if (TotalCantSector > 0)
                        {
                            Sector.id = item.id;
                            Sector.idReceta = item.idReceta;
                            Sector.idSector = item.idSector;
                            if (item.stock >= CantVendida)
                            {
                                Sector.stock = item.stock - TotalCantSector;
                                TotalCantSector = 0;
                            }
                            else
                            {
                                if (item.stock < 0)
                                {
                                    decimal totalNegativo = TotalCantSector * -1;
                                    TotalCantSector = (decimal)(totalNegativo + item.stock);
                                    Sector.stock = TotalCantSector;
                                }
                                else
                                {
                                    TotalCantSector = (decimal)(CantVendida - item.stock);
                                    Sector.stock = 0;
                                }
                            }
                            controladorStockReceta.EditarStockSectoresReceta(Sector);
                        }

                    }


                    if (TotalCantSector > 0)
                    {
                        stockSectoresReceta Sector2 = new stockSectoresReceta();
                        Sector2.idReceta = Convert.ToInt16(id);
                        Sector2.stock = TotalCantLotes * -1;
                        Sector2.idSector = 1;
                        controladorStockReceta.AgregarStockSectoresReceta(Sector2);
                    }

                }

            }
            catch (Exception)
            {

            }
        }


        public static void ReducirStockProducto(Tecnocuisine_API.Entitys.Productos prod, string[] array)
        {
            ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
            ControladorUnidad cu = new ControladorUnidad();
            ControladorProducto cp = new ControladorProducto();
            ControladorPresentacion controladorPresentacion = new ControladorPresentacion();

            try
            {
                string type = array[0];
                string id = array[1];
                decimal CantVendida = Convert.ToDecimal(array[3]);

                // StockFinal 

                var StockFinal = controladorStockProducto.ObtenerStockProducto(prod.id);
                if (StockFinal == null)
                {
                    StockProducto p = new StockProducto();
                    p.idProducto = Convert.ToInt16(id);
                    p.stock = CantVendida * -1;
                    controladorStockProducto.AgregarStockProducto(p);
                    StockPresentaciones Present = new StockPresentaciones();
                    Present.idProducto = Convert.ToInt16(id);
                    Present.idPresentaciones = 1;
                    Present.stock = CantVendida * -1;
                    controladorStockProducto.AgregarStockPresentacion(Present);
                    StockLotes SLotes = new StockLotes();
                    SLotes.idProducto = Convert.ToInt16(id);
                    SLotes.Lote = "No Existe Lote";
                    SLotes.stock = CantVendida * -1;
                    SLotes.idPresentacion = 1;
                    controladorStockProducto.AgregarStockLotes(SLotes);
                    StockSectores SSector = new StockSectores();
                    SSector.idProducto = Convert.ToInt16(id);
                    SSector.idSector = 1;
                    SSector.stock = CantVendida * -1;
                    controladorStockProducto.AgregarStockSectores(SSector);
                    StockMarca SMarca = new StockMarca();
                    SMarca.idMarca = 1;
                    SMarca.idProducto = Convert.ToInt16(id);
                    SMarca.stock = CantVendida * -1;
                    controladorStockProducto.AgregarStockMarca(SMarca);
                }
                else
                {

                    var sf = new StockProducto();
                    sf.id = StockFinal.id;
                    sf.idProducto = StockFinal.idProducto;
                    sf.stock = StockFinal.stock - CantVendida;

                    controladorStockProducto.EditarStockProducto(sf);

                    StockPresentaciones Pres = new StockPresentaciones();
                    var ListStockPresentaciones = controladorStockProducto.ObtenerStockPresentacionesByIdProducto(prod.id);
                    decimal totalCantidadPresentacion = CantVendida;
                    // Presentaciones //
                    foreach (var item in ListStockPresentaciones)
                    {
                        if (totalCantidadPresentacion > 0)
                        {
                            var presentaciones = controladorPresentacion.ObtenerPresentacionId(item.idPresentaciones);
                            decimal total = 0;
                            decimal divisor = presentaciones.cantidad;
                            if (presentaciones.cantidad < 1)
                            {
                                divisor = 1 / presentaciones.cantidad;
                            }
                            if (presentaciones.cantidad < 1)
                            {
                                total = CantVendida / presentaciones.cantidad;

                            }
                            else
                            {
                                total = presentaciones.cantidad * CantVendida;
                            }
                            Pres.id = item.id;
                            Pres.idPresentaciones = item.idPresentaciones;
                            Pres.idProducto = item.idProducto;
                            if (presentaciones.cantidad > 1)
                            {
                                decimal totalkg = (decimal)(presentaciones.cantidad * item.stock);
                                if (totalkg > CantVendida && totalCantidadPresentacion > 0)
                                {
                                    totalkg = (totalkg - CantVendida) / presentaciones.cantidad;
                                    totalCantidadPresentacion = 0;
                                    Pres.stock = totalkg;
                                }
                                else
                                {
                                    totalkg = 0;
                                    totalCantidadPresentacion = CantVendida - totalkg;
                                    Pres.stock = 0;
                                }
                            }
                            else if (presentaciones.cantidad < 1 && totalCantidadPresentacion > 0)
                            {
                                decimal totalKG = (decimal)(presentaciones.cantidad * item.stock);
                                if (totalKG > totalCantidadPresentacion && totalCantidadPresentacion > 0)
                                {
                                    totalKG = (totalKG - CantVendida) / divisor;
                                    totalCantidadPresentacion = 0;
                                    Pres.stock = totalKG;
                                }
                                else
                                {
                                    totalCantidadPresentacion = CantVendida - totalKG;
                                    Pres.stock = 0;
                                }
                            }
                            else if (totalCantidadPresentacion > 0)
                            {
                                if ((item.stock * presentaciones.cantidad) >= CantVendida)
                                {
                                    Pres.stock = (item.stock - totalCantidadPresentacion) * divisor;
                                    totalCantidadPresentacion = 0;
                                }
                                else
                                {
                                    Pres.stock = 0;
                                    totalCantidadPresentacion = (decimal)(totalCantidadPresentacion - (decimal)(presentaciones.cantidad * item.stock));
                                }
                            }
                            controladorStockProducto.EditarStockPresentaciones(Pres);

                        }
                    }

                    if (totalCantidadPresentacion > 0)
                    {
                        StockPresentaciones Present = new StockPresentaciones();
                        Present.idProducto = Convert.ToInt16(id);
                        Present.idPresentaciones = 1;
                        Present.stock = totalCantidadPresentacion * -1;
                        controladorStockProducto.AgregarStockPresentacion(Present);
                    }

                    var ListStockMarcas = controladorStockProducto.ObtenerStockMarcaByIDProducto(prod.id);
                    // Marcas //
                    StockMarca Marc = new StockMarca();
                    decimal totalCantidadMarca = CantVendida;
                    foreach (var item in ListStockMarcas)
                    {
                        if (totalCantidadMarca >= 0)
                        {

                            Marc.idMarca = item.idMarca;
                            Marc.id = item.id;
                            Marc.idProducto = item.idProducto;
                            if (item.stock > totalCantidadMarca)
                            {
                                Marc.stock = item.stock - totalCantidadMarca;
                                totalCantidadMarca = 0;
                            }
                            else if (item.stock < 0)
                            {
                                decimal totalNegativo = totalCantidadMarca * -1;
                                totalCantidadMarca = (decimal)(totalNegativo + item.stock);
                                Marc.stock = totalCantidadMarca;
                            }
                            else
                            {
                                totalCantidadMarca = (decimal)(totalCantidadMarca - item.stock);
                                Marc.stock = 0;
                            }
                            controladorStockProducto.EditarStockMarca(Marc);
                        }

                    }

                    if (totalCantidadMarca > 0)
                    {
                        StockMarca SMarca = new StockMarca();
                        SMarca.idMarca = 1;
                        SMarca.idProducto = Convert.ToInt16(id);
                        SMarca.stock = totalCantidadMarca * -1;
                        controladorStockProducto.AgregarStockMarca(SMarca);
                    }
                    // Lotes // 
                    var ListStockLotes = controladorStockProducto.ObtenerStockLotesByIdProducto(prod.id);
                    StockLotes Lotes = new StockLotes();
                    decimal TotalCantLotes = CantVendida;
                    foreach (var item in ListStockLotes)
                    {
                        if (TotalCantLotes > 0)
                        {
                            var presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                            Lotes.Lote = item.Lote;
                            Lotes.id = item.id;
                            Lotes.idProducto = item.idProducto;
                            Lotes.idPresentacion = item.idPresentacion;
                            decimal totalKg = (decimal)(item.stock * presentacion.cantidad);
                            decimal divisor = presentacion.cantidad;
                            if (presentacion.cantidad < 1)
                            {
                                divisor = 1 / presentacion.cantidad;
                            }
                            if (totalKg >= TotalCantLotes)
                            {
                                if (presentacion.cantidad > 1)
                                {

                                    Lotes.stock = (totalKg - TotalCantLotes) / divisor;
                                }
                                else
                                {
                                    Lotes.stock = (totalKg - TotalCantLotes) * divisor;
                                }
                                TotalCantLotes = 0;
                            }
                            else
                            {
                                TotalCantLotes = (decimal)(TotalCantLotes - totalKg);
                                Lotes.stock = 0;
                            }
                            controladorStockProducto.EditarStockLotes(Lotes);
                        }

                    }

                    if (TotalCantLotes > 0)
                    {
                        StockLotes SLotes = new StockLotes();
                        SLotes.idProducto = Convert.ToInt16(id);
                        SLotes.Lote = "No Existe Lote";
                        SLotes.stock = TotalCantLotes * -1;
                        SLotes.idPresentacion = 1;
                        controladorStockProducto.AgregarStockLotes(SLotes);
                    }
                    // Sector //
                    var ListStockSector = controladorStockProducto.ObtenerStockSectoresByIdProducto(prod.id);
                    StockSectores Sector = new StockSectores();
                    decimal TotalCantSector = CantVendida;
                    foreach (var item in ListStockSector)
                    {
                        if (TotalCantSector > 0)
                        {
                            Sector.id = item.id;
                            Sector.idProducto = item.idProducto;
                            Sector.idSector = item.idSector;
                            if (item.stock >= TotalCantSector)
                            {
                                Sector.stock = item.stock - TotalCantSector;
                                TotalCantSector = 0;
                            }
                            else if (item.stock < 0)
                            {
                                decimal totalNegativo = TotalCantSector * -1;
                                TotalCantSector = (decimal)(totalNegativo + item.stock);
                                Sector.stock = TotalCantSector;
                            }
                            else
                            {
                                TotalCantLotes = (decimal)(TotalCantSector - item.stock);
                                Lotes.stock = 0;
                            }
                            controladorStockProducto.EditarStockSectores(Sector);
                        }

                    }

                    if (TotalCantSector > 0)
                    {
                        StockSectores SSector = new StockSectores();
                        SSector.idProducto = Convert.ToInt16(id);
                        SSector.idSector = 1;
                        SSector.stock = TotalCantSector * -1;
                        controladorStockProducto.AgregarStockSectores(SSector);
                    }
                }

            }
            catch (Exception)
            {

            }
        }










        bool ObtenerPruductoDeLaReceta(int idReceta)
        {
            ControladorReceta controlador = new ControladorReceta();
            ControladorUnidad cu = new ControladorUnidad();
            ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
            try
            {



                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                var receta = controlador.ObtenerRecetaId(idReceta);              //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        bool type = ObtenerPruductoDeLaReceta(rr.idRecetaIngrediente);
                    }
                }
                //productos
                foreach (var RP in listaRP)
                {

                    if (RP != null)
                    {
                        var StockTotal = controladorStockProducto.ObtenerStockProducto(RP.idProducto);
                        var UnidadMedida = cu.ObtenerUnidadId(RP.Productos.unidadMedida);
                        if (StockTotal == null)
                        {
                            StockProducto p = new StockProducto();
                            p.idProducto = id;
                            p.stock = -Convert.ToDecimal(RP.cantidad);
                            controladorStockProducto.AgregarStockProducto(p);
                            StockPresentaciones Pres = new StockPresentaciones();
                            Pres.idProducto = id;
                            Pres.idPresentaciones = UnidadMedida.id;
                            Pres.stock = -Convert.ToDecimal(RP.cantidad);
                            controladorStockProducto.AgregarStockPresentacion(Pres);
                            StockLotes SLotes = new StockLotes();
                            SLotes.idProducto = id;
                            SLotes.Lote = "Generado Automaticamente";
                            SLotes.stock = -Convert.ToDecimal(RP.cantidad);
                            controladorStockProducto.AgregarStockLotes(SLotes);
                            StockSectores SSector = new StockSectores();
                            SSector.idProducto = id;
                            SSector.idSector = 1;
                            SSector.stock = -Convert.ToDecimal(RP.cantidad);
                            controladorStockProducto.AgregarStockSectores(SSector);
                            StockMarca SMarca = new StockMarca();
                            SMarca.idMarca = 1;
                            SMarca.idProducto = id;
                            SMarca.stock = -Convert.ToDecimal(RP.cantidad);
                        }
                        else
                        {
                            StockProducto p = new StockProducto();
                            p.idProducto = StockTotal.idProducto;
                            p.stock = StockTotal.stock - Convert.ToDecimal(RP.cantidad);
                            controladorStockProducto.EditarStockProducto(StockTotal);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }





        public class PresentacionClass
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}
