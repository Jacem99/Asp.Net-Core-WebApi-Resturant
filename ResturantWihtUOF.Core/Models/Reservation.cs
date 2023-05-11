using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
  public  class Reservation
    {
        [Key]
        public int Id { get; set; }
        public bool Taken { get; set; }

        public int TypeOfReservationId { get; set; }

        [ForeignKey("TypeOfReservationId")]
        public virtual TypeOfReservation TypeOfReservation { get; set; }


        public Nullable<int> CustomersId { get; set; }

        [ForeignKey("CustomersId")]
        public virtual Customers Customers { get; set; }

        public string WaiterId { get; set; }
        [ForeignKey("WaiterId")]
        public virtual ApplicationUser Waiter { get; set; }

        public Nullable<int> TableId { get; set; }

        [ForeignKey("TableId")]
        public virtual Tables Tables { get; set; }

        public Nullable<int> OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        /* public static DateTime StartTime { get; set; }

         public DateTime EndTime = StartTime.AddHours(3);*/
    }
}
