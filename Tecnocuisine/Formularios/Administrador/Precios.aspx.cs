using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Precios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreferenciasPrecios pref = ControladorPreferenciasPrecios.Obtener();
                if (pref == null)
                {
                    rbUltimoPrecio.Checked = true;
                }
                else
                {
                    rbUltimoPrecio.Checked = pref.Ultimo;
                    rbPrecioBarato.Checked = pref.Mas_Barato;
                    rbPromedioPonderado.Checked = pref.Promedio_Ponderado;
                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PreferenciasPrecios pref = ControladorPreferenciasPrecios.Obtener();

            if (pref == null)
            {
                pref = new PreferenciasPrecios();
                SetPreferencia(pref);
                ControladorPreferenciasPrecios.Agregar(pref);
                return;
            }

            SetPreferencia(pref);
            ControladorPreferenciasPrecios.Actualizar(pref);
        }

        private void SetPreferencia(PreferenciasPrecios pref)
        {
            pref.Ultimo = rbUltimoPrecio.Checked;
            pref.Mas_Barato = rbPrecioBarato.Checked;
            pref.Promedio_Ponderado = rbPromedioPonderado.Checked;
        }
    }
}