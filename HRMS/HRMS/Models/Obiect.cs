using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Obiect
    {
        public int ObiectID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadż nazwę obiektu")]
        [StringLength(100)]
        public string NameObiect { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadż adres obiektu")]
        [StringLength(100)]
        public string AdressObiect { get; set; }

    }
}