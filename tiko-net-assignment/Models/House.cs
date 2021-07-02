using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tiko_net_assignment.Models
{
    public class House
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Address { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "BedroomCount Must be between 0 to 10")]
        public int BedroomCount { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int AgentId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
    }
}
