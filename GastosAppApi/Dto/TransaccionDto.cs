using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastosAppApi.Dto
{
    public class TransaccionDto
    {
        public int TransaccionId { get; set; }

        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public int? ConceptoId { get; set; }

        [Required(ErrorMessage = "El Monto es requerido")]
        public decimal Monto { get; set; }

        public int CuentaId { get; set; }

        public int? CuentaIdTransf { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public int TipoTransaccionId { get; set; }

        [DisplayName("Tasa Conv.")]
        [DefaultValue(0)]
        public decimal TasaTransf { get; set; }

        [DisplayName("Monto Equiv.")]
        [DefaultValue(0)]
        public decimal MontoTransf { get; set; }

        public int UsuarioId { get; set; }

        public virtual int? FacturaId { get; set; }

        public decimal MontoEquivalente { get; set; }
        public string AbreviaturaMoneda { get; set; }

        public int Ano { get; set; }

        public int Mes{ get; set; }

        public string AnoMesCodigo{ get; set; }

        public string ConceptoNombre{ get; set; }

        public string CuentaNombre{ get; set; }

        public string CuentaTransfNombre{ get; set; }
    }
}