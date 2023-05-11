using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Data
{
  public class TypeOfReservationData
    {
        
        public int Id { get; set; }
        [Required]
        public string TypeReservation { get; set; }

    }
}
