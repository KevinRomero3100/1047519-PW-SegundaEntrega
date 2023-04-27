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
    public class ClienteDireeccionController : ControllerBase
    {
        private readonly PaycontroldbContext _context;

        public ClienteDireeccionController(PaycontroldbContext context)
        {
            _context = context;
        }

        // GET: api/ClienteDireeccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDireeccion>>> GetClienteDireeccions()
        {
          if (_context.ClienteDireeccions == null)
          {
              return NotFound();
          }
            return await _context.ClienteDireeccions.ToListAsync();
        }

        // GET: api/ClienteDireeccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDireeccion>> GetClienteDireeccion(int id)
        {
          if (_context.ClienteDireeccions == null)
          {
              return NotFound();
          }
            var clienteDireeccion = await _context.ClienteDireeccions.FindAsync(id);

            if (clienteDireeccion == null)
            {
                return NotFound();
            }

            return clienteDireeccion;
        }

        // PUT: api/ClienteDireeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteDireeccion(int id, ClienteDireeccion clienteDireeccion)
        {
            if (id != clienteDireeccion.IdClienteDireeccion)
            {
                return BadRequest();
            }

            _context.Entry(clienteDireeccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteDireeccionExists(id))
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

        // POST: api/ClienteDireeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClienteDireeccion>> PostClienteDireeccion(ClienteDireeccion clienteDireeccion)
        {
          if (_context.ClienteDireeccions == null)
          {
              return Problem("Entity set 'PaycontroldbContext.ClienteDireeccions'  is null.");
          }
            _context.ClienteDireeccions.Add(clienteDireeccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClienteDireeccion", new { id = clienteDireeccion.IdClienteDireeccion }, clienteDireeccion);
        }

        // DELETE: api/ClienteDireeccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteDireeccion(int id)
        {
            if (_context.ClienteDireeccions == null)
            {
                return NotFound();
            }
            var clienteDireeccion = await _context.ClienteDireeccions.FindAsync(id);
            if (clienteDireeccion == null)
            {
                return NotFound();
            }

            _context.ClienteDireeccions.Remove(clienteDireeccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteDireeccionExists(int id)
        {
            return (_context.ClienteDireeccions?.Any(e => e.IdClienteDireeccion == id)).GetValueOrDefault();
        }
    }
}
