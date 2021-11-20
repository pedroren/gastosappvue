using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GastosAppCoreEF.DAL;
using GastosAppCoreEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConceptosController : ControllerBase
    {
        private IUnitOfWork rep;

        public ConceptosController(IUnitOfWork uow)
        {
            this.rep = uow;
        }

        // GET api/<controller>/getbyusuario
        [HttpOptions, HttpGet]
        public async Task<IActionResult> GetByUsuario(string usuario)
        {
            return Ok(await rep.ConceptoRepository.Get(filter: f => f.Usuario.Login == usuario, orderBy: q => q.OrderBy(d => d.Nombre)).ToListAsync());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Concepto GetById(int id)
        {
            return rep.ConceptoRepository.GetByID(id);
        }

        // POST: api/Conceptos
        [HttpPost]
        public async Task<IActionResult> PostConcepto([FromBody]Concepto record)
        {
            rep.ConceptoRepository.Insert(record);
            await rep.SaveAsync();

            return CreatedAtAction("GetById", new { id = record.ConceptoId }, record);
        }

        // PUT: api/Conceptos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcepto([FromRoute] int id, [FromBody] Concepto record)
        {
            rep.ConceptoRepository.Update(record);
            await rep.SaveAsync();

            return Ok(record);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            rep.ConceptoRepository.Delete(id);
            await rep.SaveAsync();

            return Ok(id);
        }
    }
}