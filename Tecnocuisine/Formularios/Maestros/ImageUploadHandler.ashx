<%@ WebHandler Language="C#" Class="ImageUploadHandler" %>

using System;
using System.IO;
using System.Web;

public class ImageUploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile file = context.Request.Files["image"];
        if (file != null && file.ContentLength > 0)
        {
            // Obtener la extensión del archivo original
            string extension = Path.GetExtension(file.FileName);
            string idReceta = context.Request["idReceta"].ToString();
            string newFileName = idReceta + extension; 
            string filePath = context.Server.MapPath("~/Img/") + newFileName;

            file.SaveAs(filePath);

            context.Response.ContentType = "text/plain";
            context.Response.Write("File uploaded successfully");
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("No file uploaded");
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
