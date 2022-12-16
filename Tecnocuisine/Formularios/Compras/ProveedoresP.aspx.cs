using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Tecnocuisine_API.Controladores;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Entitys;

namespace Tecnocuisine.Formularios.Administrador
{
    public partial class ProveedoresP : System.Web.UI.Page
    {
        ControladorProveedores ControladorProveedores = new ControladorProveedores();
        Mensaje m = new Mensaje();
        int Mensaje;
        int accion;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensaje = Convert.ToInt32(Request.QueryString["m"]);
            accion = Convert.ToInt32(Request.QueryString["a"]);
            id = Convert.ToInt32(Request.QueryString["i"]);
                ObtenerProveedoresAll();

            if (!IsPostBack)
            {
            }
            if (Mensaje == 1)
            {
                this.m.ShowToastr(this.Page, "Proceso concluido con Exito!", "Exito");
            }

        }

        private void ObtenerProveedoresAll()
        {
            try
            {
                var ListaProv = ControladorProveedores.ObtenerProveedoresAll();
                foreach (var proveedor in ListaProv)
                {
                    CargarProveedoresenPH(proveedor);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargarProveedoresenPH(Proveedores proveedor)
        {
            try
            {
                TableRow tr = new TableRow();

                TableCell celCodigo = new TableCell();
                celCodigo.Text = proveedor.Codigo;
                celCodigo.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celCodigo);

                TableCell celAlias = new TableCell();
                celAlias.Text = proveedor.Alias;
                celAlias.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celAlias);

            

                TableCell celPais = new TableCell();
                celPais.Text = proveedor.paises.Pais ;
                celPais.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celPais);

                     TableCell celTipoDoc = new TableCell();
                celTipoDoc.Text = proveedor.paises.TipoDocumento;
                celTipoDoc.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoDoc);

                TableCell celTipoNumero = new TableCell();
                celTipoNumero.Text = proveedor.Numero;
                celTipoNumero.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(celTipoNumero);

                TableCell celAction = new TableCell();
                LinkButton btnEditar = new LinkButton();
                btnEditar.ID = proveedor.Id.ToString();

                btnEditar.CssClass = "btn btn-xs";
                btnEditar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEditar.Text = "<span><i style='color:black;' class='fa fa-pencil'></i></span>";
                btnEditar.Click+= new EventHandler(this.editarProveedor);
                celAction.Controls.Add(btnEditar);


                Literal l = new Literal();
                l.Text = "&nbsp";
                celAction.Controls.Add(l);


                LinkButton btnEliminar = new LinkButton();
                btnEliminar.ID = "btnEliminar_" + proveedor.Id;


                btnEliminar.OnClientClick = "abrirdialog(" + proveedor.Id + ");";
                btnEliminar.CssClass = "btn btn-xs";
                btnEliminar.Style.Add("background-color", "transparent");
                //btnDetalles.Attributes.Add("data-toggle", "tooltip");
                //btnDetalles.Attributes.Add("title data-original-title", "Editar");
                btnEliminar.Text = "<span><i style='color:black;' class='fa fa-trash'></i></span>";
                //btnEliminar.Click += new EventHandler(this.EliminarProveedor);
                celAction.Controls.Add(btnEliminar);
                celAction.Width = Unit.Percentage(10);
                celAction.VerticalAlign = VerticalAlign.Middle;
                celAction.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAction);

                phProveedores.Controls.Add(tr);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int idPRove = Convert.ToInt32(this.hiddenID.Value);
                Tecnocuisine_API.Entitys.Proveedores p = ControladorProveedores.ObtenerProveedorByID(idPRove);
                p.activo = 0;
                int resultado = ControladorProveedores.EditarProveedor(p);

                if (resultado > 0)
                {
                    Response.Redirect("ProveedoresP.aspx?m=1");
                }
                else
                {
                    this.m.ShowToastr(this.Page, "No se pudo eliminar Proveedor", "warning");
                }
            }
            catch (Exception ex)
            {
                this.m.ShowToastr(this.Page, "No se pudo eliminar Proveedor", "warning");
            }
        }

        private void editarProveedor(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = sender as LinkButton;
                string[] id = lb.ID.Split('_');
                Response.Redirect("Proveedores.aspx?i=" + id[0] + "&a=2");
            }
            catch (Exception ex)
            {

               
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}