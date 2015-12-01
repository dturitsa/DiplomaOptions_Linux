using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWebSite.ViewModels.Account
{
    public class CreateViewModel
    {

        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

    }
}
