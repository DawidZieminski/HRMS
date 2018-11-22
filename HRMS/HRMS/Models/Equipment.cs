using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Equipment {
        public int EquipmentID { get; set; }
        public int ObiectID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadż nazwę urządzenia")]
        [StringLength(100)]
        public string NameEquipment { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj ilość")]
        public int QuantityEquipment { get; set; }

        public virtual Obiect Obiect { get; set; }
    }
}