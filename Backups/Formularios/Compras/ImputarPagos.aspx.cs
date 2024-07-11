using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using Tecnocuisine.Formularios.Compras;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine.Compras
{
    public partial class ImputarPagos : Page
    {
        Mensaje m = new Mensaje();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        ControladorCuentaCorriente controladorCuentaCorriente = new ControladorCuentaCorriente();
        ControladorCuentasBancarias controladorEntidadesBancarias = new ControladorCuentasBancarias();
        ControladorEntidadesBancarias controladorBancos = new ControladorEntidadesBancarias();
        ControladorEntidad controladorEntidad = new ControladorEntidad();
        ControladorProveedores controladorProveedores = new ControladorProveedores();
        ControladorCobrosRealizados controladorCobrosRealizados = new ControladorCobrosRealizados();

        int accion;

        int Mensaje;
        string ids;
        string monto = "";
        int idcliente;
        protected void Page_Load(object sender, EventArgs e)
        {

            //VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);


            if (Request.QueryString["i"] != null)
            {
                ids = Request.QueryString["i"].ToString();
                if (Request.QueryString["t"] != null)
                {
                    monto = Request.QueryString["t"].ToString();
                }
                if (Request.QueryString["c"] != null)
                {
                    idcliente = Convert.ToInt32(Request.QueryString["c"]);
                   
                }
            }
            else
            {
                Response.Redirect("Cobros.aspx");
                this.m.ShowToastr(this.Page, "Necesita seleccionar facturas a imputar!", "Error");
            }


            if (!IsPostBack)
            {
       
                if (accion == 2)
                {
                   
                }

                if(Mensaje == 1)
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
            if (monto == "")
            {

            ObtenerFacturasImputadas(this.ids.Split('-'));
            } else
            {
             ObtenerFacturasImputadas(this.ids.Split('-'),monto);

            }
            cargarCuentasBancarias();
            cargarEntidadesBancarias();
            //cargarEntidades();
            CargarProveedorSeleccionado();
            CargarNumeroReciboCobro();


        }
       public void CargarNumeroReciboCobro()
        {
            try
            {
                idNumeroCobro.Value = controladorCobrosRealizados.TraerUltimoNum();
            }catch(Exception ex)
            {

            }
        }

        #region webMethod

        [WebMethod]

        public static string ImputarFacturas(string listFacturas,string ImporteCobro,string idProveedor,string listCheques,string listTarjetas,string totalefectivo)
        {
            ControladorCuentaCorriente controladorCuentaCorriente = new ControladorCuentaCorriente();
            ControladorPagoRealizados controladorPagoRealizados = new ControladorPagoRealizados();
            ControladorVentas controladorVentas = new ControladorVentas();


            try
            {
                string[] listFac = listFacturas.Split('%');
                int conterrores = 0;
                foreach (string item in listFac)
                {
                    int id = Convert.ToInt32(item.Split('-')[0].Trim());
                    decimal monto = ConvertToDecimal(item.Split('-')[1].Trim());
                  var factura = controladorCuentaCorriente.BuscarCuentaCorrienteByID(id);
                    factura.Saldo = monto;
                   int result2 = controladorCuentaCorriente.EditarCuentaCorriente(factura);

                   if (result2 < 1)
                    {
                        conterrores++;
                    }
                }
                Tecnocuisine_API.Entitys.PagosRealizados pagos = new Tecnocuisine_API.Entitys.PagosRealizados();
                string codigocobro = controladorPagoRealizados.TraerUltimoNum();
                pagos.fecha = DateTime.Now;
                pagos.importe = ConvertToDecimal(ImporteCobro);
                pagos.idProveedor = Convert.ToInt32(idProveedor);
                pagos.numeroRecibo = codigocobro;
                pagos.idTipoDocumento = 102;
              int result = controladorPagoRealizados.AgregarEnPagoRealizados(pagos);


                ControladorTipoDocumento tipodoc = new ControladorTipoDocumento();
                Tecnocuisine_API.Entitys.TipoDocumento doc = tipodoc.ObtenerTipoDocumentoByID(103);
                Tecnocuisine_API.Entitys.CuentaCorriente pagonuevo = new Tecnocuisine_API.Entitys.CuentaCorriente();
                pagonuevo.idProveedor = pagos.idProveedor;
                pagonuevo.Haber = 0.00M;
                pagonuevo.Debe = pagos.importe * -1;
                pagonuevo.Saldo = pagonuevo.Debe + pagonuevo.Haber;
                pagonuevo.fecha = DateTime.Now;
                pagonuevo.fechaVTO = DateTime.Now;
                string codigofac = controladorPagoRealizados.TraerUltimoNum2();
                pagonuevo.Descripcion = doc.Descripcion + " " + codigofac;

                controladorCuentaCorriente.AgregarEnCuentaCorriente(pagonuevo);

                //if (listTarjetas != "")
                //{
                //    string[] list = listTarjetas.Split('%');
                //    foreach (string str in list)
                //    {
                //        if (str != "")
                //        {
                //            int entidad = Convert.ToInt32(str.Split('/')[0]);
                //            int tarjeta = Convert.ToInt32(str.Split('/')[1]);
                //            decimal saldo = ConvertToDecimal(str.Split('/')[2]);


                //            Tecnocuisine_API.Entitys.CuentaTarjetaCreditoVentas CuentaTarjetaCreditoVentas = new Tecnocuisine_API.Entitys.CuentaTarjetaCreditoVentas();
                //        ControladorTarjetaDeCreditoVenta controladorTarjetaVenta = new ControladorTarjetaDeCreditoVenta();
                //        ControladorTipoDocumento controladorTipoDocumento = new ControladorTipoDocumento();
                //        var TipoDocumento = controladorTipoDocumento.ObtenerTipoDocumentoByID(102);
                //            CuentaTarjetaCreditoVentas.Haber = saldo;
                //            CuentaTarjetaCreditoVentas.Saldo = saldo;
                //            CuentaTarjetaCreditoVentas.Debe = 0M;

                //            CuentaTarjetaCreditoVentas.fecha = DateTime.Now;
                //        CuentaTarjetaCreditoVentas.idVenta = null;
                //        CuentaTarjetaCreditoVentas.Descripcion = TipoDocumento.Descripcion + " " + codigofac;
                //        CuentaTarjetaCreditoVentas.idCliente = cobros.idCliente;
                      
                //        CuentaTarjetaCreditoVentas.idTarjeta = Convert.ToInt32(tarjeta);
                //        CuentaTarjetaCreditoVentas.idEntidad = Convert.ToInt32(entidad);
                //        controladorTarjetaVenta.AgregarEnCuentaTarjetaCreditoVentas(CuentaTarjetaCreditoVentas);
                //        }
                //    }
                    
                //}
                if (listCheques != "")
                {
                    string[] list = listCheques.Split('%');
                    foreach (string str in list)
                    {
                        decimal importe = ConvertToDecimal(str.Split('/')[0]);
                        string numero = (str.Split('/')[1]).ToString();
                        int idBanco = Convert.ToInt32(str.Split('/')[2]);
                        string cuenta = (str.Split('/')[3]).ToString();
                        string cuit = (str.Split('/')[4]).ToString();
                        string librador = (str.Split('/')[5]).ToString();
                        DateTime fechaCheque = ConvertirFecha(str.Split('/')[6]);

                        Tecnocuisine_API.Entitys.Cheques cheque = new Tecnocuisine_API.Entitys.Cheques();
                        ControladorCheques controladorCheque = new ControladorCheques();
                        ControladorTipoDocumento controladorTipoDocumento = new ControladorTipoDocumento();
                        var TipoDocumento = controladorTipoDocumento.ObtenerTipoDocumentoByID(103);
                        cheque.cuit = cuit;
                        cheque.Librador = librador;
                        cheque.fecha = fechaCheque;
                        cheque.idClientes = null;
                        cheque.idProveedor  = pagos.idProveedor;
                        cheque.Cuenta = cuenta;
                        cheque.idBanco = idBanco;
                        cheque.Numero = numero;
                        cheque.NumeroCheque = controladorCheque.TraerUltimoNumCheque();
                        cheque.Descripcion = TipoDocumento.Descripcion + " " + codigocobro;
                        cheque.Importe = importe;
                        cheque.fecha = fechaCheque;
                        cheque.Propio = true;
                        controladorCheque.AgregarEnCheques(cheque);
                    }

                }
                if (totalefectivo != "")
                {
                    Tecnocuisine_API.Entitys.CuentaContado cuentaContado = new Tecnocuisine_API.Entitys.CuentaContado();
                    ControladorCuentaContado controladorCuentaContado = new ControladorCuentaContado();
                    ControladorTipoDocumento controladorTipoDocumento = new ControladorTipoDocumento();
                    var TipoDocumento = controladorTipoDocumento.ObtenerTipoDocumentoByID(103);
                    cuentaContado.fecha = DateTime.Now;
                    cuentaContado.idVenta = null;
                    cuentaContado.Descripcion = TipoDocumento.Descripcion + " " + codigocobro;
                    cuentaContado.idCliente = null;
                    cuentaContado.idProveedor = pagos.idProveedor;

                    cuentaContado.Importe = (ConvertToDecimal(totalefectivo) * -1);
                    controladorCuentaContado.AgregarEnCuentaContado(cuentaContado);
                }




                if (result > 0 && conterrores == 0)
                {
                    return "";
                }
                else
                {
                    return "Se Detectaron Errores al imputar. Contacte con Soporte";
                }
            }catch(Exception e) {

                return "0";

            }
        }

        public static DateTime ConvertirFecha(string fecha)
        {
            DateTime resultado;
            string fecha2 = fecha.Replace('$', '/');
            if (DateTime.TryParseExact(fecha2, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out resultado))
            {
                return resultado;
            }
            else
            {
                throw new ArgumentException("El formato de fecha no es válido.");
            }
        }

        // FUNCIONES PARA PASAR DE 12,000,000.00 a 12000000.00 ---- SEGUN LA CONFIGURACION DEL PC (. o ,)
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


        [WebMethod]
        public static string ddlOpciones_SelectedTarjeta(string idEntidad)
        {
            try
            {
                ControladorTarjetas controladorTarjetas = new ControladorTarjetas();
                ControladorEntidad controladorEntidad = new ControladorEntidad();
                List<Tecnocuisine_API.Entitys.Tarjetas> listTarjetas = controladorTarjetas.ObtenerTarjetaPorIDEntidad(Convert.ToInt32(idEntidad));
                string i = "";
                foreach (var item in listTarjetas)
                {
                    i += item.id + "&" + item.nombre + "&" + item.AcreditaEn + "%";
                }

                return i;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        public void CargarProveedorSeleccionado()
        {
            try
            {
                var proveedor = controladorProveedores.ObtenerProveedorByID(this.idcliente);
                if (proveedor != null)
                {
                    idClienteFinal.Value = proveedor.Id + " - " + proveedor.Alias;
                }
            }
            catch(Exception ex)
            {

            }
        }


    


        private void cargarEntidadesBancarias()
        {
            try
            {
                var ListBancos = controladorBancos.ObtenerBancosAlldt();
                var builder = new System.Text.StringBuilder();
                foreach(DataRow fila in ListBancos.Rows)
                {
                    int id = Convert.ToInt32(fila["id"]);
                    string Nombre = (fila["Nombre"]).ToString();
                    builder.Append(String.Format("<option value='{0}' id='c_r_" + id + "_" + Nombre + "'>", id + " - " + Nombre));

                }




                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                this.ListBancos.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }



        //public void cargarEntidadesBancarias()
        //{
        //    try
        //    {
        //        var ListBancos = controladorBancos.ObtenerBancosAlldt();
        //        // Crear un nuevo DataTable
        //        DataView dv = new DataView(ListBancos);
        //        dv.Sort = "Nombre ASC";


        //        // Recorrer las filas del DataTable original



        //        this.txtBancoDropDown.DataSource = dv;
        //        this.txtBancoDropDown.DataValueField = "id";
        //        this.txtBancoDropDown.DataTextField = "Nombre";

        //        this.txtBancoDropDown.DataBind();



        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    
        public void cargarCuentasBancarias()
        {
            try
            {
                var ListEntidades = controladorEntidadesBancarias.ObtenerCuentaBancosAlldt();
                // Crear un nuevo DataTable
                DataTable nuevoDataTable = new DataTable();
                nuevoDataTable.Columns.Add("id", typeof(int));
                nuevoDataTable.Columns.Add("Descripcion", typeof(string));

                // Recorrer las filas del DataTable original
                foreach (DataRow fila in ListEntidades.Rows)
                {
                    // Obtener los valores de las columnas "ID", "Descripción" y "CuentaNúmero"
                    int id = Convert.ToInt32(fila["id"]);
                    string descripcion = fila["Descripcion"].ToString();
                    string cuentaNumero = fila["CuentaNumero"].ToString();

                    // Concatenar los valores de "Descripción" y "CuentaNúmero"
                    string descripcionCompleta = descripcion + " - " + cuentaNumero;

                    // Crear una nueva fila para el nuevo DataTable
                    DataRow nuevaFila = nuevoDataTable.NewRow();

                    // Asignar los valores a las columnas correspondientes en la nueva fila
                    nuevaFila["id"] = id;
                    nuevaFila["Descripcion"] = descripcionCompleta;

                    // Agregar la fila al nuevo DataTable
                    nuevoDataTable.Rows.Add(nuevaFila);
                }


                this.DropListCuentasBancarias.DataSource = nuevoDataTable;
                this.DropListCuentasBancarias.DataValueField = "id";
                this.DropListCuentasBancarias.DataTextField = "Descripcion";

                this.DropListCuentasBancarias.DataBind();



            }
            catch (Exception ex)
            {
            }
        }


        public void  ObtenerFacturasImputadas(string[] ids,string monto = ""){
            try
            {
                if (monto != "")
                {
                    Montotxt.Value = monto;
                }
                List<Tecnocuisine_API.Entitys.CuentaCorriente> listcuentas = new List<Tecnocuisine_API.Entitys.CuentaCorriente>();
                foreach(string item in ids)
                {
                    if (item != ""){

                        var cuenta = controladorCuentaCorriente.BuscarCuentaCorrienteByID(Convert.ToInt32(item));
                            if (cuenta != null) listcuentas.Add(cuenta);

                    }
                }
                decimal total = 0;
                foreach(var item in listcuentas)
                {
                    RellenarTablaPH(item);
                    total += Convert.ToDecimal(item.Saldo);
                }
                RellenarTablaPHFinalRow(total);
            }
            catch(Exception ex)
            {
                
            }
            }
        public void RellenarTablaPHFinalRow(decimal item)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "-1";

                TableCell celID = new TableCell();
                celID.Text = "";
                celID.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celID);

                TableCell celAlias = new TableCell();
                celAlias.Text = "Total";
                celAlias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celAlias);

                TableCell celObservaciones1 = new TableCell();
                TextBox textBoxPrimero = new TextBox(); // Crear un nuevo elemento TextBox
                textBoxPrimero.Enabled = false; // Deshabilitar el TextBox
                textBoxPrimero.Text = FormatearNumero(item);
                textBoxPrimero.CssClass = "form-control"; // Agregar una clase CSS si es necesario
                textBoxPrimero.Attributes.Add("style", "text-align: right;");
                textBoxPrimero.Attributes.Add("id", "textBoxIgnorado");
                celObservaciones1.VerticalAlign = VerticalAlign.Middle;
                celObservaciones1.Controls.Add(textBoxPrimero); // Agregar el TextBox a la celda
                tr.Cells.Add(celObservaciones1);

                TableCell celObservaciones = new TableCell();
                TextBox textBoxObservaciones = new TextBox(); // Crear un nuevo elemento TextBox
                textBoxObservaciones.Enabled = false; // Deshabilitar el TextBox
                textBoxObservaciones.Text = FormatearNumero(item);
                textBoxObservaciones.Attributes.Add("id", "SumaFinal");
                textBoxObservaciones.Attributes.Add("style", "text-align: right;");

                textBoxObservaciones.CssClass = "form-control"; // Agregar una clase CSS si es necesario
                celObservaciones.VerticalAlign = VerticalAlign.Middle;
                celObservaciones.Controls.Add(textBoxObservaciones); // Agregar el TextBox a la celda
                tr.Cells.Add(celObservaciones);


                phCuentaCorriente.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }
        public void RellenarTablaPH(Tecnocuisine_API.Entitys.CuentaCorriente item)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = item.id.ToString();

                TableCell celID = new TableCell();
                celID.Text = item.id.ToString();
                celID.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celID);

                TableCell celAlias = new TableCell();
                celAlias.Text = item.Descripcion;
                celAlias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celAlias);

                TableCell celSectorProd = new TableCell();
                celSectorProd.Text = (item.Saldo).ToString().Replace(",", ".");
                celSectorProd.VerticalAlign = VerticalAlign.Middle;
                celSectorProd.Attributes.Add("style", "text-align: right;");
                tr.Cells.Add(celSectorProd);

                TableCell celObservaciones = new TableCell();
                TextBox textBoxObservaciones = new TextBox(); // Crear un nuevo elemento TextBox
                textBoxObservaciones.Enabled = true;
                textBoxObservaciones.Attributes.Add("type", "text");
                textBoxObservaciones.Text = (FormatearNumero((decimal)item.Saldo));
                textBoxObservaciones.Attributes.Add("style", "text-align: right;");
                textBoxObservaciones.CssClass = "form-control"; // Agregar una clase CSS si es necesario
                celObservaciones.VerticalAlign = VerticalAlign.Middle;
                textBoxObservaciones.Attributes.Add("onchange", "ActualizarPrecioFinal(this)"); // Agregar el atributo onchange y pasar this como argumento
                celObservaciones.Controls.Add(textBoxObservaciones); // Agregar el TextBox a la celda
                tr.Cells.Add(celObservaciones);


                phCuentaCorriente.Controls.Add(tr);
            }
            catch(Exception ex)
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

        public static double RevertirNumero(string numeroFormateado)
        {
            double numero = double.Parse(numeroFormateado.Replace(",", ""));
            return numero;
        }

        public static string FormatearNumero(decimal numero)
        {
            return numero.ToString("N2", new CultureInfo("en-US"));
        }


        


    }
}