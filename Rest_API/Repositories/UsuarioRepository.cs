using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Model
{
    public class UsuarioRespository
    {
        public Usuario Get(string nome, string senha)
        {
            // Obter usuários do banco de dados
            // var usuarios = AppContext.Usuarios().ToList();
            
            // Como ainda não estou conectado ao banco, vou definir os usuários no arquivo
            var usuarios = new List<Usuario>();
            usuarios.Add( new Usuario{ UsuarioId = 1 , Nome = "guilherme", Senha = "123"} );
            usuarios.Add( new Usuario{ UsuarioId = 2, Nome = "vanessa", Senha = "123"});

            return usuarios.Where(x => x.Nome == nome && x.Senha == senha).FirstOrDefault();
        }
    }
}