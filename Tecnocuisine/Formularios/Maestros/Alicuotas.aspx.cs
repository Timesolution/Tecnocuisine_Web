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
    public partial class Alicuotas : Page
    {
        Mensaje m = new Mensaje();
        ControladorIVA controladorIVA = new ControladorIVA();
        int accion;
        int idAlicuota;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

    
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idAlicuota = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
               
                if (accion == 2)
                {
                    CargarAlicuota();
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

            ObtenerAlicuotas();

        }


        public void ObtenerAlicuotas()
        {
            try
            {
                var unidades = controladorIVA.ObtenerTodosAlicuotas_IVA();

                if (unidades.Count > 0)
                {

                    foreach (var item in unidades)
                    {
                        CargarAlicuotasPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarAlicuota()
        {
            try
            {
                var alicuota = controladorIVA.ObtenerAlicuotaId(this.idAlicuota);
                if (alicuota != null)
                {
                    hiddenEditar.Value = alicuota.id.ToString();
                    txtPorcentajeAlicuota.Text = alicuota.porcentaje.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarAlicuotasPH(Tecnocuisine_API.Entitys.Alicuotas_IVA unidad)
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
                celNumero.Width = Unit.Percentage(5);
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celPorcentaje = new TableCell();
                celPorcentaje.Text = unidad.porcentaje + "%";
                celPorcentaje.VerticalAlign = VerticalAlign.Middle;
                celPorcentaje.HorizontalAlign = HorizontalAlign.Right;
                celPorcentaje.Width = Unit.Percentage(5);
                celPorcentaje.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPorcentaje);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary btn-xs";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + unidad.id + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarAlicuota);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + unidad.id;
                btnEliminar.CssClass = "btn btn-danger btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + unidad.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phAlicuotas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarAlicuota(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Alicuotas.aspx?a=2&i=" + id[1]);
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
                    EditarAlicuota();
                }
                else
                {
                    GuardarAlicuota();
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
                txtPorcentajeAlicuota.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarAlicuota()
        {
            try
            {
                Tecnocuisine_API.Entitys.Alicuotas_IVA unidad = new Tecnocuisine_API.Entitys.Alicuotas_IVA();

                unidad.porcentaje = Convert.ToDecimal(txtPorcentajeAlicuota.Text);
                unidad.estado = 1;

                int resultado = controladorIVA.AgregarAlicuota(unidad);

                if (resultado > 0)
                {
                    Response.Redirect("Alicuotas.aspx?m=1");
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

        public void EditarAlicuota()
        {
            try
            {
                Tecnocuisine_API.Entitys.Alicuotas_IVA unidad = new Tecnocuisine_API.Entitys.Alicuotas_IVA();

                unidad.id = this.idAlicuota;
                unidad.porcentaje = Convert.ToDecimal(txtPorcentajeAlicuota.Text);
                unidad.estado = 1;

                int resultado = controladorIVA.EditarAlicuota(unidad);

                if (resultado > 0)
                {
                    Response.Redirect("Alicuotas.aspx?m=2");

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
                int idAlicuota = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorIVA.EliminarAlicuota(idAlicuota);

                if (resultado > 0)
                {
                    Response.Redirect("Alicuotas.aspx?m=3");
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