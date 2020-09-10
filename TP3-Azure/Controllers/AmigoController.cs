using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3_Azure.Data;
using TP3_Azure.Models;

namespace TP3_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        private readonly TP3_AzureContext _context;

        public AmigoController(TP3_AzureContext context)
        {
            _context = context;
        }

        // GET: api/Amigo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigos>>> GetAmigos()
        {
            return await _context.Amigos.ToListAsync();
        }

        // GET: api/Amigo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amigos>> GetAmigos(int id)
        {
            var amigos = await _context.Amigos.FindAsync(id);

            if (amigos == null)
            {
                return NotFound();
            }

            return amigos;
        }

        // PUT: api/Amigo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigos(int id, Amigos amigos)
        {
            if (id != amigos.Id)
            {
                return BadRequest();
            }

            _context.Entry(amigos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmigosExists(id))
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

        // POST: api/Amigo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amigos>> PostAmigos(Amigos amigos)
        {
            _context.Amigos.Add(amigos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmigos", new { id = amigos.Id }, amigos);
        }

        // DELETE: api/Amigo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigos>> DeleteAmigos(int id)
        {
            var amigos = await _context.Amigos.FindAsync(id);
            if (amigos == null)
            {
                return NotFound();
            }

            _context.Amigos.Remove(amigos);
            await _context.SaveChangesAsync();

            return amigos;
        }

        private bool AmigosExists(int id)
        {
            return _context.Amigos.Any(e => e.Id == id);
        }
    }
}
