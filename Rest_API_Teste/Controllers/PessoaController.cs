using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API_Teste.Model;
using Microsoft.EntityFrameworkCore;

namespace Rest_API_Teste.Controllers
{
    [Route("v1/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pessoa>> Post([FromServices] Context.AppContext context, [FromBody] Pessoa model)
        {
            if (ModelState.IsValid) 
            {
                context.Pessoas.Add(model);
                await context.SaveChangesAsync();

                return Created(nameof(model), "Criado com sucesso!");
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pessoa>> Get([FromRoute] int id, [FromServices] Context.AppContext context)
        {
            var pessoas =  await context.Pessoas.Where(p=> p.PessoaId == id).FirstOrDefaultAsync();

            if (pessoas != null)
            { 
                return Ok(pessoas);
            }

            return NotFound();
        }
    }
}
