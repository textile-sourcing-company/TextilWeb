using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class RecursosModelo
    {
        public bool SaveImage(string ImgStr, string ImgName, out string mensaje)
        {
            try
            {
                String path = HttpContext.Current.Server.MapPath("~/Public/tmpliquidacion"); //Path

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                string imageName = ImgName + ".jpg";

                //set the image path
                string imgPath = Path.Combine(path, imageName);

                byte[] imageBytes = Convert.FromBase64String(ImgStr);

                File.WriteAllBytes(imgPath, imageBytes);
                mensaje = "Guardado correctamente";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            
        }
    }
}