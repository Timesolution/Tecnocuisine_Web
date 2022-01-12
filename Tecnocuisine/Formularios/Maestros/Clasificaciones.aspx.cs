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
    public partial class Clasificaciones : Page
    {
        Mensaje m = new Mensaje();
        ControladorClasificacion controladorClasificacion = new ControladorClasificacion();
        int accion;
        int idClasificacion;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
    
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idClasificacion = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
        
                if (accion == 2)
                {
                    CargarClasificacion();
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

            ObtenerClasificaciones();

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


        public void ObtenerClasificaciones()
        {
            try
            {
                var Clasificaciones = controladorClasificacion.ObtenerTodosClasificaciones();

                if (Clasificaciones.Count > 0)
                {

                    foreach (var item in Clasificaciones)
                    {
                        CargarClasificacionesPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarClasificacion()
        {
            try
            {
                var Clasificacion = controladorClasificacion.ObtenerClasificacionId(this.idClasificacion);
                if (Clasificacion != null)
                {
                    hiddenEditar.Value = Clasificacion.id.ToString();
                    txtDescripcionClasificacion.Text = Clasificacion.descripcion;
                    txtCodigoClasificacion.Text = Clasificacion.codigo.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                }


            }
            catch (Exception ex)
            {

            }
        }

        public void CargarClasificacionesPH(Tecnocuisine_API.Entitys.Clasificacion Clasificacion)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = Clasificacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Clasificacion.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celCodigo = new TableCell();
                celCodigo.Text = Clasificacion.codigo.ToString();
                celCodigo.VerticalAlign = VerticalAlign.Middle;
                celCodigo.HorizontalAlign = HorizontalAlign.Right;
                celCodigo.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCodigo);

                TableCell celNombre = new TableCell();
                celNombre.Text = Clasificacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

               

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + Clasificacion.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarClasificacion);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + Clasificacion.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + Clasificacion.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phClasificaciones.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarClasificacion(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Clasificaciones.aspx?a=2&i=" + id[1]);
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
                    EditarClasificacion();
                }
                else
                {
                    GuardarClasificacion();
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
                txtDescripcionClasificacion.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarClasificacion()
        {
            try
            {
                Tecnocuisine_API.Entitys.Clasificacion Clasificacion = new Tecnocuisine_API.Entitys.Clasificacion();

                Clasificacion.descripcion = txtDescripcionClasificacion.Text;
                Clasificacion.codigo = txtCodigoClasificacion.Text;
                Clasificacion.estado = 1;

                int resultado = controladorClasificacion.AgregarClasificacion(Clasificacion);

                if (resultado > 0)
                {
                    Response.Redirect("Clasificaciones.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar la Clasificacion", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarClasificacion()
        {
            try
            {
                Tecnocuisine_API.Entitys.Clasificacion Clasificacion = new Tecnocuisine_API.Entitys.Clasificacion();

                Clasificacion.id = this.idClasificacion;
                Clasificacion.descripcion = txtDescripcionClasificacion.Text;
                Clasificacion.codigo = txtCodigoClasificacion.Text;
                Clasificacion.estado = 1;


                int resultado = controladorClasificacion.EditarClasificacion(Clasificacion);

                if (resultado > 0)
                {
                    Response.Redirect("Clasificaciones.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la Clasificacion", "warning");
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
                int idClasificacion = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorClasificacion.EliminarClasificacion(idClasificacion);

                if (resultado > 0)
                {
                    Response.Redirect("Clasificaciones.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la Clasificacion", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}