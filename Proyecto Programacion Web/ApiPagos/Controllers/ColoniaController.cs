using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaPagos.Models;

namespace ApiPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoniaController : ControllerBase
    {
        private readonly PaycontroldbContext _context;

        public ColoniaController(PaycontroldbContext context)
        {
            _context = context;
        }

        // GET: api/Colonia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colonium>>> GetColonia()
        {
          if (_context.Colonia == null)
          {
              return NotFound();
          }
            return await _context.Colonia.ToListAsync();
        }

        // GET: api/Colonia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colonium>> GetColonium(int id)
        {
          if (_context.Colonia == null)
          {
              return NotFound();
          }
            var colonium = await _context.Colonia.FindAsync(id);

            if (colonium == null)
            {
                return NotFound();
            }

            return colonium;
        }

        // PUT: api/Colonia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColonium(int id, Colonium colonium)
        {
            colonium.IdColonia= id;
            _context.Entry(colonium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoniumExists(id))
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

        // POST: api/Colonia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colonium>> PostColonium(Colonium colonium)
        {
          if (_context.Colonia == null)
          {
              return Problem("Entity set 'PaycontroldbContext.Colonia'  is null.");
          }
            _context.Colonia.Add(colonium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColonium", new { id = colonium.IdColonia }, colonium);
        }

        // DELETE: api/Colonia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColonium(int id)
        {
            if (_context.Colonia == null)
            {
                return NotFound();
            }
            var colonium = await _context.Colonia.FindAsync(id);
            if (colonium == null)
            {
                return NotFound();
            }

            _context.Colonia.Remove(colonium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColoniumExists(int id)
        {
            return (_context.Colonia?.Any(e => e.IdColonia == id)).GetValueOrDefault();
        }
    }
}
