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
    public partial class EntidadesBancarias : System.Web.UI.Page
    {
        ControladorPaises ControladorPaises = new ControladorPaises();
        ControladorEntidadesBancarias controladorEntidadesBancarias = new ControladorEntidadesBancarias();
        Mensaje m = new Mensaje();
        int Mensaje;
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
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
            CargarBancos();
            if (Mensaje == 1)
            {
                this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }
            if (Mensaje == 5)
            {
                this.m.ShowToastr(this.Page, "Ya Existe ese Banco en Nuestros Registros!", "warning");
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

        private void CargarPaisEditar()
        {
            try
            {
                btnAgregar.Text = "Modificar";
                var banco = controladorEntidadesBancarias.ObtenerBancariasByID(id);
                txtCodigo.Text = banco.Codigo;
                txtNombre.Text = banco.Nombre;

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

                Response.Redirect("EntidadesBancarias.aspx?m=0&a=2&i=" + id[0]);
                //return;
            }
            catch (Exception Ex)
            {

            }
        }
        private void CargarBancos()
        {
            try
            {
                var Lista = controladorEntidadesBancarias.TraerCuentasBancarias();
                foreach (var bancos in Lista)
                {
                    CargarPHPaises(bancos);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarPHPaises(Tecnocuisine_API.Entitys.EntidadesBancarias p)
        {
            try
            {
                TableRow tr = new TableRow();


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = p.Codigo;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);


                TableCell celTipoDoc = new TableCell();
                celTipoDoc.Text = p.Nombre;
                celTipoDoc.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoDoc);

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = p.id.ToString();

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar entidad bancaria'></i></span>";
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
                btnEliminar.Text = "<span><i style='color:red;' class='fa fa-trash' title='Eliminar'></i></span>";
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
                int idBanco = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorEntidadesBancarias.EliminarEntidadBancaria(idBanco);

                if (resultado > 0)
                {
                    Response.Redirect("EntidadesBancarias.aspx?m=1");
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
                if (accion == 2)
                {
                    //editar
                    var banco = controladorEntidadesBancarias.ObtenerBancariasByID(id);
                    banco.Codigo = txtCodigo.Text;
                    banco.Nombre = txtNombre.Text;

                    int i = controladorEntidadesBancarias.EditarBancos(banco);
                    if (i > 0)
                    {
                        Response.Redirect("EntidadesBancarias.aspx?m=1");
                    }
                }
                else //guardar nuevo
                {
                    Tecnocuisine_API.Entitys.EntidadesBancarias nuevoBanco = new Tecnocuisine_API.Entitys.EntidadesBancarias();

                    nuevoBanco.Codigo = txtCodigo.Text;
                    nuevoBanco.Nombre = txtNombre.Text;

                    int i = controladorEntidadesBancarias.AgregarEntidadBancaria(nuevoBanco);
                    if (i > 0)
                    {
                        Response.Redirect("EntidadesBancarias.aspx?m=1");
                    }
                    else if (i == -6)
                    {
                        Response.Redirect("EntidadesBancarias.aspx?m=5");
                    }
                }


            }
            catch (Exception ex)
            {


            }
        }
    }
}