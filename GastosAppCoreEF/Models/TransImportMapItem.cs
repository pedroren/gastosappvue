using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GastosAppCoreEF.Models
{
    public class TransImportMapItem
    {
        [Key]
        public  int TransImportMapItemId { get; set; }
        public int TransImportMapId { get; set; }
        public virtual TransImportMap TransImportMap { get; set; }
        public  String Nombre { get; set; }
        public  int Posicion { get; set; }
    }
}
