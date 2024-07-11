using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Ventas;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class PanelDeControl : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        Gestion_Api.Controladores.controladorArticulo controladorArticulo = new Gestion_Api.Controladores.controladorArticulo();
        Gestion_Api.Controladores.ControladorArticulosEntity contArtEnt = new Gestion_Api.Controladores.ControladorArticulosEntity();
        int accion;
        int idProducto;
        int Mensaje;




        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);


            if (!IsPostBack)
            {

                if (accion == 2)
                {
                }

                if (Mensaje == 1)
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

            }
            CargarDllPanel();
        }
        private void CargarDllPanel()
        {
            ControladorPanelDeControl controladorPanelDeControl = new ControladorPanelDeControl();

           var PanelDeControl2 = controladorPanelDeControl.GetPanelStock();
            if (PanelDeControl2.StockPorProducionYPorVenta == false)
            {
                HiddenOption.Value = "false";
            }
            else
            {
                HiddenOption.Value = "true";
            }
        }



        [WebMethod]
        public static void CambiarPanel(string value)
        {
            try
            {
                ControladorPanelDeControl controladorPanelDeControl = new ControladorPanelDeControl();
                bool val = Convert.ToBoolean(Convert.ToInt32(value));
                Tecnocuisine_API.Entitys.PanelDeControl panel = new Tecnocuisine_API.Entitys.PanelDeControl();
                panel.StockPorProducionYPorVenta = val;
                panel.id = 1;

                controladorPanelDeControl.ModifyPanelStock(panel);

            }
            catch (Exception)
            {

            }
        }
        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Account/Login.aspx");
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

     

    
    }
}