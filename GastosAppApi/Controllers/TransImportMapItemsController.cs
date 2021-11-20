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
    [Route("api/TransImportMapItems")]
    public class TransImportMapItemsController : Controller
    {
        private readonly GastosappContext _context;

        public TransImportMapItemsController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/TransImportMapItems
        [HttpGet]
        public IEnumerable<TransImportMapItem> GetTransImportMapItems()
        {
            return _context.TransImportMapItems;
        }

        [HttpGet("GetByMaster")]
        public IEnumerable<TransImportMapItem> GetByMaster(int TransImportMapId)
        {
            return _context.TransImportMapItems.Where(t => t.TransImportMapId == TransImportMapId);
        }

        // GET: api/TransImportMapItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransImportMapItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportMapItem = await _context.TransImportMapItems.SingleOrDefaultAsync(m => m.TransImportMapItemId == id);

            if (transImportMapItem == null)
            {
                return NotFound();
            }

            return Ok(transImportMapItem);
        }

        // PUT: api/TransImportMapItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransImportMapItem([FromRoute] int id, [FromBody] TransImportMapItem transImportMapItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transImportMapItem.TransImportMapItemId)
            {
                return BadRequest();
            }

            _context.Entry(transImportMapItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransImportMapItemExists(id))
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

        // POST: api/TransImportMapItems
        [HttpPost]
        public async Task<IActionResult> PostTransImportMapItem([FromBody] TransImportMapItem transImportMapItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TransImportMapItems.Add(transImportMapItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransImportMapItem", new { id = transImportMapItem.TransImportMapItemId }, transImportMapItem);
        }

        // DELETE: api/TransImportMapItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransImportMapItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportMapItem = await _context.TransImportMapItems.SingleOrDefaultAsync(m => m.TransImportMapItemId == id);
            if (transImportMapItem == null)
            {
                return NotFound();
            }

            _context.TransImportMapItems.Remove(transImportMapItem);
            await _context.SaveChangesAsync();

            return Ok(transImportMapItem);
        }

        private bool TransImportMapItemExists(int id)
        {
            return _context.TransImportMapItems.Any(e => e.TransImportMapItemId == id);
        }
    }
}