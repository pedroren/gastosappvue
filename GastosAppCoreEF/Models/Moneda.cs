using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GastosAppCoreEF.Models
{
    public class Moneda
    {
        public virtual int MonedaId { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }

        [DefaultValue(false)]
        public bool EsPrincipal { get; set; }

        [Range(0, 1000, ErrorMessage = "La tasa debe estar entre 0 y 1000")]
        public decimal Tasa { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
