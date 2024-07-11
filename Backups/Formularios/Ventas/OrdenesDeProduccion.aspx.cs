using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class OrdenesDeProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarOrdendesDeCompra();
        }


        private void CargarOrdendesDeCompra()
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                //var ListaProv = ControladorProveedores.ObtenerProveedoresAll();
                var ListaOrdPro = cOrdenDeProduccion.GetAllOrdenesDeProduccion();
                foreach (var ordenProd in ListaOrdPro)
                {
                    CargarOrdendesProduccionenPH(ordenProd);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarOrdendesProduccionenPH(ordenesDeProduccion ordenProd)
        {
            try
            {
                TableRow tr = new TableRow();

                TableCell celOrdenNumero = new TableCell();
                celOrdenNumero.Text = ordenProd.OPNumero;
                celOrdenNumero.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celOrdenNumero);


                TableCell celCliente = new TableCell();
                celCliente.Text = ordenProd.Clientes.alias;
                celCliente.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCliente);


                TableCell celFechaDeEntrega = new TableCell();
                celFechaDeEntrega.Text = Convert.ToDateTime(ordenProd.fechaEntrega).ToString("dd/MM/yyyy");
                celFechaDeEntrega.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaDeEntrega);

                TableCell celEstado = new TableCell();
                celEstado.Text = "Pendiente";
                celEstado.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celEstado);

                //TableCell celTipoNumero = new TableCell();
                //celTipoNumero.Text = proveedor.Numero;
                //celTipoNumero.VerticalAlign = VerticalAlign.Middle;
                //tr.Cells.Add(celTipoNumero);

                //TableCell celAction = new TableCell();
                //LinkButton btnEditar = new LinkButton();
                //btnEditar.ID = proveedor.Id.ToString();

                //btnEditar.CssClass = "btn btn-xs";
                //btnEditar.Style.Add("background-color", "transparent");
                ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                //btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                //btnEditar.Click += new EventHandler(this.editarProveedor);
                //celAction.Controls.Add(btnEditar);


                //Literal l = new Literal();
                //l.Text = "&nbsp";
                //celAction.Controls.Add(l);


                //LinkButton btnEliminar = new LinkButton();
                //btnEliminar.ID = "btnEliminar_" + proveedor.Id;


                //btnEliminar.OnClientClick = "abrirdialog(" + proveedor.Id + ");";
                //btnEliminar.CssClass = "btn btn-xs";
                //btnEliminar.Style.Add("background-color", "transparent");
                ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                //btnEliminar.Text = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                ////btnEliminar.Click += new EventHandler(this.EliminarProveedor);
                //celAction.Controls.Add(btnEliminar);
                //celAction.Width = Unit.Percentage(10);
                //celAction.VerticalAlign = VerticalAlign.Middle;
                //celAction.HorizontalAlign = HorizontalAlign.Center;
                //tr.Cells.Add(celAction);

                phOrdenesProduccion.Controls.Add(tr);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}