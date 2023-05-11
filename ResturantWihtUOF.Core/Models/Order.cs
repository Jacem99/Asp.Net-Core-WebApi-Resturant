using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfOrder { get; set; }

        [Required]
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public bool Done { get; set; }


        //RelationShips
        public int TypeOfOrderId { get; set; }
        [ForeignKey("TypeOfOrderId")]
        public virtual TypeOfOrder TypeOfOrder { get; set; }

        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }
        public int MealId { get; set; }
        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }
        public virtual IEnumerable<Fatora> Fatoras { get; } = new List<Fatora>();
        public virtual IEnumerable<Reservation> Reservations { get; } = new List<Reservation>();
        public virtual IEnumerable<Delivering> Deliverings { get; } = new List<Delivering>();
        public virtual IEnumerable<Kitchen> Kitchens { get; } = new List<Kitchen>();

    }
}
