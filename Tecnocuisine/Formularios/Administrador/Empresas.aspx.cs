using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Empresas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

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
                        "\"Cuit\":\"" + item.Cuit + "\"," +
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
                    emp.Cuit = cuit;
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
                    emp.Cuit = cuit;
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