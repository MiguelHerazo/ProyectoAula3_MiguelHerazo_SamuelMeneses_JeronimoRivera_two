using System.Web.Mvc;

namespace ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index (Página de inicio)
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/About (Página "Acerca de")
        public ActionResult About()
        {
            ViewBag.Message = "Información sobre nuestra aplicación.";

            return View();
        }

        // GET: Home/Contact (Página de contacto)
        public ActionResult Contact()
        {
            ViewBag.Message = "Póngase en contacto con nosotros.";

            return View();
        }
    }
}
