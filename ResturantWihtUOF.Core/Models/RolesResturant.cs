using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class RolesResturant : IdentityRole
    {
        public Nullable<int> SectionsId { get; set; }
        [ForeignKey("SectionsId")]
        public virtual Sections Sections { get; set; }
    }
}
