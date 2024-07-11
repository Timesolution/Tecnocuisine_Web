using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class Rubros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRubrosProveedoresBackEnd();
            }
        }


        private void CargarRubrosProveedoresBackEnd()
        {
            try
            {
                //ControladorSector contSec = new ControladorSector();
                ControladorRubroProveedor controladorRubroProveedor = new ControladorRubroProveedor();
                //var sectores = contSec.GetSectores();
                var rubrosProveedores = controladorRubroProveedor.GetRubrosProveedores();


                foreach (var Rubro in rubrosProveedores)
                {
                    CargarRubroProveedoresPH(Rubro);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarRubroProveedoresPH(Tecnocuisine_API.Entitys.RubrosProveedores Rubro)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = Rubro.ID.ToString();

                TableCell celRubroDescripcion = new TableCell();
                celRubroDescripcion.Text = Rubro.DescripcionRubro;
                celRubroDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celRubroDescripcion);

                //TableCell celEstado = new TableCell();
                //celEstado.Text = sector.estado.ToString();
                //celEstado.VerticalAlign = VerticalAlign.Middle;
                //tr.Cells.Add(celEstado);


                TableCell celAction = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Style.Add("margin-right", "10px");
                //btnEditar.Attributes.Add("title data-original-title", familia);
                //btnEditar.Attributes.Add("data-toggle", "modal");
                //btnEditar.Attributes.Add("title data-original-title", "Editar");
                btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + Rubro.ID + "','" + Rubro.DescripcionRubro + "'); ");
                celAction.Controls.Add(btnEditar);

                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                HtmlGenericControl btnEliminar = new HtmlGenericControl("a");
                btnEliminar.Attributes.Add("class", "btn btn-xs");
                btnEliminar.Style.Add("background-color", "transparent");
                btnEliminar.Style.Add("margin-right", "10px");
                //btnEliminar.Attributes.Add("title data-original-title", familia);
                //btnEliminar.Attributes.Add("data-toggle", "modal");
                //btnEliminar.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.InnerHtml = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                btnEliminar.Attributes.Add("OnClick", "ModalConfirmacion(" + Rubro.ID + ");");


                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                rubrosProveedoresPH.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string AddRubros(string descripcionRubro)
        {
            try
            {
                //ControladorSector contSector = new ControladorSector();
                ControladorRubroProveedor controladorRubroProveedor = new ControladorRubroProveedor();
                if (descripcionRubro != "")
                {

                    //Tecnocuisine_API.Entitys.Sectores sec = new Tecnocuisine_API.Entitys.Sectores();
                    Tecnocuisine_API.Entitys.RubrosProveedores rubrosProveedores = new Tecnocuisine_API.Entitys.RubrosProveedores();
                    rubrosProveedores.DescripcionRubro = descripcionRubro;
                    rubrosProveedores.Estado = 1;
                    //sec.id_emp = Convert.ToInt32(id_emp);

                    var i = controladorRubroProveedor.addRubroProveedor(rubrosProveedores);

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
                    return i;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static string RemoveRubrosProveedores(string id)
        {
            try
            {
                //ControladorSector contSectores = new ControladorSector();
                ControladorRubroProveedor controladorRubroProveedor = new ControladorRubroProveedor();
                if (id != "")
                {

                    var rta = controladorRubroProveedor.RemoveRubrosProveedores(id);

                    //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                    //string resultadoJSON = javaScript.Serialize(empresas);
                    return rta;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}