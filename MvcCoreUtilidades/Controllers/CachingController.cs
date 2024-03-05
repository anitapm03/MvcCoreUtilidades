using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            //la primera vez no recibimos tiempo asi que 
            //ponemos 60 segundos 
            if (tiempo == null){
                tiempo = 60;
            }
            string fecha = DateTime.Now.ToLongDateString() + " -- " + 
                DateTime.Now.ToLongTimeString();

            //preguntamos si existe algo en cache
            if (this.memoryCache.Get("FECHA") == null)
            {
                //no existe cache aun

                //creamos la opción para el tiempo
                MemoryCacheEntryOptions options =
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));

                this.memoryCache.Set("FECHA", fecha);
                ViewData["MENSAJE"] = "Almacenado en cache";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                //tenemos la fecha en cache
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Recuperado de cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }
        //podemos indicar en seg para que 
        //reponda de nuevo al action
        [ResponseCache(Duration = 15, 
            Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " +
                DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
