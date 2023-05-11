using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class TypeOfReservation
    {
        [Key]
        public int Id { get; set; }

        [Required , MaxLength(60)]
        public string TypeReservation { get; set; }

        //RelationShips
        public virtual IEnumerable<Reservation> Reservation { get; } = new List<Reservation>();
    }
}
