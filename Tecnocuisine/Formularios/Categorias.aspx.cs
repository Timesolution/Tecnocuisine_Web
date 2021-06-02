using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
        int idInsumo;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idInsumo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
                cargarCategorias();

                if (accion == 2)
                {
                }
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

        private void cargarCategorias()
        {
            try
            {
                this.ListCategorias.DataSource = controlador.ObtenerCategorias();
                this.ListCategorias.DataValueField = "id";
                this.ListCategorias.DataTextField = "descripcion";
                this.ListCategorias.DataBind();
                this.ListCategorias.Items.Insert(0, new ListItem("Sin Padre", "-1"));

            }
            catch (Exception ex)
            {

            }
        }


        protected void editarInsumo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("InsumosF.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Tecnocuisine_API.Entitys.Categorias categoria = new Tecnocuisine_API.Entitys.Categorias();
            categoria.descripcion = txtDescripcionCategoria.Text;
            categoria.activo = 1;
            categoria.id = controlador.AgregarCategoria(categoria);

            Categorias_Familia categoriaFamilia = new Categorias_Familia { idCategoria = categoria.id, idPadre = Convert.ToInt32(ListCategorias.SelectedValue) };


            if (ListCategorias.SelectedValue != "-1")
            {
                controlador.AgregarCategoriaFamilia(categoriaFamilia);
            }

        }


        private void cargarNestedListCategorias()
        {
            List<Tecnocuisine_API.Entitys.Categorias> categorias = controlador.ObtenerCategorias();
            foreach(Tecnocuisine_API.Entitys.Categorias item in categorias)
            {
                contentArea.Controls.Add(h1);
            }
        }

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.accion == 2)
        //        {
        //            EditarInsumo();
        //        }
        //        else
        //        {
        //            GuardarInsumo();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //#region ABM
        //public void GuardarInsumo()
        //{
        //    try
        //    {
        //        Insumos insumo = new Insumos();

        //        insumo.Descripcion = txtDescripcionInsumo.Text;
        //        insumo.Estado = 1;

        //        int resultado = controladorInsumo.AgregarInsumo(insumo);

        //        if (resultado > 0)
        //        {
        //            this.m.ShowToastr(this.Page, "Proceso concluido con éxito", "Exito");
        //            ObtenerInsumos();
        //        }
        //        else
        //        {
        //            this.m.ShowToastr(this.Page, "Proceso no se pudo concluir", "warning");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //public void EditarInsumo()
        //{
        //    try
        //    {
        //        Insumos insumo = new Insumos();
        //        insumo.id_insumo = this.idInsumo;
        //        insumo.Descripcion = txtDescripcionInsumo.Text;
        //        insumo.Estado = 1;

        //        int resultado = controladorInsumo.EditarInsumo(insumo);

        //        if (resultado > 0)
        //        {
        //            this.m.ShowToastr(this.Page, "Insumo editado con Exito!", "Exito");
        //            Response.Redirect("InsumosF.aspx");

        //        }
        //        else
        //        {
        //            this.m.ShowToastr(this.Page, "No se pudo editar el insumo", "warning");
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void btnSi_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idInsumo = Convert.ToInt32(this.txtMovimiento.Text);
        //        int resultado = controladorInsumo.EliminarInsumo(idInsumo);

        //        if (resultado > 0)
        //        {
        //            this.m.ShowToastr(this.Page, "Insumo Eliminado con Exito!", "Exito");
        //            ObtenerInsumos();
        //        }
        //        else
        //        {
        //            this.m.ShowToastr(this.Page, "No se pudo eliminar el insumo", "warning");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //#endregion

    }
}