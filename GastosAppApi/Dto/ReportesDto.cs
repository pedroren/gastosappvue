using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GastosAppApi.Dto
{
    public class ReportesDto
    {
    }

    public class SumConceptoItem
    {
        public int? ConceptoId { get; set; }
        public string ConceptoNombre { get; set; }
        public bool EsGasto { get; set; }
        public decimal Value { get; set; }
    }

    public class HistConcepto
    {
        [Key]
        public int? ConceptoId { get; set; }
        public string ConceptoNombre { get; set; }
        public IEnumerable<HistConceptoAnoMes> AnoMes { get; set; }
    }

    public class HistConceptoAnoMes
    {
        public string AnoMes { get; set; }

        public decimal TotalMonto { get; set; }
    }

    public class HistAnoMes
    {
        [Key]
        public string AnoMes { get; set; }
        public IEnumerable<HistAnoMesConcepto> Conceptos { get; set; }
    }

    public class HistAnoMesConcepto
    {
        [Key]
        public int? ConceptoId { get; set; }
        public string ConceptoNombre { get; set; }
        public decimal TotalMonto { get; set; }
    }

    public class SumConceptoAnoMes
    {
        public int? ConceptoId { get; set; }
        public string ConceptoNombre { get; set; }
        public string AnoMes { get; set; }
        public decimal TotalMonto { get; set; }
    }
}
