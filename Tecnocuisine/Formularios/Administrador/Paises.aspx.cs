using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Tecnocuisine_API.Controladores;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using System.Web.Services;
using Tecnocuisine_API.Modelos;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Paises : System.Web.UI.Page
    {
        ControladorPaises ControladorPaises = new ControladorPaises();
        Mensaje m = new Mensaje();
        int Mensaje;
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            VerificarLogin();
            if (!IsPostBack)
            {
                string toastrValue = Session["toastrPaises"] as string;

                if (toastrValue == "1")
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                    Session["toastrPaises"] = null;
                }

                if (toastrValue == "2")
                {
                    this.m.ShowToastrWarning(this.Page, "Ya existe un pais con ese nombre!", "Atencion");
                    Session["toastrPaises"] = null;
                }

                if (toastrValue == "3")
                {
                    this.m.ShowToastr(this.Page, "Pais eliminado con exito!", "Exito");
                    Session["toastrPaises"] = null;
                }


                if (toastrValue == "4")
                {
                    this.m.ShowToastr(this.Page, "Pais editado con exito!", "Exito");
                    Session["toastrPaises"] = null;
                }
            }
            CargarPaises();
            if (Mensaje == 1)
            {
                this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }



        }

        private void CargarPaisEditar()
        {
            try
            {
                btnAgregar.Text = "Modificar";
                var pais = ControladorPaises.ObtenerPaisById(id);
                txtPais.Text = pais.Pais;
                txtTipoDoc.Text = pais.TipoDocumento;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void editarPais(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Paises.aspx?m=0&a=2&i=" + id[0]);
                //return;
            }
            catch (Exception Ex)
            {

            }
        }
        private void CargarPaises()
        {
            try
            {
                var Lista = ControladorPaises.ObtenerPaisesAll();
                foreach (var pais in Lista)
                {
                    CargarPHPaises(pais);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
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
                int valor = 1;
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

        private void CargarPHPaises(Tecnocuisine_API.Entitys.paises p)
        {
            try
            {
                TableRow tr = new TableRow();


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = p.Pais;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);


                TableCell celTipoDoc = new TableCell();
                celTipoDoc.Text = p.TipoDocumento;
                celTipoDoc.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoDoc);

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                celAction.Style.Add("text-align", "left"); 
                btnEditar.ID = p.id.ToString();
                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("href", "#modalAgregar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar pais'></i></span>";
                btnEditar.Attributes.Add("onclick", "openModalEditar('" + p.id + "');");
                celAction.Controls.Add(btnEditar);


                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + p.id;
                btnEliminar.OnClientClick = "abrirdialog(" + p.id + ");";
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Text = "<span><i style='color:red;' class='fa fa-trash' title='Eliminar pais'></i></span>";
                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phPaises.Controls.Add(tr);


            }
            catch (Exception ex)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", mje.mensajeBoxError("Error cargando Procedencia en la lista. " + ex.Message));
            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idPais = Convert.ToInt32(this.hiddenID.Value);
                Tecnocuisine_API.Entitys.paises p = ControladorPaises.ObtenerPaisById(idPais);
                p.estado = 0;
                int resultado = ControladorPaises.EditarPais(p);

                if (resultado > 0)
                {
                    Session["toastrPaises"] = "3";
                    Response.Redirect("Paises.aspx");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
                }
            }
            catch (Exception ex)
            {
                this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (accion == 2)
                {
                    //editar
                    var pais = ControladorPaises.ObtenerPaisById(id);
                    pais.Pais = txtPais.Text;
                    pais.TipoDocumento = txtTipoDoc.Text;

                    int i = ControladorPaises.EditarPais(pais);
                    if (i > 0)
                    {
                        Response.Redirect("Paises.aspx?m=1");
                    }
                }
                else //guardar nuevo
                {
                    Tecnocuisine_API.Entitys.paises nuevoPais = new Tecnocuisine_API.Entitys.paises();

                    nuevoPais.Pais = txtPais.Text;
                    nuevoPais.TipoDocumento = txtTipoDoc.Text;
                    nuevoPais.estado = 1;

                    int existeNombre = ControladorPaises.validarSiExisteNombre(nuevoPais);

                    if (existeNombre == 0)
                    {
                        int i = ControladorPaises.AgregarNuevoPais(nuevoPais);

                        if (i > 0)
                        {
                            Session["toastrPaises"] = "4";
                            Response.Redirect("Paises.aspx");
                        }

                    }
                    else
                    {
                        Session["toastrPaises"] = "2";
                        Response.Redirect("Paises.aspx");

                    }

                }


            }
            catch (Exception ex)
            {


            }
        }


        [WebMethod]
        public static int EditarPais(string pais, string tipoDocumento, string idPais)
        {
            try
            {
                ControladorPaises controladorPaises = new ControladorPaises();


                paises Pais = new paises();
                Pais.id = Convert.ToInt32(idPais);
                Pais.Pais = pais;
                Pais.TipoDocumento = tipoDocumento;
                Pais.estado = 1;

                int existeNombre = controladorPaises.validarSiExisteNombre(Pais);

                //int existeCodigo = controladorSector.validarSiExisteCodigo(Sector);
                //int existeNombre = controladorSector.validarSiExisteNombre(Sector);




                if (existeNombre == 0)
                {
                    int resultado = controladorPaises.EditarPais(Pais);

                    if (resultado > 0)
                    {
                        HttpContext.Current.Session["toastrPaises"] = "4";
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return -2;
                }



            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        [WebMethod]
        public static string precargarCampos(int idPais)
        {
            try
            {
                ControladorPaises controladorPaises = new ControladorPaises();
                var pais = controladorPaises.ObtenerPaisById(idPais);

                string p = "[";

                p += "{" +
                        "\"Id\":\"" + pais.id + "\"," +
                        "\"Pais\":\"" + pais.Pais + "\"," +
                        "\"TipoDocumento\":\"" + pais.TipoDocumento + "\"" +
                        "},";

                p = p.Remove(p.Length - 1) + "]";

                //if (sector.Count == 0)
                //{
                //    sec = "[]";
                //}
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}