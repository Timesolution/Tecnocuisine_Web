using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tecnocuisine
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblSiteMap.Text = HttpContext.Current.Request.Url.AbsolutePath.Remove(0,1).Replace("/"," / ").Replace(".aspx","").Replace("Formularios","");
            lblSiteMap.Style.Add("padding-top", "10%");
        }
    }
}