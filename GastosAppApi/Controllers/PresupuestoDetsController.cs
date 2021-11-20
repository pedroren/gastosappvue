using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GastosAppCoreEF.DAL;
using GastosAppCoreEF.Models;

namespace GastosAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoDetsController : ControllerBase
    {
        private readonly GastosappContext _context;

        public PresupuestoDetsController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/PresupuestoDets?PresupuestoId=x
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int PresupuestoId)
        {
            return Ok(await _context.PresupuestoDets.Where(d => d.PresupuestoId == PresupuestoId)
                .ToListAsync());
        }

        // GET: api/PresupuestoDets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPresupuestoDet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presupuestoDet = await _context.PresupuestoDets.SingleOrDefaultAsync(m => m.PresupuestoDetId == id);

            if (presupuestoDet == null)
            {
                return NotFound();
            }

            return Ok(presupuestoDet);
        }

        // PUT: api/PresupuestoDets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresupuestoDet([FromRoute] int id, [FromBody] PresupuestoDet presupuestoDet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presupuestoDet.PresupuestoDetId)
            {
                return BadRequest();
            }

            _context.Entry(presupuestoDet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresupuestoDetExists(id))
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

        // POST: api/PresupuestoDets
        [HttpPost()]
        public async Task<IActionResult> PostPresupuestoDet([FromBody] PresupuestoDet presupuestoDet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PresupuestoDets.Add(presupuestoDet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPresupuestoDet", new { id = presupuestoDet.PresupuestoDetId }, presupuestoDet);
        }

        // DELETE: api/PresupuestoDets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresupuestoDet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presupuestoDet = await _context.PresupuestoDets.SingleOrDefaultAsync(m => m.PresupuestoDetId == id);
            if (presupuestoDet == null)
            {
                return NotFound();
            }

            _context.PresupuestoDets.Remove(presupuestoDet);
            await _context.SaveChangesAsync();

            return Ok(presupuestoDet);
        }

        private bool PresupuestoDetExists(int id)
        {
            return _context.PresupuestoDets.Any(e => e.PresupuestoDetId == id);
        }
    }
}