using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Resturant
    {
        [Key]
        public string Name { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public int CountEmployees { get; set; }
    }
}
