using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class TypeOfOrder
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string TypeOrder { get; set; }

        //RelationShips
        public virtual IEnumerable<Kitchen> Kitchens { get; set; }

    }
}
