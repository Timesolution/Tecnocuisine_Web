<%@ WebHandler Language="C#" Class="ImageUploadHandler" %>

using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Drawing;

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

            // Redimensionar la imagen antes de guardarla
            using (var img = System.Drawing.Image.FromStream(file.InputStream))
            {
                // Establecer el nuevo tamaño
                int newWidth = 100;
                int newHeight = (int)(img.Height * (newWidth / (float)img.Width));

                // Crear una nueva imagen redimensionada
                using (var resizedImg = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImg))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(img, 0, 0, newWidth, newHeight);
                    }

                    // Guardar la imagen redimensionada
                    resizedImg.Save(filePath);
                }
            }

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
