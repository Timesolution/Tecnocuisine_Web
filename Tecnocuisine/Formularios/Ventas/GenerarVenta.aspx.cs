using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Schema;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using WebGrease.Css.Ast.Selectors;
namespace Tecnocuisine.Formularios.Ventas
{
    public partial class GenerarVenta : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorSectorProductivo ControladorSector = new ControladorSectorProductivo();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        ControladorEntregas ControladorEntregas = new ControladorEntregas();
        ControladorReceta ControladorReceta = new ControladorReceta();
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            if (!IsPostBack)
            {
                ObtenerRecetas();
                ObtenerProductos();
                CargarNumeroVenta();
                //ObtenerNumeroVenta();
                if (accion == 2)
                {
                    //CargarEntregaEdit();
                }
            }
        }
       


        private void CargarNumeroVenta()
        {
            try
            {
                ControladorVentas controladorVentas = new ControladorVentas();
                GenerarVenta generar = new GenerarVenta();
                var listaVentas = controladorVentas.ObtenerTodasLasVentas();
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
                    h3.InnerText = "#" +fac1;
                    lblVentaNum.Controls.Add(h3);
                }
            }
            catch (Exception ex)
            {
            }

        }
        private void ObtenerRecetas()
        {
            try
            {
                var Recetas = ControladorReceta.ObtenerTodosRecetasFinales();


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

        public static void ConfirmarLaVenta(string list)
        {
            try
            {
                ControladorPanelDeControl controladorPanelDeControl = new ControladorPanelDeControl();
                decimal VentaTotal = 0;
                decimal CantidadTotal = 0;
                decimal CostoTotal = 0;
                var PanelDeControl2 = controladorPanelDeControl.GetPanelStock();
                // si es true solo se reduce de Receta // o productos
                if (PanelDeControl2.StockPorProducionYPorVenta == true)
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
                        var Datas = list.Split(';');
                        foreach (var PyR in Datas)
                        {
                            var array = PyR.Split('-');
                            if (array.Length > 0)
                            {
                                int id = Convert.ToInt32(array[0]);
                                string type = array[1];
                                decimal PVenta = Convert.ToDecimal(array[2]);
                                decimal CantVendida = Convert.ToDecimal(array[3]);
                                string Rinde = array[4];
                                decimal costo = Convert.ToDecimal(array[0]);


                                CostoTotal += costo;
                                VentaTotal += PVenta;
                                CantidadTotal += CantVendida;
                                // PRODUCTO
                                if (type.Trim() == "Producto")
                                {
                                    Tecnocuisine_API.Entitys.StockProducto StockTotal = controladorStockProducto.ObtenerStockProducto(id);
                                    Tecnocuisine_API.Entitys.Productos ProdID = cp.ObtenerProductoId(id);
                                    Tecnocuisine_API.Entitys.Unidades UnidadMedida = cu.ObtenerUnidadId(ProdID.unidadMedida);
                                    if (StockTotal == null)
                                    {
                                        StockProducto p = new StockProducto();
                                        p.idProducto = id;
                                        p.stock = -CantVendida;
                                        controladorStockProducto.AgregarStockProducto(p);
                                        StockPresentaciones Pres = new StockPresentaciones();
                                        Pres.idProducto = id;
                                        Pres.idPresentaciones = 17;
                                        Pres.stock = -CantVendida;
                                        controladorStockProducto.AgregarStockPresentacion(Pres);
                                        StockLotes SLotes = new StockLotes();
                                        SLotes.idProducto = id;
                                        SLotes.Lote = "Generado Automaticamente";
                                        SLotes.stock = -CantVendida;
                                        SLotes.idPresentacion = 17;
                                        controladorStockProducto.AgregarStockLotes(SLotes);
                                        StockSectores SSector = new StockSectores();
                                        SSector.idProducto = id;
                                        SSector.idSector = 1;
                                        SSector.stock = -CantVendida;
                                        controladorStockProducto.AgregarStockSectores(SSector);
                                        StockMarca SMarca = new StockMarca();
                                        SMarca.idMarca = 1;
                                        SMarca.idProducto = id;
                                        SMarca.stock = -CantVendida;
                                    }
                                    else
                                    {
                                        StockProducto p = new StockProducto();
                                        p.idProducto = StockTotal.idProducto;
                                        p.stock = StockTotal.stock - CantVendida;
                                        p.id = StockTotal.id;
                                        controladorStockProducto.EditarStockProducto(p);

                                        ReducirDeTodosLosStock(ProdID, UnidadMedida, array);





                                    }
                                }
                                // RECETA
                                else
                                {
                                    Tecnocuisine_API.Entitys.StockReceta StockTotal = controladorStockRecetas.ObtenerStockReceta(id);
                                    Tecnocuisine_API.Entitys.Recetas receta = controladorReceta.ObtenerRecetaId(id);
                                    Tecnocuisine_API.Entitys.Unidades UnidadMedida = cu.ObtenerUnidadId((int)receta.UnidadMedida);
                                    var RP = controladorReceta.ObtenerUnaPresentacionByIdReceta(receta.id);

                                    if (StockTotal == null)
                                    {
                                        Entregas_Recetas p = new Entregas_Recetas();
                                        p.idRecetas = id;
                                        p.Cantidad = -Convert.ToDecimal(array[3]);
                                        p.idEntregas = null;
                                        string fecha = DateTime.Now.ToString();
                                        if (RP == null)
                                        {
                                            int i = controladorStockRecetas.AgregarStockAll_Receta(p, 1, "No Existe Lote", fecha, 1, 1);
                                        }
                                        else
                                        {

                                            int i = controladorStockRecetas.AgregarStockAll_Receta(p, 1, "No Existe Lote", fecha, 1, 1);

                                        }
                                    }
                                    else
                                    {
                                        StockReceta p = new StockReceta();

                                        p.idReceta = StockTotal.idReceta;
                                        p.stock = StockTotal.stock - CantVendida;
                                        p.id = StockTotal.id;
                                        controladorStockRecetas.EditarStockReceta(p);

                                        ReducirDeTodosLosStockReceta(receta, UnidadMedida, array);



                                    }
                                }
                            }
                        }
                    }

                    catch (Exception)
                    {

                    }




                    // SACAR TODO EL STOCK DE LAS MATERIAS PRIMAS
                }
                else
                {

                }
                CrearDetalleVenta(list, CostoTotal, CantidadTotal, VentaTotal);

            }
            catch (Exception)
            {
            }
        }

        public static void CrearDetalleVenta(string list, decimal CostoTotal, decimal CantidadTotal, decimal VentaTotal)
        {
            //document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + producto.split('-')[0].trim() + "-" + tipo + "-" + Venta.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim();
            ControladorVentas controladorVentas = new ControladorVentas();
            GenerarVenta generar = new GenerarVenta();
            VentasDetalle VD = new VentasDetalle();
            var listaVentas = controladorVentas.ObtenerTodasLasVentas();
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
            VD.CantidadTotal = CantidadTotal;
            VD.FechaVenta = DateTime.Now;
            VD.CostoTotal = CostoTotal;
            VD.PrecioVentaTotal = VentaTotal;
            VD.NumeroVenta = fac1;
            var arr = list.Split(';');

            int idVentaDetalle = controladorVentas.AgregarVentaDetalle(VD);
            foreach (var item in arr)
            {
                VentaDetalleRecetaProducto VDRP = new VentaDetalleRecetaProducto();
                var detalles = item.Split('-');
                int id = Convert.ToInt32(detalles[0]);
                string type = detalles[1];
                decimal PVenta = Convert.ToDecimal(detalles[2]);
                decimal CantVendida = Convert.ToDecimal(detalles[3]);
                string Rinde = detalles[4];
                decimal costo = Convert.ToDecimal(detalles[5]);

                if (type == "Receta")
                {
                    VDRP.idProducto = null;
                    VDRP.idReceta = id;
                    VDRP.Cantidad = CantVendida;
                    VDRP.PrecioVenta = PVenta;
                    VDRP.Costo = costo;
                    VDRP.idVenta = idVentaDetalle;
                    VDRP.descripcion = "Sin Descripcion";
                }
                else
                {
                    VDRP.idProducto = id;
                    VDRP.idReceta = null;
                    VDRP.Cantidad = CantVendida;
                    VDRP.PrecioVenta = PVenta;
                    VDRP.Costo = costo;
                    VDRP.idVenta = idVentaDetalle;
                    VDRP.descripcion = "Sin Descripcion";
                }

                controladorVentas.AgregarVentaDetalleRecetaProducto(VDRP);

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
        public static void ReducirDeTodosLosStockReceta(Tecnocuisine_API.Entitys.Recetas receta, Tecnocuisine_API.Entitys.Unidades UnidadMedida, string[] array)
        {
            ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
            ControladorUnidad cu = new ControladorUnidad();
            ControladorReceta cr = new ControladorReceta();
            ControladorReceta controladorReceta = new ControladorReceta();

            ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
            try
            {
                int id = Convert.ToInt32(array[0]);
                string type = array[1];
                decimal PVenta = Convert.ToDecimal(array[2]);
                decimal CantVendida = Convert.ToDecimal(array[3]);
                string Rinde = array[4];
                stockpresentacionesReceta Pres = new stockpresentacionesReceta();
                var RP = controladorReceta.ObtenerUnaPresentacionByIdReceta(receta.id);
                var ListStockPresentaciones = controladorStockReceta.ObtenerStockPresentacionesByIdReceta(receta.id);
                decimal totalCantidadPresentacion = CantVendida;
                // Presentaciones //
                foreach (var item in ListStockPresentaciones)
                {
                    if (totalCantidadPresentacion > 0)
                    {
                        decimal total;


                        var presentaciones = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                        total = presentaciones.cantidad * CantVendida;

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
                                totalKG = (totalKG - CantVendida) / presentaciones.cantidad;
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
                            if (item.stock < 0)
                            {
                                decimal totalNegativo = totalCantidadPresentacion * -1;
                                totalCantidadPresentacion = (decimal)(totalNegativo + item.stock);
                                Pres.stock = totalCantidadPresentacion;
                            }
                            else
                            {

                                totalCantidadPresentacion = (decimal)(totalCantidadPresentacion - item.stock);
                                Pres.stock = 0;
                            }
                        }
                        else
                        {
                            Pres.stock = (decimal)(item.stock - totalCantidadPresentacion);
                            totalCantidadPresentacion = 0;
                        }

                        controladorStockReceta.EditarStockPresentacionesReceta(Pres);

                    }
                }
                // Si TotalCantidadPresentaciones es mayor a 0 osea que no se saldo todo el stock hay que crear stock negativo
                if (totalCantidadPresentacion > 0)
                {

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


                // Lotes // 
                var ListStockLotes = controladorStockReceta.ObtenerStockLotesRecetaByIdReceta(receta.id);
                StockLotesReceta Lotes = new StockLotesReceta();
                decimal TotalCantLotes = CantVendida;
                foreach (var item in ListStockLotes)
                {
                    if (TotalCantLotes > 0)
                    {

                        Lotes.Lote = item.Lote;
                        Lotes.id = item.id;
                        Lotes.idReceta = item.idReceta;
                        Lotes.idPresentacion = item.idPresentacion;
                        if (item.stock >= CantVendida)
                        {

                            Lotes.stock = item.stock - CantVendida;
                            TotalCantLotes = 0;
                        }
                        else
                        {
                            if (item.stock < 0)
                            {
                                decimal totalNegativo = TotalCantLotes * -1;
                                TotalCantLotes = (decimal)(totalNegativo + item.stock);
                                Lotes.stock = TotalCantLotes;
                            }
                            else
                            {
                                TotalCantLotes = (decimal)(CantVendida - item.stock);
                                Lotes.stock = 0;
                            }
                        }
                        controladorStockReceta.EditarStockLotesReceta(Lotes);
                    }

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


            }
            catch (Exception)
            {

            }
        }



        public static void ReducirDeTodosLosStock(Tecnocuisine_API.Entitys.Productos prod, Tecnocuisine_API.Entitys.Unidades uni, string[] array)
        {
            ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
            ControladorUnidad cu = new ControladorUnidad();
            ControladorProducto cp = new ControladorProducto();
            ControladorPresentacion controladorPresentacion = new ControladorPresentacion();

            try
            {
                int id = Convert.ToInt32(array[0]);
                string type = array[1];
                decimal PVenta = Convert.ToDecimal(array[2]);
                decimal CantVendida = Convert.ToDecimal(array[3]);
                string Rinde = array[4];
                StockPresentaciones Pres = new StockPresentaciones();
                var ListStockPresentaciones = controladorStockProducto.ObtenerStockPresentacionesByIdProducto(prod.id);
                decimal totalCantidadPresentacion = CantVendida;
                // Presentaciones //
                foreach (var item in ListStockPresentaciones)
                {
                    if (totalCantidadPresentacion > 0)
                    {
                        var presentaciones = controladorPresentacion.ObtenerPresentacionId(item.idPresentaciones);
                        decimal total = presentaciones.cantidad * CantVendida;
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
                            if (totalKG > CantVendida && totalCantidadPresentacion > 0)
                            {
                                totalKG = (totalKG - CantVendida) / presentaciones.cantidad;
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
                            if (item.stock >= CantVendida)
                            {
                                Pres.stock = item.stock - CantVendida;
                                totalCantidadPresentacion = 0;
                            }
                            else if (item.stock < 0)
                            {
                                decimal totalNegativo = totalCantidadPresentacion * -1;
                                totalCantidadPresentacion = (decimal)(totalNegativo + item.stock);
                                Pres.stock = totalCantidadPresentacion;
                            }
                            //} else
                            //{
                            //    Pres.stock = 0;
                            //    totalCantidadPresentacion = CantVendida - item.stock;
                            //}
                        }
                        controladorStockProducto.EditarStockPresentaciones(Pres);

                    }
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


                // Lotes // 
                var ListStockLotes = controladorStockProducto.ObtenerStockLotesByIdProducto(prod.id);
                StockLotes Lotes = new StockLotes();
                decimal TotalCantLotes = CantVendida;
                foreach (var item in ListStockLotes)
                {
                    if (TotalCantLotes > 0)
                    {

                        Lotes.Lote = item.Lote;
                        Lotes.id = item.id;
                        Lotes.idProducto = item.idProducto;
                        Lotes.idPresentacion = item.idPresentacion;
                        if (item.stock >= TotalCantLotes)
                        {
                            Lotes.stock = item.stock - TotalCantLotes;
                            TotalCantLotes = 0;
                        }
                        else if (item.stock < 0)
                        {
                            decimal totalNegativo = TotalCantLotes * -1;
                            TotalCantLotes = (decimal)(totalNegativo + item.stock);
                            Lotes.stock = TotalCantLotes;
                        }
                        else
                        {
                            TotalCantLotes = (decimal)(TotalCantLotes - item.stock);
                            Lotes.stock = 0;
                        }
                        controladorStockProducto.EditarStockLotes(Lotes);
                    }

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
