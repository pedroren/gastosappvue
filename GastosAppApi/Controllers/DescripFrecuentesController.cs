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
    public class DescripFrecuentesController : Controller
    {
        private readonly GastosappContext _context;

        public DescripFrecuentesController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/DescripFrecuentes
        [HttpGet]
        public IEnumerable<DescripFrecuente> GetDescripFrecuentes(string usuario)
        {
            return _context.DescripFrecuentes.Where(f => f.Usuario.Login == usuario).OrderBy(d => d.Nombre);
        }

        // GET: api/DescripFrecuentes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDescripFrecuente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var descripFrecuente = await _context.DescripFrecuentes
                .Include(df => df.CuentaTransf)
                .Include(df => df.CuentaTransf.Moneda)
                .SingleOrDefaultAsync(m => m.DescripFrecuenteId == id);

            if (descripFrecuente == null)
            {
                return NotFound();
            }

            return Ok(descripFrecuente);
        }

        // PUT: api/DescripFrecuentes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescripFrecuente([FromRoute] int id, [FromBody] DescripFrecuente descripFrecuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != descripFrecuente.DescripFrecuenteId)
            {
                return BadRequest();
            }

            _context.Entry(descripFrecuente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescripFrecuenteExists(id))
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

        // POST: api/DescripFrecuentes
        [HttpPost]
        public async Task<IActionResult> PostDescripFrecuente([FromBody] DescripFrecuente descripFrecuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DescripFrecuentes.Add(descripFrecuente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDescripFrecuente", new { id = descripFrecuente.DescripFrecuenteId }, descripFrecuente);
        }

        // DELETE: api/DescripFrecuentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescripFrecuente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var descripFrecuente = await _context.DescripFrecuentes.SingleOrDefaultAsync(m => m.DescripFrecuenteId == id);
            if (descripFrecuente == null)
            {
                return NotFound();
            }

            _context.DescripFrecuentes.Remove(descripFrecuente);
            await _context.SaveChangesAsync();

            return Ok(descripFrecuente);
        }

        [HttpPost("DisableDescripFrecuente/{id}")]
        public async Task<IActionResult> DisableDescripFrecuente([FromRoute] int id)
        {
            var record = await _context.DescripFrecuentes.SingleOrDefaultAsync(m => m.DescripFrecuenteId == id);
            if (record == null)
            {
                return NotFound();
            }

            record.Activo = !record.Activo;
            await _context.SaveChangesAsync();

            return Ok(record);
        }

        private bool DescripFrecuenteExists(int id)
        {
            return _context.DescripFrecuentes.Any(e => e.DescripFrecuenteId == id);
        }
    }
}