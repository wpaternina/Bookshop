using Api.Interface;
using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Security.Token
{
    public class JwtGenerador : IJwtGenerador
    {
        private static readonly string llave = "Esta es mi llave";
        public string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llave));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);

        }
    }
}
