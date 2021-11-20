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
    public class MonedasController : ControllerBase
    {
        private readonly GastosappContext _context;

        public MonedasController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/Monedas
        //[HttpGet("getbyusuario")]
        public async Task<IActionResult> Get(string usuario)
        {
            return Ok(await _context.Monedas.Where(f => f.Usuario.Login == usuario).OrderBy(d => d.Nombre).ToListAsync());
        }

        // GET: api/Monedas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoneda([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moneda = await _context.Monedas.SingleOrDefaultAsync(m => m.MonedaId == id);

            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/Monedas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneda([FromRoute] int id, [FromBody] Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.MonedaId)
            {
                return BadRequest();
            }

            _context.Entry(moneda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaExists(id))
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

        // POST: api/Monedas
        //[HttpPost("PostMoneda")]
        [HttpPost]
        public async Task<IActionResult> PostMoneda([FromBody] Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Monedas.Add(moneda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneda", new { id = moneda.MonedaId }, moneda);
        }

        // DELETE: api/Monedas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneda([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moneda = await _context.Monedas.SingleOrDefaultAsync(m => m.MonedaId == id);
            if (moneda == null)
            {
                return NotFound();
            }

            _context.Monedas.Remove(moneda);
            await _context.SaveChangesAsync();

            return Ok(moneda);
        }

        private bool MonedaExists(int id)
        {
            return _context.Monedas.Any(e => e.MonedaId == id);
        }
    }
}