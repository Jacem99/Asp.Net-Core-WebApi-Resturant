using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        //RelationShips
       
        public virtual IEnumerable<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public virtual IEnumerable<Suppliers> Cheifs { get; } = new List<Suppliers>();
        public virtual ICollection<Suppliers> Suppliers { get; } = new List<Suppliers>();
        public virtual IEnumerable<WorkingPeriodUsers> WorkingPeriodUsers { get; } = new List<WorkingPeriodUsers>();

    }

}
