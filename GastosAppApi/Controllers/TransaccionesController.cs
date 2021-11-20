using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GastosAppApi.Dto;
using GastosAppCoreEF.DAL;
using GastosAppCoreEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace GastosAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private IUnitOfWork rep;
        private readonly IMapper _mapper;

        public TransaccionesController(IUnitOfWork uow, IMapper mapper)
        {
            this.rep = uow;
            this._mapper = mapper;
        }

        // GET api/<controller>/getbyusuario
        [HttpOptions, HttpGet("getbyusuario"), HttpPost]
        public IEnumerable<Transaccion> GetByUsuario(string usuario)
        {
            var resultado = rep.TransaccionRepository.Get(filter: f => f.Usuario.Login == usuario, orderBy: q => q.OrderBy(d => d.Fecha)).ToList();
            return resultado;
        }

        // GET api/<controller>/id
        [HttpGet("{id}")]
        public Transaccion GetById(int id)
        {
            var resultado = rep.TransaccionRepository.GetByID(id);
            return resultado;
        }

        // GET api/<controller>/gettipocuentas
        [HttpOptions, HttpGet("gettipotrans")]
        public IEnumerable<TipoTransaccion> GetTipoTrans()
        {
            return rep.TipoTransaccionRepository.Get();
        }

        // GET api/<controller>/getnewrecord
        [HttpOptions, HttpGet("getnewrecord")]
        public Transaccion GetNewRecord()
        {
            return rep.GetNewTransaccionRecord();
        }

        [HttpPost("GetTransaccionesUsuario")]
        public IEnumerable<TransaccionDto> GetTransaccionesUsuario([FromBody]TransFiltroCriteria criteria)
        {
            var data = rep.FindTransByFechaByUsuario(criteria.Usuario, criteria.FechaDesde, criteria.FechaHasta, criteria.ConceptoId, criteria.CuentaId);
            return _mapper.ProjectTo<TransaccionDto>(data).ToList();
        }

        // POST: api/Monedas
        [HttpPost("PostTransaccion")]
        public Transaccion PostTransaccion([FromBody] Transaccion record)
        {
            return rep.AddTransaccion(record);
        }

        [HttpPut("{id}")]
        public IActionResult PutTransaccion([FromRoute] int id, [FromBody] Transaccion record)
        {
            rep.UpdateTransaccion(record);
            return Ok(record);
        }

        [HttpPost("GetTotalConcepto")]
        public decimal GetTotalConcepto([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.FindTransByFechaByUsuario(criteria.Usuario, criteria.FechaDesde, criteria.FechaHasta, criteria.ConceptoId, criteria.CuentaId);
            return transacciones.Select(t => (decimal?)t.Monto * t.Cuenta.Moneda.Tasa).Sum() ?? 0.0M;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            rep.DeleteTransaccion(id);
            await rep.SaveAsync();

            return Ok(id);
        }
    }

    
}