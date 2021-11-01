using System;
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
    public partial class Clientes : Page
    {
        Mensaje m = new Mensaje();
        ControladorCliente controladorCliente = new ControladorCliente();
        int accion;
        int idCliente;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {

            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idCliente = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

                CargarFormasPago();
                CargarRegimenesIVA();
                CargarVendedores();
                CargarEstadosClientes();
                if (accion == 2)
                {
                    CargarCliente();
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

            ObtenerClientes();
          

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

        private void CargarRegimenesIVA()
        {
            try
            {

                ControladorRegimen controladorIVA = new ControladorRegimen();
                this.ListRegimen.DataSource = controladorIVA.ObtenerTodosRegimenes();
                this.ListRegimen.DataValueField = "id";
                this.ListRegimen.DataTextField = "descripcion";
                this.ListRegimen.DataBind();
                ListRegimen.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }

        private void CargarFormasPago()
        {

            try
            {

                ControladorFormaPago controladorFormaPago = new ControladorFormaPago();
                this.ListFormaPago.DataSource = controladorFormaPago.ObtenerTodasFormasPago();
                this.ListFormaPago.DataValueField = "id";
                this.ListFormaPago.DataTextField = "descripcion";
                this.ListFormaPago.DataBind();
                ListFormaPago.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        private void CargarVendedores()
        {

            try
            {

                ControladorVendedor controladorVendedor  = new ControladorVendedor();
                List<Tecnocuisine_API.Entitys.Vendedores> listaVendedores = controladorVendedor.ObtenerTodosVendedores();
                List<Tecnocuisine_API.Entitys.Vendedores> listaNuevaVendedores = new List<Tecnocuisine_API.Entitys.Vendedores>();
                foreach (Tecnocuisine_API.Entitys.Vendedores item in listaVendedores)
                {
                    Tecnocuisine_API.Entitys.Vendedores vendedor = new Tecnocuisine_API.Entitys.Vendedores { nombre = item.nombre + " " + item.apellido, id = item.id };
                    listaNuevaVendedores.Add(vendedor);
                }
                this.ListVendedor.DataSource = listaNuevaVendedores;
                this.ListVendedor.DataValueField = "id";
                this.ListVendedor.DataTextField = "nombre";
                this.ListVendedor.DataBind();
                ListVendedor.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        private void CargarEstadosClientes()
        {

            try
            {

                this.ListEstado.DataSource = controladorCliente.ObtenerTodosEstados();
                this.ListEstado.DataValueField = "id";
                this.ListEstado.DataTextField = "descripcion";
                this.ListEstado.DataBind();
                ListEstado.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerClientes()
        {
            try
            {
                var clientes = controladorCliente.ObtenerTodosClientes();

                if (clientes.Count > 0)
                {

                    foreach (var item in clientes)
                    {
                        CargarClientesPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarCliente()
        {
            try
            {
                var cliente = controladorCliente.ObtenerClienteId(this.idCliente);
                if (cliente != null)
                {
                    hiddenEditar.Value = cliente.id.ToString() ?? "";
                    txtCodigo.Text = cliente.codigo.ToString() ?? "";
                    txtCuit.Text = cliente.cuit.ToString() ?? "";
                    txtRazonSocial.Text = cliente.razonSocial.ToString() ?? "";
                    txtAlias.Text = cliente.alias.ToString() ?? "";
                    txtSaldoMax.Text = cliente.saldoMax.ToString() ?? "";
                    txtObservaciones.Text = cliente.observaciones.ToString() ?? "";
                    txtVencimientoFC.Text = cliente.vencimientoFC.ToString() ?? "";
                    ListFormaPago.SelectedValue = cliente.formaPago.ToString() ?? "-1";
                    ListRegimen.SelectedValue = cliente.iva.ToString() ?? "-1";
                    ListEstado.SelectedValue = cliente.estado.ToString() ?? "-1";
                    ListVendedor.SelectedValue = cliente.vendedor.ToString() ?? "-1";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarClientesPH(Tecnocuisine_API.Entitys.Clientes cliente)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = cliente.id.ToString();

                //Celdas
                TableCell celCodigo = new TableCell();
                celCodigo.Text = cliente.codigo.ToString();
                celCodigo.VerticalAlign = VerticalAlign.Middle;
                celCodigo.HorizontalAlign = HorizontalAlign.Left;
                celCodigo.Width = Unit.Percentage(5);
                celCodigo.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celCodigo);

                TableCell celRazonSocial = new TableCell();
                celRazonSocial.Text = cliente.razonSocial;
                celRazonSocial.VerticalAlign = VerticalAlign.Middle;
                celRazonSocial.HorizontalAlign = HorizontalAlign.Left;
                celRazonSocial.Width = Unit.Percentage(5);
                celRazonSocial.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celRazonSocial);


                TableCell celAlias = new TableCell();
                celAlias.Text = cliente.alias;
                celAlias.VerticalAlign = VerticalAlign.Middle;
                celAlias.HorizontalAlign = HorizontalAlign.Left;
                celAlias.Width = Unit.Percentage(5);
                celAlias.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAlias);

                TableCell celIVA = new TableCell();
                celIVA.Text = cliente.RegimenesIVA.descripcion;
                celIVA.VerticalAlign = VerticalAlign.Middle;
                celIVA.HorizontalAlign = HorizontalAlign.Left;
                celIVA.Width = Unit.Percentage(5);
                celIVA.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celIVA);

                TableCell celFormaPago = new TableCell();
                celFormaPago.Text = cliente.FormasPago.descripcion;
                celFormaPago.VerticalAlign = VerticalAlign.Middle;
                celFormaPago.HorizontalAlign = HorizontalAlign.Left;
                celFormaPago.Width = Unit.Percentage(5);
                celFormaPago.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFormaPago);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + cliente.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarCliente);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + cliente.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + cliente.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phClientes.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarCliente(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Clientes.aspx?a=2&i=" + id[1]);
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
                    EditarCliente();
                }
                else
                {
                    GuardarCliente();
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
                txtCodigo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarCliente()
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes cliente = new Tecnocuisine_API.Entitys.Clientes();

                cliente.codigo = txtCodigo.Text;
                cliente.cuit = txtCuit.Text;
                cliente.razonSocial = txtRazonSocial.Text;
                cliente.alias = txtAlias.Text;
                cliente.observaciones = txtObservaciones.Text;
                cliente.saldoMax = Convert.ToDecimal(txtSaldoMax.Text);
                cliente.vencimientoFC = Convert.ToInt32(txtVencimientoFC.Text);
                cliente.activo = 1;
                cliente.formaPago = Convert.ToInt32(ListFormaPago.SelectedValue);
                cliente.iva = Convert.ToInt32(ListRegimen.SelectedValue);
                cliente.estado = Convert.ToInt32(ListEstado.SelectedValue);
                cliente.vendedor = Convert.ToInt32(ListVendedor.SelectedValue);
                cliente.fechaAlta = DateTime.Now;

                int resultado = controladorCliente.AgregarCliente(cliente);

                if (resultado > 0)
                {
                    Response.Redirect("Clientes.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el cliente", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarCliente()
        {
            try
            {
                Tecnocuisine_API.Entitys.Clientes cliente = new Tecnocuisine_API.Entitys.Clientes();

                cliente.id = this.idCliente;
                cliente.codigo = txtCodigo.Text;
                cliente.cuit = txtCuit.Text;
                cliente.razonSocial = txtRazonSocial.Text;
                cliente.alias = txtAlias.Text;
                cliente.observaciones = txtObservaciones.Text;
                cliente.saldoMax = Convert.ToDecimal(txtSaldoMax.Text);
                cliente.vencimientoFC = Convert.ToInt32(txtVencimientoFC.Text);
                cliente.activo = 1;
                cliente.formaPago = Convert.ToInt32(ListFormaPago.SelectedValue);
                cliente.iva = Convert.ToInt32(ListRegimen.SelectedValue);
                cliente.estado = Convert.ToInt32(ListEstado.SelectedValue);
                cliente.vendedor = Convert.ToInt32(ListVendedor.SelectedValue);


                int resultado = controladorCliente.EditarCliente(cliente);

                if (resultado > 0)
                {
                    Response.Redirect("Clientes.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la cliente", "warning");
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
                int idCliente = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorCliente.EliminarCliente(idCliente);

                if (resultado > 0)
                {
                    Response.Redirect("Clientes.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el unidad", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}