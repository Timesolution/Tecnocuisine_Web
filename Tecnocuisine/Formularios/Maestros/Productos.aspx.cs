using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        protected void Page_Init()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
  

            if (!IsPostBack)
            {
                txtDescripcionAtributo.Attributes.Add("disabled", "disabled");
                txtDescripcionCategoria.Attributes.Add("disabled", "disabled");

                CargarUnidadesMedida();
                CargarAlicuotasIVA();
                CargarPresentaciones();
                cargarNestedListTipoAtributos();
                cargarNestedListCategorias();
                if (accion == 2)
                {
                    CargarProducto();
                }

                if(Mensaje == 1)
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
        private void CargarPresentaciones()
        {
            try
            {
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                this.ListPresentaciones.DataSource = controladorPresentacion.ObtenerTodosPresentaciones();
                this.ListPresentaciones.DataValueField = "id";
                this.ListPresentaciones.DataTextField = "descripcion";
                this.ListPresentaciones.DataBind();
                ListPresentaciones.Items.Insert(0, new ListItem("Seleccione", "-1"));
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
                    txtDescripcionProducto.Text = producto.descripcion;
                    txtCosto.Text = producto.costo.ToString();
                    idCategoria.Value = producto.Categorias.id.ToString();
                    idAtributo.Value = producto.Atributos.id.ToString();
                    txtDescripcionAtributo.Text = producto.Atributos.id + " - " + producto.Atributos.descripcion;
                    txtDescripcionCategoria.Text = producto.Categorias.id + " - " + producto.Categorias.descripcion;
                    ListAlicuota.SelectedValue = producto.alicuota.ToString();
                    ListPresentaciones.SelectedValue = producto.presentacion.ToString();
                    ListUnidadMedida.SelectedValue = producto.unidadMedida.ToString();
                    

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

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

                TableCell celAtributo = new TableCell();
                celAtributo.Text = producto.Atributos.descripcion;
                celAtributo.VerticalAlign = VerticalAlign.Middle;
                celAtributo.HorizontalAlign = HorizontalAlign.Left;
                celAtributo.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAtributo);

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

                TableCell celPresentacion = new TableCell();
                celPresentacion.Text = producto.Presentaciones.descripcion;
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

                celAccion.Width = Unit.Percentage(25);
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
                if (this.accion == 2)
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
                producto.atributo = Convert.ToInt32(idAtributo.Value.Trim());
                producto.categoria = Convert.ToInt32( idCategoria.Value.Trim());
                producto.costo = Convert.ToDecimal(txtCosto.Text);
                producto.unidadMedida = Convert.ToInt32(ListUnidadMedida.SelectedValue);
                producto.alicuota = Convert.ToInt32(ListAlicuota.SelectedValue);
                producto.estado = 1;
                producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.AgregarProducto(producto);

                if (resultado > 0)
                {
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
                producto.atributo = Convert.ToInt32(idAtributo.Value.Trim());
                producto.categoria = Convert.ToInt32(idCategoria.Value.Trim());
                producto.costo = Convert.ToDecimal(txtCosto.Text);
                producto.unidadMedida = Convert.ToInt32(ListUnidadMedida.SelectedValue);
                producto.alicuota = Convert.ToInt32(ListAlicuota.SelectedValue);
                producto.estado = 1;
                producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.EditarProducto(producto);

                if (resultado > 0)
                {
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
                int resultado = controladorProducto.EliminarProducto(idProducto);

                if (resultado > 0)
                {
                    Response.Redirect("Productos.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el producto", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion



        private void cargarNestedListTipoAtributos()
        {
            ControladorInsumo controladorInsumo = new ControladorInsumo();
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
                div.InnerText = item.id_insumo + " - " + item.Descripcion;

               

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
                    div.InnerText = item.id + " - " + item.descripcion;



                    liHijo.Controls.Add(div);

                   


                    HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    btnAgregar.Attributes.Add("data-action", "addAttribute");
                    div.Controls.Add(btnAgregar);

                    cargarNestedListAtributosHijos(item.id, liHijo);
                }
            }
        }

        private void cargarNestedListAtributosHijos(int id, HtmlGenericControl li)
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
                        div.InnerText = item.id + " - " + item.descripcion;



                        liHijo.Controls.Add(div);


                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        btnAgregar.Attributes.Add("data-action", "addAttribute");
                        div.Controls.Add(btnAgregar);



                        cargarNestedListAtributosHijos(item.id, liHijo);
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
                div.InnerText = item.id + " - " + item.descripcion;

                li.Controls.Add(div);

                HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnAgregar.ID = "btnSelec_"  + item.id + " - " + item.descripcion;
                btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                btnAgregar.Attributes.Add("data-action", "addCategory");
                div.Controls.Add(btnAgregar);


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
                        div.InnerText = item.id + " - " + item.descripcion;


                        liHijo.Controls.Add(div);



                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        btnAgregar.Attributes.Add("data-action", "addCategory");
                        div.Controls.Add(btnAgregar);


                        cargarNestedListCategoriasHijas(item.id, liHijo);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}