using System;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Controladores.Inspinia_MVC5_SeedProject.Controllers;
using Tecnocuisine_API.ViewModels;

namespace Tecnocuisine.Formularios.Usuario
{
    public partial class login : System.Web.UI.Page
    {
        ControladorUsuario controlador = new ControladorUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected  void btnLogin_Click(object sender, EventArgs e)
        {
            ControladorAtributo controlador = new ControladorAtributo();
            int i = controlador.Login();
        }

   


    }
}