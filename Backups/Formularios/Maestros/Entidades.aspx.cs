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
    public partial class Entidades : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        ControladorEntidad controladorEntidad = new ControladorEntidad();
        int accion;
        int idInsumo;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idInsumo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

              
                if (accion == 2)
                {
                    CargarInsumo();
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

        public void ObtenerInsumos()
        {
            try
            {
                var entidad = controladorEntidad.ObtenerTodasLasEntidades();

                if (entidad.Count > 0)
                {

                    foreach (var item in entidad)
                    {
                        CargarInsumosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumo()
        {
            try
            {
                var insumo = controladorInsumo.ObtenerInsumoId(this.idInsumo);

                if (insumo != null)
                {
                    hiddenEditar.Value = insumo.id_insumo.ToString();
                    txtDescripcionInsumo.Text = insumo.Descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumosPH(Tecnocuisine_API.Entitys.Entidades entidad)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = entidad.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = entidad.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = entidad.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Width = Unit.Percentage(40);
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + entidad.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.OnClientClick = "abrirdialog2('" + entidad.id + "', '" + entidad.descripcion + "');";
                btnDetalles.Attributes.Add("href", "#");
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + entidad.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + entidad.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

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

                Response.Redirect("Entidades.aspx?a=2&i=" + id[1]);
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
                    EditarInsumo();
                }
                else
                {
                    GuardarInsumo();
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
                //txtDescripcionInsumo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarInsumo()
        {
            try
            {
                
                Tecnocuisine_API.Entitys.Entidades entidad = new Tecnocuisine_API.Entitys.Entidades();

                entidad.descripcion = txtDescripcionInsumo.Text;
                entidad.estado = true;

                int resultado = controladorEntidad.AgregarEntidad(entidad);

                if (resultado > 0)
                {
                    Response.Redirect("Entidades.aspx?m=1");
                }
                else if (resultado == -6)
                {
                    this.m.ShowToastr(this.Page, "Ya existe entidad con ese nombre", "warning");
                } else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar la entidad", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarInsumo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Entidades entidad = new Tecnocuisine_API.Entitys.Entidades();
                entidad.id = Convert.ToInt32(hiddenID.Value);
                entidad.descripcion = txtDescripcion.Text;
                entidad.estado = true;

                int resultado = controladorEntidad.ModificarEntidades(entidad);

                if (resultado > 0)
                {
                    Response.Redirect("Entidades.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar el insumo", "warning");
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
                int idInsumo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorEntidad.EliminarEntidades(idInsumo);

                if (resultado > 0)
                {
                    Response.Redirect("Entidades.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el insumo", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}