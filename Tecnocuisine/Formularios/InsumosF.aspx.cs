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
    public partial class InsumosF : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        int accion;
        int idInsumo;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idInsumo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
                if (accion == 2)
                {
                    CargarInsumo();
                }
            }

            ObtenerInsumos();
        }


        public void ObtenerInsumos()
        {
            try
            {
                var insumos = controladorInsumo.ObtenerTodosInsumos();

                if (insumos.Count > 0)
                {

                    foreach (var item in insumos)
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
                this.phInsumos.Controls.Clear();

                if (insumo != null)
                {
                    txtDescripcionInsumo.Text = insumo.Descripcion;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumosPH(Insumos insumo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = insumo.id_insumo.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = insumo.id_insumo.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = insumo.Descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-primary";
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + insumo.id_insumo + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarInsumo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);
                tr.Cells.Add(celAccion);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + insumo.id_insumo;
                btnEliminar.CssClass = "btn btn-danger";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + insumo.id_insumo + ");";

                celAccion.Controls.Add(btnEliminar);
                celAccion.Width = Unit.Percentage(25);
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

                Response.Redirect("InsumosF.aspx?a=2&i=" + id[1]);
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

        #region ABM
        public void GuardarInsumo()
        {
            try
            {
                Insumos insumo = new Insumos();

                insumo.Descripcion = txtDescripcionInsumo.Text;
                insumo.Estado = 1;

                int resultado = controladorInsumo.AgregarInsumo(insumo);

                if (resultado > 0)
                {
                    this.m.ShowToastr(this.Page, "Insumo guardado con Exito!", "Exito");
                    ObtenerInsumos();
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el insumo", "warning");
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
                Insumos insumo = new Insumos();
                insumo.id_insumo = this.idInsumo;
                insumo.Descripcion = txtDescripcionInsumo.Text;
                insumo.Estado = 1;

                int resultado = controladorInsumo.EditarInsumo(insumo);

                if (resultado > 0)
                {
                    this.m.ShowToastr(this.Page, "Insumo editado con Exito!", "Exito");
                    Response.Redirect("InsumosF.aspx");

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
                int idInsumo = Convert.ToInt32(this.txtMovimiento.Text);
                int resultado = controladorInsumo.EliminarInsumo(idInsumo);

                if (resultado > 0)
                {
                    this.m.ShowToastr(this.Page, "Insumo Eliminado con Exito!", "Exito");
                    ObtenerInsumos();
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