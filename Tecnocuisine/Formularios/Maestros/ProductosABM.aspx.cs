using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class ProductosABM : System.Web.UI.Page
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
            //VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
                txtDescripcionAtributo.Attributes.Add("disabled", "disabled");
                txtDescripcionCategoria.Attributes.Add("disabled", "disabled");

                CargarUnidadesMedida();
                CargarAlicuotasIVA();
                
                //cargarNestedListCategorias();
                //cargarNestedListAtributos();
                

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
            //ObtenerSubGruposArticulos(Convert.ToInt32(ListGrupo.SelectedValue));
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

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.hiddenEditar.Value != "")
        //        {
        //            EditarProducto();
        //        }
        //        else
        //        {
        //            GuardarProducto();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void EditarProducto()
        {
            try
            {
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                producto.id = idProducto;
                //producto.descripcion = txtDescripcionProducto.Text;
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
        public void GuardarProducto()
        {
            try
            {
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                //producto.descripcion = txtDescripcionProducto.Text;
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
                        //GuardarArticuloGestion(producto);
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




                    HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    btnAgregar.Attributes.Add("data-action", "addAttribute");
                    div.Controls.Add(btnAgregar);

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



                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        btnAgregar.Attributes.Add("data-action", "addAttribute");
                        div.Controls.Add(btnAgregar);



                        cargarNestedListAtributosHijos(item.id, liHijo, checkboxIndex);
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