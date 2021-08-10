using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Productos : Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();

        int accion;
        int idProducto;
        int Mensaje;




        protected void Page_Load(object sender, EventArgs e)
        {

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);


            if (!IsPostBack)
            {


                txtDescripcionAtributo.Attributes.Add("disabled", "disabled");
                txtDescripcionCategoria.Attributes.Add("disabled", "disabled");

                CargarUnidadesMedida();
                CargarAlicuotasIVA();
                cargarNestedListCategorias();
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

            ObtenerProductos();
            ObtenerPresentaciones();
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
                TableCell celAtributos = new TableCell();
                celAtributos.Text = descripcionAtributos;
                celAtributos.Width = Unit.Percentage(25);
                celAtributos.VerticalAlign = VerticalAlign.Middle;
                celAtributos.HorizontalAlign = HorizontalAlign.Left;
                celAtributos.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAtributos);

                TableCell celUnidad = new TableCell();
                celUnidad.Text = producto.Unidades.descripcion;
                celUnidad.VerticalAlign = VerticalAlign.Middle;
                celUnidad.HorizontalAlign = HorizontalAlign.Left;
                celUnidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUnidad);

                TableCell celCosto = new TableCell();
                celCosto.Text = producto.costo.ToString(); ;
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Right;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCosto);

                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                string descripcionPresentacion = "";
                if (producto.Productos_Presentacion != null)
                {
                    foreach (Productos_Presentacion item in producto.Productos_Presentacion)
                    {
                        Tecnocuisine_API.Entitys.Presentaciones presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                        if (descripcionPresentacion == "")
                            descripcionPresentacion = presentacion.descripcion;
                        else
                            descripcionPresentacion += " - " + presentacion.descripcion;
                    }
                }
                TableCell celPresentacion = new TableCell();
                celPresentacion.Text = descripcionPresentacion;
                celPresentacion.Width = Unit.Percentage(12);
                celPresentacion.VerticalAlign = VerticalAlign.Middle;
                celPresentacion.HorizontalAlign = HorizontalAlign.Left;
                celPresentacion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPresentacion);

                TableCell celAlicuota = new TableCell();
                celAlicuota.Text = producto.Alicuotas_IVA.porcentaje.ToString() + "%";
                celAlicuota.VerticalAlign = VerticalAlign.Middle;
                celAlicuota.HorizontalAlign = HorizontalAlign.Left;
                celAlicuota.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAlicuota);




                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(9);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary btn-xs";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + producto.id + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarProducto);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + producto.id;
                btnEliminar.CssClass = "btn btn-danger btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + producto.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarProducto(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Productos.aspx?a=2&i=" + id[1]);
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
                div.Attributes.Add("class", "dd-handle editable");
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
                    div.Attributes.Add("class", "dd-handle editable");
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
                        div.Attributes.Add("class", "dd-handle editable");
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
                div.Attributes.Add("class", "dd-handle editable");
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
                        div.Attributes.Add("class", "dd-handle editable");
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
    }
}