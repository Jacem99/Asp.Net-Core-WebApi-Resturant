using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Address
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string AdressName { get; set; }
    }
}
