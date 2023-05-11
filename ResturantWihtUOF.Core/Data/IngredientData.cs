
using System;
using System.ComponentModel.DataAnnotations;

namespace ResturantWihtUOF.Core.Data

{
   public class IngredientData
    {
        
        public int Id { get; set; }
        [StringLength(60) , Required]
        public string Name { get; set; }
        [StringLength(100)]

        public string Description { get; set; }
        public Nullable<int> Kilo { get; set; }
        public Nullable<int> Litter { get; set; }

        //RelationShips
        public string CheifId { get; set; }
    }
}
