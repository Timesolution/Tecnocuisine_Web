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
    public partial class Unidades : Page
    {
        Mensaje m = new Mensaje();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        int accion;
        int idUnidad;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idUnidad = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

                if (accion == 2)
                {
                    CargarUnidad();
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

            ObtenerUnidades();

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
        public void ObtenerUnidades()
        {
            try
            {
                var unidades = controladorUnidad.ObtenerTodosUnidades();

                if (unidades.Count > 0)
                {

                    foreach (var item in unidades)
                    {
                        CargarUnidadesPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarUnidad()
        {
            try
            {
                var unidad = controladorUnidad.ObtenerUnidadId(this.idUnidad);
                if (unidad != null)
                {
                    hiddenEditar.Value = unidad.id.ToString();
                    txtDescripcionUnidad.Text = unidad.descripcion;
                    txtAbreviacion.Text = unidad.abreviacion ?? "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CargarUnidadesPH(Tecnocuisine_API.Entitys.Unidades unidad)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = unidad.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = unidad.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important; display:none");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = unidad.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                TableCell celabreviacion = new TableCell();
                celabreviacion.Text = unidad.abreviacion ?? "No asignada";
                celabreviacion.VerticalAlign = VerticalAlign.Middle;
                celabreviacion.HorizontalAlign = HorizontalAlign.Left;
                celabreviacion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celabreviacion);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.ID = "btnSelec_" + unidad.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar unidad'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarUnidad);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + unidad.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash - o' title='Eliminar unidad'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + unidad.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phUnidades.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarUnidad(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Unidades.aspx?a=2&i=" + id[1]);
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
                    EditarUnidad();
                }
                else
                {
                    GuardarUnidad();
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
                txtDescripcionUnidad.Text = "";
                txtAbreviacion.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarUnidad()
        {
            try
            {
                if (txtAbreviacion.Text.Length > 5) return;

                Tecnocuisine_API.Entitys.Unidades unidad = new Tecnocuisine_API.Entitys.Unidades();

                unidad.descripcion = txtDescripcionUnidad.Text.Trim();
                unidad.estado = 1;
                unidad.abreviacion = txtAbreviacion.Text.Trim() != string.Empty ? txtAbreviacion.Text.Trim() : null;

                int resultado = controladorUnidad.AgregarUnidad(unidad);

                if (resultado > 0)
                {
                    Response.Redirect("Unidades.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el unidad", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarUnidad()
        {
            try
            {
                Tecnocuisine_API.Entitys.Unidades unidad = new Tecnocuisine_API.Entitys.Unidades();

                unidad.id = this.idUnidad;
                unidad.descripcion = txtDescripcionUnidad.Text;
                unidad.estado = 1;
                unidad.abreviacion = txtAbreviacion.Text.Trim() != string.Empty ? txtAbreviacion.Text.Trim() : null;

                int resultado = controladorUnidad.EditarUnidad(unidad);

                if (resultado > 0)
                {
                    Response.Redirect("Unidades.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la unidad", "warning");
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
                int idUnidad = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorUnidad.EliminarUnidad(idUnidad);

                if (resultado > 0)
                {
                    Response.Redirect("Unidades.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el unidad", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}