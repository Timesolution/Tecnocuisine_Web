using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
    public partial class Categorias : Page
    {
        Mensaje m = new Mensaje();
        ControladorCategoria controlador = new ControladorCategoria();
        int accion;
        int idCategoria;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.idCategoria = Convert.ToInt32(Request.QueryString["id"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (!IsPostBack)
            {


                if (accion == 2)
                {
                    cargarCategoria();
                }
                cargarNestedListCategorias();
                //cargarCategorias();

                if (Mensaje != 0)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

            }
            ObtenerInsumos();


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


        protected void GuardarCategoria()
        {
            Tecnocuisine_API.Entitys.Categorias categoria = new Tecnocuisine_API.Entitys.Categorias();
            categoria.descripcion = txtDescripcionCategoria.Text;
            categoria.activo = 1;
            categoria.id = controlador.AgregarCategoria(categoria);


            if (categoria.id > 0)
            {
                Response.Redirect("Categorias.aspx?m=1");
            }

        }


        private void cargarNestedListCategorias()
        {
            List<Tecnocuisine_API.Entitys.Categorias> categorias = controlador.obtenerCategoriasPrimerNivel();
            foreach (Tecnocuisine_API.Entitys.Categorias item in categorias)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.id.ToString());
                li.Attributes.Add("id", item.id.ToString());
                li.Attributes.Add("runat", "server");

                main.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle drag_disabled editable");
                div.InnerText = item.descripcion;

                li.Controls.Add(div);




                HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                btnEliminar.ID = "btnEliminar_" + item.id;
                btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right drag_enabled");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("style", "margin-right: 0.3%;");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.Attributes.Add("data-action", "delete");
                div.Controls.Add(btnEliminar);

                Literal l4 = new Literal();
                l4.Text = "&nbsp";
                div.Controls.Add(l4);

                HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right drag_enabled");
                btnDetalles.Attributes.Add("style", "margin-right: 0.3%;");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + item.id + "_";
                btnDetalles.InnerHtml = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Attributes.Add("data-action", "edit");
                div.Controls.Add(btnDetalles);


                Literal l3 = new Literal();
                l3.Text = "&nbsp";
                div.Controls.Add(l3);


                HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right drag_enabled");
                btnAgregar.Attributes.Add("style", "margin-right: 0.3%;");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnAgregar.ID = "btnAgregar_" + item.id + "_";
                btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                btnAgregar.Attributes.Add("data-toggle", "modal");
                btnAgregar.Attributes.Add("href", "#modalAgregarSubCategoria");
                btnAgregar.Attributes.Add("data-action", "add");
                div.Controls.Add(btnAgregar);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                div.Controls.Add(l2);

                HtmlGenericControl btnSubAtributos = new HtmlGenericControl("button");
                btnSubAtributos.ID = "btnSubAtributo_" + item.id;
                btnSubAtributos.Attributes.Add("class", "btn btn-info btn-xs pull-right drag_enabled");
                btnSubAtributos.Attributes.Add("style", "margin-right: 0.3%;");
                btnSubAtributos.Attributes.Add("data-toggle", "modal");
                btnSubAtributos.Attributes.Add("href", "#modalSubAtributo");
                btnSubAtributos.Attributes.Add("OnClientClick", "abrirdialog()");
                btnSubAtributos.InnerHtml = "<span><i class='fa fa-wrench - o'></i></span>";
                btnSubAtributos.Attributes.Add("data-action", "subAttribute");
                div.Controls.Add(btnSubAtributos);


                cargarNestedListCategoriasHijas(item.id, li);
            }
        }

        private void cargarNestedListCategoriasHijas(int id, HtmlGenericControl li)
        {
            try
            {
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
                        liHijo.Attributes.Add("id", item.id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle drag_disabled editable");
                        div.InnerText = item.descripcion;


                        liHijo.Controls.Add(div);


                        HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                        btnEliminar.ID = "btnEliminar_" + item.id;
                        btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right drag_enabled");
                        btnEliminar.Attributes.Add("style", "margin-right: 0.3%;");
                        btnEliminar.Attributes.Add("data-toggle", "modal");
                        btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                        btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                        btnEliminar.Attributes.Add("data-action", "delete");
                        div.Controls.Add(btnEliminar);

                        Literal l2 = new Literal();
                        l2.Text = "&nbsp";
                        div.Controls.Add(l2);

                        HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                        btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right drag_enabled");
                        btnDetalles.Attributes.Add("style", "margin-right: 0.3%;");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnDetalles.ID = "btnSelec_" + item.id + "_";
                        btnDetalles.InnerHtml = "<span><i class='fa fa-pencil'></i></span>";
                        btnDetalles.Attributes.Add("data-action", "edit");
                        div.Controls.Add(btnDetalles);


                        Literal l3 = new Literal();
                        l3.Text = "&nbsp";
                        div.Controls.Add(l3);


                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right drag_enabled");
                        btnAgregar.Attributes.Add("style", "margin-right: 0.3%;");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnAgregar_" + item.id + "_";
                        btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                        btnAgregar.Attributes.Add("data-toggle", "modal");
                        btnAgregar.Attributes.Add("href", "#modalAgregarSubCategoria");
                        btnAgregar.Attributes.Add("data-action", "add");
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

        private void editarCategoria(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Categorias.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        public void cargarCategoria()
        {
            try
            {
                var categoria = controlador.ObtenerCategoriaById(this.idCategoria);
                if (categoria != null)
                {
                    hiddenEditar.Value = categoria.id.ToString();
                    txtDescripcionCategoria.Text = categoria.descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                int idCategoria = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controlador.EliminarCategoria(idCategoria);

                if (resultado > 0)
                {
                    resultado = controlador.EliminarFamiliaCategoria(idCategoria);
                    if (resultado > 0)
                        Response.Redirect("Categorias.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la categoria", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EditarCategoria()
        {
            try
            {
                Tecnocuisine_API.Entitys.Categorias categoria = new Tecnocuisine_API.Entitys.Categorias();
                categoria.id = this.idCategoria;
                categoria.descripcion = txtDescripcionCategoria.Text;
                categoria.activo = 1;

                int resultado = controlador.EditarCategoria(categoria);

                if (resultado > 0)
                {
                    //int? categoriaPadre = Convert.ToInt32(ListCategorias.SelectedValue);
                    //if(categoriaPadre == -1)
                    //{
                    //    categoriaPadre = null;
                    //}
                    //resultado = controlador.EditarPadreCategoria(categoria.id,categoriaPadre);
                    //if(resultado > 0)
                    Response.Redirect("Categorias.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la categoria", "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.hiddenEditar.Value != "")
                {
                    EditarCategoria();
                }
                else
                {
                    GuardarCategoria();
                }

            }
            catch (Exception)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Tecnocuisine_API.Entitys.Categorias categoria = new Tecnocuisine_API.Entitys.Categorias();
            categoria.descripcion = txtSubCategoria.Text;
            categoria.activo = 1;
            categoria.id = controlador.AgregarCategoria(categoria);

            Categorias_Familia categoriaFamilia = new Categorias_Familia { idCategoria = categoria.id, idPadre = Convert.ToInt32(hiddenID2.Value) };

            if (categoria.id > 0)
            {
                int i = controlador.AgregarCategoriaFamilia(categoriaFamilia);

                Response.Redirect("Categorias.aspx?m=1");
            }
        }

        protected void btnSubAtributos_Click(object sender, EventArgs e)
        {
            try
            {
                string idTildado = "";

                foreach (Control C in phInsumos.Controls)
                {
                    TableRow tr = C as TableRow;
                    CheckBox ch = tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox;
                    if (ch.Checked == true)
                    {
                        //idtildado += ch.ID.Substring(12, ch.ID.Length - 12) + ";";
                        idTildado += ch.ID.Split('_')[1] + ";";
                    }
                }

                int i = controlador.AgregarSubAtributo(idTildado, Convert.ToInt32(hiddenSubAtributo.Value));
                if (i > 0)
                {
                    Response.Redirect("Categorias.aspx?m=1");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerInsumos()
        {
            try
            {
                ControladorInsumo controladorInsumo = new ControladorInsumo();
                var insumos = controladorInsumo.ObtenerTodosInsumos();

                if (insumos.Count > 0)
                {

                    foreach (var item in insumos)
                    {
                        CargarInsumosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumosPH(Insumos insumo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = insumo.id_insumo.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = insumo.id_insumo.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = insumo.Descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "cbx_" + insumo.id_insumo;
                celAccion.Controls.Add(checkbox);
                celAccion.Width = Unit.Percentage(10);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;text-align:center; vertical - align:middle;");

                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        [WebMethod]
        public static string GetSubAtributos(int id)
        {
            ControladorCategoria controlador = new ControladorCategoria();
            Tecnocuisine_API.Entitys.Categorias categoriaPadre = controlador.ObtenerCategoriaPadreFinal(id);
            string tiposAtributos = controlador.obtenerTipoAtributos(categoriaPadre.id);

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(tiposAtributos);
            return resultadoJSON;
        }
        [WebMethod]
        public static string GetSubAtributos2(int id)
        {
            try
            {
                ControladorCategoria controlador = new ControladorCategoria();
                ControladorAtributo ca = new ControladorAtributo();

                Tecnocuisine_API.Entitys.Categorias categoriaPadre = controlador.ObtenerCategoriaPadreFinal(id);
                string tiposAtributos = controlador.obtenerTipoAtributos(categoriaPadre.id);
                string[] vec = tiposAtributos.Split(',');
                string datos = "";
                List<Atributos> atributos = new List<Atributos>();

                foreach (var atributoAux in vec)
                {
                    Tecnocuisine_API.Entitys.Atributos a = ca.ObtenerAtributoById(Convert.ToInt32(atributoAux));
                    if (a != null)
                        datos += a.id + "_" + a.descripcion + ",";
                }
                datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(datos);
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize("");
                return resultadoJSON;
            }
           
        }
        [WebMethod]
        public static string GetProductosRecetaByIdReceta(int idReceta)
        {
            ControladorReceta controlador = new ControladorReceta();
            ControladorUnidad cu = new ControladorUnidad();
           
            
            var listaRP= controlador.ObtenerProductosByReceta(idReceta); //recetas_productos

            string datos = "";

            

           foreach (var RP in listaRP)
            {
                if (RP != null)
                {
                    string UnidadMedida = "";
                    if (RP.Productos.unidadMedida != 0)
                    {
                        UnidadMedida = cu.ObtenerUnidadId(RP.Productos.unidadMedida).descripcion;
                    }
                    decimal costoTotal = decimal.Round( RP.cantidad * RP.Productos.costo,2);
                    datos += RP.Productos.id + "_" + RP.Productos.descripcion + "_" + RP.cantidad.ToString().Replace(',', '.') + "_" + UnidadMedida + "_" + RP.Productos.costo.ToString().Replace(',','.') +"_" +costoTotal.ToString().Replace(',','.') +",";
                }
            }
                   
            datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(datos);
            return resultadoJSON;
        }

        [WebMethod]
        public static string GetListProductosRecetaByIdReceta(int idReceta)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();



                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                StringBuilder lista = new StringBuilder("");
                 lista.Append( "<ul>");
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                                                                            //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        lista.Append("<li>" + rr.Recetas.descripcion + "<ul>");
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        dynamic dynamicObject = JsonConvert.DeserializeObject(GetListProductosRecetaByIdReceta(rr.idRecetaIngrediente));
                        string type = dynamicObject;
                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista.Append( type);
                        //foreach (var RP in listaProdAux)
                        //{
                        //    if (RP != null)
                        //    {
                        //        lista += "<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>";

                        //    }
                        //}
                        lista.Append("</ul></li>");

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        lista.Append("<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>");

                    }
                }

                lista.Append( "</ul>");
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(lista.ToString());
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetListProductosCantidadRecetaByIdReceta(int idReceta,decimal Cant)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                var receta = controlador.ObtenerRecetaId(idReceta);

                decimal PR_Cant = Cant /receta.peso;
                decimal PesoT = receta.peso * PR_Cant;

                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                StringBuilder lista = new StringBuilder("");
                lista.Append("<ul>");
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                                                                            //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        decimal PR_cantReceta = rr.cantidad/ receta.peso;
                        decimal auxCantReceta = PesoT * PR_cantReceta;
                       
                        lista.Append( "<li>" + auxCantReceta.ToString("N",culture) + "<ul>");
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        dynamic dynamicObject = JsonConvert.DeserializeObject(GetListProductosCantidadRecetaByIdReceta(rr.idRecetaIngrediente, auxCantReceta));
                        string type = dynamicObject;
                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista.Append( type);
                        //foreach (var RP in listaProdAux)
                        //{
                        //    if (RP != null)
                        //    {
                        //        lista += "<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>";

                        //    }
                        //}
                        lista.Append("</ul></li>");

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        
                        decimal PR_cantProducto = RP.cantidad / receta.peso;
                        decimal auxCantProducto = PesoT * PR_cantProducto;
                        lista.Append("<li data-jstree='{\"type\":\"html\"}'>" + auxCantProducto.ToString("#,##0.000", culture) + "</li>");

                    }
                }

                lista.Append("</ul>");
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(lista.ToString());
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetListProductosCostosRecetaByIdReceta(int idReceta)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();



                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                StringBuilder lista = new StringBuilder("");
                lista.Append("<ul>");
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                                                                            //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        lista.Append("<li>" + rr.Recetas.Costo.Value.ToString("N") + "<ul>");
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        dynamic dynamicObject = JsonConvert.DeserializeObject(GetListProductosCostosRecetaByIdReceta(rr.idRecetaIngrediente));
                        string type = dynamicObject;
                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista.Append(type);
                        //foreach (var RP in listaProdAux)
                        //{
                        //    if (RP != null)
                        //    {
                        //        lista += "<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>";

                        //    }
                        //}
                        lista.Append("</ul></li>");

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        lista.Append("<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.costo.ToString("N") + "</li>");

                    }
                }

                lista.Append("</ul>");
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(lista.ToString());
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetListProductosCostosTotalRecetaByIdReceta(int idReceta, decimal Cant)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();

                var receta = controlador.ObtenerRecetaId(idReceta);

                // TODO: Rompe si el peso es cero
                decimal PR_Cant = Cant / receta.peso;
                decimal PesoT = receta.peso * PR_Cant;

                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                StringBuilder lista = new StringBuilder("");
                lista.Append("<ul>");
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                                                                            //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        decimal PR_cantReceta = rr.cantidad / receta.peso;
                        decimal auxCantReceta = PesoT * PR_cantReceta;
                        decimal CostoTRec = rr.Recetas.Costo.Value * auxCantReceta;
                        lista.Append("<li>" + CostoTRec.ToString("N") + "<ul>");
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        dynamic dynamicObject = JsonConvert.DeserializeObject(GetListProductosCostosTotalRecetaByIdReceta(rr.idRecetaIngrediente, Cant));
                        string type = dynamicObject;
                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista.Append(type);
                        //foreach (var RP in listaProdAux)
                        //{
                        //    if (RP != null)
                        //    {
                        //        lista += "<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>";

                        //    }
                        //}
                        lista.Append("</ul></li>");

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        decimal PR_cantProducto = RP.cantidad / receta.peso;
                        decimal auxCantProducto = PesoT * PR_cantProducto;
                        decimal PrecioProducto = RP.Productos.costo * auxCantProducto;
                        lista.Append("<li data-jstree='{\"type\":\"html\"}'>" + string.Format("{0:N4}", PrecioProducto) + "</li>");

                    }
                }

                lista.Append("</ul>");
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(lista.ToString());
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetListProductosUMRecetaByIdReceta(int idReceta)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                StringBuilder lista = new StringBuilder("");
                lista.Append("<ul>");
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                                                                            //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        var unidad = controladorUnidad.ObtenerUnidadId(rr.Recetas.UnidadMedida.Value);
                        lista.Append("<li>" + unidad.descripcion + "<ul>");
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        dynamic dynamicObject = JsonConvert.DeserializeObject(GetListProductosUMRecetaByIdReceta(rr.idRecetaIngrediente));
                        string type = dynamicObject;
                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista.Append(type);
                        //foreach (var RP in listaProdAux)
                        //{
                        //    if (RP != null)
                        //    {
                        //        lista += "<li data-jstree='{\"type\":\"html\"}'>" + RP.Productos.id + "-" + RP.Productos.descripcion + "</li>";

                        //    }
                        //}
                        lista.Append("</ul></li>");

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        var unidad = controladorUnidad.ObtenerUnidadId(RP.Productos.unidadMedida);
                        lista.Append("<li data-jstree='{\"type\":\"html\"}'>" + unidad.descripcion + "</li>");

                    }
                }

                lista.Append("</ul>");
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();

                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(lista.ToString());
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [WebMethod]
        public static string ModificarRelaciones(string id)
        {
            ControladorCategoria controladorCategoria = new ControladorCategoria();
            string[] ids = id.Split(';');
            int i = controladorCategoria.EliminarTodasFamiliaCategorias();
            if (i > 0)
            {
                foreach (var item in ids)
                {
                    if (item != "")
                    {
                        int? idPadre = null;

                        try
                        {
                            idPadre = Convert.ToInt32(item.Split(',')[1]);

                        }
                        catch (Exception)
                        {
                        }
                        int idCategoria = Convert.ToInt32(item.Split(',')[0]);
                        Categorias_Familia familia = new Categorias_Familia { idCategoria = idCategoria, idPadre = idPadre };
                        controladorCategoria.AgregarCategoriaFamilia(familia);
                    }
                }
            }
            return "1";
        }



    }
}