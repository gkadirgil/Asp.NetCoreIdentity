using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name="Role:")]
        [Required(ErrorMessage ="Role ismi gereklidir!")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
