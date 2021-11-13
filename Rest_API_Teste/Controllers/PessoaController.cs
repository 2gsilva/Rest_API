using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API_Teste.Model;
using Microsoft.EntityFrameworkCore;
using Rest_API_Teste.Context;

namespace Rest_API_Teste.Controllers
{
    [Route("v1/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly Context.AppContext _context;

        public PessoaController(Context.AppContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// Método para cadastrar pessoa
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pessoa>> Post([FromBody] Pessoa model)
        {
            if (ModelState.IsValid) 
            {
                _context.Pessoas.Add(model);
                await _context.SaveChangesAsync();

                return Created(nameof(model), "Criado com sucesso!");
            }

            return BadRequest();
        }

        /// <summary>
        /// Método para consultar pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pessoa>> Get([FromRoute] int id)
        {
            var pessoas =  await _context.Pessoas.Where(p=> p.PessoaId == id).FirstOrDefaultAsync();

            if (pessoas != null)
            { 
                return Ok(pessoas);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> Put(int id, [FromServices] Context.AppContext context, [FromBody] Pessoa model)
		public async Task<ActionResult> Put(int id, Pessoa pessoa)
        {
            var pessoas = await _context.Pessoas.Where(p => p.PessoaId == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            { 
                if (pessoas == null)
                {
                    return NotFound();
                }

                _context.Entry(pessoa).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest();    
        }

    }
}
