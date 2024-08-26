﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class RecetaEvolucion : System.Web.UI.Page
    {
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorEntregas controladorEntregas = new ControladorEntregas();
        int idReceta;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            if (Request.QueryString["id"] == null)
                Response.Redirect("Recetas.aspx");

            idReceta = Convert.ToInt32(Request.QueryString["id"]);

            var receta = controladorReceta.ObtenerRecetaId(idReceta);

            lblDescripcion.Text = receta.descripcion;

            CrearTablaIngredientes();
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

        private void CrearTablaIngredientes()
        {
            CargarFilasProductos();
            //CargarFilasRecetas();

            ////ingredientes / Recetas
            //obtenerRecetasbyIdReceta(idReceta);
        }

        private void CargarFilasProductos()
        {
            List<EvolucionCostos_Productos> evoluciones = new ControladorEvolucionCostos_Productos().GetAllByRecetaId(idReceta);

            foreach (var e in evoluciones)
            {
                InsertarFilaProducto(e);
            }
        }

        public void InsertarFilaProducto(EvolucionCostos_Productos evolucion)
        {
            try
            {
                //fila
                TableRow tr = new TableRow();
                tr.ID = "Evolucion" + evolucion.Id.ToString();

                ////Celdas
                //TableCell celNumero = new TableCell();
                //celNumero.Text = entrega.id.ToString();
                //celNumero.VerticalAlign = VerticalAlign.Middle;
                //celNumero.HorizontalAlign = HorizontalAlign.Right;
                //celNumero.Attributes.Add("style", " text-align: right");
                //tr.Cells.Add(celNumero);

                TableCell celFecha = new TableCell();
                celFecha.Text = evolucion.FechaCambio.ToString();
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celFecha);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = evolucion.Productos.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celDescripcion);

                TableCell celCostoAnteriorIngrediente = new TableCell();
                celCostoAnteriorIngrediente.Text = evolucion.CostoAnterior_Producto.ToString("C").Replace(',', '.');
                celCostoAnteriorIngrediente.VerticalAlign = VerticalAlign.Middle;
                celCostoAnteriorIngrediente.HorizontalAlign = HorizontalAlign.Left;
                celCostoAnteriorIngrediente.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoAnteriorIngrediente);

                TableCell celCostoNuevoIngrediente = new TableCell();
                celCostoNuevoIngrediente.Text = evolucion.CostoNuevo_Producto.ToString("C").Replace(',', '.');
                celCostoNuevoIngrediente.VerticalAlign = VerticalAlign.Middle;
                celCostoNuevoIngrediente.HorizontalAlign = HorizontalAlign.Left;
                celCostoNuevoIngrediente.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoNuevoIngrediente);

                TableCell celCostoTotalAnteriorReceta = new TableCell();
                celCostoTotalAnteriorReceta.Text = evolucion.CostoTotalAnterior_Receta.ToString("C").Replace(',', '.');
                celCostoTotalAnteriorReceta.VerticalAlign = VerticalAlign.Middle;
                celCostoTotalAnteriorReceta.HorizontalAlign = HorizontalAlign.Left;
                celCostoTotalAnteriorReceta.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoTotalAnteriorReceta);

                TableCell celCostoTotalNuevoReceta = new TableCell();
                celCostoTotalNuevoReceta.Text = evolucion.CostoTotalNuevo_Receta.ToString("C").Replace(',', '.');
                celCostoTotalNuevoReceta.VerticalAlign = VerticalAlign.Middle;
                celCostoTotalNuevoReceta.HorizontalAlign = HorizontalAlign.Left;
                celCostoTotalNuevoReceta.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoTotalNuevoReceta);

                phIngredientes.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
    }
}