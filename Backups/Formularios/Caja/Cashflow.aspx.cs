using Gestion_Api.Controladores;
using Gestor_Solution.Controladores;
using Gestor_Solution.Modelo;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Tecnocuisine.Caja;
using Tecnocuisine.Formularios.Ventas;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine.Formularios.Caja
{
    public partial class Cashflow : Page
    {
        Mensaje m = new Mensaje();
        ControladorInsumo controladorInsumo = new ControladorInsumo();
        Tecnocuisine_API.Controladores.ControladorCashFlow controladorCashFlow = new Tecnocuisine_API.Controladores.ControladorCashFlow();
        ControladorEntidad controladorEntidad = new ControladorEntidad();
        ControladorTarjetas controladorTarjetas = new ControladorTarjetas();
        ControladorConceptos controladorConceptos = new ControladorConceptos();
        int IDEgresoIngreso = 0;


        int accion;
        int Mensaje;
        string FechaD = "";
        string FechaH = "";
        int cont = 0;
        int ContDiario = 0;
        private int Accion = 0; //Uso esta variabla para saber si se esta editando en el CashFlow

        protected void Page_Load(object sender, EventArgs e)
        {

            DateTime date = DateTime.Now;

            VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["i"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (Request.QueryString["FechaD"] != null)
            {
                this.FechaD = (Request.QueryString["FechaD"]).ToString();

                this.FechaH = (Request.QueryString["FechaH"]).ToString();
            }
            if (!IsPostBack)
            {

                this.CargarConceptos();
                this.CargarCabeceraPH2();

                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 2)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }
                else if (Mensaje == 3)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

            }
            if (FechaD != "")
            {
                FiltrarVentas(this.FechaD, this.FechaH);
                FiltrarVentasDiarias(this.FechaD, this.FechaH);
            }
            else
            {

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

        public void FiltrarVentasDiarias(string FechaD, string FechaH)
        {

            this.FechaDesde.Value = this.FechaD;
            this.FechaHasta.Value = this.FechaH;

            string FechaDesde = ConvertDateFormat(FechaD);
            string FechaHasta = ConvertDateFormat(FechaH);

            var dtTarjetaDeCredito = controladorCashFlow.FiltrarCobrosTarjetaCredito(FechaD, FechaH);
            var dtCheques = controladorCashFlow.FiltrarCobrosCheques(FechaD, FechaH);


            int Cont = CargarInsumosDiariosCabeceraPH2(this.FechaD, this.FechaH); //ok

            if (Cont != -1)
            {
                //Esta parte solo genera las filas y columnas de la tabla vacia
                CargarArrastreTabla(Cont);
                CargarEfectivoContadoTabla(Cont);
                CargarInsumosDiariosPH2TablaCheque(Cont);
                CargarInsumosDiariosPH2Tabla(Cont);
                CargarTotalesTabla(Cont);
                CargarEgresosSueldos(Cont);
                CargarEgresosSUSS(Cont);
                CargarEgresosPagoAProveedores(Cont);
                cargarTotalEgresos(Cont);
                CargarSaldoTabla(Cont);

                //Esta parte carga los datos en la tabla ya generada 
                PonerCeros(Cont);
                SeparadorDeMilesNumeros(Cont);
                CargarDatosEnTabla(Cont, dtTarjetaDeCredito, dtCheques, FechaD, FechaH);               
                PonerSignoPesos(Cont);
            }

        }

        public void CargarEfectivoContadoTabla(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_EfectivoContadoTabla";

                TableCell celType = new TableCell();
                celType.Text = "Ingreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);

                TableCell celTypeEfectivoContado = new TableCell();
                celTypeEfectivoContado.Text = "Efectivo/Contado";
                celTypeEfectivoContado.VerticalAlign = VerticalAlign.Middle;
                celTypeEfectivoContado.HorizontalAlign = HorizontalAlign.Left;
                celTypeEfectivoContado.Width = Unit.Pixel(150);
                celTypeEfectivoContado.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeEfectivoContado.Font.Bold = true;
                tr.Cells.Add(celTypeEfectivoContado);

                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);
            }
            catch (Exception ex)
            {
            }
        }
        public void CargarCabeceraPH2()
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "Cabecera_flow";

                TableHeaderCell celType = new TableHeaderCell();
                celType.Text = "Tipo";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                // celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "width: 150px; padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableHeaderCell celAcreditaEl = new TableHeaderCell();
                celAcreditaEl.Text = "Detalle";
                celAcreditaEl.VerticalAlign = VerticalAlign.Middle;
                celAcreditaEl.HorizontalAlign = HorizontalAlign.Left;
                //  celAcreditaEl.Width = Unit.Pixel(150);
                celAcreditaEl.Attributes.Add("style", "width: 150px; padding-bottom: 1px !important; padding-right: 10px;");
                celAcreditaEl.Font.Bold = true;
                tr.Cells.Add(celAcreditaEl);

                phProductosyRecetasDiariosCabecera.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }
        }
        public void cargarTotalEgresos(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_Totales_Egresos";

                TableCell celType = new TableCell();
                celType.Text = "TOTAL EGRESOS";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                celType.ForeColor = System.Drawing.Color.Red;
                tr.Cells.Add(celType);

                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);
            }
            catch (Exception ex)
            {


            }
        }

        public void CargarEgresosPagoAProveedores(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_EgresoDiarioPagoAProveedores"; //ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Egreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableCell celTypeCheques = new TableCell();
                celTypeCheques.Text = "Pago a Proveedores";
                celTypeCheques.VerticalAlign = VerticalAlign.Middle;
                celTypeCheques.HorizontalAlign = HorizontalAlign.Left;
                celTypeCheques.Width = Unit.Pixel(150);
                celTypeCheques.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeCheques.Font.Bold = true;
                tr.Cells.Add(celTypeCheques);


                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);

            }
            catch (Exception ex)
            {
            }
        }


        public void CargarEgresosSUSS(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_EgresoDiarioSUSS"; //ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Egreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableCell celTypeCheques = new TableCell();
                celTypeCheques.Text = "SUSS";
                celTypeCheques.VerticalAlign = VerticalAlign.Middle;
                celTypeCheques.HorizontalAlign = HorizontalAlign.Left;
                celTypeCheques.Width = Unit.Pixel(150);
                celTypeCheques.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeCheques.Font.Bold = true;
                tr.Cells.Add(celTypeCheques);


                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);

            }
            catch (Exception ex)
            {


            }
        }

        public void CargarEgresosSueldos(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_EgresoDiarioSueldos"; //ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Egreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableCell celTypeCheques = new TableCell();
                celTypeCheques.Text = "Sueldos";
                celTypeCheques.VerticalAlign = VerticalAlign.Middle;
                celTypeCheques.HorizontalAlign = HorizontalAlign.Left;
                celTypeCheques.Width = Unit.Pixel(150);
                celTypeCheques.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeCheques.Font.Bold = true;
                tr.Cells.Add(celTypeCheques);


                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);

            }
            catch (Exception ex)
            {


            }
        }

        public void CargarInsumosDiariosPH2TablaCheque(int Cont)
        {
            ContDiario++;
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_IngresoDiarioCheque"; //ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Ingreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableCell celTypeCheques = new TableCell();
                celTypeCheques.Text = "Cheques";
                celTypeCheques.VerticalAlign = VerticalAlign.Middle;
                celTypeCheques.HorizontalAlign = HorizontalAlign.Left;
                celTypeCheques.Width = Unit.Pixel(150);
                celTypeCheques.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeCheques.Font.Bold = true;
                tr.Cells.Add(celTypeCheques);


                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);

            }
            catch (Exception ex)
            {


            }
        }

        public void SeparadorDeMilesNumeros(int Cont)
        {
            TableRow filaArrastre = (TableRow)phProductosyRecetasDiarios.Controls[0];
            TableRow filaEfectivoContado = (TableRow)phProductosyRecetasDiarios.Controls[1];
            TableRow filaIngresoCheque = (TableRow)phProductosyRecetasDiarios.Controls[2];
            TableRow filaIngreso = (TableRow)phProductosyRecetasDiarios.Controls[3];
            TableRow filaTotal = (TableRow)phProductosyRecetasDiarios.Controls[4];
            TableRow filaEgresoSueldos = (TableRow)phProductosyRecetasDiarios.Controls[5];
            TableRow filaEgresoSUSS = (TableRow)phProductosyRecetasDiarios.Controls[6];
            TableRow filaEgresoPagoAProveedores = (TableRow)phProductosyRecetasDiarios.Controls[7];
            TableRow filaTotalEgresos = (TableRow)phProductosyRecetasDiarios.Controls[8];
            TableRow filaSaldo = (TableRow)phProductosyRecetasDiarios.Controls[9];

            for (int i = 2; i <= Cont; i++)
            {
                TableCell celdaArrastre = filaArrastre.Cells[i];
                TableCell celdaEfectivoContado = filaEfectivoContado.Cells[i];
                TableCell celdaIngresoCheque = filaIngresoCheque.Cells[i];
                TableCell celdaIngreso = filaIngreso.Cells[i];
                TableCell celdaTotal = filaTotal.Cells[i];

                TableCell celdaEgresoSueldos = filaEgresoSueldos.Cells[i];
                TableCell celdaEgresoSUSS = filaEgresoSUSS.Cells[i];
                TableCell celdaEgresoPagoAProveedores = filaEgresoPagoAProveedores.Cells[i];
                TableCell celdaTotalEgresos = filaTotalEgresos.Cells[i];

                TableCell celdaSaldo = filaSaldo.Cells[i];


                celdaArrastre.Text = Convert.ToDecimal(celdaArrastre.Text).ToString("N2", new CultureInfo("en-US"));
                celdaEfectivoContado.Text = Convert.ToDecimal(celdaEfectivoContado.Text).ToString("N2", new CultureInfo("en-US"));
                celdaIngresoCheque.Text = Convert.ToDecimal(celdaIngresoCheque.Text).ToString("N2", new CultureInfo("en-US"));
                celdaIngreso.Text = Convert.ToDecimal(celdaIngreso.Text).ToString("N2", new CultureInfo("en-US"));
                celdaTotal.Text = Convert.ToDecimal(celdaTotal.Text).ToString("N2", new CultureInfo("en-US"));


                celdaEgresoSueldos.Text = Convert.ToDecimal(celdaArrastre.Text).ToString("N2", new CultureInfo("en-US"));
                celdaEgresoSUSS.Text = Convert.ToDecimal(celdaIngresoCheque.Text).ToString("N2", new CultureInfo("en-US"));
                celdaEgresoPagoAProveedores.Text = Convert.ToDecimal(celdaIngreso.Text).ToString("N2", new CultureInfo("en-US"));
                celdaTotalEgresos.Text = Convert.ToDecimal(celdaTotal.Text).ToString("N2", new CultureInfo("en-US"));



                celdaSaldo.Text = Convert.ToDecimal(celdaSaldo.Text).ToString("N2", new CultureInfo("en-US"));

            }
        }
        public void PonerSignoPesos(int Cont)
        {
            TableRow filaArrastre = (TableRow)phProductosyRecetasDiarios.Controls[0];
            TableRow filaEfectivoContado = (TableRow)phProductosyRecetasDiarios.Controls[1];
            TableRow filaIngresoCheque = (TableRow)phProductosyRecetasDiarios.Controls[2];
            TableRow filaIngreso = (TableRow)phProductosyRecetasDiarios.Controls[3];
            TableRow filaTotal = (TableRow)phProductosyRecetasDiarios.Controls[4];

            TableRow filaEgresoSueldos = (TableRow)phProductosyRecetasDiarios.Controls[5];
            TableRow filaEgresoSUSS = (TableRow)phProductosyRecetasDiarios.Controls[6];
            TableRow filaEgresoPagoAProveedores = (TableRow)phProductosyRecetasDiarios.Controls[7];
            TableRow filaTotalEgresos = (TableRow)phProductosyRecetasDiarios.Controls[8];

            TableRow filaSaldo = (TableRow)phProductosyRecetasDiarios.Controls[9];

            for (int i = 2; i <= Cont; i++)
            {
                TableCell celdaArrastre = filaArrastre.Cells[i];
                TableCell celdaEfectivoContado = filaEfectivoContado.Cells[i];
                TableCell celdaIngresoCheque = filaIngresoCheque.Cells[i];
                TableCell celdaIngreso = filaIngreso.Cells[i];
                TableCell celdaTotal = filaTotal.Cells[i];

                TableCell celdaEgresoSueldos = filaEgresoSueldos.Cells[i];
                TableCell celdaEgresoSUSS = filaEgresoSUSS.Cells[i];
                TableCell celdaEgresoPagoAProveedores = filaEgresoPagoAProveedores.Cells[i];
                TableCell celdaTotalEgresos = filaTotalEgresos.Cells[i];

                TableCell celdaSaldo = filaSaldo.Cells[i];


                celdaArrastre.Text = "$" + celdaArrastre.Text;
                celdaEfectivoContado.Text = "$" + celdaEfectivoContado.Text;
                celdaIngresoCheque.Text = "$" + celdaIngresoCheque.Text;
                celdaIngreso.Text = "$" + celdaIngreso.Text;
                celdaTotal.Text = "$" + celdaTotal.Text;

                celdaEgresoSueldos.Text = "$" + celdaEgresoSueldos.Text;
                celdaEgresoSUSS.Text = "$" + celdaEgresoSUSS.Text;
                celdaEgresoPagoAProveedores.Text = "$" + celdaEgresoPagoAProveedores.Text;
                celdaTotalEgresos.Text = "$" + celdaTotalEgresos.Text;

                celdaSaldo.Text = "$" + celdaSaldo.Text;
            }
        }
        public void PonerCeros(int Cont)
        {
            TableRow filaArrastre = (TableRow)phProductosyRecetasDiarios.Controls[0];
            TableRow filaEfectivoContado = (TableRow)phProductosyRecetasDiarios.Controls[1];
            TableRow filaIngresoCheque = (TableRow)phProductosyRecetasDiarios.Controls[2];
            TableRow filaIngreso = (TableRow)phProductosyRecetasDiarios.Controls[3];
            TableRow filaTotal = (TableRow)phProductosyRecetasDiarios.Controls[4];
            TableRow filaEgresoSueldos = (TableRow)phProductosyRecetasDiarios.Controls[5];
            TableRow filaEgresoSUSS = (TableRow)phProductosyRecetasDiarios.Controls[6];
            TableRow filaEgresoPagoAProveedores = (TableRow)phProductosyRecetasDiarios.Controls[7];
            TableRow filaTotalEgresos = (TableRow)phProductosyRecetasDiarios.Controls[8];
            TableRow filaSaldo = (TableRow)phProductosyRecetasDiarios.Controls[9];

            for (int i = 2; i <= Cont; i++)
            {
                TableCell celdaArrastre = filaArrastre.Cells[i];
                TableCell celdaEfectivoContado = filaEfectivoContado.Cells[i];
                TableCell celdaIngresoCheque = filaIngresoCheque.Cells[i];
                TableCell celdaIngreso = filaIngreso.Cells[i];
                TableCell celdaTotal = filaTotal.Cells[i];

                TableCell celdaEgresoSueldos = filaEgresoSueldos.Cells[i];
                TableCell celdaEgresoSUSS = filaEgresoSUSS.Cells[i];
                TableCell celdaEgresoPagoAProveedores = filaEgresoPagoAProveedores.Cells[i];
                TableCell celdaTotalEgresos = filaTotalEgresos.Cells[i];

                TableCell celdaSaldo = filaSaldo.Cells[i];


                celdaArrastre.Text = "0";
                celdaArrastre.HorizontalAlign = HorizontalAlign.Right;
                celdaEfectivoContado.Text = "0";
                celdaEfectivoContado.HorizontalAlign = HorizontalAlign.Right;
                celdaIngreso.Text = "0";
                celdaIngreso.HorizontalAlign = HorizontalAlign.Right;
                celdaIngresoCheque.Text = "0";
                celdaIngresoCheque.HorizontalAlign = HorizontalAlign.Right; ;
                celdaTotal.Text = "0";
                celdaTotal.HorizontalAlign = HorizontalAlign.Right;
                celdaTotal.Font.Bold = true;
                celdaTotal.ForeColor = System.Drawing.Color.Green;
                celdaEgresoSueldos.Text = "0";
                celdaEgresoSueldos.HorizontalAlign = HorizontalAlign.Right;
                celdaEgresoSUSS.Text = "0";
                celdaEgresoSUSS.HorizontalAlign = HorizontalAlign.Right;
                celdaEgresoPagoAProveedores.Text = "0";
                celdaEgresoPagoAProveedores.HorizontalAlign = HorizontalAlign.Right;
                celdaTotalEgresos.Text = "0";
                celdaTotalEgresos.HorizontalAlign = HorizontalAlign.Right;
                celdaTotalEgresos.Font.Bold = true;
                celdaTotalEgresos.ForeColor = System.Drawing.Color.Red;

                celdaSaldo.Text = "0";
                celdaSaldo.HorizontalAlign = HorizontalAlign.Right;
                celdaSaldo.Font.Bold = true;
                celdaSaldo.Font.Size = new FontUnit("17px");


                // phProductosyRecetasDiarios.Controls.Add(tr);
            }
        }
        public void CargarDatosEnTabla(int Cont, DataTable dt, DataTable dtCheques, string FechaD, string FechaH)
        {

            //Ingreso
            TableRow filaArrastre = (TableRow)phProductosyRecetasDiarios.Controls[0];
            TableRow filaEfectivoContado = (TableRow)phProductosyRecetasDiarios.Controls[1];
            TableRow filaIngresoCheque = (TableRow)phProductosyRecetasDiarios.Controls[2];
            TableRow filaIngreso = (TableRow)phProductosyRecetasDiarios.Controls[3];
            TableRow filaTotal = (TableRow)phProductosyRecetasDiarios.Controls[4];
            TableRow filaEgresoSueldos = (TableRow)phProductosyRecetasDiarios.Controls[5];
            TableRow filaEgresoSUSS = (TableRow)phProductosyRecetasDiarios.Controls[6];
            TableRow filaEgresoPagoAProveedores = (TableRow)phProductosyRecetasDiarios.Controls[7];
            TableRow filaTotalEgresos = (TableRow)phProductosyRecetasDiarios.Controls[8];
            TableRow filaSaldo = (TableRow)phProductosyRecetasDiarios.Controls[9];


            DataTable dtEfectivoContado = new DataTable();
            DataTable dtEgresos = new DataTable();
            dtEfectivoContado = controladorCashFlow.FiltrarCobrosContadoAllColumns(FechaD, FechaH);
            dtEgresos = controladorCashFlow.ObtenerEgresos(FechaD, FechaH);

            DateTime FechaDesdeDateTime = Convert.ToDateTime(FechaD);
            for (int i = 2; i <= Cont; i++)
            {


                TableCell celdaArrastre = filaArrastre.Cells[i];
                TableCell celdaEfectivoContado = filaEfectivoContado.Cells[i];
                TableCell celdaIngresoCheque = filaIngresoCheque.Cells[i];
                TableCell celdaIngreso = filaIngreso.Cells[i];
                TableCell celdaTotal = filaTotal.Cells[i];

                TableCell celdaEgresoSueldos = filaEgresoSueldos.Cells[i];
                TableCell celdaEgresoSUSS = filaEgresoSUSS.Cells[i];
                TableCell celdaEgresoPagoAProveedores = filaEgresoPagoAProveedores.Cells[i];
                TableCell celdaTotalEgresos = filaTotalEgresos.Cells[i];

                TableCell celdaSaldo = filaSaldo.Cells[i];

                decimal ArrastreDia = 0;
                decimal IngresoDiarioEfectivoContado = 0;
                decimal IngresoDiarioCheque = 0;
                //Esta variable es el total de tarjeta
                decimal IngresoDiario = 0;
                decimal EgresoDiarioSueldos = 0;
                decimal EgresoDiarioSUSS = 0;
                decimal EgresoEgresoPagoAProveedores = 0;
                decimal TotalDia = 0;
                decimal TotalEgresos = 0;
                decimal SaldoDia = 0;

                //EfectivoContado
                foreach (DataRow dr in dtEfectivoContado.Rows)
                {

                    if (Convert.ToDateTime(dr["fecha"]) == FechaDesdeDateTime)
                    {
                        IngresoDiarioEfectivoContado += Convert.ToDecimal(dr["Importe"].ToString());
                        celdaEfectivoContado.Text = IngresoDiarioEfectivoContado.ToString("N2", new CultureInfo("en-US"));
                    }


                    TotalDia = IngresoDiarioEfectivoContado; //Seria mas el egreso pero todavia no esta
                    celdaTotal.Text = TotalDia.ToString("N2", new CultureInfo("en-US"));

                    SaldoDia = TotalDia;
                    celdaSaldo.Text = SaldoDia.ToString("N2", new CultureInfo("en-US"));
                }

                //Tarjeta
                foreach (DataRow dr in dt.Rows)
                {

                    if (Convert.ToDateTime(dr["fecha"]).AddDays(int.Parse(dr["AcreditaEn"].ToString())) == FechaDesdeDateTime)
                    {
                        IngresoDiario += Convert.ToDecimal(dr["Importe"].ToString());
                        celdaIngreso.Text = IngresoDiario.ToString("N2", new CultureInfo("en-US"));
                    }


                    TotalDia = IngresoDiario + IngresoDiarioEfectivoContado; //Seria mas el egreso pero todavia no esta
                    celdaTotal.Text = TotalDia.ToString("N2", new CultureInfo("en-US"));

                    SaldoDia = TotalDia;
                    celdaSaldo.Text = SaldoDia.ToString("N2", new CultureInfo("en-US"));
                }

                //Cheques
                foreach (DataRow dr in dtCheques.Rows)
                {

                    if (Convert.ToDateTime(dr["fecha"]) == FechaDesdeDateTime)
                    {
                        IngresoDiarioCheque += Convert.ToDecimal(dr["Importe"].ToString());
                        celdaIngresoCheque.Text = IngresoDiarioCheque.ToString("N2", new CultureInfo("en-US"));
                    }

                    TotalDia = IngresoDiarioEfectivoContado + IngresoDiario + IngresoDiarioCheque; //Seria mas el egreso pero todavia no esta
                    celdaTotal.Text = TotalDia.ToString("N2", new CultureInfo("en-US"));

                    SaldoDia = TotalDia;
                    celdaSaldo.Text = SaldoDia.ToString("N2", new CultureInfo("en-US"));

                }

                //EGRESOS
                foreach (DataRow dr in dtEgresos.Rows)
                {

                    if (Convert.ToDateTime(dr["fecha"]) == FechaDesdeDateTime)
                    {

                        if (dr["descripcion"].ToString() == "SUSS")
                        {
                            EgresoDiarioSUSS += Convert.ToDecimal(dr["Importe"].ToString());
                            celdaEgresoSUSS.Text = EgresoDiarioSUSS.ToString("N2", new CultureInfo("en-US"));
                        }

                        if (dr["descripcion"].ToString() == "Sueldos")
                        {
                            EgresoDiarioSueldos += Convert.ToDecimal(dr["Importe"].ToString());
                            celdaEgresoSueldos.Text = EgresoDiarioSueldos.ToString("N2", new CultureInfo("en-US"));
                        }

                    }

                    TotalEgresos = EgresoDiarioSueldos + EgresoDiarioSUSS + EgresoEgresoPagoAProveedores;
                    celdaTotalEgresos.Text = TotalEgresos.ToString("N2", new CultureInfo("en-US"));


                    SaldoDia = TotalDia - TotalEgresos;
                    celdaSaldo.Text = SaldoDia.ToString("N2", new CultureInfo("en-US"));

                }

                //EGRESOS, PAGO A PROVEEDORES
                ControladorFacturas controladorFacturas = new ControladorFacturas();
                List<Facturas> facturas = controladorFacturas.ObtenerTodasFactura();
                foreach (var item in facturas)
                {

                    if (item.FechaVencimiento == FechaDesdeDateTime)
                    {
                        EgresoEgresoPagoAProveedores += Convert.ToDecimal(item.ImporteTotal);
                        celdaEgresoPagoAProveedores.Text = EgresoEgresoPagoAProveedores.ToString("N2", new CultureInfo("en-US"));
                    }

                    TotalEgresos = EgresoDiarioSueldos + EgresoDiarioSUSS + EgresoEgresoPagoAProveedores;
                    celdaTotalEgresos.Text = TotalEgresos.ToString("N2", new CultureInfo("en-US"));


                    SaldoDia = TotalDia - TotalEgresos;
                    celdaSaldo.Text = SaldoDia.ToString("N2", new CultureInfo("en-US"));

                    int SaldoEsNegativoPrimeraColumna = SaldoNegativo(celdaSaldo.Text);

                    if (SaldoEsNegativoPrimeraColumna == -1)
                    {
                        celdaSaldo.Font.Bold = true;
                        celdaSaldo.ForeColor = System.Drawing.Color.Green;
                    }

                    else
                    {
                        celdaSaldo.Font.Bold = true;
                        celdaSaldo.ForeColor = System.Drawing.Color.Red;
                    }

                }
                FechaDesdeDateTime = FechaDesdeDateTime.AddDays(1);

                if (i > 2)
                {
                    TableCell celdaSaldoDiaAnterior = filaSaldo.Cells[i - 1];
                    celdaArrastre.Text = celdaSaldoDiaAnterior.Text;


                    celdaSaldo.Text = (decimal.Parse(celdaTotal.Text, NumberStyles.Currency, new CultureInfo("en-US")) +
                    decimal.Parse(celdaArrastre.Text, NumberStyles.Currency, new CultureInfo("en-US")) -
                    decimal.Parse(celdaTotalEgresos.Text, NumberStyles.Currency, new CultureInfo("en-US"))).ToString
                    ("N2", new CultureInfo("en-US"));

                    int SaldoEsNegativo = SaldoNegativo(celdaSaldo.Text);

                    if (SaldoEsNegativo == -1)
                    {
                        celdaSaldo.Font.Bold = true;
                        celdaSaldo.ForeColor = System.Drawing.Color.Green;
                    }

                    else
                    {
                        celdaSaldo.Font.Bold = true;
                        celdaSaldo.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        public int SaldoNegativo(string Saldo)
        {

            // Eliminamos los separadores de miles "," para que la cadena sea "-110005.00"
            string numeroSinSeparadores = Saldo.Replace(",", "");

            if (decimal.TryParse(numeroSinSeparadores, out decimal valorNumerico))
            {
                if (valorNumerico < 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                // Si la conversión falla, puedes manejar el error como prefieras,
                // por ejemplo, lanzar una excepción, devolver un valor predeterminado, etc.
                throw new ArgumentException("El formato del número no es válido.");
            }
        }

        public void CargarSaldoTabla(int Cont)
        {

            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_Saldo";


                TableCell celType = new TableCell();
                celType.Text = "SALDO";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);

                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }


                phProductosyRecetasDiarios.Controls.Add(tr);
            }
            catch (Exception ex)
            {
            }

        }
        public void CargarArrastreTabla(int Cont)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Arrastre";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);

                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }
                phProductosyRecetasDiarios.Controls.Add(tr);
            }
            catch (Exception)
            {


            }
        }

        public void CargarTotalesTabla(int Cont)
        {
            ContDiario++;
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_Totales_Ingresos";

                TableCell celType = new TableCell();
                celType.Text = "TOTAL INGRESOS";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                celType.ForeColor = System.Drawing.Color.Green;
                tr.Cells.Add(celType);

                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);
            }
            catch (Exception ex)
            {


            }
        }

        public int CargarInsumosDiariosCabeceraPH2(string fechad = "", string fechah = "")
        {
            try
            {
                Control control = phProductosyRecetasDiariosCabecera.FindControl("Cabecera_flow");
                phProductosyRecetasDiariosCabecera.Controls.Remove(control);

                TableRow tr = new TableRow();
                tr.ID = "Cabecera_flow";

                TableHeaderCell celType = new TableHeaderCell();
                celType.Text = "Tipo";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Right; // Cambiado a HorizontalAlign.Left
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                celType.CssClass = "no-right-align"; // Agrega una clase personalizada
                tr.Cells.Add(celType);

                TableHeaderCell celAcreditaEl = new TableHeaderCell();
                celAcreditaEl.Text = "Detalle";
                celAcreditaEl.VerticalAlign = VerticalAlign.Middle;
                celAcreditaEl.HorizontalAlign = HorizontalAlign.Right; // Cambiado a HorizontalAlign.Left
                celAcreditaEl.Width = Unit.Pixel(150);
                celAcreditaEl.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celAcreditaEl.Font.Bold = true;
                celAcreditaEl.CssClass = "no-right-align"; // Agrega una clase personalizada
                tr.Cells.Add(celAcreditaEl);
                //Hasta arriba esta ok


                int Cont = 1;
                for (DateTime fechaDesde = DateTime.Parse(fechad); fechaDesde <= DateTime.Parse(fechah); fechaDesde = fechaDesde.AddDays(1))
                {
                    TableHeaderCell celFecha = new TableHeaderCell();
                    string FechaDesdeString = ConvertDateFormat(fechaDesde.ToString());
                    celFecha.Text = fechaDesde.ToString("dd/MM/yyyy"); //FechaDesdeString.ToString();
                    celFecha.VerticalAlign = VerticalAlign.Middle;
                    celFecha.HorizontalAlign = HorizontalAlign.Right;
                    celFecha.Width = Unit.Pixel(150);
                    celFecha.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                    tr.Cells.Add(celFecha);
                    Cont++;
                }

                phProductosyRecetasDiariosCabecera.Controls.Add(tr);
                return Cont;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public void CargarInsumosDiariosPH2Tabla(int Cont)
        {
            ContDiario++;
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "ID_ImporteDiario"; //ContDiario.ToString();


                TableCell celType = new TableCell();
                celType.Text = "Ingreso";
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Pixel(150);
                celType.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celType.Font.Bold = true;
                tr.Cells.Add(celType);


                TableCell celTypeTarjetaCredito = new TableCell();
                celTypeTarjetaCredito.Text = "Tarjeta Credito";
                celTypeTarjetaCredito.VerticalAlign = VerticalAlign.Middle;
                celTypeTarjetaCredito.HorizontalAlign = HorizontalAlign.Left;
                celTypeTarjetaCredito.Width = Unit.Pixel(150);
                celTypeTarjetaCredito.Attributes.Add("style", "padding-bottom: 1px !important; padding-right: 10px;");
                celTypeTarjetaCredito.Font.Bold = true;
                tr.Cells.Add(celTypeTarjetaCredito);


                for (int i = 0; i < Cont; i++)
                {
                    TableCell emptyCell = new TableCell();
                    tr.Cells.Add(emptyCell);
                }

                phProductosyRecetasDiarios.Controls.Add(tr);

            }
            catch (Exception ex)
            {


            }
        }


        private void CargarConceptos()
        {
            try
            {
                var ListConceptos = controladorConceptos.ObtenerTodosConceptos();
                var builder = new System.Text.StringBuilder();
                foreach (Conceptos fila in ListConceptos)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_r_" + fila.id + "_" + fila.descripcion + "'>", fila.id + " - " + fila.descripcion));

                }




                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                this.ListConceptos.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }


        public void FiltrarVentas(string FechaD, string FechaH)
        {
            try
            {
                this.FechaDesde.Value = this.FechaD;
                this.FechaHasta.Value = this.FechaH;

                string FechaDesde = ConvertDateFormat(FechaD);
                string FechaHasta = ConvertDateFormat(FechaH);
                decimal total = 0;
                decimal totalEgreso = 0;
                decimal totalIngreso = 0;
                var dtContado = controladorCashFlow.FiltrarCobrosContado(FechaD, FechaH);
                decimal importeContado = 0;
                string descripcionContado = "Efectivo/Contado";

                foreach (DataRow row in dtContado.Rows)
                {
                    //vd.id = Convert.ToInt32(row["id"]);
                    //vd.fecha = ((DateTime)row["fecha"]);
                    //vd.idCliente = Convert.ToInt32(row["idCliente"]);
                    importeContado += Convert.ToDecimal(row["Importe"]);
                }
                CargarInsumosPH2("Ingreso", importeContado, descripcionContado, this.FechaD, this.FechaH);

                var dtTarjetaCredito = controladorCashFlow.FiltrarCobrosTarjetaCredito(FechaD, FechaH);
                decimal importeTarjetaCredito = 0;
                int CantDias = 0;
                DateTime fecha;
                string descripcion = "Tarjeta de Credito";
                decimal importeTC = 0;
                foreach (DataRow row in dtTarjetaCredito.Rows)
                {
                    fecha = ((DateTime)row["fecha"]);
                    importeTC = Convert.ToDecimal(row["Importe"]);
                    CantDias = Convert.ToInt32(row["AcreditaEn"]);
                    importeTarjetaCredito += importeTC;
                }
                CargarInsumosPH2("Ingreso", importeTarjetaCredito, descripcion, this.FechaD, this.FechaH);

                decimal importeCheque = 0;
                string descripcionCheque = "Cheque";
                var dtCheques = controladorCashFlow.FiltrarCobrosCheques(FechaD, FechaH);
                foreach (DataRow row in dtCheques.Rows)
                {
                    importeCheque += Convert.ToDecimal(row["Importe"]);

                }
                CargarInsumosPH2("Ingreso", importeCheque, descripcionCheque, this.FechaD, this.FechaH);

                var dtCashFlow = controladorCashFlow.FiltrarCashFlow(FechaD, FechaH);
                foreach (DataRow row in dtCashFlow.Rows)
                {
                    var concepto = controladorConceptos.ObtenerConceptosId(Convert.ToInt32(row["idConceptos"]));
                    decimal Importe = Convert.ToDecimal(row["Importe"]);
                    string tipo = (row["Tipo"]).ToString();
                    if (tipo == "Egreso")
                    {
                        totalEgreso += Importe;
                    }
                    else
                    {
                        totalIngreso += Importe;
                    }
                    IDEgresoIngreso = int.Parse(row["id"].ToString());
                    CargarInsumosPH2(tipo, Importe, concepto.descripcion);
                }


                total = importeCheque + importeTarjetaCredito + importeContado + totalIngreso;

                IngresoTotal.Value = FormatearNumero(total);
                EgresoTotal.Value = FormatearNumero(totalEgreso);
            }
            catch (Exception ex) { }
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

        private int verificarAcceso()
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
        public static DateTime ConvertirFecha(string fecha)
        {
            DateTime fechaConvertida;
            DateTime.TryParseExact(fecha, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fechaConvertida);
            return fechaConvertida;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int IDEgresoIngreso = 0;
                IDEgresoIngreso = Convert.ToInt32(this.TxtIDEditar.Text);
                string FechaD = txtFechaHoy.Text.Replace("-", "/");
                string FechaH = txtFechaVencimiento.Text.Replace("-", "/");

                if (IDEgresoIngreso > 0)
                {
                    Tecnocuisine_API.Entitys.CashFlow cashflowEditar = new Tecnocuisine_API.Entitys.CashFlow();
                    cashflowEditar.id = IDEgresoIngreso;
                    cashflowEditar.fecha = ConvertirFecha(txtDate.Text);
                    cashflowEditar.Tipo = ddlOptionsTipo.SelectedValue;
                    cashflowEditar.idConceptos = Convert.ToInt32(txtConceptos.Text.Split('-')[0]);
                    cashflowEditar.Importe = FormatNumber(txtImporte.Text);
                    int rtaEditar = controladorCashFlow.EditarCashFlow(cashflowEditar);

                    if (rtaEditar != -1)
                    {
                        Response.Redirect("Cashflow.aspx?a=2&i=1&FechaD=" + FechaD + "&FechaH=" + FechaH + "&Op=0");
                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "Error al editar!", "Warning");
                    }
                }
                else
                {
                    CashFlow cashflow = new CashFlow();
                    cashflow.fecha = ConvertirFecha(txtDate.Text);
                    cashflow.Tipo = ddlOptionsTipo.SelectedValue;
                    cashflow.idConceptos = Convert.ToInt32(txtConceptos.Text.Split('-')[0]);
                    cashflow.Importe = FormatNumber(txtImporte.Text);
                    cashflow.Estado = 1;


                    int i = controladorCashFlow.AgregarCashFlow(cashflow);
                    if (i > 0)
                    {
                        Response.Redirect("Cashflow.aspx?a=2&i=1&FechaD=" + FechaD + "&FechaH=" + FechaH + "&Op=0");

                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "Error al Agregar producto!", "Warning");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string RellenarCamposEditar(int idCashFlow)
        {
            try
            {
                Tecnocuisine_API.Controladores.ControladorCashFlow cCashFlow = new Tecnocuisine_API.Controladores.ControladorCashFlow();
                Tecnocuisine_API.Entitys.CashFlow cashflow = new Tecnocuisine_API.Entitys.CashFlow();
                cashflow = cCashFlow.ObtenerCashFlowId(idCashFlow);
                Tecnocuisine_API.Controladores.ControladorConceptos controladorConceptos = new Tecnocuisine_API.Controladores.ControladorConceptos();
                DataTable dt = controladorConceptos.ObtenerConceptosMasNumeroConceptoId((int)cashflow.idConceptos);
                //decimal Importe = cashflow.Importe.;
                var response = new
                {
                    Importe = cashflow.Importe.ToString("N2", new CultureInfo("en-US")),
                    Tipo = cashflow.Tipo,
                    Fecha = Convert.ToDateTime(cashflow.fecha).ToString("yyyy-MM-dd"),
                    idConceptos = cashflow.idConceptos,
                    //CashFlow = cashflow,
                    Concepto = dt.Rows[0][0].ToString()
                };

                // Serializar el objeto cashflow a JSON
                string cashflowJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);

                // Devolver el objeto cashflow serializado como respuesta
                return cashflowJson;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void CargarInsumosPH2(string Tipo, decimal importe, string descripcion, string fechad = "", string fechah = "")
        {
            cont++;
            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = cont.ToString();


                TableCell celType = new TableCell();
                celType.Text = Tipo;
                celType.VerticalAlign = VerticalAlign.Middle;
                celType.HorizontalAlign = HorizontalAlign.Left;
                celType.Width = Unit.Percentage(40);
                celType.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celType);



                TableCell celAcreditaEl = new TableCell();
                celAcreditaEl.Text = descripcion;
                celAcreditaEl.VerticalAlign = VerticalAlign.Middle;
                celAcreditaEl.HorizontalAlign = HorizontalAlign.Left;
                celAcreditaEl.Width = Unit.Percentage(40);
                celAcreditaEl.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAcreditaEl);

                TableCell NumProduc = new TableCell();
                NumProduc.Text = FormatearNumero(importe);
                NumProduc.VerticalAlign = VerticalAlign.Middle;
                NumProduc.HorizontalAlign = HorizontalAlign.Left;
                NumProduc.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;text-align: right;");
                tr.Cells.Add(NumProduc);

                if (descripcion == "Tarjeta de Credito" || descripcion == "Efectivo/Contado" || descripcion == "Cheque")
                {
                    string link = "";
                    if (descripcion == "Tarjeta de Credito")
                    {
                        link = "TarjetaDeCredito.aspx?c=0&FechaD=" + fechad + "&FechaH=" + fechah;
                    }
                    else if (descripcion == "Efectivo/Contado")
                    {
                        link = "Contado.aspx?p=0&FechaD=" + fechad + "&FechaH=" + fechah;
                    }
                    else
                    {
                        link = "ChequesCobros.aspx?c=0&FechaD=" + fechad + "&FechaH=" + fechah;
                    }
                    TableCell celAction = new TableCell();
                    LinkButton btnEditar = new LinkButton();


                    btnEditar.CssClass = "btn btn-xs";
                    btnEditar.Style.Add("background-color", "transparent");
                    btnEditar.Attributes.Add("href", link);
                    btnEditar.Attributes.Add("target", "_blank");
                    btnEditar.Attributes.Add("title", "Ver");
                    btnEditar.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnEditar.Text = "<span><i style='color:black;' class='fa fa-search'></i></span>";
                    celAction.Controls.Add(btnEditar);
                    celAction.Width = Unit.Percentage(10);
                    celAction.VerticalAlign = VerticalAlign.Middle;
                    celAction.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(celAction);

                }
                else
                {

                    TableCell celAction = new TableCell();

                    HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                    btnEditar.Attributes.Add("title", "Editar");
                    btnEditar.Attributes.Add("data-toggle", "tooltip");
                    btnEditar.Attributes.Add("class", "btn btn-xs");
                    btnEditar.Style.Add("background-color", "transparent");
                    btnEditar.Style.Add("margin-right", "10px");
                    //btnEliminar.Attributes.Add("title data-original-title", familia);
                    //btnVerFamilia.Attributes.Add("title data-original-title", "Editar");
                    btnEditar.ID = "btnEditar" + cont.ToString() + "";
                    btnEditar.InnerHtml = "<span><i style='color:#428bca;' class='fa fa-pencil'></i></span>";
                    btnEditar.Attributes.Add("OnClick", "AbrirModalEditarCashflow(" + IDEgresoIngreso + ");");
                    celAction.Controls.Add(btnEditar);

                    HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                    btnEliminar.Attributes.Add("title", "Eliminar");
                    btnEliminar.Attributes.Add("data-toggle", "tooltip");
                    btnEliminar.Attributes.Add("class", "btn btn-xs");
                    btnEliminar.Style.Add("background-color", "transparent");
                    btnEliminar.Style.Add("margin-right", "10px");
                    //btnEliminar.Attributes.Add("title data-original-title", familia);
                    //btnVerFamilia.Attributes.Add("title data-original-title", "Editar");
                    btnEliminar.ID = "btnEliminar" + cont.ToString() + "";
                    btnEliminar.InnerHtml = "<span><i style='color:#ed5565;' class='fa fa-trash'></i></span>";
                    btnEliminar.Attributes.Add("OnClick", "AbrirModalEliminarCashflow(" + IDEgresoIngreso + ");");
                    celAction.Controls.Add(btnEliminar);

                    celAction.Width = Unit.Percentage(10);
                    celAction.VerticalAlign = VerticalAlign.Middle;
                    celAction.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(celAction);

                }

                phProductosyRecetas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        public static double RevertirNumero(string numeroFormateado)
        {
            double numero = double.Parse(numeroFormateado.Replace(",", ""));
            return numero;
        }

        public static string FormatearNumero(decimal numero)
        {
            return numero.ToString("N2", new CultureInfo("en-US"));
        }
        protected void editarInsumo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("CashFlow.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        public static decimal ConvertToDecimal(string valor, string formato = "0,000,000.00")
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            NumberFormatInfo formatInfo = (NumberFormatInfo)culture.NumberFormat.Clone();

            // Remover separadores de miles y establecer el separador decimal
            formatInfo.NumberGroupSeparator = ",";
            formatInfo.NumberDecimalSeparator = ".";

            // Convertir el valor al formato adecuado
            string valorFormateado = valor.Replace(formatInfo.NumberGroupSeparator, "");

            // Convertir a decimal utilizando la configuración regional
            decimal resultado;
            if (decimal.TryParse(valorFormateado, NumberStyles.AllowDecimalPoint, formatInfo, out resultado))
            {
                return resultado;
            }

            // Si no se pudo convertir, intentar convertir a través de palabras numéricas
            string valorPalabras = TextToNumber(valor, culture);
            if (!string.IsNullOrEmpty(valorPalabras) && decimal.TryParse(valorPalabras, out resultado))
            {
                return resultado;
            }

            throw new ArgumentException("No se pudo convertir el valor proporcionado a decimal.");
        }

        public static string TextToNumber(string texto, CultureInfo culture)
        {
            string[] partes = texto.Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length > 0)
            {
                long valor = 0;
                for (int i = 0; i < partes.Length; i++)
                {
                    string palabra = partes[i].Trim();
                    if (!string.IsNullOrEmpty(palabra))
                    {
                        try
                        {
                            valor += (long)double.Parse(palabra, culture);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                return valor.ToString();
            }

            return null;
        }



        public static decimal FormatNumber(string number)
        {
            return ConvertToDecimal(number);
        }


        public void LimpiarCampos()
        {
            try
            {
                //txtDescripcionInsumo.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        //Este funcion es el onclick del boton Eliminar
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int IDEgresoIngreso = Convert.ToInt32(this.txtIDIngresoEgreso.Text);
            int RtaEliminar = controladorCashFlow.EliminarCashFlow(IDEgresoIngreso);

            if (RtaEliminar != -1)
            {
                Response.Redirect("Cashflow.aspx?a=2&i=1&FechaD=" + FechaD + "&FechaH=" + FechaH + "&Op=0");
            }
            else
            {
                this.m.ShowToastr(this.Page, "Error al eliminar!", "Warning");
            }

            // Response.Redirect("Cashflow.aspx", false);
        }
    }
}