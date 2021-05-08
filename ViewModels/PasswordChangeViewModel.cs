using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Eski şifreniz gereklidir.")]
        [Display(Name = "Eski Şifre:")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        public string PasswordOld { get; set; }


        [Required(ErrorMessage = "Yeni şifreniz gereklidir.")]
        [Display(Name = "Yeni Şifre:")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        public string PasswordNew { get; set; }



        [Required(ErrorMessage = "Onay yeni şifreniz gereklidir.")]
        [Display(Name = "Onay Yeni Şifre:")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        [Compare("PasswordNew",ErrorMessage ="Yeni şifreniz ve onay şifreniz uyuşmuyor!")]
        public string PasswordConfirm { get; set; }
    }
}
