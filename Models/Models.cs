using System.ComponentModel.DataAnnotations;

namespace InternsApi.Models
{
    public class Intern
    {
        public int Id { get; set; }
        
        [Required]
        public required string InternID { get; set; }
        
        [Required]
        public required string Name { get; set; }
        
        [Required]
        public required string Location { get; set; }
        
        [Required]
        public required string ProgrammingLanguage { get; set; }
        
        [Required]
        [EmailAddress]
        public required string OfficialEmailID { get; set; }
        
        [Required]
        public required string CollegeName { get; set; }
        
        [Required]
        public required string PrimarySkill { get; set; }
        
        [Required]
        public required string SecondarySkill { get; set; }
        
        [Required]
        public required string AreaOfInterest { get; set; }
        
        [Required]
        public required string BU { get; set; }
    }
}
