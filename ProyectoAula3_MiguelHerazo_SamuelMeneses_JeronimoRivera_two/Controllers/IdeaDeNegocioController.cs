using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Models;
namespace ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Controllers
{
    public class IdeaDeNegocioController : Controller
    {
        private static List<IdeaDeNegocio> ideas = new List<IdeaDeNegocio>(); // Lista en memoria para almacenar las ideas.

        // GET: IdeaDeNegocio
        public ActionResult Index()
        {
            return View(ideas);
        }

        // GET: IdeaDeNegocio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdeaDeNegocio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdeaDeNegocio ideaDeNegocio)
        {
            if (ModelState.IsValid)
            {
                ideas.Add(ideaDeNegocio);
                return RedirectToAction("Index");
            }

            return View(ideaDeNegocio);
        }

        // Editar una idea de negocio existente
        public ActionResult Edit(int id)
        {
            IdeaDeNegocio idea = ideas.FirstOrDefault(i => i.Codigo == id);

            if (idea == null)
            {
                return HttpNotFound(); // Retorna un error 404 si la idea no se encuentra
            }

            // Muestra una vista de edición con los detalles de la idea de negocio
            return View(idea);
        }

        [HttpPost]
        public ActionResult Edit(IdeaDeNegocio idea)
        {
            // Busca la idea de negocio correspondiente por su ID
            IdeaDeNegocio ideaExistente = ideas.FirstOrDefault(i => i.Codigo == idea.Codigo);

            if (ideaExistente == null)
            {
                return HttpNotFound(); // Retorna un error 404 si la idea no se encuentra
            }

            // Actualiza la idea de negocio con los nuevos datos
            ideaExistente.Nombre = idea.Nombre;
            ideaExistente.Impacto = idea.Impacto;
            ideaExistente.ValorInversion = idea.ValorInversion;
            ideaExistente.Ingresos3Anios = idea.Ingresos3Anios;
            ideaExistente.Herramientas4RI = idea.Herramientas4RI;

            // Redirecciona al usuario a la vista Index u otra vista de tu elección
            return RedirectToAction("Index");
        }

        // Eliminar una idea de negocio existente
        public ActionResult Delete(int id)
        {
            // Busca la idea de negocio correspondiente por su ID
            IdeaDeNegocio idea = ideas.FirstOrDefault(i => i.Codigo == id);

            if (idea == null)
            {
                return HttpNotFound(); // Retorna un error 404 si la idea no se encuentra
            }

            // Muestra una vista de confirmación de eliminación
            return View(idea);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Busca la idea de negocio correspondiente por su ID
            IdeaDeNegocio idea = ideas.FirstOrDefault(i => i.Codigo == id);

            if (idea == null)
            {
                return HttpNotFound(); // Retorna un error 404 si la idea no se encuentra
            }

            // Elimina la idea de negocio de la lista
            ideas.Remove(idea);

            // Redirecciona al usuario a la vista Index u otra vista de tu elección
            return RedirectToAction("Index");
        }

        public ActionResult IdeaConMayorImpacto()
        {
            try
            {
                // Encuentra la idea de negocio con el mayor número de departamentos impactados
                IdeaDeNegocio ideaConMayorImpacto = ideas.OrderByDescending(i => i.DepartamentosBeneficiados.Count).FirstOrDefault();

                if (ideaConMayorImpacto == null)
                {
                    throw new Exception("No se encontró ninguna idea de negocio.");
                }

                return View(ideaConMayorImpacto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al buscar la idea de negocio con mayor impacto: " + ex.Message;
                return View("Error");
            }

        }
        public ActionResult IdeaConMayorIngresos()
        {
            try
            {
                // Encuentra la idea de negocio con el mayor total de ingresos en los primeros 3 años
                IdeaDeNegocio ideaConMayorIngresos = ideas.OrderByDescending(i => i.Ingresos3Anios).FirstOrDefault();

                if (ideaConMayorIngresos == null)
                {
                    throw new Exception("No se encontró ninguna idea de negocio.");
                }

                return View(ideaConMayorIngresos);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al buscar la idea de negocio con mayores ingresos: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult IdeasConMayorRentabilidad()
        {
            try
            {
                // Ordena las ideas de negocio por rentabilidad de forma descendente y toma las primeras 3
                List<IdeaDeNegocio> ideasConMayorRentabilidad = ideas.OrderByDescending(i => i.CalcularRentabilidad()).Take(3).ToList();

                if (ideasConMayorRentabilidad.Count == 0)
                {
                    throw new Exception("No se encontraron ideas de negocio.");
                }

                List<string> nombresIdeas = ideasConMayorRentabilidad.Select(i => i.Nombre).ToList();

                return View(nombresIdeas);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al buscar las ideas de negocio con mayor rentabilidad: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult IdeasConMayorImpactoDepartamentos()
        {
            try
            {
                // Filtrar las ideas de negocio que impacten en más de 3 departamentos
                List<IdeaDeNegocio> ideasConMayorImpacto = ideas.Where(i => i.DepartamentosBeneficiados.Count > 3).ToList();

                if (ideasConMayorImpacto.Count == 0)
                {
                    throw new Exception("No se encontraron ideas de negocio con impacto en más de 3 departamentos.");
                }

                // Obtén los nombres de las ideas y colócalos en una lista
                List<string> nombresIdeas = ideasConMayorImpacto.Select(i => i.Nombre).ToList();

                return View(nombresIdeas);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al buscar las ideas de negocio con mayor impacto en departamentos: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult SumaTotalIngresos()
        {
            try
            {
                // Obtén la suma total de ingresos de todas las ideas de negocio
                decimal sumaTotalIngresos = ideas.Sum(i => i.Ingresos3Anios);

                ViewBag.SumaTotalIngresos = sumaTotalIngresos;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al calcular la suma total de ingresos: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult SumaTotalInversion()
        {
            try
            {
                // Obtén la suma total de inversión de todas las ideas de negocio
                decimal sumaTotalInversion = ideas.Sum(i => i.ValorInversion);

                ViewBag.SumaTotalInversion = sumaTotalInversion;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al calcular la suma total de inversión: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult IdeaConMayorHerramientas4RI()
        {
            try
            {
                // Encuentra la idea de negocio con la mayor cantidad de herramientas 4RI
                var ideaConMayorHerramientas4RI = ideas.OrderByDescending(i => i.Herramientas4RI.Count).FirstOrDefault();

                if (ideaConMayorHerramientas4RI != null)
                {
                    ViewBag.NombreIdea = ideaConMayorHerramientas4RI.Nombre;
                    ViewBag.IntegrantesEquipo = ideaConMayorHerramientas4RI.IntegrantesEquipo;
                    ViewBag.CantidadHerramientas = ideaConMayorHerramientas4RI.Herramientas4RI.Count;
                }
                else
                {
                    ViewBag.NombreIdea = "No hay ideas de negocio registradas.";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al obtener la idea de negocio con mayor cantidad de herramientas 4RI: " + ex.Message;
                return View("Error");
            }
        }
        // Obtener la cantidad de ideas de negocio que tienen "Inteligencia Artificial"
        public ActionResult CantidadIdeasConInteligenciaArtificial()
        {
            try
            {
                // Filtra las ideas de negocio que contienen "Inteligencia Artificial" en sus herramientas 4RI
                var ideasConInteligenciaArtificial = ideas.Where(i => i.Herramientas4RI.Any(h => h.Contains("Inteligencia Artificial"))).ToList();
                int cantidad = ideasConInteligenciaArtificial.Count;

                ViewBag.CantidadIdeas = cantidad;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al obtener la cantidad de ideas de negocio con 'Inteligencia Artificial': " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult IdeasConDesarrolloSostenible()
        {
            try
            {
                // Filtra las ideas de negocio que tienen "Desarrollo sostenible" en su impacto
                var ideasConDesarrolloSostenible = ideas.Where(i => i.Impacto.ToLower().Contains("desarrollo sostenible")).ToList();

                return View(ideasConDesarrolloSostenible);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al obtener las ideas de negocio con 'Desarrollo sostenible': " + ex.Message;
                return View("Error");
            }
        }
    }
}
