
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantWihtUOF.Core.Models
{
  public  class WorkingPeriodUsers
    {
        [Required]
        public string WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public virtual ApplicationUser Worker{ get; set; }

        [Required]
        public int PeroidId { get; set; }
        [ForeignKey("PeroidId")]
        public virtual WorkingPeroid Peroid { get; set; }
    }
}
