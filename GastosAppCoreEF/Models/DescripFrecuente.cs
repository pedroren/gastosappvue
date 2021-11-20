using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GastosAppCoreEF.Models
{
    public class DescripFrecuente
    {
        public int DescripFrecuenteId { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        public int? ConceptoId { get; set; }
        public virtual Concepto Concepto { get; set; }
        
        public decimal Monto { get; set; }
        
        public int? CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        
        public int? CuentaIdTransf { get; set; }
        [DisplayName("Cuenta Destino")]
        [ForeignKey("CuentaIdTransf")]
        public virtual Cuenta CuentaTransf { get; set; }

        public bool Activo { get; set; }
        
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual string ConceptoNombre
        {
            get { if (Concepto != null) return Concepto.Nombre; else return ""; }
        }

        public virtual string CuentaNombre
        {
            get { if (Cuenta != null) return Cuenta.Nombre; else return ""; }
        }

        public virtual string CuentaTransfNombre
        {
            get { if (CuentaTransf != null) return CuentaTransf.Nombre; else return ""; }
        }
    }
}
