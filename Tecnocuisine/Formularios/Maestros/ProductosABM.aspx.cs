using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static Tecnocuisine.Formularios.Compras.Entregas;
using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using System.Web.Services.Description;
using System.Text.RegularExpressions;


namespace Tecnocuisine.Formularios.Maestros
{
    public partial class ProductosABM : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorStock controladorStock = new ControladorStock();
        ControladorCategoria cc = new ControladorCategoria();
        ControladorReceta cr = new ControladorReceta();


        Gestion_Api.Controladores.controladorArticulo controladorArticulo = new Gestion_Api.Controladores.controladorArticulo();
        Gestion_Api.Controladores.ControladorArticulosEntity contArtEnt = new Gestion_Api.Controladores.ControladorArticulosEntity();
        int accion;
        int idProducto;
        int Mensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            //VerificarLogin();
            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.idProducto = Convert.ToInt32(Request.QueryString["i"]);

            if (!IsPostBack)
            {


                CargarUnidadesMedida();
                CargarAlicuotasIVA();
                CargarSectores();
                CargarRubros();

                //CargarListaCategoriasSoloHijas();
                //cargarNestedListAtributos();

                if (accion == 2)
                {
                    CargarProducto();
                }
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
            ObtenerGruposArticulos();
            
            //ObtenerSubGruposArticulos(Convert.ToInt32(ListGrupo.SelectedValue));
            ObtenerPresentaciones();
            ObtenerMarca();
        }


        public void CargarRubros()
        {
            try
            {

                ControladorRubros cr = new ControladorRubros();

                var listRubros = cr.ObtenerTodosRubros();
                CargarOptionsRubros(listRubros);




            }
            catch (Exception ex)
            {

            }
        }
        private void CargarOptionsRubros(List<Tecnocuisine_API.Entitys.Rubros> marcas)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var press in marcas)
                {

                    builder.Append(String.Format("<option value='{0}' id='Rubro_" + press.id + " _ " + press.descripcion + "'>", press.id + " - " + press.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListOptionRubro.InnerHtml = builder.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ObtenerMarca()
        {
            try
            {
                phMarcas.Controls.Clear();
                ControladorMarca controladorMarca = new ControladorMarca();
                var marcas = controladorMarca.ObtenerTodasMarcas();
                if (marcas.Count > 0)
                {
                    CargarOptionsListMarcas(marcas);
                    foreach (var item in marcas)
                    {
                        CargarMarcasPH(item);

                    }
                }
                else
                {
                    new List<PresentacionClass>();
                }
            }
            catch (Exception)
            {
                new List<PresentacionClass>();

            }
        }
        private void CargarOptionsListMarcas(List<Tecnocuisine_API.Entitys.Articulos_Marcas> marcas)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var press in marcas)
                {

                    builder.Append(String.Format("<option value='{0}' id='Marca_" + press.id + " _ " + press.descripcion + "'>", press.id + " - " + press.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListOptionMarcas.InnerHtml = builder.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void CargarMarcasPH(Tecnocuisine_API.Entitys.Articulos_Marcas marca)
        {
            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "marca_" + marca.id.ToString();

                //Celdas

                TableCell celNombre = new TableCell();
                celNombre.Text = marca.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celNombre);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                cbxAgregar.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                cbxAgregar.Attributes.Add("type", "checkbox");
                //cbxAgregar.Attributes.Add("value", "1");
                cbxAgregar.ID = "btnSelecPres_" + marca.id + " - " + marca.descripcion;
                celAccion.Controls.Add(cbxAgregar);

                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phMarcas.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        public void CargarProducto()
        {
            try
            {
                var producto = controladorProducto.ObtenerProductoId(this.idProducto);
                if (producto != null)
                {
                    //hiddenEditar.Value = producto.id.ToString();
                    ProdDescripcion.Text = producto.descripcion;
                    txtCosto.Text = producto.costo.ToString().Replace(',', '.');
                    //idCategoria.Value = producto.Categorias.id.ToString();
                    string descripcionAtributos = "";
                    if (producto.ProductoFinal == true)
                    {

                        cbxGestion.Checked = true;
                    }
                    else
                    {
                        cbxGestion.Checked = false;
                    }
                    if (producto.Productos_Atributo != null)
                    {
                        ControladorAtributo controladorAtributo = new ControladorAtributo();
                        foreach (Productos_Atributo item in producto.Productos_Atributo)
                        {

                            Tecnocuisine_API.Entitys.Atributos atributo = controladorAtributo.ObtenerAtributoById(item.atributo);
                            if (descripcionAtributos == "")
                            {
                                descripcionAtributos = atributo.id + " - " + atributo.descripcion;
                                //idAtributo.Value = atributo.id.ToString();
                            }

                            else
                            {
                                descripcionAtributos += " , " + atributo.id + " - " + atributo.descripcion;
                                //idAtributo.Value += "," + atributo.id.ToString();
                            }
                        }
                    }
                    string descripcionPresentaciones = "";
                    if (producto.Productos_Presentacion != null)
                    {
                        ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                        foreach (Productos_Presentacion item in producto.Productos_Presentacion)
                        {

                            Tecnocuisine_API.Entitys.Presentaciones presentacion = controladorPresentacion.ObtenerPresentacionId(item.idPresentacion);
                            if (descripcionPresentaciones == "")
                            {
                                descripcionPresentaciones = presentacion.id + " - " + presentacion.descripcion;
                                idPresentacion.Value = presentacion.id.ToString();
                            }

                            else
                            {
                                descripcionPresentaciones += " , " + presentacion.id + " - " + presentacion.descripcion;
                                idPresentacion.Value += "," + presentacion.id.ToString();
                            }
                            CargarPresentacionesFinalPH(presentacion);

                        }
                    }
                    string descripcionMarcas = "";
                    if (producto.Marca_Productos != null)
                    {
                        ControladorMarca controladorMarca = new ControladorMarca();
                        foreach (Marca_Productos item in producto.Marca_Productos)
                        {
                            Tecnocuisine_API.Entitys.Articulos_Marcas marcas = controladorMarca.ObtenerMarcaId((int)item.id_marca);
                            if (marcas != null)
                            {

                                if (descripcionMarcas == "")
                                {
                                    descripcionMarcas = marcas.id + " - " + marcas.descripcion;
                                    idMarca.Value = marcas.id.ToString();
                                }

                                else
                                {
                                    descripcionMarcas += " , " + marcas.id + " - " + marcas.descripcion;
                                    idMarca.Value += "," + marcas.id.ToString();
                                }
                                CargarMarcaFinalPH(marcas);

                            }
                        }
                    }



                    txtDescripcionPresentacion.Text = "";
                    hfPresentaciones.Value = descripcionPresentaciones;
                    txtDescripcionMarca.Text = "";
                    hfMarcas.Value = descripcionMarcas;
                    //txtDescripcionAtributo.Text = descripcionAtributos;
                    //txtDescripcionCategoria.Text = producto.Categorias.id + " - " + producto.Categorias.descripcion;
                    ListAlicuota.SelectedValue = producto.alicuota.ToString();
                    ListUnidadMedida.SelectedValue = producto.unidadMedida.ToString();

                    ddlSectores.SelectedValue = producto.idSectorProductivo.ToString();

                    if (producto.idRubro != null)
                    {
                        var rubro = new ControladorRubros().ObtenerRubrosId((int)producto.idRubro);
                        ListRubro.Text = producto.idRubro + " - " + rubro.descripcion;                       
                    }
                    
                    //btnAtributos.Attributes.Remove("disabled");

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarListaCategoriasSoloHijas()
        {
            try
            {

                var listaCategoria = cc.obtenerTodasCategoriasHijas();
                CargarCategoriasOptions(listaCategoria);
                foreach (var categoria in listaCategoria)
                {
                    CargarCategoriaPH(categoria);
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void CargarCategoriasOptions(List<Tecnocuisine_API.Entitys.Categorias> categorias)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var cat in categorias)
                {

                    builder.Append(String.Format("<option value='{0}' id='c_" + cat.id + "_" + cat.descripcion + "'>", cat.id + " - " + cat.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                //listaCategoria.InnerHtml = builder.ToString();

            }
            catch (Exception ex)
            {
            }
        }
        private void CargarCategoriaPH(Tecnocuisine_API.Entitys.Categorias categoria)
        {
            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = categoria.id.ToString();
                string familia = cc.BuscarFamiliaCompleta(categoria.id);
                familia = familia.Remove(familia.LastIndexOf(" > ")).TrimEnd();
                //Celdas


                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = categoria.descripcion;
                celDescripcion.Font.Bold = true;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celDescripcion);

                TableCell celFamilia = new TableCell();
                celFamilia.Text = familia;
                celFamilia.VerticalAlign = VerticalAlign.Middle;
                celFamilia.HorizontalAlign = HorizontalAlign.Left;
                celFamilia.Width = Unit.Percentage(5);
                celFamilia.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celFamilia);
                //agrego fila a tabla
                TableCell celAccion = new TableCell();

                //LinkButton btnDetalles = new LinkButton();
                //btnDetalles.CssClass = "btn btn-primary btn-xs";
                //HtmlGenericControl btnVerFamilia = new HtmlGenericControl("a");
                //btnVerFamilia.Attributes.Add( "class", "btn btn-xs");
                //btnVerFamilia.Style.Add("background-color", "transparent");
                //btnVerFamilia.Style.Add("margin-right", "10px");
                ////btnVerFamilia.Attributes.Add("title data-original-title", familia);
                ////btnVerFamilia.Attributes.Add("data-toggle", "modal");
                ////btnVerFamilia.Attributes.Add("title data-original-title", "Editar");
                //btnVerFamilia.ID = "btnSelec_" + categoria.id + "_";
                //btnVerFamilia.InnerHtml = "<span><i style='color:black;' class='fa fa-search'></i></span>";
                //btnVerFamilia.Attributes.Add("OnClick", "MostrarModalFamilia(\'" + familia + "\')");
                //celAccion.Controls.Add(btnVerFamilia);

                HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "agregarCategoria(this.id); return false;");
                btnDetalles.Attributes.Add("type", "checkbox");
                btnDetalles.ID = "btnSelecProd_" + categoria.id + "_" + categoria.descripcion;
                //btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                celAccion.Controls.Add(btnDetalles);

                //agrego fila a tabla





                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                PHCategorias.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }
        }
        private void VerificarLogin()
        {
            try
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("../../Account/Login.aspx");
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

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.hiddenEditar.Value != "")
        //        {
        //            EditarProducto();
        //        }
        //        else
        //        {
        //            GuardarProducto();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void EditarProducto()
        {
            try
            {
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                producto.id = idProducto;
                //producto.descripcion = txtDescripcionProducto.Text;
                producto.categoria = 1;
                producto.costo = Convert.ToDecimal(txtCosto.Text);
                producto.unidadMedida = Convert.ToInt32(ListUnidadMedida.SelectedValue);
                producto.alicuota = Convert.ToInt32(ListAlicuota.SelectedValue);
                producto.estado = 1;
                //producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.EditarProducto(producto);

                if (resultado > 0)
                {

                    //string[] atributos = idAtributo.Value.Split(',');
                    //controladorProducto.EliminarProductoAtributo(producto.id);

                    //foreach (string s in atributos)
                    //{
                    //    if (s != "")
                    //    {
                    //        Productos_Atributo atributo = new Productos_Atributo();
                    //        atributo.producto = producto.id;
                    //        atributo.atributo = Convert.ToInt32(s.Split('-')[0]);
                    //        controladorProducto.AgregarProductoAtributo(atributo);
                    //    }

                    //}


                    string[] presentaciones = idPresentacion.Value.Split(',');
                    controladorProducto.EliminarProductoPresentacion(producto.id);

                    foreach (string s in presentaciones)
                    {
                        if (s != "")
                        {
                            Productos_Presentacion atributo = new Productos_Presentacion();
                            atributo.idProducto = producto.id;
                            atributo.idPresentacion = Convert.ToInt32(s.Split('-')[0]);
                            controladorProducto.AgregarProductoPresentacion(atributo);
                        }

                    }
                    Response.Redirect("Productos.aspx?m=2");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la producto", "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string GuardarRubro(string descripcion)
        {
            try
            {
                ControladorRubros controladorRubros = new ControladorRubros();
                Tecnocuisine_API.Entitys.Rubros rubro = new Tecnocuisine_API.Entitys.Rubros();

                rubro.descripcion = descripcion;
                rubro.estado = true;

                int resultado = controladorRubros.AgregarRubros(rubro);

                if (resultado > 0)
                {
                    return "Rubro Agregado con Exito ;" + resultado;
                }
                else
                {
                    return "Error al agregar el Rubro";
                }
            }
            catch (Exception ex)
            {
                return "Error al agregar el Rubro";
            }
        }

        [WebMethod]
        public static string GuardarPresentacion(string descripcion, string Cantidad)
        {
            try
            {
                Tecnocuisine_API.Entitys.Presentaciones presentacion = new Tecnocuisine_API.Entitys.Presentaciones();
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                presentacion.descripcion = descripcion;
                presentacion.cantidad = Convert.ToDecimal(Cantidad);
                presentacion.estado = 1;

                int resultado = controladorPresentacion.AgregarPresentacion(presentacion);

                if (resultado > 0)
                {
                    return resultado + "-" + descripcion;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        [WebMethod]
        public static string CrearMarca(string descripcion)
        {
            try
            {
                Tecnocuisine_API.Entitys.Articulos_Marcas marca = new Tecnocuisine_API.Entitys.Articulos_Marcas();
                ControladorMarca controladorMarca = new ControladorMarca();

                marca.descripcion = descripcion;
                marca.estado = 1;

                int resultado = controladorMarca.AgregarMarca(marca);

                if (resultado > 0)
                {
                    return resultado + " - " + descripcion;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        [WebMethod]
        public static void GuardarProducto(string descripcion, string Categoria, string Atributos, string Costo, string IVA, string Unidad, string Presentacion, string Marca, bool cbxGestion, string Rubro, string img, string sectorId)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorStock controladorStock = new ControladorStock();
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();

                producto.descripcion = descripcion;
                producto.ProductoFinal = cbxGestion;

                if (Rubro == "")
                    producto.idRubro = 1;
                else
                    producto.idRubro = Convert.ToInt32(Rubro);

                if (Categoria.Trim() == "")
                    producto.categoria = 1;
                else
                    producto.categoria = Convert.ToInt32(Categoria.Split('-')[0].Trim());

                List<Productos_Atributo> AtributosProd = new List<Productos_Atributo>();

                if (Atributos.Trim() != "")
                {
                    string[] vecAtributos = Atributos.Split(',');

                    foreach (string i in vecAtributos)
                    {
                        if (!string.IsNullOrEmpty(i))
                        {
                            Productos_Atributo pa = new Productos_Atributo();
                            pa.atributo = Convert.ToInt32(i.Split('-')[0].Trim());

                            AtributosProd.Add(pa);
                        }
                    }
                }
                else
                {
                    Productos_Atributo pa = new Productos_Atributo();
                    pa.atributo = 1;
                    AtributosProd.Add(pa);
                }

                producto.Productos_Atributo = AtributosProd;
                producto.costo = decimal.Parse(Costo, CultureInfo.InvariantCulture);
                producto.unidadMedida = Convert.ToInt32(Unidad);
                producto.alicuota = Convert.ToInt32(IVA);
                producto.estado = 1;

                List<Productos_Presentacion> PresentacionProd = new List<Productos_Presentacion>();
                if (Presentacion.Trim() == "")
                {
                    Productos_Presentacion pp = new Productos_Presentacion();
                    pp.idPresentacion = 1;

                    PresentacionProd.Add(pp);
                }
                else
                {

                    string[] vecPresentaciones = Presentacion.Split(',');

                    foreach (string i in vecPresentaciones)
                    {
                        if (!string.IsNullOrEmpty(i) && i.Trim().Count() > 0)
                        {
                            Productos_Presentacion pp = new Productos_Presentacion();
                            pp.idPresentacion = Convert.ToInt32(i.Split('-')[0].Trim());

                            PresentacionProd.Add(pp);
                        }
                    }
                }
                producto.Productos_Presentacion = PresentacionProd;
                List<Marca_Productos> marca_Productos = new List<Marca_Productos>();
                if (Marca.Trim() == "")
                {
                    Marca_Productos pp = new Marca_Productos();
                    pp.id_marca = 1;
                    pp.estado = 1;
                    pp.FechaCreacion = DateTime.Now;

                    marca_Productos.Add(pp);

                }
                else
                {
                    string[] verMarcas = Marca.Split(',');

                    foreach (string i in verMarcas)
                    {
                        if (!string.IsNullOrEmpty(i) && i.Trim().Count() > 0)
                        {
                            Marca_Productos pp = new Marca_Productos();
                            pp.id_marca = Convert.ToInt32(i.Split('-')[0].Trim());
                            pp.estado = 1;
                            pp.FechaCreacion = DateTime.Now;

                            marca_Productos.Add(pp);
                        }
                    }
                }


                producto.Marca_Productos = marca_Productos;


                //Set Sector
                if(sectorId == "-1")
                    producto.idSectorProductivo = null;
                else
                    producto.idSectorProductivo = int.Parse(sectorId);


                //producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorProducto.AgregarProducto(producto);

                if (resultado > 0)
                {
                    if (cbxGestion)
                    {
                        //GuardarArticuloGestion(producto);
                    }
                    else
                    {
                        controladorStock.AgregarProductoStock(resultado);

                    }
                    if (!string.IsNullOrEmpty(img))
                    {

                        String path = System.Web.HttpContext.Current.Server.MapPath("../../images/Productos/" + resultado + "/");
                        //byte[] bytes = Convert.FromBase64String(img);
                        //System.Drawing.Image image;
                        //using (MemoryStream ms = new MemoryStream(bytes))
                        //{
                        //    image = System.Drawing.Image.FromStream(ms); //Image.FromStream(ms);
                        //}

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        //lo subo
                        //Page page = HttpContext.Current.Handler as Page;
                        //FileUpload inputImagen = (FileUpload)page.FindControl("inputImage2");
                        //TextBox txt = (TextBox)page.FindControl("txtDescripcionCategoria");
                        //HtmlInputText txt2 = (HtmlInputText)page.FindControl("ProdDescripcion");
                        //inputImagen.PostedFile.SaveAs(path + inputImagen.FileName);

                        //FileUpload upload = new FileUpload();
                        //HttpContext context = new HttpContext();
                        //HttpPostedFile file = context.Request.Files[s];
                        //objProperty.PropertyImage = filepath;
                        //upload.SaveAs(img);
                        //upload.PostedFile.SaveAs(path + upload.FileName);
                    }

                }

            }
            catch (Exception ex)
            {

            }

        }
        [WebMethod]
        public static string EditarProducto(string descripcion, string Categoria, string Atributos, string Costo, string IVA, string Unidad, string Presentacion, string Marca, bool cbxGestion, string Rubro, string idProducto, string sectorId)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                Tecnocuisine_API.Entitys.Productos producto = new Tecnocuisine_API.Entitys.Productos();
                ControladorReceta cr = new ControladorReceta();
                producto.id = Convert.ToInt32(idProducto);
                producto.descripcion = descripcion;

                if (Rubro == "")
                    producto.idRubro = 1;
                else
                    producto.idRubro = Convert.ToInt32(Rubro);

                if (Categoria.Trim() == "")
                    producto.categoria = 1;
                else
                    producto.categoria = Convert.ToInt32(Categoria.Split('-')[0].Trim());

                producto.costo = Convert.ToDecimal(Costo.Replace(".", ","));
                producto.unidadMedida = Convert.ToInt32(Unidad);
                producto.alicuota = Convert.ToInt32(IVA);
                producto.estado = 1;
                producto.ProductoFinal = cbxGestion;
                //producto.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);
                List<Recetas_Producto> ListRP = cr.ObtenerListRecetaByIdProducto(Convert.ToInt32(idProducto));
                if (ListRP.Count > 0)
                {
                    VerificarCosto(ListRP, Costo);
                }

                //Set Sector
                if (sectorId == "-1")
                    producto.idSectorProductivo = null;
                else
                    producto.idSectorProductivo = int.Parse(sectorId);

                int resultado = controladorProducto.EditarProducto(producto);

                if (resultado > 0)
                {
                    if (Atributos.Trim() != "")
                    {

                        string[] atributos = Atributos.Split(',');
                        controladorProducto.EliminarProductoAtributo(producto.id);

                        foreach (string s in atributos)
                        {
                            if (s != "")
                            {
                                Productos_Atributo atributo = new Productos_Atributo();
                                atributo.producto = producto.id;
                                atributo.atributo = Convert.ToInt32(s.Split('-')[0]);
                                controladorProducto.AgregarProductoAtributo(atributo);
                            }

                        }
                    }
                    else
                    {
                        controladorProducto.EliminarProductoAtributo(producto.id);
                        Productos_Atributo atributo = new Productos_Atributo();
                        atributo.producto = producto.id;
                        atributo.atributo = 1;
                        controladorProducto.AgregarProductoAtributo(atributo);
                    }

                    if (Presentacion.Trim() != "")
                    {

                        string[] presentaciones = Presentacion.Split(',');
                        controladorProducto.EliminarProductoPresentacion(producto.id);

                        foreach (string s in presentaciones)
                        {
                            if (s.Trim() != "")
                            {
                                Productos_Presentacion pres = new Productos_Presentacion();
                                pres.idProducto = producto.id;
                                pres.idPresentacion = Convert.ToInt32(s.Split('-')[0]);
                                controladorProducto.AgregarProductoPresentacion(pres);
                            }

                        }
                    }
                    else
                    {
                        controladorProducto.EliminarProductoPresentacion(producto.id);
                        Productos_Presentacion pres = new Productos_Presentacion();
                        pres.idProducto = producto.id;
                        pres.idPresentacion = 1;
                        controladorProducto.AgregarProductoPresentacion(pres);
                    }



                    if (Marca.Trim() != "")
                    {
                        string[] verMarcas = Marca.Split(',');
                        controladorProducto.EliminarProductoMarca(producto.id);
                        foreach (string i in verMarcas)
                        {
                            if (!string.IsNullOrEmpty(i) && i.Trim().Count() > 0)
                            {
                                Marca_Productos pp = new Marca_Productos();
                                pp.id_producto = producto.id;
                                pp.id_marca = Convert.ToInt32(i.Split('-')[0].Trim());
                                pp.estado = 1;
                                pp.FechaCreacion = DateTime.Now;

                                controladorProducto.AgregarProductoMarca(pp);
                            }
                        }
                    }
                    else
                    {
                        controladorProducto.EliminarProductoMarca(producto.id);
                        Marca_Productos pp = new Marca_Productos();
                        pp.id_producto = producto.id;
                        pp.id_marca = 1;
                        pp.estado = 1;
                        pp.FechaCreacion = DateTime.Now;
                    }

                    JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    javaScript.MaxJsonLength = 5000000;
                    string resultadoJSON = javaScript.Serialize("Exito al editar el Producto.");
                    return resultadoJSON;
                }
                else
                {
                    JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    javaScript.MaxJsonLength = 5000000;
                    string resultadoJSON = javaScript.Serialize("Hubo un error al editar el producto.");
                    return resultadoJSON;
                }

            }
            catch (Exception ex)
            {
                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize("Error al editar. ex:" + ex.Message);
                return resultadoJSON;
            }
        }

        public static void VerificarCosto(List<Recetas_Producto> ListRP, string Costo)
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorReceta cr = new ControladorReceta();
                foreach (var item in ListRP)
                {
                    decimal costoActualizado = Convert.ToDecimal(Costo.Replace(".", ","));
                    if (item.Productos.costo != costoActualizado)
                    {
                        decimal CostoViejo = (decimal)item.Recetas.Costo;
                        decimal costoNuevo = (CostoViejo - item.Productos.costo) + costoActualizado;
                        Tecnocuisine_API.Entitys.Recetas receta = new Tecnocuisine_API.Entitys.Recetas();
                        receta.id = item.Recetas.id;
                        receta.descripcion = item.Recetas.descripcion;
                        receta.estado = item.Recetas.estado;
                        receta.desperdicio = item.Recetas.desperdicio;
                        receta.peso = item.Recetas.peso;
                        receta.rinde = item.Recetas.rinde;
                        receta.merma = item.Recetas.merma;
                        receta.coeficiente = item.Recetas.coeficiente;
                        receta.categoria = item.Recetas.categoria;
                        receta.Tipo = item.Recetas.Tipo;
                        receta.UnidadMedida = item.Recetas.UnidadMedida;
                        receta.Costo = costoNuevo;
                        receta.PrVenta = item.Recetas.PrVenta;
                        receta.PorcFoodCost = item.Recetas.PorcFoodCost;
                        receta.CostMarginal = item.Recetas.CostMarginal;
                        receta.BuenasPracticas = item.Recetas.BuenasPracticas;
                        receta.InfNutricional = item.Recetas.InfNutricional;
                        receta.Codigo = item.Recetas.Codigo;
                        receta.PesoU = item.Recetas.PesoU;
                        receta.CostoU = costoNuevo / item.Recetas.rinde;
                        receta.ProductoFinal = item.Recetas.ProductoFinal;

                        cr.EditarReceta(receta);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        //Este WebMethod guarda la receta con todos sus datos y todos su productos, cuando se esta creando por primera vez
        [WebMethod]
        public static string GuardarReceta2(string descripcion, string codigo, /*string Categoria,*/ string Sector, /*string Atributos,*/ string Unidad,
            string Tipo, string rinde, string prVenta, string idProductosRecetas, string BrutoT, string CostoT, string BrutoU, string CostoU,
            string FoodCost, string ContMarg, string BuenasPract, string InfoNut, string idPasosRecetas, string Presentaciones, bool ProdFinal, bool Comprable, string Rubro)
        {
            try
            {
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorAtributo controladorAtributo = new ControladorAtributo();
                Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();
                
                //Esta parte se encarga de guardar los datos de la receta, es decir la primera parte de la receta
                Receta.descripcion = descripcion;
                Receta.Codigo = codigo;
                Receta.categoria = /*Convert.ToInt32(Categoria.Trim().Split('-')[0]);*/ null;
                Receta.UnidadMedida = Convert.ToInt32(Unidad);
                Receta.Tipo = Convert.ToInt32(Tipo);
                Receta.PrVenta = decimal.Parse(prVenta, CultureInfo.InvariantCulture);
                Receta.Costo = decimal.Parse(CostoT, CultureInfo.InvariantCulture);
                Receta.peso = decimal.Parse(BrutoT, CultureInfo.InvariantCulture);
                Receta.CostoU = decimal.Parse(CostoU, CultureInfo.InvariantCulture);
                Receta.PesoU = decimal.Parse(BrutoU, CultureInfo.InvariantCulture);
                Receta.PorcFoodCost = decimal.Parse(FoodCost, CultureInfo.InvariantCulture);
                Receta.CostMarginal = decimal.Parse(ContMarg, CultureInfo.InvariantCulture);
                Receta.BuenasPracticas = BuenasPract;
                Receta.InfNutricional = InfoNut;
                Receta.ProductoFinal = ProdFinal;
                Receta.Comprable = Comprable;
                Receta.idRubro = Convert.ToInt32(Rubro);
                //Receta.sector -- falta agregar

                Receta.estado = 1;
                if (rinde == "")
                {
                    Receta.rinde = null;
                }
                else
                {
                    Receta.rinde = decimal.Parse(rinde, CultureInfo.InvariantCulture);
                }

                //if (txtDesperdicio.Text == "")
                //{
                //    Receta.desperdicio = null;
                //    Receta.merma = null;
                //}
                //else
                //{
                //    Receta.desperdicio = Convert.ToDecimal(txtDesperdicio.Text.Replace('.', ','));
                //    Receta.merma = Convert.ToDecimal(txtMerma.Text.Replace('.', ','));
                //}

                //Receta.presentacion = Convert.ToInt32(ListPresentaciones.SelectedValue);

                int resultado = controladorReceta.AgregarReceta(Receta); //Guarda los datos de la primera parte de la receta

                if (resultado > 0)
                {
                    string[] items = idProductosRecetas.Split(';');
                    int idProducto = 0;
                    foreach (var pr in items)
                    {
                        if (pr != "")
                        {
                            string[] producto = pr.Split(',');
                            int ultimaPosicion = producto.Length - 1;


                            //-----------------------------------------------------------------------------------------------------

                            int numeroEncontrado = -1;
                            foreach (string elemento in producto)
                            {
                                // Utiliza una expresión regular para buscar el patrón "idSectorProductivo_" seguido de un número
                                Match match = Regex.Match(elemento, @"idSectorProductivo_(\d+)");

                                if (match.Success)
                                {
                                    // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                    string numeroStr = match.Groups[1].Value;
                                    if (int.TryParse(numeroStr, out int numero))
                                    {
                                        numeroEncontrado = numero;
                                        break; // Rompe el bucle una vez que se encuentra el número
                                    }
                                }
                            }


                            //-----------------------------------------------------------------------------------------


                            int tiempoParte = 1;
                            foreach (string elemento in producto)
                            {
                                Match match = Regex.Match(elemento, @"Tiempo_(\d+)");

                                if (match.Success)
                                {
                                    // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                    string numeroStr = match.Groups[1].Value;
                                    if (int.TryParse(numeroStr, out int numero))
                                    {
                                        tiempoParte = numero;
                                        break; // Rompe el bucle una vez que se encuentra el número
                                    }

                                }
                            }


                            //-----------------------------------------------------------------------------------------


                            //Esta parte pregunta si elemento de es un producto o receta usada como ingrediente
                            if (producto[1] == "Producto")
                            {
                                Recetas_Producto productoNuevo = new Recetas_Producto();
                                productoNuevo.idReceta = resultado;
                                productoNuevo.idProducto = Convert.ToInt32(producto[0]);
                                idProducto = productoNuevo.idProducto;
                                productoNuevo.cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                productoNuevo.idSectorProductivo = numeroEncontrado;
                                productoNuevo.Tiempo = tiempoParte;


                                controladorReceta.AgregarReceta_Producto(productoNuevo);
                            }
                            //Si es falsa la condicion, quiere decir que es una receta usada como ingrediente
                            else
                            {
                                int idSectorProductivo = 1;
                                foreach (string elemento in producto)
                                {
                                    Match match = Regex.Match(elemento, @"idSectorProductivo_(\d+)");

                                    if (match.Success)
                                    {
                                        // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                        string numeroStr = match.Groups[1].Value;
                                        if (int.TryParse(numeroStr, out int numero))
                                        {
                                            idSectorProductivo = numero;
                                            break; // Rompe el bucle una vez que se encuentra el número
                                        }
                                    }
                                }


                                int tiempoParteRecetes_Recetas = 1;
                                foreach (string elemento in producto)
                                {
                                    Match match = Regex.Match(elemento, @"Tiempo_(\d+)");

                                    if (match.Success)
                                    {
                                        // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                        string numeroStr = match.Groups[1].Value;
                                        if (int.TryParse(numeroStr, out int numero))
                                        {
                                            tiempoParteRecetes_Recetas = numero;
                                            break; // Rompe el bucle una vez que se encuentra el número
                                        }

                                    }
                                }

                                //Guarda la receta usada como ingrediente
                                Recetas_Receta recetaNueva = new Recetas_Receta();
                                recetaNueva.idReceta = Receta.id;
                                recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
                                recetaNueva.cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                recetaNueva.idSectorProductivo = idSectorProductivo;
                                recetaNueva.Tiempo = tiempoParteRecetes_Recetas;
                                controladorReceta.AgregarReceta_Receta(recetaNueva);

                            }
                        }
                    }
                    //if (controladorReceta.EsMonoProducto(resultado))
                    //{
                    //    int categoria = controladorProducto.ObtenerProductoId(idProducto).categoria;
                    //    if (categoria > 0)
                    //    {
                    //        Tecnocuisine_API.Entitys.Recetas r = controladorReceta.ObtenerRecetaId(resultado);
                    //        r.categoria = categoria;
                    //        controladorReceta.EditarReceta(r);

                    //        List<Productos_Atributo> atributos = controladorAtributo.ObtenerAtributosByIdProducto(idProducto);
                    //        foreach (Productos_Atributo item in atributos)
                    //        {
                    //            Recetas_Atributo atributo = new Recetas_Atributo();
                    //            atributo.idAtributo = item.atributo;
                    //            atributo.idReceta = resultado;


                    //            controladorReceta.AgregarReceta_Atributo(atributo);
                    //        }

                    //    }
                    //}

                    //try
                    //{
                        //string[] itemsA;
                        //if (Atributos.Contains(","))
                        //    itemsA = Atributos.Split(',');
                        //else
                        //    itemsA = Atributos.Split(';');
                        //int idAtributo = 0;
                        //foreach (var pr in itemsA)
                        //{
                        //    if (pr != "")
                        //    {
                        //        string[] Atributo = pr.Split('-');
                        //        if (Atributo[1] != "")
                        //        {
                        //            Recetas_Atributo AtributoNuevo = new Recetas_Atributo();
                        //            AtributoNuevo.idReceta = resultado;
                        //            AtributoNuevo.idAtributo = Convert.ToInt32(Atributo[0]);
                        //            idAtributo = AtributoNuevo.idAtributo.Value;

                        //            controladorReceta.AgregarReceta_Atributo(AtributoNuevo);
                        //        }

                        //    }
                        //}
                    //}
                    //catch { }

                    try
                    {
                        string[] itemsP;
                        if (idPasosRecetas.Contains(";"))
                            itemsP = idPasosRecetas.Split(';');
                        else
                            itemsP = idPasosRecetas.Split(',');
                        foreach (var pr in itemsP)
                        {
                            if (pr != "")
                            {
                                string[] Pasos = pr.Split('-');
                                if (Pasos[1] != "")
                                {
                                    Recetas_Pasos PasosNuevo = new Recetas_Pasos();
                                    PasosNuevo.idReceta = resultado;
                                    PasosNuevo.numpaso = Pasos[0];
                                    PasosNuevo.paso = Pasos[1];
                                    PasosNuevo.estado = 1;

                                    controladorReceta.AgregarReceta_Pasos(PasosNuevo);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {


                    }

                    try
                    {
                        string[] itemsS;

                        if (Sector.Contains(","))
                            itemsS = Sector.Split(',');
                        else
                            itemsS = Sector.Split(';');

                        foreach (var pr in itemsS)
                        {
                            if (pr != "")
                            {
                                string[] Sect = pr.Split('-');
                                if (Sect[1] != "")
                                {
                                    SectorP_Recetas sectorP_ = new SectorP_Recetas();
                                    sectorP_.idReceta = resultado;
                                    sectorP_.idSectorP = Convert.ToInt32(Sect[0]);
                                    sectorP_.estado = 1;

                                    controladorReceta.EliminarSectorProductivoRecetaByIdReceta(resultado); //Eliminar sector anterior si tiene
                                    controladorReceta.AgregarSectorProductivoReceta(sectorP_);
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        string[] itemsPres;

                        if (Sector.Contains(","))
                            itemsPres = Presentaciones.Split(',');
                        else
                            itemsPres = Presentaciones.Split(';');

                        foreach (var pr in itemsPres)
                        {
                            if (pr != "")
                            {
                                string[] PRess = pr.Split('-');
                                if (PRess[1] != "")
                                {
                                    Recetas_Presentacion Rec_Pres = new Recetas_Presentacion();
                                    Rec_Pres.idPresentacion = Convert.ToInt32(PRess[0]);
                                    Rec_Pres.idRecetas = resultado;


                                    controladorReceta.AgregarPresentacionReceta(Rec_Pres);
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {


                    }

                    JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    javaScript.MaxJsonLength = 5000000;
                    string resultadoJSON = javaScript.Serialize("Exito guardando la Receta.");
                    return resultadoJSON;
                }
                else
                {
                    JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    javaScript.MaxJsonLength = 5000000;
                    string resultadoJSON = javaScript.Serialize("Hubo un error al guardar la receta.");
                    return resultadoJSON;
                }
            }
            catch (Exception ex)
            {
                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(" Error al guardar la Receta. ex:" + ex.Message);
                return resultadoJSON;
            }

        }

        //Esra funcion se ejecuta cuando se edita una receta ya existente
        [WebMethod]
        public static void EditarReceta2(string descripcion, string codigo, /*string Categoria,*/ string Sector, /*string Atributos,*/ string Unidad,
           string Tipo, string rinde, string prVenta, string idProductosRecetas, string BrutoT, string CostoT, string BrutoU, string CostoU,
           string FoodCost, string ContMarg, string BuenasPract, string InfoNut, string idPasosRecetas, string idReceta, string Presentaciones, bool ProdFinal)
        {
            try
            {
                //Esta parte guarda todos los datos de la primera parte de la receta 
                ControladorReceta controladorReceta = new ControladorReceta();
                ControladorProducto controladorProducto = new ControladorProducto();
                ControladorAtributo controladorAtributo = new ControladorAtributo();
                Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();
                Receta.id = Convert.ToInt32(idReceta);
                Receta.descripcion = descripcion;
                Receta.Codigo = codigo;
                Receta.categoria = /*Convert.ToInt32(Categoria.Trim().Split('-')[0])*/ null;
                Receta.UnidadMedida = Convert.ToInt32(Unidad);
                Receta.Tipo = Convert.ToInt32(Tipo);
                Receta.PrVenta = decimal.Parse(prVenta, CultureInfo.InvariantCulture);
                Receta.Costo = decimal.Parse(CostoT, CultureInfo.InvariantCulture);
                Receta.peso = decimal.Parse(BrutoT, CultureInfo.InvariantCulture);
                Receta.CostoU = decimal.Parse(CostoU, CultureInfo.InvariantCulture);
                Receta.PesoU = decimal.Parse(BrutoU, CultureInfo.InvariantCulture);
                Receta.PorcFoodCost = decimal.Parse(FoodCost, CultureInfo.InvariantCulture);
                Receta.CostMarginal = decimal.Parse(ContMarg, CultureInfo.InvariantCulture);
                Receta.BuenasPracticas = BuenasPract;
                Receta.InfNutricional = InfoNut;
                Receta.ProductoFinal = ProdFinal;

                //Receta.sector -- falta agregar

                Receta.estado = 1;
                //Receta.coeficiente = Convert.ToDecimal(hiddenCoeficiente.Value.Replace('.', ','));
                if (rinde == "")
                {
                    Receta.rinde = null;
                }
                else
                {
                    Receta.rinde = decimal.Parse(rinde, CultureInfo.InvariantCulture);
                }

                int resultado = controladorReceta.EditarReceta(Receta); //Edita los datos de la primera parte de la receta

                //Si los datos de la primera parte de la receta se puedieron editar entonces entrea al if
                if (resultado > 0)
                {
                    //Elimina todos los productos y tambien las recetas usadas como ingrediente que pertenezcan a la receta
                    //Esto lo hace porque los va a agregar nuevamente, es como si los pisara
                    controladorReceta.EliminarIngredientes(Receta.id);
                    string[] items = idProductosRecetas.Split(';');
                    foreach (var pr in items)
                    {
                        if (!string.IsNullOrEmpty(pr))
                        {

                            string[] producto = pr.Split(',');
                            int numeroEncontrado = -1;
                            foreach (string elemento in producto)
                            {
                                // Utiliza una expresión regular para buscar el patrón "idSectorProductivo_" seguido de un número
                                Match match = Regex.Match(elemento, @"idSectorProductivo_(\d+)");

                                if (match.Success)
                                {
                                    // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                    string numeroStr = match.Groups[1].Value;
                                    if (int.TryParse(numeroStr, out int numero))
                                    {
                                        numeroEncontrado = numero;
                                        break; // Rompe el bucle una vez que se encuentra el número
                                    }
                                }
                            }

                            int tiempoParte = 1;
                            foreach (string elemento in producto)
                            {
                                Match match = Regex.Match(elemento, @"Tiempo_(\d+)");

                                if (match.Success)
                                {
                                    // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                    string numeroStr = match.Groups[1].Value;
                                    if (int.TryParse(numeroStr, out int numero))
                                    {
                                        tiempoParte = numero;
                                        break; // Rompe el bucle una vez que se encuentra el número
                                    }

                                }
                            }

                            //Valida si el elemento es producto o una receta usada como ingrediente
                            if (producto[1] == "Producto")
                            {
                                //obtiene los datos del producto
                                Recetas_Producto productoNuevo = new Recetas_Producto();
                                productoNuevo.idReceta = Receta.id;
                                productoNuevo.idProducto = Convert.ToInt32(producto[0]);
                                productoNuevo.cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                //productoNuevo.idSectorProductivo = Convert.ToInt32(1);
                                productoNuevo.idSectorProductivo = numeroEncontrado;
                                productoNuevo.Tiempo = tiempoParte;
                                //Guarda el producto
                                controladorReceta.AgregarReceta_Producto(productoNuevo);
                            }
                            //Si viene por aca quiere decir es una receta usada como ingrediente y no un producto
                            else
                            {
                                int idSectorProductivo = 1;
                                foreach (string elemento in producto)
                                {
                                    // Utiliza una expresión regular para buscar el patrón "idSectorProductivo_" seguido de un número
                                    //Match match = Regex.Match(elemento, @"idSectorProductivoRecetas_recetas_(\d+)");
                                    Match match = Regex.Match(elemento, @"idSectorProductivo_(\d+)");

                                    if (match.Success)
                                    {
                                        // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                        string numeroStr = match.Groups[1].Value;
                                        if (int.TryParse(numeroStr, out int numero))
                                        {
                                            idSectorProductivo = numero;
                                            break; // Rompe el bucle una vez que se encuentra el número
                                        }
                                    }
                                }


                                int tiempoParteRecetes_Recetas = 1;
                                foreach (string elemento in producto)
                                {
                                    Match match = Regex.Match(elemento, @"Tiempo_(\d+)");

                                    if (match.Success)
                                    {
                                        // Si se encuentra una coincidencia, extrae el número y almacénalo en la variable
                                        string numeroStr = match.Groups[1].Value;
                                        if (int.TryParse(numeroStr, out int numero))
                                        {
                                            tiempoParteRecetes_Recetas = numero;
                                            break; // Rompe el bucle una vez que se encuentra el número
                                        }

                                    }
                                }



                                //Obtiene los datos de la receta
                                Recetas_Receta recetaNueva = new Recetas_Receta();
                                recetaNueva.idReceta = Receta.id;
                                recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
                                recetaNueva.cantidad = decimal.Parse(producto[2], CultureInfo.InvariantCulture);
                                recetaNueva.idSectorProductivo = idSectorProductivo;
                                recetaNueva.Tiempo = tiempoParteRecetes_Recetas;
                                //Guarda la receta usada como ingrediente
                                controladorReceta.AgregarReceta_Receta(recetaNueva);
                            }
                        }
                    }

                    try
                    {
                        //string[] itemsA = Atributos.Split(',');
                        //int idAtributo = 0;
                        //controladorReceta.EliminarAtributos(Convert.ToInt32(idReceta));
                        //foreach (var pr in itemsA)
                        //{
                        //    if (pr != "")
                        //    {
                        //        string[] Atributo = pr.Split('-');
                        //        if (Atributo[1] != "")
                        //        {
                        //            Recetas_Atributo AtributoNuevo = new Recetas_Atributo();
                        //            AtributoNuevo.idReceta = Receta.id;
                        //            AtributoNuevo.idAtributo = Convert.ToInt32(Atributo[0]);
                        //            idAtributo = AtributoNuevo.idAtributo.Value;

                        //            controladorReceta.AgregarReceta_Atributo(AtributoNuevo);
                        //        }

                        //    }
                        //}
                    }
                    catch { }

                    try
                    {
                        string[] itemsP = idPasosRecetas.Split(';');

                        controladorReceta.eliminarPasos(Convert.ToInt32(idReceta));
                        foreach (var pr in itemsP)
                        {
                            if (pr != "")
                            {
                                string[] Pasos = pr.Split('-');
                                if (Pasos[1] != "")
                                {
                                    Recetas_Pasos PasosNuevo = new Recetas_Pasos();
                                    PasosNuevo.idReceta = Receta.id;
                                    PasosNuevo.numpaso = Pasos[0];
                                    PasosNuevo.paso = Pasos[1];
                                    PasosNuevo.estado = 1;

                                    controladorReceta.AgregarReceta_Pasos(PasosNuevo);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {


                    }

                    try
                    {
                        string[] itemsS = Sector.Split(',');

                        foreach (var pr in itemsS)
                        {
                            if (pr != "")
                            {
                                string[] Sect = pr.Split('-');
                                if (Sect[1] != "")
                                {
                                    SectorP_Recetas sectorP_ = new SectorP_Recetas();
                                    sectorP_.idReceta = Receta.id;
                                    sectorP_.idSectorP = Convert.ToInt32(Sect[0]);
                                    sectorP_.estado = 1;

                                    controladorReceta.EliminarSectorProductivoRecetaByIdReceta(Receta.id); //Eliminar sector anterior si tiene
                                    controladorReceta.AgregarSectorProductivoReceta(sectorP_);
                                }

                            }
                        }
                    }
                    catch (Exception)
                    {


                    }
                    try
                    {
                        string[] itemsPres;
                        controladorReceta.EliminarPresentaciones(resultado);
                        if (Sector.Contains(","))
                            itemsPres = Presentaciones.Split(',');
                        else
                            itemsPres = Presentaciones.Split(';');

                        foreach (var pr in itemsPres)
                        {
                            if (pr != "")
                            {
                                string[] PRess = pr.Split('-');
                                if (PRess[1] != "")
                                {
                                    Recetas_Presentacion Rec_Pres = new Recetas_Presentacion();
                                    Rec_Pres.idPresentacion = Convert.ToInt32(PRess[0]);
                                    Rec_Pres.idRecetas = resultado;

                                    controladorReceta.AgregarPresentacionReceta(Rec_Pres);
                                }

                            }
                        }
                    }
                    //agregar toast
                    catch (Exception)
                    {
                        //agregar toast

                    }

                }

            }
            catch (Exception ex)
            {

            }

        }


        [WebMethod]
        public static void GuardarProducto2(string id)
        {
            try
            {
                String path = System.Web.HttpContext.Current.Server.MapPath("../../images/Productos/" + 46 + "/");

                //foreach (var formFile in fileUpload)
                //{
                //    if (formFile.Length > 0)
                //    {
                //        string ruta = Path.GetFullPath("ArchivosImportacion");
                //        if (!Directory.Exists(ruta))
                //        {
                //            Directory.CreateDirectory(ruta);
                //        }
                //        var filePath = "../../images/Productos/" + 46 + "/img.png";

                //        using (var stream = System.IO.File.Create(filePath))
                //        {
                //            await formFile.CopyToAsync(stream);
                //        }
                //        path = System.IO.Path.Combine(filePath);

                //    }
                //}


                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}

                //FileUpload upload = new FileUpload();
                //HttpContext context = new HttpContext();
                //HttpPostedFile file = context.Request.Files[s];
                //objProperty.PropertyImage = filepath;
                //upload.SaveAs(img);
                //upload.PostedFile.SaveAs(path + upload.FileName);

            }
            catch (Exception ex)
            {

            }

        }
        private void CargarUnidadesMedida()
        {
            try
            {

                ControladorUnidad controladorUnidad = new ControladorUnidad();
                this.ListUnidadMedida.DataSource = controladorUnidad.ObtenerTodosUnidades();
                this.ListUnidadMedida.DataValueField = "id";
                this.ListUnidadMedida.DataTextField = "descripcion";
                this.ListUnidadMedida.DataBind();
                ListUnidadMedida.Items.Insert(0, new ListItem("Seleccione", "-1"));



            }
            catch (Exception ex)
            {

            }
        }
        private void CargarAlicuotasIVA()
        {
            try
            {
                ControladorIVA controladorIVA = new ControladorIVA();
                this.ListAlicuota.DataSource = controladorIVA.ObtenerTodosAlicuotas_IVA();
                this.ListAlicuota.DataValueField = "id";
                this.ListAlicuota.DataTextField = "porcentaje";
                this.ListAlicuota.DataBind();
                ListAlicuota.Items.Insert(0, new ListItem("Seleccione", "-1"));

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
                this.ddlSectores.DataSource = cSectorProductivo.ObtenerTodosSectorProductivo();
                this.ddlSectores.DataValueField = "id";
                this.ddlSectores.DataTextField = "descripcion";
                this.ddlSectores.DataBind();
                ddlSectores.Items.Insert(0, new ListItem("Seleccione", "-1"));
            }
            catch (Exception)
            {

            }
        }

        private void ObtenerGruposArticulos()
        {
            try
            {

                DataTable dt = controladorArticulo.obtenerGruposArticulos();

                //agrego todos
                DataRow dr = dt.NewRow();
                dr["descripcion"] = "Seleccione...";
                dr["id"] = -1;
                dt.Rows.InsertAt(dr, 0);

                this.ListGrupo.DataSource = dt;
                this.ListGrupo.DataValueField = "id";
                this.ListGrupo.DataTextField = "descripcion";

                this.ListGrupo.DataBind();


            }
            catch (Exception ex)
            {
            }
        }

        private void ObtenerSubGruposArticulos(int grupo)
        {
            try
            {
                DataTable dt = controladorArticulo.obtenerSubGruposArticulos(grupo);

                //agrego todos
                DataRow dr = dt.NewRow();
                dr["descripcion"] = "Seleccione...";
                dr["id"] = -1;
                dt.Rows.InsertAt(dr, 0);

                this.ListSubgrupo.DataSource = dt;
                this.ListSubgrupo.DataValueField = "id";
                this.ListSubgrupo.DataTextField = "descripcion";

                this.ListSubgrupo.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        public void ObtenerPresentaciones()
        {
            try
            {
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                var presentaciones = controladorPresentacion.ObtenerTodosPresentaciones();

                if (presentaciones.Count > 0)
                {
                    CargarOptionsListPresentacion(presentaciones);
                    foreach (var item in presentaciones)
                    {
                        CargarPresentacionesPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarOptionsListPresentacion(List<Tecnocuisine_API.Entitys.Presentaciones> presentaciones)
        {
            try
            {
                var builder = new System.Text.StringBuilder();

                foreach (var press in presentaciones)
                {

                    builder.Append(String.Format("<option value='{0}' id='PresentacionID_" + press.id + "_" + press.cantidad + "'>", press.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListOptionsPresentacion.InnerHtml = builder.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CargarMarcaFinalPH(Tecnocuisine_API.Entitys.Articulos_Marcas marcas)
        {
            try
            {

                //fila
                TableRow tr = new TableRow();
                //tr.ID = "presentacion_" + presentacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = marcas.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:  0px; vertical-align: middle;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = marcas.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:  0px; vertical-align: middle;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("a");
                cbxAgregar.Attributes.Add("class", "btn  btn-xs");
                cbxAgregar.Attributes.Add("onclick", $"EliminarFilaMarca({marcas.id})");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar.Attributes.Add("data-original-title", "Eliminar");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");
                //cbxAgregar.Attributes.Add("value", "1");
                cbxAgregar.Style.Add("background-color", "transparent");
                cbxAgregar.ID = "ContentPlaceHolder1_btnEliminar_" + marcas.id;
                cbxAgregar.InnerHtml = "<span><i style=\"color: black\" class=\"fa fa-trash - o\"></i></span>";
                celAccion.Controls.Add(cbxAgregar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:  0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phMarcasDefinitivo.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarPresentacionesFinalPH(Tecnocuisine_API.Entitys.Presentaciones presentacion)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                //tr.ID = "presentacion_" + presentacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = presentacion.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:  0px; vertical-align: middle;");



                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = presentacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");

                tr.Cells.Add(celNombre);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = presentacion.cantidad.ToString().Replace(',', '.');
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Right;
                celCantidad.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celCantidad);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("a");
                cbxAgregar.Attributes.Add("class", "btn  btn-xs");
                //cbxAgregar.Attributes.Add("value", "1");
                cbxAgregar.Attributes.Add("onclick", $"EliminarFila({presentacion.id})");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");
                cbxAgregar.Attributes.Add("data-original-title", "Eliminar");
                cbxAgregar.Attributes.Add("data-toggle", "tooltip");

                cbxAgregar.Style.Add("background-color", "transparent");
                cbxAgregar.ID = "ContentPlaceHolder1_btnEliminar_" + presentacion.id;
                cbxAgregar.InnerHtml = "<span><i style=\"color: black\" class=\"fa fa-trash - o\"></i></span>";
                celAccion.Controls.Add(cbxAgregar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                PHPresentacionFinal.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }

        }
        public void CargarPresentacionesPH(Tecnocuisine_API.Entitys.Presentaciones presentacion)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "presentacion_" + presentacion.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = presentacion.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = presentacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celNombre);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = presentacion.cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Right;
                celCantidad.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celCantidad);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                cbxAgregar.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                cbxAgregar.Attributes.Add("type", "checkbox");
                //cbxAgregar.Attributes.Add("value", "1");
                cbxAgregar.ID = "btnSelecPres_" + presentacion.id + " - " + presentacion.descripcion;
                celAccion.Controls.Add(cbxAgregar);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 0px !important; padding-top:   0px; vertical-align: middle;");
                tr.Cells.Add(celAccion);

                phPresentaciones.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        private void cargarNestedListCategoriasHijas(int id, HtmlGenericControl li)
        {
            try
            {
                ControladorCategoria controlador = new ControladorCategoria();
                List<Tecnocuisine_API.Entitys.Categorias> categorias = controlador.obtenerCategoriasHijas(id);
                if (categorias.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Categorias item in categorias)
                    {
                        HtmlGenericControl liHijo = new HtmlGenericControl("li");

                        liHijo.Attributes.Add("class", "dd-item");
                        liHijo.Attributes.Add("data-id", item.id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle not-draggable editable");
                        div.InnerText = item.descripcion;


                        liHijo.Controls.Add(div);


                        if (controlador.VerificarUltimoNivel(item.id))
                        {
                            HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                            btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                            //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                            //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                            btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                            btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                            btnAgregar.Attributes.Add("data-action", "addCategory");
                            div.Controls.Add(btnAgregar);
                        }


                        cargarNestedListCategoriasHijas(item.id, liHijo);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }


        [WebMethod]
        public static string GetSubgrupos(int id)
        {
            Gestion_Api.Controladores.controladorArticulo controladorProducto = new Gestion_Api.Controladores.controladorArticulo();
            var gruposList = controladorProducto.obtenerSubGrupoByGrupo(id);
            string grupos = "";
            foreach (var item in gruposList)
            {
                grupos += item.id + "," + item.descripcion + ";";
            }

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(grupos);
            return resultadoJSON;
        }

        [WebMethod]
        public static string GetDescripcionProducto(int id)
        {
            ControladorProducto controladorProducto = new ControladorProducto();
            string producto = controladorProducto.ObtenerProductoId(id).descripcion;

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(producto);
            return resultadoJSON;
        }

        private void cargarNestedListAtributos(int idTipoAtributo, HtmlGenericControl liPadre)
        {
            ControladorAtributo controlador = new ControladorAtributo();
            List<Tecnocuisine_API.Entitys.Atributos> atributos = controlador.obtenerAtributosPrimerNivel(idTipoAtributo);
            if (atributos != null)
            {
                HtmlGenericControl ol = new HtmlGenericControl("ol");
                ol.Attributes.Add("class", "dd-list");

                liPadre.Controls.Add(ol);
                foreach (Tecnocuisine_API.Entitys.Atributos item in atributos)
                {
                    HtmlGenericControl liHijo = new HtmlGenericControl("li");

                    liHijo.Attributes.Add("class", "dd-item");
                    liHijo.Attributes.Add("data-id", item.id.ToString());
                    liHijo.Attributes.Add("runat", "server");

                    ol.Controls.Add(liHijo);

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "dd-handle not-draggable editable");
                    div.InnerText = item.descripcion;

                    HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                    cbxAgregar.Attributes.Add("class", "atributos radio btn btn-primary btn-xs pull-right");
                    cbxAgregar.Attributes.Add("type", "checkbox");
                    cbxAgregar.Attributes.Add("value", "1");
                    cbxAgregar.Attributes.Add("name", "fooby[" + idTipoAtributo + "][]");
                    cbxAgregar.Attributes.Add("data-action", "selectcbxAttribute");
                    cbxAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    div.Controls.Add(cbxAgregar);



                    liHijo.Controls.Add(div);




                    HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    btnAgregar.Attributes.Add("data-action", "addAttribute");
                    div.Controls.Add(btnAgregar);

                    cargarNestedListAtributosHijos(item.id, liHijo, idTipoAtributo);
                }
            }
        }
        private void cargarNestedListAtributosHijos(int id, HtmlGenericControl li, int checkboxIndex)
        {
            try
            {
                ControladorAtributo controlador = new ControladorAtributo();
                List<Tecnocuisine_API.Entitys.Atributos> atributos = controlador.obtenerAtributosHijas(id);
                if (atributos.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.Atributos item in atributos)
                    {
                        HtmlGenericControl liHijo = new HtmlGenericControl("li");

                        liHijo.Attributes.Add("class", "dd-item");
                        liHijo.Attributes.Add("data-id", item.id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle not-draggable editable");
                        div.InnerText = item.descripcion;

                        HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                        cbxAgregar.Attributes.Add("class", "atributos radio btn btn-primary btn-xs pull-right");
                        cbxAgregar.Attributes.Add("type", "checkbox");
                        cbxAgregar.Attributes.Add("value", "1");
                        cbxAgregar.Attributes.Add("name", "fooby[" + checkboxIndex + "][]");
                        cbxAgregar.Attributes.Add("data-action", "selectcbxAttribute");
                        cbxAgregar.ID = "btnSelec_" + item.id;
                        div.Controls.Add(cbxAgregar);


                        liHijo.Controls.Add(div);



                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        btnAgregar.Attributes.Add("data-action", "addAttribute");
                        div.Controls.Add(btnAgregar);



                        cargarNestedListAtributosHijos(item.id, liHijo, checkboxIndex);
                    }
                }

            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}