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
    public class DireccionController : ControllerBase
    {
        private readonly PaycontroldbContext _context;

        public DireccionController(PaycontroldbContext context)
        {
            _context = context;
        }

        // GET: api/Direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccion>>> GetDireccions()
        {
          if (_context.Direccions == null)
          {
              return NotFound();
          }
            return await _context.Direccions.ToListAsync();
        }

        // GET: api/Direccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccion>> GetDireccion(int id)
        {
          if (_context.Direccions == null)
          {
              return NotFound();
          }
            var direccion = await _context.Direccions.FindAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        // PUT: api/Direccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion(int id, Direccion direccion)
        {
            if (id != direccion.IdDireccion)
            {
                return BadRequest();
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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

        // POST: api/Direccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Direccion>> PostDireccion(Direccion direccion)
        {
          if (_context.Direccions == null)
          {
              return Problem("Entity set 'PaycontroldbContext.Direccions'  is null.");
          }
            _context.Direccions.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDireccion", new { id = direccion.IdDireccion }, direccion);
        }

        // DELETE: api/Direccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion(int id)
        {
            if (_context.Direccions == null)
            {
                return NotFound();
            }
            var direccion = await _context.Direccions.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccions.Remove(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DireccionExists(int id)
        {
            return (_context.Direccions?.Any(e => e.IdDireccion == id)).GetValueOrDefault();
        }
    }
}
