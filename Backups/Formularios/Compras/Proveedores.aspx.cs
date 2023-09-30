using Gestor_Solution.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class Proveedores : System.Web.UI.Page
    {
        ControladorPaises ControladorPaises = new ControladorPaises();
        ControladorRubros controladorRubros = new ControladorRubros();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        Mensaje m = new Mensaje();
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
            if (!IsPostBack)
            {

                CargarPaisesDLL();
                CargarRubrosDLL();

                if (accion == 2)
                {
                    cargarProveedor();
                }
            }
        }

        private void cargarProveedor()
        {
            try
            {
                var proveedor = ControladorProveedores.ObtenerProveedorByID(id);
                btnGuardar.Text = "Modificar";

                txtCodigo.Text = proveedor.Codigo;
                txtAlias.Text = proveedor.Alias;
                txtNumero.Text = proveedor.Numero;
                txtRazonSocial.Text = proveedor.RazonSocial;
                ddlPais.SelectedValue = proveedor.IdPais.ToString();
                ddlRubro.SelectedValue = proveedor.idRubro.ToString();
                ddlEstado.SelectedValue = proveedor.estado.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void CargarPaisesDLL()
        {
            try
            {
                //var Lista = ControladorPaises.ObtenerPaisesAllModelo();

                DataTable dt = ControladorPaises.ObtenerPaisesAlldt();

                DataRow dr = dt.NewRow();
                dr["id"] = "-1";
                dr["Pais_Documento"] = "Seleccione...";
                dt.Rows.InsertAt(dr, 0);
                //dt.Rows.Add(dr);

                ddlPais.DataSource = dt;
                ddlPais.DataValueField = "id";
                ddlPais.DataTextField = "Pais_Documento";
                ddlPais.SelectedValue = "-1";
                ddlPais.DataBind();

            }
            catch (Exception ex)
            {


            }
        }


        protected void CargarRubrosDLL()
        {
            try
            {
                DataTable dt = controladorRubros.GetAllRubrosDT();
                DataRow drSeleccione = dt.NewRow();
                drSeleccione["descripcion"] = "Seleccione...";
                drSeleccione["id"] = -1;
                dt.Rows.InsertAt(drSeleccione, 0);

                // Ordenar los datos alfabéticamente por la descripción (ignorando la primera fila)
                var sortedData = dt.AsEnumerable()
                                   .Skip(1) // Ignorar la primera fila ("Seleccione...")
                                   .OrderBy(row => row.Field<string>("descripcion"));

                // Crear una nueva tabla con los elementos ordenados y el elemento inicial
                DataTable sortedTable = dt.Clone();
                sortedTable.Rows.Add(drSeleccione.ItemArray);
                foreach (DataRow row in sortedData)
                {
                    sortedTable.ImportRow(row);
                }

                this.ddlRubro.DataSource = sortedTable;
                this.ddlRubro.DataValueField = "id";
                this.ddlRubro.DataTextField = "descripcion";

                this.ddlRubro.DataBind();

                this.ddlRubro.SelectedValue = "-1";

            }
            catch (Exception ex)
            {


            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    Tecnocuisine_API.Entitys.Proveedores nuevoProveedor = new Tecnocuisine_API.Entitys.Proveedores();
                    if (accion != 2)
                    {

                        nuevoProveedor.Codigo = txtCodigo.Text;
                        nuevoProveedor.Alias = txtAlias.Text;
                        nuevoProveedor.RazonSocial = txtRazonSocial.Text;
                        nuevoProveedor.Numero = txtNumero.Text;
                        nuevoProveedor.IdPais = Convert.ToInt32(ddlPais.SelectedValue);
                        nuevoProveedor.estado = Convert.ToInt32(ddlEstado.SelectedValue);
                        nuevoProveedor.idRubro = Convert.ToInt32(ddlRubro.SelectedValue);
                        int i = ControladorProveedores.AgregarNuevoProveedor(nuevoProveedor);
                        if (i > 0)
                        {
                            m.ShowToastr(Page, "Se agrego el proveedor!", "Exito", "success");
                            LimpiarCampos();
                        }
                        else
                        {
                            m.ShowToastr(Page, "Hubo un problema al agregar el proveedor", "Error", "danger");
                        }
                    }
                    else
                    {
                        nuevoProveedor = ControladorProveedores.ObtenerProveedorByID(id);
                        nuevoProveedor.Codigo = txtCodigo.Text;
                        nuevoProveedor.Alias = txtAlias.Text;
                        nuevoProveedor.RazonSocial = txtRazonSocial.Text;
                        nuevoProveedor.Numero = txtNumero.Text;
                        nuevoProveedor.IdPais = Convert.ToInt32(ddlPais.SelectedValue);
                        nuevoProveedor.idRubro = Convert.ToInt32(ddlRubro.SelectedValue);
                        nuevoProveedor.estado = Convert.ToInt32(ddlEstado.SelectedValue);

                        int i = ControladorProveedores.EditarProveedor(nuevoProveedor);

                        if (i > 0)
                        {
                            m.ShowToastr(Page, "Se Modifico el proveedor!", "Exito", "success");
                        }
                        else
                        {
                            m.ShowToastr(Page, "Hubo un problema al Modificar el proveedor", "Error", "danger");
                        }

                    }

                }
                else
                {
                    m.ShowToastr(Page, "Alguno de los datos esta incompleto", "Advertencia", "warning");
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void LimpiarCampos()
        {
            try
            {

                txtCodigo.Text = "";
                txtRazonSocial.Text = "";
                txtAlias.Text = "";
                txtNumero.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
    }
}