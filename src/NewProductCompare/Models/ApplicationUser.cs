using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductCompareDotNet.Models
{
    public class ApplicationUser : IdentityUser
    {

      
        public virtual ICollection<Product> Products { get; set; }



    }
}
