using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Formularios.Administrador;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static Tecnocuisine.Formularios.Ventas.PedidosOrdenes;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class Pre_Produccion : System.Web.UI.Page
    {

        ControladorSectorProductivo sectorProductivo = new ControladorSectorProductivo();
        string FechaD = "";
        string FechaH = "";
        int idSectorProductivo = -1;
        DataTable dt;
        DataTable dt2;
        DataTable dtAuxiliar = new DataTable();
        int ultimoID = -1;
        string sector = "";
        string sectorValue = "";
        public class RowClass
        {
            public string Id { get; set; }
            public string sectorOrigen { get; set; }
            public string sectorDestino { get; set; }
            public string producto { get; set; }
            public string cantidad { get; set; }
            public string cantidadConfirmada { get; set; }
            public string cantidadEnviada { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            CargarSectoresProductivodDDL();
            filtrarDatosTransferenciasBySectorOrigen();
            filtrarProduccionBySector();
            if (!IsPostBack)
            {
                filtrarRemitosInternos();
            }
        }

        public void filtrarProduccionBySector()
        {
            if (Request.QueryString["O"] != null)
            {
                sector = Request.QueryString["O"].ToString();
                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                DataTable dt = cTransferencia.getTransferenciasBySector(sector);

                int cont = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    cont++;
                    TableRow tr = new TableRow();
                    tr.ID = cont.ToString();

                    //Celdas
                    TableCell celProducto = new TableCell();
                    celProducto.Text = dr["productoOrigen"].ToString();
                    celProducto.VerticalAlign = VerticalAlign.Middle;
                    celProducto.HorizontalAlign = HorizontalAlign.Left;
                    celProducto.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celProducto);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = dr["cantidadOrigen"].ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Left;
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);


                    phProduccion.Controls.Add(tr);
                }

            }

        }

        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Usuario/Login.aspx");
                }
                else
                {
                    if (this.verificarAcceso() != 1)
                    {
                        Response.Redirect("/Default.aspx?m=1", false);
                    }
                }
            }
            catch
            {
                Response.Redirect("../../Account/Login.aspx");
            }
        }

        private int verificarAcceso()
        {
            try
            {
                int valor = 1;
                string permisos = Session["Login_Permisos"] as string;
                string[] listPermisos = permisos.Split(';');

                string permiso = listPermisos.Where(x => x == "215").FirstOrDefault();

                if (!string.IsNullOrEmpty(permiso))
                    valor = 1;

                return valor;
            }
            catch
            {
                return -1;
            }
        }

        //Esta funcion filtra los remitos internos en la pestaña de recepcion
        public void filtrarRemitosInternos()
        {
            if (Request.QueryString["O"] != null)
            {
                sector = Request.QueryString["O"].ToString();
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                var remitosInternos = cRemitosInternos.getRemitosInternosBySectorDestino(sector);


                //Session["remitosInternosRecepcion"] = remitosInternos;

                foreach (var remito in remitosInternos)
                {

                    int cont = 0;

                    cont++;
                    TableRow tr = new TableRow();
                    tr.ID = cont.ToString();

                    //Celdas
                    TableCell celSectorDestino = new TableCell();
                    celSectorDestino.Text = remito.sectorDestino;
                    celSectorDestino.VerticalAlign = VerticalAlign.Middle;
                    celSectorDestino.HorizontalAlign = HorizontalAlign.Left;
                    celSectorDestino.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celSectorDestino);


                    TableCell celNumero = new TableCell();
                    celNumero.Text = remito.numero;
                    celNumero.VerticalAlign = VerticalAlign.Middle;
                    celNumero.HorizontalAlign = HorizontalAlign.Left;
                    celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celNumero);



                    TableCell celFecha = new TableCell();
                    celFecha.Text = remito.fecha.ToString();
                    celFecha.VerticalAlign = VerticalAlign.Middle;
                    celFecha.HorizontalAlign = HorizontalAlign.Left;
                    celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celFecha);



                    TableCell celAccion = new TableCell();
                    LinkButton btnDetalle = new LinkButton();
                    btnDetalle.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                    btnDetalle.CssClass = "btn btn-xs";
                    btnDetalle.Style.Add("background-color", "transparent");
                    btnDetalle.Attributes.Add("data-toggle", "modal");
                    btnDetalle.Attributes.Add("href", "#modalConfirmacion2");
                    btnDetalle.Text = "<span title='Ver pedidos'><i class='fa fa-search' style='color: black;'></i></span>";
                    btnDetalle.Attributes.Add("onclick", "verDetalleRemitoInternoPdf('" + remito.id + "');");
                    celAccion.Controls.Add(btnDetalle);
                    tr.Cells.Add(celAccion);




                    LinkButton btnDetalleRemitoInterno = new LinkButton();
                    btnDetalleRemitoInterno.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                    btnDetalleRemitoInterno.CssClass = "btn btn-xs";
                    btnDetalleRemitoInterno.Style.Add("background-color", "transparent");
                    btnDetalleRemitoInterno.Attributes.Add("data-toggle", "modal");
                    btnDetalleRemitoInterno.Attributes.Add("href", "#modalConfirmacion2");
                    btnDetalleRemitoInterno.Text = "<span title='Ver pedidos'><i class='fa fa-exchange' style='color: black;'></i></span>";
                    btnDetalleRemitoInterno.Attributes.Add("onclick", "verDetalleRemitoInternoModal('" + remito.id + "');");
                    celAccion.Controls.Add(btnDetalleRemitoInterno);

                    phRecepcion.Controls.Add(tr);
                }



            }

        }

        public void filtrarDatosTransferenciasBySectorOrigen()
        {
            if (Request.QueryString["O"] != null)
            {
                sector = Request.QueryString["O"].ToString();
                sectorValue = Request.QueryString["OV"].ToString();
                cargarHiddenFields();
                //DataTable datosTransferencias = obtenerDatosTranferencias();
                //Dictionary<string, DataTable> datosSeparados = separarDatosTransferenciaPorSectorDestino(datosTransferencias);
                //controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
                //DataTable datosTransferencias = cDatosTransferencias.getDatosTransferenciaGroupBySectorDestinoBySectorOrigen(sector);


                controladorDatosTransferencias cTransferencias = new controladorDatosTransferencias();
                DataTable dt = cTransferencias.getDatosTransferenciaGroupBySectorDestino(sector);

                cargarDatosTransferenciasEnPh(dt);
            }

        }


        public Dictionary<string, DataTable> separarDatosTransferenciaPorSectorDestino(DataTable datosTransferencias)
        {

            Dictionary<string, DataTable> datosSeparados = new Dictionary<string, DataTable>();

            foreach (DataRow row in datosTransferencias.Rows)
            {
                string sectorDestino = row["sectorDestino"].ToString();

                if (!datosSeparados.ContainsKey(sectorDestino))
                {
                    DataTable nuevaTabla = datosTransferencias.Clone();
                    datosSeparados.Add(sectorDestino, nuevaTabla);
                }

                datosSeparados[sectorDestino].ImportRow(row);
            }

            return datosSeparados;

        }

        public void cargarHiddenFields()
        {
            try
            {
                //bool itemSeleccionado = false; // Variable para controlar si se ha seleccionado un elemento

                //// Recorre cada elemento del DropDownList
                //foreach (ListItem item in ddlSector.Items)
                //{
                //    // Comprueba si el texto del elemento coincide con el valor de la variable sector
                //    if (item.Text == this.sector)
                //    {
                //        // Si hay coincidencia, establece el elemento como seleccionado
                //        item.Selected = true;
                //        itemSeleccionado = true; // Marca que se ha seleccionado un elemento
                //        break; // Sal del bucle una vez que se haya encontrado la coincidencia
                //    }
                //}

                //// Si no se ha seleccionado ningún elemento y el DropDownList tiene al menos un elemento, selecciona el primero por defecto
                //if (!itemSeleccionado && ddlSector.Items.Count > 0)
                //{
                //    ddlSector.SelectedIndex = -1;
                //}

                ddlSectorSelecionadoValue.Value = sectorValue;
                ddlSector.SelectedValue = sectorValue;

            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
        }


        public void cargarDatosTransferenciasEnPh(Dictionary<string, DataTable> datosSeparados)
        {
            int cont = 0;
            foreach (var kvp in datosSeparados)
            {
                cont++;
                TableRow tr = new TableRow();
                tr.ID = cont.ToString();

                ////Celdas
                TableCell celFecha = new TableCell();
                celFecha.Text = kvp.Key.ToString();
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);


                //TableCell celProducto = new TableCell();
                //celProducto.Text = datos.productoOrigen.ToString();
                //celProducto.VerticalAlign = VerticalAlign.Middle;
                //celProducto.HorizontalAlign = HorizontalAlign.Left;
                //celProducto.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProducto);


                //TableCell celCantidadConfirmada = new TableCell();
                //celCantidadConfirmada.Text = datos.CantidadAConfirmar.ToString();
                //celCantidadConfirmada.VerticalAlign = VerticalAlign.Middle;
                //celCantidadConfirmada.HorizontalAlign = HorizontalAlign.Left;
                //celCantidadConfirmada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celCantidadConfirmada);



                //TableCell celCantidadEnviada = new TableCell();
                //TextBox cantidadAConfirmarTextBox = new TextBox();
                //cantidadAConfirmarTextBox.Style["width"] = "100%";
                //cantidadAConfirmarTextBox.Style["text-align"] = "right";
                //cantidadAConfirmarTextBox.Attributes["placeholder"] = "Cantidad";
                //if (datos.cantidadEnviada == null)
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = datos.CantidadAConfirmar.ToString();

                //}
                //else
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = datos.cantidadEnviada.ToString();

                //}
                //cantidadAConfirmarTextBox.Attributes["oninput"] = "validarTextBox(this);";
                //celCantidadEnviada.Controls.Add(cantidadAConfirmarTextBox);
                //celCantidadEnviada.VerticalAlign = VerticalAlign.Middle;
                //celCantidadEnviada.HorizontalAlign = HorizontalAlign.Left;
                //celCantidadEnviada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celCantidadEnviada);


                phEnvio.Controls.Add(tr);
            }
        }


        public void cargarDatosTransferenciasEnPh(DataTable datosTransferencias)
        {
            int cont = 0;
            foreach (DataRow dr in datosTransferencias.Rows)
            {
                cont++;
                TableRow tr = new TableRow();
                tr.ID = cont.ToString();

                //Celdas
                TableCell celSectorOrigen = new TableCell();
                celSectorOrigen.Text = dr["sectorOrigen"].ToString();
                celSectorOrigen.VerticalAlign = VerticalAlign.Middle;
                celSectorOrigen.HorizontalAlign = HorizontalAlign.Left;
                celSectorOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celSectorOrigen);


                TableCell celSectorDestino = new TableCell();
                celSectorDestino.Text = dr["sectorDestino"].ToString();
                celSectorDestino.VerticalAlign = VerticalAlign.Middle;
                celSectorDestino.HorizontalAlign = HorizontalAlign.Left;
                celSectorDestino.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celSectorDestino);


                //TableCell celProducto = new TableCell();
                //celProducto.Text = dr["cantidadConfirmada"].ToString();
                //celProducto.VerticalAlign = VerticalAlign.Middle;
                //celProducto.HorizontalAlign = HorizontalAlign.Left;
                //celProducto.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celProducto);


                //TableCell celCantidadConfirmada = new TableCell();
                //celCantidadConfirmada.Text = dr["cantidadEnviada"].ToString();
                //celCantidadConfirmada.VerticalAlign = VerticalAlign.Middle;
                //celCantidadConfirmada.HorizontalAlign = HorizontalAlign.Left;
                //celCantidadConfirmada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celCantidadConfirmada);


                //if (dr["cantidadEnviada"] == null || dr["cantidadEnviada"].ToString() == "")
                //{

                //    TableCell celCantidadEnviada = new TableCell();
                //    celCantidadEnviada.Text = dr["cantidad"].ToString();
                //    celCantidadEnviada.VerticalAlign = VerticalAlign.Middle;
                //    celCantidadEnviada.HorizontalAlign = HorizontalAlign.Left;
                //    celCantidadEnviada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //    tr.Cells.Add(celCantidadEnviada);
                //}
                //else
                //{
                //    TableCell celCantidadEnviada = new TableCell();
                //    celCantidadEnviada.Text = dr["cantidadEnviada"].ToString();
                //    celCantidadEnviada.VerticalAlign = VerticalAlign.Middle;
                //    celCantidadEnviada.HorizontalAlign = HorizontalAlign.Left;
                //    celCantidadEnviada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //    tr.Cells.Add(celCantidadEnviada);
                //}



                //TableCell celCantidadEnviada = new TableCell();
                //TextBox cantidadAConfirmarTextBox = new TextBox();
                ////cantidadAConfirmarTextBox.ID = "cantAproducir_" + cont;
                //cantidadAConfirmarTextBox.Style["width"] = "100%";
                //cantidadAConfirmarTextBox.Style["text-align"] = "right";
                //cantidadAConfirmarTextBox.Attributes["placeholder"] = "Cantidad";
                //if (dr["cantidadEnviada"] == null || dr["cantidadEnviada"].ToString() == "")
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = dr["cantidad"].ToString();

                //}
                //else
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = dr["cantidadEnviada"].ToString(); ;

                //}
                //cantidadAConfirmarTextBox.Attributes["oninput"] = "validarTextBox(this);";
                //celCantidadEnviada.Controls.Add(cantidadAConfirmarTextBox);
                //celCantidadEnviada.VerticalAlign = VerticalAlign.Middle;
                //celCantidadEnviada.HorizontalAlign = HorizontalAlign.Left;
                //celCantidadEnviada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celCantidadEnviada);



                //TableCell celCantidadEnviada = new TableCell();
                //TextBox cantidadAConfirmarTextBox = new TextBox();
                ////cantidadAConfirmarTextBox.ID = "cantAproducir_" + cont;
                //cantidadAConfirmarTextBox.Style["width"] = "100%";
                //cantidadAConfirmarTextBox.Style["text-align"] = "right";
                //cantidadAConfirmarTextBox.Attributes["placeholder"] = "Cantidad";
                //if (datos.cantidadEnviada == null)
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = datos.CantidadAConfirmar.ToString();

                //}
                //else
                //{
                //    cantidadAConfirmarTextBox.Attributes["value"] = datos.cantidadEnviada.ToString();

                //}
                //cantidadAConfirmarTextBox.Attributes["oninput"] = "validarTextBox(this);";
                //celCantidadEnviada.Controls.Add(cantidadAConfirmarTextBox);
                //celCantidadEnviada.VerticalAlign = VerticalAlign.Middle;
                //celCantidadEnviada.HorizontalAlign = HorizontalAlign.Left;
                //celCantidadEnviada.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celCantidadEnviada);



                //TableCell celEstadoTransferencia = new TableCell();
                //celEstadoTransferencia.Text = item.estadoTransferencias.descripcion;
                //celEstadoTransferencia.VerticalAlign = VerticalAlign.Middle;
                //celEstadoTransferencia.HorizontalAlign = HorizontalAlign.Left;
                //celEstadoTransferencia.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celEstadoTransferencia);


                TableCell celAccion = new TableCell();
                LinkButton btnDetalle = new LinkButton();
                btnDetalle.ID = "btnVerPedidos_" + cont.ToString();
                btnDetalle.CssClass = "btn btn-xs";
                btnDetalle.Style.Add("background-color", "transparent");
                btnDetalle.Attributes.Add("data-toggle", "modal");
                btnDetalle.Attributes.Add("href", "#modalConfirmacion2");
                btnDetalle.Text = "<span title='Ver pedidos'><i class='fa fa-exchange' style='color: black;'></i></span>";
                btnDetalle.Attributes.Add("onclick", "getDatosTransferenciaBySectorDestino('" + dr["sectorOrigen"].ToString() + "', '" + dr["sectorDestino"].ToString() + "');");
                celAccion.Controls.Add(btnDetalle);


                tr.Cells.Add(celAccion);

                //transferencias.Value += item.fecha.ToString() + "," +
                //item.origen.ToString() + "," + item.destino.ToString() + "," +
                //item.orden.ToString() + "," + item.estadoTransferencias.descripcion + ";";

                phEnvio.Controls.Add(tr);
            }
        }


        public List<Tecnocuisine_API.Entitys.PedidosInternos> obtenerDatosTranferencias(string sector)
        {
            Tecnocuisine_API.Entitys.Transferencia transferencia = new Tecnocuisine_API.Entitys.Transferencia();
            ControladorTransferencia cTransferencia = new ControladorTransferencia();
            var datosTransferencias = cTransferencia.obtenerDatosTrasferenciaBySectorOrigen(sector);
            return datosTransferencias;
        }


        public DataTable obtenerDatosTranferencias()
        {
            Tecnocuisine_API.Entitys.Transferencia transferencia = new Tecnocuisine_API.Entitys.Transferencia();
            ControladorTransferencia cTransferencia = new ControladorTransferencia();
            var datosTransferencias = cTransferencia.getTransferenciaDatos(sector);
            return datosTransferencias;
        }


        public DataTable ObtenerRecetasPorIdReceta(int idReceta, int Nivel, DateTime fechaEntrega, int idFila, int ultimoID)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtRecetas = cReceta.getRecetaPorIdReceta(idReceta, Nivel, fechaEntrega, idFila, ultimoID);
            return dtRecetas;
        }


        public void ObtenerProductoDeLasRecetas(DataTable dtRecetas, int Nivel, int idFila, int ultimo_id)
        {
            try
            {
                foreach (DataRow dr in dtRecetas.Rows)
                {
                    if (dr["ProducoOreceta"].ToString() == "Receta")
                    {
                        int idReceta = Convert.ToInt32(dr["id"].ToString());
                        DataTable productosDeLaReceta = ObtenerProductosPorIdReceta(idReceta, Nivel, Convert.ToDateTime(dr["fechaEntrega"].ToString()), Convert.ToInt32(dr["idReceta"].ToString()), Convert.ToInt32(dr["idFila"].ToString()), ultimo_id);

                        if (productosDeLaReceta != null)
                        {
                            ultimoID = Convert.ToInt32(productosDeLaReceta.Rows[productosDeLaReceta.Rows.Count - 1][0]);
                            dtAuxiliar.Merge(productosDeLaReceta);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }

        public DataTable ObtenerProductosPorIdReceta(int idReceta, int Nivel, DateTime fechaEntrega, int id, int idFila, int ultimoID)
        {
            ControladorReceta cReceta = new ControladorReceta();
            DataTable dtProductos = cReceta.getProductosPorIdReceta(idReceta, Nivel, fechaEntrega, id, idFila, ultimoID);
            return dtProductos;
        }


        public DataTable obtenerRecetasDeLasRecetas(string ProductoOreceta, DateTime fechaEntrega, int id, int idFila, int ultimoID)
        {

            DataTable RecetasDeLaReceta = null;

            if (ProductoOreceta == "Receta")
            {

                int idReceta = id;
                RecetasDeLaReceta = ObtenerRecetasPorIdReceta(idReceta, 2, fechaEntrega, idFila, ultimoID);

                if (RecetasDeLaReceta != null && RecetasDeLaReceta.Rows.Count > 0)
                {
                    dtAuxiliar.Merge(RecetasDeLaReceta);
                    return RecetasDeLaReceta;
                }

            }

            return null;


        }



        private string ConvertDateFormat(string fecha)
        {

            DateTime fechaConvertida;
            try
            {

                if (DateTime.TryParseExact(fecha, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaConvertida))
                {
                    return fechaConvertida.ToString("MM/dd/yyyy");
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
                throw new ArgumentException("El formato de fecha proporcionado es inválido.");
            }
        }

        protected void CargarSectoresProductivodDDL()
        {
            try
            {
                DataTable dt = sectorProductivo.GetAllSectoresProductivosDT();
                DataRow drSeleccione = dt.NewRow();
                drSeleccione["descripcion"] = "Seleccione...";
                drSeleccione["id"] = -1;
                dt.Rows.InsertAt(drSeleccione, 0);

                // Ordenar los datos alfabéticamente por la descripción (ignorando la primera fila)
                var sortedData = dt.AsEnumerable()
                                   .Skip(1) // Ignorar la primera fila ("Seleccione...")
                                   .OrderBy(row => row.Field<string>("descripcion"));

                // Crear una nueva tabla con los elementos ordenados y el elemento inicial
                DataTable sortedTable = dt.Clone();
                sortedTable.Rows.Add(drSeleccione.ItemArray);
                foreach (DataRow row in sortedData)
                {
                    sortedTable.ImportRow(row);
                }

                this.ddlSector.DataSource = sortedTable;
                this.ddlSector.DataValueField = "id";
                this.ddlSector.DataTextField = "descripcion";

                this.ddlSector.DataBind();

                this.ddlSector.SelectedValue = "-1";

            }
            catch (Exception ex)
            {


            }
        }

        //protected void DetalleIngredientes_Click(object sender, EventArgs e)
        //{
        //    Session["dtProductoOrdenes"] = dt;
        //    Session["dtProductoRecetas"] = dt2;
        //    //Response.Redirect("DetalleIngredientes.aspx", false);


        //    string url = "DetalleIngredientes.aspx";
        //    string script = "window.open('" + url + "', '_blank');";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "openNewTab", script, true);
        //}



        [WebMethod]
        public static string getIngredientesRecetaByIdReceta(int idReceta, decimal cantidad)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.obtenerIngredientePorIdReceta(idReceta, cantidad);
                string ingredientesDeLaReceta = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string Cantidad = dr["Cantidad"].ToString();
                    Cantidad = Cantidad.Replace(",", ".");

                    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + ";";
                }
                return ingredientesDeLaReceta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Esta funcion obtiene los ingredientes de una receta y el sector al que pertenece cada ingrediente
        [WebMethod]
        public static string getIngredientesRecetaByIdRecetaSectorProductivo(int idReceta, decimal cantidad)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.obtenerIngredientePorIdRecetaSectorProductivo(idReceta, cantidad);
                string ingredientesDeLaReceta = "";

                string hola = HttpContext.Current.Session["DatosReceta"] as string;

                foreach (DataRow dr in dt.Rows)
                {
                    string Cantidad = dr["Cantidad"].ToString();
                    Cantidad = Cantidad.Replace(",", ".");

                    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + "," + dr["SectorProductivo"].ToString() + ";";
                }
                return ingredientesDeLaReceta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static string GetProductosEnRecetas(string idsRecetas)
        {
            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idsRecetas));

                List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idsRecetas));
                if (listProd.Count > 0)
                {
                    foreach (var item in listProd)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(item.idProducto);
                        if (stock != null)
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + "," + item.Productos.costo.ToString().Replace(',', '.') + "; ";
                        }
                        else
                        {
                            ListFinal += item.idProducto + "," + item.Productos.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(item.Productos.unidadMedida).descripcion + "," + "Producto" + "," + item.Productos.costo.ToString().Replace(',', '.') + ";";

                        }
                    }
                }
                if (listRecetas.Count > 0)
                {
                    foreach (var item in listRecetas)
                    {
                        var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                        if (receta != null)
                        {
                            //item.Recetas.id
                            //item.Recetas.descripcion
                            //item.cantidad
                            //item.Recetas.CostoU


                            var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                            decimal stock;
                            if (CantStock == null)
                            {
                                stock = 0;
                            }
                            else
                            {

                                stock = (decimal)CantStock.stock;
                            }
                            if (item.Recetas.UnidadMedida == null)
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + stock.ToString().Replace(",", ".") + "," + item.cantidad.ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";

                            }
                            else
                            {
                                ListFinal += receta.id + "," + receta.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                            }
                        }
                    }
                }
                return ListFinal;

            }
            catch (Exception ex)
            {
                return "";
            }
        }


        [WebMethod]
        public static string getRecetasEntrega(string idsRecetas, string recetasSeleccionadas, int idReceta)
        {
            try
            {
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getIdrecetasEntrega(idsRecetas, idReceta);
                //string ingredientesDeLaReceta = "";
                string valorCelda = dt.Rows[0][0].ToString();
                valorCelda = valorCelda.TrimEnd(',');
                //idsRecetas = idsRecetas.TrimEnd(',');

                string[] vectorIdsRecetas = valorCelda.Split(',');
                //string[] elementosSeparadosPuntoComa = recetasSeleccionadas.Split(';');

                string[] elementosSeparadosPuntoComa = recetasSeleccionadas.Split(';')
                .Where(elemento => !string.IsNullOrWhiteSpace(elemento))
                .ToArray();


                string nuevaCedena = "";



                //foreach (string idRecetaStr in vectorIdsRecetas)
                //{
                //    int idRecetaInt = Convert.ToInt32(idRecetaStr);
                //    // Ahora puedes trabajar con idRecetaInt

                foreach (string elementoPuntoComa in elementosSeparadosPuntoComa)
                {
                    string[] elementosSeparadosComa = elementoPuntoComa.Split(',');
                    bool existeReceta = false;
                    for (int i = 0; i < vectorIdsRecetas.Length; i++)
                    {
                        if (elementosSeparadosComa[1].ToString() == vectorIdsRecetas[i])
                        {
                            existeReceta = true;
                        }
                    }

                    if (existeReceta == true)
                    {
                        nuevaCedena += elementoPuntoComa + ";";
                    }

                }

                //}

                //string hola = HttpContext.Current.Session["DatosReceta"] as string;

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string Cantidad = dr["Cantidad"].ToString();
                //    Cantidad = Cantidad.Replace(",", ".");

                //    ingredientesDeLaReceta += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + Cantidad + "," + dr["SectorProductivo"].ToString() + ";";
                //}
                return nuevaCedena;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static string getProductoSeleccionado(string idsRecetas)
        {
            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorReceta cReceta = new ControladorReceta();

                DataTable dt = cReceta.ObtenerProductosByRecetaDT(idsRecetas);
                DataTable dt2 = cReceta.obtenerRecetasbyRecetaDT(idsRecetas);

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                //List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idProd));

                HttpContext.Current.Session["DatosReceta"] = "hola";

                //List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idsRecetas));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(Convert.ToInt32(dr["idProducto"].ToString()));
                        if (stock != null)
                        {
                            //ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["cantidad"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                        }
                        else
                        {
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                        }
                    }
                }
                //if (listRecetas.Count > 0)
                //{
                foreach (DataRow dr in dt2.Rows)
                {
                    //var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                    //if (receta != null)
                    //{
                    //var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                    var CantStock = controladorStockReceta.ObtenerStockReceta(Convert.ToInt32((dr["id"].ToString())));
                    decimal stock;
                    if (CantStock == null)
                    {
                        stock = 0;
                    }
                    else
                    {
                        stock = (decimal)CantStock.stock;
                    }
                    if (dr["UnidadMedida"].ToString() == null)
                    {
                        //  ListFinal += receta.id + "," + receta.descripcion + "," + stock.ToString().Replace(",", ".") + "," + item.cantidad.ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + stock.ToString().Replace(",", ".") + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                    }
                    else
                    {
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32((dr["UnidadMedida"].ToString()))).descripcion + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";
                        //ListFinal += item.Recetas.id + "," + item.Recetas.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                    }
                    //}
                    //}
                }
                return ListFinal;

                //return "hola";

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public static string GetProductosEnRecetas2(string idsRecetas)
        {
            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorReceta cReceta = new ControladorReceta();

                DataTable dt = cReceta.ObtenerProductosByRecetaDT(idsRecetas);
                DataTable dt2 = cReceta.obtenerRecetasbyRecetaDT(idsRecetas);

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorStockReceta controladorStockReceta = new ControladorStockReceta();
                string ListFinal = "";
                //List<Recetas_Producto> listProd = controladorReceta.ObtenerProductosByReceta(Convert.ToInt16(idProd));

                HttpContext.Current.Session["DatosReceta"] = "hola";

                //List<Recetas_Receta> listRecetas = controladorReceta.obtenerRecetasbyReceta(Convert.ToInt16(idsRecetas));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var stock = controladorStockProducto.ObtenerStockProducto(Convert.ToInt32(dr["idProducto"].ToString()));
                        if (stock != null)
                        {
                            //ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["cantidad"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().ToString().Replace(',', '.') + "; ";
                        }
                        else
                        {
                            ListFinal += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + 0 + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32(dr["UnidadMedida"].ToString())).descripcion + "," + "Producto" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                        }
                    }
                }
                //if (listRecetas.Count > 0)
                //{
                foreach (DataRow dr in dt2.Rows)
                {
                    //var receta = controladorReceta.ObtenerRecetaId(item.idRecetaIngrediente);
                    //if (receta != null)
                    //{
                    //var CantStock = controladorStockReceta.ObtenerStockReceta(item.idRecetaIngrediente);
                    var CantStock = controladorStockReceta.ObtenerStockReceta(Convert.ToInt32((dr["id"].ToString())));
                    decimal stock;
                    if (CantStock == null)
                    {
                        stock = 0;
                    }
                    else
                    {
                        stock = (decimal)CantStock.stock;
                    }
                    if (dr["UnidadMedida"].ToString() == null)
                    {
                        //  ListFinal += receta.id + "," + receta.descripcion + "," + stock.ToString().Replace(",", ".") + "," + item.cantidad.ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + stock.ToString().Replace(",", ".") + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + "1" + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";

                    }
                    else
                    {
                        ListFinal += dr["id"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId(Convert.ToInt32((dr["UnidadMedida"].ToString()))).descripcion + "," + "Receta" + "," + dr["costo"].ToString().Replace(',', '.') + ";";
                        //ListFinal += item.Recetas.id + "," + item.Recetas.descripcion + "," + item.cantidad.ToString().Replace(",", ".") + "," + stock.ToString().Replace(",", ".") + "," + controladorUnidad.ObtenerUnidadId((int)item.Recetas.UnidadMedida).descripcion + "," + "Receta" + "," + receta.CostoU.ToString().Replace(',', '.') + ";";
                    }
                    //}
                    //}
                }
                return ListFinal;

                //return "hola";

            }
            catch (Exception ex)
            {
                return "";
            }
        }



        [WebMethod]
        public static string getRecepcionEntregaAgrupadoPorSector(string idsRecetas)
        {

            try
            {
                idsRecetas = idsRecetas.TrimEnd(',');
                ControladorReceta cReceta = new ControladorReceta();
                DataTable dt = cReceta.getIngredientesAndRecetasGruopBySector(idsRecetas);


                dt.DefaultView.Sort = "SectorProductivo ASC";
                DataTable sortedDt = dt.DefaultView.ToTable();

                string listaProductos = "";


                foreach (DataRow dr in sortedDt.Rows)
                {

                    listaProductos += dr["idProducto"].ToString() + "," + dr["descripcion"].ToString() + "," + dr["cantidad"].ToString().Replace(",", ".") + "," + dr["unidadMedida"].ToString() + "," + dr["costo"].ToString().Replace(',', '.') + "," + dr["SectorProductivo"].ToString() + "," + dr["id"].ToString() + ";";
                    // dr[].ToString()

                }

                return listaProductos;
            }
            catch (Exception)
            {

                return "";
            }
        }



        [WebMethod]
        public static string getStockCostoUnidad(string id)
        {

            try
            {

                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorReceta cReceta = new ControladorReceta();
                var stock = controladorStockProducto.ObtenerStockProducto(Convert.ToInt32(id));
                var producto = cReceta.getUnidadMedidaByIdProducto(Convert.ToInt32(id));
                ControladorUnidad controladorUnidad = new ControladorUnidad();

                string datos = "";
                string costo = producto.costo.ToString();
                string Stock = stock.stock.ToString();
                datos += costo.Replace(",", ".") + "," + Stock.Replace(",", ".") + "," + producto.Unidades.descripcion + ";";

                return datos;
            }
            catch (Exception)
            {

                return "";
            }
        }



        [WebMethod]
        public static string getDatosTransferenciaBySectorDestino(string sectorOrigen, string sectorDestino)
        {
            try
            {
                controladorDatosTransferencias cTransferencias = new controladorDatosTransferencias();
                controladorsumaDatosTransferencia csumaDatosTransferencia = new controladorsumaDatosTransferencia();
                DataTable dt = cTransferencias.getDatosTransferenciaBySectorDestino(sectorOrigen, sectorDestino);
                DataTable dt2 = csumaDatosTransferencia.getDatosTransferencias(sectorDestino);

                //HttpContext.Current.Session["datosAEnviar"] = dt2;


                string datosTransferencia = "[";
                string cantidad = "";
                string cantidadConfirmada = "";
                foreach (DataRow dr in dt2.Rows)
                {
                    cantidad = dr["cantidad"].ToString().Replace(",", ".");
                    cantidadConfirmada = dr["cantidadConfirmada"].ToString().Replace(",", ".");
                    datosTransferencia += "{" +
                        "\"sectorOrigen\":\"" + dr["sectorOrigen"].ToString() + "\"," +
                        "\"sectorDestino\":\"" + dr["sectorDestino"].ToString() + "\"," +
                        "\"producto\":\"" + dr["producto"].ToString() + "\"," +
                        "\"cantidad\":\"" + cantidad + "\"," +
                        "\"cantidadConfirmada\":\"" + cantidadConfirmada + "\"" +
                        "},";
                }

                // Elimina la última coma y cierra el array
                if (dt2.Rows.Count > 0)
                {
                    datosTransferencia = datosTransferencia.Remove(datosTransferencia.Length - 1);
                }
                datosTransferencia += "]";

                if (dt2.Rows.Count == 0)
                {
                    datosTransferencia = "[]";
                }

                return datosTransferencia;
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }


        //[WebMethod]
        //public static int guardarDatosTransferencia(string idsPedidos, string cantidadesPedidos)
        //{
        //    try
        //    {

        //      //  int id = Convert.ToInt32(idTransferencia);
        //        ControladorTransferencia cTransferencia = new ControladorTransferencia();
        //        controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
        //      //  int r = cTransferencia.CambiarEstadoTransferencia(id);
        //      //
        //        idsPedidos = idsPedidos.TrimEnd(',');
        //        cantidadesPedidos = cantidadesPedidos.TrimEnd(',');
        //        var arrayIdsPedidos = idsPedidos.Split(',');
        //        var arraycantidadesPedidos = cantidadesPedidos.Split(',');
        //      //
        //        for (int i = 0; i < arrayIdsPedidos.Length; i++)
        //        {
        //            int idPedido = Convert.ToInt32(arrayIdsPedidos[i]);
        //            string cantAConfirmar = arraycantidadesPedidos[i].ToString();
        //            cDatosTransferencias.updateDatosTransferenciaCantidadEnviada(idPedido, cantAConfirmar);
        //        }

        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }
        //}




        [WebMethod]
        public static string getRecetasProveniente(string idsRecetas)
        {

            try
            {
                int entro = 1;
                string recetas = "";
                DataTable dt = HttpContext.Current.Session["insumosYrecetas"] as DataTable;
                string[] idsRecetasSplitComa = idsRecetas.Split(',');
                DataTable insumosFiltrados = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {


                    for (int i = 0; i < idsRecetasSplitComa.Length; i++)
                    {

                        if (dr["ProducoOreceta"].ToString() == "Receta" && dr["idFila"].ToString() == idsRecetasSplitComa[i])
                        {
                            string cantidad = dr["cantidad"].ToString();
                            insumosFiltrados.Rows.Add(dr.ItemArray);
                        }
                    }

                }

                var resultados = from DataRow dr in insumosFiltrados.Rows
                                 group dr by new
                                 {
                                     Fecha = Convert.ToDateTime(dr["Fecha"]),
                                     descripcion = dr["descripcion"].ToString(),
                                     sectorProductivo = dr["sectorProductivo"].ToString()
                                 } into g
                                 select new
                                 {
                                     Fecha = g.Key.Fecha,
                                     descripcion = g.Key.descripcion,
                                     sectorProductivo = g.Key.sectorProductivo,
                                     CantidadTotal = g.Sum(row => Convert.ToDecimal(row["Cantidad"])),
                                 };


                foreach (var resultado in resultados)
                {
                    string cantidad = resultado.CantidadTotal.ToString();
                    recetas += resultado.Fecha.ToString() + "," + resultado.descripcion.ToString() + "," + resultado.sectorProductivo.ToString() + "," + cantidad.Replace(",", ".") + ";";

                }


                return recetas;
            }
            catch (Exception)
            {

                return "";
            }
        }


        [WebMethod]
        public static string getStockSector(string idIngrediente, string idSectorProductivo)
        {

            try
            {

                ControladorStockProducto ControladorStockProducto = new ControladorStockProducto();
                var listStockSector = ControladorStockProducto.ObtenerStockSectoresByIdProducto(Convert.ToInt32(idIngrediente));

                //aca tengo que quitar el elemento de la lista
                //foreach (var list in listStockSector)
                //{
                //    if (list.idSector == Convert.ToInt32(idSectorProductivo))
                //    {
                //        listStockSector.RemoveAll(item => item.idSector == Convert.ToInt32(idSectorProductivo));
                //    }
                //}


                for (int i = listStockSector.Count - 1; i >= 0; i--)
                {
                    if (listStockSector[i].idSector == Convert.ToInt32(idSectorProductivo))
                    {
                        listStockSector.RemoveAt(i);
                    }
                }

                string stockSectores = "";
                string UnidadMedida = "";
                ControladorUnidad cu = new ControladorUnidad();
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                foreach (var i in listStockSector)
                {
                    UnidadMedida = cu.ObtenerUnidadId(i.Productos.unidadMedida).descripcion;
                    stockSectores += i.SectorProductivo.descripcion + "," + UnidadMedida + "," + i.stock.Value.ToString("N", culture) + "," + i.id + ";";
                }

                return stockSectores;
            }
            catch (Exception)
            {
                return "";
            }
        }



        [WebMethod]
        public static string updateStockSector(string stockSectores, int idSectorProductivo, string ingredientesARecepcionar)
        {

            try
            {
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                char[] separadores = { ';' };
                string[] stockSector = stockSectores.Split(separadores, StringSplitOptions.RemoveEmptyEntries);
                //string [] stockSector = stockSectores.Split(';');

                foreach (string items in stockSector)
                {

                    string[] item = items.Split(',');

                    var stock = controladorStockProducto.ObtenerStockSectorByID(Convert.ToInt32(item[0]));
                    stock.stock = stock.stock - (Convert.ToDecimal(item[4], CultureInfo.InvariantCulture));
                    controladorStockProducto.EditarStockSectores(stock);
                }


                //Aca tiene que actualizar el stock del sector filtrado
                string[] ingredientesRecepcion = ingredientesARecepcionar.Split(separadores, StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in ingredientesRecepcion)
                {

                    string[] item = items.Split(',');
                    StockSectores stockSectoresSectorFiltrado = controladorStockProducto.ObtenerStockSectores(Convert.ToInt32(item[0]), idSectorProductivo);
                    if (stockSectoresSectorFiltrado == null)
                    {
                        StockSectores stockSectores2 = new StockSectores();
                        stockSectores2.idProducto = Convert.ToInt32(item[0]);
                        stockSectores2.idSector = idSectorProductivo;
                        stockSectores2.stock = Convert.ToDecimal(item[1], CultureInfo.InvariantCulture);
                        controladorStockProducto.AgregarStockSectores(stockSectores2);
                    }
                    else
                    {
                        //var numeroDecimal = Convert.ToDecimal(item[1], CultureInfo.InvariantCulture);
                        stockSectoresSectorFiltrado.stock += Convert.ToDecimal(item[1], CultureInfo.InvariantCulture);
                        //stockSectoresSectorFiltrado.stock += (decimal?)(item[1]);
                        controladorStockProducto.EditarStockSectores(stockSectoresSectorFiltrado);
                    }

                }




                //StockSectores sectorFiltrado = new StockSectores();
                //sectorFiltrado.idSector = idSectorProductivo;   

                //controladorStockProducto.AgregarStockSectores(sectorFiltrado);



                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        protected void btnSector_Click(object sender, EventArgs e)
        {
            string sector = ddlSector.SelectedItem.Text;
        }


        [WebMethod]
        public static string verDetallesPedidos(int idTransferencia, string productoDescripcion)
        {
            try
            {
                controladorPedidosTranferencia cPedidosTranferencia = new controladorPedidosTranferencia();
                DataTable dt = cPedidosTranferencia.getDatosPedidoByIdTransferenciaDT(idTransferencia, productoDescripcion);
                string detalleTransferencias = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string cantidad = dr["cantidadOrigen"].ToString();
                    string cantidadEnviada = dr["cantidadEnviada"].ToString();
                    cantidad = cantidad.Replace(",", ".");
                    cantidadEnviada = cantidadEnviada.Replace(",", ".");
                    detalleTransferencias += dr["id"].ToString() + "," + dr["sectorOrigen"].ToString()
                        + "," + dr["ProductoOrigen"].ToString() + "," + cantidad
                        + "," + dr["ProductoDestinodestino"].ToString() + "," + dr["SectorDestinodestino"].ToString()
                        + "," + dr["orden"].ToString() + "," + dr["cliente"].ToString()
                        + "," + dr["idTransferencia"].ToString() + "," + dr["estado"].ToString()
                        + "," + dr["estadoTransferencia"].ToString() + "," + dr["CantidadAConfirmar"].ToString()
                        + "," + cantidadEnviada + ";";

                }

                return detalleTransferencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [WebMethod]
        public static int guardarDatosTransferencia(List<RowClass> tableData)
        {

            try
            {
                controladorsumaDatosTransferencia csumaDatosTransferencia = new controladorsumaDatosTransferencia();
                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                sumaDatosTransferencia sumaDatosTransferencia = new sumaDatosTransferencia();
                string sectorDestino = "";
                DataTable dtTransferencias = new DataTable();
                dtTransferencias.Columns.Add("sectorOrigen", typeof(string));
                dtTransferencias.Columns.Add("sectorDestino", typeof(string));
                dtTransferencias.Columns.Add("producto", typeof(string));
                dtTransferencias.Columns.Add("cantidad", typeof(decimal));
                dtTransferencias.Columns.Add("cantidadConfirmada", typeof(decimal));
                dtTransferencias.Columns.Add("cantidadEnviada", typeof(decimal));
                dtTransferencias.Columns.Add("estado", typeof(bool));

                foreach (var row in tableData)
                {
                    sectorDestino = row.sectorDestino.ToString();

                    //sumaDatosTransferencia.sectorOrigen = row.sectorOrigen.ToString();
                    //sumaDatosTransferencia.sectorDestino = row.sectorDestino.ToString();
                    //sumaDatosTransferencia.producto = row.producto.ToString();
                    //sumaDatosTransferencia.cantidad = Convert.ToDecimal(row.cantidad.ToString());
                    //sumaDatosTransferencia.cantidadConfirmada = Convert.ToDecimal(row.cantidadConfirmada.ToString());
                    //sumaDatosTransferencia.cantidadEnviada = Convert.ToDecimal(row.cantidadEnviada.ToString());
                    //sumaDatosTransferencia.estado = true;
                    //csumaDatosTransferencia.addsumaDatosTransferencia(sumaDatosTransferencia);

                    // Agregar fila a DataTable
                    DataRow dr = dtTransferencias.NewRow();
                    dr["sectorOrigen"] = row.sectorOrigen.ToString();
                    dr["sectorDestino"] = row.sectorDestino.ToString();
                    dr["producto"] = row.producto.ToString();
                    dr["cantidad"] = Convert.ToDecimal(row.cantidad.ToString());
                    dr["cantidadConfirmada"] = Convert.ToDecimal(row.cantidadConfirmada.ToString());
                    dr["cantidadEnviada"] = Convert.ToDecimal(row.cantidadEnviada.ToString());
                    dr["estado"] = true;
                    dtTransferencias.Rows.Add(dr);
                }




                //ACA TENGO QUE GUARDAR LOS REMUTOS INTERNOS 
                //1-Guardo el remito interno 
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                var primerElemento = tableData[0];
                string SectorDestino = primerElemento.sectorDestino;
                RemitosInternos remitosInternos = new RemitosInternos();
                remitosInternos.sectorDestino = SectorDestino;
                remitosInternos.numero = "0001-00000001";
                remitosInternos.fecha = DateTime.Now;
                remitosInternos.estado = true;


                int r = cRemitosInternos.addRemitosInternos(remitosInternos);

                //2-Si el remito interno se guardo, guardo sus items
                if (r > 0)
                {
                    ControladorItemsRemitosInternos cItemsRemitosInternos = new ControladorItemsRemitosInternos();
                    foreach (var row in tableData)
                    {
                        ItemsRemitosInternos itemRemitosInternos = new ItemsRemitosInternos();
                        itemRemitosInternos.idRemito = r;
                        itemRemitosInternos.Producto = row.producto;
                        itemRemitosInternos.cantidad = Convert.ToDecimal(row.cantidad);
                        itemRemitosInternos.cantidadConfirmada = Convert.ToDecimal(row.cantidadConfirmada);
                        itemRemitosInternos.cantidadEnviada = Convert.ToDecimal(row.cantidadEnviada);
                        itemRemitosInternos.fecha = DateTime.Now;
                        itemRemitosInternos.estado = true;
                        cItemsRemitosInternos.addItemsRemitosInternos(itemRemitosInternos);
                    }
                }

                //foreach (var row in tableData)
                //{

                //}


                // Guardar DataTable en la sesión
                HttpContext.Current.Session["datosAEnviar"] = dtTransferencias;

                //cambiar el estado de las transferencias involucradas en el envio
                DataTable dtId = new DataTable();
                dtId = cTransferencia.getidTransferenciasConfirmadas(sectorDestino);


                foreach (DataRow dr in dtId.Rows)
                {
                    cTransferencia.updateEstadoTransferenciasAEnviar(Convert.ToInt32(dr["id"].ToString()));
                }

                return r;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        [WebMethod]
        public static string getItemsRemitosInternos(int idRemito)
        {
            try
            {
                ControladorItemsRemitosInternos cItemsRemitosInternos = new ControladorItemsRemitosInternos();
                var itemsRemitoInterno = cItemsRemitosInternos.getItemsRemitosInternosByIdRemito(idRemito);

                if (itemsRemitoInterno == null || itemsRemitoInterno.Count == 0)
                {
                    return "[]";
                }

                List<string> itemsList = new List<string>();
                foreach (var remitoInterno in itemsRemitoInterno)
                {
                    itemsList.Add("{" +
                                  "\"Id\":\"" + remitoInterno.id + "\"," +
                                  "\"Producto\":\"" + remitoInterno.Producto + "\"," +
                                  "\"cantidadEnviada\":\"" + remitoInterno.cantidadEnviada + "\"," +
                                  "\"cantidadRecepcionada\":\"" + remitoInterno.cantidadEnviada + "\"" +
                                  "}");
                }

                return "[" + string.Join(",", itemsList) + "]";
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnRecepcion_Click(object sender, EventArgs e)
        {

        }
    }
}