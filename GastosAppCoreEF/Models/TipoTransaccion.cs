using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GastosAppCoreEF.Models
{
    [Table("TipoTransacciones")]
    public class TipoTransaccion
    {
        public int TipoTransaccionId { get; set; }
        public string Nombre { get; set; }
    }
}
