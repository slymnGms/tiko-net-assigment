using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tiko_net_assignment.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        [Required]
        public string Name { get; set; }
        
        public virtual List<Agent> Agents { get; set; }
        public virtual List<House> Houses { get; set; }
    }
}
