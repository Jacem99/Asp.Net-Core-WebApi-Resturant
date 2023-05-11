using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Models
{
   public class Fatora
    {
        [Key]
        public int FatoraId { get; set; }
        public DateTime Time { get; set; }
        //RalationShips

        public string CasheerId { get; set; }
        [ForeignKey("CasheerId")]
        public virtual ApplicationUser Casheer { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
