using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Maestros;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Productos : Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        Gestion_Api.Controladores.controladorArticulo controladorArticulo = new Gestion_Api.Controladores.controladorArticulo();
        Gestion_Api.Controladores.ControladorArticulosEntity contArtEnt = new Gestion_Api.Controladores.ControladorArticulosEntity();
        int accion;
        int idProducto;
        int Mensaje;




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);


            if (!IsPostBack)
            {


                txtDescripcionAtributo.Attributes.Add("disabled", "disabled");
                txtDescripcionCategoria.Attributes.Add("disabled", "disabled");

                CargarUnidadesMedida();
                CargarAlicuotasIVA();

                cargarNestedListAtributos();
                if (accion == 2)
                {
                    btnPresentacion.Attributes.Add("data-action", "editPresentation");
                    CargarProducto();
                }

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

            }
            ObtenerGruposArticulos();
            ObtenerSubGruposArticulos(Convert.ToInt32(ListGrupo.SelectedValue));
            ObtenerProductos();
            ObtenerPresentaciones();
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
                int valor = 1;
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

        [WebMethod]
        public static string GetPresentaciones(int id)
        {
            ControladorProducto controladorProducto = new ControladorProducto();
            string presentaciones = controladorProducto.obtenerPresentaciones(id);

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(presentaciones);
            return resultadoJSON;
        }


        [WebMethod]
        public static string GetSubgrupos(int id)
        {
            Gestion_Api.Controladores.controladorArticulo controladorProducto = new Gestion_Api.Controladores.controladorArticulo();
            var gruposList = controladorProducto.obtenerSubGrupoByGrupo(id);
            string grupos = "";
            foreach (var item in gruposList)
            {
                grupos += item.id + "," + item.descripcion + ";";
            }

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(grupos);
            return resultadoJSON;
        }

        [WebMethod]
        public static string GetDescripcionProducto(int id)
        {
            ControladorProducto controladorProducto = new ControladorProducto();
            string producto = controladorProducto.ObtenerProductoId(id).descripcion;

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(producto);
            return resultadoJSON;
        }

        public void ObtenerPresentaciones()
        {
            try
            {
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                var presentaciones = controladorPresentacion.ObtenerTodosPresentaciones();

                if (presentaciones.Count > 0)
                {

                    foreach (var item in presentaciones)
                    {
                        CargarPresentacionesPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void ObtenerGruposArticulos()
        {
            try
            {

                DataTable dt = controladorArticulo.obtenerGruposArticulos();

                //agrego todos
                DataRow dr = dt.NewRow();
                dr["descripcion"] = "Seleccione...";
                dr["id"] = -1;
                dt.Rows.InsertAt(dr, 0);

                this.ListGrupo.DataSource = dt;
                this.ListGrupo.DataValueField = "id";
                this.ListGrupo.DataTextField = "descripcion";

                this.ListGrupo.DataBind();


            }
            catch (Exception ex)
            {
            }
        }

        private void ObtenerSubGruposArticulos(int grupo)
        {
            try
            {
                DataTable dt = controladorArticulo.obtenerSubGruposArticulos(grupo);

                //agrego todos
                DataRow dr = dt.NewRow();
                dr["descripcion"] = "Seleccione...";
                dr["id"] = -1;
                dt.Rows.InsertAt(dr, 0);

                this.ListSubgrupo.DataSource = dt;
                this.ListSubgrupo.DataValueField = "id";
                this.ListSubgrupo.DataTextField = "descripcion";

                this.ListSubgrupo.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        public void CargarPresentacionesPH(Tecnocuisine_API.Entitys.Presentaciones presentacion)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "presentacion_" + presentacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = presentacion.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = presentacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = presentacion.cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Right;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCantidad);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                cbxAgregar.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                cbxAgregar.Attributes.Add("type", "checkbox");
                cbxAgregar.Attributes.Add("value", "1");
                cbxAgregar.ID = "btnSelecPres_" + presentacion.id + " - " + presentacion.descripcion;
                celAccion.Controls.Add(cbxAgregar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phPresentaciones.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }



        private void CargarAlicuotasIVA()
        {
            try
            {
                ControladorIVA controladorIVA = new ControladorIVA();
                this.ListAlicuota.DataSource = controladorIVA.ObtenerTodosAlicuotas_IVA();
                this.ListAlicuota.DataValueField = "id";
                this.ListAlicuota.DataTextField = "porcentaje";
                this.ListAlicuota.DataBind();
                ListAlicuota.Items.Insert(0, new ListItem("Seleccione", "-1"));

            }
            catch (Exception ex)
            {

            }
        }



        private void CargarUnidadesMedida()
        {
            try
            {

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                this.ListUnidadMedida.DataSource = controladorUnidad.ObtenerTodosUnidades();
                this.ListUnidadMedida.DataValueField = "id";
                this.ListUnidadMedida.DataTextField = "descripcion";
                this.ListUnidadMedida.DataBind();
                ListUnidadMedida.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        public void ObtenerProductos()
        {
            try
            {
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

        public void CargarProducto()
        {
            try
            {
                var producto = controladorProducto.ObtenerProductoId(this.idProducto);
                if (producto != null)
                {
                    hiddenEditar.Value = producto.id.ToString();
                    txtDescripcionProducto.Text = producto.descripcion;
                    txtCosto.Text = producto.costo.ToString();
                    idCategoria.Value = producto.Categorias.id.ToString();
                    string descripcionAtributos = "";
                    if (producto.Productos_Atributo != null)
                    {
                        ControladorAtributo controladorAtributo = new ControladorAtributo();
                        foreach (Productos_Atributo item in producto.Productos_Atributo)
                        {

                            Tecnocuisine_API.Entitys.Atributos atributo = controladorAtributo.ObtenerAtributoById(item.atributo);
                            if (descripcionAtributos == "")
                            {
                                descripcionAtributos = atributo.id + " - " + atributo.descripcion;
                                idAtributo.Value = atributo.id.ToString();
                            }

                            else
                            {
                                descripcionAtributos += " , " + atributo.id + " - " + atributo.descripcion;
                                idAtributo.Value += "," + atributo.id.ToString();
                            }
                        }
                    }
                    string descripcionPresentaciones = "";
                    if (producto.Productos_Presentacion != null)
                    {
                        ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                        foreach (Productos_Presentacion item in producto.Productos_Presentacion)
                        {

                            Tecnocuisine_API.Entitys.Presentaciones presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                            if (descripcionPresentaciones == "")
                            {
                                descripcionPresentaciones = presentacion.id + " - " + presentacion.descripcion;
                                idPresentacion.Value = presentacion.id.ToString();
                            }

                            else
                            {
                                descripcionPresentaciones += " , " + presentacion.id + " - " + presentacion.descripcion;
                                idPresentacion.Value += "," + presentacion.id.ToString();
                            }
                        }
                    }

                    txtDescripcionPresentacion.Text = descripcionPresentaciones;
                    txtDescripcionAtributo.Text = descripcionAtributos;
                    txtDescripcionCategoria.Text = producto.Categorias.id + " - " + producto.Categorias.descripcion;
                    ListAlicuota.SelectedValue = producto.alicuota.ToString();
                    ListUnidadMedida.SelectedValue = producto.unidadMedida.ToString();
                    btnAtributos.Attributes.Remove("disabled");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "cargarCbx();", true);

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
                tr.ID = producto.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = producto.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Width = Unit.Percentage(10);
                celDescripcion.Text = producto.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCategoria = new TableCell();
                celCategoria.Text = producto.Categorias.descripcion;
                celCategoria.VerticalAlign = VerticalAlign.Middle;
                celCategoria.HorizontalAlign = HorizontalAlign.Left;
                celCategoria.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCategoria);

                ControladorAtributo controladorAtributo = new ControladorAtributo();
                string descripcionAtributos = "";
                if (producto.Productos_Atributo != null)
                {
                    foreach (Productos_Atributo item in producto.Productos_Atributo)
                    {
                        Tecnocuisine_API.Entitys.Atributos atributo = controladorAtributo.ObtenerAtributoById(item.atributo);
                        if (descripcionAtributos == "")
                            descripcionAtributos = atributo.descripcion;
                        else
                            descripcionAtributos += " - " + atributo.descripcion;
                    }
                }
                if (descripcionAtributos == null || descripcionAtributos.Trim() == "")
                {
                    TableCell celAtributos2 = new TableCell();
                    celAtributos2.Text = "No Existe Atributos";
                    celAtributos2.Width = Unit.Percentage(25);
                    celAtributos2.VerticalAlign = VerticalAlign.Middle;
                    celAtributos2.HorizontalAlign = HorizontalAlign.Left;
                    celAtributos2.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celAtributos2);
                }
                else
                {
                    TableCell celAtributos = new TableCell();
                    celAtributos.Text = descripcionAtributos;
                    celAtributos.Width = Unit.Percentage(25);
                    celAtributos.VerticalAlign = VerticalAlign.Middle;
                    celAtributos.HorizontalAlign = HorizontalAlign.Left;
                    celAtributos.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celAtributos);
                }

                TableCell celUnidad = new TableCell();
                celUnidad.Text = producto.Unidades.descripcion;
                celUnidad.VerticalAlign = VerticalAlign.Middle;
                celUnidad.HorizontalAlign = HorizontalAlign.Left;
                celUnidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUnidad);

                TableCell celCosto = new TableCell();
                celCosto.Text = FormatearNumero(producto.costo);
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Right;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);


                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                string descripcionPresentacion = "";
                if (producto.Productos_Presentacion != null)
                {
                    foreach (Productos_Presentacion item in producto.Productos_Presentacion)
                    {
                        Tecnocuisine_API.Entitys.Presentaciones presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                        if (presentacion != null)
                        {
                            if (descripcionPresentacion == "")
                            {
                                descripcionPresentacion = presentacion.descripcion;
                            }
                            else
                            {

                                descripcionPresentacion += " - " + presentacion.descripcion;
                            }
                        }
                    }
                }
                TableCell celPresentacion = new TableCell();
                celPresentacion.Text = descripcionPresentacion.Length > 1 ? descripcionPresentacion : "No Existen Presentaciones"; 
                celPresentacion.Width = Unit.Percentage(12);
                celPresentacion.VerticalAlign = VerticalAlign.Middle;
                celPresentacion.HorizontalAlign = HorizontalAlign.Left;
                celPresentacion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPresentacion);

                ControladorMarca controladorMarca = new ControladorMarca();
                string descripcionMarca = "";
                if (producto.Marca_Productos != null)
                {
                    foreach (Marca_Productos item in producto.Marca_Productos)
                    {
                        Tecnocuisine_API.Entitys.Articulos_Marcas marca = controladorMarca.ObtenerMarcaId((int)item.id_marca);
                        if (marca != null)
                        {
                            if (descripcionMarca == "")
                            {
                                descripcionMarca = marca.descripcion;
                            }
                            else
                            {

                                descripcionMarca += " - " + marca.descripcion;
                            }
                        }
                    }
                }

                TableCell celMarcas = new TableCell();
                celMarcas.Text = descripcionMarca.Length > 1 ? descripcionMarca : "No Existe Marcas" ;
                celMarcas.Width = Unit.Percentage(12);
                celMarcas.VerticalAlign = VerticalAlign.Middle;
                celMarcas.HorizontalAlign = HorizontalAlign.Left;
                celMarcas.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celMarcas);


                TableCell celAlicuota = new TableCell();
                celAlicuota.Text = producto.Alicuotas_IVA.porcentaje.ToString().Replace(',', '.') + "%";
                celAlicuota.VerticalAlign = VerticalAlign.Middle;
                celAlicuota.HorizontalAlign = HorizontalAlign.Left;
                celAlicuota.Attributes.Add("style", "padding-bottom: 1px !important;text-align: right;");
                tr.Cells.Add(celAlicuota);





                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(9);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + producto.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("data-placement", "top");
                btnDetalles.Attributes.Add(" data-original-title", "Editar Producto");
                btnDetalles.Click += new EventHandler(this.editarProducto);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnStock = new LinkButton();
                btnStock.ID = "btnStock_" + producto.id;
                btnStock.CssClass = "btn btn-xs";
                btnStock.Style.Add("background-color", "transparent");
                btnStock.Attributes.Add("data-toggle", "tooltip");
                btnStock.Attributes.Add("data-placement", "top");
                btnStock.Attributes.Add(" data-original-title", "Visualizar Stock");
                btnStock.Text = "<span><i style='color:black' class='fa fa-list-alt'></i></span>";
                btnStock.PostBackUrl = "StockDetallado.aspx?t=1&i="+producto.id;
                celAccion.Controls.Add(btnStock);

                Literal l3 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l3);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + producto.id;
                btnEliminar.CssClass = "btn  btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=\"Eliminar Producto\"><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.Style.Add("background-color", "transparent");

                btnEliminar.OnClientClick = "abrirdialog(" + producto.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected void editarProducto(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("ProductosABM.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.hiddenEditar.Value != "")
                {
                    EditarProducto();
                }
                else
                {
                    GuardarProducto();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void LimpiarCampos()
        {
            try
            {
                txtDescripcionProducto.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarProducto()
        {
            try
            {
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                producto.descripcion = txtDescripcionProducto.Text;
                producto.categoria = Convert.ToInt32(idCategoria.Value.Trim());
                producto.costo = Convert.ToDecimal(txtCosto.Text);
                producto.unidadMedida = Convert.ToInt32(ListUnidadMedida.SelectedValue);
                producto.alicuota = Convert.ToInt32(ListAlicuota.SelectedValue);
                producto.estado = 1;
                //producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.AgregarProducto(producto);

                if (resultado > 0)
                {
                    if (cbxGestion.Checked)
                    {
                        GuardarArticuloGestion(producto);
                    }
                    else
                    {
                        controladorStock.AgregarProductoStock(resultado);

                    }

                    string[] atributos = idAtributo.Value.Split(',');


                    foreach (string s in atributos)
                    {
                        if (s != "")
                        {
                            Productos_Atributo atributo = new Productos_Atributo();
                            atributo.producto = resultado;
                            atributo.atributo = Convert.ToInt32(s.Split('-')[0]);
                            controladorProducto.AgregarProductoAtributo(atributo);
                        }

                    }


                    string[] presentaciones = idPresentacion.Value.Split(',');


                    foreach (string s in presentaciones)
                    {
                        if (s != "")
                        {
                            Productos_Presentacion presentacion = new Productos_Presentacion();
                            presentacion.idProducto = resultado;
                            presentacion.idPresentacion = Convert.ToInt32(s.Split('-')[0]);
                            controladorProducto.AgregarProductoPresentacion(presentacion);
                        }

                    }
                    Response.Redirect("Productos.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el producto", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void GuardarArticuloGestion(Tecnocuisine_API.Entitys.Productos producto)
        {
            Articulo art = new Articulo();
            art.codigo = producto.id.ToString();
            art.descripcion = producto.descripcion;
            art.proveedor.id = Convert.ToInt32(2);
            art.grupo.id = Convert.ToInt32(hiddenGrupo.Value);
            art.subGrupo.id = Convert.ToInt32(hiddenSubGrupo.Value);
            art.costo = producto.costo;
            art.margen = 0;
            art.impInternos = 0;
            art.ingBrutos = 0;
            art.precioVenta = Convert.ToDecimal(producto.costo);
            art.monedaVenta.id = 1;
            art.stockMinimo = 0;
            art.apareceLista = 1;
            art.ubicacion = "";
            art.fechaAlta = DateTime.Now;
            art.ultActualizacion = DateTime.Now;
            art.modificado = DateTime.Now;
            art.procedencia.id = 1;
            art.porcentajeIva = 21;
            art.codigoBarra = art.codigo;
            art.incidencia = 0;
            art.costoImponible = 0;
            art.costoReal = producto.costo;
            art.precioSinIva = Convert.ToDecimal(producto.costo, CultureInfo.InvariantCulture);
            art.listaCategoria.id = Convert.ToInt32(1);
            art.observacion = "";

            art.alerta = new AlertaArticulo();
            art.alerta.descripcion = "";

            int i = controladorArticulo.agregarArticulo(art);
            if (i > 0)
            {
                //si logre dar de alta el articulo intento guardar los datos de despacho y los datos de presentaciones 
                // i = idArticulo nuevo

                Articulos_Catalogo artCat = new Articulos_Catalogo();
                artCat.Articulo = i;
                artCat.ApareceLista = 1;

                int a = contArtEnt.agregarApareceLista(artCat);
            }
            //traspaso temporal para el siguiente
            //int idart=0;

            if (i > 0)
            {
                //agrego la marca
                Articulos_Marca am = new Articulos_Marca();
                am.idArticulo = i;
                am.idMarca = Convert.ToInt64(1);
                am.TipoDistribucion = Convert.ToInt32(1);
                //this.controlador.agregarArticuloMarca(i, Convert.ToInt32(this.DropListMarca.SelectedValue));
                i = this.contArtEnt.agregarMarca(am);
                i = 1;
            }

        }

        public void EditarProducto()
        {
            try
            {
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                producto.id = idProducto;
                producto.descripcion = txtDescripcionProducto.Text;
                producto.categoria = Convert.ToInt32(idCategoria.Value.Trim());
                producto.costo = Convert.ToDecimal(txtCosto.Text);
                producto.unidadMedida = Convert.ToInt32(ListUnidadMedida.SelectedValue);
                producto.alicuota = Convert.ToInt32(ListAlicuota.SelectedValue);
                producto.estado = 1;
                //producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.EditarProducto(producto);

                if (resultado > 0)
                {

                    string[] atributos = idAtributo.Value.Split(',');
                    controladorProducto.EliminarProductoAtributo(producto.id);

                    foreach (string s in atributos)
                    {
                        if (s != "")
                        {
                            Productos_Atributo atributo = new Productos_Atributo();
                            atributo.producto = producto.id;
                            atributo.atributo = Convert.ToInt32(s.Split('-')[0]);
                            controladorProducto.AgregarProductoAtributo(atributo);
                        }

                    }


                    string[] presentaciones = idPresentacion.Value.Split(',');
                    controladorProducto.EliminarProductoPresentacion(producto.id);

                    foreach (string s in presentaciones)
                    {
                        if (s != "")
                        {
                            Productos_Presentacion atributo = new Productos_Presentacion();
                            atributo.idProducto = producto.id;
                            atributo.idPresentacion = Convert.ToInt32(s.Split('-')[0]);
                            controladorProducto.AgregarProductoPresentacion(atributo);
                        }

                    }
                    Response.Redirect("Productos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la producto", "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(this.hiddenID.Value);
                if (controladorProducto.verificarRelacionRecetaProducto(idProducto))
                {
                    int resultado = controladorProducto.EliminarProducto(idProducto);

                    if (resultado > 0)
                    {
                        Response.Redirect("Productos.aspx?m=3");
                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "No se pudo eliminar el producto", "Alerta", "warning");
                    }
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el producto porque tiene una receta relacionada", "Alerta", "warning");
                }
            }

            catch (Exception ex)
            {

            }
        }
        #endregion


        private void cargarNestedListAtributos()
        {
            ControladorInsumo controladorInsumo = new ControladorInsumo();
            //List<Insumos> insumos = c.Obtener(controladorCategoria.ObtenerTopPadre(Convert.ToInt32(idCategoria.Value)));
            List<Insumos> insumos = controladorInsumo.ObtenerTodosInsumos();
            foreach (Tecnocuisine_API.Entitys.Insumos item in insumos)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.id_insumo.ToString());
                li.Attributes.Add("runat", "server");

                main.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle not-draggable editable");
                div.InnerText = item.Descripcion;



                li.Controls.Add(div);



                cargarNestedListAtributos(item.id_insumo, li);

            }
        }

        private void cargarNestedListAtributos(int idTipoAtributo, HtmlGenericControl liPadre)
        {
            ControladorAtributo controlador = new ControladorAtributo();
            List<Tecnocuisine_API.Entitys.Atributos> atributos = controlador.obtenerAtributosPrimerNivel(idTipoAtributo);
            if (atributos != null)
            {
                HtmlGenericControl ol = new HtmlGenericControl("ol");
                ol.Attributes.Add("class", "dd-list");

                liPadre.Controls.Add(ol);
                foreach (Tecnocuisine_API.Entitys.Atributos item in atributos)
                {
                    HtmlGenericControl liHijo = new HtmlGenericControl("li");

                    liHijo.Attributes.Add("class", "dd-item");
                    liHijo.Attributes.Add("data-id", item.id.ToString());
                    liHijo.Attributes.Add("runat", "server");

                    ol.Controls.Add(liHijo);

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "dd-handle not-draggable editable");
                    div.InnerText = item.descripcion;

                    HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                    cbxAgregar.Attributes.Add("class", "atributos radio btn btn-primary btn-xs pull-right");
                    cbxAgregar.Attributes.Add("type", "checkbox");
                    cbxAgregar.Attributes.Add("value", "1");
                    cbxAgregar.Attributes.Add("name", "fooby[" + idTipoAtributo + "][]");
                    cbxAgregar.Attributes.Add("data-action", "selectcbxAttribute");
                    cbxAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    div.Controls.Add(cbxAgregar);



                    liHijo.Controls.Add(div);




                    //HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    //btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    //btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    //btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    //btnAgregar.Attributes.Add("data-action", "addAttribute");
                    //div.Controls.Add(btnAgregar);

                    cargarNestedListAtributosHijos(item.id, liHijo, idTipoAtributo);
                }
            }
        }

        private void cargarNestedListAtributosHijos(int id, HtmlGenericControl li, int checkboxIndex)
        {
            try
            {
                ControladorAtributo controlador = new ControladorAtributo();
                List<Tecnocuisine_API.Entitys.Atributos> atributos = controlador.obtenerAtributosHijas(id);
                if (atributos.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Atributos item in atributos)
                    {
                        HtmlGenericControl liHijo = new HtmlGenericControl("li");

                        liHijo.Attributes.Add("class", "dd-item");
                        liHijo.Attributes.Add("data-id", item.id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle not-draggable editable");
                        div.InnerText = item.descripcion;

                        HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                        cbxAgregar.Attributes.Add("class", "atributos radio btn btn-primary btn-xs pull-right");
                        cbxAgregar.Attributes.Add("type", "checkbox");
                        cbxAgregar.Attributes.Add("value", "1");
                        cbxAgregar.Attributes.Add("name", "fooby[" + checkboxIndex + "][]");
                        cbxAgregar.Attributes.Add("data-action", "selectcbxAttribute");
                        cbxAgregar.ID = "btnSelec_" + item.id;
                        div.Controls.Add(cbxAgregar);


                        liHijo.Controls.Add(div);



                        //HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        //btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        //btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        //btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        //btnAgregar.Attributes.Add("data-action", "addAttribute");
                        //div.Controls.Add(btnAgregar);



                        cargarNestedListAtributosHijos(item.id, liHijo, checkboxIndex);
                    }
                }

            }

            catch (Exception)
            {

                throw;
            }
        }
        private void cargarNestedListCategorias()
        {
            ControladorCategoria controlador = new ControladorCategoria();
            List<Tecnocuisine_API.Entitys.Categorias> categorias = controlador.obtenerCategoriasPrimerNivel();
            foreach (Tecnocuisine_API.Entitys.Categorias item in categorias)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.id.ToString());
                li.Attributes.Add("runat", "server");

                olCategorias.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle not-draggable editable");
                div.InnerText = item.descripcion;

                li.Controls.Add(div);

                if (controlador.VerificarUltimoNivel(item.id))
                {
                    HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    btnAgregar.Attributes.Add("data-action", "addCategory");
                    div.Controls.Add(btnAgregar);

                }



                cargarNestedListCategoriasHijas(item.id, li);
            }
        }

        private void cargarNestedListCategoriasHijas(int id, HtmlGenericControl li)
        {
            try
            {
                ControladorCategoria controlador = new ControladorCategoria();
                List<Tecnocuisine_API.Entitys.Categorias> categorias = controlador.obtenerCategoriasHijas(id);
                if (categorias.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Categorias item in categorias)
                    {
                        HtmlGenericControl liHijo = new HtmlGenericControl("li");

                        liHijo.Attributes.Add("class", "dd-item");
                        liHijo.Attributes.Add("data-id", item.id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle not-draggable editable");
                        div.InnerText = item.descripcion;


                        liHijo.Controls.Add(div);


                        if (controlador.VerificarUltimoNivel(item.id))
                        {
                            HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                            btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                            //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                            //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                            btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                            btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                            btnAgregar.Attributes.Add("data-action", "addCategory");
                            div.Controls.Add(btnAgregar);
                        }


                        cargarNestedListCategoriasHijas(item.id, liHijo);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        protected void btnAtributos_Click(object sender, EventArgs e)
        {


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