﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Administrador;
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
                string toastrValue = Session["toastrSectorProductivo"] as string;
                if (accion == 2)
                {
                    CargarSector();
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
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                    Session["toastrSectorProductivo"] = null;
                }
                if (toastrValue == "2")
                {
                    this.m.ShowToastr(this.Page, "Sector Productivo eliminado con exito!", "Exito");
                    Session["toastrSectorProductivo"] = null;
                }

                if (toastrValue == "3")
                {
                    this.m.ShowToastr(this.Page, "Sector Productivo modificado con exito!", "Exito");
                    Session["toastrSectorProductivo"] = null;
                }
                //Session["toastr"] = "1";
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
                btnDetalles.ID = "btnSelec_" + Sector.id + "_";
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.Attributes.Add("href", "#modalAgregar");
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar'></i></span>";
                btnDetalles.Attributes.Add("onclick", "openModalEditar('" + Sector.id + "');");
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
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash - o' title='Eliminar'></i></span>";
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

                int existe = controladorSector.validarSiExisteCodigo(Sector.codigo);
                if (existe == 0)
                {
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
                else
                {
                    this.m.ShowToastr(this.Page, "Ya existe un sector con ese codigo", "warning");

                }
            }
            catch (Exception ex)
            {

            }

        }


        [WebMethod]
        public static int GuardarSector(string codigoSector, string descripcionSector)
        {
            try
            {
                ControladorSectorProductivo controladorSector = new ControladorSectorProductivo();
                Tecnocuisine_API.Entitys.SectorProductivo Sector = new Tecnocuisine_API.Entitys.SectorProductivo();

                Sector.codigo = codigoSector;
                Sector.descripcion = descripcionSector;
                Sector.estado = 1;

                int existeCodigo = controladorSector.validarSiExisteCodigo(Sector.codigo);
                int existeNombre = controladorSector.validarSiExisteNombre(Sector);


                if (existeCodigo == 0)
                {

                    if (existeNombre == 0)
                    {
                        int resultado = controladorSector.AgregarSectorProductivo(Sector);

                        if (resultado > 0)
                        {
                            HttpContext.Current.Session["toastrSectorProductivo"] = "1";
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
                else
                {
                    return 0;

                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        [WebMethod]
        public static int EditarSector(string codigoSector, string descripcionSector, int idSector)
        {
            try
            {
                ControladorSectorProductivo controladorSector = new ControladorSectorProductivo();
                Tecnocuisine_API.Entitys.SectorProductivo Sector = new Tecnocuisine_API.Entitys.SectorProductivo();

                Sector.id = idSector;       
                Sector.codigo = codigoSector;
                Sector.descripcion = descripcionSector;
                Sector.estado = 1;

                int existeCodigo = controladorSector.validarSiExisteCodigo(Sector);
                int existeNombre = controladorSector.validarSiExisteNombre(Sector);


                if (existeCodigo == 0)
                {

                    if (existeNombre == 0)
                    {
                        int resultado = controladorSector.EditarSectorProductivo(Sector);

                        if (resultado > 0)
                        {
                            HttpContext.Current.Session["toastrSectorProductivo"] = "3";
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
                else
                {
                    return 0;

                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        [WebMethod]
        public static string precargarCampos(int idSector)
        {
            try
            {
                ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
                var sector = cSectorProductivo.ObtenerSectorProductivoId(idSector);

                string sec = "[";

                sec += "{" +
                        "\"Id\":\"" + sector.id + "\"," +
                        "\"Codigo\":\"" + sector.codigo + "\"," +
                        "\"Descripcion\":\"" + sector.descripcion + "\"" +
                        "},";

                sec = sec.Remove(sec.Length - 1) + "]";

                //if (sector.Count == 0)
                //{
                //    sec = "[]";
                //}
                return sec;
            }
            catch (Exception ex)
            {
                return null;
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

                int existe = controladorSector.validarSiExisteCodigo(Sector);
                if (existe == 0)
                {

                    int resultado = controladorSector.EditarSectorProductivo(Sector);

                    if (resultado != 0)
                    {
                        Response.Redirect("SectorProductivo.aspx?m=2");

                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "No se pudo editar la Sector", "warning");
                    }

                }
                else
                {
                    this.m.ShowToastr(this.Page, "Ya existe un sector con ese codigo", "warning");

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
                    HttpContext.Current.Session["toastrSectorProductivo"] = "2";
                    Response.Redirect("SectorProductivo.aspx");
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


        [WebMethod]
        public static string CargarSectoresProductivo()
        {
            try
            {
                ControladorSectorProductivo controladorSectorProductivo = new ControladorSectorProductivo();
                var sectores = controladorSectorProductivo.ObtenerTodosSectorProductivo();

                string sec = "[";
                foreach (var item in sectores)
                {
                    sec += "{" +
                        "\"Id\":\"" + item.id + "\"," +
                        "\"Codigo\":\"" + item.codigo + "\"," +
                        "\"Descripcion\":\"" + item.descripcion + "\"" +
                        "},";

                }
                sec = sec.Remove(sec.Length - 1) + "]";

                if (sectores.Count == 0)
                {
                    sec = "[]";
                }

                return sec;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion
    }
}