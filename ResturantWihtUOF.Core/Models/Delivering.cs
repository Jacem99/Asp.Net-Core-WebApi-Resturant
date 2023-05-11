using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
  public  class Delivering
    {
        [Key]
        public int Id { get; set; }
        public string Site { get; set; }

        public bool isDelivered { get; set; }

        //RelationShips
        public int CutomerOrderId { get; set; }
        [ForeignKey("CutomerOrderId")]
        public virtual Customers CutomerOrder { get; set; }

        public string DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public ApplicationUser Delivery { get; set; }

       public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
