using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
    public class Ingredient
    {

        [Key]
        public int Id { get; set; }
        [MaxLength(60) , Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public Nullable<int> Kilo { get; set; }
        public Nullable<int> Litter { get; set; }


        //RelationShips
        public string CheifId { get; set; }
        [ForeignKey("CheifId")]
        public virtual ApplicationUser Cheif { get; set; }
        public virtual IEnumerable<Kitchen> Kitchens { get; set; } = new List<Kitchen>();
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public virtual IEnumerable<Meal> Meals { get; set; } = new List<Meal>();

        public virtual IEnumerable<SupplierIngredientProvides> SupplierIngredientProvides { get; } = new List<SupplierIngredientProvides>();

    }
}
