using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tiko_net_assignment.Models
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual List<House> Houses { get; set; }
    }
}
