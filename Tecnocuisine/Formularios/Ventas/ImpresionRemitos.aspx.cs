using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class ImpresionRemitos : System.Web.UI.Page
    {
        ReportViewer ReportViewer1 = new ReportViewer();
        int idRemito = -1;       
        protected void Page_Load(object sender, EventArgs e)
        {
            idRemito = Convert.ToInt32(Request.QueryString["r"]);
            generarReporte8();
            if (!IsPostBack)
            {
                //generarReporte9();
            }
        }

        private void generarReporte8()
        {
            try
            {
                ControladorRemitosInternos cRemitosInternos = new ControladorRemitosInternos();
                DataTable dtRemito = cRemitosInternos.getRemitosInternosByIdDT(idRemito);    
                //controladorCliente cont = new controladorCliente();
                //controladorSucursal contSuc = new controladorSucursal();

                //var remito = this.contCompraEntity.obtenerRemito(this.idRemito);
                //Cliente p = cont.obtenerProveedorID(remito.IdProveedor.Value);
                //Sucursal s = contSuc.obtenerSucursalID(remito.IdSucursal.Value);

                //DataTable dtDatos = Session["datosAEnviar"] as DataTable;

                //datos empresa emisora
                //DataTable dtEmpresa = controlEmpresa.obtenerEmpresaById(s.empresa.id);

                //String razonSoc = String.Empty;
                //String direComer = String.Empty;
                //String condIVA = String.Empty;

                //foreach (DataRow row2 in dtEmpresa.Rows)//Datos empresa 
                //{
                //    razonSoc = row2["Razon Social"].ToString();
                //    condIVA = row2["Condicion IVA"].ToString();
                //    direComer = row2["Direccion"].ToString();
                //}

                //DataTable dtDatos = new DataTable();
                //dtDatos.Columns.Add("Fecha");
                //dtDatos.Columns.Add("Proveedor");
                //dtDatos.Columns.Add("Numero");
                //dtDatos.Columns.Add("Tipo");
                //dtDatos.Columns.Add("Sucursal");
                //dtDatos.Columns.Add("Devolucion");
                //dtDatos.Columns.Add("Observaciones");

                //DataRow row = dtDatos.NewRow();
                //row["Fecha"] = remito.Fecha.Value.ToString("dd/MM/yyyy");
                //row["Proveedor"] = p.razonSocial;
                //row["Sucursal"] = s.nombre;
                //row["Numero"] = remito.Numero;
                //row["Tipo"] = remito.Tipo;
                //row["Devolucion"] = remito.Devolucion;
                //if (remito.RemitosCompras_Comentarios != null)
                //{
                //    row["Observaciones"] = remito.RemitosCompras_Comentarios.Observacion;
                //}
                //dtDatos.Rows.Add(row);


                //DataTable dtItems = new DataTable();
                //dtItems.Columns.Add("Codigo");
                //dtItems.Columns.Add("Descripcion");
                //dtItems.Columns.Add("Cantidad", typeof(decimal));
                //dtItems.Columns.Add("FechaDespacho");
                //dtItems.Columns.Add("NumeroDespacho");
                //dtItems.Columns.Add("Lote");
                //dtItems.Columns.Add("Vencimiento");

                //foreach (var item in remito.RemitosCompras_Items)
                //{
                //    DataRow dr = dtItems.NewRow();

                //    if (remito.TipoItems == 1)
                //    {
                //        var articulo = this.contArticulo.obtenerArticuloByID(item.Codigo.Value);

                //        dr["Codigo"] = articulo.codigo;
                //        dr["Descripcion"] = articulo.descripcion;
                //        dr["Cantidad"] = item.Cantidad;
                //        dr["FechaDespacho"] = item.FechaDespacho;
                //        dr["NumeroDespacho"] = item.NumeroDespacho;
                //        dr["Lote"] = item.Lote;
                //        dr["Vencimiento"] = item.Vencimiento;
                //        dtItems.Rows.Add(dr);
                //    }
                //    else if (remito.TipoItems == 2)
                //    {
                //        var MateriasPrimas = this.contMateriaPrima.obtenerMateriaPrima(item.Codigo.Value);

                //        dr["Codigo"] = MateriasPrimas.Codigo;
                //        dr["Descripcion"] = MateriasPrimas.Descripcion;
                //        dr["Cantidad"] = item.Cantidad;
                //        dr["FechaDespacho"] = item.FechaDespacho;
                //        dr["NumeroDespacho"] = item.NumeroDespacho;
                //        dr["Lote"] = item.Lote;
                //        dr["Vencimiento"] = item.Vencimiento;
                //        dtItems.Rows.Add(dr);
                //    }

                //}

                //string logo = Server.MapPath("../../Facturas/" + s.empresa.id + "/Logo.jpg");
                //Log.EscribirSQL(1, "INFO", "Logo Remito Compra: " + logo);

                //String IdRemito = idRemito.ToString();
                ////codigo de barra
                //string imagen = this.generarCodigo(this.idRemito);
                //ReportParameter param10 = new ReportParameter("ParamCodBarra", @"file:///" + imagen);
                //ReportParameter param11 = new ReportParameter("ParamIdRemito", "");

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("DetalleRemitoR.rdlc");
                this.ReportViewer1.LocalReport.EnableExternalImages = true;


                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dtRemito);
                //ReportDataSource rds = new ReportDataSource("ItemsRemito", dtItems);

                //ReportParameter param1 = new ReportParameter("ParamImagen", @"file:///" + logo);
                //ReportParameter param12 = new ReportParameter("ParamRazonSoc", razonSoc);
                //ReportParameter param13 = new ReportParameter("ParamDomComer", direComer);
                //ReportParameter param14 = new ReportParameter("ParamCondIva", condIVA);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);
                //this.ReportViewer1.LocalReport.DataSources.Add(rds2);

                //this.ReportViewer1.LocalReport.SetParameters(param1);//logo
                //this.ReportViewer1.LocalReport.SetParameters(param12);
                //this.ReportViewer1.LocalReport.SetParameters(param13);
                //this.ReportViewer1.LocalReport.SetParameters(param14);
                //this.ReportViewer1.LocalReport.SetParameters(param10);
                //this.ReportViewer1.LocalReport.SetParameters(param11);

                this.ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string mimeType, encoding, fileNameExtension;

                string[] streams;

                //if (this.excel == 1)
                //{
                //    Byte[] xlsContent = this.ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //    String filename = string.Format("{0}.{1}", "RemitoCompra", "xls");

                //    this.Response.Clear();
                //    this.Response.Buffer = true;
                //    this.Response.ContentType = "application/ms-excel";
                //    this.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                //    //this.Response.AddHeader("content-length", pdfContent.Length.ToString());
                //    this.Response.BinaryWrite(xlsContent);

                //    this.Response.End();
                //}
                //else
                //{
                //get pdf content
                Byte[] pdfContent = this.ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.ContentType = "application/pdf";
                this.Response.AddHeader("content-length", pdfContent.Length.ToString());
                this.Response.BinaryWrite(pdfContent);

                this.Response.End();
                //}

            }
            catch (Exception ex)
            {

            }
        }




    }
}