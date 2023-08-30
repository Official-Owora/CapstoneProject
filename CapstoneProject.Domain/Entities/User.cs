using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Navigational Property
        /*[ForeignKey(nameof(Mentor))]
        public Mentor? Mentors { get; set; }*/
        //public ICollection<Mentee> Mentees { get; set; }
    }
}
