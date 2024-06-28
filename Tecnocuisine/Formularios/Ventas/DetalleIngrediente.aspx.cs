using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class DetalleIngrediente : System.Web.UI.Page
    {
        public DataTable dt;
        public DataTable dtOrdenDeProduccionRecetas_recetas;
        public DataTable dtCantidadRecetasPorCadaOrden;
        //Esta datatable contiene una combinacion de los ingredientes normales
        //y las recetas que se usan como ingredientes
        public DataTable dtCombined;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string idsQueryString = Request.QueryString["ids"];

                if (string.IsNullOrWhiteSpace(idsQueryString))
                    Response.Redirect("OrdenesDeProduccion.aspx");

                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccion(idsQueryString);
                dtOrdenDeProduccionRecetas_recetas = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccionRecetas_Recetas(idsQueryString);
                dtCombined = dt.Clone();
                // Combina los datos de ambas DataTables
                dtCombined.Merge(dt);
                dtCombined.Merge(dtOrdenDeProduccionRecetas_recetas);

                // Ordena la DataTable combinada por la columna "Fecha" de mayor a menor
                dtCombined.DefaultView.Sort = "Fecha ASC";
                dtCombined = dtCombined.DefaultView.ToTable();

                dt = dtCombined;

                dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);

                obtenerOPNumeros(idsQueryString);
            }
            catch (Exception)
            {
                Response.Redirect("OrdenesDeProduccion.aspx");
            }
        }

        //Esta funcion busca y obtiene todos los numeros de produccion en base a sus id
        public void obtenerOPNumeros(string idsQueryString)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            DataTable dt = cOrdenDeProduccion.GetallOPNumeros(idsQueryString);

            string OPNumeros = string.Empty;
            OPNumeros = dt.Rows[0][0].ToString();
            MostrarOPNumeros(OPNumeros);
        }
        public void MostrarOPNumeros(string OPNumeros)
        {
            OPNumeros = OPNumeros.TrimStart(',');
            lblIdsQueryString.Text = OPNumeros;
        }
    }
}