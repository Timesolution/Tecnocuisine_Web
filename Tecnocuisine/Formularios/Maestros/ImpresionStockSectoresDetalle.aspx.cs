using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tecnocuisine_API.Controladores;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class ImpresionStockSectoresDetalle : System.Web.UI.Page
    {
        private ReportViewer ReportViewer1 = new ReportViewer();
        private ControladorStockProducto cStockProducto = new ControladorStockProducto();
        private ControladorStockReceta cStockReceta = new ControladorStockReceta();
        private ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
        private int sectorId;
        private string sectorDescripcion;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            sectorId = Convert.ToInt32(Request.QueryString["sector"].ToString());
            sectorDescripcion = cSectorProductivo.ObtenerSectorProductivoId(sectorId).descripcion;

            generarReporte();
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
                    if (this.VerificarAcceso() != 1)
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

        private int VerificarAcceso()
        {
            try
            {
                int valor = 0;
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

        public DataTable GetListasCombinadasEnDT(List<Tecnocuisine_API.Entitys.StockSectores> stocksProductos, List<Tecnocuisine_API.Entitys.stockSectoresReceta> stocksRecetas)
        {
            // Crear un nuevo DataTable
            DataTable dataTable = new DataTable();

            // Definir las columnas
            dataTable.Columns.Add("Descripcion", typeof(string));
            dataTable.Columns.Add("Stock", typeof(decimal));
            dataTable.Columns.Add("Unidad", typeof(string));

            // Agregar datos de stocksProductos
            foreach (var stocksProducto in stocksProductos)
            {
                DataRow row = dataTable.NewRow();
                row["Descripcion"] = stocksProducto.Productos.descripcion;
                row["Stock"] = stocksProducto.stock;
                row["Unidad"] = stocksProducto.Productos.Unidades.abreviacion;
                dataTable.Rows.Add(row);
            }

            // Agregar datos de stocksProductos
            foreach (var stocksReceta in stocksRecetas)
            {
                DataRow row = dataTable.NewRow();
                row["Descripcion"] = stocksReceta.Recetas.descripcion;
                row["Stock"] = stocksReceta.stock;
                row["Unidad"] = stocksReceta.Recetas.Unidades.abreviacion;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private void generarReporte()
        {
            try
            {
                var stocksProductos = cStockProducto.ObtenerStockSectoresByIdSector(sectorId);
                var stocksRecetas = cStockReceta.ObtenerStockSectoresByIdSector(sectorId);

                DataTable dt = GetListasCombinadasEnDT(stocksProductos, stocksRecetas);

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("StockSectorDetalle.rdlc");
                this.ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource("dsDatosPedidos", dt);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter paramFecha = new ReportParameter("Fecha", DateTime.Now.ToShortDateString());
                ReportParameter paramSector = new ReportParameter("Sector", sectorDescripcion);
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramFecha });
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramSector });

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