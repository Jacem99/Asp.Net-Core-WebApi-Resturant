using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Data
{
   public class MealData
    {

      
        public int Id { get; set; }
        [StringLength(60) , Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        //RelationShips
        [Required]
        public int IngredientId { get; set; }
    }
}
