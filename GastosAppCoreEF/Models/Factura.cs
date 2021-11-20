using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GastosAppCoreEF.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }
        
        [StringLength(100)]
        public string Nombre { get; set; }

        public int DescripFrecuenteId { get; set; }
        public virtual DescripFrecuente DescripFrecuente { get; set; }
        
        public string Frecuencia { get; set; }
        
        public int Dia { get; set; }
        
        public int Mes { get; set; }

        [Display(Name = "ProxFecha"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProxFecha { get; set; }

        public bool Activo { get; set; }

        [Display(Name = "UltFecha"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UltFecha { get; set; }

        public decimal UltMonto { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
