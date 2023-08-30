using CapstoneProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public string ImageURL { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
