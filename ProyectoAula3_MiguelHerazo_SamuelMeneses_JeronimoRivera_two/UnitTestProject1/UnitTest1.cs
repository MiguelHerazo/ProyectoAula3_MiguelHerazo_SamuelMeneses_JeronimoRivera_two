using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using TuProyecto.Controllers;
using TuProyecto.Models;

namespace TuProyecto.Tests
{
    [TestClass]
    public class IdeaNegocioControllerTests
    {
        [TestMethod]
        public void TestIndex_DebeRetornarVistaConListaDeIdeas()
        {
            // Arrange
            var controller = new IdeaNegocioController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model as List<IdeaNegocio>);
        }

        [TestMethod]
        public void TestCreate_Get_DebeRetornarVistaCreate()
        {
            // Arrange
            var controller = new IdeaNegocioController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreate_Post_ConDatosValidos_DebeRedirigirAIndex()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var nuevaIdea = new IdeaNegocio { Nombre = "Mi Idea", ImpactoSocial = "Alto", Inversion = 10000 };

            // Act
            var result = controller.Create(nuevaIdea) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestCreate_Post_ConDatosInvalidos_DebeRetornarVistaCreate()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var nuevaIdea = new IdeaNegocio(); // Datos inválidos

            // Act
            var result = controller.Create(nuevaIdea) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestEdit_Get_IdExistente_DebeRetornarVistaEditConIdea()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var ideaExistente = new IdeaNegocio { Id = 1, Nombre = "Idea Original", ImpactoSocial = "Medio", Inversion = 5000 };

            // Act
            var result = controller.Edit(ideaExistente.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model as IdeaNegocio);
        }

        [TestMethod]
        public void TestEdit_Get_IdNoExistente_DebeRetornarHttpNotFound()
        {
            // Arrange
            var controller = new IdeaNegocioController();

            // Act
            var result = controller.Edit(999) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEdit_Post_ConDatosValidos_DebeRedirigirAIndex()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var ideaExistente = new IdeaNegocio { Id = 1, Nombre = "Idea Original", ImpactoSocial = "Medio", Inversion = 5000 };

            // Act
            var result = controller.Edit(ideaExistente) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestEdit_Post_ConDatosInvalidos_DebeRetornarVistaEdit()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var ideaExistente = new IdeaNegocio { Id = 1, Nombre = "Idea Original", ImpactoSocial = "Medio", Inversion = 5000 };
            controller.ModelState.AddModelError("Nombre", "El nombre es requerido"); // Datos inválidos

            // Act
            var result = controller.Edit(ideaExistente) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void TestDelete_Get_IdExistente_DebeRetornarVistaDeleteConIdea()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var ideaExistente = new IdeaNegocio { Id = 1, Nombre = "Idea Original", ImpactoSocial = "Medio", Inversion = 5000 };

            // Act
            var result = controller.Delete(ideaExistente.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model as IdeaNegocio);
        }

        [TestMethod]
        public void TestDelete_Get_IdNoExistente_DebeRetornarHttpNotFound()
        {
            // Arrange
            var controller = new IdeaNegocioController();

            // Act
            var result = controller.Delete(999) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteConfirmed_DebeRedirigirAIndex()
        {
            // Arrange
            var controller = new IdeaNegocioController();
            var ideaExistente = new IdeaNegocio { Id = 1, Nombre = "Idea Original", ImpactoSocial = "Medio", Inversion = 5000 };

            // Act
            var result = controller.DeleteConfirmed(ideaExistente.Id) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
