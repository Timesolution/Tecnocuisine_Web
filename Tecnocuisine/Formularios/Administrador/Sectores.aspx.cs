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
    public partial class Sectores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSectoresBackEnd();
            }
        }

        private void CargarSectoresBackEnd()
        {
            try
            {
                ControladorSector contSec = new ControladorSector();

                var sectores = contSec.GetSectores();

                foreach (var sector in sectores)
                {
                    CargarSectorPH(sector);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CargarSectorPH(Tecnocuisine_API.Entitys.Sectores sector)
        {
            try
            {
                TableRow tr = new TableRow();
                tr.ID = sector.id.ToString();

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = sector.nombre;
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celDescripcion);

                TableCell celEstado = new TableCell();
                celEstado.Text = sector.estado.ToString();
                celEstado.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celEstado);


                TableCell celAction = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                btnEditar.Style.Add("background-color", "transparent");
                btnEditar.Style.Add("margin-right", "10px");
                //btnEditar.Attributes.Add("title data-original-title", familia);
                //btnEditar.Attributes.Add("data-toggle", "modal");
                //btnEditar.Attributes.Add("title data-original-title", "Editar");
                btnEditar.InnerHtml = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Attributes.Add("OnClick", "vaciarInputs();ModalModificar('" + sector.id + "','" + sector.nombre + "','" + sector.id_emp + "'); ");
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
                btnEliminar.Attributes.Add("OnClick", "ModalConfirmacion(" + sector.id + ");");


                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                SectoresPH.Controls.Add(tr);
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string CargarEmpresas()
        {
            try
            {
                ControladorEmpresa contEmpresa = new ControladorEmpresa();

                var empresas = contEmpresa.GetEmpresas();

                string reg = "[";
                foreach (var item in empresas)
                {
                    reg += "{" +
                        "\"id\":\"" + item.Id + "\"," +
                        "\"descripcion\":\"" + item.Razon_Social + "\"" +
                        "},";

                }
                reg = reg.Remove(reg.Length - 1) + "]";

                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return reg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string CargarSectores()
        {
            try
            {
                ControladorSector contSec = new ControladorSector();

                var sectores = contSec.GetSectores();

                string sec = "[";
                foreach (var item in sectores)
                {
                    sec += "{" +
                        "\"Id\":\"" + item.id + "\"," +
                        "\"nombre\":\"" + item.nombre + "\"," +
                        "\"Empresa\":\"" + item.id_emp + "\"," +
                        "\"Estado\":\"" + item.estado + "\"" +
                        "},";

                }
                sec = sec.Remove(sec.Length - 1)+"]";

                if (sectores.Count == 0)
                {
                    sec = "[]";
                }
                //JavaScriptSerializer javaScript = new JavaScriptSerializer();
                //string resultadoJSON = javaScript.Serialize(empresas);
                return sec;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string AddSectores(string nombre, string id_emp)
        {
            try
            {
                ControladorSector contSector = new ControladorSector();
                if (nombre != "" && id_emp != "")
                {

                    Tecnocuisine_API.Entitys.Sectores sec = new Tecnocuisine_API.Entitys.Sectores();
                    sec.nombre = nombre;
                    sec.estado = 1;
                    sec.id_emp = Convert.ToInt32(id_emp);

                    var i = contSector.AddSectores(sec);

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
        public static string ChangeSectores(string id, string nombre, string id_emp)
        {
            try
            {
                ControladorSector contSectores = new ControladorSector();
                if (id != "" && id_emp != "")
                {

                    Tecnocuisine_API.Entitys.Sectores sec = new Tecnocuisine_API.Entitys.Sectores();
                    sec.id = Convert.ToInt32(id);
                    sec.nombre = nombre;
                    sec.id_emp = Convert.ToInt32(id_emp);
                    sec.estado = 1;

                    var rta = contSectores.ChangeSectores(sec);

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

        [WebMethod]
        public static string RemoveSectores(string id)
        {
            try
            {
                ControladorSector contSectores = new ControladorSector();
                if (id != "")
                {

                    var rta = contSectores.RemoveSectores(id);

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