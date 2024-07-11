using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tecnocuisine.Modelos;
using Tecnocuisine_API.Controladores;
using Tecnocuisine_API.Entitys;


namespace Tecnocuisine
{
    public partial class StockPopUp : Page
    {
        private int idStock;
        private Tecnocuisine_API.Controladores.ControladorStock controladorStock = new ControladorStock();
        Gestion_Api.Controladores.controladorSucursal contSucu = new Gestion_Api.Controladores.controladorSucursal();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.idStock = Convert.ToInt32(Request.QueryString["idStock"]);

                btn_Agregar.Attributes.Add("onclick", " this.disabled = true; this.value='Aguarde…'; " + ClientScript.GetPostBackEventReference(btn_Agregar, null) + ";");

                this.cargarStock();
                this.txtAgregarStock.Focus();
            }
            catch (Exception ex)
            {

            }
        }

        private void cargarStock()
        {
            try
            {
                Tecnocuisine_API.Entitys.Stock st = controladorStock.obtenerStockID(idStock);


               
                txtCodigo.Text = st.id.ToString();
                txtSucursal.Text = contSucu.obtenerSucursalID(st.local).nombre;
                txtArticulo.Text = st.Productos.descripcion;
                txtStockActual.Text = st.stock1.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtAgregarStock.Text))
                {
                    Tecnocuisine_API.Entitys.Stock st = controladorStock.obtenerStockID(idStock);
                    Stock_Movimiento s = new Stock_Movimiento();
                    txtAgregarStock.Text = txtAgregarStock.Text.Replace(',', '.');

                    //Agrego el movimiento de stock                  
                    s.IdUsuario = (int)Session["Login_IdUser"];
                    s.Cantidad = Convert.ToDecimal(this.txtAgregarStock.Text);
                    s.Articulo = st.Productos.id;
                    s.IdSucursal = st.local;
                    s.Fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-AR"));
                    s.TipoMovimiento = "Inventario";
                    s.Comentarios = this.txtComentarios.Text;

                    int j = controladorStock.AgregarMovimientoStock(s);
                    if (j > 0)
                    {
                        int i = controladorStock.ActualizarStock(this.idStock, Convert.ToDecimal(txtAgregarStock.Text));
                        if (i > 0)
                        {
                            Modal.Close(this, "OK");
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}