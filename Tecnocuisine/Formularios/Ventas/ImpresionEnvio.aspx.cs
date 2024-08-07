using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Globalization;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Ventas
{
    public partial class ImpresionEnvio : System.Web.UI.Page
    {
        ReportViewer ReportViewer1 = new ReportViewer();
        //string sector;
        //string fDesde;
        //string fHasta;

        string sectorOrigen;
        string sectorDestino;

        protected void Page_Load(object sender, EventArgs e)
        {
            //sector = Request.QueryString["sector"].ToString();
            //fDesde = Request.QueryString["fDesde"].ToString();
            //fHasta = Request.QueryString["fHasta"].ToString();

            sectorOrigen = Request.QueryString["sectorO"].ToString();
            sectorDestino = Request.QueryString["sectorD"].ToString();

            generarReporte();
        }

        private void generarReporte()
        {
            try
            {
                DataTable dt = new controladorsumaDatosTransferencia().getDatosTransferencias(sectorOrigen, sectorDestino);

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("DetalleEnvio.rdlc");
                this.ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dt);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);
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