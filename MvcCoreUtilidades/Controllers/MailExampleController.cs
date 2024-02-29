using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailExampleController : Controller
    {
        //necesitamos recuperar las claves de settings
        private HelperMails helperMails;
        private HelperUploadFiles uploadFiles;

        public MailExampleController (
            HelperMails helperMails,
            HelperUploadFiles uploadFiles)
        {
            this.helperMails = helperMails;
            this.uploadFiles = uploadFiles;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail
            (string para, string asunto, string mensaje, IFormFile fichero)
        {
            
            if (fichero != null)
            {
                string path = await this.uploadFiles.UploadFileAsync(fichero, Folders.Mails);
                await this.helperMails.SendMailAsync(para, asunto, mensaje, path);
            } else
            {
                await this.helperMails.SendMailAsync(para, asunto, mensaje);
            }
            
            ViewData["MENSAJE"] = "Enviado!";

            return View();
        }
    }
}
