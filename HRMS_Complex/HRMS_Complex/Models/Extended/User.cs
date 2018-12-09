using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HRMS_Complex.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name ="Imię")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Imię jest wymagane")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwisko jest wymagane")]
        public string Last_Name { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Emial jest wymagany")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "Hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Hasło musi zawierać minimum 6 znaków")]
        public string Password { get; set; }

        
        [Display(Name = "Potwierdz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Oba hasła muszą być takie same")]
        public string ConfirmPassword { get; set; }
    }

}