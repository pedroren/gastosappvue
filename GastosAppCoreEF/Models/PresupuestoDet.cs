using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastosAppCoreEF.Models
{
    public class PresupuestoDet
    {
        public int PresupuestoDetId { get; set; }

        public int PresupuestoId { get; set; }
        public virtual Presupuesto Presupuesto { get; set; }

        public int ConceptoId { get; set; }
        public virtual Concepto Concepto { get; set; }

        [Required(ErrorMessage = "El Monto es requerido")]
        public decimal Monto { get; set; }
    }
}
