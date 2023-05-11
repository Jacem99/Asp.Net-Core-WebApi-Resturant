using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
    public class SupplierIngredientProvides
    {
        [Key]
        public int Id { get; set; }

        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Suppliers Supplier { get; set; }

        public Nullable<int> Kilo { get; set; }
        public Nullable<int> Litter { get; set; }

    }
}
