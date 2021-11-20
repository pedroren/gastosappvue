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
    public class PresupuestosController : ControllerBase
    {
        private readonly GastosappContext _context;

        public PresupuestosController(GastosappContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get(string usuario)
        {
            return Ok(await _context.Presupuestos.Where(f => f.Usuario.Login == usuario).OrderByDescending(d => d.FechaDesde)
                .ToListAsync());
        }

        // GET: api/Presupuestos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPresupuesto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presupuesto = await _context.Presupuestos.SingleOrDefaultAsync(m => m.PresupuestoId == id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return Ok(presupuesto);
        }

        // PUT: api/Presupuestos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresupuesto([FromRoute] int id, [FromBody] Presupuesto presupuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presupuesto.PresupuestoId)
            {
                return BadRequest();
            }

            _context.Entry(presupuesto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresupuestoExists(id))
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

        // POST: api/Presupuestos
        [HttpPost]
        public async Task<IActionResult> PostPresupuesto([FromBody]Presupuesto presupuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPresupuesto", new { id = presupuesto.PresupuestoId }, presupuesto);
        }

        // DELETE: api/Presupuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresupuesto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presupuesto = await _context.Presupuestos.SingleOrDefaultAsync(m => m.PresupuestoId == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();

            return Ok(presupuesto);
        }

        private bool PresupuestoExists(int id)
        {
            return _context.Presupuestos.Any(e => e.PresupuestoId == id);
        }
    }
}