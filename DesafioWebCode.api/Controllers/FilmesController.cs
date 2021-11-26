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
    /// classe Controller de filmes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly Context_Db _context;
        /// <summary>
        /// Context da class controller filmes
        /// </summary>
        /// <param name="context"></param>
        public FilmesController(Context_Db context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os filmes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filmes>>> GetFilmes()
        {
            return await _context.Filmes.ToListAsync();
        }

        /// <summary>
        /// Localiza um filme pelo id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Filmes>> GetFilmes(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);

            if (filmes == null)
            {
                return NotFound();
            }

            return filmes;
        }

        /// <summary>
        /// Atualiza o filme pelo id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmes(int id, Filmes filmes)
        {
            if (id != filmes.Id)
            {
                return BadRequest();
            }

            _context.Entry(filmes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmesExists(id))
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
        /// Inclui novo filme.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Filmes>> PostFilmes(Filmes filmes)
        {
            _context.Filmes.Add(filmes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmes", new { id = filmes.Id }, filmes);
        }

        /// <summary>
        /// Apaga o filme pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmes(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);
            if (filmes == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filmes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmesExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
