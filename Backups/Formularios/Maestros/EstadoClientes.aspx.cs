﻿using System;
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
    public partial class EstadoClientes : Page
    {
        Mensaje m = new Mensaje();
        ControladorCliente controladorCliente = new ControladorCliente();
        int accion;
        int idEstado;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idEstado = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

                VerificarLogin();
                if (accion == 2)
                {
                    CargarEstado();
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

            ObtenerEstados();

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

        public void ObtenerEstados()
        {
            try
            {
                var estados = controladorCliente.ObtenerTodosEstados();

                if (estados.Count > 0)
                {

                    foreach (var item in estados)
                    {
                        CargarEstadosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarEstado()
        {
            try
            {
                var estado = controladorCliente.ObtenerEstadoId(this.idEstado);

                if (estado != null)
                {
                    hiddenEditar.Value = estado.id.ToString();
                    txtDescripcionEstado.Text = estado.descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarEstadosPH(Clientes_Estados estado)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = estado.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = estado.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = estado.descripcion;
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
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + estado.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarEstado);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + estado.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + estado.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phEstados.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarEstado(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("EstadoClientes.aspx?a=2&i=" + id[1]);
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
                    EditarEstado();
                }
                else
                {
                    GuardarEstado();
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
                //txtDescripcionEstado.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarEstado()
        {
            try
            {
                Clientes_Estados estado = new Clientes_Estados();

                estado.descripcion = txtDescripcionEstado.Text;
                estado.estado = 1;

                int resultado = controladorCliente.AgregarEstado(estado);

                if (resultado > 0)
                {
                    Response.Redirect("EstadoClientes.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el estado", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarEstado()
        {
            try
            {
                Clientes_Estados estado = new Clientes_Estados();
                estado.id = this.idEstado;
                estado.descripcion = txtDescripcionEstado.Text;
                estado.estado = 1;

                int resultado = controladorCliente.EditarEstado(estado);

                if (resultado > 0)
                {
                    Response.Redirect("EstadoClientes.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar el estado", "warning");
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
                int idEstado = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorCliente.EliminarEstado(idEstado);

                if (resultado > 0)
                {
                    Response.Redirect("EstadoClientes.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el estado", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}