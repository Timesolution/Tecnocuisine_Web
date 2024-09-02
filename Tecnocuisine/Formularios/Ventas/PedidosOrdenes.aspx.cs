using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using System.Web.Script.Serialization;
using System.Web.WebSockets;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.EnterpriseServices.Internal;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class PedidosOrdenes : System.Web.UI.Page
    {
        public DataTable dt;
        public int idRow = 0;

        public class RowData
        {
            public string Id { get; set; }
            public string SectorProductivo { get; set; }
            public string Producto { get; set; }
            public string Cantidad { get; set; }
            public string Confirmada { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();
            ControladorTransferencia cTransferencia = new ControladorTransferencia();
            if (!IsPostBack)
            {
                cargarOrigen();
                cargarDestino();
                cargarEstadosOrdenes();
                //cargarSectorUsuario();
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

        public void cargarEstadosOrdenes()
        {
            controladorEstadoTransferencia cEstadoTransferencia = new controladorEstadoTransferencia();
            var estadosTransferencias = cEstadoTransferencia.getAllEstadosTransferencia();

            estadosTransferencias = estadosTransferencias.OrderBy(z => z.descripcion).ToList();
            estadosTransferencias.Insert(0, new estadoTransferencias { id = -1, descripcion = "Seleccione..." });


            this.ddlEstado.DataSource = estadosTransferencias;
            this.ddlEstado.DataValueField = "id";
            this.ddlEstado.DataTextField = "Descripcion";
            this.ddlEstado.DataBind();

            this.ddlEstado.SelectedIndex = 0;
        }

        public void cargarOrigen()
        {
            try
            {
                ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
                DataTable dt = cSectorProductivo.GetAllSectoresProductivosDT();
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

                this.ddlOrigen.DataSource = sortedTable;
                this.ddlOrigen.DataValueField = "id";
                this.ddlOrigen.DataTextField = "descripcion";

                this.ddlOrigen.DataBind();

                this.ddlOrigen.SelectedValue = "-1";

            }
            catch (Exception ex)
            {
            }

        }

        public void cargarDestino()
        {
            try
            {
                ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
                DataTable dt = cSectorProductivo.GetAllSectoresProductivosDT();
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

                this.ddlDestino.DataSource = sortedTable;
                this.ddlDestino.DataValueField = "id";
                this.ddlDestino.DataTextField = "descripcion";

                this.ddlDestino.DataBind();

                this.ddlDestino.SelectedValue = "-1";

            }
            catch (Exception ex)
            {
            }

        }

        //public void cargarSectorUsuario()
        //{
        //    try
        //    {
        //        ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
        //        DataTable dt = cSectorProductivo.GetAllSectoresProductivosDT();
        //        DataRow drSeleccione = dt.NewRow();
        //        drSeleccione["descripcion"] = "Seleccione...";
        //        drSeleccione["id"] = -1;
        //        dt.Rows.InsertAt(drSeleccione, 0);

        //        // Ordenar los datos alfabéticamente por la descripción (ignorando la primera fila)
        //        var sortedData = dt.AsEnumerable()
        //                           .Skip(1) // Ignorar la primera fila ("Seleccione...")
        //                           .OrderBy(row => row.Field<string>("descripcion"));

        //        // Crear una nueva tabla con los elementos ordenados y el elemento inicial
        //        DataTable sortedTable = dt.Clone();
        //        sortedTable.Rows.Add(drSeleccione.ItemArray);
        //        foreach (DataRow row in sortedData)
        //        {
        //            sortedTable.ImportRow(row);
        //        }

        //        this.ddlSectorUsuario.DataSource = sortedTable;
        //        this.ddlSectorUsuario.DataValueField = "id";
        //        this.ddlSectorUsuario.DataTextField = "descripcion";

        //        this.ddlSectorUsuario.DataBind();

        //        this.ddlSectorUsuario.SelectedValue = "-1";

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}
   
        [WebMethod]
        public static object GetTransferencias()
        {
            controladorPedidosTranferencia cTranferencia = new controladorPedidosTranferencia();
            var transferencias = cTranferencia.GetTransferencias();
            return transferencias;
        }

        //public void cargarTransferencias(string clave)
        //{
        //    idRow++;
        //    TableRow tr = new TableRow();
        //    tr.ID = idRow.ToString();

        //    //Celdas
        //    TableCell celproductoOrigen = new TableCell();
        //    celproductoOrigen.Text = clave;
        //    celproductoOrigen.VerticalAlign = VerticalAlign.Middle;
        //    celproductoOrigen.HorizontalAlign = HorizontalAlign.Right;
        //    celproductoOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //    tr.Cells.Add(celproductoOrigen);


        //    TableCell celcantidadOrigen = new TableCell();
        //    celcantidadOrigen.Text = clave;
        //    celcantidadOrigen.VerticalAlign = VerticalAlign.Middle;
        //    celcantidadOrigen.HorizontalAlign = HorizontalAlign.Right;
        //    celcantidadOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //    tr.Cells.Add(celcantidadOrigen);


        //    phTransferencias.Controls.Add(tr);

        //}
        //public void cargarPedidosEnPH(KeyValuePair<string, DataTable> kvp)
        //{
        //    idRow++;
        //    int flag = 0;
        //    foreach (System.Data.DataRow dr in kvp.Value.DefaultView.Table.Rows)
        //    {
        //        //if (flag == 0)
        //        //{
        //        //    flag++;

        //        //    TableRow trTransferencia = new TableRow();
        //        //    trTransferencia.ID = idRow.ToString();

        //        //    //Celdas
        //        //    TableCell celTranferencia = new TableCell();
        //        //    celTranferencia.Text = dr["id"].ToString();
        //        //    celTranferencia.VerticalAlign = VerticalAlign.Middle;
        //        //    celTranferencia.HorizontalAlign = HorizontalAlign.Left;
        //        //    celTranferencia.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        //    trTransferencia.Cells.Add(celTranferencia);


        //        //    TableCell celTranferencia2 = new TableCell();
        //        //    celTranferencia2.Text = dr["ProductoDestinodestino"].ToString();
        //        //    celTranferencia2.VerticalAlign = VerticalAlign.Middle;
        //        //    celTranferencia2.HorizontalAlign = HorizontalAlign.Right;
        //        //    celTranferencia2.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        //    trTransferencia.Cells.Add(celTranferencia2);


        //        //    phTransferencias.Controls.Add(trTransferencia);

        //        //}
        //        //else
        //        //{

        //        TableRow tr = new TableRow();
        //        tr.ID = idRow.ToString();

        //        //Celdas
        //        TableCell celproductoOrigen = new TableCell();
        //        celproductoOrigen.Text = dr["fecha"].ToString();
        //        celproductoOrigen.VerticalAlign = VerticalAlign.Middle;
        //        celproductoOrigen.HorizontalAlign = HorizontalAlign.Left;
        //        celproductoOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celproductoOrigen);


        //        TableCell celidOrden = new TableCell();
        //        celidOrden.Text = dr["id"].ToString();
        //        celidOrden.VerticalAlign = VerticalAlign.Middle;
        //        celidOrden.HorizontalAlign = HorizontalAlign.Left;
        //        celidOrden.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celidOrden);


        //        TableCell celcantidadOrigen = new TableCell();
        //        celcantidadOrigen.Text = dr["cantidadOrigen"].ToString();
        //        celcantidadOrigen.VerticalAlign = VerticalAlign.Middle;
        //        celcantidadOrigen.HorizontalAlign = HorizontalAlign.Left;
        //        celcantidadOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celcantidadOrigen);


        //        phTransferencias.Controls.Add(tr);
        //        //}
        //        //}
        //    }
        //}
      
        //public void filtrarTransferencias(string FechaD, string FechaH, string sector, int estadoTransferencia)
        //{
        //    try
        //    {
        //        ControladorTransferencia cTransferencia = new ControladorTransferencia();
        //        DataTable dt = cTransferencia.filtrarTransferenciasDT(FechaD, FechaH, sector, estadoTransferencia);

        //        phTransferencias.Controls.Clear();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            idRow++;
        //            TableRow tr = new TableRow();
        //            tr.ID = idRow.ToString();

        //            //Celdas
        //            TableCell celFecha = new TableCell();
        //            celFecha.Text = dr["fecha"].ToString();
        //            celFecha.VerticalAlign = VerticalAlign.Middle;
        //            celFecha.HorizontalAlign = HorizontalAlign.Left;
        //            celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
        //            tr.Cells.Add(celFecha);


        //            TableCell celOrigen = new TableCell();
        //            celOrigen.Text = dr["origen"].ToString();
        //            celOrigen.VerticalAlign = VerticalAlign.Middle;
        //            celOrigen.HorizontalAlign = HorizontalAlign.Left;
        //            celOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //            tr.Cells.Add(celOrigen);


        //            TableCell celDestino = new TableCell();
        //            celDestino.Text = dr["destino"].ToString();
        //            celDestino.VerticalAlign = VerticalAlign.Middle;
        //            celDestino.HorizontalAlign = HorizontalAlign.Left;
        //            celDestino.Attributes.Add("style", "padding-bottom: 1px !important;");
        //            tr.Cells.Add(celDestino);



        //            TableCell celOrden = new TableCell();
        //            celOrden.Text = dr["orden"].ToString();
        //            celOrden.VerticalAlign = VerticalAlign.Middle;
        //            celOrden.HorizontalAlign = HorizontalAlign.Left;
        //            celOrden.Attributes.Add("style", "padding-bottom: 1px !important;");
        //            tr.Cells.Add(celOrden);


        //            TableCell celEstadoTransferencia = new TableCell();
        //            celEstadoTransferencia.Text = dr["descripcion"].ToString();
        //            celEstadoTransferencia.VerticalAlign = VerticalAlign.Middle;
        //            celEstadoTransferencia.HorizontalAlign = HorizontalAlign.Left;
        //            celEstadoTransferencia.Attributes.Add("style", "padding-bottom: 1px !important;");
        //            tr.Cells.Add(celEstadoTransferencia);


        //            TableCell celAccion = new TableCell();
        //            LinkButton btnEliminar = new LinkButton();
        //            btnEliminar.ID = "btnEliminarReceta_" + dr["id"].ToString();
        //            btnEliminar.CssClass = "btn btn-xs";
        //            btnEliminar.Style.Add("background-color", "transparent");
        //            btnEliminar.Attributes.Add("data-toggle", "modal");
        //            btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
        //            btnEliminar.Text = "<span title='Detalle'><i class='fa fa-exchange' style='color: black;'></i></span>";
        //            btnEliminar.Attributes.Add("onclick", "verDetalleTranferencia('" + dr["id"].ToString() + "', '" + celFecha.Text + "');");
        //            celAccion.Controls.Add(btnEliminar);


        //            tr.Cells.Add(celAccion);

        //            phTransferencias.Controls.Add(tr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        [WebMethod]
        public static string verDetallesTransferencia(int idTransferencia)
        {
            try
            {
                controladorPedidosTranferencia cPedidosTranferencia = new controladorPedidosTranferencia();
                DataTable dt = cPedidosTranferencia.getDetalleTransferenciaByIdTransferenciaDT(idTransferencia);
                string detalleTransferencias = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string cantidad = dr["cantidad"].ToString();
                    string cantidadAConfirmar = dr["cantidadConfirmada"].ToString();
                    cantidad = cantidad.Replace(",", ".");
                    cantidadAConfirmar = cantidadAConfirmar.Replace(",", ".");
                    detalleTransferencias += dr["id"].ToString() + ","
                        + dr["sectorOrigen"].ToString() + ","
                        + dr["producto"].ToString() + ","
                        + cantidad + ","
                        + cantidadAConfirmar + ";";
                }

                return detalleTransferencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string verDetallesPedidos(int idTransferencia, string ProductoOrigen)
        {
            try
            {
                controladorPedidosTranferencia cPedidosTranferencia = new controladorPedidosTranferencia();
                DataTable dt = cPedidosTranferencia.getDetallePedidoByIdTransferenciaDT(idTransferencia, ProductoOrigen);
                string detalleTransferencias = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string cantidad = dr["cantidadOrigen"].ToString();
                    cantidad = cantidad.Replace(",", ".");
                    detalleTransferencias += dr["id"].ToString() + "," + dr["sectorOrigen"].ToString()
                        + "," + dr["ProductoOrigen"].ToString() + "," + cantidad
                        + "," + dr["ProductoDestinodestino"].ToString() + "," + dr["SectorDestinodestino"].ToString()
                        + "," + dr["orden"].ToString() + "," + dr["cliente"].ToString()
                        + "," + dr["idTransferencia"].ToString() + "," + dr["estado"].ToString()
                        + "," + dr["estadoTransferencia"].ToString() + "," + dr["CantidadAConfirmar"].ToString() + ";";

                }

                return detalleTransferencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string cambiarEstadoTransferencia(int idTransferencia)
        {
            try
            {
                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                int r = cTransferencia.CambiarEstadoTransferencia(idTransferencia);
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static int GetEstadoTransferencia(int idTransferencia)
        {
            ControladorTransferencia cTransferencia = new ControladorTransferencia();
            var transferencia = cTransferencia.getTransferenciaById(idTransferencia);
            return (int)(transferencia?.estadoTransferencia);
        }

        [WebMethod]
        public static int guardarDatosTransferencia(string idsPedidos, string cantidadesPedidos, string idTransferencia)
        {
            try
            {
                int id = Convert.ToInt32(idTransferencia);

                // Si el estado es distinto a "A confirmar", no permitir confirmar la transferencia
                if (GetEstadoTransferencia(id) != 2) return -1;

                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                int r = cTransferencia.CambiarEstadoTransferencia(id);

                idsPedidos = idsPedidos.TrimEnd(',');
                cantidadesPedidos = cantidadesPedidos.TrimEnd(',');
                var arrayIdsPedidos = idsPedidos.Split(',');
                var arraycantidadesPedidos = cantidadesPedidos.Split(',');

                for (int i = 0; i < arrayIdsPedidos.Length; i++)
                {
                    int idPedido = Convert.ToInt32(arrayIdsPedidos[i]);
                    string cantAConfirmar = arraycantidadesPedidos[i].ToString();
                    cTransferencia.updatePedidosCantidadAConfirmar(idPedido, cantAConfirmar);
                }

                return 1;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [WebMethod]
        public static int confirmarTransferencia(string idTransferencia, List<RowData> tableData)
        {
            try
            {
                int id = Convert.ToInt32(idTransferencia);

                // Si el estado es distinto a "A confirmar", no permitir confirmar la transferencia
                if(GetEstadoTransferencia(id) != 2) return -1;

                ControladorTransferencia cTransferencia = new ControladorTransferencia();
                int r = cTransferencia.CambiarEstadoTransferencia(id);
                // Usar expresiones regulares para extraer filas de la tabla
                controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
                datosTransferencias DatosTransferencias = new datosTransferencias();

                foreach (var row in tableData)
                {
                    string rowId = row.Id;
                    string sectorProductivo = row.SectorProductivo;
                    string producto = row.Producto;
                    string cantidad = row.Cantidad;
                    string confirmada = row.Confirmada;
                    DatosTransferencias.id = Convert.ToInt32(rowId);
                    DatosTransferencias.cantidadConfirmada = Convert.ToDecimal(confirmada, CultureInfo.InvariantCulture);
                    cDatosTransferencias.updateDatosTransferenciaCantidadEnviada(DatosTransferencias);
                }

                saveSumaDatosTransferencia();
                return r;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static void saveSumaDatosTransferencia()
        {
            controladorDatosTransferencias cDatosTransferencias = new controladorDatosTransferencias();
            controladorsumaDatosTransferencia csumaDatosTransferencia = new controladorsumaDatosTransferencia();
            DataTable dt = cDatosTransferencias.getDatosTransferencias();
            foreach (DataRow dr in dt.Rows)
            {
                sumaDatosTransferencia sumaDatosTransferencia = new sumaDatosTransferencia();
                sumaDatosTransferencia.sectorOrigen = dr["sectorOrigen"].ToString();
                sumaDatosTransferencia.sectorDestino = dr["sectorDestino"].ToString();
                sumaDatosTransferencia.producto = dr["producto"].ToString();
                sumaDatosTransferencia.cantidad = Convert.ToDecimal(dr["cantidad"].ToString());
                sumaDatosTransferencia.cantidadConfirmada = Convert.ToDecimal(dr["cantidadConfirmada"].ToString());
                sumaDatosTransferencia.cantidadEnviada = Convert.ToDecimal(dr["cantidadConfirmada"].ToString());
                csumaDatosTransferencia.addsumaDatosTransferencia(sumaDatosTransferencia);
            }
        }

        public static string ObtenerValorInput(string inputHTML)
        {
            // Usar expresión regular para extraer el valor del input
            string valuePattern = @"value=[""']?(?<value>[^'""\s>]+)[""']?";
            Match match = Regex.Match(inputHTML, valuePattern);
            if (match.Success)
            {
                return match.Groups["value"].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idTransferencia.Value);
            ControladorTransferencia cTransferencia = new ControladorTransferencia();
            int r = cTransferencia.CambiarEstadoTransferencia(id);

            string urlActual = HttpContext.Current.Request.Url.AbsoluteUri;

            Response.Redirect(urlActual, false);

            // UpdatePanel2.Update();
            //
            // ScriptManager.RegisterClientScriptBlock(this.UpdatePanel2, UpdatePanel2.GetType(), "alert", "$.msgbox(\"Cambios guardados con exito!.\", {type: \"info\",buttons: [{ type: \"submit\", value: \"Aceptar\"},]});$('#MainContent_AgendaDatePickerDesde').datepicker({ dateFormat: 'dd/mm/yy' });$('#MainContent_AgendaDatePickerHasta').datepicker({ dateFormat: 'dd/mm/yy' });", true);

        }
     
        protected void GetTransferenciasConFiltros()
        {
            try
            {
                //Obtener valores para filtro
                string origen = ddlOrigen.SelectedValue != "-1" ? ddlOrigen.SelectedItem.Text : "";
                string destino = ddlDestino.SelectedValue != "-1" ? ddlDestino.SelectedItem.Text : "";
                int estado = int.Parse(ddlEstado.SelectedValue);

                //Obtener transferencias filtradas
                var transferencias = new ControladorTransferencia().filtrarTransferencias(txtFechaHoy.Text, txtFechaVencimiento.Text, origen, destino, estado);

                //Mostrar
                //LoadTableTransferencias(transferencias);
            }
            catch (FormatException)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: formato de fecha no valido');", true);
            }
            catch (Exception)
            {
            }
        }

        //private void LoadTableTransferencias(List<Transferencia> transferenciasFiltradas)
        //{
        //    phTransferencias.Controls.Clear();
        //    transferencias.Value = "";

        //    foreach (var item in transferenciasFiltradas)
        //    {
        //        idRow++;
        //        TableRow tr = new TableRow();
        //        tr.ID = idRow.ToString();

        //        //Celdas
        //        TableCell celFecha = new TableCell();
        //        DateTime soloFecha;
        //        if (DateTime.TryParse(item.fecha.ToString(), out soloFecha))
        //        {
        //            celFecha.Text = soloFecha.ToString("dd/MM/yyyy");
        //        }

        //        celFecha.VerticalAlign = VerticalAlign.Middle;
        //        celFecha.HorizontalAlign = HorizontalAlign.Left;
        //        celFecha.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celFecha);

        //        TableCell celOrigen = new TableCell();
        //        celOrigen.Text = item.origen.ToString();
        //        celOrigen.VerticalAlign = VerticalAlign.Middle;
        //        celOrigen.HorizontalAlign = HorizontalAlign.Left;
        //        celOrigen.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celOrigen);

        //        TableCell celDestino = new TableCell();
        //        celDestino.Text = item.destino.ToString();
        //        celDestino.VerticalAlign = VerticalAlign.Middle;
        //        celDestino.HorizontalAlign = HorizontalAlign.Left;
        //        celDestino.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celDestino);

        //        TableCell celOrden = new TableCell();
        //        celOrden.Text = item.orden.ToString();
        //        celOrden.VerticalAlign = VerticalAlign.Middle;
        //        celOrden.HorizontalAlign = HorizontalAlign.Left;
        //        celOrden.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celOrden);

        //        TableCell celEstadoTransferencia = new TableCell();
        //        celEstadoTransferencia.Text = item.estadoTransferencias.descripcion;
        //        celEstadoTransferencia.VerticalAlign = VerticalAlign.Middle;
        //        celEstadoTransferencia.HorizontalAlign = HorizontalAlign.Left;
        //        celEstadoTransferencia.Attributes.Add("style", "padding-bottom: 1px !important;");
        //        tr.Cells.Add(celEstadoTransferencia);

        //        TableCell celAccion = new TableCell();
        //        LinkButton btnEliminar = new LinkButton();
        //        btnEliminar.ID = "btnEliminarReceta_" + item.id.ToString();
        //        btnEliminar.CssClass = "btn btn-xs";
        //        btnEliminar.Style.Add("background-color", "transparent");
        //        btnEliminar.Attributes.Add("data-toggle", "modal");
        //        btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
        //        btnEliminar.Text = "<span title='Detalle'><i class='fa fa-exchange' style='color: black;'></i></span>";
        //        btnEliminar.Attributes.Add("onclick", "verDetalleTranferencia('" + item.id.ToString() + "', '" + celFecha.Text + "');");

        //        celAccion.Controls.Add(btnEliminar);

        //        tr.Cells.Add(celAccion);

        //        transferencias.Value += item.fecha.ToString() + "," +
        //        item.origen.ToString() + "," + item.destino.ToString() + "," +
        //        item.orden.ToString() + "," + item.estadoTransferencias.descripcion + ";";

        //        phTransferencias.Controls.Add(tr);
        //    }
        //}
    }
}