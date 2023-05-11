using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Data
{
  public  class ReservationData
    {
      
        public int Id { get; set; }
        public bool Taken { get; set; }

        [Required]
        public int TypeOfReservationId { get; set; }
      
        public Nullable<int> CustomersId { get; set; }

        [Required]
        public string WaiterId { get; set; }

        public Nullable<int> TableId { get; set; }
        public Nullable<int> OrderId { get; set; }
       
    }
}
