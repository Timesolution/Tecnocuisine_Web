using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Stock : Page
    {
        Mensaje m = new Mensaje();
        ControladorStock controladorStock = new ControladorStock();
        Gestion_Api.Controladores.controladorSucursal contSucu = new Gestion_Api.Controladores.controladorSucursal();
        int accion;
        int idAlicuota;
        int Mensaje;
        private int suc;
        private int idArticulo;
        private string fechaD;
        private string fechaH;
        private int sucursal;

        private int permisoEditar = 0;

        protected void Page_Load(object sender, EventArgs e)
        {


            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idAlicuota = Convert.ToInt32(Request.QueryString["i"]);
            this.idArticulo = Convert.ToInt32(Request.QueryString["articulo"]);
            this.fechaD = Request.QueryString["fd"].ToString();
            this.fechaH = Request.QueryString["fh"].ToString();
            VerificarLogin();

            if (!IsPostBack)
            {


                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 2)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 3)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

                if (accion == 2) //Auditoria stock
                {
                    this.suc = Convert.ToInt32(Request.QueryString["s"]);
                    this.lstSucursal.SelectedValue = this.suc.ToString();
                    this.txtFechaDesdeMov.Text = fechaD;
                    this.txtFechaHastaMov.Text = fechaH;
                    this.cargarMovimientoStock();
                    //this.btnAccion.Visible = true;
                }
                if (accion == 0)
                {
                    this.suc = Convert.ToInt32(Request.QueryString["s"]);
                    this.lstSucursal.SelectedValue = this.suc.ToString();
                    this.txtFechaDesdeMov.Text = DateTime.Today.AddDays(-7).ToString("dd/MM/yyyy");
                    this.txtFechaHastaMov.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    this.filtrar();
                }
                //if (accion == 3) //Cantidad ventas PRP FACT
                //{
                //    this.suc = Convert.ToInt32(Request.QueryString["s"]);
                //    this.ListSucursalPF.SelectedValue = this.suc.ToString();
                //    this.txtFechaDesdeMov.Text = DateTime.Today.AddDays(-7).ToString("dd/MM/yyyy");
                //    this.txtFechaHastaMov.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //    this.txtDesdePF.Text = fechaD;
                //    this.txtHastaPF.Text = fechaH;
                //    this.lblParametros.Text = fechaD + "," + fechaH + ", " + this.lstSucursal.SelectedItem.Text;
                //    this.cargarVentasProducto();
                //    this.cargarCompraProducto();
                //    this.btnAccion.Visible = true;
                //}
                cargarSucursal();
                this.cargarStockPRoducto();
                this.cargarStockTotalProducto();


            }


        }
        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Account/Login.aspx");
                }
                else
                {
                    //    if (this.contUser.validarAcceso((int)Session["Login_IdUser"], "Maestro.Articulos.Articulos") != 1)
                    //    {
                    //        Response.Redirect("/Default.aspx?m=1", false);
                    //    }
                    if (this.verificarAcceso() != 1)
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

        private int verificarAcceso()
        {
            try
            {
                string permisos = Session["Login_Permisos"] as string;
                string[] listPermisos = permisos.Split(';');
                string permiso = listPermisos.Where(x => x == "14").FirstOrDefault();

                if (permiso == null)
                {
                    return 0;
                }
                else
                {
                    //verifico si puede cambiar preci0s
                    string permiso2 = listPermisos.Where(x => x == "62").FirstOrDefault();
                    if (permiso2 != null)
                    {
                        if (accion == 2)
                        {
                            this.permisoEditar = 1;

                        }
                    }
                    //verifico si puede cambiar stock
                    string permiso3 = listPermisos.Where(x => x == "69").FirstOrDefault();
                    if (permiso3 != null)
                    {
                        if (accion == 2)
                        {
                            this.permisoEditar = 1;
                        }
                    }

                    //verifico si es super admin
                    string perfil = Session["Login_NombrePerfil"] as string;
                    if (perfil == "SuperAdministrador")
                    {
                        this.permisoEditar = 1;
                        this.lstSucursal.Attributes.Remove("disabled");
                    }
                    else
                    {
                        //o si tiene permiso cambio suc                        
                        string permiso4 = listPermisos.Where(x => x == "153").FirstOrDefault();
                        if (permiso4 != null)
                        {
                            this.lstSucursal.Attributes.Remove("disabled");
                        }
                        else
                        {
                            this.lstSucursal.SelectedValue = Session["Login_SucUser"].ToString();
                        }
                    }

                    return 1;
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private void cargarStockPRoducto()
        {
            try
            {
                int suc = (int)Session["Login_SucUser"];
                phStock.Controls.Clear();
                List<Tecnocuisine_API.Entitys.Stock> stocks = this.controladorStock.obtenerStockArticulo(this.idArticulo, this.sucursal);
                stocks = stocks.OrderBy(x => x.sucursal.nombre).ToList();
                foreach (Tecnocuisine_API.Entitys.Stock s in stocks)
                {
                    if (suc == s.sucursal.id)
                    {
                        this.lblCantidadSucursal.Text = s.stock1.ToString();
                        this.lblSucursal.Text = s.sucursal.nombre;
                    }

                    this.cargarStockTable(s);
                    //labelNombre.Text = s.Productos.id + "- " + s.Productos.descripcion;

                }
            }

            catch
            {

            }
        }

        private void cargarStockTotalProducto()
        {
            try
            {
                if (this.idArticulo > 0)
                {
                    var list = this.controladorStock.obtenerStockArticulo(this.idArticulo, this.sucursal);
                    decimal total = 0;

                    if (list != null)
                    {
                        foreach (Tecnocuisine_API.Entitys.Stock s in list)
                        {
                            total += s.stock1;
                        }

                        lblStockTotal.Text = total.ToString();
                    }
                }

            }
            catch (Exception Ex)
            {
            }
        }

        private void cargarStockTable(Tecnocuisine_API.Entitys.Stock s)
        {
            try
            {
                TableRow tr = new TableRow();

                TableCell celSucursal = new TableCell();
                celSucursal.Text = s.sucursal.nombre;
                celSucursal.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celSucursal);

                TableCell celStock = new TableCell();
                celStock.Text = s.stock1.ToString("N");
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(celStock);

                //cargarVisualizacionTablaStock(tr, s);
                TableCell celAccion = new TableCell();

                LinkButton btnEditar = new LinkButton();
                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("title data-original-title", "Editar");
                btnEditar.ID = "btnEditar_" + s.id;
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.OnClientClick = "create(" + s.id + ");";
                btnEditar.Attributes.Add("data-toggle", "modal");
                btnEditar.Attributes.Add("href", "#modalAgregar");
                btnEditar.Attributes.Add("OnClientClick", "create(" + s.id + ")");
                //permiso editar
                if (this.permisoEditar == 0)
                {
                    btnEditar.Visible = false;
                }

                celAccion.Controls.Add(btnEditar);
                celAccion.VerticalAlign = VerticalAlign.Middle;
                celAccion.Width = Unit.Percentage(12);
                tr.Cells.Add(celAccion);

                phStock.Controls.Add(tr);

            }
            catch (Exception ex)
            {
            }
        }
        //void cargarVisualizacionTablaStock(TableRow tr, Stock s)//carga las columnas dinamicamente
        //{
        //    VisualizacionStock visualizacionStock = new VisualizacionStock();
        //    var stockImpPendiente = controladorStock.obtenerStockImportacionesPendientesBySuc(s.articulo.id, s.sucursal.id);//asigna el dato; 
        //    var stockRemitoPendiente = controladorStock.obtenerStockRemitosPendientesBySuc(s.articulo.id, s.sucursal.id);//asigna el dato 
        //    if (visualizacionStock.columnaImportacionesPendientes == 1)
        //    {
        //        TableCell celStock = new TableCell();
        //        celStock.Text = stockImpPendiente.ToString("N");
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Right;
        //        tr.Cells.Add(celStock);
        //        phImportacionesPendientes.Visible = true;
        //    }
        //    if (visualizacionStock.columnaRemitosPendientes == 1)
        //    {
        //        TableCell celStock = new TableCell();
        //        celStock.Text = stockRemitoPendiente.ToString("N");
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Right;
        //        tr.Cells.Add(celStock);
        //        phRemitosPendientes.Visible = true;
        //    }
        //    if (visualizacionStock.columnaStockReal == 1)
        //    {
        //        TableCell celStock = new TableCell();
        //        celStock.Text = (stockImpPendiente + stockRemitoPendiente).ToString("N");
        //        celStock.VerticalAlign = VerticalAlign.Middle;
        //        celStock.HorizontalAlign = HorizontalAlign.Right;
        //        tr.Cells.Add(celStock);
        //        phStockReal.Visible = true;
        //    }


        //}

        public void cargarSucursal()
        {
            try
            {

                DataTable dt = contSucu.obtenerSucursales();

                //agrego Seleccione...
                //DataRow dr = dt.NewRow();
                //dr["nombre"] = "Seleccione...";
                //dr["id"] = -1;
                //dt.Rows.InsertAt(dr, 0);

                this.lstSucursal.DataSource = dt;
                this.lstSucursal.DataValueField = "Id";
                this.lstSucursal.DataTextField = "nombre";

                this.lstSucursal.DataBind();
                this.lstSucursal.SelectedValue = Session["Login_SucUser"].ToString();



            }
            catch (Exception ex)
            {
            }
        }



        private void cargarMovimientoStock()
        {
            try
            {
                phMovimientoStock.Controls.Clear();
                DateTime desde = Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR"));
                DateTime hasta = Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")).AddHours(23);

                DataTable dt = this.controladorStock.obtenerMovimientoStockArticuloCompra(this.idArticulo.ToString(), desde, hasta, this.suc);

                dt.DefaultView.Sort = "Fecha";
                dt = dt.DefaultView.ToTable();

                decimal saldo = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    decimal cantidad = Convert.ToDecimal(dr["Cantidad"]);

                    this.cargarMovimientoStock(dr, cantidad);
                    saldo += cantidad;
                }
                //this.labelSaldo.Text = saldo.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        private void cargarMovimientoStock(DataRow dr, decimal cantidad)
        {
            try
            {
                TableRow tr = new TableRow();
                //Convert.ToDateTime(dr["Fecha"].ToString(), new CultureInfo("es-AR"));
                TableCell celFecha = new TableCell();
                celFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy");
                celFecha.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFecha);

                TableCell celTipo = new TableCell();
                //verifico si es una nota de credito y lo pongo como ingreso
                if (!dr["Numero"].ToString().Contains("Credito"))
                {
                    if (dr["Tipo"].ToString().Contains("Compra a"))//si es una compra a otra sucursal
                    {
                        celTipo.Text = "Compra Interna";
                    }
                    else
                    {
                        if (dr["Tipo"].ToString().Contains("RemitoCompra") || dr["Tipo"].ToString().Contains("Baja"))//si es anulacion de remito compra
                        {
                            celTipo.Text = "Egreso";
                        }
                        else
                        {
                            if (dr["Tipo"].ToString().Contains("Compuesto"))
                            {
                                celTipo.Text = "Articulo Compuesto";
                            }
                            else
                            {
                                celTipo.Text = dr["Tipo"].ToString();//si es ingreso o egreso por RemitoCompra o Venta.
                            }
                        }
                    }
                }
                else//si es nta de credito lo anoto como un ingreso
                {
                    celTipo.Text = "Ingreso";
                }
                celTipo.VerticalAlign = VerticalAlign.Middle;
                celTipo.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celTipo);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = dr["Tipo"].ToString();

                if (dr["Tipo"].ToString() == "Ingreso")
                {
                    celDescripcion.Text = "Remito " + dr["Numero"].ToString();
                }
                if (dr["Tipo"].ToString() == "Inventario")
                {
                    celDescripcion.Text = "Correcion stock 0" + dr["Numero"].ToString();
                }
                if (dr["Tipo"].ToString().Contains("Compra a"))
                {
                    celDescripcion.Text = dr["Tipo"].ToString();
                }
                if (dr["Tipo"].ToString() == "Egreso")
                {
                    celDescripcion.Text = "Remito " + dr["Numero"].ToString();
                }
                if (dr["Tipo"].ToString().Contains("RemitoCompra") || dr["Tipo"].ToString().Contains("Baja"))
                {
                    celDescripcion.Text = dr["Tipo"].ToString();
                }
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                //celCantidad.Text = dr["Cantidad"].ToString();
                celCantidad.Text = cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(celCantidad);

                TableCell celCliente = new TableCell();
                if (dr["SucursalFacturada"].ToString() != "0" && !String.IsNullOrEmpty(dr["SucursalFacturada"].ToString()))
                {
                    Gestion_Api.Modelo.Sucursal facturada = contSucu.obtenerSucursalID(Convert.ToInt32(dr["SucursalFacturada"]));
                    celCliente.Text = facturada.nombre;
                }
                else
                {
                    celCliente.Text = dr["Cliente"].ToString();
                }
                celCliente.VerticalAlign = VerticalAlign.Middle;
                celCliente.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celCliente);

                phMovimientoStock.Controls.Add(tr);

            }
            catch (Exception ex)
            {
            }
        }

        private void filtrar()
        {
            Response.Redirect("Stock.aspx?a=2&articulo=" + this.idArticulo + "&fd=" + this.txtFechaDesdeMov.Text + "&fh=" + this.txtFechaHastaMov.Text + "&s=" + this.sucursal);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                filtrar();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImpresionMovStock.aspx?a=1&articulo=" + this.idArticulo + "&fd=" + this.txtFechaDesdeMov.Text + "&fh=" + this.txtFechaHastaMov.Text + "&s=" + this.lstSucursal.SelectedValue);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "window.open('/Formularios/Articulos/ImpresionMovStock.aspx?a=1&articulo=" + this.idArticulo + "&s=" + this.lstSucursal.SelectedValue + "&fd=" + this.fechaD + "&fh=" + this.fechaH + "', 'fullscreen', 'top=0,left=0,width='+(screen.availWidth)+',height ='+(screen.availHeight)+',fullscreen=yes,toolbar=0 ,location=0,directories=0,status=0,menubar=0,resiz able=0,scrolling=0,scrollbars=0');", true);
        }


        //private void actualixarStock()
        //{
        //    try
        //    {

        //        StreamWriter sw = new StreamWriter(Server.MapPath("ArchivoStock.txt") );
        //        List<Articulo> articulos = this.controlador.buscarArticuloList("%");
        //        foreach (var a in articulos)
        //        {
        //            decimal stock = this.obtnerStock(a.id.ToString());
        //            //
        //            int suc = Convert.ToInt32(this.lstSucursal.SelectedValue);
        //            string query = a.codigo + " ; " + a.descripcion.Replace('\n',' ') + " ; " + stock;
        //            //string query = "update stock set stock =" + stock + " where producto=" + a.id + " and local=" + suc;
        //            sw.WriteLine(query);
        //        }
        //        sw.Close();
        //    }
        //    catch
        //    {

        //    }
        //}

        private decimal obtnerStock(string art)
        {
            try
            {
                int suc = this.suc;//(int)Session["Login_SucUser"];

                phMovimientoStock.Controls.Clear();
                DateTime ddesde = new DateTime(2016, 1, 1);
                //DataTable dt = this.controlador.obtenerMovimientoStockArticuloCompra(art, Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR")), Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), Convert.ToInt32(this.lstSucursal.SelectedValue));
                //DataTable dt2 = this.controlador.obtenerMovimientoStockArticuloVenta(art, Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR")), Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), Convert.ToInt32(this.lstSucursal.SelectedValue));
                //DataTable dt3 = this.controlador.obtenerMovimientoStockArticulo(art, Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR")), Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), Convert.ToInt32(this.lstSucursal.SelectedValue));
                DataTable dt = this.controladorStock.obtenerMovimientoStockArticuloCompra(art, ddesde, Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), suc);
                DataTable dt2 = this.controladorStock.obtenerMovimientoStockArticuloVenta(art, Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR")), Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), suc);
                DataTable dt3 = this.controladorStock.obtenerMovimientoStockArticulo(art, Convert.ToDateTime(this.fechaD, new CultureInfo("es-AR")), Convert.ToDateTime(this.fechaH, new CultureInfo("es-AR")), suc);
                dt.Merge(dt2);
                dt.Merge(dt3);

                dt.DefaultView.Sort = "Fecha";
                dt = dt.DefaultView.ToTable();

                decimal saldo = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    decimal cantidad = Convert.ToDecimal(dr["Cantidad"]);

                    if (dr["Numero"].ToString().Contains("Credito"))
                    {
                        cantidad = cantidad * -1;
                    }

                    saldo += cantidad;
                }

                return saldo;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [WebMethod]
        public static string ObtenerStock(int id)
        {
            ControladorStock controlador = new ControladorStock();
            var stock = controlador.obtenerStockID(id);

            string JsonStock = stock.id + "|" + stock.sucursal.nombre + "|" + stock.Productos.descripcion + "|" + stock.stock1;

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(JsonStock);
            return resultadoJSON;
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtAgregarStock.Text))
            {
                Tecnocuisine_API.Entitys.Stock st = controladorStock.obtenerStockID(Convert.ToInt32(hiddenidStock.Value));
                Stock_Movimiento s = new Stock_Movimiento();
                txtAgregarStock.Text = txtAgregarStock.Text.Replace(',', '.');

                //Agrego el movimiento de stock                  
                s.IdUsuario = (int)Session["Login_IdUser"];
                s.Cantidad = Convert.ToDecimal(this.txtAgregarStock.Text);
                s.Articulo = st.Productos.id;
                s.IdSucursal = st.sucursal.id;
                s.Fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-AR"));
                s.TipoMovimiento = "Inventario";
                s.Comentarios = this.txtComentarios.Text;

                int j = controladorStock.AgregarMovimientoStock(s);
                if (j > 0)
                {
                    int i = controladorStock.ActualizarStock(Convert.ToInt32(hiddenidStock.Value), Convert.ToDecimal(txtAgregarStock.Text));
                    if (i > 0)
                    {
                        Response.Redirect("Stock.aspx?a=2&articulo=" + this.idArticulo + "&fd=" + this.txtFechaDesdeMov.Text + "&fh=" + this.txtFechaHastaMov.Text + "&s=" + this.sucursal);
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
        }



        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    //this.actualixarStock();
        //    this.obtenerStockHistorico();
        //}

        //private void obtenerStockHistorico()
        //{
        //    try
        //    {
        //        int suc = this.suc;//(int)Session["Login_SucUser"];
        //        StreamWriter sw = new StreamWriter(Server.MapPath("ArchivoStockHistorico.txt"));
        //        //List<Articulo> articulos = this.controlador.buscarArticuloList("%");
        //        List<Articulo> articulos = this.controladorStock.buscarArticuloListReduc("%");
        //        Sucursal sucursal = this.contSucu.obtenerSucursalID(suc);

        //        foreach (var a in articulos)
        //        {
        //            //List<Stock> stocks = this.controlador.obtenerStockArticulo(a.id);// obtengo stocks del art en todas las suc
        //            List<Stock> stocks = this.controladorStock.obtenerStockArticuloReduc(a.id, this.sucursal);// obtengo stocks del art en todas las suc
        //            if (stocks != null && stocks.Count > 0)
        //            {
        //                Stock s = stocks.Where(x => x.sucursal.id == sucursal.id).FirstOrDefault();//filtro y obtengo id stock en esta suc      
        //                if (s != null)
        //                {
        //                    decimal stock = this.obtnerStock(a.id.ToString());// obtengo la cant de stock actual/real con el historico

        //                    //if (stock > 0)
        //                    if (stock != s.cantidad)
        //                    {
        //                        this.controladorStock.ActualizarStock(s.id, stock);//corrigo el stock segun lo que me dio el historico.
        //                        //string query = "update stock set stock =" + stock + " where Id=" + s.id + " and local=" + suc;//query que ejecuto
        //                        //string query = a.codigo + ";" + sucursal.id + ";" + s.cantidad + ";" + stock;
        //                        //sw.WriteLine(query);
        //                    }
        //                    //else
        //                    //{
        //                    //    //this.controlador.ActualizarStock(s.id, stock);//corrigo el stock segun lo que me dio el historico.
        //                    //    string query = "NEGATIVO --update stock set stock =" + stock + " where Id=" + s.id + " and local=" + suc;//query que ejecuto
        //                    //    sw.WriteLine(query);
        //                    //}
        //                }
        //                else
        //                {
        //                    string query = "NO HAY STOCK EN LA SUCURSAL: " + suc + " PARA EL ARTICULO: ID = " + a.id;
        //                    sw.WriteLine(query);
        //                }
        //            }
        //            else
        //            {
        //                string query = "NO HAY STOCK EN LA SUCURSAL: " + suc + " PARA EL ARTICULO: ID = " + a.id;
        //                sw.WriteLine(query);
        //            }

        //        }
        //        sw.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        ////}

    }
}
