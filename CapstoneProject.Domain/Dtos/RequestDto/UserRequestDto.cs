using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Roles Roles { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
