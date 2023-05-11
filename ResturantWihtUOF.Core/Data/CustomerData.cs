using System;
using System.ComponentModel.DataAnnotations;


namespace ResturantWihtUOF.Core.Data
{
    public class CustomerData
    {
     
        public int CustomerId { get; set; }
      
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
        public Nullable< int> AddressId { get; set; }
       
     
    }
}
