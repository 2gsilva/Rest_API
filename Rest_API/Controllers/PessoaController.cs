using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API.Model;
using Microsoft.EntityFrameworkCore;
using Rest_API.Context;
using Microsoft.AspNetCore.Authorization;

namespace Rest_API.Controllers
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
        [Authorize]
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
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Método para consultar pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pessoa>> Get([FromRoute] int id)
        {
            var pessoas = await _context.Pessoas.Where(p => p.PessoaId == id).FirstOrDefaultAsync();

            if (pessoas != null)
            {
                return Ok(pessoas);
            }

            return NotFound();
        }

        /// <summary>
        /// Método para alterar dados da pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Pessoa model)
        {
            var pessoas = await _context.Pessoas.Where(p => p.PessoaId == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                if (pessoas == null)
                {
                    return NotFound();
                }

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest();
        }

    }
}
