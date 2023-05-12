﻿using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using System.Xml.Linq;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static Gestion_Api.Auxiliares.PushNotification.AppCenterPush;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class Compras : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorSectorProductivo ControladorSector = new ControladorSectorProductivo();
        ControladorUnidad controladorUnidad = new ControladorUnidad();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        ControladorEntregas ControladorEntregas = new ControladorEntregas();
        ControladorReceta ControladorReceta = new ControladorReceta();
        ControladorMarca controladorMarca = new ControladorMarca();
        ControladorPresentacion controladorPresentacion1 = new ControladorPresentacion();
        ControladorTipoDocumento controladorTipoDocumento = new ControladorTipoDocumento();
        ControladorProveedores cp = new ControladorProveedores();
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        int accion;
        string id = "";
        int Mensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
                accion = Convert.ToInt32(Request.QueryString["a"]);


                if (Request.QueryString["i"] != null)
                {
                    id = Request.QueryString["i"].ToString();
                }
                else
                {
                    id = "";
                }


                if (Mensaje == 1)
                {
                    this.m.ShowToastr(this.Page, "Compra Generada con Exito!", "Exito");
                }
            }
            if (id != "")
            {
                string[] ids = id.Split('-');
                RellenarCampos(ids);
            }
            ObtenerTipoDocumento();
            ObtenerTodosLosProveedores(); /*ListaEntregas ListaProveedores*/
            ObtenerTodasLasEntregas();
        }

        public void ObtenerTodosLosProveedores()
        {
            try
            {

                var allTipos = cp.ObtenerProveedoresAll();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in allTipos)
                {

                    builder.Append(String.Format("<option value='{0}' id='" + rec.Id + "'>", rec.Id + " - " + rec.Alias));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaProveedores.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerTodasLasEntregas()
        {
            try
            {

                var allTipos = ControladorEntregas.ObtenerEntregasAll();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in allTipos)
                {

                    builder.Append(String.Format("<option value='{0}' id='" + rec.id + "'>", rec.CodigoEntrega));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaEntregas.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }
        public void ObtenerTipoDocumento()
        {
            try
            {

                var allTipos = controladorTipoDocumento.ObtenerTipoDocumento();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in allTipos)
                {

                    builder.Append(String.Format("<option value='{0}' id='" + rec.id + "'>", rec.id + " - " + rec.Descripcion));
                }



                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaTipoDoc.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }
        public void RellenarCampos(string[] ids)
        {
            try
            {
                //NumeroEntregas
                string numerosentregas = "";
                string proveedor = "";

                foreach (string item in ids)
                {
                    if (item != "")
                    {

                        var entrega = ControladorEntregas.ObtenerEntregasByID(Convert.ToInt32(item));
                        if (numerosentregas == "")
                        {
                            numerosentregas += "#" + entrega.CodigoEntrega;
                        }
                        else
                        {
                            numerosentregas += " - " + "#" + entrega.CodigoEntrega;
                        }

                        proveedor = entrega.Proveedores.Id + "-" + entrega.Proveedores.Alias;
                    }
                }
                txtProveedor.Text = proveedor;
                NumeroEntregas.Value = numerosentregas;
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static int GenerarFactura(string CodigoEntregas, string FechaActual, string TipoDocumento, string ImporteTotal, string Proveedor, string NumeroFactura, string FechaVencimiento)
        {
            try
            {
                ControladorEntregas ControladorEntregas = new ControladorEntregas();
                ControladorFacturas cf = new ControladorFacturas();
                ControladorCuentaCorriente cc = new ControladorCuentaCorriente();
                ControladorProveedores controladorProveedores = new ControladorProveedores();
                int idProveedor = Convert.ToInt32(Proveedor.Split('-')[0].Trim());
                string CodigosCorrectos = CodigoEntregas.Replace("#", "");
                string[] Codigos = { };
                if (CodigosCorrectos.Contains('-'))
                {

                    Codigos = CodigosCorrectos.Split('-');
                }
                else
                {
                    string[] Auxiliar = { CodigosCorrectos };
                    Codigos = Auxiliar;
                }
                DateTime FechaActual2 = DateTime.ParseExact(FechaActual, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime FechaVencimiento2 = DateTime.ParseExact(FechaVencimiento, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                Facturas facturas = new Facturas();
                string tipodoc = TipoDocumento.Split('-')[0];
                string importetotalReplace = ImporteTotal.Replace(",", "");
                facturas.TipoDocumentoID = Convert.ToInt32(tipodoc);
                facturas.FechaEmitido = FechaActual2;
                facturas.FechaVencimiento = FechaVencimiento2;
                facturas.ImporteTotal = Convert.ToDecimal(importetotalReplace.Replace(".", ","));
                facturas.idProveedor = idProveedor;
                facturas.NumeroFactura = NumeroFactura;
                bool Verificacion = cf.VerificarNumeroFactura(NumeroFactura, idProveedor);
                if (Verificacion == false)
                {
                    int idfac = cf.AgregarFactura(facturas);
                    if (Codigos.Count() != 0)
                    {
                        foreach (string cod in Codigos)
                        {
                            if (cod.Trim() != "")
                            {

                                var entre = ControladorEntregas.ObtenerEntregasByCodigoEntrega(cod.Trim());
                                ControladorEntregas.CambiarEstadoFacturacion(entre);
                                int result2 = cf.AgregarDetalleFactura(idfac, entre.id);
                                if (result2 == -1)
                                {
                                    return 3;
                                }
                            }
                        }
                    }
                    Tecnocuisine_API.Entitys.CuentaCorriente cuentaCorriente = new Tecnocuisine_API.Entitys.CuentaCorriente();
                    cuentaCorriente.fecha = FechaActual2;
                    cuentaCorriente.fechaVTO = FechaVencimiento2;
                    cuentaCorriente.idFactura = idfac;
                    cuentaCorriente.Descripcion = TipoDocumento.Split('-')[1] + " " + NumeroFactura;
                    int numTipoDocumento = Convert.ToInt32(tipodoc);
                    cuentaCorriente.idProveedor = idProveedor;
                    if (TipoDocumento.Split('-')[1].ToLower().Contains("credito"))
                    {
                        cuentaCorriente.Debe = 0;
                        cuentaCorriente.Haber = Convert.ToDecimal(ImporteTotal.Replace(",", "").Replace(".", ","));
                    }
                    else
                    {
                        cuentaCorriente.Debe = Convert.ToDecimal(ImporteTotal.Replace(",", "").Replace(".", ","));
                        cuentaCorriente.Haber = 0;
                    }
                    int result = cc.AgregarEnCuentaCorriente(cuentaCorriente);
                    if (result == -1)
                    {
                        return 2;
                    }
                    return 1;
                }
                else
                {
                    return 99;
                }
            }/*7 - 8 - 9 - 11 - 25*/
            catch (Exception)
            {
                return 4;
            }
        }


        [WebMethod]
        public static int GenerarFacturaImpuestos(string CodigoEntregas, string FechaActual, string TipoDocumento, string ImporteTotal, string Proveedor, string NumeroFactura, string FechaVencimiento, string txtNeto2Value, string divNeto10Value, string divNeto27Value, string divNeto5Value, string txtIB, string PercepcionIVA, string divNeto21Value, string RetIIBB, string RetIVA, string RetGan, string RetSUSS, string divITC, string Otros, string NetoNOGrabado, string ImpInt, string TotalImp, string Observaciones)
        {
            try
            {
                ControladorEntregas ControladorEntregas = new ControladorEntregas();
                ControladorFacturas cf = new ControladorFacturas();
                ControladorImpuesto ci = new ControladorImpuesto();
                ControladorProveedores controladorProveedores = new ControladorProveedores();
                ControladorCuentaCorriente cc = new ControladorCuentaCorriente();

                int idProveedor = Convert.ToInt32(Proveedor.Split('-')[0].Trim());
                string CodigosCorrectos = CodigoEntregas.Replace("#", "");
                string[] Codigos = { };

                if (CodigosCorrectos.Contains('-'))
                {

                    Codigos = CodigosCorrectos.Split('-');
                }
                else
                {
                    string[] Auxiliar = { CodigosCorrectos };
                    Codigos = Auxiliar;
                }
                DateTime FechaActual2 = DateTime.ParseExact(FechaActual, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime FechaVencimiento2 = DateTime.ParseExact(FechaVencimiento, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                Facturas facturas = new Facturas();
                string tipodoc = TipoDocumento.Split('-')[0];
                facturas.TipoDocumentoID = Convert.ToInt32(tipodoc);
                facturas.FechaEmitido = FechaActual2;
                facturas.FechaVencimiento = FechaVencimiento2;
                facturas.ImporteTotal = Convert.ToDecimal(ImporteTotal.Replace(",", "").Replace(".", ","));
                facturas.idProveedor = idProveedor;
                facturas.NumeroFactura = NumeroFactura;
                bool Verificacion = cf.VerificarNumeroFactura(NumeroFactura, idProveedor);
                if (Verificacion == false)
                {

                    int idfac = cf.AgregarFactura(facturas);
                    if (Codigos.Count() != 0)
                    {
                        foreach (string cod in Codigos)
                        {
                            if (cod.Trim() != "")
                            {

                                var entre = ControladorEntregas.ObtenerEntregasByCodigoEntrega(cod.Trim());
                                ControladorEntregas.CambiarEstadoFacturacion(entre);
                                int result2 = cf.AgregarDetalleFactura(idfac, entre.id);
                                if (result2 != -1)
                                {
                                    return 3;
                                }
                            }
                        }
                    }
                    if (idfac > 0)
                    {
                        impuestos imp = new impuestos();
                        imp.idFactura = idfac;
                        imp.neto_2_5 = Convert.ToDecimal(txtNeto2Value.Replace(",", "").Replace(".", ","));
                        imp.neto_10_5 = Convert.ToDecimal(divNeto10Value.Replace(",", "").Replace(".", ","));
                        imp.neto_27 = Convert.ToDecimal(divNeto27Value.Replace(",", "").Replace(".", ","));
                        imp.neto_5 = Convert.ToDecimal(divNeto5Value.Replace(",", "").Replace(".", ","));
                        imp.ingresos_brutos = Convert.ToDecimal(txtIB.Replace(",", "").Replace(".", ","));
                        imp.percepcion_iva = Convert.ToDecimal(PercepcionIVA.Replace(",", "").Replace(".", ","));
                        imp.neto_21 = Convert.ToDecimal(divNeto21Value.Replace(",", "").Replace(".", ","));
                        imp.retencion_iibb = Convert.ToDecimal(RetIIBB.Replace(",", "").Replace(".", ","));
                        imp.retencion_iva = Convert.ToDecimal(RetIVA.Replace(",", "").Replace(".", ","));
                        imp.retencion_ganancias = Convert.ToDecimal(RetGan.Replace(",", "").Replace(".", ","));
                        imp.retencion_suss = Convert.ToDecimal(RetSUSS.Replace(",", "").Replace(".", ","));
                        imp.itc = Convert.ToDecimal(divITC.Replace(",", "").Replace(".", ","));
                        imp.otros = Convert.ToDecimal(Otros.Replace(",", "").Replace(".", ","));
                        imp.neto_no_grabado = Convert.ToDecimal(NetoNOGrabado.Replace(",", "").Replace(".", ","));
                        imp.impuestos_internos = Convert.ToDecimal(ImpInt.Replace(",", "").Replace(".", ","));
                        imp.total = Convert.ToDecimal(TotalImp.Replace(",", "").Replace(".", ","));
                        imp.Observaciones = Observaciones;
                        var impuest = ci.AgregarImpuestos(imp);
                        Tecnocuisine_API.Entitys.CuentaCorriente cuentaCorriente = new Tecnocuisine_API.Entitys.CuentaCorriente();
                        cuentaCorriente.fecha = FechaActual2;
                        cuentaCorriente.idFactura = idfac;
                        cuentaCorriente.fechaVTO = FechaVencimiento2;
                        cuentaCorriente.Descripcion = TipoDocumento.Split('-')[1] + " " + NumeroFactura;
                        cuentaCorriente.idProveedor = idProveedor;
                        int numTipoDocumento = Convert.ToInt32(tipodoc);
                        if (TipoDocumento.Split('-')[1].ToLower().Contains("credito"))
                        {
                            cuentaCorriente.Debe = 0;
                            cuentaCorriente.Haber = Convert.ToDecimal(ImporteTotal.Replace(",", "").Replace(".", ","));
                        }
                        else
                        {
                            cuentaCorriente.Debe = Convert.ToDecimal(ImporteTotal.Replace(",", "").Replace(".", ","));
                            cuentaCorriente.Haber = 0;
                        }
                        int result = cc.AgregarEnCuentaCorriente(cuentaCorriente);
                        if (result == -1)
                        {
                            return 2;
                        }
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 99;
                }
            }
            catch (Exception)
            {
                return 2;
            }
        }
    }
}