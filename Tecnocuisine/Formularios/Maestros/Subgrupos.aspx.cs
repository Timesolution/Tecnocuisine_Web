using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;


namespace Tecnocuisine
{
    public partial class Subgrupos : Page
    {
        Mensaje m = new Mensaje();
        ControladorGrupo controladorGrupo = new ControladorGrupo();
        int accion;
        int idSubgrupo;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

         
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idSubgrupo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
       
                if (accion == 2)
                {
                    CargarSubgrupo();
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

            ObtenerSubgrupos();

        }


        public void ObtenerSubgrupos()
        {
            try
            {
                var grupos = controladorGrupo.ObtenerTodosArticulos_SubGrupos();

                if (grupos.Count > 0)
                {

                    foreach (var item in grupos)
                    {
                        CargarSubgruposPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarSubgrupo()
        {
            try
            {
                var grupo = controladorGrupo.ObtenerSubgrupoId(this.idSubgrupo);
                if (grupo != null)
                {
                    hiddenEditar.Value = grupo.id.ToString();
                    txtDescripcionSubGrupo.Text = grupo.descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarSubgruposPH(Tecnocuisine_API.Entitys.Articulos_SubGrupos grupo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = grupo.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = grupo.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = grupo.descripcion;
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
                btnDetalles.ID = "btnSelec_" + grupo.id + "_";
                btnDetalles.Text = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarSubgrupo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + grupo.id;
                btnEliminar.CssClass = "btn btn-danger btn-xs";
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + grupo.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phSubgrupos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarSubgrupo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Subgrupos.aspx?a=2&i=" + id[1]);
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
                    EditarSubgrupo();
                }
                else
                {
                    GuardarSubgrupo();
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
                txtDescripcionSubGrupo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarSubgrupo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Articulos_SubGrupos grupo = new Tecnocuisine_API.Entitys.Articulos_SubGrupos();

                grupo.descripcion = txtDescripcionSubGrupo.Text;
                grupo.estado = 1;

                int resultado = controladorGrupo.AgregarSubgrupo(grupo);

                if (resultado > 0)
                {
                    Response.Redirect("Subgrupos.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el grupo", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarSubgrupo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Articulos_SubGrupos grupo = new Tecnocuisine_API.Entitys.Articulos_SubGrupos();

                grupo.id = this.idSubgrupo;
                grupo.descripcion = txtDescripcionSubGrupo.Text;
                grupo.estado = 1;

                int resultado = controladorGrupo.EditarSubgrupo(grupo);

                if (resultado > 0)
                {
                    Response.Redirect("Subgrupos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la grupo", "warning");
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
                int idSubgrupo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorGrupo.EliminarSubgrupo(idSubgrupo);

                if (resultado > 0)
                {
                    Response.Redirect("Subgrupos.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el grupo", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}