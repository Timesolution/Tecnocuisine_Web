using Gestion_Api.Entitys;
using Gestor_Solution.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Services;
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
        int idEstado = -1;
        ControladorCliente controladorCliente = new ControladorCliente();
        ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();

        Mensaje m = new Mensaje();


        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            txtFechaHoy.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();
                this.idCliente = Convert.ToInt32((Request.QueryString["c"]).ToString());
                this.FechaH = (Request.QueryString["FechaH"]).ToString();
                this.idEstado = Convert.ToInt32(Request.QueryString["Estado"]);
            }

            if (!IsPostBack)
            {
                ObtenerClientes();
                ObtenerEstados();
                CargarOrdendesDeCompra();

            }

            if (idCliente != -1)
            {
                filtrarOrdenesDeCompra(this.FechaD, this.FechaH, this.idCliente, this.idEstado);
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

        private void filtrarOrdenesDeCompra(string FechaD, string FechaH, int idCliente, int idEstado)
        {


            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                //var ListaOrdPro = cOrdenDeProduccion.GetAllOrdenesDeProduccion();
                var ListaOrdPro = cOrdenDeProduccion.GetAllOrdenesDeFiltradasPorRangoDeFechaYCliente(FechaD, FechaH, idCliente, idEstado);

                phOrdenesProduccion.Controls.Clear();
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
                    //celOPNumero.VerticalAlign = VerticalAlign.Middle;
                    //celOPNumero.HorizontalAlign = HorizontalAlign.Left;
                    //celOPNumero.Attributes.Add("style", "padding-bottom: 1px !important; margin-left: 10px;");
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

        private void ObtenerEstados()
        {
            try
            {
                var estados = cOrdenDeProduccion.ObtenerTodosLosEstado();
                if (estados.Count > 0)
                {
                    CargarEstadosOptions(estados);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void CargarEstadosOptions(List<Tecnocuisine_API.Entitys.EstadosOrdenes> estados)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var estado in estados)
                {
                    builder.Append(String.Format("<option value='{0}' id='c_r_" + estado.id + "_" + estado.Descripcion + "'>", estado.id + " - " + estado.Descripcion + " - " + "Estado"));
                }
                ListaEstados.InnerHtml = builder.ToString();

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
                celOrdenNumero.Attributes.Add("style", "padding-bottom: 1px !important; margin-left: 10px; padding-left: 4px;");
                tr.Cells.Add(celOrdenNumero);




                TableCell celCliente = new TableCell();
                celCliente.Text = ordenProd.Clientes.alias;
                celCliente.VerticalAlign = VerticalAlign.Middle;
                celCliente.Attributes.Add("style", "padding-bottom: 1px !important; margin-left: 10px; padding-left: 4px;");
                tr.Cells.Add(celCliente);


                TableCell celFechaDeEntrega = new TableCell();
                celFechaDeEntrega.Text = Convert.ToDateTime(ordenProd.fechaEntrega).ToString("dd/MM/yyyy");
                celFechaDeEntrega.VerticalAlign = VerticalAlign.Middle;
                celFechaDeEntrega.Attributes.Add("style", "padding-bottom: 1px !important; margin-left: 10px; padding-left: 4px;");
                tr.Cells.Add(celFechaDeEntrega);

                TableCell celEstado = new TableCell();
                celEstado.Text = ordenProd.EstadosOrdenes.Descripcion;
                celEstado.Attributes.Add("style", "padding-bottom: 1px !important; margin-left: 10px; padding-left: 4px;");
                celEstado.VerticalAlign = VerticalAlign.Middle;

                switch (ordenProd.EstadosOrdenes.id)
                {
                    case 1:
                        celEstado.ForeColor = System.Drawing.Color.Blue;
                        break;
                    //No pongo case 2 dos porque no hace falta
                    case 3:
                        celEstado.ForeColor = System.Drawing.Color.Yellow;
                        break;
                    case 4:
                        celEstado.ForeColor = System.Drawing.Color.Green;
                        break;
                }

                //if (ordenProd.EstadosOrdenes.id == 1) { 
                //celEstado.ForeColor = System.Drawing.Color.Blue;                
                //}

                tr.Cells.Add(celEstado);

                TableCell celAccion = new TableCell();
                CheckBox chkSeleccionar = new CheckBox();
                chkSeleccionar.ID = "chkSeleccionar_" + ordenProd.id;
                chkSeleccionar.Attributes.Add("onchange", "javascript:return guardarIdOrdenDeProduccion(" + ordenProd.id + ");");

                Literal l3 = new Literal();
                l3.Text = "&nbsp";
                celAccion.Controls.Add(l3);

                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                btnDetalles.ID = "btnSelecReceta_" + ordenProd.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar'></i></span>"; // Agregar la tooltip "Editar"
                btnDetalles.PostBackUrl = "OrdenesDeProduccionABM.aspx?Accion=2&id=" + ordenProd.id;

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminarReceta_" + ordenProd.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash-o' title='Eliminar'></i></span>"; // Agregar la tooltip "Eliminar"
                btnEliminar.OnClientClick = "abrirdialog(" + ordenProd.id + ");";


                // Si la orden ya se mando a producir, no se podra editar, producir, y eliminar
                if (ordenProd.EstadosOrdenes.id == 1)
                {
                    chkSeleccionar.InputAttributes.Add("disabled", "true");
                    btnDetalles.Attributes.Add("disabled", "true");
                    btnEliminar.Attributes.Add("disabled", "true");
                }

                celAccion.Controls.Add(chkSeleccionar);
                celAccion.Controls.Add(btnDetalles);
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

        //[WebMethod]
        //public static int cambiarEstadoDeLaOrden(string idOrdenDeProduccion)
        //{
        //    ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
        //    //string idOrdenDeProduccion = Request.QueryString["ids"];
        //    cOrdenDeProduccion.cambiarEstadoOrden(idOrdenDeProduccion);

        //    return 1;
        //}

    }
}