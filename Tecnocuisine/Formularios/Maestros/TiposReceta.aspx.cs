using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class TiposReceta : System.Web.UI.Page
    {
        ControladorTiposDeReceta cTiposDeReceta = new ControladorTiposDeReceta();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarListado();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnSi_Click(object sender, EventArgs e)
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
    }
}