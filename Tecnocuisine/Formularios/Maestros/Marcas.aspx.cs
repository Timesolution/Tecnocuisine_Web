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
    public partial class Marcas : Page
    {
        Mensaje m = new Mensaje();
        ControladorMarca controladorMarca = new ControladorMarca();
        int accion;
        int idMarca;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

         
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idMarca = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
       
                if (accion == 2)
                {
                    CargarUnidad();
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

            ObtenerMarcas();

        }


        public void ObtenerMarcas()
        {
            try
            {
                var marcas = controladorMarca.ObtenerTodasMarcas();

                if (marcas.Count > 0)
                {

                    foreach (var item in marcas)
                    {
                        CargarMarcasPH(item);

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
                var marca = controladorMarca.ObtenerMarcaId(this.idMarca);
                if (marca != null)
                {
                    hiddenEditar.Value = marca.id.ToString();
                    txtDescripcionUnidad.Text = marca.descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarMarcasPH(Tecnocuisine_API.Entitys.Articulos_Marcas marca)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = marca.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = marca.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = marca.descripcion;
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
                btnDetalles.ID = "btnSelec_" + marca.id + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarUnidad);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + marca.id;
                btnEliminar.CssClass = "btn btn-danger btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + marca.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phMarcas.Controls.Add(tr);

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

                Response.Redirect("Marcas.aspx?a=2&i=" + id[1]);
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
                Tecnocuisine_API.Entitys.Articulos_Marcas marca = new Tecnocuisine_API.Entitys.Articulos_Marcas();

                marca.descripcion = txtDescripcionUnidad.Text;
                marca.estado = 1;

                int resultado = controladorMarca.AgregarMarca(marca);

                if (resultado > 0)
                {
                    Response.Redirect("Marcas.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el marca", "warning");
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
                Tecnocuisine_API.Entitys.Articulos_Marcas marca = new Tecnocuisine_API.Entitys.Articulos_Marcas();

                marca.id = this.idMarca;
                marca.descripcion = txtDescripcionUnidad.Text;
                marca.estado = 1;

                int resultado = controladorMarca.EditarMarca(marca);

                if (resultado > 0)
                {
                    Response.Redirect("Marcas.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la marca", "warning");
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
                int resultado = controladorMarca.EliminarMarca(idUnidad);

                if (resultado > 0)
                {
                    Response.Redirect("Marcas.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el marca", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}