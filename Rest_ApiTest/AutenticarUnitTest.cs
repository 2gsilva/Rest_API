using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rest_API.Controllers;
using Rest_API.Model;
using System;
using Xunit;

namespace Rest_ApiTest
{
    public class AutenticarUnitTest : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AutenticarUnitTest(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [Fact]
        public void Autenticar_UsernamePassword_Ok()
        {
            // Arrange
            
            var usuario = new Usuario() 
            {
                UsuarioId = 1,
                Nome = "guilherme",
                Senha = "123"
            };
            var resultado = true;

            // Act

            var retorno = new AutenticacaoController(_configuration).AutenticarAsync(usuario);
            
            // Assert

            Assert.Equal(resultado, retorno);
        }
    }
}
