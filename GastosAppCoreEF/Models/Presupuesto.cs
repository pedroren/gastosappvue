using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GastosAppCoreEF.Models
{
    public class Presupuesto
    {
        public int PresupuestoId { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(101)]
        public string Nombre { get; set; }

        [Display(Name = "FechaDesde"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "FechaHasta"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaHasta { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
