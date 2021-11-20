using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GastosAppCoreEF.Models
{
    [Table("Conceptos")]
    public class Concepto
    {
        public int ConceptoId { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(101)]
        public string Nombre { get; set; }

        [Column("ConceptoPadre_ConceptoId")]
        public int? ConceptoPadreId { get; set; }

        [DisplayName("Concepto Padre")]
        [ForeignKey("ConceptoPadreId")]
        public virtual Concepto ConceptoPadre { get; set; }

        [DefaultValue(true)]
        public bool EsGasto { get; set; }

        public bool EsFrecuente { get; set; }

        public bool Activo { get; set; }

        public int UsuarioId { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        public virtual string NombreConPadre { get { if (ConceptoPadre != null) { return ConceptoPadre.Nombre + " - " + Nombre; } else { return Nombre; } } }
    }
}
