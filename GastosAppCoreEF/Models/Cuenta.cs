using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GastosAppCoreEF.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int MonedaId { get; set; }
        public virtual Moneda Moneda { get; set; }
        
        public int TipoCuentaId { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        
        public decimal Balance { get; set; }

        public bool Activo { get; set; }
        
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
