using Gestion_Api.Modelo;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Security;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Controladores.Inspinia_MVC5_SeedProject.Controllers;
using Tecnocuisine_API.ViewModels;

namespace Tecnocuisine.Account
{
    public partial class login : System.Web.UI.Page
    {
        Gestion_Api.Controladores.controladorUsuario controlador = new Gestion_Api.Controladores.controladorUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeCulture();
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
        }

        protected  void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Gestion_Api.Modelo.Usuario usuario = new Gestion_Api.Modelo.Usuario();
                //ApplicationUser user = manager.Find(this.txtUsuario.Text, this.txtpassword.Text);
                if (usuario.validar(this.txtUsuario.Text, this.txtpassword.Text))
                {
                    usuario = controlador.ObtenerUsuario(this.txtUsuario.Text, this.txtpassword.Text);
                    Session.Add("Login_SucUser", usuario.sucursal.id);
                    Session.Add("Login_PtoUser", usuario.ptoVenta.id);
                    Session.Add("Login_EmpUser", usuario.empresa.id);
                    Session.Add("User", this.txtUsuario.Text);
                    Session.Add("Pass", this.txtpassword.Text);
                    Session.Add("Login_IdUser", usuario.id);
                    Session.Add("Login_NombrePerfil", usuario.perfil.nombre);
                    Session.Add("Login_Permisos", usuario.permisos);
                    Log.EscribirSQL(usuario.id, "INFO", "Login");

                    Response.Redirect("/Default.aspx");

                }
                else
                {
                    //Response.Redirect("Account/Login.aspx");
                    string options = "toastr.options = { ";
                    options += " positionClass: 'toast-bottom-right', ";
                    options += "}; ";

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "toastr_message",
                          options + "toastr." + "success" + "('" + "Error" + "', '" + "Error" + "');", addScriptTags: true);
                    txtUsuario.Text = "";
                    txtpassword.Text = "";
                }
            }

        }




    }
}