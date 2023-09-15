using System.Runtime.Serialization;

namespace CapstoneProject.Domain.Enums
{
    public enum YearsOfExperience
    {
        [EnumMember(Value = "Less Than One Year")]
        LessThanOneYear = 1,
        [EnumMember(Value = "One Year")]
        OneYear = 2,
        [EnumMember(Value = "Two Years")]
        TwoYear = 3,
        [EnumMember(Value = "Three Years")]
        ThreeYear = 4,
        [EnumMember(Value = "Four Years")]
        FourYears = 5,
        [EnumMember(Value = "Five Years")]
        FiveYears = 6,
        [EnumMember(Value = "Six Years")]
        SixYears = 7,
        [EnumMember(Value = "Seven Years")]
        SevenYears = 8,
        [EnumMember(Value = "Eight Years")]
        EightYears = 9,
        [EnumMember(Value = "Nine Years")]
        NineYears = 10,
        [EnumMember(Value = "Ten Years")]
        TenYears = 11,
        [EnumMember(Value = "More Than Ten Years")]
        MoreThanTenYears = 12
    }
}
