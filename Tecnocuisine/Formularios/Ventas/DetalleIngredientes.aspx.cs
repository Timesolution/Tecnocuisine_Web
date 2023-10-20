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
    public partial class DetalleIngredientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dtProductoOrdenes"];
            DataTable dt2 = (DataTable)Session["dtProductoRecetas"];

            cargarProductosEnPh(dt);
            obtenerProductosDeUnaReceta(dt2);

        }

        public void obtenerProductosDeUnaReceta(DataTable dtRecetas)
        {
            ControladorReceta cReceta = new ControladorReceta();
            List<Recetas_Producto> listProd = new List<Recetas_Producto>();

            foreach (DataRow receta in dtRecetas.Rows)
            {
                DataTable dt = cReceta.obtenerIdRecetaByDescripcionReceta(receta["Producto"].ToString());

                int idReceta = 0;
                idReceta = Convert.ToInt32(dt.Rows[0][0]);

                listProd.AddRange(cReceta.ObtenerProductosByReceta(Convert.ToInt16(idReceta)));


                // 

            }

            // Agrupa y suma dentro de la misma lista
            var grupos = listProd
                .GroupBy(producto => producto.idProducto)
                .Select(grupo => new
                {
                    IdProducto = grupo.Key,
                    SumaCantidad = grupo.Sum(producto => producto.cantidad)
                });

            foreach (var grupo in grupos)
            {
                foreach (Recetas_Producto producto in listProd.Where(p => p.idProducto == grupo.IdProducto))
                {
                    producto.cantidad = grupo.SumaCantidad;
                }
            }


            List<Recetas_Producto> listaGrupos = grupos.Select(grupo => new Recetas_Producto
            {
                idProducto = grupo.IdProducto,
                cantidad = grupo.SumaCantidad,
                // Aquí asigna otros valores necesarios o copia propiedades según tus necesidades
            }).ToList();


            // Ordena la lista por Receta_Producto.id de forma ascendente
            listProd = listProd.OrderBy(producto => producto.idProducto).ToList();

            //SumarCantidadAgrupadaPorDescripcion

            cargarProductosDeLasRecetasEnPh(listProd);
        }

        public void SumarCantidadAgrupadaPorDescripcion(List<Recetas_Producto> listProd)
        {
            try
            {   
                int PrimerVuelta = 0;
                string RecetaActual;
                foreach (Recetas_Producto producto in listProd)
                {
                    if (PrimerVuelta == 0) {
                        PrimerVuelta++;
                        RecetaActual = producto.Productos.descripcion;
                    }


                    
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void cargarProductosDeLasRecetasEnPh(List<Recetas_Producto> listProd)
        {
            try
            {
                int Cont = 0; //Esta variable contadora la uso para los ids de las filas 
                foreach (Recetas_Producto prod in listProd)
                {
                    Cont++;
                    //fila
                    TableRow tr = new TableRow();
                    tr.ID = Cont.ToString();


                    //TableCell celFecha = new TableCell();
                    ////celFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy");
                    //celFecha.Text = "01-01-2023";
                    //celFecha.VerticalAlign = VerticalAlign.Middle;
                    //celFecha.HorizontalAlign = HorizontalAlign.Left;
                    //celFecha.Width = Unit.Percentage(40);
                    //celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                    //tr.Cells.Add(celFecha);


                    TableCell celReceta = new TableCell();
                    celReceta.Text = prod.Productos.descripcion;
                    celReceta.VerticalAlign = VerticalAlign.Middle;
                    celReceta.HorizontalAlign = HorizontalAlign.Left;
                    celReceta.Width = Unit.Percentage(40);
                    celReceta.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celReceta);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = prod.cantidad.ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Right;
                    celCantidad.Width = Unit.Percentage(40);
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);

                    phProductos.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void cargarProductosEnPh(DataTable dt)
        {
            try
            {

                int Cont = 0; //Esta variable contadora la uso para los ids de las filas 
                foreach (DataRow dr in dt.Rows)
                {
                    Cont++;
                    //fila
                    TableRow tr = new TableRow();
                    tr.ID = Cont.ToString();


                    //TableCell celFecha = new TableCell();
                    //celFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy");
                    //celFecha.VerticalAlign = VerticalAlign.Middle;
                    //celFecha.HorizontalAlign = HorizontalAlign.Left;
                    //celFecha.Width = Unit.Percentage(40);
                    //celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                    //tr.Cells.Add(celFecha);


                    TableCell celReceta = new TableCell();
                    celReceta.Text = dr["Producto"].ToString();
                    celReceta.VerticalAlign = VerticalAlign.Middle;
                    celReceta.HorizontalAlign = HorizontalAlign.Left;
                    celReceta.Width = Unit.Percentage(40);
                    celReceta.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celReceta);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = dr["Kilogramos"].ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Right;
                    celCantidad.Width = Unit.Percentage(40);
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);

                    phProductos.Controls.Add(tr);

                }
            }
            catch (Exception ex)
            {


            }
        }

    }
}