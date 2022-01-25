using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rest_API.Model;

namespace Rest_API.Service
{
    /// <summary>
    /// Classe utilizada para gerar o token.
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            // A classe  JwtSecurityTokenHandler é utilizada para gerar um Token baseado
            // em algumas informações que podemos prover.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Essa é a chave privada (ou chave secreta) que se encontra no AppSettings.
            var secretKey = Encoding.ASCII.GetBytes(_configuration["secretKey"].ToString());

            // Estas informações são chamadas de SecurityTokenDescriptor e elas proveem
            // dentre outras opções, o usuário e senha, tornando-os também disponíveis no
            // ClaimIdentity do ASP.NET.
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim (ClaimTypes.Name, usuario.Nome.ToString()),
                }), 
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
