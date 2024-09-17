using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class Pre_Produccion : System.Web.UI.Page
    {

        ControladorSectorProductivo sectorProductivo = new ControladorSectorProductivo();
        string fDesde;
        string fHasta;
        int idSectorProductivo = -1;
        DataTable dt;
        DataTable dt2;
        DataTable dtAuxiliar = new DataTable();
        int ultimoID = -1;
        string sector = "";
        string sectorValue = "";
        ReportViewer ReportViewer1 = new ReportViewer();
        public class RowClass
        {
            public string Id { get; set; }
            public string idSectorOrigen { get; set; }
            public string sectorOrigen { get; set; }
            public string sectorDestino { get; set; }
            public string idProducto { get; set; }
            public string producto { get; set; }
            public string cantidad { get; set; }
            public string cantidadConfirmada { get; set; }
            public string cantidadEnviada { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            CargarSectoresProductivodDDL();
            SetParametros();

            if (!IsPostBack)
            {
            }

            filtrarRemitosInternos();
            filtrarProduccion();
            filtrarDatosTransferenciasEnvios();
            PrecargarFiltros();
        }

        private void PrecargarFiltros()
        {
            if (!HayBusqueda()) return;

            txtFechaHoy.Text = fDesde.ToString();
            txtFechaVencimiento.Text = fHasta.ToString();
            ddlSector.SelectedValue = sectorValue;
        }

        private void SetParametros()
        {
            if (!HayBusqueda()) return;

            sectorValue = Request.QueryString["OV"].ToString();
            sector = Request.QueryString["O"].ToString();
            fDesde = Request.QueryString["fDesde"].ToString();
            fHasta = Request.QueryString["fHasta"].ToString();
        }

        public void filtrarProduccion()
        {
            if (!HayBusqueda())
                return;
            // Si no hay ningun filtro/parametro
            if (sectorValue == "-1" && string.IsNullOrEmpty(fDesde) && string.IsNullOrEmpty(fHasta))
                return;

            string sectorId = string.Empty;
            if (Request.QueryString["OV"] != null)
                sectorId = Request.QueryString["OV"].ToString();

            if (Request.QueryString["O"] != null)
            {
                this.sector = this.sectorValue != "-1" ? Request.QueryString["O"].ToString() : "";

                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                DataTable dt = cTransferencia.getTransferenciasByFiltros(sector, fDesde, fHasta);

                OrdenarRecetas(dt);

                int cont = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    cont++;
                    TableRow tr = new TableRow();
                    tr.ID = cont.ToString();

                    TableCell celFecha = new TableCell();
                    celFecha.Text = dr["fecha"].ToString();
                    celFecha.VerticalAlign = VerticalAlign.Middle;
                    celFecha.HorizontalAlign = HorizontalAlign.Left;
                    celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celFecha);

                    //Celdas
                    TableCell celProducto = new TableCell();
                    celProducto.Text = dr["productoOrigen"].ToString();
                    celProducto.VerticalAlign = VerticalAlign.Middle;
                    celProducto.HorizontalAlign = HorizontalAlign.Left;
                    celProducto.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celProducto);


                    TableCell celCantidad = new TableCell();
                    celCantidad.Text = dr["cantidadConfirmada"].ToString();
                    celCantidad.VerticalAlign = VerticalAlign.Middle;
                    celCantidad.HorizontalAlign = HorizontalAlign.Left;
                    celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                    tr.Cells.Add(celCantidad);

                   

                    TableCell celAccion = new TableCell();
                    LinkButton btnProducir = new LinkButton();
                    //btnDetalle.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                    btnProducir.CssClass = "btn btn-xs";
                    btnProducir.Style.Add("background-color", "transparent");
                    btnProducir.Text = "<span title='Producir'><i class='fa fa-utensils' style='color: black;'></i></span>";
                    btnProducir.Attributes.Add("href", "GenerarProduccion.aspx?i=" + dr["id"].ToString() + "&C=" + dr["cantidadConfirmada"].ToString() + "&s=" + sector + "&sid=" + sectorId);
                    btnProducir.Attributes.Add("target", "_blank");
                    celAccion.Controls.Add(btnProducir);
                    tr.Cells.Add(celAccion);


                    LinkButton btnDetalle = new LinkButton();
                    btnDetalle.ID = "btnVerPedidos_" + cont.ToString();
                    btnDetalle.CssClass = "btn btn-xs";
                    btnDetalle.Style.Add("background-color", "transparent");
                    btnDetalle.Attributes.Add("data-toggle", "modal");
                    btnDetalle.Attributes.Add("href", "#modalConfirmacion2");
                    btnDetalle.Text = "<span title='Ver Detalle'><i class='fa fa-file-text' style='color: black;'></i></span>";
                    btnDetalle.Attributes.Add("onclick", "verDetalleProduccion('" + sector + "', '" + dr["id"].ToString() + "', '" + dr["fecha"].ToString() + "');");

                    celAccion.Controls.Add(btnDetalle);

                    //LinkButton btnImprimir = new LinkButton();
                    ////btnDetalle.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                    //btnImprimir.CssClass = "btn btn-xs";
                    //btnImprimir.Style.Add("background-color", "transparent");
                    //btnImprimir.Text = "<span title='Imprimir'><i class='fa fa-print' style='color: black;'></i></span>";
                    //btnImprimir.Attributes.Add("href", $"ImpresionProduccion.aspx?id=" + dr["id"].ToString() + "&&sector=" + sector + "&&fecha=" + dr["fecha"].ToString());
                    //btnImprimir.Attributes.Add("target", "_blank");
                    ////btnDetalle.Attributes.Add("onclick", "impresionProduccion('" + sector + "', '" + dr["id"].ToString() + "', '" + dr["fecha"].ToString() + "');");

                    //celAccion.Controls.Add(btnImprimir);


                    phProduccion.Controls.Add(tr);
                }

            }

        }

        /// <summary>
        /// Genera un RDLC en otra pestaña con los registros de produccion filtrados
        /// </summary>
        protected void btnImprimirProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                string url = $"ImpresionProduccion.aspx?sector={Server.UrlEncode(sector)}&fDesde={Server.UrlEncode(fDesde)}&fHasta={Server.UrlEncode(fHasta)}";
                string script = $"window.open('{url}', '_blank');";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
                //Response.Redirect($"ImpresionProduccion.aspx?sector={sector}&&fDesde={fDesde}&&fHasta={fHasta}");
            }
            catch (Exception ex)
            {

            }
        }

        private void OrdenarRecetas(DataTable dt)
        {
            ControladorReceta cReceta = new ControladorReceta();
            int idR1, idR2;
            DataTable dtAux = dt.Copy();

            // Recorrer las filas de recetas 
            foreach (DataRow r1 in dt.Rows)
            {
                idR1 = int.Parse(r1["id"].ToString());

                // Comparar la receta con todas las recetas de la tabla
                DataRow copiaR2 = null;
                int indexR2 = 0;

                foreach (DataRow r2 in dt.Rows)
                {
                    idR2 = int.Parse(r2["id"].ToString());

                    if (cReceta.EsSubReceta(idR1, idR2))
                    {
                        //Si tiene hijos
                        //do
                        //{
                        //    foreach (DataRow r3 in dt.Rows)
                        //    {

                        //    }
                        //} while ();

                        //mover r2 al final porque es padre de una receta
                        copiaR2 = r2;
                        break; // Sale de este for
                    }
                    // Es receta final
                    else
                    {

                    }

                    indexR2++;
                }

                // Si no es subreceta se coloca al final. El orden es: primero subrecetas y al final recetas
                if (copiaR2 != null)
                {
                    // Crear una copia de la fila
                    DataRow nuevaFila = dt.NewRow();
                    nuevaFila.ItemArray = copiaR2.ItemArray.Clone() as object[];

                    // Eliminar la fila de su posición actual
                    //dtAux.Rows.RemoveAt(indexR2);
                    // Insertar la fila al final
                    // dtAux.Rows.Add(nuevaFila);
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
            if (!HayBusqueda())
                return;
            // Si no hay ningun filtro/parametro
            if (sectorValue == "-1" && string.IsNullOrEmpty(fDesde) && string.IsNullOrEmpty(fHasta))
                return;

            ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
            //var remitosInternos = cRemitosInternos.getRemitosInternosBySectorDestino(sector);
            var remitosInternos = cRemitosInternos.getRemitosInternosByFiltros(sector, fDesde, fHasta);

            if (remitosInternos == null) return;
            //Session["remitosInternosRecepcion"] = remitosInternos;

            foreach (var remito in remitosInternos)
            {
                int cont = 0;

                cont++;
                TableRow tr = new TableRow();
                tr.ID = cont.ToString();
                if (remito.recepcionado != null && remito.recepcionado == true)
                    tr.Style.Add("color", "green");

                //Celdas
                //TableCell celSectorDestino = new TableCell();
                //celSectorDestino.Text = remito.sectorDestino;
                //celSectorDestino.VerticalAlign = VerticalAlign.Middle;
                //celSectorDestino.HorizontalAlign = HorizontalAlign.Left;
                //celSectorDestino.Attributes.Add("style", "padding-bottom: 1px !important;");
                //tr.Cells.Add(celSectorDestino);

                TableCell celFecha = new TableCell();
                celFecha.Text = remito.fecha.ToShortDateString();
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);

                //string sectorOrigen = string.Empty;
                //if(remito.idSectorOrigen!=null)
                //    sectorOrigen = new ControladorSectorProductivo().ObtenerSectorProductivoId((int)remito.idSectorOrigen).descripcion;

                TableCell celSectorOrigen = new TableCell();
                celSectorOrigen.Text = remito.SectorProductivo?.descripcion??"";
                celSectorOrigen.VerticalAlign = VerticalAlign.Middle;
                celSectorOrigen.HorizontalAlign = HorizontalAlign.Left;
                celSectorOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celSectorOrigen);

                TableCell celNumero = new TableCell();
                celNumero.Text = remito.numero;
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Left;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNumero);

                

                TableCell celAccion = new TableCell();
                //LinkButton btnDetalle = new LinkButton();
                //btnDetalle.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                //btnDetalle.CssClass = "btn btn-xs";
                //btnDetalle.Style.Add("background-color", "transparent");
                //btnDetalle.Attributes.Add("data-toggle", "modal");
                //btnDetalle.Attributes.Add("href", "#modalConfirmacion2");
                //btnDetalle.Text = "<span title='Ver pedidos'><i class='fa fa-search' style='color: black;'></i></span>";
                //btnDetalle.Attributes.Add("onclick", "verDetalleRemitoInternoPdf('" + remito.id + "');");
                //celAccion.Controls.Add(btnDetalle);


                LinkButton btnDetalleRemitoInterno = new LinkButton();
                btnDetalleRemitoInterno.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                btnDetalleRemitoInterno.CssClass = "btn btn-xs";
                btnDetalleRemitoInterno.Style.Add("background-color", "transparent");
                btnDetalleRemitoInterno.Attributes.Add("data-toggle", "modal");
                btnDetalleRemitoInterno.Attributes.Add("href", "#modalConfirmacion2");
                btnDetalleRemitoInterno.Text = "<span title='Ver pedidos'><i class='fa fa-tasks' style='color: black;'></i></span>";
                bool recepcionado = remito.recepcionado ?? false;
                btnDetalleRemitoInterno.Attributes.Add("onclick", "verDetalleRemitoInternoModal('" + remito.id + "', " + recepcionado.ToString().ToLower() + ");");

                celAccion.Controls.Add(btnDetalleRemitoInterno);
                tr.Cells.Add(celAccion);

                phRecepcion.Controls.Add(tr);
            }
        }

        /// <summary>
        /// Verifica que esten en la URL los parametros enviados al hacer una busqueda
        /// </summary>
        /// <returns></returns>
        private bool HayBusqueda()
        {
            return Request.QueryString["O"] != null &&
                    Request.QueryString["fDesde"] != null &&
                    Request.QueryString["fHasta"] != null;
        }

        public void filtrarDatosTransferenciasEnvios()
        {
            if (!HayBusqueda())
                return;
            // Si no hay ningun filtro/parametro
            if (sectorValue == "-1" && string.IsNullOrEmpty(fDesde) && string.IsNullOrEmpty(fHasta))
                return;

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
                DataTable dt = cTransferencias.getDatosTransferenciaGroupByFiltros(sector, fDesde, fHasta);

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
                TableCell celFecha = new TableCell();
                DateTime fecha = Convert.ToDateTime(dr["fecha"]);
                celFecha.Text = fecha.ToString("dd/MM/yyyy");
                celFecha.VerticalAlign = VerticalAlign.Middle;
                celFecha.HorizontalAlign = HorizontalAlign.Left;
                celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFecha);

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
                btnDetalle.Text = "<span title='Ver pedidos'><i class='fa fa-eye' style='color: black;'></i></span>";
                btnDetalle.Attributes.Add("onclick", "getDatosTransferenciaDetalle('" + dr["sectorOrigen"].ToString() + "', '" + dr["sectorDestino"].ToString() + "', '" + fecha.ToString("yyyy-MM-dd") + "');");
                celAccion.Controls.Add(btnDetalle);

                //LinkButton btnRemito = new LinkButton();
                //btnRemito.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                //btnRemito.CssClass = "btn btn-xs";
                //btnRemito.Style.Add("background-color", "transparent");
                //btnRemito.Text = "<span title='Imprimir'><i class='fa fa-search' style='color: black;'></i></span>";
                //// btnRemito.Attributes.Add("onclick", "verDetalleRemitoInternoPdf('" + remito.id + "');");
                //celAccion.Controls.Add(btnRemito);

                LinkButton btnImprimir = new LinkButton();
                //btnDetalle.ID = "btnVerDetalleRemitoInterno_" + cont.ToString();
                btnImprimir.CssClass = "btn btn-xs";
                btnImprimir.Style.Add("background-color", "transparent");
                btnImprimir.Text = "<span title='Imprimir'><i class='fa fa-print' style='color: black;'></i></span>";
                btnImprimir.Attributes.Add("href", $"ImpresionEnvio.aspx?sectorO={dr["sectorOrigen"]}&&sectorD={dr["sectorDestino"]}&&fecha={fecha.ToString("yyyy-MM-dd")}");
                btnImprimir.Attributes.Add("target", "_blank");
                //btnDetalle.Attributes.Add("onclick", "impresionProduccion('" + sector + "', '" + dr["id"].ToString() + "', '" + dr["fecha"].ToString() + "');");

                celAccion.Controls.Add(btnImprimir);


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
        public static string getDatosTransferenciaDetalle(string sectorOrigen, string sectorDestino, DateTime fecha)
        {
            try
            {
                //controladorDatosTransferencias cTransferencias = new controladorDatosTransferencias();
                //DataTable dt = cTransferencias.getDatosTransferenciaBySectorDestino(sectorOrigen, sectorDestino);

                controladorsumaDatosTransferencia csumaDatosTransferencia = new controladorsumaDatosTransferencia();
                DataTable dt2 = csumaDatosTransferencia.getDatosTransferenciasDetalle(sectorOrigen, sectorDestino, fecha);

                //HttpContext.Current.Session["datosAEnviar"] = dt2;

                string datosTransferencia = "[";
                string cantidad = "";
                string cantidadConfirmada = "";
                foreach (DataRow dr in dt2.Rows)
                {
                    //Verificar si tiene stock
                    bool tieneStock = TieneStock(dr);

                    cantidadConfirmada = dr["cantidadConfirmada"].ToString().Replace(",", ".");
                    cantidad = dr["cantidad"].ToString().Replace(",", ".");
                    datosTransferencia += "{" +
                        "\"idSectorOrigen\":\"" + dr["idSectorOrigen"].ToString() + "\"," +
                        "\"sectorOrigen\":\"" + dr["sectorOrigen"].ToString() + "\"," +
                        "\"sectorDestino\":\"" + dr["sectorDestino"].ToString() + "\"," +
                        "\"idProducto\":\"" + dr["idProducto"].ToString() + "\"," +
                        "\"producto\":\"" + dr["producto"].ToString() + "\"," +
                        "\"cantidad\":\"" + cantidad + "\"," +
                        "\"cantidadConfirmada\":\"" + cantidadConfirmada + "\"," +

                        "\"tieneStock\":\"" + tieneStock + "\"" +
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

        private static bool TieneStock(DataRow dr)
        {
            string descripcionItem = dr["producto"].ToString();
            int idItem = Convert.ToInt32(dr["idProducto"].ToString());
            int idSector = Convert.ToInt32(dr["idSectorOrigen"].ToString());
            decimal cantidadAEnviar = Convert.ToDecimal(dr["cantidadConfirmada"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);

            ControladorProducto controladorProducto = new ControladorProducto();

            // Si es producto se verifica su stock en su tabla correspondiente
            if (controladorProducto.ExisteProducto(idItem, descripcionItem))
            {
                Tecnocuisine_API.Entitys.StockSectores stockSector = new ControladorStockProducto().ObtenerStockSectores(idItem, idSector);
                if (stockSector == null || cantidadAEnviar > stockSector.stock)
                    return false;

                return true;
            }
            // Si es receta se verifica su stock en su tabla correspondiente
            else
            {
                stockSectoresReceta stockSector = new ControladorStockReceta().ObtenerStockSectoresReceta(idItem, idSector);
                if (stockSector == null || cantidadAEnviar > stockSector.stock)
                    return false;

                return true;
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
                    Tecnocuisine_API.Entitys.StockSectores stockSectoresSectorFiltrado = controladorStockProducto.ObtenerStockSectores(Convert.ToInt32(item[0]), idSectorProductivo);
                    if (stockSectoresSectorFiltrado == null)
                    {
                        Tecnocuisine_API.Entitys.StockSectores stockSectores2 = new Tecnocuisine_API.Entitys.StockSectores();
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
        public static string verDetalleProduccion(string sector, int idProducto, string fecha)
        {
            try
            {
                DataTable dt = new controladorDatosTransferencias().getDatosTransferenciaByProductoSectorFecha(sector, idProducto, DateTime.Parse(fecha));
                string detalleTransferencias = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    string producto = dr["producto"].ToString();
                    string sectorDestino = dr["sectorDestino"].ToString();
                    string cantidadConfirmada = dr["cantidadConfirmada"].ToString();
                    cantidadConfirmada = cantidadConfirmada.Replace(",", ".");

                    detalleTransferencias +=
                        sectorDestino + "," +
                        producto + "," +
                        cantidadConfirmada + ";";
                }

                return detalleTransferencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guardar los datos del envio realizado
        /// </summary>
        /// <param name="tableData"></param>
        /// <returns></returns>
        [WebMethod]
        public static int guardarDatosTransferencia(List<RowClass> tableData)
        {
            try
            {
                controladorsumaDatosTransferencia csumaDatosTransferencia = new controladorsumaDatosTransferencia();
                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                sumaDatosTransferencia sumaDatosTransferencia = new sumaDatosTransferencia();
                ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();

                // Validar que todos los items a enviar tengan stock suficiente
                foreach (var row in tableData)
                {
                    int result = ValidarStock(row);

                    if (result != 1)
                    {
                        Exception ex = new Exception();
                        ex.Data["ErrorCode"] = result;
                        throw ex;
                    }
                }

                // 1-Generar remito interno 
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                RemitosInternos remitosInternos = new RemitosInternos();
                remitosInternos.idSectorOrigen = Convert.ToInt32(tableData[0].idSectorOrigen);
                remitosInternos.sectorDestino = tableData[0].sectorDestino;
                remitosInternos.numero = NuevoNumeroRemitoInterno();
                remitosInternos.fecha = DateTime.Now;
                remitosInternos.estado = true;

                int r = cRemitosInternos.addRemitosInternos(remitosInternos);

                // 2-Si el remito interno se guardo, guardo sus items
                if (r > 0)
                {
                    ControladorItemsRemitosInternos cItemsRemitosInternos = new ControladorItemsRemitosInternos();
                    foreach (var row in tableData)
                    {
                        var cantidad = Convert.ToDecimal(row.cantidad, CultureInfo.InvariantCulture);
                        var cantidadConfirmada = Convert.ToDecimal(row.cantidadConfirmada, CultureInfo.InvariantCulture);
                        var cantidadEnviada = Convert.ToDecimal(row.cantidadEnviada, CultureInfo.InvariantCulture);

                        ItemsRemitosInternos itemRemitosInternos = new ItemsRemitosInternos();
                        itemRemitosInternos.idRemito = r;
                        itemRemitosInternos.Producto = row.producto;
                        itemRemitosInternos.cantidad = cantidad;
                        itemRemitosInternos.cantidadConfirmada = cantidadConfirmada;
                        itemRemitosInternos.cantidadEnviada = cantidadEnviada;
                        itemRemitosInternos.fecha = DateTime.Now;
                        itemRemitosInternos.estado = true;
                        cItemsRemitosInternos.addItemsRemitosInternos(itemRemitosInternos);

                        //DescontarStockSector(row);
                        //AumentarStockSector(row);
                    }
                }

                //cambiar el estado de las transferencias involucradas en el envio
                DataTable dtId = new DataTable();
                dtId = cTransferencia.getidTransferenciasConfirmadas(tableData[0].sectorDestino); //TODO VER ESTO

                foreach (DataRow dr in dtId.Rows)
                    cTransferencia.updateEstadoTransferenciasAEnviar(Convert.ToInt32(dr["id"].ToString()));

                //generarReporteEnviado2(r);

                return r;
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("ErrorCode"))
                {
                    int errorCode = (int)ex.Data["ErrorCode"];
                    switch (errorCode)
                    {
                        case 2:
                            return -2;
                        case 3:
                            return -3;
                        case 4:
                            return -4;
                        default:
                            return -1;
                    }
                }

                return -1;
            }
        }

        private static void DescontarStockSector(RowClass row)
        {
            int idItem = Convert.ToInt32(row.idProducto);
            int idSectorOrigen = Convert.ToInt32(row.idSectorOrigen);
            string descripcion = row.producto;
            var cantidadEnviada = Convert.ToDecimal(row.cantidadEnviada, CultureInfo.InvariantCulture);

            // Si es Producto
            if (new ControladorProducto().ExisteProducto(idItem, descripcion))
            {
                ControladorStockProducto cStockProducto = new ControladorStockProducto();
                Tecnocuisine_API.Entitys.StockSectores stockSector = cStockProducto.ObtenerStockSectores(idItem, idSectorOrigen);
                stockSector.stock -= cantidadEnviada;
                cStockProducto.EditarStockSectores(stockSector);
            }
            // Si es Receta
            else
            {
                ControladorStockReceta cStockReceta = new ControladorStockReceta();
                stockSectoresReceta stockSector = cStockReceta.ObtenerStockSectoresReceta(idItem, idSectorOrigen);
                stockSector.stock -= cantidadEnviada;
                cStockReceta.EditarStockSectoresReceta(stockSector);
            }
        }

        private static void AumentarStockSector(RowClass row)
        {
            ControladorProducto controladorProducto = new ControladorProducto();
            ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();

            int idItem = Convert.ToInt32(row.idProducto);
            string descripcion = row.producto;
            int idSectorDestino = cSectorProductivo.ObtenerSectorProductivoByNombre(row.sectorDestino).id;
            var cantidadEnviada = Convert.ToDecimal(row.cantidadEnviada, CultureInfo.InvariantCulture);

            // Si es Producto
            if (controladorProducto.ExisteProducto(idItem, descripcion))
            {
                AumentarStockSector_Producto(idItem, idSectorDestino, cantidadEnviada);
            }
            // Si es Receta
            else
            {
                AumentarStockSector_Receta(idItem, idSectorDestino, cantidadEnviada);
            }
        }

        private static void AumentarStockSector_Producto(int idProducto, int idSectorDestino, decimal cantidad)
        {
            ControladorStockProducto cStockProducto = new ControladorStockProducto();
            Tecnocuisine_API.Entitys.StockSectores stockSector = cStockProducto.ObtenerStockSectores(idProducto, idSectorDestino);

            // Si no existe el registro crearlo
            if (stockSector == null)
            {
                stockSector = new Tecnocuisine_API.Entitys.StockSectores
                {
                    idProducto = idProducto,
                    idSector = idSectorDestino,
                    stock = cantidad,
                };

                cStockProducto.AgregarStockSectores(stockSector);
            }
            else
            {
                stockSector.stock += cantidad;
                cStockProducto.EditarStockSectores(stockSector);
            }
        }

        private static void AumentarStockSector_Receta(int idItem, int idSectorDestino, decimal cantidad)
        {
            ControladorStockReceta cStockReceta = new ControladorStockReceta();
            stockSectoresReceta stockSector = cStockReceta.ObtenerStockSectoresReceta(idItem, idSectorDestino);
            stockSector.stock -= cantidad;
            cStockReceta.EditarStockSectoresReceta(stockSector);

            // Si no existe el registro crearlo
            if (stockSector == null)
            {
                stockSector = new stockSectoresReceta
                {
                    idReceta = idItem,
                    idSector = idSectorDestino,
                    stock = cantidad
                };

                cStockReceta.AgregarStockSectoresReceta(stockSector);
            }
            else
            {
                stockSector.stock += cantidad;
                cStockReceta.EditarStockSectoresReceta(stockSector);
            }
        }

        private static int ValidarStock(RowClass row)
        {
            ControladorProducto cProducto = new ControladorProducto();
            ControladorReceta cReceta = new ControladorReceta();
            var cantidadAEnviar = Convert.ToDecimal(row.cantidadEnviada, CultureInfo.InvariantCulture);
            decimal stock = 0;
            int idItem = Convert.ToInt32(row.idProducto);
            string descripcion = row.producto;

            if (cProducto.ExisteProducto(idItem, descripcion))
            {
                StockProducto stockProducto = new ControladorStockProducto().ObtenerStockProducto(idItem);
                stock = stockProducto?.stock ?? 0;
            }
            else // Es Receta
            {
                StockReceta stockReceta = new ControladorStockReceta().ObtenerStockReceta(idItem);
                stock = stockReceta?.stock ?? 0;
            }

            if (stock == 0)
                return 2;

            else if (cantidadAEnviar == 0)
                return 3;

            else if (cantidadAEnviar > stock)
                return 4;

            return 1;
        }

        private static string NuevoNumeroRemitoInterno()
        {
            var ultimoNumero = new ControladorRemitosInternos().GetUltimoNumero();

            string[] partes = ultimoNumero.Split('-');
            string prefijo = partes[0]; // "0001"
            string numero = partes[1];  // "00000001"

            // Convertir la parte numérica a entero e incrementarla
            int numeroInt = int.Parse(numero);
            numeroInt += 1;

            // Verificar si el número ha alcanzado su límite
            if (numeroInt > 99999999)
            {
                // Reiniciar la parte numérica a 0
                numeroInt = 0;

                // Incrementar el prefijo
                int prefijoInt = int.Parse(prefijo);
                prefijoInt += 1;
                prefijo = prefijoInt.ToString().PadLeft(prefijo.Length, '0');
            }

            // Convertir la parte incrementada de nuevo a cadena con ceros a la izquierda
            string nuevoNumero = numeroInt.ToString().PadLeft(numero.Length, '0');

            // Reunir las partes en el nuevo formato de número de remito
            return $"{prefijo}-{nuevoNumero}";
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
            //Actualizar estado de recepcion del remito
            ControladorRemitosInternos contRemitosInternos = new ControladorRemitosInternos();
            var idRemitoInterno = Convert.ToInt32(HFIdRemitoInterno.Value);

            RemitosInternos remitoInterno = contRemitosInternos.getRemitosInternosById(idRemitoInterno).FirstOrDefault();
            remitoInterno.recepcionado = true;
            contRemitosInternos.UpdateRemitosInternos(remitoInterno);

            //ACTUALIZAR STOCKS DEL SECTOR X PRODUCTO (DESCONTAR STOCK EN EL SECTOR ORIGEN Y AUMENTAR STOCK EN EL SECTOR DESTINO)
            ActualizarStockSectorXProductos((int)remitoInterno.idSectorOrigen, remitoInterno.sectorDestino);

            Response.Redirect(Request.RawUrl);
        }

        private void ActualizarStockSectorXProductos(int idSectorOrigen, string sector)
        {
            string[] itemsRemitosInternos = HFItems.Value.Split(';');

            var idSectorDestino = new ControladorSectorProductivo().ObtenerSectorProductivoByNombre(sector).id;

            ControladorStockProducto controladorStockProducto = new ControladorStockProducto();
            ControladorStockReceta controladorStockReceta = new ControladorStockReceta();

            //Actualizar stock para cada producto del remito interno
            foreach (var item in itemsRemitosInternos)
            {
                string[] itemSpliteado = item.Split('&');
                string descripcion = itemSpliteado[0];
                string cantidadRecepcionada = itemSpliteado[2];

                //buscar el producto en la tabla productos
                var idItem = new ControladorProducto().ObtenerProductoByDescripcion(descripcion).id;
                bool esProducto = true;

                ////si no existe, buscarlo en la tabla recetas
                if (idItem == 0)
                {
                    idItem = new ControladorReceta().ObtenerRecetaByDescripcion(descripcion).id;
                    esProducto = false;
                }

                if (esProducto)
                {
                    //Descontar Stock en el sector origen (sector que hizo el envio)
                    Tecnocuisine_API.Entitys.StockSectores stockSectorOrigen = controladorStockProducto.ObtenerStockSectores(idItem, idSectorOrigen);
                    stockSectorOrigen.stock -= decimal.Parse(cantidadRecepcionada);
                    controladorStockProducto.EditarStockSectores(stockSectorOrigen);

                    //Aumentar Stock en el sector destino (sector que recepciona)
                    AumentarStockSector_Producto(idItem, idSectorDestino, decimal.Parse(cantidadRecepcionada));           
                }
                else
                {
                    //Descontar Stock en el sector origen (sector que hizo el envio)
                    stockSectoresReceta stockSectorOrigen = controladorStockReceta.ObtenerStockSectoresReceta(idItem, idSectorOrigen);
                    stockSectorOrigen.stock -= decimal.Parse(cantidadRecepcionada);
                    controladorStockReceta.EditarStockSectoresReceta(stockSectorOrigen);

                    //Aumentar Stock en el sector destino (sector que recepciona)
                    AumentarStockSector_Receta(idItem, idSectorDestino, decimal.Parse(cantidadRecepcionada));
                }
            }
        }

        [WebMethod]
        public static int generarReporteEnviado(int idRemito)
        {
            try
            {
                ReportViewer ReportViewer1 = new ReportViewer();
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                DataTable dtRemito = cRemitosInternos.getRemitosInternosByIdDT(idRemito);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("DetalleRemitoR.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dtRemito);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string mimeType, encoding, fileNameExtension;

                string[] streams;


                //get pdf content
                Byte[] pdfContent = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-length", pdfContent.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(pdfContent);
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int generarReporteEnviado2(int idRemito)
        {
            try
            {
                ReportViewer ReportViewer1 = new ReportViewer();
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                DataTable dtRemito = cRemitosInternos.getRemitosInternosByIdDT(idRemito);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("DetalleRemitoR.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dtRemito);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string mimeType, encoding, fileNameExtension;

                string[] streams;


                //get pdf content
                Byte[] pdfContent = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-length", pdfContent.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(pdfContent);
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        protected void btnImprimirProduccion_Click1(object sender, EventArgs e)
        {
            int a = 0;
        }
    }
}