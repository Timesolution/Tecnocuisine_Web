using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Empresas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
                cargarEmpresasBackEnd();
        }

        [WebMethod]
        public static string CargarCondicion_IVA()
        {
            try
            {
                ControladorRegimen contRegimen = new ControladorRegimen();

                var regimenes = contRegimen.ObtenerTodosRegimenes();

                string reg = "[";
                foreach (var item in regimenes)
                {
                    reg += "{" +
                        "\"id\":\"" + item.id + "\"," +
                        "\"descripcion\":\"" + item.descripcion + "\"" +
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
        public void cargarEmpresasBackEnd()
        {
            try
            {
                ControladorEmpresa contEmpresas = new ControladorEmpresa();

                var empresas = contEmpresas.GetEmpresas();
                foreach (var empresa in empresas)
                {
                    CargarEmpresaPH(empresa);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarEmpresaPH(Empresa empresa)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = empresa.Id.ToString();

                TableCell celrazonSocial = new TableCell();
                celrazonSocial.Text = empresa.Razon_Social;
                celrazonSocial.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celrazonSocial);

                TableCell celCuit = new TableCell();
                celCuit.Text = empresa.Cuit;
                celCuit.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCuit);

                TableCell celIIBB = new TableCell();
                celIIBB.Text = empresa.Ingresos_Brutos;
                celIIBB.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celIIBB);

                TableCell celFechaInicio = new TableCell(); 
                celFechaInicio.Text = "<span> " + empresa.Fecha_inicio.Value.ToString("yyyyMMdd") + "</span>" + empresa.Fecha_inicio.Value.ToString("dd/MM/yyyy");
                celFechaInicio.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celFechaInicio);

                TableCell celIVA = new TableCell();
                celIVA.Text = empresa.Condicion_IVA;
                celIVA.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celIVA);

                TableCell celDireccion = new TableCell();
                celDireccion.Text = empresa.Direccion;
                celDireccion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDireccion);

                TableCell celAlias = new TableCell();
                celAlias.Text = empresa.Alias;
                celAlias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celAlias);

                TableCell celCBU = new TableCell();
                celCBU.Text = empresa.CBU;
                celCBU.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCBU);

                TableCell celAction = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                //btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Style.Add("margin-right", "10px");
                btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + empresa.Id + "','" + empresa.Razon_Social + "','" + empresa.Cuit + "','" + empresa.Ingresos_Brutos + "','" + empresa.Condicion_IVA + "','" + empresa.Direccion + "','" + empresa.Alias + "','" + empresa.CBU + "'); ");
                celAction.Controls.Add(btnEditar);


                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                btnEliminar.Attributes.Add("class", "btn btn-xs");
                //btnEliminar.Style.Add("background-color", "transparent");
                //btnEliminar.Style.Add("margin-right", "10px");
                btnEliminar.InnerHtml = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                btnEliminar.Attributes.Add("OnClick", "ModalConfirmacion(" + empresa.Id + ");");

                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phEmpresas2.Controls.Add(tr);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        public static string CargarEmpresas()
        {
            try
            {
                ControladorEmpresa contEmpresas = new ControladorEmpresa();

                var empresas = contEmpresas.GetEmpresas();

                string emp = "[";
                foreach (var item in empresas)
                {
                    emp += "{" +
                        "\"Id\":\"" + item.Id + "\"," +
                        "\"FechaInicio\":\"" + item.Fecha_inicio + "\"," +
                        "\"Razon_Social\":\"" + item.Razon_Social + "\"," +
                        "\"Ingresos_Brutos\":\"" + item.Ingresos_Brutos + "\"," +
                        "\"Fecha_inicio\":\"" + item.Fecha_inicio?.ToString("dd/MM/yyyy") + "\"," +
                        "\"idCondicion_IVA\":\"" + item.Condicion_IVA + "\"," +
                        "\"Condicion_IVA\":\"" + item.Condicion_IVA + "\"," +
                        "\"Direccion\":\"" + item.Direccion + "\"," +
                        "\"Alias\":\"" + item.Alias + "\"," +
                        "\"CBU\":\"" + item.CBU + "\"" +
                        "},";

                }
                emp = emp.Remove(emp.Length - 1) + "]";

                if (empresas.Count == 0)
                {
                    emp = "[]";
                }
                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return emp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string AddEmpresas(string razonSocial, string cuit, string ingBrutos, string condIVA, string dir, string alias, string cbu)
        {
            try
            {
                ControladorEmpresa contEmpresas = new ControladorEmpresa();
                if (razonSocial != "" && cuit != "" && ingBrutos != "" && condIVA != "" && dir != "" && alias != "" && cbu != "")
                {

                    Empresa emp = new Empresa();
                    emp.Razon_Social = razonSocial;
                    emp.Fecha_inicio = DateTime.Now;
                    emp.Ingresos_Brutos = ingBrutos;
                    emp.Fecha_inicio = DateTime.Now;
                    emp.Condicion_IVA = condIVA;
                    emp.Direccion = dir;
                    emp.Alias = alias;
                    emp.CBU = cbu;

                    var empresas = contEmpresas.AddEmpresas(emp);

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
                    return "1";
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
        public static string ChangeEmpresas(string id, string razonSocial, string cuit, string ingBrutos, string condIVA, string dir, string alias, string cbu)
        {
            try
            {
                ControladorEmpresa contEmpresas = new ControladorEmpresa();
                if (id != "" && razonSocial != "" && cuit != "" && ingBrutos != "" && condIVA != "" && dir != "" && alias != "" && cbu != "")
                {

                    Empresa emp = new Empresa();
                    emp.Id = Convert.ToInt32(id);
                    emp.Razon_Social = razonSocial;
                    //emp.Fecha_inicio = Convert.ToDateTime( FechaInicio);
                    emp.Ingresos_Brutos = ingBrutos;
                    emp.Fecha_inicio = DateTime.Now;
                    emp.Condicion_IVA = condIVA;
                    emp.Direccion = dir;
                    emp.Alias = alias;
                    emp.CBU = cbu;

                    var rta = contEmpresas.ChangeEmpresas(emp);

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
        public static string RemoveEmpresa(string id)
        {
            try
            {
                ControladorEmpresa contEmpresas = new ControladorEmpresa();
                if (id != "")
                {

                    var rta = contEmpresas.RemoveEmpresa(id);

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