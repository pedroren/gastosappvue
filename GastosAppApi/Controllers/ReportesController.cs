using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GastosAppApi.Dto;
using GastosAppCoreEF.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace GastosAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Reportes")]
    public class ReportesController : Controller
    {
        private IUnitOfWork rep;
        private readonly IMapper _mapper;

        public ReportesController(IUnitOfWork uow, IMapper mapper)
        {
            this.rep = uow;
            this._mapper = mapper;
        }

        [HttpPost("GetSumConcepto")]
        public IEnumerable<SumConceptoItem> GetSumConcepto([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.TransaccionRepository.Get(t => t.Usuario.Login == criteria.Usuario 
                && t.Fecha >= criteria.FechaDesde 
                && t.Fecha <= criteria.FechaHasta 
                && t.ConceptoId != null, null, "Concepto,Cuenta,Cuenta.Moneda");
            if(criteria.CuentaId != null)
            {
                transacciones = transacciones.Where(t => t.CuentaId == criteria.CuentaId || t.CuentaIdTransf == criteria.CuentaId);
            }
            //Para Hacer mas eficiente en dotnetcore 3
            var transList = transacciones.Select(t => new { t.TransaccionId, t.ConceptoId, t.Concepto.Nombre, t.Concepto.EsGasto, MontoEquivalente = t.Monto * t.Cuenta.Moneda.Tasa });
            var result = transList.GroupBy(t => new { t.ConceptoId, t.Nombre, t.EsGasto})
                .Select(g => new SumConceptoItem() {
                    ConceptoId = g.Key.ConceptoId,
                    ConceptoNombre = g.Key.Nombre,
                    EsGasto = g.Key.EsGasto,
                    //Value = g.Sum(i => i.Monto * i.Cuenta.Moneda.Tasa)  //No soportado por dotnetcore 3
                    Value = g.Sum(i => i.MontoEquivalente)
                });

            return result.OrderBy(t => t.Value);
        }

        [HttpPost("GetSumCashFlow")]
        public IEnumerable<SumConceptoItem> GetSumCashFlow([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.vCashFlowRepository.Get(t => t.Usuario.Login == criteria.Usuario && t.Fecha >= criteria.FechaDesde && t.Fecha <= criteria.FechaHasta);
            var result = transacciones.GroupBy(t => new { t.ConceptoId, t.NombreConcepto })
                .Select(g => new SumConceptoItem() {
                    ConceptoId = g.Key.ConceptoId,
                    ConceptoNombre = g.Key.NombreConcepto,
                    EsGasto = false,
                    Value = g.Sum(i => i.Monto)
                });

            return result.OrderBy(t => t.Value);
        }

        [HttpPost("GetHistConcepto")]
        public IEnumerable<HistConcepto> GetHistConcepto([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.TransaccionRepository
                .Get(t => t.Usuario.Login == criteria.Usuario && t.Fecha >= criteria.FechaDesde && t.Fecha <= criteria.FechaHasta && t.ConceptoId != null, null, "Concepto");
            if (criteria.ConceptoId != null)
                transacciones = transacciones.Where(t => t.ConceptoId == criteria.ConceptoId);
            var transList = transacciones.ToList();
            var histconcepto = from t in transList
                               orderby t.Concepto.Nombre
                               group t by t.ConceptoId into g
                               select new HistConcepto
                               {
                                   ConceptoId = g.Key,
                                   AnoMes =
                                       from t in g
                                       orderby t.AnoMesCodigo
                                       group t by t.AnoMesCodigo into gm
                                       select new HistConceptoAnoMes { AnoMes = gm.Key, TotalMonto = gm.Sum(p => p.MontoEquivalente) }
                               };

            histconcepto = histconcepto.ToList();

            foreach (var item in histconcepto)
            {
                var concepto = rep.ConceptoRepository.GetByID(item.ConceptoId);
                item.ConceptoNombre = concepto.Nombre;
            }

            return histconcepto;
        }

        [HttpPost("GetHistAnoMes")]
        public IEnumerable<HistAnoMes> GetHistAnoMes([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.TransaccionRepository
                .Get(t => t.Usuario.Login == criteria.Usuario 
                && t.Fecha >= criteria.FechaDesde && t.Fecha <= criteria.FechaHasta && t.ConceptoId != null, null,
                "Concepto,Cuenta,Cuenta.Moneda");
            if (criteria.ConceptoId != null)
                transacciones = transacciones.Where(t => t.ConceptoId == criteria.ConceptoId);
            var transList = transacciones.ToList();
            var histconcepto = from t in transList
                               orderby t.AnoMesCodigo
                               group t by t.AnoMesCodigo into g
                               select new HistAnoMes
                               {
                                   AnoMes = g.Key,
                                   Conceptos =
                                       from t in g
                                       orderby t.ConceptoNombre
                                       group t by t.ConceptoNombre into gm
                                       select new HistAnoMesConcepto { ConceptoNombre = gm.Key, TotalMonto = gm.Sum(p => p.MontoEquivalente) }
                               };

            histconcepto = histconcepto.ToList();

            /*foreach (var item in histconcepto)
            {
                var concepto = rep.ConceptoRepository.GetByID(item.ConceptoId);
                item.ConceptoNombre = concepto.Nombre;
            }*/

            return histconcepto;
        }

        [HttpPost("GetSumConceptoAnoMes")]
        public IEnumerable<SumConceptoAnoMes> GetSumConceptoAnoMes([FromBody]TransFiltroCriteria criteria)
        {
            var transacciones = rep.TransaccionRepository
                .Get(t => t.Usuario.Login == criteria.Usuario && t.Fecha >= criteria.FechaDesde && t.Fecha <= criteria.FechaHasta && t.ConceptoId != null, null, "Concepto");
            if (criteria.ConceptoId != null)
                transacciones = transacciones.Where(t => t.ConceptoId == criteria.ConceptoId);
            var transList = transacciones.ToList();
            var histconcepto = from t in transList
                               orderby t.AnoMesCodigo
                               group t by new { t.AnoMesCodigo, t.ConceptoId, t.ConceptoNombre} into g
                               select new SumConceptoAnoMes
                               {
                                   ConceptoId = g.Key.ConceptoId,
                                   ConceptoNombre = g.Key.ConceptoNombre,
                                   AnoMes = g.Key.AnoMesCodigo,
                                   TotalMonto = g.Sum(p => p.MontoEquivalente)
                               };

            histconcepto = histconcepto.ToList();

            foreach (var item in histconcepto)
            {
                if (string.IsNullOrEmpty(item.ConceptoNombre))
                {
                    var concepto = rep.ConceptoRepository.GetByID(item.ConceptoId);
                    item.ConceptoNombre = concepto.Nombre;
                }
            }

            return histconcepto;
        }

        [HttpGet("GetRepPresupuesto")]
        public IEnumerable<RepPresupuestoDetDto> GetRepPresupuesto([FromQuery]int PresupuestoId)
        {
            var presup = rep.PresupuestoRepository.Get(filter: p => p.PresupuestoId == PresupuestoId, orderBy: null, includeProperties: "Usuario").FirstOrDefault();
            var pdet = rep.PresupuestoDetRepository.Get(p => p.PresupuestoId == presup.PresupuestoId)
                .Select(d => new RepPresupuestoDetDto
                {
                    PresupuestoId = presup.PresupuestoId,
                    ConceptoId = d.ConceptoId,
                    ConceptoNombre = d.Concepto.Nombre,
                    MontoPresupuesto = d.Monto
                }).ToList();
            
            foreach (var item in pdet)
            {
                var montoTrans = rep.TransaccionRepository.Get(t => t.Usuario.Login == presup.Usuario.Login
                    && t.Fecha >= presup.FechaDesde
                    && t.Fecha <= presup.FechaHasta
                    && t.ConceptoId == item.ConceptoId)
                    .Select(t => t.Monto * t.Cuenta.Moneda.Tasa).Sum();
                var conceptosHijos = rep.ConceptoRepository.Get(c => c.ConceptoPadreId == item.ConceptoId).ToList();
                foreach (var concepto in conceptosHijos)
                {
                    var montoTransHijo = rep.TransaccionRepository.Get(t => t.Usuario.Login == presup.Usuario.Login
                        && t.Fecha >= presup.FechaDesde
                        && t.Fecha <= presup.FechaHasta
                        && t.ConceptoId == concepto.ConceptoId)
                    .Select(t => t.Monto * t.Cuenta.Moneda.Tasa).Sum();
                    montoTrans += montoTransHijo;
                }
                item.MontoEjecutado = montoTrans*-1;
            }

            return pdet;
        }
    }

    
}