using System.Runtime.Serialization;

namespace CapstoneProject.Domain.Enums
{
    public enum UserType
    {
        [EnumMember(Value = "Mentor")]
        Mentor = 1,
        [EnumMember(Value = "Mentee")]
        Mentee = 2
    }
}
