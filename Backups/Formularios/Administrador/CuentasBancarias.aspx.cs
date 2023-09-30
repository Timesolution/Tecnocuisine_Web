using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Tecnocuisine_API.Controladores;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using System.Data;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class CuentasBancarias : System.Web.UI.Page
    {
        ControladorPaises ControladorPaises = new ControladorPaises();
        ControladorCuentasBancarias controladorCuentasBancarias = new ControladorCuentasBancarias();
        ControladorEntidadesBancarias controladorEntidadesBancarias = new ControladorEntidadesBancarias();

        Mensaje m = new Mensaje();
        int Mensaje;
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            if (!IsPostBack)
            {
                switch (accion)
                {
                    case 1:
                        break;
                    case 2:
                        //CargarPaisEditar();
                        break;
                }
            CargarCuentaBancos();
            cargarEntidadesBancarias();
            }
            if (Mensaje == 1)
            {
                this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }
           
        }
        public void cargarEntidadesBancarias()
        {
            try
            {
                var ListEntidades = controladorEntidadesBancarias.ObtenerBancosAlldt();
                DataTable dataTable = new DataTable();


                this.txtEntidadBancaria.DataSource = ListEntidades;
                this.txtEntidadBancaria.DataValueField = "id";
                this.txtEntidadBancaria.DataTextField = "Nombre";

                this.txtEntidadBancaria.DataBind();

                this.DropDownListEditar.DataSource = ListEntidades;
                this.DropDownListEditar.DataValueField = "id";
                this.DropDownListEditar.DataTextField = "Nombre";

                this.DropDownListEditar.DataBind();
                

            }
            catch (Exception ex)
            {
            }
        }


        //private void CargarPaisEditar()
        //{
        //    try
        //    {
        //        btnAgregar.Text = "Modificar";
        //        var pais = ControladorPaises.ObtenerPaisById(id);
        //        txtPais.Text = pais.Pais;
        //        txtTipoDoc.Text = pais.TipoDocumento;

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        private void CargarCuentaBancos()
        {
            try
            {
                var Lista = controladorCuentasBancarias.TraerTodasLasCuentasBancarias();
                foreach (var bancos in Lista)
                {
                    CargarPHPaises(bancos);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarPHPaises(Tecnocuisine_API.Entitys.CuentasBancarias p)
        {
            try
            {
                TableRow tr = new TableRow();


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = p.EntidadesBancarias.Nombre;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);


                TableCell celTipoDoc = new TableCell();
                celTipoDoc.Text = p.CuentaNumero;
                celTipoDoc.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoDoc);

                TableCell celDescripcion2 = new TableCell();
                celDescripcion2.Text = p.Descripcion;
                celDescripcion2.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion2);

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = p.id.ToString();

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("href", "#");
                btnEditar.Attributes.Add("onclick", "abrirdialog2('" + p.id + "','" + p.idEntidadBancaria + "','" + p.CuentaNumero + "','" + p.Descripcion + "');");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                //btnEditar.OnClientClick = "abrirdialog2(" + p.id +","+p.CuentaNumero + "," + p.Descripcion + ");";
                //btnEditar.Text = "<a onclick=\"abrirdialog2(\""+p.id +"\",\""+p.CuentaNumero + "\",\"" + p.Descripcion+ "\")><span><i style='color:black;' class='fa fa-pencil'></i></span></a>";
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                celAction.Controls.Add(btnEditar);

                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + p.id;


                btnEliminar.OnClientClick = "abrirdialog(" + p.id + ");";
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.Text = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                //btnDetalles.Click += new EventHandler(this.editarReceta);
                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phPaises.Controls.Add(tr);


            }
            catch (Exception ex)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", mje.mensajeBoxError("Error cargando Procedencia en la lista. " + ex.Message));
            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idCuentaBancaria = Convert.ToInt32(this.hiddenID.Value);
                
                int resultado = controladorCuentasBancarias.EliminarCuentaBancaria(idCuentaBancaria);

                if (resultado > 0)
                {
                    Response.Redirect("CuentasBancarias.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
                }
            }
            catch (Exception ex)
            {
                this.m.ShowToastr(this.Page, "No se pudo eliminar el país", "warning");
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                //editar
                    int id = Convert.ToInt32(hiddenID.Value);
                    var CuentasBancarias = controladorCuentasBancarias.TraerCuentasBancariasByID(id);
                    CuentasBancarias.idEntidadBancaria = Convert.ToInt32(DropDownListEditar.SelectedValue);
                    CuentasBancarias.CuentaNumero = txtNumeroEditar.Text;
                    CuentasBancarias.Descripcion = txtDescripcionEditar.Text;


                    int i = controladorCuentasBancarias.EditarCuentasBancarias(CuentasBancarias);
                    if (i > 0)
                    {
                        Response.Redirect("CuentasBancarias.aspx?m=1");
                    }
                
            }catch(Exception ex)
            {

            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
               
                    Tecnocuisine_API.Entitys.CuentasBancarias cuentasBancarias = new Tecnocuisine_API.Entitys.CuentasBancarias();

                    cuentasBancarias.idEntidadBancaria = Convert.ToInt32(txtEntidadBancaria.SelectedValue);
                    cuentasBancarias.CuentaNumero = txtNumeroCuenta.Text;
                    cuentasBancarias.Descripcion = txtDescripcion.Text;

                    int i = controladorCuentasBancarias.AgregarEntidad(cuentasBancarias);
                    if (i > 0)
                    {
                        Response.Redirect("CuentasBancarias.aspx?m=1");
                    }
                }

           
            catch (Exception ex)
            {


            }
        }
    }
}