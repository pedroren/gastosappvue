using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GastosAppCoreEF.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public String Login { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
    }
}
