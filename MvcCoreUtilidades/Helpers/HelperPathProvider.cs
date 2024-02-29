using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;

namespace MvcCoreUtilidades.Helpers
{
    //aqui debemos de tener todas las carpetas que
    //deseemos que nuestros controllers utilicen
    public enum Folders { Images = 0, Facturas = 1, Uploads = 2, Temporal = 3, Mails = 4 }
    public class HelperPathProvider
    {
        //necesitamos acceder al sistema de archivos del webserver
        private IWebHostEnvironment hostEnvironment;
        private IServer server;

        public HelperPathProvider
            (IWebHostEnvironment hostEnvironment,
            IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server;
        }

        //creamos un metodo privado que nos devuelva el nombre
        //de la carpeta dependiendo del folder
        private string GetFolderPath(Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }else if (folder == Folders.Mails)
            {
                carpeta = "mails";
            }
            return carpeta;
        }

        public string MapPath(string fileName, Folders folder)
        {
            
            string carpeta = GetFolderPath(folder);
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }


        public string MapServerPath(string fileName, Folders folder)
        {
            var adresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string http = adresses.FirstOrDefault();

            string carpeta = GetFolderPath(folder);

            string url = http + "/" + carpeta + "/" + fileName;
            return url;
        }
        

        
        

    }
}
