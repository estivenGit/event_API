using NUnit.Framework;
using Moq;
using API.Controllers;
using LogicaNegocio.Services;
using AccesoDatos.Repositories;
using Transversal.Models;
using LogicaNegocio.Services.Evento;
using System.Web.Http.Results;


namespace Api.Tests
{
    [TestFixture]
    public class EventoControllerTest
    {
        private Mock<IEventoService> _mockEventoService;
        private EventoController _controller;

        [SetUp]
        public void Setup()
        {
            _mockEventoService = new Mock<IEventoService>();
            _controller = new EventoController(_mockEventoService.Object);
        }

        [Test]
        public async void GetEventById_EventExists_ReturnsOkResult()
        {
            // Arrange
            var eventoID = 3;
            var evento = new EventoModel { IdEvento = eventoID, Nombre = "Evento de prueba" };
            _mockEventoService.Setup(s => s.GetEventByIdAsync(eventoID)).ReturnsAsync(evento);

            // Act
            var result = await _controller.GetEventById(eventoID);

            // Assert
            Assert.That(result, Is.Not.Null); 
            var okResult = result as OkNegotiatedContentResult<EventoModel>;
            Assert.That(okResult, Is.Not.Null); 
            Assert.That(okResult.Content, Is.Not.Null); 
            Assert.That(okResult.Content.Nombre, Is.EqualTo(evento.Nombre)); 
        }
    }
}
