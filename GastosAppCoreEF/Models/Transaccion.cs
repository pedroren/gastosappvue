using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosAppCoreEF.Models
{
    [Table("Transacciones")]
    public class Transaccion
    {
        public int TransaccionId { get; set; }

        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public int? ConceptoId { get; set; }
        public virtual Concepto Concepto { get; set; }

        [Required(ErrorMessage = "El Monto es requerido")]
        public decimal Monto { get; set; }

        public int CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }

        public int? CuentaIdTransf { get; set; }
        [DisplayName("Cta Destino")]
        [ForeignKey("CuentaIdTransf")]
        public virtual Cuenta CuentaTransf { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public int TipoTransaccionId { get; set; }
        [DisplayName("Tipo de Transacción")]
        public virtual TipoTransaccion TipoTransaccion { get; set; }

        [DisplayName("Tasa Conv.")]
        [DefaultValue(0)]
        public decimal TasaTransf { get; set; }

        [DisplayName("Monto Equiv.")]
        [DefaultValue(0)]
        public decimal MontoTransf { get; set; }

        //public virtual List<TransConceptoItem> TransConceptoItems { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [NotMapped]
        public virtual int? FacturaId { get; set; }
        [NotMapped]
        public virtual Factura Factura { get; set; }

        public virtual decimal MontoEquivalente
        {
            get {
                if (Cuenta != null && Cuenta.Moneda != null) { return (Monto * Cuenta.Moneda.Tasa); } else { return Monto; }
            }
        }

        public virtual int Ano
        {
            get { return Fecha.Year; }
        }

        public virtual int Mes
        {
            get { return Fecha.Month; }
        }

        public virtual string AnoMesCodigo
        {
            get { return Ano.ToString("0000") + "-" + Mes.ToString("00"); }
        }

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
