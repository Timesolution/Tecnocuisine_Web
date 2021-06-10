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
    public partial class Presentaciones : Page
    {
        Mensaje m = new Mensaje();
        ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
        int accion;
        int idPresentacion;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idPresentacion = Convert.ToInt32(Request.QueryString["i"]);
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);

            if (!IsPostBack)
            {
                if (accion == 2)
                {
                    CargarPresentacion();
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

            ObtenerPresentaciones();

        }


        public void ObtenerPresentaciones()
        {
            try
            {
                var presentaciones = controladorPresentacion.ObtenerTodosPresentaciones();

                if (presentaciones.Count > 0)
                {

                    foreach (var item in presentaciones)
                    {
                        CargarPresentacionsPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarPresentacion()
        {
            try
            {
                var presentacion = controladorPresentacion.ObtenerPresentacionId(this.idPresentacion);
                this.phPresentaciones.Controls.Clear();

                if (presentacion != null)
                {
                    txtDescripcionPresentacion.Text = presentacion.descripcion;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarPresentacionsPH(Tecnocuisine_API.Entitys.Presentaciones presentacion)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = presentacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = presentacion.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = presentacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary btn-xs";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + presentacion.id + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarPresentacion);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + presentacion.id;
                btnEliminar.CssClass = "btn btn-danger btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + presentacion.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phPresentaciones.Controls.Add(tr);

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

                Response.Redirect("Presentaciones.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.accion == 2)
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
                Tecnocuisine_API.Entitys.Presentaciones presentacion = new Tecnocuisine_API.Entitys.Presentaciones();

                presentacion.descripcion = txtDescripcionPresentacion.Text;
                presentacion.cantidad = Convert.ToDecimal(txtCantidadPresentacion.Text);
                presentacion.estado = 1;

                int resultado = controladorPresentacion.AgregarPresentacion(presentacion);

                if (resultado > 0)
                {
                    Response.Redirect("Presentaciones.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar la presentacion", "warning");
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
                Tecnocuisine_API.Entitys.Presentaciones presentacion = new Tecnocuisine_API.Entitys.Presentaciones();

                presentacion.id = this.idPresentacion;
                presentacion.descripcion = txtDescripcionPresentacion.Text;
                presentacion.estado = 1;

                int resultado = controladorPresentacion.EditarPresentacion(presentacion);

                if (resultado > 0)
                {
                    Response.Redirect("Presentaciones.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la presentacion", "warning");
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
                int idPresentacion = Convert.ToInt32(this.hiddenID.ClientID);
                int resultado = controladorPresentacion.EliminarPresentacion(idPresentacion);

                if (resultado > 0)
                {
                    Response.Redirect("Presentaciones.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la presentacion", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}