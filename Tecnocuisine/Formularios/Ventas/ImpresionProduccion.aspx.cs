using Gestion_Api.Modelo;
using Microsoft.Reporting.WebForms;
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
    public partial class ImpresionProduccion : System.Web.UI.Page
    {
        ReportViewer ReportViewer1 = new ReportViewer();
        int idProducto;
        string sector;
        string fecha;
        protected void Page_Load(object sender, EventArgs e)
        {
            idProducto = Convert.ToInt32(Request.QueryString["id"]);
            sector = Request.QueryString["sector"].ToString();
            fecha = Request.QueryString["fecha"].ToString();

            generarReporte();
        }

        private void generarReporte()
        {
            try
            {
                DataTable dt = new controladorDatosTransferencias().getDatosTransferenciaByProductoSectorFecha(sector, idProducto, DateTime.Parse(fecha));

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("DetalleProduccion2.rdlc");
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