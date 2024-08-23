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
                PreCargarOpciones();
            }
        }

        private void PreCargarOpciones()
        {
            PreferenciasPrecios pref = ControladorPreferenciasPrecios.Obtener();
            if (pref == null)
            {
                rbUltimoPrecio.Checked = true;
            }
            else // Si existen preferencias en la base, se cargan sus valores
            {
                rbUltimoPrecio.Checked = pref.Ultimo;
                rbPrecioBarato.Checked = pref.Mas_Barato;
                rbPromedioPonderado.Checked = pref.Promedio_Ponderado;

                if(pref.Porcentaje_Alerta_FoodCost != null)
                    txtPorcentajeAlertaFoodCost.Text = pref.Porcentaje_Alerta_FoodCost.ToString();

                // Formatear fechas para los input date si el valor no es la preferencia "ultimo"
                if (rbPrecioBarato.Checked)
                {
                    txtPrecioBaratoInicio.Text = pref.Fecha_Inicio.Value.ToString("yyyy-MM-dd");
                    txtPrecioBaratoFin.Text = pref.Fecha_Fin.Value.ToString("yyyy-MM-dd");
                }
                else if (rbPromedioPonderado.Checked)
                {
                    txtPromedioPonderadoInicio.Text = pref.Fecha_Inicio.Value.ToString("yyyy-MM-dd");
                    txtPromedioPonderadoFin.Text = pref.Fecha_Fin.Value.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                PreferenciasPrecios pref = ControladorPreferenciasPrecios.Obtener();

                if (pref == null)
                {
                    pref = new PreferenciasPrecios();
                    this.SetPreferencia(pref);
                    ControladorPreferenciasPrecios.Agregar(pref);
                }
                else
                {
                    this.SetPreferencia(pref);
                    ControladorPreferenciasPrecios.Actualizar(pref);
                }

                this.ActualizarCostos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ActualizarCostos()
        {
            // Actualiza el costo de los productos y recetas de manera masiva en base a la configuracion del sistema
            new ControladorProducto().ActualizarTodosLosCostos();
            new ControladorReceta().ActualizarTodosLosCostos();
        }

        private void SetPreferencia(PreferenciasPrecios pref)
        {
            pref.Ultimo = rbUltimoPrecio.Checked;
            pref.Mas_Barato = rbPrecioBarato.Checked;
            pref.Promedio_Ponderado = rbPromedioPonderado.Checked;

            if (txtPorcentajeAlertaFoodCost.Text.Trim() != string.Empty)
                pref.Porcentaje_Alerta_FoodCost = Convert.ToDouble(txtPorcentajeAlertaFoodCost.Text.Trim());
            else
                pref.Porcentaje_Alerta_FoodCost = null; // El foodcost no fue ingresado

            /// Asignar rango de fechas
            if (pref.Ultimo)
            {
                pref.Fecha_Inicio = null;
                pref.Fecha_Fin = null;
            }
            else if (pref.Mas_Barato)
            {
                pref.Fecha_Inicio = DateTime.Parse(txtPrecioBaratoInicio.Text);
                pref.Fecha_Fin = DateTime.Parse(txtPrecioBaratoFin.Text);
            }
            else if (pref.Promedio_Ponderado)
            {
                pref.Fecha_Inicio = DateTime.Parse(txtPromedioPonderadoInicio.Text);
                pref.Fecha_Fin = DateTime.Parse(txtPromedioPonderadoFin.Text);
            }

        }
    }
}