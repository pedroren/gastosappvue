using GastosAppCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GastosAppApi.Dto
{
    public class RepPresupuestoDetDto
    {
        public int PresupuestoId { get; set; }
        public int ConceptoId { get; set; }
        public string ConceptoNombre { get; set; }
        public decimal MontoPresupuesto { get; set; }
        public decimal? MontoEjecutado { get; set; }
    }
}
