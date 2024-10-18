using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class Recetas : Page
    {
        Mensaje m = new Mensaje();
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorAtributo controladorAtributo = new ControladorAtributo();

        int accion;
        int idReceta;
        int Mensaje;


        protected void Page_Init()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idReceta = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {


                //cargarNestedListCategorias();
                //cargarNestedListAtributos();
                if (accion == 2)
                {
                    CargarReceta();
                }
                if(accion == 3)
                {
                    CargarAtributosReceta();
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

            ObtenerRecetas();
            //ObtenerProductos();
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

        private void CargarAtributosReceta()
        {
            try
            {
                var Receta = controladorReceta.ObtenerRecetaId(this.idReceta);
                if (Receta != null)
                {
                    idCategoria.Value = Receta.Categorias.id.ToString();
                    txtDescripcionCategoria.Text = Receta.Categorias.id + " - " + Receta.Categorias.descripcion;
                    string descripcionAtributos = "";
                    if (Receta.Recetas_Atributo != null)
                    {
                        ControladorAtributo controladorAtributo = new ControladorAtributo();
                        foreach (Recetas_Atributo item in Receta.Recetas_Atributo)
                        {

                            Tecnocuisine_API.Entitys.Atributos atributo = controladorAtributo.ObtenerAtributoById((int)item.idAtributo);
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
                    txtDescripcionAtributo.Text = descripcionAtributos;
                    btnAtributos.Attributes.Remove("disabled");
                    hiddenidReceta.Value = this.idReceta.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "cargarCbx();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "openModalAtributos();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ObtenerProductos()
            {
                try
                {
                ControladorProducto controladorProducto = new ControladorProducto();
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
        

        public void CargarProductosPH(Tecnocuisine_API.Entitys.Productos producto)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "Productos_" + producto.id.ToString();

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




                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary btn-xs";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "agregarProducto(this.id); return false;");
                btnDetalles.ID = "btnSelecProd_" + producto.id + "_" + producto.descripcion;
                btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phProductosAgregar.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }


        public void ObtenerRecetas()
        {
            try
            {
                var Recetas = controladorReceta.ObtenerTodosRecetas();

                if (Recetas.Count > 0)
                {

                    foreach (var item in Recetas)
                    {
                        CargarRecetasPHPrincipal(item);
                        //CargarRecetasPHModal(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string GetIngredientes(int id)
        {
            try
            {
                ControladorReceta controladorReceta = new ControladorReceta();
                string ingredientes = controladorReceta.obtenerIngredientes(id);
                string[] ingrediente = ingredientes.Split(';');
                string ingredienteReturn = "";
                foreach (string s in ingrediente)
                {
                    if (s != "")
                    {
                        int idIngrediente = Convert.ToInt32(s.Split(',')[0]);
                        string tipo = s.Split(',')[1];


                        if (tipo == "Producto")
                        {
                            ControladorProducto controladorProducto = new ControladorProducto();
                            ingredienteReturn += s + "," + controladorProducto.ObtenerProductoId(idIngrediente).descripcion + ";";
                        }
                        else
                        {
                            ingredienteReturn += s + "," + controladorReceta.ObtenerRecetaId(idIngrediente).descripcion + ";";
                        }
                    }
                }
                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(ingredienteReturn);
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        [WebMethod]
        public static string GetDescripcionReceta(int id)
        {
            ControladorReceta controladorReceta = new ControladorReceta();
            string receta = controladorReceta.ObtenerRecetaId(id).descripcion;

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(receta);
            return resultadoJSON;
        }



        public void CargarReceta()
        {
            try
            {
                var Receta = controladorReceta.ObtenerRecetaId(this.idReceta);
                if (Receta != null)
                {
                    hiddenReceta.Value = idReceta.ToString();
                    txtDescripcionReceta.Text = Receta.descripcion;
                    txtMerma.Text = Receta.merma.ToString();
                    if(txtMerma.Text == "")
                    {
                        txtMerma.Attributes.Add("disabled", "disabled");
                        txtDesperdicio.Attributes.Add("disabled", "disabled");
                    }
                    txtCoeficiente.Text = Receta.coeficiente.ToString();
                    hiddenCoeficiente.Value = Receta.coeficiente.ToString();
                    txtDesperdicio.Text = Receta.desperdicio.ToString();
                    txtRinde.Text = Receta.rinde.ToString();


                    if (txtRinde.Text == "")
                    {
                        txtRinde.Attributes.Add("disabled", "disabled");
                    }


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cargarProductos();", true);


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarRecetasPHPrincipal(Tecnocuisine_API.Entitys.Recetas Receta)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "phRecetas_"+Receta.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Receta.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important; display:none;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = Receta.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);
                
                TableCell celAccion = new TableCell();

                //agrego fila a tabla
                LinkButton btnInfo = new LinkButton();
                btnInfo.CssClass = "btn btn-xs";
                //btnInfo.Attributes.Add("data-toggle", "tooltip");
                //btnInfo.Attributes.Add("title data-original-title", "Editar");
                btnInfo.ID = "btnInfoReceta_" + Receta.id + "_";
                btnInfo.Text = "<span><i style='color:black' class='fa fa-search'></i></span>";
                btnInfo.Click += new EventHandler(this.infoReceta);
                btnInfo.Attributes.Add("title", "Ver");
                celAccion.Controls.Add(btnInfo);

                Literal l3 = new Literal();
                l3.Text = "&nbsp";
                celAccion.Controls.Add(l3);

                //agrego fila a tabla
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelecReceta_" + Receta.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarReceta);
                btnDetalles.Attributes.Add("title", "Editar");
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminarReceta_" + Receta.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "tooltip");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + Receta.id + ");";
                btnEliminar.Attributes.Add("title", "Eliminar");
                celAccion.Controls.Add(btnEliminar);

                Literal l4 = new Literal();
                l4.Text = "&nbsp";
                celAccion.Controls.Add(l4);

                LinkButton btnSubAtributos = new LinkButton();
                btnSubAtributos.ID = "btnSubAtributosReceta_" + Receta.id;
                btnSubAtributos.CssClass = "btn btn-xs";
                btnSubAtributos.Attributes.Add("data-toggle", "modal");
                btnSubAtributos.Attributes.Add("onclick", "actualizarBotonAtributo("+Receta.id+")");
                btnSubAtributos.Text = "<span><i style='color:black' class='fa fa-wrench - o'></i></span>";
                btnSubAtributos.OnClientClick = "abrirdialog(" + Receta.id + ");";
                btnSubAtributos.Attributes.Add("title", "Ver Atributos");
                celAccion.Controls.Add(btnSubAtributos);

                //LinkButton btnArticulos = new LinkButton();
                //btnArticulos.ID = "btnArticulos" + Receta.id;
                //btnArticulos.CssClass = "btn btn-xs";
                //btnArticulos.Attributes.Add("data-toggle", "modal");
                //btnArticulos.Text = "<span><i class='fa fa-wrench - o'></i></span>";
                //btnArticulos.Click += new EventHandler(this.agregarArticulo);
                //celAccion.Controls.Add(btnArticulos);

                LinkButton btnEvolucion = new LinkButton();
                btnEvolucion.ID = "btnEvolucion_" + Receta.id;
                btnEvolucion.CssClass = "btn btn-xs";
                btnEvolucion.Click += new EventHandler(this.verEvolucionReceta);
                btnEvolucion.Text = "<span><i style='color:black' class='fa fa-line-chart'></i></span>";
                btnEvolucion.Attributes.Add("title", "Ver Evolucion");
                celAccion.Controls.Add(btnEvolucion);


                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important; text-align:end");
                tr.Cells.Add(celAccion);

                phRecetas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        private void agregarArticulo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Articulos.aspx?a=3&r=" + id[1]);
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        private void infoReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("RecetasABM.aspx?a=2&i=" + id[1] + "&b=1");
            }
            catch (Exception Ex)
            {

            }
        }

        public void CargarRecetasPHModal(Tecnocuisine_API.Entitys.Recetas Receta)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = Receta.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Receta.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = Receta.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);


                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary btn-xs";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.Attributes.Add("onclick", "agregarReceta(this.id); return false;");
                btnDetalles.ID = "btnSelecRec_" + Receta.id + "_" +  Receta.descripcion;
                btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phRecetasModal.Controls.Add(tr);

            }
            catch (Exception ex)
            {

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



                //cargarNestedListCategoriasHijas(item.id, li);
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


        protected void editarReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("RecetasABM.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void verEvolucionReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("RecetaEvolucion.aspx?id=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.hiddenReceta.Value != "")
                {
                    EditarReceta();
                }
                else
                {
                    GuardarReceta();
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
                txtDescripcionReceta.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarReceta()
        {
            try
            {
                Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();

                Receta.descripcion = txtDescripcionReceta.Text;
                Receta.estado = 1;
                Receta.coeficiente = Convert.ToDecimal(hiddenCoeficiente.Value.Replace('.',','));
                Receta.peso = Convert.ToDecimal(hiddenPeso.Value.Replace('.', ','));
                if(txtRinde.Text == "")
                {
                    Receta.rinde = null;
                }
                else
                {
                    Receta.rinde = Convert.ToDecimal(txtRinde.Text.Replace('.', ','));
                }
                if(txtDesperdicio.Text == "")
                {
                    Receta.desperdicio = null;
                    Receta.merma = null;
                }
                else
                {
                    Receta.desperdicio = Convert.ToDecimal(txtDesperdicio.Text.Replace('.', ','));
                    Receta.merma = Convert.ToDecimal(txtMerma.Text.Replace('.', ','));
                }
                
                //Receta.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorReceta.AgregarReceta(Receta);

                if (resultado > 0)
                {
                    string[] items = idProductosRecetas.Value.Split(';');
                    int idProducto = 0;
                    foreach (var pr in items)
                    {
                        if (pr != "")
                        {
                            string[] producto = pr.Split(',');
                            if (producto[1] == "Producto")
                            {
                                Recetas_Producto productoNuevo = new Recetas_Producto();
                                productoNuevo.idReceta = resultado;
                                productoNuevo.idProducto = Convert.ToInt32(producto[0]);
                                idProducto = productoNuevo.idProducto;
                                productoNuevo.cantidad = Convert.ToInt32(producto[2]);
                                controladorReceta.AgregarReceta_Producto(productoNuevo);
                            }
                            else
                            {
                                Recetas_Receta recetaNueva = new Recetas_Receta();
                                recetaNueva.idReceta = resultado;
                                recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
                                recetaNueva.cantidad = Convert.ToInt32(producto[2]);
                                controladorReceta.AgregarReceta_Receta(recetaNueva);
                            }
                        }
                    }
                    if (controladorReceta.EsMonoProducto(resultado))
                    {
                        int categoria = controladorProducto.ObtenerProductoId(idProducto).categoria;
                        if(categoria > 0)
                        {
                            Tecnocuisine_API.Entitys.Recetas r = controladorReceta.ObtenerRecetaId(resultado);
                            r.categoria = categoria;
                            controladorReceta.EditarReceta(r);

                            List<Productos_Atributo> atributos = controladorAtributo.ObtenerAtributosByIdProducto(idProducto);
                            foreach(Productos_Atributo item in atributos)
                            {
                                Recetas_Atributo atributo = new Recetas_Atributo();
                                atributo.idAtributo = item.atributo;
                                atributo.idReceta = resultado;


                                controladorReceta.AgregarReceta_Atributo(atributo);
                            }

                        }
                    }
                    Response.Redirect("Recetas.aspx?m=1");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el Receta", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void EditarAtributoReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Recetas.aspx?a=3&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        public void EditarReceta()
        {
            try
            {
                Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();

                Receta.id = idReceta;
                Receta.descripcion = txtDescripcionReceta.Text;
                Receta.estado = 1;
                Receta.coeficiente = Convert.ToDecimal(hiddenCoeficiente.Value.Replace('.', ','));
                Receta.peso = Convert.ToDecimal(hiddenPeso.Value.Replace('.', ','));
                if (txtRinde.Text == "")
                {
                    Receta.rinde = null;
                }
                else
                {
                    Receta.rinde = Convert.ToDecimal(txtRinde.Text.Replace('.', ','));
                }
                if (txtDesperdicio.Text == "")
                {
                    Receta.desperdicio = null;
                    Receta.merma = null;
                }
                else
                {
                    Receta.desperdicio = Convert.ToDecimal(txtDesperdicio.Text.Replace('.', ','));
                    Receta.merma = Convert.ToDecimal(txtMerma.Text.Replace('.', ','));
                }

                int resultado = controladorReceta.EditarReceta(Receta);

                if (resultado > 0)
                {
                    controladorReceta.EliminarIngredientes(Receta.id);
                    string[] items = idProductosRecetas.Value.Split(';');
                    foreach (var pr in items)
                    {
                        string[] producto = pr.Split(',');
                        if (producto[1] == "Producto")
                        {
                            Recetas_Producto productoNuevo = new Recetas_Producto();
                            productoNuevo.idReceta = Receta.id;
                            productoNuevo.idProducto = Convert.ToInt32(producto[0]);
                            productoNuevo.cantidad = Convert.ToInt32(producto[2]);
                            controladorReceta.AgregarReceta_Producto(productoNuevo);
                        }
                        else
                        {
                            Recetas_Receta recetaNueva = new Recetas_Receta();
                            recetaNueva.idReceta = Receta.id;
                            recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
                            recetaNueva.cantidad = Convert.ToInt32(producto[2]);
                            controladorReceta.AgregarReceta_Receta(recetaNueva);
                        }
                    }
                Response.Redirect("Recetas.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la Receta", "warning");
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
                int idReceta = Convert.ToInt32(this.hiddenID.Value);
                if (controladorReceta.verificarRelacionRecetaReceta(idReceta))
                {
                    int resultado = controladorReceta.EliminarReceta(idReceta);

                    if (resultado > 0)
                    {
                        Response.Redirect("Recetas.aspx?m=3");
                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "No se pudo eliminar el Receta", "warning");
                    }
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la receta porque tiene una receta relacionada", "Alerta", "warning");

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion





       

        protected void btnAtributos_Click(object sender, EventArgs e)
        {


        }

        protected void txtRinde_TextChanged(object sender, EventArgs e)
        {
            txtDesperdicio.Text = (Convert.ToDecimal(txtPesoBruto.Text) / Convert.ToDecimal(txtRinde.Text)).ToString();
        }

        [WebMethod]
        public static string GetSubAtributos(int id)
        {
            ControladorReceta controlador = new ControladorReceta();
            Tecnocuisine_API.Entitys.Recetas r = controlador.ObtenerRecetaId(id);
            string resultado = "";
            if(r.Recetas_Atributo.Count > 0)
             resultado = "true," + id.ToString(); 
            else
                resultado = "false," + id.ToString();
            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(resultado);
            return resultadoJSON;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int idReceta = Convert.ToInt32(hiddenidReceta.Value);
           Tecnocuisine_API.Entitys.Recetas r = controladorReceta.ObtenerRecetaId(idReceta);
            if (r != null)
            {
                controladorReceta.EliminarAtributos(idReceta);
                r.categoria = Convert.ToInt32(idCategoria.Value.Trim());
                int nuevaReceta = controladorReceta.EditarReceta(r);

                if (nuevaReceta > 0)
                {
                    string[] atributos = idAtributo.Value.Split(',');


                    foreach (string s in atributos)
                    {
                        if (s != "")
                        {
                            Recetas_Atributo atributo = new Recetas_Atributo();
                            atributo.idReceta = Convert.ToInt32(idReceta);
                            atributo.idAtributo = Convert.ToInt32(s.Split('-')[0]);
                            controladorReceta.AgregarReceta_Atributo(atributo);
                        }

                    }
                    Response.Redirect("Recetas.aspx?m=1");

                }

            }

        }
    }
}