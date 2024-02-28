namespace MvcCoreUtilidades.Helpers
{
    //aqui debemos de tener todas las carpetas que
    //deseemos que nuestros controllers utilicen
    public enum Folders { Images = 0, Facturas = 1, Uploads = 2, Temporal = 3 }
    public class HelperPathProvider
    {
        //necesitamos acceder al sistema de archivos del webserver
        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if(folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }

            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
    }
}
