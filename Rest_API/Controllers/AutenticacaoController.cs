using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API.Model;
using Microsoft.EntityFrameworkCore;
using Rest_API.Context;
using Rest_API.Service;
using Microsoft.Extensions.Configuration;

namespace Rest_API.Controllers
{
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
       
        public AutenticacaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("v1/Autenticacao")]
        public async Task <ActionResult<dynamic>> AutenticarAsync([FromBody] Usuario model)
        { 
            // Recuperar o usuário
            var usuario = new UsuarioRespository().Get(model.Nome, model.Senha);

            // Verificar se o usuário existe.
            if (usuario == null)
            return NotFound(new{message = "Usuário ou senha inválidos"});

            // Gerar Token
            var token = new TokenService(_configuration).GerarToken(usuario);           

            // Retornar os dados
            return new 
            {
                usuario = usuario.Nome,
                token = token
            };
        }
    }
}
