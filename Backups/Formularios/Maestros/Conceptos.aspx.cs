using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Maestros
{
    public partial class Conceptos : Page 
    {
        Mensaje m = new Mensaje();
        ControladorRubros controladorRubros = new ControladorRubros();
        ControladorConceptos controladorConceptos = new ControladorConceptos();
        int accion;
        int idRubro;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
    
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idRubro = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
        
                if (accion == 2)
                {
                    CargarRubros();
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

            ObtenerConceptos();

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


        public void ObtenerConceptos()
        {
            try
            {
                var rubros = controladorConceptos.ObtenerTodosConceptos();

                if (rubros.Count > 0)
                {

                    foreach (var item in rubros)
                    {
                        CargarRubrosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarRubros()
        {
            try
            {
                var presentacion = controladorRubros.ObtenerRubrosId(this.idRubro);
                if (presentacion != null)
                {
                    hiddenEditar.Value = presentacion.id.ToString();
                    txtDescripcionPresentacion.Text = presentacion.descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                }


            }
            catch (Exception ex)
            {

            }
        }

        public void CargarRubrosPH(Tecnocuisine_API.Entitys.Conceptos rubro)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = rubro.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = rubro.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = rubro.descripcion;
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
                btnDetalles.ID = "btnSelec_" + rubro.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarPresentacion);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + rubro.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + rubro.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phRubros.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarPresentacion(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Conceptos.aspx?a=2&i=" + id[1]);
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
                    EditarPresentacion();
                }
                else
                {
                    GuardarPresentacion();
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
                txtDescripcionPresentacion.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarPresentacion()
        {
            try
            {
                Tecnocuisine_API.Entitys.Conceptos rubro = new Tecnocuisine_API.Entitys.Conceptos();

                rubro.descripcion = txtDescripcionPresentacion.Text;
                rubro.estado = true;

                int resultado = controladorConceptos.AgregarConceptos(rubro);

                if (resultado > 0)
                {
                    Response.Redirect("Conceptos.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el Concepto", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarPresentacion()
        {
            try
            {
                Tecnocuisine_API.Entitys.Conceptos rubro = new Tecnocuisine_API.Entitys.Conceptos();

                rubro.id = this.idRubro;
                rubro.descripcion = txtDescripcionPresentacion.Text;
                rubro.estado = true;


                int resultado = controladorConceptos.EditarConceptos(rubro);

                if (resultado > 0)
                {
                    Response.Redirect("Conceptos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar el Concepto", "warning");
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
                int idRubro = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorConceptos.EliminarConceptos(idRubro);

                if (resultado > 0)
                {
                    Response.Redirect("Conceptos.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el Concepto", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}