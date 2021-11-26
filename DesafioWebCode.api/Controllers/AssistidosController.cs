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
    /// Classe controller de assistidos
    /// </summary>
    [Route("assistidos")]
    [ApiController]
    public class AssistidosController : ControllerBase
    {
        private readonly Context_Db _context;
        /// <summary>
        /// Context de assistidos
        /// </summary>
        /// <param name="context"></param>
        public AssistidosController(Context_Db context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os assistidos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assistidos>>> GetAssistidos()
        {
            return await _context.Assistidos.Include(x => x.Pessoas)
                .Include(x => x.Filmes)
                .ToListAsync();
        }

        /// <summary>
        /// Localiza um assistido pelo Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Assistidos>> GetAssistidos(int id)
        {
            var assistidos = await _context.Assistidos.FindAsync(id);
            _context.Assistidos.Include(x => x.Pessoas)
            .Include(x => x.Filmes)
            .ToList();

            if (assistidos == null)
            {
                return NotFound();
            }

            return assistidos;
        }

        /// <summary>
        /// Atualiza o assistido pelo id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssistidos(int id, Assistidos assistidos)
        {
            if (id != assistidos.Id)
            {
                return BadRequest();
            }

            _context.Entry(assistidos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssistidosExists(id))
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
        /// Inclui um novo assistido.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Assistidos>> PostAssistidos(Assistidos assistidos)
        {
            _context.Assistidos.Add(assistidos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssistidos", new { id = assistidos.Id }, assistidos);
        }

        /// <summary>
        /// Apaga o filme pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssistidos(int id)
        {
            var assistidos = await _context.Assistidos.FindAsync(id);
            if (assistidos == null)
            {
                return NotFound();
            }

            _context.Assistidos.Remove(assistidos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssistidosExists(int id)
        {
            return _context.Assistidos.Any(e => e.Id == id);
        }

        /// <summary>
        /// Lista a quantidade de pessoas por filme pelo Id do filme.
        /// </summary>

        [HttpGet]
        [Route("filme")]
        public async Task<ActionResult<IEnumerable<Assistidos>>> FilmesAssistidos(int id)
        {
            var assistidos = _context.Assistidos.Where(x => x.FilmesId == id)
            .Include(x => x.Pessoas)
            .Include(x => x.Filmes)
            .ToList();

            return assistidos;
        }

        /// <summary>
        /// Lista a quantidade de filmes por pessoas pelo Id da pessoa.
        /// </summary>
        [HttpGet]
        [Route("espectadores")]
        public async Task<ActionResult<IEnumerable<Assistidos>>> PessoasAssistidos(int id)
        {
            var assistidos = _context.Assistidos.Where(x => x.PessoasId == id)
            .Include(x => x.Pessoas)
            .Include(x => x.Filmes)
            .ToList();

            return assistidos;
        }
    }
}
