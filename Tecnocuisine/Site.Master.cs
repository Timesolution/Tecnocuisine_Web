using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tecnocuisine
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblSiteMap.Text = HttpContext.Current.Request.Url.AbsolutePath.Remove(0, 1).Replace("/", " / ").Replace(".aspx", "").Replace("Formularios", "");
            lblSiteMap.Style.Add("padding-top", "5%");

            if (lblSiteMap.Text.Contains("PedidosOrdenes"))
                this.lblSiteMap.Text = "Produccion / Pedidos";
        }
        protected void btnGestion_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Session["User"].ToString();
                string pass = Session["Pass"].ToString();
                string urlGestion = WebConfigurationManager.AppSettings.Get("urlGestion");

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.open('" + urlGestion + "/Account/Login.aspx?us=" + user + "&pw=" + pass + "&mascotas=2', '_blank');", true);

            }
            catch
            {

            }
        }
    }
}