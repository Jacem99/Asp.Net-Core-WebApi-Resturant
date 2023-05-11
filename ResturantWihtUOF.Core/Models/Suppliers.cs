using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
    public class Suppliers
    {
        [Key]
        public int Id { get; set; }

        //RelationShips
        [Required]
        public string SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual ApplicationUser Supplier { get; set; }

        public string ChiefId { get; set; }
        [ForeignKey("ChiefId")]
        public virtual ApplicationUser Chief { get; set; }

        public virtual IEnumerable<SupplierIngredientProvides> SupplierIngredientProvides { get; } = new List<SupplierIngredientProvides>();
    }
}
