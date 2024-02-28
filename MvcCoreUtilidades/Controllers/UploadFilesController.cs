using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {

        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> SubirFichero()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            ////aqui trabajaremos con files, es decir, system.IO
            ////comenzamos almacenando el fichero en una ruta temporal

            //string tempFolder = Path.GetTempPath();
            //string filename = fichero.FileName;

            //SEGUNDA VERSION
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            string filename = fichero.FileName;

            //necesitamos la ruta fisica para poder escribir el fichero 
            //la ruta es la combinación de tempfolder y filename 
            // C:\Documents\Temp\file1.txt
            //cuando estemos hablando de files (System.IO) para 
            //acceder a rutas debemos usar path.combine
            string path = Path.Combine(rootFolder, "uploads", filename);

            //subimos el fichero utilizando stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //mediante IformFile copiamoes el
                //Contenido del fichero al stream
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "fichero subido a " + path;
            return View();
        }
    }
}
