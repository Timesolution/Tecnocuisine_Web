using Gestor_Solution.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using System.Text.RegularExpressions;
using Tecnocuisine_API.Entitys;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class OrdenesDeProduccionABM : System.Web.UI.Page
    {
        ControladorReceta ControladorReceta = new ControladorReceta();
        ControladorCliente controladorCliente = new ControladorCliente();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerRecetas();
                ObtenerClientes();
                txtFechaHoy.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (Request.QueryString["Accion"] != null)
                {
                    if (int.Parse(Request.QueryString["Accion"]) == 2)
                    {

                        if (Request.QueryString["id"] != null)
                        {
                            int idOrdenDeProduccion = int.Parse(Request.QueryString["id"]);
                            precargarOrdenDeProduccion(idOrdenDeProduccion);
                        }

                    }

                }

                else
                {
                    CargarCodigoOrdenCompra();
                }


            }


        }


        private void precargarOrdenDeProduccion(int idOrdenDeProduccion)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            ordenesDeProduccion ordenesDeProduccion = cOrdenDeProduccion.GetOneOrdenesDeProduccionById(idOrdenDeProduccion);
            txtFechaHoy.Text = Convert.ToDateTime(ordenesDeProduccion.fechaEntrega).ToString("dd/MM/yyyy");
            lblOPNumero.Text = ordenesDeProduccion.OPNumero;
            TxtClientes.Text = ordenesDeProduccion.idCliente.ToString() + " - " + ordenesDeProduccion.Clientes.alias + " - " + "Cliente";

            precargarRecetasDeLaOrden(idOrdenDeProduccion);

        }

        public void precargarRecetasDeLaOrden(int idOrdenDeProduccion)
        {
            ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
            List<ordenesXRecetas> listaRecetasXOrden = cOrdenDeProduccion.GetAllRecetasXOrdenProduccionByidOrdenDeProduccion(idOrdenDeProduccion);

            foreach (var RecetaXOrden in listaRecetasXOrden)
            {
                CargarOrdendesProduccionenPH(RecetaXOrden);
            }

        }



        private void CargarOrdendesProduccionenPH(ordenesXRecetas RecetaXOrden)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = RecetaXOrden.id.ToString();


                TableCell celProducto = new TableCell();
                celProducto.Text = RecetaXOrden.Producto.ToString();
                celProducto.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celProducto);


                TableCell celCantidad = new TableCell();
                celCantidad.Text = RecetaXOrden.cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.Attributes.Add("style", "text-align: right;");
                tr.Cells.Add(celCantidad);


                TableCell celAccion = new TableCell();

                Literal l2 = new Literal();
                l2.Text = "&nbsp";
                celAccion.Controls.Add(l2);

                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminarReceta_" + RecetaXOrden.id;
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.Text = "<span><i style='color:black' class='fa fa-trash - o'></i></span>";
                //btnEliminar.OnClientClick = "abrirdialog(" + RecetaXOrden.id + ");";
                btnEliminar.Attributes.Add("onclick", "borrarDocumentoSelect('ContentPlaceHolder1_" + RecetaXOrden.id.ToString() + "');");
                celAccion.Controls.Add(btnEliminar);

                tr.Cells.Add(celAccion);
                phRubroRecaudacionMensual.Controls.Add(tr);


                //DatosProductos.Text += "ID=" + RecetaXOrden.idReceta.ToString() + " - " + RecetaXOrden.Producto + " - " + "Receta" + "," + RecetaXOrden.cantidad + ";";
            //document.getElementById('<%= DatosProductos.ClientID%>').value += "ID=" + ID + "," + ProductoDescripcion + "," + Cantidad + ";"


            }
            catch (Exception ex)
            {

            }
        }
        private void CargarCodigoOrdenCompra()
        {
            try
            {
                ControladorOrdenDeProduccion cOrdenDeProduccion = new ControladorOrdenDeProduccion();
                var listaOrdeneProduccion = cOrdenDeProduccion.GetAllOrdenesDeProduccion();

                string fac1;
                if (listaOrdeneProduccion.Count == 0)
                {
                    lblOPNumero.Text = "#000001";
                }
                else
                {
                    string codigo = (listaOrdeneProduccion.Count + 1).ToString();
                    fac1 = cOrdenDeProduccion.GenerarCodigoPedido(codigo);


                    lblOPNumero.Text = "#" + fac1;


                }
            }
            catch (Exception ex)
            {
            }

        }

        private void ObtenerClientes()
        {
            try
            {
                var Clientes = controladorCliente.ObtenerTodosClientes();


                if (Clientes.Count > 0)
                {
                    CargarClientesOptions(Clientes);
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void CargarClientesOptions(List<Tecnocuisine_API.Entitys.Clientes> clientes)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var cliente in clientes)
                {


                    builder.Append(String.Format("<option value='{0}' id='c_r_" + cliente.id + "_" + cliente.alias + "_" + cliente.cuit + "'>", cliente.id + " - " + cliente.alias + " - " + "Cliente"));

                }



                ListaNombreCliente.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }
        private void ObtenerRecetas()
        {
            try
            {
                var Recetas = ControladorReceta.ObtenerTodosRecetas();


                if (Recetas.Count > 0)
                {
                    CargarRecetasOptions(Recetas);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarRecetasOptions(List<Tecnocuisine_API.Entitys.Recetas> recetas)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in recetas)
                {
                    string UnidadMedida = "";
                    if (rec.UnidadMedida != null)
                    {
                        if (rec.UnidadMedida.Value == -1)
                            rec.UnidadMedida = 1;

                        UnidadMedida = cu.ObtenerUnidadId(rec.UnidadMedida.Value).descripcion;
                        builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + UnidadMedida + "_" + rec.Costo.ToString().Replace(',', '.') + "'>", rec.id + " - " + rec.descripcion + " - " + "Receta"));
                    }
                    else
                    {

                    }
                }



                ListaNombreProd.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnGuardarOrdenDeCompra_Click(object sender, EventArgs e)
        {

            Tecnocuisine_API.Entitys.ordenesDeProduccion ordDeProduccion = new Tecnocuisine_API.Entitys.ordenesDeProduccion();
            ordDeProduccion.OPNumero = lblOPNumero.Text;
            ordDeProduccion.fechaEntrega = Convert.ToDateTime(fechaEntrega.Text.ToString());

            //string clienteTexto = TxtClientes.Text;
            string clienteTexto = Cliente.Text;
            int numeroCliente;
            string[] partes = clienteTexto.Split('-');
            if (partes.Length >= 2)
            {
                if (int.TryParse(partes[0].Trim(), out numeroCliente))
                {
                    // Ahora tienes el número del cliente en la variable numeroCliente
                    // Puedes usarlo en tu código
                    ordDeProduccion.idCliente = numeroCliente;
                }
            }
            ordDeProduccion.Estado = true;
            ControladorOrdenDeProduccion controladorOrdenDeProduccion = new ControladorOrdenDeProduccion();
            int idOrdenDeProduccion = controladorOrdenDeProduccion.AgregarOrdenDeProduccion(ordDeProduccion);
            ControladorOrdenesxRecetas cOrdenesxRecetas = new ControladorOrdenesxRecetas();
            //cOrdenesxRecetas.AgregarOrdenesxRecetas()

            string texto = DatosProductos.Text;
            string[] cadenas = texto.Split(';'); // Divide el texto en cadenas usando ';' como delimitador

            // Ahora 'cadenas' es un arreglo que contiene las cadenas separadas por ';'

            // Si quieres almacenarlas en una lista, puedes hacerlo así:
            List<string> listaCadenas = cadenas.ToList();

            // Puedes recorrer la lista o el arreglo para trabajar con cada cadena individualmente:
            foreach (string cadena in listaCadenas)
            {
                // Dividir la cadena en partes usando '-' como delimitador
                string[] elementos = cadena.Split('-');

                if (elementos.Length >= 2)
                {
                    // elementos[0] contiene el primer número (7)
                    string primerElemento = elementos[0].Trim();
                    int comaIndex = primerElemento.IndexOf(",");
                    string[] partesDelPrimerElemento = primerElemento.Split(',');
                    string numeroDespuesDeLaComa = partesDelPrimerElemento[1].Trim();
                    int numero = int.Parse(numeroDespuesDeLaComa);
                    string Producto = elementos[1].Trim();
                    string TercerElemento = elementos[2].Trim();

                    int idReceta = Convert.ToInt32(numero);
                    // Definir una expresión regular para buscar números en la cadena
                    Regex regex = new Regex(@"\d+");



                    // Buscar coincidencias en la cadena
                    Match match = regex.Match(TercerElemento);
                    int Cantidad = int.Parse(match.Value);
                    ordenesXRecetas ordenesXRecetas = new ordenesXRecetas();
                    ordenesXRecetas.idReceta = idReceta;
                    ordenesXRecetas.idOrdenDeProduccion = idOrdenDeProduccion;
                    ordenesXRecetas.Estado = true;
                    ordenesXRecetas.cantidad = Cantidad;
                    ordenesXRecetas.Producto = Producto;
                    cOrdenesxRecetas.AgregarOrdenesxRecetas(ordenesXRecetas);
                    DatosProductos.Text = "";

                }
            }

            Response.Redirect("OrdenesDeProduccion.aspx", false);

        }
    }
}