using Gestion_Api.Entitys;
using Gestor_Solution.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class OrdenesDeProduccion : System.Web.UI.Page
    {

        int idCliente = -1;
        string FechaD = "";
        string FechaH = "";
        ControladorCliente controladorCliente = new ControladorCliente();
        ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
        Mensaje m = new Mensaje();


        protected void Page_Load(object sender, EventArgs e)
        {
            CargarOrdendesDeCompra();
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idCliente = Convert.ToInt32((Request.QueryString["c"]).ToString());
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }

            if (!IsPostBack)
            {
                ObtenerClientes();
            }

            if (idCliente != -1)
            {
                filtrarOrdenesDeCompra(this.FechaD, this.FechaH, this.idCliente);
            }
        }

        private void filtrarOrdenesDeCompra(string FechaD, string FechaH, int idCliente)
        {
            DataTable dt = cOrdenDeProduccion.getAllOrdenesPorRangoDeFechaYCliente(FechaD, FechaH, idCliente);
            CargarOrdenesDeCompraPH(dt);

        }


        private void CargarOrdenesDeCompraPH(DataTable dt)
        {

            phOrdenesProduccion.Controls.Clear();

            try
            {
                foreach (DataRow dr in dt.Rows)
                {


                    TableRow tr = new TableRow();
                    tr.ID = dr["id"].ToString();

                    TableCell celOPNumero = new TableCell();
                    celOPNumero.Text = dr["OPNumero"].ToString();
                    celOPNumero.VerticalAlign = VerticalAlign.Middle;
                    celOPNumero.HorizontalAlign = HorizontalAlign.Left;
                    celOPNumero.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celOPNumero);


                    TableCell celCliente = new TableCell();
                    celCliente.Text = dr["alias"].ToString();
                    celCliente.VerticalAlign = VerticalAlign.Middle;
                    celCliente.HorizontalAlign = HorizontalAlign.Left;
                    celCliente.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCliente);


                    TableCell celFechaEntrega = new TableCell();
                    celFechaEntrega.Text = Convert.ToDateTime(dr["fechaEntrega"]).ToString("dd/MM/yyyy");
                    celFechaEntrega.VerticalAlign = VerticalAlign.Middle;
                    celFechaEntrega.HorizontalAlign = HorizontalAlign.Left;
                    celFechaEntrega.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celFechaEntrega);


                    TableCell celEstado = new TableCell();
                    celEstado.Text = "Pendiente";
                    celEstado.VerticalAlign = VerticalAlign.Middle;
                    celEstado.HorizontalAlign = HorizontalAlign.Left;
                    celEstado.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celEstado);


                    //TableCell celAccion = new TableCell();
                    //LinkButton btnDetalles = new LinkButton();
                    //btnDetalles.CssClass = "btn btn-primary btn-xs";
                    //btnDetalles.ID = "btnSelecRec_" + dr["id"].ToString();
                    //btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                    //celAccion.Controls.Add(btnDetalles);



                    //CheckBox chkSeleccionar = new CheckBox();
                    //chkSeleccionar.CssClass = "tu-clase-css"; // Puedes establecer una clase CSS si lo deseas
                    //chkSeleccionar.ID = "chkSeleccionar_" + Receta.id + "_" + Receta.descripcion;
                    //chkSeleccionar.Checked = true; // Si quieres que el checkbox esté marcado inicialmente
                    //celAccion.Controls.Add(chkSeleccionar);

                    //celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");



                    //tr.Cells.Add(celAccion);
                    phOrdenesProduccion.Controls.Add(tr);

                }
            }
            catch (Exception ex)
            {


            }
        }
        private void ObtenerClientes()
        {
            try
            {
                var Clientes = controladorCliente.ObtenerTodosClientes();
                if (Clientes.Count > 0)
                {
                    CargarClientesOptions(Clientes);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarClientesOptions(List<Tecnocuisine_API.Entitys.Clientes> clientes)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var cliente in clientes)
                {


                    builder.Append(String.Format("<option value='{0}' id='c_r_" + cliente.id + "_" + cliente.alias + "_" + cliente.cuit + "'>", cliente.id + " - " + cliente.alias + " - " + "Cliente"));

                }



                ListaNombreCliente.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
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
                tr.ID = ordenProd.id.ToString();    

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

                TableCell celAccion = new TableCell();
                CheckBox chkSeleccionar = new CheckBox();
                //chkSeleccionar.CssClass = "tu-clase-css"; // Puedes establecer una clase CSS si lo deseas
                chkSeleccionar.ID = "chkSeleccionar_" + ordenProd.id;
                chkSeleccionar.Attributes.Add("onchange", "javascript:return guardarIdOrdenDeProduccion(" + ordenProd.id + ");");
                //chkSeleccionar.Checked = true; // Si quieres que el checkbox esté marcado inicialmente
                celAccion.Controls.Add(chkSeleccionar);



                Literal l3 = new Literal();
                l3.Text = "&nbsp";
                celAccion.Controls.Add(l3);

                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelecReceta_" + ordenProd.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.PostBackUrl = "OrdenesDeProduccionABM.aspx?Accion=2&id=" + ordenProd.id;

                //btnDetalles.Click += new EventHandler(this.editarReceta);
                celAccion.Controls.Add(btnDetalles);


                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminarReceta_" + ordenProd.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + ordenProd.id + ");";
                celAccion.Controls.Add(btnEliminar);


                tr.Cells.Add(celAccion);
                phOrdenesProduccion.Controls.Add(tr);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idOrdenProduccion = Convert.ToInt32(this.hiddenID.Value);
                cOrdenDeProduccion.EliminarOrdenesProduccion(idOrdenProduccion);


                Response.Redirect("OrdenesDeProduccion.aspx");


                //if (controladorReceta.verificarRelacionRecetaReceta(idReceta))
                //{
                //int resultado = cOrdenDeProduccion.el(idReceta);

                //    if (resultado > 0)
                //    {
                //        Response.Redirect("Recetas.aspx?m=3");
                //    }
                //    else
                //    {
                //        this.m.ShowToastr(this.Page, "No se pudo eliminar el Receta", "warning");
                //    }
                //}
                //else
                //{
                //    this.m.ShowToastr(this.Page, "No se pudo eliminar la receta porque tiene una receta relacionada", "Alerta", "warning");

                //}
            }
            catch (Exception ex)
            {

            }
        }

    }
}