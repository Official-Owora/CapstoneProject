using CapstoneProject.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Domain.Entities
{
    public class Mentee : BaseEntity
    {
        public bool IsMatched { get; set; }
        [Key]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Mentor")]
        public string MentorId { get; set; }    
        public Mentor Mentor { get; set; }    
    }
}
