using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Common
{

    public class BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
