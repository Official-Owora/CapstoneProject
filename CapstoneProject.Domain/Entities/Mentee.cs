using CapstoneProject.Domain.Common;
using CapstoneProject.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Domain.Entities
{
    public class Mentee : BaseEntity
    {
        public bool IsMatched { get; set; }
        //Navigational Property
        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
