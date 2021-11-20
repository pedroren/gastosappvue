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
    public class CuentasController : ControllerBase
    {
        private IUnitOfWork rep;

        public CuentasController(IUnitOfWork uow)
        {
            this.rep = uow;
        }

        // GET api/<controller>/get
        [HttpOptions, HttpGet]
        public async Task<IActionResult> Get(string usuario)
        {
            var resultado = await rep.CuentaRepository
                .Get(filter: f => f.Usuario.Login == usuario, orderBy: q => q.OrderBy(d => d.Nombre), includeProperties: "Moneda")
                .ToListAsync();
            return Ok(resultado);
        }

        // GET api/<controller>/getmonedas
        /*[HttpOptions, HttpGet("getmonedas")]
        public async Task<IActionResult> GetMonedas(string usuario)
        {
            return Ok(await rep.MonedaRepository.Get(filter: f => f.Usuario.Login == usuario, orderBy: q => q.OrderBy(d => d.Nombre)).ToAsyncEnumerable<Moneda>().ToList());
        }*/

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public Cuenta GetById(int id)
        {
            return rep.CuentaRepository.GetByID(id);
        }
        
        // POST: api/Cuentas
        [HttpPost]
        public async Task<IActionResult> PostCuenta([FromBody]Cuenta record)
        {
            rep.CuentaRepository.Insert(record);
            await rep.SaveAsync();

            return CreatedAtAction("GetById", new { id = record.CuentaId }, record);
        }
        
        // PUT: api/Cuentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta([FromRoute] int id, [FromBody] Cuenta record)
        {
            rep.CuentaRepository.Update(record);
            await rep.SaveAsync();

            return Ok(record);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            rep.CuentaRepository.Delete(id);
            await rep.SaveAsync();

            return Ok(id);
        }
    }
}
