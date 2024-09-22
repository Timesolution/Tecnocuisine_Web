using Gestion_Api.Modelo;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;


namespace Tecnocuisine.Formularios.Ventas
{
    public partial class ImpresionProduccion : System.Web.UI.Page
    {
        ReportViewer ReportViewer1 = new ReportViewer();
        string sector;
        string fDesde;
        string fHasta;

        protected void Page_Load(object sender, EventArgs e)
        {
            sector = Request.QueryString["sector"].ToString();
            fDesde = Request.QueryString["fDesde"].ToString();
            fHasta = Request.QueryString["fHasta"].ToString();
            generarReporte();
        }

        private void generarReporte()
        {
            try
            {
                DataTable dt = new ControladorTransferencia().getTransferenciasByFiltros(sector, fDesde, fHasta);

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("DetalleProduccion2.rdlc");
                this.ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dt);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);

                // SETEAR FECHAS
                string fechaDesdeFormateada = string.Empty;
                string fechaHastaFormateada = string.Empty;

                if (!string.IsNullOrEmpty(fDesde))
                {
                    // Convertir el string a DateTime
                    DateTime fechaDesdeDT = DateTime.ParseExact(fDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    // Convertir de vuelta a string en el formato deseado
                    fechaDesdeFormateada = fechaDesdeDT.ToString("dd/MM/yyyy");
                }
                if (!string.IsNullOrEmpty(fHasta))
                {
                    // Convertir el string a DateTime
                    DateTime fechaHastaDT = DateTime.ParseExact(fHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    // Convertir de vuelta a string en el formato deseado
                    fechaHastaFormateada = fechaHastaDT.ToString("dd/MM/yyyy");
                }

                ReportParameter paramFecha = new ReportParameter("fechaParametro", fechaDesdeFormateada + " - " + fechaHastaFormateada);
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramFecha });

                this.ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string mimeType, encoding, fileNameExtension;

                string[] streams;

                Byte[] pdfContent = this.ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.ContentType = "application/pdf";
                this.Response.AddHeader("content-length", pdfContent.Length.ToString());
                this.Response.BinaryWrite(pdfContent);
                this.Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
}