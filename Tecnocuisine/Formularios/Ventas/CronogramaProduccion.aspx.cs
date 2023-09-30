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
    public partial class CronogramaProduccion : System.Web.UI.Page
    {
        public DataTable dt;
        public DataTable dtCantidadRecetasPorCadaOrden;
        public DataTable dtSinDuplicados = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            string idsQueryString = Request.QueryString["ids"];

            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            dt = cOrdenDeProduccion.GetAllIngredientesOrdenesProduccion(idsQueryString);

            dtCantidadRecetasPorCadaOrden = cOrdenDeProduccion.GetAllCantidadProductosGroupByProductoColumn(idsQueryString);


            //DataTable dtSinDuplicados = new DataTable();

            // Agregar las mismas columnas a la nueva DataTable
            //foreach (DataColumn col in dt.Columns)
            //{
            //    dtSinDuplicados.Columns.Add(col.ColumnName, col.DataType);
            //}

            //// Utilizar LINQ para filtrar las filas sin duplicados
            //var filasSinDuplicados = dt.AsEnumerable()
            //    .GroupBy(row => new
            //    {
            //        Descripcion = row.Field<string>("descripcion1"),
            //        Tiempo = row.Field<int>("Tiempo")
            //    })
            //    .Select(group => group.First());

            //// Agregar las filas sin duplicados a la nueva DataTable
            //foreach (var fila in filasSinDuplicados)
            //{
            //    dtSinDuplicados.ImportRow(fila);
            //}


            //var resultado = dtSinDuplicados.AsEnumerable()
            //.GroupBy(row => row.Field<string>("Producto"))
            //.Select(group => new
            //{
            //    Producto = group.Key,
            //    CantidadTotal = group.Sum(row => row.Field<int>("Cantidad"))
            //});


            MostrarIDSDeOrdenesDeProduccion();
        }

        public void MostrarIDSDeOrdenesDeProduccion()
        {
            string idsQueryString = Request.QueryString["ids"];
            idsQueryString = idsQueryString.TrimStart(',');
            lblIdsQueryString.Text = idsQueryString;
        }
    }
}