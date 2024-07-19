using Gestion_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine.Formularios.Maestros
{
    public partial class RecetasABM : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorReceta controladorReceta = new ControladorReceta();
        ControladorProducto controladorProducto = new ControladorProducto();
        ControladorAtributo controladorAtributo = new ControladorAtributo();
        ControladorCategoria cc = new ControladorCategoria();
        ControladorSectorProductivo sectorProductivo = new ControladorSectorProductivo();
        ControladorSectorProductivo controladorSector = new ControladorSectorProductivo();

        int accion;
        int idProducto;
        int Mensaje;
        int idReceta;
        int bloqueados;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        protected void Page_Load(object sender, EventArgs e)
        {
            //VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            this.bloqueados = Convert.ToInt32(Request.QueryString["b"]);
            this.idReceta = Convert.ToInt32(Request.QueryString["i"]);

            ObtenerSectores();
            ObtenerRecetas();
            ObtenerProductos();
            ObtenerPresentaciones();
            ObtenerMarcas();
            if (!IsPostBack)
            {
                CargarRubros();
                CargarUnidadesMedida();
                CargarListaCategoriasSoloHijas();
                CargarSectoresProductivodDDL();
                //cargarNestedListCategorias();
                //cargarNestedListAtributos();
                if (accion == 2)
                {
                    CargarReceta();
                    if (bloqueados == 1)
                    {
                        BloquearInputs();
                    }
                }
                if (accion == 3)
                {
                    //CargarAtributosReceta();
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

        }

        public void CargarRubros()
        {
            try
            {
                ControladorRubros cr = new ControladorRubros();
                var listRubros = cr.ObtenerTodosRubros();
                this.ddlRubros.DataSource = listRubros;
                this.ddlRubros.DataValueField = "id";
                this.ddlRubros.DataTextField = "descripcion";
                this.ddlRubros.DataBind();
                ddlRubros.Items.Insert(0, new ListItem("Rubro", "-1"));
            }
            catch (Exception ex)
            {
            }
        }

        //Esta funcion carga los sectores producitvos en la ddl de sectores productivos
        protected void CargarSectoresProductivodDDL()
        {
            try
            {
                DataTable dt = sectorProductivo.GetAllSectoresProductivosDT();
                DataRow drSeleccione = dt.NewRow();
                drSeleccione["descripcion"] = "Seleccione...";
                drSeleccione["id"] = -1;
                dt.Rows.InsertAt(drSeleccione, 0);

                // Ordenar los datos alfabéticamente por la descripción (ignorando la primera fila)
                var sortedData = dt.AsEnumerable()
                                   .Skip(1) // Ignorar la primera fila ("Seleccione...")
                                   .OrderBy(row => row.Field<string>("descripcion"));

                // Crear una nueva tabla con los elementos ordenados y el elemento inicial
                DataTable sortedTable = dt.Clone();
                sortedTable.Rows.Add(drSeleccione.ItemArray);
                foreach (DataRow row in sortedData)
                {
                    sortedTable.ImportRow(row);
                }

                this.ddlSector.DataSource = sortedTable;
                this.ddlSector.DataValueField = "id";
                this.ddlSector.DataTextField = "descripcion";

                this.ddlSector.DataBind();

                this.ddlSector.SelectedValue = "-1";

            }
            catch (Exception ex)
            {


            }
        }

        private void BloquearInputs()
        {
            try
            {
                txtCodigo.Attributes.Add("disabled", "disabled");
                txtDescripcionReceta.Attributes.Add("disabled", "disabled");
                //txtDescripcionCategoria.Attributes.Add("disabled", "disabled");
                ddlTipoReceta.Attributes.Add("disabled", "disabled");
                ddlUnidadMedida.Attributes.Add("disabled", "disabled");
                txtRinde.Attributes.Add("disabled", "disabled");
                txtPrVenta.Attributes.Add("disabled", "disabled");
                txtCantidad.Attributes.Add("disabled", "disabled");
                txtFactor.Attributes.Add("disabled", "disabled");
                txtObservaciones.Attributes.Add("disabled", "disabled");
                txtInfoNutr.Attributes.Add("disabled", "disabled");
                txtPasoDesc.Attributes.Add("disabled", "disabled");
                btnAgregarPaso.Attributes.Add("disabled", "disabled");
                //btnAgregarProducto.Attributes.Add("disabled", "disabled");
                //btnCategorias.Attributes.Add("disabled", "disabled");
                btnProductos.Attributes.Add("disabled", "disabled");
                btnSectores.Attributes.Add("disabled", "disabled");
            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerSectores()
        {
            try
            {
                var Sectores = controladorSector.ObtenerTodosSectorProductivo();

                if (Sectores.Count > 0)
                {

                    foreach (var item in Sectores)
                    {
                        CargarSectoresPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarReceta()
        {
            try
            {
                var Receta = controladorReceta.ObtenerRecetaId(this.idReceta);
                if (Receta != null)
                {
                    hiddenReceta.Value = idReceta.ToString();
                    txtDescripcionReceta.Text = Receta.descripcion;
                    //txtMerma.Text = Receta.merma.ToString();
                    txtCodigo.Text = Receta.Codigo;
                    CheckSePuedeComprar.Checked = (bool)Receta.Comprable;
                    //categoria
                    //var categoria = cc.ObtenerCategoriaById(Receta.categoria.Value);
                    //txtDescripcionCategoria.Text = categoria.id + " - " + categoria.descripcion;
                    if (Receta.ProductoFinal == true)
                    {

                        CheckProductoFinal.Checked = true;
                    }
                    else
                    {
                        CheckProductoFinal.Checked = false;
                    }
                    //sector

                    var listaSectores = Receta.SectorP_Recetas;

                    foreach (var sector in listaSectores)
                    {
                        var sec = controladorSector.ObtenerSectorProductivoId(sector.idSectorP.Value);
                        if (sec != null)

                            txtSector.Text += sec.id + " - " + sec.descripcion /*+ ", "*/;
                    }


                    //atributo depende de la categoria seleccionada
                    //var listaDeatributo = Receta.Recetas_Atributo;
                    //foreach (var atributo in listaDeatributo)
                    //{

                    //    var atr = controladorAtributo.ObtenerAtributoById(atributo.idAtributo.Value);

                    //    if (atr != null)

                    //        txtDescripcionAtributo.Text += atr.id + " - " + atr.descripcion + ", ";

                    //}

                    PrecargarPresentaciones();
                    PrecargarMarcas();
                     
                    ddlTipoReceta.SelectedValue = Receta.Tipo.ToString();
                    ddlUnidadMedida.SelectedValue = Receta.UnidadMedida.ToString();
                    ddlRubros.SelectedValue = Receta.idRubro.ToString();
                    txtKgBrutTotal.Text = Receta.peso.ToString().Replace(',', '.');
                    txtKgxPorcion.Text = Receta.PesoU.Value.ToString().Replace(',', '.');
                    txtRinde.Text = Receta.rinde.Value.ToString().Replace(',', '.');
                    txtCostoTotal.Text = Receta.Costo.Value.ToString("N", culture);
                    txtCostoxPorcion.Text = Receta.CostoU.Value.ToString("N", culture);
                    txtPrVenta.Text = Receta.PrVenta.Value.ToString("N", culture);
                    txtPFoodCost.Text = Receta.PorcFoodCost.Value.ToString("N", culture);
                    txtContMarg.Text = Receta.CostMarginal.Value.ToString("N", culture);


                    //ingredientes/productos
                    ObtenerProductosbyIdReceta(idReceta);

                    //ingredientes / Recetas
                    obtenerRecetasbyIdReceta(idReceta);

                    txtObservaciones.Text = Receta.BuenasPracticas;
                    txtInfoNutr.Text = Receta.InfNutricional;

                    //paso a paso
                    CargarPasos();

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void PrecargarPresentaciones()
        {
            try
            {
                var Presentacionreceta = controladorReceta.ObtenerPresentacionByIdReceta(idReceta);
                if (Presentacionreceta != null)
                {
                    hfPresentaciones.Value = Presentacionreceta.FirstOrDefault().idPresentacion.ToString() + " - " + Presentacionreceta.FirstOrDefault().Presentaciones.descripcion;
                    txtPresentaciones.Text = Presentacionreceta.FirstOrDefault().idPresentacion.ToString() + " - " + Presentacionreceta.FirstOrDefault().Presentaciones.descripcion;
                }
            }
            catch (Exception)
            {
            }
        }

        private void PrecargarMarcas()
        {
            ControladorMarca controladorMarca = new ControladorMarca();

            // Obtener todas las marcas asignadas a la receta
            var marcasReceta = controladorReceta.ObtenerMarcasRecetaByIdReceta(this.idReceta);

            if (marcasReceta != null)
            {
                foreach (Marca_Recetas marcaReceta in marcasReceta)
                {
                    Articulos_Marcas marca = controladorMarca.ObtenerMarcaId((int)marcaReceta.id_marca);

                    if (marca != null)
                        txtMarcas.Text += marca.id + " - " + marca.descripcion + ", ";
                }
            }
        }

        private void obtenerRecetasbyIdReceta(int idReceta)
        {
            try
            {
                var listaRR = controladorReceta.obtenerRecetasbyReceta(idReceta); //recetas_recetas

                if (listaRR.Count > 0)
                {
                    //CargarProductosOptions(productos);

                    foreach (var item in listaRR)
                    {
                        CargarRecetasPHModal2(item);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CargarPasos()
        {
            try
            {
                DataTable listaPasos = controladorReceta.obtenerPasos(idReceta);

                foreach (DataRow l in listaPasos.Rows)
                {
                    cargarPasoPH(l);
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void cargarPasoPH(DataRow r)
        {
            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = "Row_" + r["id"];

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = r["numpaso"].ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = r["paso"].ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                //celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";

                btnDetalles.Attributes.Add("data-toggle", "tooltip");

                btnDetalles.Text = "<span><i style=\"color:black\" class='fa fa-trash'></i></span>";
                btnDetalles.Attributes.Add("class", "btn  btn-xs");
                btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");

                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "borrarPaso('Row_" + r["id"].ToString() + "');");
                //btnDetalles.Style.Add("padding", " 0% 5% 2% 5.5%"); btnDetalles.Style.Add("background-color"," transparent");
                //btnDetalles.Attributes.Add("type", "checkbox");
                //btnDetalles.ID = "btnSelecProd_" + producto.id + "_" + producto.descripcion + "_" + UnidadMedida;
                //btnDetalles.InnerText = "<i class=\"fa fa-trash - o\" style=\"color: black\"></i>";
                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", " text-align: center");
                tr.Cells.Add(celAccion);

                PasosPH.Controls.Add(tr);

                hfPasos.Value += r["id"].ToString() + "-" + r["paso"].ToString() + ";";
            }
            catch (Exception ex)
            {


            }
        }

        public void CargarSectoresPH(Tecnocuisine_API.Entitys.SectorProductivo Sector)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = Sector.id.ToString();

                TableCell celNum = new TableCell();
                celNum.Text = Sector.id.ToString();
                //celNum.Font.Bold = true;
                celNum.VerticalAlign = VerticalAlign.Middle;
                celNum.HorizontalAlign = HorizontalAlign.Left;
                celNum.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNum);


                TableCell celFamilia = new TableCell();
                celFamilia.Text = Sector.codigo;
                celFamilia.VerticalAlign = VerticalAlign.Middle;
                celFamilia.HorizontalAlign = HorizontalAlign.Left;
                celFamilia.Width = Unit.Percentage(5);
                //celFamilia.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celFamilia);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = Sector.descripcion;
                celDescripcion.Font.Bold = true;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

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

                //HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                //btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs");
                ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                ////btnDetalles.Attributes.Add("onclick", "agregarSector(this.id); return false;");
                //btnDetalles.Attributes.Add("type", "checkbox");
                //btnDetalles.ID = "btnSelecSec_" + Sector.id + "_" + Sector.descripcion;
                ////btnDetalles.Text = "<span><i class='fa fa-check'></i></span>";
                //celAccion.Controls.Add(btnDetalles);

                ////agrego fila a tabla


                //celAccion.Width = Unit.Percentage(5);
                //celAccion.Attributes.Add("style", "padding-bottom: 1px !important;text-align:right");
                //tr.Cells.Add(celAccion);




                // Crear la celda para la acción (radio button)
                TableCell cellRb = new TableCell();
                HtmlGenericControl radioButton = new HtmlGenericControl("input");
                radioButton.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs");
                radioButton.Attributes.Add("type", "radio");
                radioButton.Attributes.Add("style", "width:100%; height:100%");
                radioButton.Attributes.Add("name", "SectorSelection"); // Grupo de radio buttons
                radioButton.ID = "btnSelecSec_" + Sector.id + "_" + Sector.descripcion;
                cellRb.Controls.Add(radioButton);


                cellRb.Width = Unit.Percentage(5);
                cellRb.Attributes.Add("style", "");
                tr.Cells.Add(cellRb);

                phSectores.Controls.Add(tr);


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

        public void ObtenerPresentaciones()
        {
            try
            {
                ControladorPresentacion controladorPresentacion = new ControladorPresentacion();
                var presentaciones = controladorPresentacion.ObtenerTodosPresentaciones();

                if (presentaciones.Count > 0)
                {
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

        public void ObtenerMarcas()
        {
            try
            {
                ControladorMarca controladorMarca = new ControladorMarca();
                var marcas = controladorMarca.ObtenerTodasMarcas();

                if (marcas.Count > 0)
                {
                    foreach (var marca in marcas)
                    {
                        CargarMarcasPH(marca);
                    }
                }
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
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = presentacion.descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = presentacion.cantidad.ToString();
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Right;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celCantidad);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                //HtmlGenericControl cbxAgregar = new HtmlGenericControl("input");
                //cbxAgregar.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs pull-right");
                //cbxAgregar.Attributes.Add("type", "checkbox");
                ////cbxAgregar.Attributes.Add("value", "1");
                //cbxAgregar.ID = "btnSelecPres_" + presentacion.id + " - " + presentacion.descripcion;
                //celAccion.Controls.Add(cbxAgregar);

                HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Attributes.Add("onclick", "agregarPresentaciones()");
                btnDetalles.Attributes.Add("type", "checkbox");
                btnDetalles.ID = "btnPresentacion_" + presentacion.id.ToString();


                celAccion.Controls.Add(btnDetalles);

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celAccion);

                phPresentaciones.Controls.Add(tr);

            }
            catch (Exception ex)
            {

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.hiddenReceta.Value != "")
                //{
                //    EditarReceta();
                //}
                //else
                //{
                GuardarReceta();
                //}

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
                celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celFamilia = new TableCell();
                celFamilia.Text = familia;
                celFamilia.VerticalAlign = VerticalAlign.Middle;
                celFamilia.HorizontalAlign = HorizontalAlign.Left;
                celFamilia.Width = Unit.Percentage(5);
                tr.Cells.Add(celFamilia);
                TableCell celAccion = new TableCell();



                HtmlGenericControl btnDetalles = new HtmlGenericControl("input");
                btnDetalles.Attributes.Add("class", "presentacion radio btn btn-primary btn-xs");
                btnDetalles.Attributes.Add("onclick", "agregarCategoria(this.id); return false;");
                btnDetalles.Attributes.Add("type", "checkbox");
                btnDetalles.ID = "btnSelecProd_" + categoria.id + "_" + categoria.descripcion;
                celAccion.Controls.Add(btnDetalles);

                //agrego fila a tabla
                celAccion.Width = Unit.Percentage(5);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;text-align:right");
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
        //private void CargarUnidadesMedida()
        //{
        //    try
        //    {

        //        ControladorUnidad controladorUnidad = new ControladorUnidad();
        //        this.ListUnidadMedida.DataSource = controladorUnidad.ObtenerTodosUnidades();
        //        this.ListUnidadMedida.DataValueField = "id";
        //        this.ListUnidadMedida.DataTextField = "descripcion";
        //        this.ListUnidadMedida.DataBind();
        //        ListUnidadMedida.Items.Insert(0, new ListItem("Seleccione", "-1"));



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private void CargarAtributosReceta()
        //{
        //    try
        //    {
        //        var Receta = controladorReceta.ObtenerRecetaId(this.idReceta);
        //        if (Receta != null)
        //        {
        //            idCategoria.Value = Receta.Categorias.id.ToString();
        //            txtDescripcionCategoria.Text = Receta.Categorias.id + " - " + Receta.Categorias.descripcion;
        //            string descripcionAtributos = "";
        //            if (Receta.Recetas_Atributo1 != null)
        //            {
        //                ControladorAtributo controladorAtributo = new ControladorAtributo();
        //                foreach (Recetas_Atributo item in Receta.Recetas_Atributo1)
        //                {

        //                    Tecnocuisine_API.Entitys.Atributos atributo = controladorAtributo.ObtenerAtributoById((int)item.idAtributo);
        //                    if (descripcionAtributos == "")
        //                    {
        //                        descripcionAtributos = atributo.id + " - " + atributo.descripcion;
        //                        idAtributo.Value = atributo.id.ToString();
        //                    }

        //                    else
        //                    {
        //                        descripcionAtributos += " , " + atributo.id + " - " + atributo.descripcion;
        //                        idAtributo.Value += "," + atributo.id.ToString();
        //                    }
        //                }
        //            }
        //            txtDescripcionAtributo.Text = descripcionAtributos;
        //            btnAtributos.Attributes.Remove("disabled");
        //            hiddenidReceta.Value = this.idReceta.ToString();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "cargarCbx();", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "openModalAtributos();", true);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public void ObtenerProductos()
        {
            try
            {
                ControladorProducto controladorProducto = new ControladorProducto();
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

        public void ObtenerProductosbyIdReceta(int IdReceta)
        {
            try
            {

                List<Tecnocuisine_API.Entitys.Productos> productos = controladorReceta.obteneProductosPadres(IdReceta);
                //var productos = controladorProducto.ObtenerTodosProductos();

                if (productos.Count > 0)
                {
                    //CargarProductosOptions(productos);

                    foreach (var item in productos)
                    {
                        CargarProductosPH2(item);
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
                    builder.Append(String.Format("<option value='{0}' id='c_p_" + prod.id + "_" + prod.descripcion + "_" + UnidadMedida + "_" + prod.costo.ToString().Replace(',', '.') + "'>", prod.id + " - " + prod.descripcion));
                }

                //for (int i = 0; i < table.Rows.Count; i++)
                //    builder.Append(String.Format("<option value='{0}'>", table.Rows[i][0]));
                ListaNombreProd.InnerHtml += builder.ToString();

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

        //Esta funcion se encarga de precargar los productos de la receta 
        public void CargarProductosPH2(Tecnocuisine_API.Entitys.Productos producto)
        {

            try
            {
                var prodRec = controladorReceta.obtenerProductos_Recetas2SectorProductivo(producto.id, idReceta);
                //fila
                TableRow tr = new TableRow();
                tr.ID = "Producto_" + producto.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = producto.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = producto.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = prodRec.FirstOrDefault().cantidad.ToString().Replace(',', '.');
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCantidad);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(prodRec.FirstOrDefault().Productos.unidadMedida).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celUM);

                TableCell celCosto = new TableCell();
                celCosto.Text = prodRec.FirstOrDefault().Productos.costo.ToString("N", culture);
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                TableCell celCostoTotal = new TableCell();
                celCostoTotal.Text = (prodRec.FirstOrDefault().Productos.costo * prodRec.FirstOrDefault().cantidad).ToString("N", culture);
                celCostoTotal.VerticalAlign = VerticalAlign.Middle;
                celCostoTotal.HorizontalAlign = HorizontalAlign.Left;
                celCostoTotal.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoTotal);


                TableCell celSectorProductivo = new TableCell();

                if (!(prodRec.FirstOrDefault().SectorProductivo.descripcion is null || prodRec.FirstOrDefault().SectorProductivo.descripcion == ""))
                {

                    celSectorProductivo.Text = prodRec.FirstOrDefault().SectorProductivo.descripcion;
                }

                else
                {
                    celSectorProductivo.Text = " ";
                }

                celSectorProductivo.VerticalAlign = VerticalAlign.Middle;
                celSectorProductivo.HorizontalAlign = HorizontalAlign.Left;
                celSectorProductivo.Attributes.Add("style", "padding-bottom: 1px !important; text-align: left;");
                tr.Cells.Add(celSectorProductivo);


                TableCell celTiempo = new TableCell();

                if (!(prodRec.FirstOrDefault().Tiempo is null))
                {

                    celTiempo.Text = prodRec.FirstOrDefault().Tiempo.ToString();
                }

                else
                {
                    celTiempo.Text = " ";
                }

                celTiempo.VerticalAlign = VerticalAlign.Middle;
                celTiempo.HorizontalAlign = HorizontalAlign.Left;
                celTiempo.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celTiempo);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();

                if (bloqueados != 1)
                {
                    LinkButton btnDetalles = new LinkButton();
                    btnDetalles.CssClass = "btn btn-xs";
                    btnDetalles.Attributes.Add("data-toggle", "tooltip");

                    // Cambiar el color del icono a rojo
                    btnDetalles.Text = "<span><i style=\"color: red\" class='fa fa-trash'></i></span>";

                    btnDetalles.Attributes.Add("class", "btn btn-xs");
                    btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%; background-color: transparent;");
                    btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Producto_" + producto.id.ToString() + "');");
                    celAccion.Controls.Add(btnDetalles);

                    celAccion.Width = Unit.Percentage(25);
                    // Alinear contenido a la izquierda
                    celAccion.Attributes.Add("style", "text-align: left");
                    tr.Cells.Add(celAccion);



                }

                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", " text-align: center");
                tr.Cells.Add(celAccion);

                phProductos.Controls.Add(tr);

                idProductosRecetas.Value += producto.id.ToString() + " ,Producto," + prodRec.FirstOrDefault().cantidad.ToString().Replace(',', '.') + ", ContentPlaceHolder1_Producto_" + producto.id.ToString() + "," + "idSectorProductivo_" + prodRec.FirstOrDefault().SectorProductivo.id + "," + "Tiempo_" + prodRec.FirstOrDefault().Tiempo + ";";

            }
            catch (Exception ex)
            {

            }

        }
        public void ObtenerRecetas()
        {
            try
            {
                var Recetas = controladorReceta.ObtenerTodosRecetas();

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

        [WebMethod]
        public static string GetIngredientes(int id)
        {
            try
            {
                ControladorReceta controladorReceta = new ControladorReceta();
                string ingredientes = controladorReceta.obtenerIngredientes(id);
                string[] ingrediente = ingredientes.Split(';');
                string ingredienteReturn = "";
                foreach (string s in ingrediente)
                {
                    if (s != "")
                    {
                        int idIngrediente = Convert.ToInt32(s.Split(',')[0]);
                        string tipo = s.Split(',')[1];


                        if (tipo == "Producto")
                        {
                            ControladorProducto controladorProducto = new ControladorProducto();
                            ingredienteReturn += s + "," + controladorProducto.ObtenerProductoId(idIngrediente).descripcion + ";";
                        }
                        else
                        {
                            ingredienteReturn += s + "," + controladorReceta.ObtenerRecetaId(idIngrediente).descripcion + ";";
                        }
                    }
                }
                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(ingredienteReturn);
                return resultadoJSON;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        [WebMethod]
        public static string GetDescripcionReceta(int id)
        {
            ControladorReceta controladorReceta = new ControladorReceta();
            string receta = controladorReceta.ObtenerRecetaId(id).descripcion;

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(receta);
            return resultadoJSON;
        }







        private void agregarArticulo(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Articulos.aspx?a=3&r=" + id[1]);
            }
            catch (Exception)
            {

            }

        }

        private void infoReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("RecetasDetalle.aspx?id=" + id[1]);
            }
            catch (Exception Ex)
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

        //Esta funcion se encarga de precargar las recetas que se usan como ingredientes para la receta
        public void CargarRecetasPHModal2(Tecnocuisine_API.Entitys.Recetas_Receta Receta)
        {

            try
            {

                //fila
                var RecetaingredienteI = controladorReceta.ObtenerRecetaId(Receta.idRecetaIngrediente);

                int idSectorProductivo = Convert.ToInt32(Receta.idSectorProductivo);
                Tecnocuisine_API.Entitys.SectorProductivo secProductivo = controladorReceta.obterner_sectorProductivoByIdsectorProductivo(idSectorProductivo);

                HFRecetas.Value += RecetaingredienteI.id + ",";
                TableRow tr = new TableRow();
                tr.ID = "Receta_" + RecetaingredienteI.id.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = Receta.Recetas.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = "<div id=\"jstree" + RecetaingredienteI.id + "\"> <ul><li id='RecetaLI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + RecetaingredienteI.descripcion + ObtenerrecetaString(Receta.idRecetaIngrediente, 1, 0) + "</li></ul></div>";
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                //celDescripcion.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = "<div id=\"jstree_C" + RecetaingredienteI.id + "\"> <ul><li id='RecetaC_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + Receta.cantidad.ToString("N", culture) + ObtenerrecetaString(Receta.idRecetaIngrediente, 2, Receta.cantidad) + "</li></ul></div>"; ;
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                //celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCantidad);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                UnidadMedida = cu.ObtenerUnidadId(Receta.Recetas.UnidadMedida.Value).descripcion;

                TableCell celUM = new TableCell();
                celUM.Text = "<div id=\"jstree_UM" + RecetaingredienteI.id + "\"> <ul><li id='RecetaUM_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + UnidadMedida + ObtenerrecetaString(Receta.idRecetaIngrediente, 3, 0) + "</li></ul></div>";
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                //celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);

                TableCell celCosto = new TableCell();
                celCosto.Text = "<div id=\"jstree_CS" + RecetaingredienteI.id + "\"> <ul><li id='RecetaCS_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + Receta.Recetas.Costo.Value.ToString("N", culture) + ObtenerrecetaString(Receta.idRecetaIngrediente, 4, 0) + "</li></ul></div>";
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                //celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                TableCell celCostoTotal = new TableCell();
                celCostoTotal.Text = "<div id=\"jstree_CST" + RecetaingredienteI.id + "\"> <ul><li id='RecetaCST_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + (Receta.Recetas.Costo.Value * Receta.cantidad).ToString("N", culture) + ObtenerrecetaString(Receta.idRecetaIngrediente, 5, 0) + "</li></ul></div>";
                celCostoTotal.VerticalAlign = VerticalAlign.Middle;
                celCostoTotal.HorizontalAlign = HorizontalAlign.Left;
                //celCostoTotal.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCostoTotal);


                TableCell celSectorProductivo = new TableCell();


                if (secProductivo.descripcion is null || secProductivo.descripcion == "" || secProductivo is null)
                {
                    celSectorProductivo.Text = " ";
                }

                else
                {
                    celSectorProductivo.Text = secProductivo.descripcion;
                }
                celSectorProductivo.VerticalAlign = VerticalAlign.Middle;
                celSectorProductivo.HorizontalAlign = HorizontalAlign.Left;
                celSectorProductivo.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celSectorProductivo);


                TableCell celTiempo = new TableCell();

                if (!(Receta.Tiempo is null))
                {

                    celTiempo.Text = Receta.Tiempo.ToString();
                }

                else
                {
                    celTiempo.Text = " ";
                }

                celTiempo.VerticalAlign = VerticalAlign.Middle;
                celTiempo.HorizontalAlign = HorizontalAlign.Right;
                celTiempo.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celTiempo);


                //agrego fila a tabla
                TableCell celAccion = new TableCell();


                if (bloqueados != 1)
                {
                    LinkButton btnDetalles = new LinkButton();
                    btnDetalles.CssClass = "btn btn-xs";
                    btnDetalles.Attributes.Add("data-toggle", "tooltip");

                    // Cambiar el color del icono a rojo
                    btnDetalles.Text = "<span><i style=\"color: red\" class='fa fa-trash'></i></span>";

                    btnDetalles.Attributes.Add("class", "btn  btn-xs");
                    btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                    // btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Receta_" + Receta.Recetas.id.ToString() + "');");
                    btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Receta_" + RecetaingredienteI.id + "');");
                    celAccion.Controls.Add(btnDetalles);

                    celAccion.Width = Unit.Percentage(25);
                    celAccion.Attributes.Add("style", "text-align: center");
                    tr.Cells.Add(celAccion);

                }
                phProductos.Controls.Add(tr);

                //idProductosRecetas.Value += RecetaingredienteI.id.ToString() + " ,Receta," + Receta.cantidad.ToString().Replace(',', '.') + ", ContentPlaceHolder1_Receta_" + RecetaingredienteI.id.ToString() + ";";

                idProductosRecetas.Value += RecetaingredienteI.id.ToString() + " ,Receta," + Receta.cantidad.ToString().Replace(',', '.') + ", ContentPlaceHolder1_Receta_" + RecetaingredienteI.id.ToString() + "idSectorProductivoRecetas_recetas_" + Receta.idSectorProductivo + "," + "Tiempo_" + Receta.Tiempo + ";";


            }
            catch (Exception ex)
            {

            }

        }

        string ObtenerrecetaString(int idReceta, int dato, decimal x)
        {
            try
            {
                ControladorReceta controlador = new ControladorReceta();
                ControladorUnidad cu = new ControladorUnidad();


                var listaRP = controlador.ObtenerProductosByReceta(idReceta); //recetas_productos
                string lista = "<ul>";
                var listaRR = controlador.obtenerRecetasbyReceta(idReceta); //recetas_recetas
                var receta = controlador.ObtenerRecetaId(idReceta);              //receta
                if (listaRR != null && listaRR.Count > 0)
                {
                    foreach (var rr in listaRR)
                    {
                        lista += "<li>";
                        switch (dato)
                        {
                            case 1: //descripcion
                                lista += rr.Recetas.descripcion;
                                break;
                            case 2: //cantidad
                                lista += rr.cantidad.ToString("#,##0.000", culture);
                                break;
                            case 3: //Unidad
                                lista += cu.ObtenerUnidadId(rr.Recetas.UnidadMedida.Value).descripcion;
                                break;
                            case 4: //Costo
                                lista += rr.Recetas.Costo.Value.ToString("N", culture);
                                break;
                            case 5: //Costo total
                                lista += decimal.Round((rr.Recetas.Costo.Value * rr.cantidad), 2).ToString("N", culture);
                                break;
                        };


                        lista += "<ul>";
                        //var listaProdAux = controlador.ObtenerProductosByReceta(rr.Recetas.id);
                        string type = ObtenerrecetaString(rr.idRecetaIngrediente, dato, 0);

                        type = "" + type.Substring(4);
                        type = type.Remove(type.Length - 5);
                        lista += type;
                        lista += "</ul></li>";

                    }
                }
                //productos
                foreach (var RP in listaRP)
                {
                    if (RP != null)
                    {
                        lista += "<li data-jstree='{\"type\":\"html\"}'>";
                        switch (dato)
                        {
                            case 1: //descripcion
                                lista += +RP.Productos.id + "-" + RP.Productos.descripcion;
                                break;
                            case 2: //cantidad
                                decimal PR_Cant = x / receta.peso;
                                decimal PesoT = receta.peso * PR_Cant;
                                decimal PR_cantReceta = RP.cantidad / receta.peso;
                                decimal auxCantReceta = PesoT * PR_cantReceta;
                                lista += auxCantReceta.ToString("#,##0.000", culture);
                                break;
                            case 3: //Unidad
                                lista += cu.ObtenerUnidadId(RP.Productos.unidadMedida).descripcion;
                                break;
                            case 4: //Costo
                                lista += RP.Productos.costo.ToString("N", culture);
                                break;
                            case 5: //Costo total
                                lista += decimal.Round((RP.Productos.costo * RP.cantidad), 2).ToString("N", culture);
                                break;
                        };


                        lista += "</li>";

                    }
                }

                lista += "</ul>";
                //datos = datos.Remove(datos.LastIndexOf(",")).TrimEnd();


                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
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
                    div.Attributes.Add("class", "dd-handle editable");
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




                    //HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                    //btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                    ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                    ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                    //btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                    //btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                    //btnAgregar.Attributes.Add("data-action", "addAttribute");
                    //div.Controls.Add(btnAgregar);

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
                        div.Attributes.Add("class", "dd-handle editable");
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



                        //HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        //btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right");
                        ////btnDetalles.Attributes.Add("data-toggle", "tooltip");
                        ////btnDetalles.Attributes.Add("title data-original-title", "Editar");
                        //btnAgregar.ID = "btnSelec_" + item.id + " - " + item.descripcion;
                        //btnAgregar.InnerHtml = "<span><i class='fa fa-check'></i></span>";
                        //btnAgregar.Attributes.Add("data-action", "addAttribute");
                        //div.Controls.Add(btnAgregar);



                        cargarNestedListAtributosHijos(item.id, liHijo, checkboxIndex);
                    }
                }

            }

            catch (Exception)
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
                        div.Attributes.Add("class", "dd-handle editable");
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

            }
        }


        protected void editarReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Recetas.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }


        #region ABM
        public void GuardarReceta()
        {
            try
            {
                Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();

                Receta.descripcion = txtDescripcionReceta.Text;
                Receta.estado = 1;
                Receta.ProductoFinal = CheckProductoFinal.Checked;
                //Receta.coeficiente = Convert.ToDecimal(hiddenCoeficiente.Value.Replace('.', ','));
                //Receta.peso = Convert.ToDecimal(hiddenPeso.Value.Replace('.', ','));
                if (txtRinde.Text == "")
                {
                    Receta.rinde = null;
                }
                else
                {
                    Receta.rinde = Convert.ToDecimal(txtRinde.Text.Replace('.', ','));
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

                int resultado = controladorReceta.AgregarReceta(Receta);

                if (resultado > 0)
                {
                    string[] items = idProductosRecetas.Value.Split(';');
                    int idProducto = 0;
                    foreach (var pr in items)
                    {
                        if (pr != "")
                        {
                            string[] producto = pr.Split(',');
                            if (producto[1] == "Producto")
                            {
                                Recetas_Producto productoNuevo = new Recetas_Producto();
                                productoNuevo.idReceta = resultado;
                                productoNuevo.idProducto = Convert.ToInt32(producto[0]);
                                idProducto = productoNuevo.idProducto;
                                productoNuevo.cantidad = Convert.ToInt32(producto[2]);
                                controladorReceta.AgregarReceta_Producto(productoNuevo);
                            }
                            else
                            {
                                Recetas_Receta recetaNueva = new Recetas_Receta();
                                recetaNueva.idReceta = resultado;
                                recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
                                recetaNueva.cantidad = Convert.ToInt32(producto[2]);
                                controladorReceta.AgregarReceta_Receta(recetaNueva);
                            }
                        }
                    }
                    if (controladorReceta.EsMonoProducto(resultado))
                    {
                        int categoria = controladorProducto.ObtenerProductoId(idProducto).categoria;
                        if (categoria > 0)
                        {
                            Tecnocuisine_API.Entitys.Recetas r = controladorReceta.ObtenerRecetaId(resultado);
                            r.categoria = categoria;
                            controladorReceta.EditarReceta(r);

                            List<Productos_Atributo> atributos = controladorAtributo.ObtenerAtributosByIdProducto(idProducto);
                            foreach (Productos_Atributo item in atributos)
                            {
                                Recetas_Atributo atributo = new Recetas_Atributo();
                                atributo.idAtributo = item.atributo;
                                atributo.idReceta = resultado;


                                controladorReceta.AgregarReceta_Atributo(atributo);
                            }

                        }
                    }
                    Response.Redirect("Recetas.aspx?m=1");

                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo agregar el Receta", "warning");
                }
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
                this.ddlUnidadMedida.DataSource = controladorUnidad.ObtenerTodosUnidades();
                this.ddlUnidadMedida.DataValueField = "id";
                this.ddlUnidadMedida.DataTextField = "descripcion";
                this.ddlUnidadMedida.DataBind();
                ddlUnidadMedida.Items.Insert(0, new ListItem("Unidad Medida", "-1"));



            }
            catch (Exception ex)
            {

            }
        }

        protected void EditarAtributoReceta(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Recetas.aspx?a=3&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        //public void EditarReceta()
        //{
        //    try
        //    {
        //        Tecnocuisine_API.Entitys.Recetas Receta = new Tecnocuisine_API.Entitys.Recetas();

        //        Receta.id = idReceta;
        //        Receta.descripcion = txtDescripcionReceta.Text;
        //        Receta.estado = 1;
        //        Receta.coeficiente = Convert.ToDecimal(hiddenCoeficiente.Value.Replace('.', ','));
        //        Receta.peso = Convert.ToDecimal(hiddenPeso.Value.Replace('.', ','));
        //        if (txtRinde.Text == "")
        //        {
        //            Receta.rinde = null;
        //        }
        //        else
        //        {
        //            Receta.rinde = Convert.ToDecimal(txtRinde.Text.Replace('.', ','));
        //        }
        //        if (txtDesperdicio.Text == "")
        //        {
        //            Receta.desperdicio = null;
        //            Receta.merma = null;
        //        }
        //        else
        //        {
        //            Receta.desperdicio = Convert.ToDecimal(txtDesperdicio.Text.Replace('.', ','));
        //            Receta.merma = Convert.ToDecimal(txtMerma.Text.Replace('.', ','));
        //        }

        //        int resultado = controladorReceta.EditarReceta(Receta);

        //        if (resultado > 0)
        //        {
        //            controladorReceta.EliminarIngredientes(Receta.id);
        //            string[] items = idProductosRecetas.Value.Split(';');
        //            foreach (var pr in items)
        //            {
        //                string[] producto = pr.Split(',');
        //                if (producto[1] == "Producto")
        //                {
        //                    Recetas_Producto productoNuevo = new Recetas_Producto();
        //                    productoNuevo.idReceta = Receta.id;
        //                    productoNuevo.idProducto = Convert.ToInt32(producto[0]);
        //                    productoNuevo.cantidad = Convert.ToInt32(producto[2]);
        //                    controladorReceta.AgregarReceta_Producto(productoNuevo);
        //                }
        //                else
        //                {
        //                    Recetas_Receta recetaNueva = new Recetas_Receta();
        //                    recetaNueva.idReceta = Receta.id;
        //                    recetaNueva.idRecetaIngrediente = Convert.ToInt32(producto[0]);
        //                    recetaNueva.cantidad = Convert.ToInt32(producto[2]);
        //                    controladorReceta.AgregarReceta_Receta(recetaNueva);
        //                }
        //            }
        //            Response.Redirect("Recetas.aspx?m=2");

        //        }
        //        else
        //        {
        //            this.m.ShowToastr(this.Page, "No se pudo editar la Receta", "warning");
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idReceta = Convert.ToInt32(this.hiddenID.Value);
                if (controladorReceta.verificarRelacionRecetaReceta(idReceta))
                {
                    int resultado = controladorReceta.EliminarReceta(idReceta);

                    if (resultado > 0)
                    {
                        Response.Redirect("Recetas.aspx?m=3");
                    }
                    else
                    {
                        this.m.ShowToastr(this.Page, "No se pudo eliminar el Receta", "warning");
                    }
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la receta porque tiene una receta relacionada", "Alerta", "warning");

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion







        protected void btnAtributos_Click(object sender, EventArgs e)
        {


        }


        [WebMethod]
        public static string GetSubAtributos(int id)
        {
            ControladorReceta controlador = new ControladorReceta();
            Tecnocuisine_API.Entitys.Recetas r = controlador.ObtenerRecetaId(id);
            string resultado = "";
            if (r.Recetas_Atributo.Count > 0)
                resultado = "true," + id.ToString();
            else
                resultado = "false," + id.ToString();
            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(resultado);
            return resultadoJSON;
        }

        [WebMethod]
        public static string GetProd(string d)
        {
            try
            {
                List<string> productosDescripcion = new List<string>();
                ControladorProducto c = new ControladorProducto();

                productosDescripcion = c.ObtenerTodosProductosDescripciones(d);
                JavaScriptSerializer javaScript = new JavaScriptSerializer();
                javaScript.MaxJsonLength = 5000000;
                string resultadoJSON = javaScript.Serialize(productosDescripcion);
                return resultadoJSON;

            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}