using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Atributos : Page
    {
        Mensaje m = new Mensaje();
        ControladorAtributo controlador = new ControladorAtributo();
        int accion;
        int idAtributo;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.idAtributo = Convert.ToInt32(Request.QueryString["id"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (!IsPostBack)
            {

                cargarNestedListTipoAtributos();
                //cargarAtributos();
                //cargarTiposAtributos();

                if (accion == 2)
                {
                    cargarAtributo();
                }

                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
            }

        }

   

        private void cargarAtributo()
        {
            try
            {
                var atributo = controlador.ObtenerAtributoById(this.idAtributo);
                var atributoPadre = controlador.ObtenerAtributoPadreById(this.idAtributo);



            }
            catch (Exception ex)
            {

            }
        }






        protected void guardarAtributo()
        {
            Tecnocuisine_API.Entitys.Atributos atributo = new Tecnocuisine_API.Entitys.Atributos();
            atributo.descripcion = txtDescripcionAtributoModal.Text;
            atributo.activo = 1;
            atributo.id = controlador.AgregarAtributo(atributo);

            //Atributos_Familia categoriaFamilia = new Atributos_Familia { idAtributo = atributo.id, idPadre = Convert.ToInt32(ListAtributos.SelectedValue) };

            //if(ListAtributos.SelectedValue == "-1")
            {
                //categoriaFamilia.idPadre = null;
            }
            if (atributo.id > 0)
            {
                //controlador.AgregarAtributoFamilia(/*categoriaFamilia*/);

            }
            if (atributo.id > 0)
            {
                //controlador.AgregarTipoAtributo(atributo.id,Convert.ToInt32(ListInsumos.SelectedValue));
                Response.Redirect("Atributos.aspx?m=1");
            }

        }

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

                HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnAgregar.ID = "btnSelec_" + item.id_insumo + "_";
                btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                btnAgregar.Attributes.Add("data-toggle", "modal");
                btnAgregar.Attributes.Add("href", "#modalAgregarAtributo");
                btnAgregar.Attributes.Add("data-action", "add");
                div.Controls.Add(btnAgregar);

                li.Controls.Add(div);

                

                cargarNestedListAtributos(item.id_insumo, li);
            }

        }

        private void cargarNestedListAtributos(int idTipoAtributo, HtmlGenericControl liPadre)
        {
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

                    HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                    btnEliminar.ID = "btnEliminar_" + item.id;
                    btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right");
                    btnEliminar.Attributes.Add("data-toggle", "modal");
                    btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                    btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                    btnEliminar.Attributes.Add("data-action", "delete");
                    div.Controls.Add(btnEliminar);

                    Literal l2 = new Literal();
                    l2.Text = "&nbsp";
                    div.Controls.Add(l2);

                    HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                    btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right");
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
                    btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnAgregar.ID = "btnSelec_" + item.id + "_";
                    btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                    btnAgregar.Attributes.Add("data-toggle", "modal");
                    btnAgregar.Attributes.Add("href", "#modalAgregarSubAtributo");
                    btnAgregar.Attributes.Add("data-action", "add");
                    div.Controls.Add(btnAgregar);
                   
                    cargarNestedListAtributosHijos(item.id, liHijo);
                }
            }
        }

        private void cargarNestedListAtributosHijos(int id, HtmlGenericControl li)
        {
            try
            {
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

                       

                        HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                        btnEliminar.ID = "btnEliminar_" + item.id;
                        btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right");
                        btnEliminar.Attributes.Add("data-toggle", "modal");
                        btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                        btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                        btnEliminar.Attributes.Add("data-action", "delete");
                        div.Controls.Add(btnEliminar);

                        Literal l3 = new Literal();
                        l3.Text = "&nbsp";
                        div.Controls.Add(l3);


                        HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                        btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnDetalles.ID = "btnSelec_" + item.id + "_";
                        btnDetalles.InnerHtml = "<span><i class='fa fa-pencil'></i></span>";
                        btnDetalles.Attributes.Add("data-action", "edit");
                        div.Controls.Add(btnDetalles);

                        Literal l2 = new Literal();
                        l2.Text = "&nbsp";
                        div.Controls.Add(l2);

                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnSelec_" + item.id + "_";
                        btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                        btnAgregar.Attributes.Add("data-toggle", "modal");
                        btnAgregar.Attributes.Add("href", "#modalAgregarSubAtributo");
                        btnAgregar.Attributes.Add("data-action", "add");
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

        private void editarAtributo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Atributos.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idAtributo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controlador.EliminarAtributo(idAtributo);

                if (resultado > 0)
                {
                    resultado = controlador.EliminarFamiliaAtributo(idAtributo);
                    if (resultado > 0)
                        Response.Redirect("Atributos.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el atributo", "warning");
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
                if (this.accion == 2)
                {
                    editarAtributo();
                }
                else
                {
                    guardarAtributo();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void editarAtributo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Atributos atributo = new Tecnocuisine_API.Entitys.Atributos();
                atributo.id = this.idAtributo;
                atributo.descripcion = txtDescripcionAtributoModal.Text;
                atributo.activo = 1;

                int resultado = controlador.EditarAtributo(atributo);

                if (resultado > 0)
                {
                    ////int? atributoPadre = Convert.ToInt32(ListAtributos.SelectedValue);
                    //if (atributoPadre == -1)
                    //{
                    //    atributoPadre = null;
                    //}
                    //int i = controlador.EditarTipoAtributo(atributo.id, Convert.ToInt32(ListInsumos.SelectedValue));
                    //resultado = controlador.EditarPadreAtributo(atributo.id, atributoPadre);
                    //if (i > 0)
                    Response.Redirect("Atributos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar el atributo", "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            Tecnocuisine_API.Entitys.Atributos atributo = new Tecnocuisine_API.Entitys.Atributos();
            atributo.descripcion = txtSubAtributo.Text;
            atributo.activo = 1;
            atributo.id = controlador.AgregarAtributo(atributo);

            Atributos_Familia categoriaFamilia = new Atributos_Familia { idAtributo = atributo.id, idPadre = Convert.ToInt32(hiddenID2.Value) };

           
            if (atributo.id > 0)
            {
                controlador.AgregarAtributoFamilia(categoriaFamilia);

            }
            if (atributo.id > 0)
            {

                var atributoPadre = controlador.ObtenerAtributoById(Convert.ToInt32(hiddenID2.Value)).Insumos.id_insumo;
                controlador.AgregarTipoAtributo(atributo.id, atributoPadre);
                Response.Redirect("Atributos.aspx?m=1");
            }

        }

        protected void btnAgregarAtributoFromTipoAtributo_Click(object sender, EventArgs e)
        {
            Tecnocuisine_API.Entitys.Atributos atributo = new Tecnocuisine_API.Entitys.Atributos();
            atributo.descripcion = txtDescripcionAtributoModal.Text;
            atributo.activo = 1;
            atributo.id = controlador.AgregarAtributo(atributo);

            if (atributo.id > 0)
            {

                controlador.AgregarTipoAtributo(atributo.id, Convert.ToInt32(hiddenID2.Value));
                Response.Redirect("Atributos.aspx?m=1");
            }
        }

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