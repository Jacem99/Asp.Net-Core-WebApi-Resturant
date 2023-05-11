using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
  public  class Meal
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60) , Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }

        //RelationShips

        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }
        public virtual IEnumerable<Kitchen> Kitchens { get;  } = new List<Kitchen>();
        public virtual IEnumerable<Order> Orders { get; } = new List<Order>();

    }
}
