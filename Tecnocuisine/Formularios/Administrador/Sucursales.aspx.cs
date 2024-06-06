using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Sucursales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            if (!IsPostBack)
            {
                CargarSucursalesBackEnd();
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

        private void CargarSucursalesBackEnd()
        {
            try
            {
                ControladorSucursal contSuc = new ControladorSucursal();

                var sucursales = contSuc.GetSucursales();

                foreach (var s in sucursales)
                {
                    CargarSucursalPH(s);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarSucursalPH(sucursales s)
        {

            try
            {
                TableRow tr = new TableRow();
                tr.ID = s.id.ToString();

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = s.nombre;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);

                TableCell celDireccion = new TableCell();
                celDireccion.Text = s.direccion;
                celDireccion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDireccion);

                //TableCell celEstado = new TableCell();
                //celEstado.Text = s.estado.ToString();
                //celEstado.VerticalAlign = VerticalAlign.Middle;
                //tr.Cells.Add(celEstado);


                TableCell celAction = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Style.Add("margin-right", "10px");
                btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil' title='Editar sucursal'></i></span>";
                btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + s.id + "','" + s.nombre + "','" + s.id_emp + "','" + s.direccion + "'); ");
                celAction.Controls.Add(btnEditar);

                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                btnEliminar.Attributes.Add("class", "btn btn-xs");
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Style.Add("margin-right", "10px");
                btnEliminar.InnerHtml = "<span><i style='color:red;' class='fa fa-trash' title='Eliminar sucursal'></i></span>";
                btnEliminar.Attributes.Add("OnClick", "ModalConfirmacion(" + s.id + ");");
                celAction.Controls.Add(btnEliminar);

                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phSucursalesPH.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string CargarEmpresas()
        {
            try
            {
                ControladorEmpresa contEmpresa = new ControladorEmpresa();

                var empresas = contEmpresa.GetEmpresas();

                string reg = "[";
                foreach (var item in empresas)
                {
                    reg += "{" +
                        "\"id\":\"" + item.Id + "\"," +
                        "\"descripcion\":\"" + item.Razon_Social + "\"" +
                        "},";

                }
                reg = reg.Remove(reg.Length - 1) + "]";

                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return reg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string CargarSucursales()
        {
            try
            {
                ControladorSucursal contSuc = new ControladorSucursal();

                var sucursales = contSuc.GetSucursales();

                string suc = "[";
                foreach (var item in sucursales)
                {
                    suc += "{" +
                        "\"Id\":\"" + item.id + "\"," +
                        "\"nombre\":\"" + item.nombre + "\"," +
                        "\"Direccion\":\"" + item.direccion + "\"," +
                        //"\"Empresa\":\"" + item.id_emp + "\"," +
                        "\"Empresa\":\"" + item.id_emp + "\"" +
                        "},";

                }
                suc = suc.Remove(suc.Length - 1) + "]";

                if (sucursales.Count == 0)
                {
                    suc = "[]";
                }
                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return suc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string AddSucursal(string nombre, string dir, string id_emp)
        {
            try
            {
                ControladorSucursal contSucursal = new ControladorSucursal();
                if (nombre != "" && dir != "")
                {

                    sucursales suc = new sucursales();
                    suc.nombre = nombre;
                    suc.direccion = dir;
                    suc.estado = 1;
                    suc.id_emp = Convert.ToInt32(id_emp);

                    var i = contSucursal.AddSucursales(suc);

                    return i;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string ChangeSucursal(string id, string nombre, string id_emp, string dir)
        {
            try
            {
                ControladorSucursal contSucursales = new ControladorSucursal();
                if (id != "" && dir != "" && id_emp != "")
                {

                    sucursales suc = new sucursales();
                    suc.id = Convert.ToInt32(id);
                    suc.nombre = nombre;
                    suc.direccion = dir;
                    suc.id_emp = Convert.ToInt32(id_emp);
                    suc.estado = 1;

                    var rta = contSucursales.ChangeSucursales(suc);

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
                    return rta;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string RemoveSucursal(string id)
        {
            try
            {
                ControladorSucursal contSucursales = new ControladorSucursal();
                if (id != "")
                {

                    var rta = contSucursales.RemoveSucursales(id);

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
                    return rta;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}