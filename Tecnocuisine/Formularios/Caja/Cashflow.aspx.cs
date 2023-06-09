using Gestion_Api.Controladores;
using Gestor_Solution.Controladores;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
        
        
        int accion;
        int Mensaje;
        string FechaD = "";
        string FechaH = "";
        int cont = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                CargarInsumosPH2("Ingreso", importeContado, descripcionContado,this.FechaD,this.FechaH);

                var dtTarjetaCredito = controladorCashFlow.FiltrarCobrosTarjetaCredito(FechaD, FechaH);
                decimal importeTarjetaCredito = 0;
                int CantDias = 0;
                DateTime fecha;
               string descripcion = "Tarjeta de Credito";
                foreach (DataRow row in dtTarjetaCredito.Rows)
                {
                    fecha = ((DateTime)row["fecha"]);
                    importeContado = Convert.ToDecimal(row["Importe"]);
                    CantDias = Convert.ToInt32(row["AcreditaEn"]);
                    importeTarjetaCredito += importeContado;
                }
                    CargarInsumosPH2("Ingreso", importeContado, descripcion, this.FechaD, this.FechaH);

                decimal importeCheque = 0;
                string descripcionCheque = "Cheque";
                var dtCheques = controladorCashFlow.FiltrarCobrosCheques(FechaD, FechaH);
                foreach (DataRow row in dtCheques.Rows)
                {
                    importeCheque += Convert.ToDecimal(row["Importe"]);

                }
                CargarInsumosPH2("Ingreso", importeCheque,descripcionCheque, this.FechaD, this.FechaH);

                var dtCashFlow = controladorCashFlow.FiltrarCashFlow(FechaD, FechaH);
                foreach(DataRow row in dtCashFlow.Rows)
                {
                    var concepto = controladorConceptos.ObtenerConceptosId(Convert.ToInt32(row["idConceptos"]));
                    decimal Importe = Convert.ToDecimal(row["Importe"]);
                    string tipo = (row["Tipo"]).ToString();
                    if (tipo == "Egreso")
                    {
                        totalEgreso += Importe;
                    } else
                    {
                        totalIngreso += Importe;
                    }
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
                CashFlow cashflow = new CashFlow();
                cashflow.fecha = ConvertirFecha(txtDate.Text);
                cashflow.Tipo = ddlOptionsTipo.SelectedValue;
                cashflow.idConceptos = Convert.ToInt32(txtConceptos.Text.Split('-')[0]);
                cashflow.Importe = FormatNumber(txtImporte.Text);


                int i = controladorCashFlow.AgregarCashFlow(cashflow);
                if (i > 0)
                {
                Response.Redirect("CashFlow.aspx?a=2&i=" + 1);

                } else
                {
                    this.m.ShowToastr(this.Page, "Error al Agregar producto!", "Warning");
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void CargarInsumosPH2(string Tipo, decimal importe, string descripcion,string fechad = "",string fechah = "")
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
                    } else if (descripcion == "Efectivo/Contado")
                    {
                        link = "Contado.aspx?p=0&FechaD=" + fechad + "&FechaH=" + fechah;
                    } else
                    {
                        link = "ChequesCobros.aspx?c=0&FechaD=" + fechad + "&FechaH=" + fechah;
                    }
                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
             

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Attributes.Add("href", link);
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-search'></i></span>";
                celAction.Controls.Add(btnEditar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                } else
                {

                    TableCell celAction = new TableCell();
                    LinkButton btnEditar = new LinkButton();


                    btnEditar.CssClass = "btn btn-xs";
                    btnEditar.Style.Add("background-color", "transparent");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnEditar.Text = "<span></span>";
                    celAction.Controls.Add(btnEditar);
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


    }
}