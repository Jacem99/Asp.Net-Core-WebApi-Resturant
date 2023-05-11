using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Sections
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string Name { get; set; }
        public virtual IEnumerable<RolesResturant> RolesResturants { get; } = new List<RolesResturant>();
    }
}
