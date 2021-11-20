using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GastosAppApi.Dto
{
    public class TransFiltroCriteria
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int? ConceptoId { get; set; }
        public int? CuentaId { get; set; }
        public string Usuario { get; set; }
    }
}
