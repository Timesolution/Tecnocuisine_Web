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
            if (!IsPostBack){
                CargarSucursalesBackEnd();
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

                    TableCell celEstado = new TableCell();
                    celEstado.Text = s.estado.ToString();
                    celEstado.VerticalAlign = VerticalAlign.Middle;
                    tr.Cells.Add(celEstado);


                    TableCell celAction = new TableCell();

                    HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                    btnEditar.Attributes.Add("class", "btn btn-xs");
                    btnEditar.Style.Add("background-color", "transparent");
                    btnEditar.Style.Add("margin-right", "10px");
                    //btnEditar.Attributes.Add("title data-original-title", familia);
                    //btnEditar.Attributes.Add("data-toggle", "modal");
                    //btnEditar.Attributes.Add("title data-original-title", "Editar");
                    btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                    btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + s.id + "','" + s.nombre + "','" + s.id_emp + "','" + s.direccion + "'); ");
                    celAction.Controls.Add(btnEditar);

                    Literal l = new Literal();
                    l.Text = "&nbsp";
                    celAction.Controls.Add(l);


                HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                btnEliminar.Attributes.Add("class", "btn btn-xs");
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Style.Add("margin-right", "10px");
                //btnEliminar.Attributes.Add("title data-original-title", familia);
                //btnEliminar.Attributes.Add("data-toggle", "modal");
                //btnEliminar.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.InnerHtml = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
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
                        "\"Empresa\":\"" + item.id_emp + "\"," +
                        "\"Estado\":\"" + item.estado + "\"" +
                        "},";

                }
                suc = suc.Remove(suc.Length - 1)+"]";

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

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
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