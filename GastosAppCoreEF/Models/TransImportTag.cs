using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GastosAppCoreEF.Models
{
    public class TransImportTag
    {
        [Key]
        public  int TransImportTagId { get; set; }
        public  String Tag { get; set; }
        public int ConceptoId { get; set; }
        public virtual Concepto Concepto { get; set; }
        public  String Descripcion { get; set; }
        public  int Orden { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
