using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gestion_Api.Modelo;
using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Tecnocuisine.Formularios.Administrador;
using Tecnocuisine.Formularios.Ventas;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using WebGrease.Css.Ast.Selectors;
using static System.Net.Mime.MediaTypeNames;
using static Tecnocuisine.Formularios.Compras.Entregas;

namespace Tecnocuisine.Formularios.Compras
{
    public partial class OrdenDeCompraABM : System.Web.UI.Page
    {
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorSectorProductivo ControladorSector = new ControladorSectorProductivo();
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        ControladorEntregas ControladorEntregas = new ControladorEntregas();
        ControladorOrdenesDeCompra ControladorOrdenesDeCompra = new ControladorOrdenesDeCompra();
        ControladorReceta ControladorReceta = new ControladorReceta();
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        Mensaje m = new Mensaje();
        int accion;
        int id;
        int idSectorProductivo = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //accion = Convert.ToInt32(Request.QueryString["a"]);
            //id = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {
                ObtenerRecetas();
                ObtenerProductos();
                ObtenerProveedores();
                CargarSectores();

                txtFechaEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //if (accion == 2)
                //{
                //    CargarEntregaEdit();
                //}
            }

            CargarNumeroDeOrden();
        }

        private void CargarNumeroDeOrden()
        {
            var proximoId = ControladorOrdenesDeCompra.GetProximoId();
            var h3 = new HtmlGenericControl("h3");
            h3.InnerText = "#" + proximoId;
            h3.Attributes["style"] = "float: right; margin-right:10px;";
            lblProdNum.Controls.Add(h3);
        }

        public void ObtenerPresentaciones()
        {
            try
            {
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                var presentaciones = controladorPresentacion.ObtenerTodosPresentaciones();
                this.ddlPresentaciones.Items.Clear();
                this.ddlPresentaciones.DataSource = presentaciones;
                this.ddlPresentaciones.DataValueField = "id";
                this.ddlPresentaciones.DataTextField = "descripcion";
                this.ddlPresentaciones.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarEntregaEdit()
        {
            try
            {
                var entrega = ControladorEntregas.ObtenerEntregasByID(id);
                Tecnocuisine_API.Entitys.Proveedores p = ControladorProveedores.ObtenerProveedorByID(entrega.idProveedor);
                //Tecnocuisine_API.Entitys.SectorProductivo s = ControladorSector.ObtenerSectorProductivoId(entrega.idSector);
                txtFechaEntrega.Text = entrega.fechaEntrega.ToString("dd/MM/yyyy");
                txtObservaciones.Text = entrega.Observaciones;
                txtProveedor.Text = p.Id.ToString() + " - " + p.Alias;
                //txtSector.Text = s.id + " - " + s.descripcion;
                CargarPHEntrega();
            }
            catch (Exception ex)
            {


            }
        }

        private void CargarPHEntrega()
        {
            try
            {
                var RecetasDelaEntrega = ControladorEntregas.ObtenerEntregasRecetaByidEntrega(id);
                foreach (var r in RecetasDelaEntrega)
                {
                    CargarRecetaEnPHProducto(r);
                }
                var ProductosDelaEntrega = ControladorEntregas.ObtenerEntregasProductoByidEntrega(id);
                foreach (var p in ProductosDelaEntrega)
                {
                    CargarProductoEnPHProducto(p);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarProductoEnPHProducto(Entregas_Productos p)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = "Producto_" + p.Productos.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = p.Productos.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = p.Productos.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                //celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = p.Cantidad.ToString("N", culture);
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCantidad);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(p.Productos.unidadMedida).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                //celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                //LinkButton btnEditDetalles = new LinkButton();
                //btnEditDetalles.CssClass = "btn btn-xs";
                //btnEditDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnEditDetalles.Text = "<span><i style=\"color: black\" class='fa fa-pencil'></i></span>";
                //btnEditDetalles.Attributes.Add("class", "btn  btn-xs");
                //btnEditDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                //btnEditDetalles.Attributes.Add("onclick", "EditarProd('ContentPlaceHolder1_Producto_" + producto.id.ToString() + "');");
                //celAccion.Controls.Add(btnEditDetalles);

                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Text = "<span><i style=\"color: black\" class='fa fa-trash'></i></span>";
                btnDetalles.Attributes.Add("class", "btn  btn-xs");
                btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Producto_" + p.Productos.id.ToString() + "');");
                celAccion.Controls.Add(btnDetalles);


                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", " text-align: center");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);

                idProductosRecetas.Value += p.Productos.id.ToString() + " ,Producto," + p.Cantidad.ToString("N", culture) + ", ContentPlaceHolder1_Producto_" + p.Productos.id.ToString() + ";";

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CargarRecetaEnPHProducto(Tecnocuisine_API.Entitys.Entregas_Recetas recetas)
        {

            try
            {
                //fila
                TableRow tr = new TableRow();
                tr.ID = "Receta_" + recetas.Recetas.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = recetas.Recetas.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = recetas.Recetas.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                //celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = recetas.Cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCantidad);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(recetas.Recetas.UnidadMedida.Value).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                //celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                //LinkButton btnEditDetalles = new LinkButton();
                //btnEditDetalles.CssClass = "btn btn-xs";
                //btnEditDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnEditDetalles.Text = "<span><i style=\"color: black\" class='fa fa-pencil'></i></span>";
                //btnEditDetalles.Attributes.Add("class", "btn  btn-xs");
                //btnEditDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                //btnEditDetalles.Attributes.Add("onclick", "EditarProd('ContentPlaceHolder1_Producto_" + producto.id.ToString() + "');");
                //celAccion.Controls.Add(btnEditDetalles);

                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Text = "<span><i style=\"color: black\" class='fa fa-trash'></i></span>";
                btnDetalles.Attributes.Add("class", "btn  btn-xs");
                btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Receta_" + recetas.Recetas.id.ToString() + "');");
                celAccion.Controls.Add(btnDetalles);


                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", " text-align: center");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);

                idProductosRecetas.Value += recetas.Recetas.id.ToString() + " ,Producto," + recetas.Cantidad.ToString("N", culture) + ", ContentPlaceHolder1_Producto_" + recetas.Recetas.id.ToString() + ";";

            }
            catch (Exception ex)
            {

            }

        }

        private void CargarSectores()
        {
            try
            {
                ControladorSectorProductivo cSectorProductivo = new ControladorSectorProductivo();
                this.ddlDepositos.DataSource = cSectorProductivo.ObtenerTodosSectorProductivo();
                this.ddlDepositos.DataValueField = "id";
                this.ddlDepositos.DataTextField = "descripcion";
                this.ddlDepositos.DataBind();
                ddlDepositos.Items.Insert(0, new ListItem("", "-1"));
            }
            catch (Exception)
            {

            }
        }

        private void ObtenerRecetas()
        {
            try
            {
                var Recetas = ControladorReceta.ObtenerTodosRecetasComprables();

                if (Recetas.Count > 0)
                {
                    CargarRecetasOptions(Recetas);
                    foreach (var item in Recetas)
                    {

                        CargarRecetasPHModal(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarRecetasPHModal(Tecnocuisine_API.Entitys.Recetas Receta)
        {

            try
            {

                //fila


                TableRow tr = new TableRow();
                tr.ID = "Recetas_" + Receta.id.ToString() + "_" + Receta.UnidadMedida;

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Receta.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = Receta.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCosto = new TableCell();
                celCosto.Text = Receta.Costo.ToString().Replace(',', '.');
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(Receta.UnidadMedida.Value).descripcion;

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                //LinkButton btnDetalles = new LinkButton();
                //btnDetalles.CssClass = "btn btn-primary btn-xs";
                HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "agregarReceta(this.id,'" + Receta.Costo.ToString().Replace(',', '.') + "'); return false;");
                btnDetalles.Attributes.Add("type", "checkbox");
                btnDetalles.ID = "btnSelecProd_" + Receta.id + "_" + Receta.descripcion + "_" + UnidadMedida;
                //btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phRecetasModal.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        private void CargarRecetasOptions(List<Tecnocuisine_API.Entitys.Recetas> recetas)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var rec in recetas)
                {
                    string UnidadMedida = "";
                    if (rec.UnidadMedida != null)
                    {
                        if (rec.UnidadMedida.Value == -1)
                            rec.UnidadMedida = 1;

                        UnidadMedida = cu.ObtenerUnidadId(rec.UnidadMedida.Value).descripcion;
                        builder.Append(String.Format("<option value='{0}' id='c_r_" + rec.id + "_" + rec.descripcion + "_" + UnidadMedida + "_" + rec.Costo.ToString().Replace(',', '.') + "'>", rec.id + " - " + rec.descripcion));
                    }
                    else
                    {

                    }
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));

                ListaNombreProd.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void ObtenerProveedores()
        {
            try
            {
                var listaProv = ControladorProveedores.ObtenerProveedoresAll();
                if (listaProv.Count > 0)
                {
                    CargarProveedoresOptions(listaProv);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarProveedoresOptions(List<Tecnocuisine_API.Entitys.Proveedores> listaProv)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var prov in listaProv)
                {

                    builder.Append(String.Format("<option value='{0}' id='Prov_" + prov.Id + "'>", prov.Id + " - " + prov.RazonSocial));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListaNombreProveedores.InnerHtml += builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        private void ObtenerSectores()
        {
            try
            {
                var ListSectores = ControladorSector.ObtenerTodosSectorProductivo();
                if (ListSectores.Count > 0)
                    cargarSectoresOptions(ListSectores);
            }
            catch (Exception ex)
            {


            }
        }

        private void cargarSectoresOptions(List<Tecnocuisine_API.Entitys.SectorProductivo> sectorProductivoslista)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var sector in sectorProductivoslista)
                {

                    builder.Append(String.Format("<option value='{0}' id='sector_" + sector.id + "'>", sector.id + " - " + sector.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                //ListaNombreSectores.InnerHtml += builder.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerProductos()
        {
            try
            {
                var productos = controladorProducto.ObtenerTodosProductos();

                if (productos.Count > 0)
                {
                    CargarProductosOptions(productos);

                    foreach (var item in productos)
                    {
                        CargarProductosPH(item);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarProductosOptions(List<Tecnocuisine_API.Entitys.Productos> productos)
        {
            try
            {
                ControladorUnidad cu = new ControladorUnidad();
                var builder = new System.Text.StringBuilder();

                foreach (var prod in productos)
                {
                    string UnidadMedida = "";
                    UnidadMedida = cu.ObtenerUnidadId(prod.unidadMedida).descripcion;
                    builder.Append(String.Format("<option value='{0}' id='c_p_" + prod.id + "_" + prod.descripcion + "_" + UnidadMedida + "_" + prod.costo.ToString().Replace(',', '.') + "_" + prod.Alicuotas_IVA.porcentaje + "'>", prod.id + " - " + prod.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListaNombreProd.InnerHtml += builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        public void CargarProductosPH(Tecnocuisine_API.Entitys.Productos producto)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "Productos_" + producto.id.ToString() + "_" + producto.unidadMedida.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = producto.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = producto.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCosto = new TableCell();
                celCosto.Text = producto.costo.ToString().Replace(',', '.');
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(producto.unidadMedida).descripcion;

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                //LinkButton btnDetalles = new LinkButton();
                //btnDetalles.CssClass = "btn btn-primary btn-xs";
                HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "agregarProducto(this.id,'" + producto.costo.ToString().Replace(',', '.') + "'); return false;");
                btnDetalles.Attributes.Add("type", "checkbox");
                btnDetalles.ID = "btnSelecProd_" + producto.id + "_" + producto.descripcion + "_" + UnidadMedida;
                //btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phProductosAgregar.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }

        private Tecnocuisine_API.Entitys.Entregas CrearEntrega()
        {
            Tecnocuisine_API.Entitys.Entregas newEntrega = new Tecnocuisine_API.Entitys.Entregas();
            newEntrega.idProveedor = Convert.ToInt32(txtProveedor.Text.Split('-')[0]);
            //newEntrega.idSector = Convert.ToInt32(txtSector.Text.Split('-')[0]);
            newEntrega.Observaciones = txtObservaciones.Text;
            newEntrega.fechaEntrega = DateTime.ParseExact(txtFechaEntrega.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            newEntrega.fechaRealizada = DateTime.Now;
            newEntrega.Estado = 1;
            var EntregasList = ControladorEntregas.ObtenerEntregasAll();
            string fac1 = "000000";

            if (EntregasList.Count == 0)
            {
                fac1 = "000001";
            }
            else
            {
                string codigo = (EntregasList.Count + 1).ToString();
                fac1 = GenerarCodigoPedido(codigo);

            }
            newEntrega.CodigoEntrega = fac1;

            return newEntrega;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    string ingredientes = idProductosRecetas.Value;
                    ingredientes = ingredientes.Replace('.', ',');

                    Tecnocuisine_API.Entitys.Entregas newEntrega = CrearEntrega();

                    if (accion == 2)
                    {
                        newEntrega.id = id;
                        int i = ControladorEntregas.EditarEntregas(newEntrega);

                        if (i > 0)
                        {
                            ControladorEntregas.EliminarIngredientes(id);
                            string[] items = ingredientes.Split(';');
                            int idProducto = 0;
                            foreach (var pr in items)
                            {
                                if (pr != "")
                                {
                                    string[] producto = pr.Split('%');
                                    string id_Marca = producto[2];
                                    string id_Producto = producto[0];
                                    string Tipo = producto[1];
                                    string Cantidad = producto[3];
                                    string Presentaciones = producto[5];
                                    string LoteEnviado = string.IsNullOrEmpty(producto[6].Trim()) ? null : producto[6].Trim();
                                    string fechaVencimientoItem = string.IsNullOrEmpty(producto[7].Trim()) ? null : producto[7].Trim();

                                    if (producto[1] == "Producto")
                                    {
                                        Entregas_Productos productoNuevo = new Entregas_Productos();
                                        productoNuevo.idEntregas = i;
                                        productoNuevo.idProductos = Convert.ToInt32(producto[0]);
                                        productoNuevo.Lote = LoteEnviado;
                                        productoNuevo.Stock = null;
                                        productoNuevo.CodigoEntrega = "";
                                        //productoNuevo.idSector = Convert.ToInt32(txtSector.Text.Split('-')[0]);
                                        productoNuevo.idPresentacion = Convert.ToInt32(Presentaciones);
                                        productoNuevo.FechaVencimiento = fechaVencimientoItem;
                                        productoNuevo.idMarca = Convert.ToInt32(id_Marca);
                                        productoNuevo.Cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                        ControladorEntregas.AgregarEntrega_Producto(productoNuevo, LoteEnviado, fechaVencimientoItem, Convert.ToInt32(producto[4]));
                                    }
                                    else
                                    {
                                        Entregas_Recetas RecetaNuevo = new Entregas_Recetas();
                                        RecetaNuevo.idEntregas = i;
                                        RecetaNuevo.idRecetas = Convert.ToInt32(producto[0]);
                                        idProducto = RecetaNuevo.idRecetas.Value;
                                        RecetaNuevo.Cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                        //ControladorEntregas.AgregarEntrega_Receta(RecetaNuevo, newEntrega.idSector, LoteEnviado, fechaVencimientoItem, Convert.ToInt32(producto[4]));
                                    }
                                }
                            }
                            Response.Redirect("EntregasE.aspx?m=1");
                        }
                        else
                        {
                            m.ShowToastr(Page, "No se pudo agregar la entrega!", "Error", "warning");
                        }
                    }
                    else
                    {
                        int i = ControladorEntregas.AgregarEntrega(newEntrega);

                        if (i > 0)
                        {
                            string[] items = ingredientes.Split(';');

                            foreach (var pr in items)
                            {
                                //AgregarNuevoProductoVenta(pr, Convert.ToInt32(txtSector.Text.Split('-')[0]), txtFechaVencimiento.Text);
                                if (pr != "")
                                {
                                    string[] producto = pr.Split('%');

                                    int? id_Marca;
                                    if (string.IsNullOrEmpty(producto[2]))
                                        id_Marca = null;
                                    else
                                        id_Marca = Convert.ToInt32(producto[2]);


                                    int? id_presentacion;
                                    if (string.IsNullOrEmpty(producto[5]))
                                        id_presentacion = null;
                                    else
                                        id_presentacion = Convert.ToInt32(producto[5]);


                                    string id_Producto = producto[0].Trim();
                                    string Cantidad = producto[3];

                                    string LoteEnviado = string.IsNullOrEmpty(producto[6].Trim()) ? null : producto[6].Trim();
                                    string fechaVencimientoItem = string.IsNullOrEmpty(producto[7].Trim()) ? null : producto[7].Trim();
                                    var precio = Convert.ToDecimal(producto[8].Replace("$", ""));
                                    int idSector = Convert.ToInt32(producto[9]);

                                    if (producto[1] == "Producto")
                                    {
                                        Entregas_Productos productoNuevo = new Entregas_Productos();
                                        productoNuevo.idEntregas = i;
                                        productoNuevo.idProductos = Convert.ToInt32(producto[0]);
                                        productoNuevo.Lote = LoteEnviado;
                                        productoNuevo.Stock = null;
                                        productoNuevo.CodigoEntrega = newEntrega.CodigoEntrega;
                                        productoNuevo.idSector = idSector;
                                        productoNuevo.idPresentacion = id_presentacion;
                                        productoNuevo.FechaVencimiento = fechaVencimientoItem;
                                        productoNuevo.idMarca = id_Marca;
                                        productoNuevo.Cantidad = Convert.ToDecimal(Cantidad);
                                        productoNuevo.Precio = precio;
                                        ControladorEntregas.AgregarEntrega_Producto(productoNuevo, LoteEnviado, fechaVencimientoItem, id_presentacion);
                                        controladorProducto.ActualizarCosto((int)productoNuevo.idProductos);
                                    }
                                    else
                                    {
                                        Entregas_Recetas RecetaNuevo = new Entregas_Recetas();
                                        RecetaNuevo.idEntregas = i;
                                        RecetaNuevo.idRecetas = Convert.ToInt32(id_Producto);
                                        RecetaNuevo.Cantidad = Convert.ToDecimal(Cantidad);
                                        RecetaNuevo.Precio = precio;
                                        RecetaNuevo.idSector = idSector;
                                        RecetaNuevo.idPresentacion = id_presentacion;
                                        RecetaNuevo.FechaVencimiento = fechaVencimientoItem;
                                        RecetaNuevo.idMarca = id_Marca;
                                        ControladorEntregas.AgregarEntrega_Receta(RecetaNuevo, LoteEnviado, fechaVencimientoItem, id_presentacion);
                                        ControladorReceta.ActualizarCosto((int)RecetaNuevo.idRecetas);
                                    }
                                }
                            }

                            m.ShowToastr(Page, "Agregado con exito!", "Exito", "success");
                            LimpiarCampos();
                        }
                        else
                        {
                            m.ShowToastr(Page, "No se pudo agregar la entrega!", "Error", "error");
                        }
                    }
                }
                else
                {
                    m.ShowToastr(Page, "Algun dato no se ingreso correctamente", "Error", "warning");
                }

                //txtNroFactura.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //m.ShowToastr(Page, ex.Message, "Error", "Error");
            }
        }

        public string GenerarCodigoPedido(string value)
        {
            var value2 = Convert.ToInt64(value);
            int length = value2.ToString().Length;
            int calc = 6 - length;
            int decimalLength = value2.ToString("D").Length + calc;
            var result2 = value2.ToString("D" + decimalLength.ToString());
            return result2;
        }

        public void AgregarNuevoProductoVenta(string prod, int idsector, string fecha)
        {
            Tecnocuisine_API.Entitys.ProductoVentas newProductoVentas = new Tecnocuisine_API.Entitys.ProductoVentas();
            ControladorProductoVenta controladorProductoVenta = new ControladorProductoVenta();
            newProductoVentas.idSector = idsector;
            string[] producto = prod.Split(',');
            string id_Marca = producto[2];
            string id_Producto = producto[0];
            string Tipo = producto[1];
            string Cantidad = producto[3];
            string Presentaciones = producto[5];
            string LoteEnviado = producto[6];
            newProductoVentas.idMarca = Convert.ToInt32(id_Marca);
            newProductoVentas.idProducto = Convert.ToInt32(id_Producto);
            newProductoVentas.idPresentacion = Convert.ToInt32(Presentaciones);
            newProductoVentas.Stock = Convert.ToInt32(Cantidad);
            newProductoVentas.Lote = LoteEnviado;
            newProductoVentas.FechaVencimiento = Convert.ToDateTime(fecha);
            controladorProductoVenta.AgregarProductosVentas(newProductoVentas);
        }

        private void LimpiarCampos()
        {
            try
            {
                txtCantidad.Text = "";
                txtDescripcionProductos.Text = "";
                txtProveedor.Text = "";
                //txtSector.Text = "";
                //txtUnidadMed.Text = "";
                txtObservaciones.Text = "";
                idProductosRecetas.Value = "";
                //txtNroFactura.Text = "";
            }
            catch (Exception ex)
            {
            }
        }

        [WebMethod]
        public static List<PresentacionClass> GetPresentaciones(int idProd, int tipo)
        {
            try
            {
                if (tipo == 1)
                {

                    ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                    var lista = controladorPresentacion.ObtenerPresentacionByIdProducto(idProd);
                    List<Tecnocuisine_API.Entitys.Presentaciones> ListaDDL = new List<Tecnocuisine_API.Entitys.Presentaciones>();
                    foreach (var pres in lista)
                    {
                        ListaDDL.Add(pres.Presentaciones);
                    }
                    List<PresentacionClass> listaFInal = new List<PresentacionClass>();
                    foreach (var pres in ListaDDL)
                    {
                        PresentacionClass pc = new PresentacionClass();
                        pc.id = pres.id;
                        pc.descripcion = pres.descripcion;

                        listaFInal.Add(pc);
                    }

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //javaScript.MaxJsonLength = 5000000;
                    //string resultadoJSON = javaScript.Serialize("Exito al editar el Producto.");
                    //return resultadoJSON;
                    return listaFInal.ToList();
                }
                else if (tipo == 2)
                {
                    ControladorReceta controladorReceta = new ControladorReceta();
                    var lista = controladorReceta.ObtenerPresentacionByIdReceta(idProd);
                    List<Tecnocuisine_API.Entitys.Presentaciones> ListaDDL = new List<Tecnocuisine_API.Entitys.Presentaciones>();
                    foreach (var pres in lista)
                    {
                        ListaDDL.Add(pres.Presentaciones);
                    }
                    List<PresentacionClass> listaFInal = new List<PresentacionClass>();
                    foreach (var pres in ListaDDL)
                    {
                        PresentacionClass pc = new PresentacionClass();
                        pc.id = pres.id;
                        pc.descripcion = pres.descripcion;

                        listaFInal.Add(pc);
                    }

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //javaScript.MaxJsonLength = 5000000;
                    //string resultadoJSON = javaScript.Serialize("Exito al editar el Producto.");
                    //return resultadoJSON;
                    return listaFInal.ToList();
                }
                return new List<PresentacionClass>();
            }
            catch (Exception)
            {
                return new List<PresentacionClass>();

            }
        }

        [WebMethod]
        public static List<PresentacionClass> GetRubro(int idProd, int tipo)
        {
            try
            {
                List<PresentacionClass> listaFInal = new List<PresentacionClass>();
                PresentacionClass presentacionClass = new PresentacionClass();
                ControladorRubros cRubro = new ControladorRubros();
                Tecnocuisine_API.Entitys.Rubros rubro;

                // Es producto
                if (tipo == 1)
                {
                    ControladorProducto cProducto = new ControladorProducto();
                    Tecnocuisine_API.Entitys.Productos producto = cProducto.ObtenerProductoId(idProd);
                    rubro = cRubro.ObtenerRubrosId((int)producto.idRubro);
                    presentacionClass.id = rubro.id;
                    presentacionClass.descripcion = rubro.descripcion;
                }
                // Es receta
                else
                {
                    ControladorReceta cReceta = new ControladorReceta();
                    Tecnocuisine_API.Entitys.Recetas receta = cReceta.ObtenerRecetaId(idProd);
                    rubro = cRubro.ObtenerRubrosId((int)receta.idRubro);
                    presentacionClass.id = rubro.id;
                    presentacionClass.descripcion = rubro.descripcion;
                }

                listaFInal.Add(presentacionClass);

                return listaFInal.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [WebMethod]
        public static int GetIdSectorByIdProd(int idProd, int tipo)
        {
            try
            {
                ControladorSectorProductivo cSector = new ControladorSectorProductivo();

                // Es producto
                if (tipo == 1)
                {
                    ControladorProducto cProducto = new ControladorProducto();
                    Tecnocuisine_API.Entitys.Productos producto = cProducto.ObtenerProductoId(idProd);
                    return producto.idSectorProductivo ?? -1;
                }

                // Es receta
                ControladorReceta cReceta = new ControladorReceta();
                Tecnocuisine_API.Entitys.Recetas receta = cReceta.ObtenerRecetaId(idProd);

                if (receta.SectorP_Recetas == null)
                    return -1;

                // TODO: sacar for
                foreach (var sector in receta.SectorP_Recetas)
                    return sector.idSectorP ?? -1;

                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //OBTENER TODAS LAS MARCAS

        [WebMethod]
        public static List<PresentacionClass> GetMarca(int idItem, int tipo)
        {
            try
            {
                ControladorMarca controladorMarca = new ControladorMarca();
                List<PresentacionClass> listaFInal = new List<PresentacionClass>();

                // Es Producto
                if (tipo == 1)
                {
                    var marcas = controladorMarca.ObtenerMarcaPorIDProducto(idItem);

                    if (marcas.Count > 0)
                    {
                        foreach (var item in marcas)
                        {
                            PresentacionClass pc = new PresentacionClass();
                            pc.id = item.Articulos_Marcas.id;
                            pc.descripcion = item.Articulos_Marcas.descripcion;
                            listaFInal.Add(pc);
                        }
                    }

                    return listaFInal;
                }

                // Es Receta
                else
                {
                    var marcas = controladorMarca.ObtenerMarcasPorIDReceta(idItem);

                    if (marcas.Count > 0)
                    {
                        foreach (var item in marcas)
                        {
                            PresentacionClass pc = new PresentacionClass();
                            pc.id = item.Articulos_Marcas.id;
                            pc.descripcion = item.Articulos_Marcas.descripcion;
                            listaFInal.Add(pc);
                        }
                    }

                    return listaFInal;
                }

            }
            catch (Exception)
            {
                return new List<PresentacionClass>();
            }
        }
        public class PresentacionClass
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}