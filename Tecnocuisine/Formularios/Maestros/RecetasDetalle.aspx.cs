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
    public partial class RecetasDetalle : Page
    {
        Mensaje m = new Mensaje();
        ControladorReceta controlador = new ControladorReceta();
        int accion;
        int idReceta;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.idReceta = Convert.ToInt32(Request.QueryString["id"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (!IsPostBack)
            {
                lblReceta.InnerText = controlador.ObtenerRecetaId(idReceta).descripcion;
                cargarNestedListRecetas();
                //cargarCategorias();

                if (Mensaje != 0)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

            }


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

        //public void ObtenerInsumos()
        //{
        //    try
        //    {
        //        var insumos = controladorInsumo.ObtenerTodosInsumos();

        //        if (insumos.Count > 0)
        //        {

        //            foreach (var item in insumos)
        //            {
        //                CargarInsumosPH(item);

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void cargarCategorias()
        //{
        //    try
        //    {
        //        this.ListCategorias.DataSource = controlador.ObtenerCategorias();
        //        this.ListCategorias.DataValueField = "id";
        //        this.ListCategorias.DataTextField = "descripcion";
        //        this.ListCategorias.DataBind();
        //        this.ListCategorias.Items.Insert(0, new ListItem("Sin Padre", "-1"));

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}






        private void cargarNestedListRecetas()
        {
            List<Tecnocuisine_API.Entitys.Recetas> recetas = controlador.obteneRecetasPadres(idReceta);
            foreach (Tecnocuisine_API.Entitys.Recetas item in recetas)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.id.ToString());
                li.Attributes.Add("runat", "server");

                main.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle editable");
                div.InnerText = item.id + " - " + item.descripcion;

                li.Controls.Add(div);






                cargarNestedListProductosPadres(item.id, li);
                cargarNestedListRecetasPadres(item.id, li);
            }
            List<Tecnocuisine_API.Entitys.Productos> productos = controlador.obteneProductosPadres(idReceta);
            foreach (Tecnocuisine_API.Entitys.Productos item in productos)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.id.ToString());
                li.Attributes.Add("runat", "server");

                main.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle editable");
                div.InnerText = item.id + " - " + item.descripcion;

                li.Controls.Add(div);




                


                cargarNestedListProductosPadres(item.id, li);
            }
        }

        private void cargarNestedListProductosPadres(int id, HtmlGenericControl li)
        {
            try
            {
                List<Tecnocuisine_API.Entitys.Productos> productos = controlador.obteneProductosPadres(id);
                if (productos.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Productos item in productos)
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


                        


                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        private void cargarNestedListRecetasPadres(int id, HtmlGenericControl li)
        {
            try
            {
                List<Tecnocuisine_API.Entitys.Recetas> recetas = controlador.obteneRecetasPadres(id);
                if (recetas.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Recetas item in recetas)
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


                        


                        cargarNestedListProductosPadres(item.id, liHijo);
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