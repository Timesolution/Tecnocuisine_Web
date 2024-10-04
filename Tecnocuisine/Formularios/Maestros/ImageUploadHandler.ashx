<%@ WebHandler Language="C#" Class="ImageUploadHandler" %>

using System;
using System.IO;
using System.Web;
using System.Linq;

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
            string filePath = context.Server.MapPath("~/Img/Recetas/") + newFileName;

            // Eliminar anterior (por que si tiene distinta extension no se reemplaza)
            // Busca el archivo que contenga el nombre dado
            var files = Directory.GetFiles(context.Server.MapPath("~/Img/Recetas/"))
                .Where(f => Path.GetFileNameWithoutExtension(f) == idReceta); // Filtra por nombre
            foreach (var f in files)
            {
                File.Delete(f);
            }

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
