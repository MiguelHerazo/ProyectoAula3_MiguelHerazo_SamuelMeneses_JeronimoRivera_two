using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Models;

namespace ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera.Controllers
{
    public class DepartamentoController : Controller
    {
        private static List<Departamento> departamentos = new List<Departamento>(); // Lista de departamentos (en memoria)

        // Mostrar la lista de departamentos
        public ActionResult Index()
        {
            try
            {
                // Muestra una vista con la lista de departamentos
                return View(departamentos);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar la lista de departamentos: " + ex.Message;
                return View("Error");
            }
        }

        // Crear un nuevo departamento
        public ActionResult Create()
        {
            // Muestra una vista de creación de departamento vacía
            return View();
        }

        [HttpPost]
        public ActionResult Create(Departamento departamento)
        {
            try
            {
                // Asigna un ID único al nuevo departamento 
                departamento.Id = Guid.NewGuid();

                // Agrega el nuevo departamento a la lista
                departamentos.Add(departamento);

                // Redirecciona al usuario a la vista Index u otra vista de tu elección
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al crear el departamento: " + ex.Message;
                return View("Error");
            }
        }

        // Editar un departamento existente
        public ActionResult Edit(Guid id)
        {
            try
            {
                Departamento departamento = departamentos.FirstOrDefault(d => d.Id == id);

                if (departamento == null)
                {
                    return HttpNotFound(); // Retorna un error 404 si el departamento no se encuentra
                }

                return View(departamento);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar la página de edición: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(Departamento departamento)
        {
            try
            {
                // Busca el departamento correspondiente por su ID
                Departamento departamentoExistente = departamentos.FirstOrDefault(d => d.Id == departamento.Id);

                if (departamentoExistente == null)
                {
                    return HttpNotFound(); // Retorna un error 404 si el departamento no se encuentra
                }

                // Actualiza el departamento con los nuevos datos
                departamentoExistente.Nombre = departamento.Nombre;

                // Redirecciona al usuario a la vista Index u otra vista de tu elección
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al editar el departamento: " + ex.Message;
                return View("Error");
            }
        }

        // Eliminar un departamento existente
        public ActionResult Delete(Guid id)
        {
            try
            {
                // Busca el departamento correspondiente por su ID
                Departamento departamento = departamentos.FirstOrDefault(d => d.Id == id);

                if (departamento == null)
                {
                    return HttpNotFound(); // Retorna un error 404 si el departamento no se encuentra
                }

                // Muestra una vista de confirmación de eliminación
                return View(departamento);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar la página de eliminación: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                // Busca el departamento correspondiente por su ID
                Departamento departamento = departamentos.FirstOrDefault(d => d.Id == id);

                if (departamento == null)
                {
                    return HttpNotFound(); // Retorna un error 404 si el departamento no se encuentra
                }

                // Elimina el departamento de la lista
                departamentos.Remove(departamento);

                // Redirecciona al usuario a la vista Index u otra vista de tu elección
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al eliminar el departamento: " + ex.Message;
                return View("Error");
            }
        }
    }
}
