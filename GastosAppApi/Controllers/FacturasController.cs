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
    [Route("api/Facturas")]
    public class FacturasController : ControllerBase
    {
        private readonly GastosappContext _context;

        public FacturasController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet()]
        public IEnumerable<Factura> GetFacturas(string usuario)
        {
            return _context.Facturas.Where(f => f.Usuario.Login == usuario).OrderBy(d => d.Nombre);
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactura([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factura = await _context.Facturas.SingleOrDefaultAsync(m => m.FacturaId == id);

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura([FromRoute] int id, [FromBody] Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.FacturaId)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        [HttpPost()]
        public async Task<IActionResult> PostFactura([FromBody] Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura", new { id = factura.FacturaId }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factura = await _context.Facturas.SingleOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return Ok(factura);
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaId == id);
        }

        [HttpPost("GetProxFechaFact")]
        public DateTime GetProxFechaFact([FromBody]Factura factura)
        {
            DateTime proxfecha;

            DateTime ultfecha;
            ultfecha = factura.UltFecha ?? DateTime.Today;
            /*if (ultfecha < factura.ProxFecha)
                ultfecha = factura.ProxFecha;*/
            proxfecha = GetProxFechaFact(factura.Frecuencia, ultfecha, factura.Dia, factura.Mes);

            return proxfecha;
        }

        public DateTime GetProxFechaFact(String frecuencia, DateTime ultFecha, int dia, int mes)
        {
            DateTime proxfecha;

            if (frecuencia == "M")
            {
                //Mensual
                try
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + ultFecha.Month.ToString() + "-" + dia.ToString());
                }
                catch
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + ultFecha.Month.ToString() + "-01").AddMonths(1).AddDays(-1);
                }
                if (proxfecha <= DateTime.Today || proxfecha <= ultFecha)
                {
                    proxfecha = proxfecha.AddMonths(1);
                }
            }
            else
            {
                //Anual
                try
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + mes.ToString() + "-" + dia.ToString());
                }
                catch
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + mes.ToString() + "-" + dia.ToString()).AddMonths(1).AddDays(-1);
                }
                if (proxfecha <= DateTime.Today || proxfecha <= ultFecha)
                {
                    proxfecha = proxfecha.AddYears(1);
                }
            }

            return proxfecha;
        }
    }
}