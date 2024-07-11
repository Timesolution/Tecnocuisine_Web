using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class Tarjetas : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        ControladorTarjetas controladorTarjetas = new ControladorTarjetas();
        ControladorEntidad controladorEntidad = new ControladorEntidad();

        int accion;
        int idInsumo;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idInsumo = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {

              
                if (accion == 2)
                {
                    CargarInsumo();
                    cargarEntidades();
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

            cargarEntidades();
            }

            ObtenerInsumos();

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

        public void cargarEntidades()
        {
            try
            {
                var ListEntidades = controladorEntidad.ObtenerTodasLasEntidades();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("id", typeof(int));
                dataTable.Columns.Add("descripcion", typeof(string));

                // Recorrer la lista de entidades y agregar filas a la DataTable
                foreach (var entidad in ListEntidades)
                {
                    DataRow row = dataTable.NewRow();
                    row["id"] = entidad.id;
                    row["descripcion"] = entidad.descripcion;
                    dataTable.Rows.Add(row);
                }


                DataRow dr = dataTable.NewRow();
                dr["descripcion"] = "Seleccione...";
                dr["id"] = -1;
                dataTable.Rows.InsertAt(dr, 0);

                this.DropDownEntidadList.DataSource = dataTable;
                this.DropDownEntidadList.DataValueField = "id";
                this.DropDownEntidadList.DataTextField = "descripcion";

                this.DropDownEntidadList.DataBind();
                
                this.DropDownListEdit.DataSource = dataTable;
                this.DropDownListEdit.DataValueField = "id";
                this.DropDownListEdit.DataTextField = "descripcion";

                this.DropDownListEdit.DataBind();

            }
            catch (Exception ex)
            {
            }
        }



        public void ObtenerInsumos()
        {
            try
            {
                var tarjetas = controladorTarjetas.ObtenerTodasLasTarjetas();

                if (tarjetas.Count > 0)
                {

                    foreach (var item in tarjetas)
                    {
                        CargarInsumosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumo()
        {
            try
            {
                var insumo = controladorInsumo.ObtenerInsumoId(this.idInsumo);

                if (insumo != null)
                {
                    hiddenEditar.Value = insumo.id_insumo.ToString();
                    txtDescripcionInsumo.Text = insumo.Descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumosPH(Tecnocuisine_API.Entitys.Tarjetas item)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = item.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = item.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Width = Unit.Percentage(20);

                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = item.nombre;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Width = Unit.Percentage(40);
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                TableCell celEntidad = new TableCell();
                celEntidad.Text = item.Entidades.descripcion;
                celEntidad.VerticalAlign = VerticalAlign.Middle;
                celEntidad.HorizontalAlign = HorizontalAlign.Left;
                celEntidad.Width = Unit.Percentage(40);
                celEntidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celEntidad);


                TableCell celAcreditacion = new TableCell();
                celAcreditacion.Text = item.AcreditaEn.ToString();
                celAcreditacion.VerticalAlign = VerticalAlign.Middle;
                celAcreditacion.HorizontalAlign = HorizontalAlign.Right;
                celAcreditacion.Width = Unit.Percentage(40);
                celAcreditacion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcreditacion);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnDetalles.ID = "btnSelec_" + item.id + "_";
                btnDetalles.Text = "<span><i style='color:black;' class='fa fa-pencil' title='Editar tarjeta'></i></span>";
                btnDetalles.OnClientClick = "abrirdialog2('" + item.id + "', '" + item.nombre + "', '" + item.idEntidad.ToString() + "', '" + item.AcreditaEn + "');";
                btnDetalles.Attributes.Add("href", "#");
                celAccion.Controls.Add(btnDetalles);

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + item.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:red' class='fa fa-trash - o' title='Eliminar'></i></span>";
                btnEliminar.OnClientClick = "abrirdialog(" + item.id + ");";
                celAccion.Controls.Add(btnEliminar);

                celAccion.Width = Unit.Percentage(30);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        protected void editarInsumo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("InsumosF.aspx?a=2&i=" + id[1]);
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
                    EditarInsumo();
                }
                else
                {
                    GuardarInsumo();
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
                //txtDescripcionInsumo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        #region ABM
        public void GuardarInsumo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Tarjetas tarjeta = new Tecnocuisine_API.Entitys.Tarjetas();

                tarjeta.nombre = txtDescripcionInsumo.Text;
                tarjeta.idEntidad = Convert.ToInt32(DropDownEntidadList.SelectedValue);
                tarjeta.AcreditaEn = Convert.ToInt32(txtAcreditaEnDias.Text);
                tarjeta.estado = true;
                int resultado = controladorTarjetas.AgregarTarjeta(tarjeta);

                if (resultado > 0)
                {
                    Response.Redirect("Tarjetas.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el insumo", "warning");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void EditarInsumo()
        {
            try
            {
                Tecnocuisine_API.Entitys.Tarjetas tarjeta = new Tecnocuisine_API.Entitys.Tarjetas();
                tarjeta.id = Convert.ToInt32(hiddenID.Value);
                tarjeta.nombre = txtNombreTarjetaEditar.Text;
                tarjeta.AcreditaEn = Convert.ToInt32(txtAcreditaEnEdit.Text);
                tarjeta.idEntidad = Convert.ToInt32(DropDownListEdit.SelectedValue);
                tarjeta.estado = true;

                int resultado = controladorTarjetas.ModificarTarjeta(tarjeta);

                if (resultado > 0)
                {
                    Response.Redirect("Tarjetas.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar el insumo", "warning");
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
                int idInsumo = Convert.ToInt32(this.hiddenID.Value);
                int resultado = controladorTarjetas.EliminarTarjetas(idInsumo);

                if (resultado > 0)
                {
                    Response.Redirect("Tarjetas.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el insumo", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}