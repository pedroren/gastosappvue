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
    [Route("api/TransImportMaps")]
    public class TransImportMapsController : Controller
    {
        private readonly GastosappContext _context;

        public TransImportMapsController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/TransImportMaps
        [HttpGet]
        public IEnumerable<TransImportMap> GetTransImportMaps()
        {
            return _context.TransImportMaps;
        }

        [HttpGet("GetByUsuario")]
        public IEnumerable<TransImportMap> GetByUsuario(string usuario)
        {
            return _context.TransImportMaps.Where(t => t.Usuario.Login == usuario);
        }

        // GET: api/TransImportMaps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransImportMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportMap = await _context.TransImportMaps.SingleOrDefaultAsync(m => m.TransImportMapId == id);

            if (transImportMap == null)
            {
                return NotFound();
            }

            return Ok(transImportMap);
        }

        // PUT: api/TransImportMaps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransImportMap([FromRoute] int id, [FromBody] TransImportMap transImportMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transImportMap.TransImportMapId)
            {
                return BadRequest();
            }

            _context.Entry(transImportMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransImportMapExists(id))
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

        // POST: api/TransImportMaps
        [HttpPost]
        public async Task<IActionResult> PostTransImportMap([FromBody] TransImportMap transImportMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TransImportMaps.Add(transImportMap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransImportMap", new { id = transImportMap.TransImportMapId }, transImportMap);
        }

        // DELETE: api/TransImportMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransImportMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportMap = await _context.TransImportMaps.SingleOrDefaultAsync(m => m.TransImportMapId == id);
            if (transImportMap == null)
            {
                return NotFound();
            }

            _context.TransImportMaps.Remove(transImportMap);
            await _context.SaveChangesAsync();

            return Ok(transImportMap);
        }

        private bool TransImportMapExists(int id)
        {
            return _context.TransImportMaps.Any(e => e.TransImportMapId == id);
        }
    }
}