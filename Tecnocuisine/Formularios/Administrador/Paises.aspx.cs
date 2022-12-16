using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Tecnocuisine_API.Controladores;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Paises : System.Web.UI.Page
    {
        ControladorPaises ControladorPaises = new ControladorPaises();
        Mensaje m = new Mensaje();
        int Mensaje;
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            if (!IsPostBack)
            {
                switch (accion)
                {
                    case 1:
                        break;
                    case 2:
                        CargarPaisEditar();
                        break;
                }
            }
                CargarPaises();
            if (Mensaje == 1)
            {
                this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }
           
        }

        private void CargarPaisEditar()
        {
            try
            {
                btnAgregar.Text = "Modificar";
                var pais = ControladorPaises.ObtenerPaisById(id);
                txtPais.Text = pais.Pais;
                txtTipoDoc.Text = pais.TipoDocumento;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void editarPais(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Paises.aspx?m=0&a=2&i=" + id[0]);
                //return;
            }
            catch (Exception Ex)
            {

            }
        }
        private void CargarPaises()
        {
            try
            {
                var Lista = ControladorPaises.ObtenerPaisesAll();
                foreach (var pais in Lista)
                {
                    CargarPHPaises(pais);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarPHPaises(Tecnocuisine_API.Entitys.paises p)
        {
            try
            {
                TableRow tr = new TableRow();


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = p.Pais;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);


                TableCell celTipoDoc = new TableCell();
                celTipoDoc.Text = p.TipoDocumento;
                celTipoDoc.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoDoc);

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = p.id.ToString();

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Click += new EventHandler(this.editarPais);
                celAction.Controls.Add(btnEditar);


                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + p.id;


                btnEliminar.OnClientClick = "abrirdialog(" + p.id + ");";
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.Text = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                //btnDetalles.Click += new EventHandler(this.editarReceta);
                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phPaises.Controls.Add(tr);


            }
            catch (Exception ex)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", mje.mensajeBoxError("Error cargando Procedencia en la lista. " + ex.Message));
            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idPais = Convert.ToInt32(this.hiddenID.Value);
                Tecnocuisine_API.Entitys.paises p = ControladorPaises.ObtenerPaisById(idPais);
                p.estado = 0;
                int resultado = ControladorPaises.EditarPais(p);

                if (resultado > 0)
                {
                    Response.Redirect("Paises.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
                }
            }
            catch (Exception ex)
            {
                this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if(accion == 2)
                {
                    //editar
                    var pais = ControladorPaises.ObtenerPaisById(id);
                    pais.Pais  = txtPais.Text;
                    pais.TipoDocumento  = txtTipoDoc.Text;

                    int i = ControladorPaises.EditarPais(pais);
                    if (i > 0)
                    {
                        Response.Redirect("Paises.aspx?m=1");
                    }
                }
                else //guardar nuevo
                {
                    Tecnocuisine_API.Entitys.paises nuevoPais = new Tecnocuisine_API.Entitys.paises();

                    nuevoPais.Pais = txtPais.Text;
                    nuevoPais.TipoDocumento = txtTipoDoc.Text;
                    nuevoPais.estado = 1;

                    int i = ControladorPaises.AgregarNuevoPais(nuevoPais);
                    if (i > 0)
                    {
                        Response.Redirect("Paises.aspx?m=1");
                    }
                }

               
            }
            catch (Exception ex)
            {


            }
        }
    }
}