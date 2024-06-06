using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
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
            VerificarLogin();
         
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idSubgrupo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
                CargarGrupos();
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

        private void CargarGrupos()
        {
            try
            {
                this.ListGrupo.DataSource = controladorGrupo.ObtenerTodosArticulos_Grupos();
                this.ListGrupo.DataValueField = "id";
                this.ListGrupo.DataTextField = "descripcion";
                this.ListGrupo.DataBind();
                ListGrupo.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
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
                    ListGrupo.SelectedValue = grupo.Articulos_Grupos.id.ToString();
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


                TableCell celGrupo = new TableCell();
                celGrupo.Text = grupo.Articulos_Grupos.descripcion;
                celGrupo.VerticalAlign = VerticalAlign.Middle;
                celGrupo.HorizontalAlign = HorizontalAlign.Left;
                celGrupo.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celGrupo);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + grupo.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Attributes.Add("title", "Editar");
                btnDetalles.Click += new EventHandler(this.editarSubgrupo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + grupo.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash-o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + grupo.id + ");";
                btnEliminar.Attributes.Add("title", "Eliminar"); 
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
                grupo.grupo = Convert.ToInt32(ListGrupo.SelectedValue);
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
                grupo.grupo = Convert.ToInt32(ListGrupo.SelectedValue);
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
        [WebMethod]
        public static string GetSubgrupos(int id)
        {
            ControladorGrupo controlador = new ControladorGrupo();
            string tiposAtributos = controlador.obtenerSubgrupos(id);

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(tiposAtributos);
            return resultadoJSON;
        }
    }

}
