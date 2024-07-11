using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;

namespace Tecnocuisine.App_Code
{
    public class BaseCulture : Page
    {

        protected override void InitializeCulture()
        {
            string lang = string.Empty;
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (cookie!=null && cookie.Value!=null)
            {
                lang = cookie.Value;
                CultureInfo cul = CultureInfo.CreateSpecificCulture("en-US");
                System.Threading.Thread.CurrentThread.CurrentUICulture = cul;
                System.Threading.Thread.CurrentThread.CurrentCulture= cul;
            }
            else
            {
                if (string.IsNullOrEmpty(lang)) lang = "en-US";
                CultureInfo cul = CultureInfo.CreateSpecificCulture("en-US");
                System.Threading.Thread.CurrentThread.CurrentUICulture = cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = cul;
                HttpCookie cookie_new = new HttpCookie("CurrentLanguage");
                cookie_new.Value = lang;
                Response.SetCookie(cookie_new);
            }
            base.InitializeCulture();
        }

    }
}