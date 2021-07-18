using Api.Services.Autor;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Autor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBaseApi
    {
        // http://localhost:5000/api/Autor
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<AutorDTO>>> ObtenerAutores() 
        {
            return await Mediator.Send(new ConsultaAutor.ListaAutor());
        }

        // http://localhost:5000/api/Autor
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Unit>> GuardarAutores(NuevoAutor.RegistrarAutor autor) 
        {
            return await Mediator.Send(autor);
        }

        // http://localhost:5000/api/Autor/{AutorId}
        [HttpPut ("{AutorId}")]
        //[Authorize]
        public async Task<ActionResult<Unit>> EditarAutores(int AutorId, EditarAutor.ModificarAutor autor) 
        {
            autor.AutorId = AutorId;
            return await Mediator.Send(autor);
        }

        // http://localhost:5000/api/Autor/{AutorId}
        [HttpDelete("{AutorId}")]
        //[Authorize]
        public async Task<ActionResult<Unit>> EliminarAutores(int AutorId) 
        {
            return await Mediator.Send(new BorrarAutor.EliminarAutor { AutorId = AutorId});
        }

        // http://localhost:5000/api/Autor/{AutorId}
        [HttpGet("{AutorId}")]
        //[Authorize]
        public async Task<ActionResult<AutorDTO>> ObtenerAutoresById(int AutorId)
        {
            return await Mediator.Send(new ConsultaAutorPorId.ListaAutorPorId { AutorId  = AutorId });
        }
    }
}
