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
    public partial class SectorProductivo : Page
    {
        Mensaje m = new Mensaje();
        ControladorSectorProductivo controladorSector = new ControladorSectorProductivo();
        int accion;
        int idSector;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
    
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idSector = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
        
                if (accion == 2)
                {
                    CargarSector();
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

            ObtenerSectores();

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


        public void ObtenerSectores()
        {
            try
            {
                var Sectores = controladorSector.ObtenerTodosSectorProductivo();

                if (Sectores.Count > 0)
                {

                    foreach (var item in Sectores)
                    {
                        CargarSectoresPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarSector()
        {
            try
            {
                var Sector = controladorSector.ObtenerSectorProductivoId(this.idSector);
                if (Sector != null)
                {
                    hiddenEditar.Value = Sector.id.ToString();
                    txtDescripcionSector.Text = Sector.descripcion;
                    txtCodigoSector.Text = Sector.codigo.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                }


            }
            catch (Exception ex)
            {

            }
        }

        public void CargarSectoresPH(Tecnocuisine_API.Entitys.SectorProductivo Sector)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = Sector.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Sector.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celCodigo = new TableCell();
                celCodigo.Text = Sector.codigo.ToString();
                celCodigo.VerticalAlign = VerticalAlign.Middle;
                celCodigo.HorizontalAlign = HorizontalAlign.Right;
                celCodigo.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCodigo);

                TableCell celNombre = new TableCell();
                celNombre.Text = Sector.descripcion;
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
                btnDetalles.ID = "btnSelec_" + Sector.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarSector);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + Sector.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + Sector.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phSectores.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarSector(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("SectorProductivo.aspx?a=2&i=" + id[1]);
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
                    EditarSector();
                }
                else
                {
                    GuardarSector();
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
                txtDescripcionSector.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarSector()
        {
            try
            {
                Tecnocuisine_API.Entitys.SectorProductivo Sector = new Tecnocuisine_API.Entitys.SectorProductivo();

                Sector.descripcion = txtDescripcionSector.Text;
                Sector.codigo = txtCodigoSector.Text;
                Sector.estado = 1;

                int resultado = controladorSector.AgregarSectorProductivo(Sector);

                if (resultado > 0)
                {
                    Response.Redirect("SectorProductivo.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar la Sector", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarSector()
        {
            try
            {
                Tecnocuisine_API.Entitys.SectorProductivo Sector = new Tecnocuisine_API.Entitys.SectorProductivo();

                Sector.id = this.idSector;
                Sector.descripcion = txtDescripcionSector.Text;
                Sector.codigo = txtCodigoSector.Text;
                Sector.estado = 1;


                int resultado = controladorSector.EditarSectorProductivo(Sector);

                if (resultado > 0)
                {
                    Response.Redirect("Sectores.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la Sector", "warning");
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
                int idSector = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorSector.EliminarSectorProductivo(idSector);

                if (resultado > 0)
                {
                    Response.Redirect("Sectores.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la Sector", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}