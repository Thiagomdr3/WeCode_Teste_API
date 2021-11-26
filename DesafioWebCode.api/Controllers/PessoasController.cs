using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioWebCode.api.Data;
using DesafioWebCode.api.Models;

namespace DesafioWebCode.api.Controllers
{
    /// <summary>
    /// class controller de pessoas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly Context_Db _context;
        /// <summary>
        /// context da classe controller de pessoas
        /// </summary>
        /// <param name="context"></param>
        public PessoasController(Context_Db context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos as pessoas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoas>>> GetPessoas()
        {
            return await _context.Pessoas.ToListAsync();
        }

        /// <summary>
        /// Localiza uma pessoa pelo id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoas>> GetPessoas(int id)
        {
            var pessoas = await _context.Pessoas.FindAsync(id);

            if (pessoas == null)
            {
                return NotFound();
            }

            return pessoas;
        }

        /// <summary>
        /// Altera uma pessoa pelo id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoas(int id, Pessoas pessoas)
        {
            if (id != pessoas.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Inclui uma pessoa.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Pessoas>> PostPessoas(Pessoas pessoas)
        {
            _context.Pessoas.Add(pessoas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoas", new { id = pessoas.Id }, pessoas);
        }

        /// <summary>
        /// Apaga uma pessoa pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoas(int id)
        {
            var pessoas = await _context.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoasExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
