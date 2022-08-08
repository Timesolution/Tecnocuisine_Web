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
    public partial class Vendedores : Page
    {
        Mensaje m = new Mensaje();
        ControladorVendedor controladorVendedor = new ControladorVendedor();
        int accion;
        int idVendedor;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idVendedor = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

                if (accion == 2)
                {
                    CargarVendedor();
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

            ObtenerVendedores();
          

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

        public void ObtenerVendedores()
        {
            try
            {
                var vendedores = controladorVendedor.ObtenerTodosVendedores();

                if (vendedores.Count > 0)
                {

                    foreach (var item in vendedores)
                    {
                        CargarVendedoresPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarVendedor()
        {
            try
            {
                var vendedor = controladorVendedor.ObtenerVendedorId(this.idVendedor);
                if (vendedor != null)
                {
                    hiddenEditar.Value = vendedor.id.ToString();
                    txtNombre.Text = vendedor.nombre;
                    txtApellido.Text = vendedor.apellido;
                    txtDNI.Text = vendedor.documento.ToString();
                    txtDatos.Text = vendedor.datos;
                    txtLegajo.Text = vendedor.legajo;

                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarVendedoresPH(Tecnocuisine_API.Entitys.Vendedores vendedor)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = vendedor.id.ToString();

                //Celdas
                TableCell celLegajo = new TableCell();
                celLegajo.Text = vendedor.legajo.ToString();
                celLegajo.VerticalAlign = VerticalAlign.Middle;
                celLegajo.HorizontalAlign = HorizontalAlign.Right;
                celLegajo.Width = Unit.Percentage(5);
                celLegajo.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celLegajo);

                TableCell celNombre = new TableCell();
                celNombre.Text = vendedor.nombre + " " + vendedor.apellido;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Right;
                celNombre.Width = Unit.Percentage(5);
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);


                TableCell celDocumento = new TableCell();
                celDocumento.Text = vendedor.documento.ToString();
                celDocumento.VerticalAlign = VerticalAlign.Middle;
                celDocumento.HorizontalAlign = HorizontalAlign.Right;
                celDocumento.Width = Unit.Percentage(5);
                celDocumento.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDocumento);

                TableCell celDatos = new TableCell();
                celDatos.Text = vendedor.datos;
                celDatos.VerticalAlign = VerticalAlign.Middle;
                celDatos.HorizontalAlign = HorizontalAlign.Right;
                celDatos.Width = Unit.Percentage(5);
                celDatos.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDatos);

       
                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + vendedor.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarVendedor);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + vendedor.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + vendedor.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phVendedores.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarVendedor(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Vendedores.aspx?a=2&i=" + id[1]);
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
                    EditarVendedor();
                }
                else
                {
                    GuardarVendedor();
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
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarVendedor()
        {
            try
            {
                Tecnocuisine_API.Entitys.Vendedores vendedor = new Tecnocuisine_API.Entitys.Vendedores();
                vendedor.legajo = txtLegajo.Text;
                vendedor.nombre = txtNombre.Text;
                vendedor.apellido = txtApellido.Text;
                vendedor.datos = txtDatos.Text;
                vendedor.documento = Convert.ToInt32(txtDNI.Text);
                vendedor.estado = 1;

                int resultado = controladorVendedor.AgregarVendedor(vendedor);

                if (resultado > 0)
                {
                    Response.Redirect("Vendedores.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el vendedor", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarVendedor()
        {
            try
            {
                Tecnocuisine_API.Entitys.Vendedores vendedor = new Tecnocuisine_API.Entitys.Vendedores();

                vendedor.id = this.idVendedor;
                vendedor.legajo = txtLegajo.Text;
                vendedor.nombre = txtNombre.Text;
                vendedor.apellido = txtApellido.Text;
                vendedor.datos = txtDatos.Text;
                vendedor.documento = Convert.ToInt32(txtDNI.Text);
                vendedor.estado = 1;


                int resultado = controladorVendedor.EditarVendedor(vendedor);

                if (resultado > 0)
                {
                    Response.Redirect("Vendedores.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la vendedor", "warning");
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
                int idVendedor = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorVendedor.EliminarVendedor(idVendedor);

                if (resultado > 0)
                {
                    Response.Redirect("Vendedores.aspx?m=3");
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