
using System.ComponentModel.DataAnnotations;


namespace ResturantWihtUOF.Core.Data
{
   public class DeliveringData
    {
       
        public int Id { get; set; }
        public string Site { get; set; }

        public bool isDelivered { get; set; }

        //RelationShips

        [Required]
        public int CutomerOrderId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string DeliveryId { get; set; }


    }
}
