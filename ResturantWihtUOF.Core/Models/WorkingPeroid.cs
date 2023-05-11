using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class WorkingPeroid
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Period { get; set; }
        public virtual IEnumerable<WorkingPeriodUsers> WorkingPeriodUsers { get; } = new List<WorkingPeriodUsers>();

    }
}
