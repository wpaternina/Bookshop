using Api.Interface;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Api.Security.Token
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string ObtenerUsuarioSesion()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}
