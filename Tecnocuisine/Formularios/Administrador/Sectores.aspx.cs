using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Sectores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        public static string CargarSectores()
        {
            try
            {
                ControladorSector contSec = new ControladorSector();

                var sectores = contSec.GetSectores();

                string sec = "[";
                foreach (var item in sectores)
                {
                    sec += "{" +
                        "\"Id\":\"" + item.id + "\"," +
                        "\"nombre\":\"" + item.nombre + "\"," +
                        "\"Empresa\":\"" + item.id_emp + "\"," +
                        "\"Estado\":\"" + item.estado + "\"" +
                        "},";

                }
                sec = sec.Remove(sec.Length - 1)+"]";

                if (sectores.Count == 0)
                {
                    sec = "[]";
                }
                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return sec;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string AddSectores(string nombre, string id_emp)
        {
            try
            {
                ControladorSector contSector = new ControladorSector();
                if (nombre != "" && id_emp != "")
                {

                    Tecnocuisine_API.Entitys.Sectores sec = new Tecnocuisine_API.Entitys.Sectores();
                    sec.nombre = nombre;
                    sec.estado = 1;
                    sec.id_emp = Convert.ToInt32(id_emp);

                    var i = contSector.AddSectores(sec);

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
        public static string ChangeSectores(string id, string nombre, string id_emp)
        {
            try
            {
                ControladorSector contSectores = new ControladorSector();
                if (id != "" && id_emp != "")
                {

                    Tecnocuisine_API.Entitys.Sectores sec = new Tecnocuisine_API.Entitys.Sectores();
                    sec.id = Convert.ToInt32(id);
                    sec.nombre = nombre;
                    sec.id_emp = Convert.ToInt32(id_emp);
                    sec.estado = 1;

                    var rta = contSectores.ChangeSectores(sec);

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
        public static string RemoveSectores(string id)
        {
            try
            {
                ControladorSector contSectores = new ControladorSector();
                if (id != "")
                {

                    var rta = contSectores.RemoveSectores(id);

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