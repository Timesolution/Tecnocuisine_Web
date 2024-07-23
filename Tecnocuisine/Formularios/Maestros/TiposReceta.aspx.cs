using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class TiposReceta : System.Web.UI.Page
    {
        ControladorTiposDeReceta cTiposDeReceta = new ControladorTiposDeReceta();
        Mensaje m = new Mensaje();
        int accion;
        int Mensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            //this.idInsumo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

                string toastrValue = Session["toastrTipos"] as string;
                if (accion == 2)
                {
                    //CargarInsumo();
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

                if (toastrValue == "1")
                {
                    this.m.ShowToastr(this.Page, "Tipo de receta agregado con exito!", "Exito");
                    Session["toastrTipos"] = null;
                }

                //if (toastrValue == "2")
                //{
                //    this.m.ShowToastr(this.Page, "Tipo atributo modificado con exito!", "Exito");
                //    Session["toastrTipos"] = null;
                //}

                if (toastrValue == "3")
                {
                    this.m.ShowToastr(this.Page, "Tipo de receta modificado con exito!", "Exito");
                    Session["toastrTipos"] = null;
                }

                if (toastrValue == "4")
                {
                    this.m.ShowToastr(this.Page, "Tipo de receta eliminado con exito!", "Exito");
                    Session["toastrTipos"] = null;
                }

            }

            CargarListado();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.hiddenEditar.Value != "")
                //{
                //    EditarInsumo();
                //}
                //else
                //{
                //    GuardarInsumo();
                //}

                Agregar();

            }
            catch (Exception ex)
            {

            }
        }

        public void Agregar()
        {
            try
            {
                TiposDeReceta tipo = new TiposDeReceta();
                tipo.tipo = txtDescripcionTipo.Text;
                tipo.estado = true;

                if (!cTiposDeReceta.Existe(tipo.tipo)) // Si no existe, lo crea
                {
                    int resultado = cTiposDeReceta.AgregarTipoDeReceta(tipo);
                    if (resultado > 0)
                    {
                        Session["toastrTipos"] = "1";
                        Response.Redirect("TiposReceta.aspx");
                    }
                    else
                    {
                        this.m.ShowToastrError(this.Page, "No se pudo agregar el tipo de receta", "Error");
                    }
                }
                else
                {
                    this.m.ShowToastrError(this.Page, "Ya existe un tipo de receta con esa descripcion", "Error");
                }

            }
            catch (Exception ex)
            {
                this.m.ShowToastrError(this.Page, "No se pudo agregar el insumo", "warning");
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        public void CargarListado()
        {
            try
            {
                var tipos = cTiposDeReceta.ObtenerTodosTiposDeReceta();

                if (tipos.Count > 0)
                {
                    foreach (var tipo in tipos)
                    {
                        CargarPH(tipo);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CargarPH(TiposDeReceta tipo)
        {
            try
            {
                //fila
                TableRow tr = new TableRow();
                tr.ID = tipo.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = tipo.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = tipo.tipo;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Width = Unit.Percentage(40);
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.Attributes.Add("data-toggle", "modal");
                btnDetalles.Attributes.Add("title", "Editar");
                btnDetalles.Attributes.Add("href", "#openModalEditar");
                btnDetalles.ID = "btnSelec_" + tipo.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.OnClientClick = "openModalEditar(" + tipo.id + ");";
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + tipo.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("title", "Eliminar");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + tipo.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phTipos.Controls.Add(tr);
            }
            catch (Exception ex)
            {
            }
        }

        [WebMethod]
        public static string EditarTipo(int idTipo, string Descripcion)
        {
            try
            {
                TiposDeReceta tipo = new TiposDeReceta();
                tipo.id = idTipo;
                tipo.tipo = Descripcion;
                tipo.estado = true;

                int r = new ControladorTiposDeReceta().EditarTipoDeReceta(tipo);
                if (r > 0)
                {
                    HttpContext.Current.Session["toastrTipos"] = "3";
                    return "3";
                }
                else
                {
                    return "4";
                }
            }
            catch
            {
                return "-1";
            }
        }

        [WebMethod]
        public static string GetTipoById(int idTipo)
        {
            try
            {
                TiposDeReceta tipo = new ControladorTiposDeReceta().ObtenerTipoDeRecetaById(idTipo);

                return 
                    tipo.id.ToString() + "," +
                    tipo.tipo.ToString() + "," +
                    tipo.estado.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idTipo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = cTiposDeReceta.EliminarTipoDeReceta(idTipo);

                if (resultado > 0)
                {
                    Session["toastrTipos"] = "4";
                    Response.Redirect("TiposReceta.aspx");
                }
                else
                {
                    this.m.ShowToastrError(this.Page, "No se pudo eliminar el tipo de receta", "Error");
                }
            }
            catch (Exception ex)
            {
                this.m.ShowToastrError(this.Page, "No se pudo eliminar el tipo de receta", "Error");
            }
        }
    }
}