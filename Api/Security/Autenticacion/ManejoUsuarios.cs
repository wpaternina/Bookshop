using Api.ErrorHandling;
using Api.Interface;
using Api.Models;
using Api.Persistences;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class ManejoUsuarios
    {
        public class RegistrarUsuario : IRequest<UsuarioData> 
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class ManejadorUsuario : IRequestHandler<RegistrarUsuario, UsuarioData>
        {
            private readonly BookshopContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

            public ManejadorUsuario(BookshopContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(RegistrarUsuario request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(u => u.Email == request.Email).AnyAsync();

                if (existe)
                {
                    throw new Error(HttpStatusCode.BadRequest, new { mensaje = "Existe un usuario registrado con este email" });
                }

                var existeUserName = await _context.Users.Where(u => u.UserName == request.UserName).AnyAsync();

                if (existeUserName)
                {
                    throw new Error(HttpStatusCode.BadRequest, new { mensaje = "Existe un usuario registrado con este nombre de usuario" });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario),
                        Username = usuario.UserName
                    };
                }

                throw new Exception("No se pudo registrar al nuevo usuario");
            }
        }
    }
}
