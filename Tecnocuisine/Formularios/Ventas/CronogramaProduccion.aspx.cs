using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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


                string idsQueryString = Request.QueryString["ids"]; //Esta variable obtiene todos los ids de todas las ordenes de produccion seleccionadas
                int Nivel = 1;
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();

                //Esta funcion obtiene todas los ingredientes de todas las ordenes de produccion de nivel 1
                dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccionGroupBySector(idsQueryString);
                //dtOrdenDeProduccionRecetas_recetas = cOrdenDeProduccion.GetAllIngredientesRecetasOrdenesProduccionGroupBySector(idsQueryString);

                //Esta funcion obtiene las fechas de entrega de todas las ordenes de produccion
                fechaOrdenDeProduccion = obtenerFechaOrden(idsQueryString);

                dtCombined = dt.Clone();
                dtCombined.Merge(dt);
                //dtCombined.Merge(dtOrdenDeProduccionRecetas_recetas);
                Nivel++;

                //Esta funcion obtiene las productos de nivel 2 en adelante de las ordenes de produccion
                ObtenerProductoDeLasRecetas(dtCombined, Nivel);
                //Esta funcion obtiene las productos de nivel 2 en adelante de las ordenes de produccion
                DataTable dtRecetas = ObtenerRecetasDeLasRecetas(dtCombined, Nivel);

                //Aca lo que hace es obtener todos los productos y recetas de todos los niveles que tienen todas las recetas 
                //De las ordenes de produccion seleccionadas
                while (dtRecetas != null && dtRecetas.Rows.Count != 0)
                {
                    Nivel++;
                    ObtenerProductoDeLasRecetas(dtRecetas, Nivel);
                    dtRecetas = ObtenerRecetasDeLasRecetas(dtRecetas, Nivel);
                }



                dt.DefaultView.Sort = "SectorProductivo ASC, DiasMasNivel DESC";
                dt = dt.DefaultView.ToTable(); 
                //dt = dtCombined;

                //if (dt.Rows.Count > 0)
                //{
                //    cambiarEstadoDeLaOrdenSeleccionada();
                //}

                dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);

                obtenerOPNumeros(idsQueryString);
            }
            catch (Exception ex)
            {
            }

        }

        //public void cambiarEstadoDeLaOrdenSeleccionada()
        //{
        //    ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
        //    string idOrdenDeProduccion = Request.QueryString["ids"];
        //    cOrdenDeProduccion.cambiarEstadoOrden(idOrdenDeProduccion);
        //}

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
                    if (dr["descripcion"].ToString() == "Receta")
                    {
                        int idReceta = Convert.ToInt32(dr["id"].ToString());
                        DataTable productosDeLaReceta = ObtenerProductosPorIdReceta(idReceta, Nivel);

                        if (productosDeLaReceta != null)
                        {
                            dt.Merge(productosDeLaReceta);
                        }
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
                if (dr["descripcion"].ToString() == "Receta")
                {

                    //int idReceta = obterneridRecetaByDescripcion(dr["ProductoOReceta"].ToString());
                    int idReceta = Convert.ToInt32(dr["id"].ToString());
                    RecetasDeLaReceta = ObtenerRecetasPorIdReceta(idReceta, Nivel);

                    if (RecetasDeLaReceta != null)
                    {
                        dt.Merge(RecetasDeLaReceta);
                    }
                }

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


        [WebMethod]
        public static string getProductoByidReceta(int id, int nivel)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dtProductos = cReceta.ObtenerProductosPorIdReceta(id, nivel);
                DataTable dtRecetas = cReceta.ObtenerRecetaPorIdReceta(id, nivel);
                string ProductosDeLaReceta = "";

                foreach (DataRow dr in dtProductos.Rows)
                {
                    string cantidad = dr["Cantidad"].ToString();
                    cantidad = cantidad.Replace(",", ".");

                    ProductosDeLaReceta += dr["id"].ToString() + "," + dr["SectorProductivo"].ToString() + "," + dr["ProductoOReceta"].ToString() + "," + dr["Dias"].ToString() +
                    "," + dr["Nivel"].ToString() + "," + dr["DiasMasNivel"].ToString() + "," + dr["Descripcion"].ToString() + "," + cantidad + ";";
                }

                foreach (DataRow drRecetas in dtRecetas.Rows)
                {
                    string cantidad = drRecetas["Cantidad"].ToString();
                    cantidad = cantidad.Replace(",", ".");

                    ProductosDeLaReceta += drRecetas["id"].ToString() + "," + drRecetas["SectorProductivo"].ToString() + "," + drRecetas["ProductoOReceta"].ToString() + "," + drRecetas["Dias"].ToString() +
                    "," + drRecetas["Nivel"].ToString() + "," + drRecetas["DiasMasNivel"].ToString() + "," + drRecetas["Descripcion"].ToString() + "," + cantidad + ";";
                }

                return ProductosDeLaReceta;

            }
            catch (Exception ex)
            {
                return null;
            }
        }




        [WebMethod]
        public static string getSectorProductivoByIdReceta(int idReceta)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getSectorProductivoByIdReceta(idReceta);
                string SectorProductivo = dt.Rows[0][9].ToString();
                string Cantidad = dt.Rows[0][5].ToString();
                return SectorProductivo + ";" + Cantidad;

            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [WebMethod]
        public static int cambiarEstadoDeLaOrden(string id, int estadoOrden)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            cOrdenDeProduccion.cambiarEstadoOrden(id, estadoOrden);

            return 1;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenesDeProduccion.aspx", false);
        }
    }
}