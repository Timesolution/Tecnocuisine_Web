using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Articulos : Page
    {
        Mensaje m = new Mensaje();
        ControladorArticulo controladorArticulo = new ControladorArticulo();
        int accion;
        int idArticulo;
        int Mensaje;
        int idReceta;
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
    
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idArticulo = Convert.ToInt32(Request.QueryString["i"]);
            this.idReceta = Convert.ToInt32(Request.QueryString["r"]);

            if (!IsPostBack)
            {
                txtFechaActualizacion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaAlta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaModificacion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarAlicuotas();
                CargarCategorias();
                CargarMarcas();
                CargarGruposArticulos();
                if (accion == 2)
                {
                    CargarArticulo();
                }
                if (accion == 3)
                {
                    CargarReceta();
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

            }

            ObtenerArticulos();
          

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

        private void CargarReceta()
        {
            ControladorReceta controladorReceta = new ControladorReceta();
            var receta = controladorReceta.ObtenerRecetaId(this.idReceta);
            txtDescripcion.Text = receta.descripcion;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        private void CargarGruposArticulos()
        {

            try
            {
                ControladorGrupo controladorGrupo = new ControladorGrupo();
                this.ListGrupo.DataSource = controladorGrupo.ObtenerTodosArticulos_Grupos();
                this.ListGrupo.DataValueField = "id";
                this.ListGrupo.DataTextField = "descripcion";
                this.ListGrupo.DataBind();
                ListGrupo.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        ////private void CargarSubGruposArticulos()
        //{

        //    try
        //    {
        //        ControladorGrupo controladorGrupo = new ControladorGrupo();
        //        this.ListSubgrupo.DataSource = controladorGrupo.ObtenerTodosArticulos_SubGrupos(Convert.ToInt32(ListGrupo.SelectedValue));
        //        this.ListSubgrupo.DataValueField = "id";
        //        this.ListSubgrupo.DataTextField = "descripcion";
        //        this.ListSubgrupo.DataBind();
        //        ListSubgrupo.Items.Insert(0, new ListItem("Seleccione", "-1"));



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void CargarMarcas()
        {

            try
            {
                ControladorMarca controladorMarca = new ControladorMarca();
                this.ListMarca.DataSource = controladorMarca.ObtenerTodasMarcas();
                this.ListMarca.DataValueField = "id";
                this.ListMarca.DataTextField = "descripcion";
                this.ListMarca.DataBind();
                ListMarca.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        private void CargarCategorias()
        {

            try
            {
                ControladorCategoria controladorCategoria = new ControladorCategoria();
                this.ListCategoria.DataSource = controladorCategoria.ObtenerCategorias();
                this.ListCategoria.DataValueField = "id";
                this.ListCategoria.DataTextField = "descripcion";
                this.ListCategoria.DataBind();
                ListCategoria.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        private void CargarAlicuotas()
        {

            try
            {
                ControladorIVA controladorIVA = new ControladorIVA();
                this.ListAlicuotas.DataSource = controladorIVA.ObtenerTodosAlicuotas_IVA();
                this.ListAlicuotas.DataValueField = "id";
                this.ListAlicuotas.DataTextField = "porcentaje";
                this.ListAlicuotas.DataBind();
                ListAlicuotas.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }


        public void ObtenerArticulos()
        {
            try
            {
                var articulos = controladorArticulo.ObtenerTodosArticulos();

                if (articulos.Count > 0)
                {

                    foreach (var item in articulos)
                    {
                        CargarArticulosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarArticulo()
        {
            try
            {
                var articulo = controladorArticulo.ObtenerArticuloId(this.idArticulo);
                if (articulo != null)
                {
                    hiddenEditar.Value = articulo.id.ToString() ?? "";
                    txtDescripcion.Text = articulo.descripcion;
                    txtCodigo.Text = articulo.codigo.ToString() ?? "";
                    txtCodigoBarra.Text = articulo.codigoBarra.ToString() ?? "";
                    txtCosto.Text = articulo.costo.ToString() ?? "";
                    txtPrecioVenta.Text = articulo.precioVenta.ToString() ?? "";
                    txtObservaciones.Text = articulo.observaciones.ToString() ?? "";
                    txtFechaActualizacion.Text = articulo.fechaActualizacionPrecio.ToString() ?? "";
                    txtFechaAlta.Text = articulo.fechaAlta.ToString() ?? "";
                    txtFechaModificacion.Text = articulo.fechaModificacion.ToString() ?? "";
                    ListAlicuotas.SelectedValue = articulo.alicuotaIVA.ToString();
                    ListCategoria.SelectedValue = articulo.categoria.ToString();
                    ListGrupo.SelectedValue = articulo.grupo.ToString();
                    //CargarSubGruposArticulos();
                    ListMarca.SelectedValue = articulo.marca.ToString();
                    //ListSubgrupo.SelectedValue = articulo.subgrupo.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarArticulosPH(Tecnocuisine_API.Entitys.Articulos articulo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = articulo.id.ToString();

                //Celdas
                TableCell celCodigo = new TableCell();
                celCodigo.Text = articulo.codigo.ToString();
                celCodigo.VerticalAlign = VerticalAlign.Middle;
                celCodigo.HorizontalAlign = HorizontalAlign.Left;
                celCodigo.Width = Unit.Percentage(5);
                celCodigo.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celCodigo);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = articulo.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Width = Unit.Percentage(10);
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);


                TableCell celGrupo = new TableCell();
                celGrupo.Text = articulo.Articulos_Grupos.descripcion;
                celGrupo.VerticalAlign = VerticalAlign.Middle;
                celGrupo.HorizontalAlign = HorizontalAlign.Left;
                celGrupo.Width = Unit.Percentage(5);
                celGrupo.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celGrupo);

                TableCell celUltimaActualizacion = new TableCell();
                celUltimaActualizacion.Text = articulo.fechaActualizacionPrecio.ToString();
                celUltimaActualizacion.VerticalAlign = VerticalAlign.Middle;
                celUltimaActualizacion.HorizontalAlign = HorizontalAlign.Left;
                celUltimaActualizacion.Width = Unit.Percentage(5);
                celUltimaActualizacion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUltimaActualizacion);

                TableCell celPrecioVenta = new TableCell();
                celPrecioVenta.Text = articulo.precioVenta.ToString(); 
                celPrecioVenta.VerticalAlign = VerticalAlign.Middle;
                celPrecioVenta.HorizontalAlign = HorizontalAlign.Right;
                celPrecioVenta.Width = Unit.Percentage(3);
                celPrecioVenta.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celPrecioVenta);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                celAccion.Width = Unit.Percentage(3);
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + articulo.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnDetalles.Click += new EventHandler(this.editarArticulo);
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + articulo.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + articulo.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phArticulos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarArticulo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Articulos.aspx?a=2&i=" + id[1]);
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
                    EditarArticulo();
                }
                else
                {
                    GuardarArticulo();
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
        public void GuardarArticulo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Articulos articulo = new Tecnocuisine_API.Entitys.Articulos();

                articulo.codigo = txtCodigo.Text;
                articulo.descripcion = txtDescripcion.Text;
                articulo.codigoBarra = txtCodigoBarra.Text;
                articulo.fechaAlta = DateTime.Now;
                articulo.fechaModificacion = DateTime.Now;
                articulo.fechaActualizacionPrecio = DateTime.Now;
                articulo.costo = Convert.ToDecimal(txtCosto.Text);
                articulo.precioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                articulo.grupo = Convert.ToInt32(ListGrupo.SelectedValue);
                articulo.subgrupo = 5;
                //articulo.subgrupo = Convert.ToInt32(ListSubgrupo.SelectedValue);
                articulo.marca = Convert.ToInt32(ListMarca.SelectedValue);
                articulo.categoria = Convert.ToInt32(ListCategoria.SelectedValue);
                articulo.alicuotaIVA = Convert.ToInt32(ListAlicuotas.SelectedValue);
                articulo.observaciones = txtObservaciones.Text;
                articulo.estado = 1;


                int resultado = controladorArticulo.AgregarArticulo(articulo);

                if (resultado > 0)
                {
                    Response.Redirect("Articulos.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el articulo", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarArticulo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Articulos articulo = new Tecnocuisine_API.Entitys.Articulos();

                articulo.id = this.idArticulo;
                articulo.codigo = txtCodigo.Text;
                articulo.descripcion = txtDescripcion.Text;
                articulo.codigoBarra = txtCodigoBarra.Text;
                articulo.fechaModificacion = DateTime.Now;
                articulo.fechaActualizacionPrecio = DateTime.Now;
                articulo.fechaAlta = DateTime.Now;

                articulo.costo = decimal.Parse(txtCosto.Text, CultureInfo.InvariantCulture);
                articulo.precioVenta = decimal.Parse(txtPrecioVenta.Text, CultureInfo.InvariantCulture);
                articulo.grupo = Convert.ToInt32(ListGrupo.SelectedValue);
                //articulo.subgrupo = Convert.ToInt32(ListSubgrupo.SelectedValue);
                articulo.subgrupo = 1;
                articulo.marca = Convert.ToInt32(ListMarca.SelectedValue);
                articulo.categoria = Convert.ToInt32(ListCategoria.SelectedValue);
                articulo.alicuotaIVA = Convert.ToInt32(ListAlicuotas.SelectedValue);
                articulo.observaciones = txtObservaciones.Text;
                articulo.estado = 1;


                int resultado = controladorArticulo.EditarArticulo(articulo);

                if (resultado > 0)
                {
                    Response.Redirect("Articulos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la articulo", "warning");
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
                int idArticulo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorArticulo.EliminarArticulo(idArticulo);

                if (resultado > 0)
                {
                    Response.Redirect("Articulos.aspx?m=3");
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
       

    }
}
#endregion
