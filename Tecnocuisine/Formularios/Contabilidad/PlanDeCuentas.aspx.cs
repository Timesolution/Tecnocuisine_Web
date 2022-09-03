using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Contabilidad
{
    public partial class PlanDeCuentas : System.Web.UI.Page
    {
        Mensaje m = new Mensaje();
        ControladorCategoria controlador = new ControladorCategoria();
        ControladorPlanDeCuentas controladorPC = new ControladorPlanDeCuentas();
        int accion;
        int idCategoria;
        int idPlanDeCuenta;
        int Mensaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarLogin();

            this.Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            this.idCategoria = Convert.ToInt32(Request.QueryString["id"]);
            this.idPlanDeCuenta = Convert.ToInt32(Request.QueryString["id"]);
            this.accion = Convert.ToInt32(Request.QueryString["a"]);
            if (!IsPostBack)
            {


                if (accion == 2)
                {
                    cargarPlanDeCuentas();
                }
                cargarNestedListPlanDeCuentas();

                if (Mensaje != 0)
                {
                    this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
                }

            }
            ObtenerInsumos();


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

        protected void GuardarPlanDeCuentas()
        {
            try
            {
                Tecnocuisine_API.Entitys.PlanDeCuentas planDeCuentas = new Tecnocuisine_API.Entitys.PlanDeCuentas();
                planDeCuentas.Descripcion = txtDescripcionCategoria.Text;
                planDeCuentas.Activo = 1;
                planDeCuentas.Id = controladorPC.AgregarPlanDeCuentas(planDeCuentas);


                if (planDeCuentas.Id > 0)
                {
                    Response.Redirect("PlanDeCuentas.aspx?m=1");
                }
            }
            catch (Exception ex)
            {
            }

        }


        private void cargarNestedListPlanDeCuentas()
        {
            List<Tecnocuisine_API.Entitys.PlanDeCuentas> PlanDeCuentas = controladorPC.obtenerPlanDeCuentasPrimerNivel();
            foreach (Tecnocuisine_API.Entitys.PlanDeCuentas item in PlanDeCuentas)
            {


                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "dd-item");
                li.Attributes.Add("data-id", item.Id.ToString());
                li.Attributes.Add("id", item.Id.ToString());
                li.Attributes.Add("runat", "server");

                main.Controls.Add(li);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "dd-handle drag_disabled editable");
                div.InnerText = item.Descripcion;

                li.Controls.Add(div);




                HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                btnEliminar.ID = "btnEliminar_" + item.Id;
                btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right drag_enabled");
                btnEliminar.Attributes.Add("data-toggle", "modal");
                btnEliminar.Attributes.Add("style", "margin-right: 0.3%;");
                btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                btnEliminar.Attributes.Add("data-action", "delete");
                div.Controls.Add(btnEliminar);

                Literal l4 = new Literal();
                l4.Text = "&nbsp";
                div.Controls.Add(l4);

                HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right drag_enabled");
                btnDetalles.Attributes.Add("style", "margin-right: 0.3%;");
                btnDetalles.ID = "btnSelec_" + item.Id + "_";
                btnDetalles.InnerHtml = "<span><i class='fa fa-pencil'></i></span>";
                btnDetalles.Attributes.Add("data-action", "edit");
                div.Controls.Add(btnDetalles);


                Literal l3 = new Literal();
                l3.Text = "&nbsp";
                div.Controls.Add(l3);


                HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right drag_enabled");
                btnAgregar.Attributes.Add("style", "margin-right: 0.3%;");
                btnAgregar.ID = "btnAgregar_" + item.Id + "_";
                btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                btnAgregar.Attributes.Add("data-toggle", "modal");
                btnAgregar.Attributes.Add("href", "#modalAgregarSubCategoria");
                btnAgregar.Attributes.Add("data-action", "add");
                div.Controls.Add(btnAgregar);

                cargarNestedListPlanDeCuentasHijas(item.Id, li);
            }
        }

        private void cargarNestedListPlanDeCuentasHijas(int id, HtmlGenericControl li)
        {
            try
            {
                List<Tecnocuisine_API.Entitys.PlanDeCuentas> PlanDeCuentas = controladorPC.obtenerPlanDeCuentasHijas(id);
                if (PlanDeCuentas.Count > 0)
                {
                    HtmlGenericControl ol = new HtmlGenericControl("ol");
                    ol.Attributes.Add("class", "dd-list");

                    li.Controls.Add(ol);
                    foreach (Tecnocuisine_API.Entitys.PlanDeCuentas item in PlanDeCuentas)
                    {
                        HtmlGenericControl liHijo = new HtmlGenericControl("li");

                        liHijo.Attributes.Add("class", "dd-item");
                        liHijo.Attributes.Add("data-id", item.Id.ToString());
                        liHijo.Attributes.Add("id", item.Id.ToString());
                        liHijo.Attributes.Add("runat", "server");

                        ol.Controls.Add(liHijo);

                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Attributes.Add("class", "dd-handle drag_disabled editable");
                        div.InnerText = item.Descripcion;


                        liHijo.Controls.Add(div);


                        HtmlGenericControl btnEliminar = new HtmlGenericControl("button");
                        btnEliminar.ID = "btnEliminar_" + item.Id;
                        btnEliminar.Attributes.Add("class", "btn btn-danger btn-xs pull-right drag_enabled");
                        btnEliminar.Attributes.Add("style", "margin-right: 0.3%;");
                        btnEliminar.Attributes.Add("data-toggle", "modal");
                        btnEliminar.Attributes.Add("href", "#modalConfirmacion2");
                        btnEliminar.InnerHtml = "<span><i class='fa fa-trash - o'></i></span>";
                        btnEliminar.Attributes.Add("data-action", "delete");
                        div.Controls.Add(btnEliminar);

                        Literal l2 = new Literal();
                        l2.Text = "&nbsp";
                        div.Controls.Add(l2);

                        HtmlGenericControl btnDetalles = new HtmlGenericControl("button");
                        btnDetalles.Attributes.Add("class", "btn btn-success btn-xs pull-right drag_enabled");
                        btnDetalles.Attributes.Add("style", "margin-right: 0.3%;");
                        btnDetalles.ID = "btnSelec_" + item.Id + "_";
                        btnDetalles.InnerHtml = "<span><i class='fa fa-pencil'></i></span>";
                        btnDetalles.Attributes.Add("data-action", "edit");
                        div.Controls.Add(btnDetalles);


                        Literal l3 = new Literal();
                        l3.Text = "&nbsp";
                        div.Controls.Add(l3);


                        HtmlGenericControl btnAgregar = new HtmlGenericControl("button");
                        btnAgregar.Attributes.Add("class", "btn btn-primary btn-xs pull-right drag_enabled");
                        btnAgregar.Attributes.Add("style", "margin-right: 0.3%;");
                        btnAgregar.ID = "btnAgregar_" + item.Id + "_";
                        btnAgregar.InnerHtml = "<span><i class='fa fa-plus'></i></span>";
                        btnAgregar.Attributes.Add("data-toggle", "modal");
                        btnAgregar.Attributes.Add("href", "#modalAgregarSubCategoria");
                        btnAgregar.Attributes.Add("data-action", "add");
                        div.Controls.Add(btnAgregar);


                        cargarNestedListPlanDeCuentasHijas(item.Id, liHijo);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        private void editarCategoria(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');

                Response.Redirect("Categorias.aspx?a=2&i=" + id[1]);
            }
            catch (Exception Ex)
            {

            }
        }

        public void cargarPlanDeCuentas()
        {
            try
            {
                //var categoria = controlador.ObtenerCategoriaById(this.idCategoria);
                var planDeCuenta = controladorPC.ObtenerPlanDeCuentaById(this.idCategoria);
                if (planDeCuenta != null)
                {
                    hiddenEditar.Value = planDeCuenta.Id.ToString();
                    txtDescripcionCategoria.Text = planDeCuenta.Descripcion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idPlanDeCuenta = Convert.ToInt32(this.hiddenID.Value);
                //int resultado = controlador.EliminarCategoria(idCategoria);
                int resultado = controladorPC.EliminarPlanDeCuenta(idPlanDeCuenta);

                if (resultado > 0)
                {
                    //resultado = controlador.EliminarFamiliaCategoria(idPlanDeCuenta);
                    resultado = controladorPC.EliminarFamiliaPlanDeCuenta(idPlanDeCuenta);
                    if (resultado > 0)
                        Response.Redirect("PlanDeCuentas.aspx?m=3");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar la categoria", "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EditarCategoria()
        {
            try
            {
                Tecnocuisine_API.Entitys.PlanDeCuentas planDeCuenta = new Tecnocuisine_API.Entitys.PlanDeCuentas();
                planDeCuenta.Id = this.idPlanDeCuenta;
                planDeCuenta.Descripcion = txtDescripcionCategoria.Text;
                planDeCuenta.Activo = 1;


                int resultado = controladorPC.EditarPlanDeCuenta(planDeCuenta);

                if (resultado > 0)
                {
                    Response.Redirect("PlanDeCuentas.aspx?m=2");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo editar la categoria", "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.hiddenEditar.Value != "")
                {
                    EditarCategoria();
                }
                else
                {
                    GuardarPlanDeCuentas();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Tecnocuisine_API.Entitys.PlanDeCuentas planDeCuentas = new Tecnocuisine_API.Entitys.PlanDeCuentas();
            planDeCuentas.Descripcion = txtSubCategoria.Text;
            planDeCuentas.Activo = 1;
            planDeCuentas.Id = controladorPC.AgregarPlanDeCuentas(planDeCuentas);

            PlanDeCuentas_Familia PlanDeCuentasFamilia = new PlanDeCuentas_Familia { IdPlanDeCuenta = planDeCuentas.Id, IdPadre = Convert.ToInt32(hiddenID2.Value) };

            if (planDeCuentas.Id > 0)
            {
                int i = controladorPC.AgregarPlanDeCuentasFamilia(PlanDeCuentasFamilia);

                Response.Redirect("PlanDeCuentas.aspx?m=1");
            }
        }

        protected void btnSubAtributos_Click(object sender, EventArgs e)
        {
            try
            {
                string idTildado = "";

                foreach (Control C in phInsumos.Controls)
                {
                    TableRow tr = C as TableRow;
                    CheckBox ch = tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox;
                    if (ch.Checked == true)
                    {
                        //idtildado += ch.ID.Substring(12, ch.ID.Length - 12) + ";";
                        idTildado += ch.ID.Split('_')[1] + ";";
                    }
                }

                int i = controlador.AgregarSubAtributo(idTildado, Convert.ToInt32(hiddenSubAtributo.Value));
                if (i > 0)
                {
                    Response.Redirect("Categorias.aspx?m=1");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ObtenerInsumos()
        {
            try
            {
                ControladorInsumo controladorInsumo = new ControladorInsumo();
                var insumos = controladorInsumo.ObtenerTodosInsumos();

                if (insumos.Count > 0)
                {

                    foreach (var item in insumos)
                    {
                        CargarInsumosPH(item);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CargarInsumosPH(Insumos insumo)
        {

            try
            {

                //fila
                TableRow tr = new TableRow();
                tr.ID = insumo.id_insumo.ToString();

                //Celdas
                TableCell celNumero = new TableCell();
                celNumero.Text = insumo.id_insumo.ToString();
                celNumero.VerticalAlign = VerticalAlign.Middle;
                celNumero.HorizontalAlign = HorizontalAlign.Right;
                celNumero.Attributes.Add("style", "padding-bottom: 1px !important;");

                tr.Cells.Add(celNumero);

                TableCell celNombre = new TableCell();
                celNombre.Text = insumo.Descripcion;
                celNombre.VerticalAlign = VerticalAlign.Middle;
                celNombre.HorizontalAlign = HorizontalAlign.Left;
                celNombre.Attributes.Add("style", "padding-bottom: 1px !important;");
                tr.Cells.Add(celNombre);

                //agrego fila a tabla
                TableCell celAccion = new TableCell();
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "cbx_" + insumo.id_insumo;
                celAccion.Controls.Add(checkbox);
                celAccion.Width = Unit.Percentage(10);
                celAccion.Attributes.Add("style", "padding-bottom: 1px !important;text-align:center; vertical - align:middle;");

                tr.Cells.Add(celAccion);

                phInsumos.Controls.Add(tr);

            }
            catch (Exception ex)
            {

            }

        }
        [WebMethod]
        public static string GetSubAtributos(int id)
        {
            ControladorCategoria controlador = new ControladorCategoria();
            string tiposAtributos = controlador.obtenerTipoAtributos(id);

            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            javaScript.MaxJsonLength = 5000000;
            string resultadoJSON = javaScript.Serialize(tiposAtributos);
            return resultadoJSON;
        }

        [WebMethod]
        public static string ModificarRelaciones(string id)
        {
            ControladorPlanDeCuentas controladorPDC = new ControladorPlanDeCuentas();
            string[] ids = id.Split(';');
            int i = controladorPDC.EliminarTodasFamiliaCategorias();
            if (i > 0)
            {
                foreach (var item in ids)
                {
                    if (item != "")
                    {
                        int? idPadre = null;

                        try
                        {
                            idPadre = Convert.ToInt32(item.Split(',')[1]);

                        }
                        catch (Exception)
                        {
                        }
                        int idPlanDeCuenta = Convert.ToInt32(item.Split(',')[0]);
                        PlanDeCuentas_Familia familia = new PlanDeCuentas_Familia { IdPlanDeCuenta = idPlanDeCuenta, IdPadre = idPadre };
                        controladorPDC.AgregarPlanDeCuentasFamilia(familia);
                    }
                }
            }
            return "1";
        }

    }
}