using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GastosAppCoreEF.Models
{
    public class TransImportMap
    {
        [Key]
        public  int TransImportMapId { get; set; }
        public  String Nombre { get; set; }
        public virtual IList<TransImportMapItem> Campos { get; set; }
        public  int SkipRows { get; set; }
        public  bool EsExcel { get; set; }
        public  String Separador { get; set; }
        public  String DateFormat { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
