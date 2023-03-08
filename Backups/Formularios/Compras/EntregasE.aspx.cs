﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class EntregasE : System.Web.UI.Page
    {
        ControladorEntregas controladorEntregas = new ControladorEntregas();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        ControladorSectorProductivo ControladorSector = new ControladorSectorProductivo();
        int Mensaje;
        int accion;
        int id;
            Mensaje m = new Mensaje();
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
                CargarEntregas();
            if (!IsPostBack)
            {
            }
            if (Mensaje == 1)
            {
                m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }
        }

        private void CargarEntregas()
        {
            try
            {
                var listaEntregas = controladorEntregas.ObtenerEntregasAll();
                foreach(var entrega in listaEntregas)
                {
                    CargarEntregasPH(entrega);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarEntregasPH(Tecnocuisine_API.Entitys.Entregas entrega)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = entrega.id.ToString();

                Tecnocuisine_API.Entitys.Proveedores p = ControladorProveedores.ObtenerProveedorByID(entrega.idProveedor);
                Tecnocuisine_API.Entitys.SectorProductivo s = ControladorSector.ObtenerSectorProductivoId(entrega.idSector);

                TableCell celFechaEntrega = new TableCell();
                celFechaEntrega.Text = "<span> "+ entrega.fechaEntrega.ToString("yyyyMMdd")+"</span>"+ entrega.fechaEntrega.ToString("dd/MM/yyyy");
                celFechaEntrega.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaEntrega);

                TableCell celAlias = new TableCell();
                celAlias.Text = p.Alias;
                celAlias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celAlias);

                TableCell celSectorProd = new TableCell();
                celSectorProd.Text = s.descripcion;
                celSectorProd.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celSectorProd);

                TableCell celObservaciones = new TableCell();
                celObservaciones.Text = entrega.Observaciones;
                celObservaciones.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celObservaciones);

                

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = "BtnEdit_"+ entrega.id.ToString()+"_";

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Click += new EventHandler(this.editarEntrega);
                celAction.Controls.Add(btnEditar);


                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + entrega.id;


                btnEliminar.OnClientClick = "abrirdialog(" + entrega.id + ");";
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.Text = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                //btnEliminar.Click += new EventHandler(this.EliminarProveedor);
                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phEntregas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void editarEntrega(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');
                Response.Redirect("Entregas.aspx?i=" + id[1] + "&a=2");
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idEntrega = Convert.ToInt32(this.hiddenID.Value);
                Tecnocuisine_API.Entitys.Entregas entregaEdit = controladorEntregas.ObtenerEntregasByID(idEntrega);
                entregaEdit.Estado = 0;
                int resultado = controladorEntregas.EditarEntregas(entregaEdit);

                if (resultado > 0)
                {
                    Response.Redirect("EntregasE.aspx?m=1");
                }
                else
                {
                   m.ShowToastr(this.Page, "No se pudo eliminar Proveedor", "warning");
                }
            }
            catch (Exception ex)
            {
                m.ShowToastr(this.Page, "No se pudo eliminar Proveedor", "warning");
            }
        }

    }
}