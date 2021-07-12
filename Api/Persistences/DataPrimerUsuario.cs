using Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Persistences
{
    public class DataPrimerUsuario
    {
        public static async Task InsertarData(BookshopContext context, UserManager<Usuario> usuarioManager) 
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "William Alberto Paternina Romo",
                    UserName = "wpaternina",
                    PhoneNumber = "3053073278",
                    Email = "ingwillianpaternina2101@gmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "WA1369pr$");
            }
        }
    }
}
