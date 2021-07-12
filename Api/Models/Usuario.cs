using Microsoft.AspNetCore.Identity;

namespace Api.Models
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
