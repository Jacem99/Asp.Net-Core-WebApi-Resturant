using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Kitchen
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> billNumber { get; set; }
        public DateTime Time { get; set; }

        //RelationShips
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }


        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int MealId { get; set; }
        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        public int TypeOfOrderId { get; set; }
        [ForeignKey("TypeOfOrderId")]
        public virtual TypeOfOrder TypeOfOrder { get; set; }
    }
}
