using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Tecnocuisine.Modelos
{
    public class Mensaje
    {
        public void ShowToastr(Page page, string message, string title, string type = "success")
        {
            try
            {
                string options = "toastr.options = { ";
                options += " positionClass: 'toast-bottom-right', ";
                options += "}; ";

                page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
                      options + "toastr." + type.ToLower() + "('" + message + "', '" + title + "');", addScriptTags: true);
            }
            catch (Exception ex)
            {

            }
        }



        public void ShowToastrWarning(Page page, string message, string title, string type = "warning")
        {
            try
            {
                string options = "toastr.options = { ";
                options += " positionClass: 'toast-bottom-right', ";
                options += "}; ";

                page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
                      options + "toastr." + type.ToLower() + "('" + message + "', '" + title + "');", addScriptTags: true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}