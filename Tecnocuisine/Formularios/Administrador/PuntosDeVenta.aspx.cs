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
    public partial class PuntosDeVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                suc = suc.Remove(suc.Length - 1) + "]";

                if (sucursales.Count == 0)
                {
                    suc = "[]";
                }
                return suc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public static string CargarEmpresas(string idSuc)
        {
            try
            {
                ControladorEmpresa contEmpresa = new ControladorEmpresa();

                var empresas = contEmpresa.GetEmpresasByIdSuc(idSuc);

                string reg = "[";
                foreach (var item in empresas)
                {
                    reg += "{" +
                        "\"id\":\"" + item.Id + "\"," +
                        "\"descripcion\":\"" + item.Razon_Social + "\"" +
                        "},";

                }
                reg = reg.Remove(reg.Length - 1) + "]";

                if (empresas.Count == 0)
                {
                    reg = "[]";
                }
                return reg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string CargarPuntoDeVenta()
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();

                var puntoVta = contPtoDeVta.GetPuntosDeVenta();

                string ptoDeVta = "[";
                foreach (var item in puntoVta)
                {
                    ptoDeVta += "{" +
                        "\"Id\":\"" + item.Id + "\"," +
                        "\"nombre\":\"" + item.NombreFantasia + "\"," +
                        "\"Direccion\":\"" + item.Direccion + "\"," +
                        "\"Empresa\":\"" + item.Id_emp + "\"," +
                        "\"Estado\":\"" + item.estado + "\"," +
                        "\"CAIRemito\":\"" + item.CAIRemito + "\"," +
                        "\"CAIVencimiento\":\"" + item.CAIVencimiento + "\"," +
                        "\"formaDeFacturar\":\"" + item.FormaFactura + "\"," +
                        "\"monedaFacturacion\": \"" + item.MonedaFacturacion + "\"," +
                        "\"retieneGanancias\":\"" + item.Retiene_Gan + "\"," +
                        "\"retieneIngresosBrutos\":\"" + item.Retiene_IB + "\"," +
                        "\"puntoDeVenta\":\"" + item.PtoVenta + "\"," +
                        "\"idSuc\":\"" + item.Id_Suc + "\"" +
                        "},";

                }
                ptoDeVta = ptoDeVta.Remove(ptoDeVta.Length - 1) + "]";

                if (puntoVta.Count == 0)
                {
                    ptoDeVta = "[]";
                }
                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return ptoDeVta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string AddPuntoDeVenta(string nombre, string empresa, string dir, string CAIRemito, string CAIVencimiento,
            string formaDeFacturar, string monedaFacturacion, string retieneGanancias, string retieneIngresosBrutos, string puntoDeVenta, string idSuc)
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();
                if (nombre != "" && dir != "")
                {

                    PuntoVta ptoDeVta = new PuntoVta();
                    ptoDeVta.NombreFantasia = nombre;
                    ptoDeVta.Id_emp = Convert.ToInt32(empresa);
                    ptoDeVta.PtoVenta = puntoDeVenta;
                    ptoDeVta.Direccion = dir;
                    ptoDeVta.estado = 1;
                    ptoDeVta.CAIRemito = CAIRemito;
                    ptoDeVta.CAIVencimiento = Convert.ToDateTime(CAIVencimiento);
                    ptoDeVta.FormaFactura = formaDeFacturar;
                    ptoDeVta.MonedaFacturacion = Convert.ToInt32(monedaFacturacion);
                    ptoDeVta.Retiene_Gan = Convert.ToBoolean(Convert.ToInt32(retieneGanancias));
                    ptoDeVta.Retiene_IB = Convert.ToBoolean(Convert.ToInt32(retieneIngresosBrutos));
                    ptoDeVta.Id_Suc = Convert.ToInt32(idSuc);

                    var i = contPtoDeVta.AddPuntosDeVenta(ptoDeVta);

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
        public static string ChangePuntoDeVenta(string id, string nombre, string id_emp, string dir, string CAIRemito, string CAIVencimiento,
            string formaDeFacturar, string monedaFacturacion, string retieneGanancias, string retieneIngresosBrutos, string puntoDeVenta, string idSuc)
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();
                if (id != "" && dir != "" && id_emp != "")
                {

                    PuntoVta ptoDeVta = new PuntoVta();
                    ptoDeVta.Id = Convert.ToInt32(id);
                    ptoDeVta.NombreFantasia = nombre;
                    ptoDeVta.Direccion = dir;
                    ptoDeVta.Id_emp = Convert.ToInt32(id_emp);
                    ptoDeVta.estado = 1;
                    ptoDeVta.PtoVenta = puntoDeVenta;
                    ptoDeVta.CAIRemito = CAIRemito;
                    ptoDeVta.CAIVencimiento = Convert.ToDateTime(CAIVencimiento);
                    ptoDeVta.FormaFactura = formaDeFacturar;
                    ptoDeVta.MonedaFacturacion = Convert.ToInt32(monedaFacturacion);
                    ptoDeVta.Retiene_Gan = Convert.ToBoolean(Convert.ToInt32(retieneGanancias));
                    ptoDeVta.Retiene_IB = Convert.ToBoolean(Convert.ToInt32(retieneIngresosBrutos));
                    ptoDeVta.Id_Suc = Convert.ToInt32(idSuc);

                    var rta = contPtoDeVta.ChangePuntosDeVenta(ptoDeVta);

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
        public static string RemovePtsDeVta(string id)
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();
                if (id != "")
                {

                    var rta = contPtoDeVta.RemovePuntosDeVenta(id);

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
        public static string GetLastPuntoVta(string idSuc)
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();

                var rta = contPtoDeVta.GetLastPuntoVta(idSuc);
                string lastPuntoVta = "";
                if (rta == null)
                {
                    lastPuntoVta = "0000";
                }
                else
                {
                    lastPuntoVta = (Convert.ToInt32(rta.PtoVenta) + 1).ToString().PadLeft(4, '0');
                }
                return lastPuntoVta;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}