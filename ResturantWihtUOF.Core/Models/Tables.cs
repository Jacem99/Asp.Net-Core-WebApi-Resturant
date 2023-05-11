using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Tables
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }
        public int TotalPeaple { get; set; }

        //RelationShips
       /* public virtual IEnumerable<Kitchen> Kitchens { get; set; }*/
        public virtual IEnumerable<Order> Orders { get; } = new List<Order>();

    }
}
