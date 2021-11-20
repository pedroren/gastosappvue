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
    [Route("api/TransImportTags")]
    public class TransImportTagsController : Controller
    {
        private readonly GastosappContext _context;

        public TransImportTagsController(GastosappContext context)
        {
            _context = context;
        }

        // GET: api/TransImportTags
        [HttpGet]
        public IEnumerable<TransImportTag> GetTransImportTags()
        {
            return _context.TransImportTags;
        }

        [HttpGet("GetByUsuario")]
        public IEnumerable<TransImportTag> GetByUsuario(string usuario)
        {
            return _context.TransImportTags.Where(t => t.Usuario.Login == usuario);
        }
        
        // GET: api/TransImportTags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransImportTag([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportTag = await _context.TransImportTags.SingleOrDefaultAsync(m => m.TransImportTagId == id);

            if (transImportTag == null)
            {
                return NotFound();
            }

            return Ok(transImportTag);
        }

        // PUT: api/TransImportTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransImportTag([FromRoute] int id, [FromBody] TransImportTag transImportTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transImportTag.TransImportTagId)
            {
                return BadRequest();
            }

            _context.Entry(transImportTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransImportTagExists(id))
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

        // POST: api/TransImportTags
        [HttpPost]
        public async Task<IActionResult> PostTransImportTag([FromBody] TransImportTag transImportTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TransImportTags.Add(transImportTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransImportTag", new { id = transImportTag.TransImportTagId }, transImportTag);
        }

        // DELETE: api/TransImportTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransImportTag([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transImportTag = await _context.TransImportTags.SingleOrDefaultAsync(m => m.TransImportTagId == id);
            if (transImportTag == null)
            {
                return NotFound();
            }

            _context.TransImportTags.Remove(transImportTag);
            await _context.SaveChangesAsync();

            return Ok(transImportTag);
        }

        private bool TransImportTagExists(int id)
        {
            return _context.TransImportTags.Any(e => e.TransImportTagId == id);
        }
    }
}