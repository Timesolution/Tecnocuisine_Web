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
    public partial class PuntosDeVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            if (!IsPostBack)
            {
                CargarPuntoDeVentaBackEnd();
            }
        }

        private void CargarPuntoDeVentaBackEnd()
        {
            try
            {
                ControladorPuntosDeVenta contPtoDeVta = new ControladorPuntosDeVenta();

                var puntoVtas = contPtoDeVta.GetPuntosDeVenta();
                
                foreach(var puntoventa in puntoVtas)
                {
                    CargarPuntoDeVentaPH(puntoventa);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarPuntoDeVentaPH(PuntoVta puntoventa)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = puntoventa.Id.ToString();

                TableCell celrazonSocial = new TableCell();
                celrazonSocial.Text = puntoventa.PtoVenta;
                celrazonSocial.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celrazonSocial);
                string FormaFacturar = "";
                if (puntoventa.FormaFactura == "1")
                {
                    FormaFacturar = "Electronica";
                }
                else if (puntoventa.FormaFactura == "2")
                {
                    FormaFacturar = "Preimpresa";
                }
                else if (puntoventa.FormaFactura == "3")
                {
                    FormaFacturar = "Fiscal";
                }
                TableCell celCuit = new TableCell();
                celCuit.Text = FormaFacturar;
                celCuit.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCuit);
                string opcionR = "";
                if (!puntoventa.Retiene_IB.Value)
                    opcionR = "No";
                else
                    opcionR = "Si";
                TableCell celIIBB = new TableCell();
                celIIBB.Text = opcionR;
                celIIBB.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celIIBB);


                string opcionG = "";
                if (!puntoventa.Retiene_Gan.Value)
                    opcionG = "No";
                else
                    opcionG = "Si";

                TableCell celganancias = new TableCell();
                celganancias.Text = opcionG;
                celganancias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celganancias);

                TableCell celfantasia = new TableCell();
                celfantasia.Text = puntoventa.NombreFantasia;
                celfantasia.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celfantasia);

                TableCell celDireccion = new TableCell();
                celDireccion.Text = puntoventa.Direccion;
                celDireccion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDireccion);

                TableCell celempresa = new TableCell();
                celempresa.Text = puntoventa.Id_emp.ToString();
                celempresa.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celempresa);

                TableCell celAction = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Style.Add("margin-right", "10px");
                btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil' title='Editar punto de venta'></i></span>";
                btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + puntoventa.Id + "','" + puntoventa.PtoVenta + "','" + puntoventa.Id_emp+ "','" + puntoventa.Direccion + "','" + puntoventa.PtoVenta + "','" + puntoventa.FormaFactura + "','" + puntoventa.Retiene_IB + "','" + puntoventa.Retiene_Gan + "','" + puntoventa.CAIRemito +"','" + puntoventa.CAIVencimiento+ "','" + puntoventa.MonedaFacturacion + "','" + puntoventa.Id_Suc+ "'); ");
                celAction.Controls.Add(btnEditar);

                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);

                HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                btnEliminar.Attributes.Add("class", "btn btn-xs");
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Style.Add("margin-right", "10px");
                btnEliminar.InnerHtml = "<span><i style='color:red;' class='fa fa-trash' title='Eliminar'></i></span>";
                btnEliminar.Attributes.Add("OnClick", "ModalConfirmacion(" + puntoventa.Id + ");");

                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                PuntoDeVentaPH.Controls.Add(tr);
            }
            catch (Exception ex)
            {

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