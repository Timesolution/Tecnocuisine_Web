using Gestion_Api.Entitys;
using Gestion_Api.Modelo;
using Microsoft.ReportingServices.Interfaces;
using PdfSharp.UniversalAccessibility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;
using static PdfSharp.Capabilities;


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
        static CultureInfo cultureSt = CultureInfo.CreateSpecificCulture("en-US");

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

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
                CargarTipos();

                //CargarTablaIngredientes();

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

        private void CargarTipos()
        {
            ControladorTiposDeReceta cTiposDeReceta = new ControladorTiposDeReceta();
            this.ddlTipoReceta.DataSource = cTiposDeReceta.ObtenerTodosTiposDeReceta();
            this.ddlTipoReceta.DataValueField = "id";
            this.ddlTipoReceta.DataTextField = "tipo";
            this.ddlTipoReceta.DataBind();
            ddlTipoReceta.Items.Insert(0, new ListItem("Tipo de Receta", "-1"));
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


                this.ddlSectorProductivo.DataSource = sortedTable;
                this.ddlSectorProductivo.DataValueField = "id";
                this.ddlSectorProductivo.DataTextField = "descripcion";
                this.ddlSectorProductivo.DataBind();
                this.ddlSectorProductivo.SelectedValue = "-1";
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
                ddlRubros.Attributes.Add("disabled", "disabled");
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
                ddlSectorProductivo.Attributes.Add("disabled", "disabled");
                txtDescripcionProductos.Attributes.Add("disabled", "disabled");
                ddlSector.Attributes.Add("disabled", "disabled");
                TiempoDePreparacion.Attributes.Add("disabled", "disabled");

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
                            ddlSectorProductivo.SelectedValue = sec.id.ToString();
                        //txtSector.Text += sec.id + " - " + sec.descripcion /*+ ", "*/;
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

                    // Si tiene un tipo de receta inactivo, agregarlo al select
                    TiposDeReceta tipo = new ControladorTiposDeReceta().ObtenerTipoDeRecetaById((int)Receta.Tipo);
                    if (!tipo.estado)
                        ddlTipoReceta.Items.Insert(1, new ListItem(tipo.tipo, tipo.id.ToString()));

                    ddlTipoReceta.SelectedValue = Receta.Tipo.ToString();
                    ddlUnidadMedida.SelectedValue = Receta.UnidadMedida.ToString();
                    ddlRubros.SelectedValue = Receta.idRubro.ToString();
                    txtKgBrutTotal.Text = Receta.peso.ToString().Replace(',', '.');
                    txtKgxPorcion.Text = Receta.PesoU.Value.ToString().Replace(',', '.');
                    txtRinde.Text = Receta.rinde.Value.ToString().Replace(',', '.');
                    txtCostoTotal.Text = Receta.Costo.Value.ToString("N", culture);
                    txtCostoxPorcion.Text = Receta.CostoU.Value.ToString("N", culture);
                    txtPrVenta.Text = Receta.PrVenta.Value.ToString("N", culture);
                    txtPFoodCost.Text = Receta.PorcFoodCost.Value.ToString("N", culture) + "%";
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

        [WebMethod]
        public static string GetImagenByIdReceta(string idReceta)
        {
            string folderPath = HttpContext.Current.Server.MapPath("~/Img/Recetas/"); // Ruta de la carpeta en el servidor
            string relativeFolderPath = "/Img/Recetas/"; // Ruta relativa para devolver al cliente


            if (!string.IsNullOrEmpty(idReceta))
            {
                // Busca el archivo que contenga el nombre dado
                var files = Directory.GetFiles(folderPath)
                    .Where(f => Path.GetFileNameWithoutExtension(f) == idReceta); // Filtra por nombre

                // Si se encuentra algún archivo, toma el primero
                if (files.Any())
                {
                    var file = files.First(); // Obtiene la primera coincidencia
                    string fileName = Path.GetFileName(file); // Obtiene el nombre del archivo con extensión
                    return relativeFolderPath + fileName; // Devuelve la ruta relativa (para ser usada en el navegador)
                }
            }

            return "";
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
                var recetas_receta = controladorReceta.obtenerRecetasbyReceta(idReceta); //recetas_recetas

                if (recetas_receta.Count > 0)
                {
                    //CargarProductosOptions(productos);

                    foreach (var rr in recetas_receta)
                    {
                        //CargarRecetasPHModal2(item);

                        // Devuelve una cadena html con la receta inicial
                        var recetaPadre = GenerarFilaReceta_Edit(rr, true);
                        phProductos.Controls.Add(recetaPadre);

                        // Generar y agregar las filas hijas (subrecetas y productos)
                        GenerarFilasHijas_Edit(rr.idRecetaIngrediente, 1, rr.cantidad);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Es el metodo principal para precargar los ingredientes de tipo receta
        /// </summary>
        /// <param name="Receta_Receta"></param>
        /// <param name="esReceta"></param>
        /// <returns></returns>
        private TableRow GenerarFilaReceta_Edit(Tecnocuisine_API.Entitys.Recetas_Receta Receta_Receta, bool esReceta)
        {     
            var receta = controladorReceta.ObtenerRecetaId(Receta_Receta.idRecetaIngrediente);

            // Setear valores
            int id = receta.id;
            string descripcion = receta.descripcion;
            string cantidad = Receta_Receta.cantidad.ToString("N3", culture);
            string unidad = new ControladorUnidad().ObtenerUnidadId(receta.UnidadMedida.Value).abreviacion;
            string costo = receta.CostoU.Value.ToString("C", culture);
            string tiempo = Receta_Receta.Tiempo?.ToString() ?? "0";

            var costototal = (receta.CostoU??0) * Receta_Receta.cantidad;
            string cTotal = costototal.ToString("C", culture);

            string sector = "";

            if (Receta_Receta.idSectorProductivo != null)
                sector = controladorReceta.obterner_sectorProductivoByIdsectorProductivo((int)Receta_Receta.idSectorProductivo).descripcion;



            // Agregar receta a la cadena que se enviara al servidor para guardarla
            idProductosRecetas.Value += id + " ,Receta," + cantidad + ", ContentPlaceHolder1_Receta_" + id + ",idSectorProductivoRecetas_recetas_" + Receta_Receta.idSectorProductivo + "," + "Tiempo_" + tiempo + "," + "Factor_" + Receta_Receta.Factor.ToString().Replace(',', '.') + ";";

            // Crear la fila principal (padre)
            TableRow parentRow = new TableRow();
            parentRow.ID = "Receta_" + id.ToString();
            parentRow.CssClass = "parent"; // Clases para personalizar con CSS
            parentRow.Attributes.Add("onclick", "toggleChildren(this)");
            parentRow.Attributes.Add("style", "cursor: pointer;"); // Cambia el cursor a pointer para indicar que es clicable

            // Crear las celdas y agregarlas a la fila
            parentRow.Cells.Add(GenerarCelda(id.ToString())); // Espacio vacío para los hijos
            parentRow.Cells.Add(GenerarCelda("<b>+</b> &nbsp;" + descripcion));
            parentRow.Cells.Add(GenerarCelda(cantidad, "text-align:right"));
            parentRow.Cells.Add(GenerarCelda(unidad));
            parentRow.Cells.Add(GenerarCelda(costo, "text-align:right"));

            var cellTotal = GenerarCelda(cTotal, "text-align:right");
            cellTotal.CssClass = "total-ingrediente-receta";
            parentRow.Cells.Add(cellTotal);

            parentRow.Cells.Add(GenerarCelda(sector));
            parentRow.Cells.Add(GenerarCelda(tiempo, "text-align:right"));

            TableCell celAccion = new TableCell();
            // Verificar si la vista no esta en estado visualizacion
            if (bloqueados != 1)
            {
                LinkButton btnDetalles = new LinkButton();
                btnDetalles.CssClass = "btn btn-xs";
                btnDetalles.Attributes.Add("data-toggle", "tooltip");
                btnDetalles.Text = "<span><i style=\"color: darkred\" class='fa fa-trash'></i></span>";
                btnDetalles.Attributes.Add("class", "btn  btn-xs");
                btnDetalles.Attributes.Add("style", "padding: 0% 5% 2% 5.5%;background-color: transparent;");
                // btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Receta_" + Receta.Recetas.id.ToString() + "');");
                btnDetalles.Attributes.Add("onclick", "borrarProd('ContentPlaceHolder1_Receta_" + id + "');");
                
                celAccion.Controls.Add(btnDetalles);
                celAccion.Width = Unit.Percentage(25);
                celAccion.Attributes.Add("style", "text-align: center");

                parentRow.Cells.Add(celAccion);
            }

            return parentRow;
        }

        /// <summary>
        /// Precarga todas las filas internas de una receta dada
        /// </summary>
        /// <param name="idReceta"></param>
        /// <param name="nivel"></param>
        private void GenerarFilasHijas_Edit(int idReceta, int nivel, decimal cantidadPadre)
        {
            var receta = controladorReceta.ObtenerRecetaId(idReceta);
            var rindePadre = receta.rinde ?? 1;

            // Si es receta, ver si tiene más recetas o productos y dibujarlos anidados
            var recetasInternas = controladorReceta.obtenerRecetasbyReceta(idReceta); // recetas_recetas
            foreach (var ri in recetasInternas)
            {
                var recetaHijaDb = controladorReceta.ObtenerRecetaId(ri.idRecetaIngrediente);

                // Obtener valores de las celdas
                int id = recetaHijaDb.id;
                string descripcion = recetaHijaDb.descripcion;

                var cantidad = ((ri.cantidad * cantidadPadre) / rindePadre);
                string cantidadStr = cantidad.ToString("N3", culture);

                string unidad = new ControladorUnidad().ObtenerUnidadId(recetaHijaDb.UnidadMedida.Value).abreviacion;
                string costo = recetaHijaDb.CostoU.Value.ToString("C", culture);
                string tiempo = ri.Tiempo?.ToString() ?? "0";
                string sector = string.Empty;
                string cTotal;

                var costototal = (recetaHijaDb.CostoU ?? 0) * cantidad;
                cTotal = costototal.ToString("C", culture);


                if (ri.idSectorProductivo != null)
                    sector = controladorReceta.obterner_sectorProductivoByIdsectorProductivo((int)ri.idSectorProductivo).descripcion;


                // Crear fila hija
                // Crear las celdas para la fila hija
                string margen = string.Empty;
                for (int i = 0; i < nivel; i++)
                {
                    margen += "&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                TableRow childRow = new TableRow();
                childRow.CssClass = $"child children hidden nivel-{nivel}"; // Oculto inicialmente, con clase de nivel
                childRow.Attributes.Add("data-nivel", nivel.ToString());
                childRow.Attributes.Add("onclick", "toggleChildren(this)");
                childRow.Attributes.Add("style", "cursor: pointer;"); // Cambia el cursor a pointer para indicar que es clicable

                // Crear las celdas para la fila hija
                childRow.Cells.Add(GenerarCelda(id.ToString())); // Indicador de que es una fila hija
                childRow.Cells.Add(GenerarCelda(margen + "<b>+</b> &nbsp;" + descripcion)); // Descripción del hijo
                childRow.Cells.Add(GenerarCelda(cantidadStr, "text-align:right"));
                childRow.Cells.Add(GenerarCelda(unidad));
                childRow.Cells.Add(GenerarCelda(costo, "text-align:right"));

                var cellTotal = GenerarCelda(cTotal, "text-align:right");
                cellTotal.CssClass = "total-ingrediente-receta";
                childRow.Cells.Add(cellTotal);

                childRow.Cells.Add(GenerarCelda(sector));
                childRow.Cells.Add(GenerarCelda(tiempo, "text-align:right"));
                childRow.Cells.Add(GenerarCelda("")); // Última columna vacía

                // Agregar la fila hija justo después de la fila padre
                phProductos.Controls.Add(childRow);

                // Generar las sub-filas recursivamente (si hay más recetas dentro)
                GenerarFilasHijas_Edit(ri.idRecetaIngrediente, nivel + 1, cantidad); // Recursión aumentando el nivel
            }

            var productosInternos = controladorReceta.ObtenerProductosByReceta(idReceta); // recetas_productos
            foreach (var pi in productosInternos)
            {
                // Obtener valores de las celdas
                int id = pi.Productos.id;
                string descripcion = pi.Productos.descripcion;

                var cantidad = ((pi.cantidad * cantidadPadre) / rindePadre);
                string cantidadStr = cantidad.ToString("N3", cultureSt);

                string unidad = new ControladorUnidad().ObtenerUnidadId(pi.Productos.unidadMedida).abreviacion;
                string costo = pi.Productos.costo.ToString("C", culture);
                string tiempo = pi.Tiempo?.ToString() ?? "0";
                string sector = string.Empty;
                string cTotal;

                var costototal = pi.Productos.costo * cantidad;
                cTotal = costototal.ToString("C", culture);

                if (pi.idSectorProductivo != null)
                    sector = controladorReceta.obterner_sectorProductivoByIdsectorProductivo((int)pi.idSectorProductivo).descripcion;



                // Crear fila hija para productos
                TableRow childRow = new TableRow();
                childRow.CssClass = $"child children hidden nivel-{nivel}"; // Oculto inicialmente, con clase de nivel
                childRow.Attributes.Add("data-nivel", nivel.ToString());

                // Crear las celdas para la fila hija
                string margen = string.Empty;
                for (int i = 0; i < nivel; i++)
                {
                    margen += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    //margen += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                childRow.Cells.Add(GenerarCelda(id.ToString())); // Indicador de que es una fila hija
                childRow.Cells.Add(GenerarCelda(margen + descripcion)); // Descripción del hijo
                childRow.Cells.Add(GenerarCelda(cantidadStr, "text-align:right"));
                childRow.Cells.Add(GenerarCelda(unidad));
                childRow.Cells.Add(GenerarCelda(costo, "text-align:right"));

                var cellTotal = GenerarCelda(cTotal, "text-align:right");
                cellTotal.CssClass = "total-ingrediente-receta";
                childRow.Cells.Add(cellTotal);

                childRow.Cells.Add(GenerarCelda(sector));
                childRow.Cells.Add(GenerarCelda(tiempo, "text-align:right"));
                childRow.Cells.Add(GenerarCelda("")); // Última columna vacía

                // Agregar la fila hija justo después de la fila padre
                phProductos.Controls.Add(childRow);
            }
        }

        // Método para generar celdas
        private TableCell GenerarCelda(string texto, string style = "")
        {
            TableCell cell = new TableCell();
            cell.Text = texto;

            // Si se pasan estilos, se aplican a la celda
            if (!string.IsNullOrEmpty(style))
            {
                cell.Attributes.Add("style", style);
            }

            return cell;
        }


        /// <summary>
        /// Devuelve una cadena que representa un elemento <tr> HTML de una receta para la tabla de ingredientes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidad"></param>
        /// <param name="costo"></param>
        /// <param name="idSector"></param>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GenerarFilaReceta_Add(string id, string cantidad, string idSector, string tiempo)
        {
            ControladorReceta cReceta = new ControladorReceta();

            // Setear informacion
            var receta = cReceta.ObtenerRecetaId(Convert.ToInt32(id));
            string unidad = new ControladorUnidad().ObtenerUnidadId(receta.UnidadMedida.Value).abreviacion;

            // Se formatea la cantidad para poder usarlo en el calculo del costo total
            decimal cantidadDecimal = Convert.ToDecimal(cantidad, CultureInfo.InvariantCulture);
            decimal costoU = Convert.ToDecimal(receta.CostoU);
            var costototal = costoU * cantidadDecimal;
            string sector = string.Empty;

            if (idSector != "-1")
                sector = cReceta.obterner_sectorProductivoByIdsectorProductivo(Convert.ToInt32(idSector)).descripcion;


            // Generar HTML dinamico
            string rows = $@"
                <tr id='Receta_{id}' class='parent' onclick='toggleChildren(this)' style='cursor: pointer;'>
                    <td>{id}</td>
                    <td><b>+</b> &nbsp;{receta.descripcion}</td>
                    <td style='text-align:right'>{cantidad:N3}</td>
                    <td>{unidad}</td>
                    <td style='text-align:right'>{costoU:C}</td>
                    <td class='total-ingrediente-receta' style='text-align:right'>{costototal:C}</td>
                    <td>{sector}</td>
                    <td style='text-align:right'>{tiempo}</td>
                    <td style='text-align: center;'>
                        <a href='javascript:void(0);' class='btn btn-xs' data-toggle='tooltip' 
                style='padding: 0% 5% 2% 5.5%; background-color: transparent;' 
                onclick='borrarProd(""ContentPlaceHolder1_Receta_{id}_{costototal}"");'>
                <span><i style='color: darkred' class='fa fa-trash'></i></span>
            </a>
                    </td>
                </tr>";

            string filasHijas = string.Empty;
            rows += GenerarFilasHijas_Add(receta.id, 1, ref filasHijas, cantidadDecimal, receta.rinde??1);

            return rows;
        }

        public static string childRows = string.Empty;
        /// <summary>
        /// Devuelve una cadena que representa varios elementos <tr> HTML que son filas hijas de la receta con el id recibido
        /// </summary>
        /// <param name="idItem"></param>
        /// <param name="nivel"></param>
        /// <returns></returns>
        private static string GenerarFilasHijas_Add(int idItem, int nivel, ref string filasAcumuladas, decimal cantidadPadre, decimal rindePadre)
        {  
            ControladorReceta cReceta = new ControladorReceta();

            // Si es receta, ver si tiene más recetas o productos y dibujarlos anidados
            var recetasInternas = cReceta.obtenerRecetasbyReceta(idItem); // recetas_recetas
            foreach (var ri in recetasInternas)
            {
                var recetaHijaDb = cReceta.ObtenerRecetaId(ri.idRecetaIngrediente);

                // Obtener valores de las celdas
                int id = recetaHijaDb.id;
                string descripcion = recetaHijaDb.descripcion;
                var cantidad = ((ri.cantidad * cantidadPadre) / rindePadre);
                string cantidadStr = cantidad.ToString("N3", cultureSt);
                string unidad = new ControladorUnidad().ObtenerUnidadId(recetaHijaDb.UnidadMedida.Value).abreviacion;
                string costo = recetaHijaDb.CostoU.Value.ToString("C", cultureSt);
                string tiempo = ri.Tiempo?.ToString() ?? "0";
                string sector = string.Empty;
                string cTotal;

                var costototal = recetaHijaDb.CostoU != null
                     ? recetaHijaDb.CostoU.Value * cantidad
                     : 0;
                cTotal = costototal.ToString("C", cultureSt);

                if (ri.idSectorProductivo != null)
                    sector = cReceta.obterner_sectorProductivoByIdsectorProductivo((int)ri.idSectorProductivo).descripcion;

                //// Crear fila hija para productos
                string margen = string.Empty;
                for (int i = 0; i < nivel; i++)
                {
                    margen += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                filasAcumuladas += $@"
                <tr class='child children hidden nivel-{nivel}' data-nivel='{nivel}' onclick='toggleChildren(this)' style='cursor: pointer'> 
                    <td>{id}</td>
                    <td>{margen}<b>+</b> &nbsp;{descripcion}</td>
                    <td style='text-align:right'>{cantidadStr:N3}</td>
                    <td>{unidad}</td>
                    <td style='text-align:right'>{costo:C}</td>
                    <td class='total-ingrediente-receta' style='text-align:right'>{costototal:C}</td>
                    <td>{sector}</td>
                    <td style='text-align:right'>{tiempo}</td>
                    <td style='text-align: center;'>
                    </td>
                </tr>";

                // Generar las sub-filas recursivamente (si hay más recetas dentro)
                GenerarFilasHijas_Add(ri.idRecetaIngrediente, nivel + 1, ref filasAcumuladas, cantidad, recetaHijaDb.rinde??1); // Recursión aumentando el nivel
            }


            var productosInternos = cReceta.ObtenerProductosByReceta(idItem); // recetas_productos
            foreach (var pi in productosInternos)
            {
                // Obtener valores de las celdas
                int id = pi.Productos.id;
                string descripcion = pi.Productos.descripcion;

                var cantidad = ((pi.cantidad * cantidadPadre) / rindePadre);
                string cantidadStr = cantidad.ToString("N3", cultureSt);

                string unidad = new ControladorUnidad().ObtenerUnidadId(pi.Productos.unidadMedida).abreviacion;
                string costo = pi.Productos.costo.ToString("C", cultureSt);
                string tiempo = pi.Tiempo?.ToString() ?? "0";
                string sector = string.Empty;
                string cTotal;

                var costototal = pi.Productos.costo * cantidad;
                cTotal = costototal.ToString("C", cultureSt);

                if (pi.idSectorProductivo != null)
                    sector = cReceta.obterner_sectorProductivoByIdsectorProductivo((int)pi.idSectorProductivo).descripcion;


                //// Crear fila hija para productos

                string margen = string.Empty;
                for (int i = 0; i < nivel; i++)
                {
                    margen += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    //margen += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                filasAcumuladas += $@"
                <tr class='child children hidden nivel-{nivel}' data-nivel='{nivel}'> 
                    <td>{id}</td>
                    <td>{margen}{descripcion}</td>
                    <td style='text-align:right'>{cantidad:N3}</td>
                    <td>{unidad}</td>
                    <td style='text-align:right'>{costo:C}</td>
                    <td class='total-ingrediente-receta' style='text-align:right'>{costototal:C}</td>
                    <td>{sector}</td>
                    <td style='text-align:right'>{tiempo}</td>
                    <td style='text-align: center;'>
                    </td>
                </tr>";
            }

            return filasAcumuladas;
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

                btnDetalles.Text = "<span><i style=\"color:darkred\" class='fa fa-trash'></i></span>";
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
                    var unidad = cu.ObtenerUnidadId(prod.unidadMedida);
                    UnidadMedida = unidad.abreviacion;

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

                        var unidad = cu.ObtenerUnidadId(rec.UnidadMedida.Value);
                        UnidadMedida = unidad.abreviacion;
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
                var unidad = cu.ObtenerUnidadId(producto.unidadMedida);
                UnidadMedida = unidad.abreviacion;

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
                tr.CssClass = "parent";

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = producto.id.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", " text-align: right");

                tr.Cells.Add(celNumero);

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + producto.descripcion;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celDescripcion);

                TableCell celCantidad = new TableCell();
                celCantidad.Text = prodRec.FirstOrDefault().cantidad.ToString("N3").Replace(',', '.');
                celCantidad.VerticalAlign = VerticalAlign.Middle;
                celCantidad.HorizontalAlign = HorizontalAlign.Left;
                celCantidad.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCantidad);

                ControladorUnidad cu = new ControladorUnidad();
                string UnidadMedida = "";
                var unidad = cu.ObtenerUnidadId(prodRec.FirstOrDefault().Productos.unidadMedida);
                UnidadMedida = unidad.abreviacion;


                TableCell celUM = new TableCell();
                celUM.Text = UnidadMedida;
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(celUM);

                TableCell celFactor = new TableCell();
                celFactor.Text = prodRec.FirstOrDefault().Factor.ToString().Replace(',', '.');
                celFactor.VerticalAlign = VerticalAlign.Middle;
                celFactor.HorizontalAlign = HorizontalAlign.Left;
                //tr.Cells.Add(celFactor);

                TableCell celCantBruta = new TableCell();
                celCantBruta.Text = (prodRec.FirstOrDefault().cantidad * Convert.ToDecimal(prodRec.FirstOrDefault().Factor)).ToString("N3", culture);
                celCantBruta.VerticalAlign = VerticalAlign.Middle;
                celCantBruta.HorizontalAlign = HorizontalAlign.Left;
                //tr.Cells.Add(celCantBruta);

                TableCell celCosto = new TableCell();
                celCosto.Text = prodRec.FirstOrDefault().Productos.costo.ToString("C", culture);
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                TableCell celCostoTotal = new TableCell();
                decimal factor = prodRec.FirstOrDefault().Factor != null ? (decimal)prodRec.FirstOrDefault().Factor : 1;
                celCostoTotal.Text = (prodRec.FirstOrDefault().Productos.costo * prodRec.FirstOrDefault().cantidad * 1 /*factor*/).ToString("C", culture);
                celCostoTotal.VerticalAlign = VerticalAlign.Middle;
                celCostoTotal.HorizontalAlign = HorizontalAlign.Left;
                celCostoTotal.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                celCostoTotal.CssClass = "total-ingrediente-receta";
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
                    btnDetalles.Text = "<span><i style=\"color: darkred\" class='fa fa-trash'></i></span>";

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

                idProductosRecetas.Value += producto.id.ToString() + " ,Producto," + prodRec.FirstOrDefault().cantidad.ToString().Replace(',', '.') + ", ContentPlaceHolder1_Producto_" + producto.id.ToString() + "," + "idSectorProductivo_" + prodRec.FirstOrDefault().SectorProductivo.id + "," + "Tiempo_" + prodRec.FirstOrDefault().Tiempo + "," + "Factor_" + prodRec.FirstOrDefault().Factor.ToString().Replace(',', '.') + ";";

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
                var unidad = cu.ObtenerUnidadId(Receta.UnidadMedida.Value);
                UnidadMedida = unidad.abreviacion;

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
                var unidad = cu.ObtenerUnidadId(Receta.Recetas.UnidadMedida.Value);
                UnidadMedida = unidad.abreviacion;

                TableCell celUM = new TableCell();
                celUM.Text = "<div id=\"jstree_UM" + RecetaingredienteI.id + "\"> <ul><li id='RecetaUM_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + UnidadMedida + ObtenerrecetaString(Receta.idRecetaIngrediente, 3, 0) + "</li></ul></div>";
                celUM.VerticalAlign = VerticalAlign.Middle;
                celUM.HorizontalAlign = HorizontalAlign.Left;
                //celUM.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celUM);

                TableCell celFactor = new TableCell();
                celFactor.Text = Receta.Factor?.ToString().Replace(',', '.');
                celFactor.VerticalAlign = VerticalAlign.Middle;
                celFactor.HorizontalAlign = HorizontalAlign.Left;
                //tr.Cells.Add(celFactor);

                TableCell celCantBruta = new TableCell();
                celCantBruta.Text = (Receta.cantidad * Convert.ToDecimal(Receta.Factor)).ToString("N", culture);
                celCantBruta.VerticalAlign = VerticalAlign.Middle;
                celCantBruta.HorizontalAlign = HorizontalAlign.Left;
                //tr.Cells.Add(celCantBruta);

                TableCell celCosto = new TableCell();
                celCosto.Text = "<div id=\"jstree_CS" + RecetaingredienteI.id + "\"> <ul><li id='RecetaCS_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + Receta.Recetas.Costo.Value.ToString("N", culture) + ObtenerrecetaString(Receta.idRecetaIngrediente, 4, 0) + "</li></ul></div>";
                celCosto.VerticalAlign = VerticalAlign.Middle;
                celCosto.HorizontalAlign = HorizontalAlign.Left;
                //celCosto.Attributes.Add("style", "padding-bottom: 1px !important; text-align: right;");
                tr.Cells.Add(celCosto);

                TableCell celCostoTotal = new TableCell();
                decimal factor = Receta.Factor != null ? (decimal)Receta.Factor : 1;
                celCostoTotal.Text = "<div id=\"jstree_CST" + RecetaingredienteI.id + "\"> <ul><li id='RecetaCST_LI_" + RecetaingredienteI.id + "' class=\"jstree-open\">" + (Receta.Recetas.Costo.Value /* Receta.cantidad * factor*/).ToString("N", culture) + ObtenerrecetaString(Receta.idRecetaIngrediente, 5, 0) + "</li></ul></div>";
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
                    btnDetalles.Text = "<span><i style=\"color: darkred\" class='fa fa-trash'></i></span>";

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

                idProductosRecetas.Value += RecetaingredienteI.id.ToString() + " ,Receta," + Receta.cantidad.ToString().Replace(',', '.') + ", ContentPlaceHolder1_Receta_" + RecetaingredienteI.id.ToString() + ",idSectorProductivoRecetas_recetas_" + Receta.idSectorProductivo + "," + "Tiempo_" + Receta.Tiempo + "," + "Factor_" + Receta.Factor.ToString().Replace(',', '.') + ";";


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
                var unidades = controladorUnidad.ObtenerTodosUnidades();

                this.ddlUnidadMedida.DataSource = unidades;
                this.ddlUnidadMedida.DataValueField = "id";
                this.ddlUnidadMedida.DataTextField = "descripcion";
                this.ddlUnidadMedida.DataBind();
                ddlUnidadMedida.Items.Insert(0, new ListItem("Unidad Medida", "-1"));

                // Agregar a cada option un atributo que guarda la abreviacion (esto es para usarla en el texto de un campo cuando cambia la unidad)
                for (int i = 0; i < unidades.Count; i++)
                {
                    string abreviacion = unidades[i].abreviacion;

                    if (string.IsNullOrEmpty(abreviacion)) 
                        continue;

                    ddlUnidadMedida.Items[i+1].Attributes["data-abreviacion"] = abreviacion.ToUpper();
                }

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


        private void CargarTablaIngredientes()
        {
            //Obtener las recetas hijas de la receta visualizada
            var recetasHijas = controladorReceta.obtenerRecetasbyReceta(this.idReceta); //recetas_recetas
            var productos = controladorReceta.obteneProductosPadres(this.idReceta);


            // inicializar el HTML para la tabla
            productContainer.Text = "<table class='table table-bordered'><tbody>";

            foreach (var p in productos)
            {
                productContainer.Text += GenerarHTMLRecetaProducto(p.id, false);
            }
            foreach (var recetaHija in recetasHijas)
            {
                productContainer.Text += GenerarHTMLRecetaProducto(recetaHija.idRecetaIngrediente, true);
            }

            // Cerrar la tabla
            productContainer.Text += "</tbody></table>";
        }

        private string GenerarHTMLRecetaProducto(int idItem, bool esReceta, bool boldStyle = false)
        {
            string descripcion;

            if (esReceta)
            {
                var receta = controladorReceta.ObtenerRecetaId(idItem);
                descripcion = receta.descripcion;
            }
            else
            {
                var producto = controladorProducto.ObtenerProductoId(idItem);
                descripcion = producto.descripcion;
            }

            string onclick = esReceta ? "onclick='toggleChildren(event)'" : "";
            string pointer = esReceta ? "cursor:pointer" : "";
            string bold = boldStyle ? "font-weight:bold" : "";

            //string itemHTML = $@"
            //<div class='product' {onclick}' style='{bold}'>
            //    <p>{descripcion}</h3>
            //</div>
            //<div class='children hidden'>";

            // Crear la fila principal (padre) con la descripción
            string itemHTML = $@"
    <tr {onclick} style='{bold}; {pointer}'>
        <td>{descripcion}</td>
    </tr>";


            //        // Ahora agregamos la fila que contendrá los hijos, con un colspan para abarcar el ancho completo
            //        itemHTML += $@"
            //<tr class='children hidden'>
            //    <td colspan='100%'>
            //    ";



            // Si es receta, ver si dentro tiene mas recetas o productos y dibujarlos anidados
            if (esReceta)
            {
                // Abrir la fila hija con colspan
                itemHTML += $@"
        <tr class='children hidden'>
            <td colspan=''>
            <table class='table table-bordered table-child'>
                <tbody>";

                var recetasInternas = controladorReceta.obtenerRecetasbyReceta(idItem); //recetas_recetas
                foreach (var ri in recetasInternas)
                {
                    itemHTML += GenerarHTMLRecetaProducto(ri.idRecetaIngrediente, true); // Llamada recursiva
                }

                var productosInternos = controladorReceta.ObtenerProductosByReceta(idItem); //recetas_productos
                foreach (var pi in productosInternos)
                {
                    itemHTML += GenerarHTMLRecetaProducto(pi.idProducto, false); // Llamada recursiva
                }

                // Cerrar la tabla y la fila hija
                itemHTML += @"
                </tbody>
            </table>
            </td>
        </tr>";
            }


            //itemHTML += "</div>";

            // Cerrar las etiquetas de la fila hija
            itemHTML += "</td></tr>";

            return itemHTML;
        }
    }
}