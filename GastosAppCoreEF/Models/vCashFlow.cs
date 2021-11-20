using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GastosAppCoreEF.Models
{
    [Table("vCashFlow")]
    public class vCashFlow
    {
        [Key]
        public string NombreConcepto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int? ConceptoId { get; set; }
        public virtual Concepto Concepto { get; set; }
        public int? CuentaIdTransf { get; set; }
        [ForeignKey("CuentaIdTransf")]
        public virtual Cuenta CuentaTransf { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
