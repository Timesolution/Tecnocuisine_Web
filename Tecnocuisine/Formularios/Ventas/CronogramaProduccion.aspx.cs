using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class CronogramaProduccion : System.Web.UI.Page
    {
        public DataTable dt;
        public DataTable dtOrdenDeProduccionRecetas_recetas;
        public DataTable dtCantidadRecetasPorCadaOrden;
        //Esta datatable contiene una combinacion de los ingredientes normales
        //y las recetas que se usan como ingredientes
        public DataTable dtCombined;
        public DateTime fechaOrdenDeProduccion;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                string idsQueryString = Request.QueryString["ids"];
                int Nivel = 1;
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                //dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccion(idsQueryString);

                dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccionGroupBySector(idsQueryString);
                dtOrdenDeProduccionRecetas_recetas = cOrdenDeProduccion.GetAllIngredientesRecetasOrdenesProduccionGroupBySector(idsQueryString);

                fechaOrdenDeProduccion = obtenerFechaOrden(idsQueryString);
                dtCombined = dt.Clone();
                Nivel++;
                ObtenerProductoDeLasRecetas(dtOrdenDeProduccionRecetas_recetas, Nivel);
                DataTable dtRecetas = ObtenerRecetasDeLasRecetas(dtOrdenDeProduccionRecetas_recetas, Nivel);

                while (dtRecetas != null && dtRecetas.Rows.Count != 0)
                {
                    Nivel++;
                    ObtenerProductoDeLasRecetas(dtRecetas, Nivel);
                    dtRecetas = ObtenerRecetasDeLasRecetas(dtRecetas, Nivel);
                }

                //dtCombined = dt.Clone();
                // Combina los datos de ambas DataTables
                dtCombined.Merge(dt);
                dtCombined.Merge(dtOrdenDeProduccionRecetas_recetas);

                // Ordena la DataTable combinada por la columna "Fecha" de mayor a menor
                //dtCombined.DefaultView.Sort = "SectorProductivo ASC";
                dtCombined.DefaultView.Sort = "SectorProductivo ASC, DiasMasNivel DESC";
                dtCombined = dtCombined.DefaultView.ToTable();

                dt = dtCombined;

                dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);

                obtenerOPNumeros(idsQueryString);
            }
            catch (Exception ex)
            {
            }


            //if (dt2 != null)
            //{
            //    ObtenerProductoDeLasRecetas(dt2);
            //    //Aca tendria que obtener las recetas de las recetas 
            //    ObtenerRecetasDeLasRecetas(dt2);

            //}


        }


        public DateTime obtenerFechaOrden(string idOrdenDeProduccion)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            DataTable fechaOrdenProduccion = cOrdenDeProduccion.getFechaEntregaOrdenProduccion(idOrdenDeProduccion);

            DateTime FechaOrdenDeProduccion = Convert.ToDateTime(fechaOrdenProduccion.Rows[0][0]);
            return FechaOrdenDeProduccion;
        }

        public void ObtenerProductoDeLasRecetas(DataTable dtRecetas, int Nivel)
        {
            try
            {
                foreach (DataRow dr in dtRecetas.Rows)
                {
                    int idReceta = obterneridRecetaByDescripcion(dr["ProductoOReceta"].ToString());
                    DataTable productosDeLaReceta = ObtenerProductosPorIdReceta(idReceta, Nivel);

                    if (productosDeLaReceta != null)
                    {
                        dtCombined.Merge(productosDeLaReceta);
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }


        public DataTable ObtenerRecetasDeLasRecetas(DataTable dtRecetas, int Nivel)
        {
            DataTable RecetasDeLaReceta = null;

            foreach (DataRow dr in dtRecetas.Rows)
            {
                int idReceta = obterneridRecetaByDescripcion(dr["ProductoOReceta"].ToString());
                RecetasDeLaReceta = ObtenerRecetasPorIdReceta(idReceta, Nivel);

                if (RecetasDeLaReceta != null)
                {
                    dtCombined.Merge(RecetasDeLaReceta);
                }

                //Aca tengo que poner el codigo
                //ObtenerProductoDeLasRecetas(RecetasDeLaReceta);
                //ObtenerRecetasDeLasRecetas(RecetasDeLaReceta);
            }

            return RecetasDeLaReceta;
        }

        public int obterneridRecetaByDescripcion(string Descripcion)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtidReceta = cReceta.obtenerIdRecetaByDescripcionReceta(Descripcion);


            int idReceta = 0;
            idReceta = Convert.ToInt32(dtidReceta.Rows[0][0]);
            return idReceta;
        }

        public DataTable ObtenerProductosPorIdReceta(int idReceta, int Nivel)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtProductos = cReceta.ObtenerProductosPorIdReceta(idReceta, Nivel);
            return dtProductos;
        }


        public DataTable ObtenerRecetasPorIdReceta(int idReceta, int Nivel)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtRecetas = cReceta.ObtenerRecetaPorIdReceta(idReceta, Nivel);
            return dtRecetas;
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
            //string idsQueryString = Request.QueryString["ids"];
            OPNumeros = OPNumeros.TrimStart(',');
            lblIdsQueryString.Text = OPNumeros;
        }
    }
}