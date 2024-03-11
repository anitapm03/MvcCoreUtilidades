using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private List<Coche> Cars;
        private RepositoryCoches repo;
        public CochesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        public IActionResult Details(int idcoche)
        {
            Coche car = this.repo.FindCoche(idcoche);
            return View(car);
        }

        //AQUI SOLAMENTE MOSTRAMOS LA VISTA INDEX
        public IActionResult Index()
        {
            return View();
        }

        //NECESITAMOS UNA PETICION IACTIONRESULT QUE CARGARA LOS 
        //COCHES.  DICHA PETICION IRA INTEGRADA DENTRO DE OTRA VISTA (INDEX)
        public IActionResult _CochesPartial()
        {
            //SI VAMOS A UTILIZAR UNA VISTA PARCIAL CON AJAX
            //DEBEMOS DEVOLVER PartialView()
            //Y EL MODEL SI LO NECESITAMOS
            return PartialView("_CochesPartial", this.Cars);
        }

        public IActionResult _DetailsCoche(int idcoche)
        {
            Coche car =
                this.Cars.FirstOrDefault(x => x.IdCoche == idcoche);
            return PartialView("_DetailsCoche", car);
        }
    }
}
