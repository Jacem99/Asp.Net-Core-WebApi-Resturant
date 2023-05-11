using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Data
{
  public  class OrderData
    {
      
        public int Id { get; set; }
        public int NumberOfOrder { get; set; }

        [Required]
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public bool Done { get; set; }


        //RelationShips
        [Required]
        public int TypeOfOrderId { get; set; }
        [Required]
        public int IngredientId { get; set; }
        [Required]
        public int MealId { get; set; }
        
    }
}
