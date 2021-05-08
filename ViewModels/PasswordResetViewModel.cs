using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Email Adresi Gereklidir.")]
        [EmailAddress]
        [Display(Name = "Email Adres:")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Parola Giriniz.")]
        [Display(Name = "Yeni Şifre:")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        public string PasswordNew { get; set; }
    }
}
