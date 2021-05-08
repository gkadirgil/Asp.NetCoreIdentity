using ASP.NetCoreIdentity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Kullanıcı ismi gereklidir.")]
        [Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }

        [Display(Name = "Tel No:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name ="Email Adresiniz:")]
        [EmailAddress(ErrorMessage ="Email adresiniz doğru formatta değil.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifreniz gereklidir")]
        [Display(Name = "Şifre:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şehir:")]
        public string City { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi:")]
        public DateTime? BirthDay { get; set; }
        
       
        public string Picture { get; set; }

        [Display(Name = "Cinsiyet:")]
        public Gender Gender { get; set; }
    }
}
