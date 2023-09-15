using System.Runtime.Serialization;

namespace CapstoneProject.Domain.Enums
{
    public enum MentorshipDuration
    {
        [EnumMember(Value = "One Month")]
        OneMonth = 1,
        [EnumMember(Value = "Two Months")]
        TwoMonths = 2,
        [EnumMember(Value = "Three Months")]
        ThreeMonths = 3,
        [EnumMember(Value = "Four Months")]
        FourMonths = 4,
        [EnumMember(Value = "Five Months")]
        FiveMonths = 5,
        [EnumMember(Value = "Six Months")]
        SixMonths = 6,
        [EnumMember(Value = "Seven Months")]
        SevenMonths = 7,
        [EnumMember(Value = "Eight Months")]
        EightMonths = 8,
        [EnumMember(Value = "Nine Months")]
        NineMonths =9,
        [EnumMember(Value = "Ten Months")]
        TenMonths = 10,
        [EnumMember(Value = "Eleven Months")]
        Eleven = 11,
        [EnumMember(Value = "One Year")]
        OneYear = 12,
        [EnumMember(Value = "One Year and Months")]
        OneYearAndSixMonths = 13,
        [EnumMember(Value = "Two Months")]
        TwoYears = 14,
        [EnumMember(Value = "LifeTime")]
        LifeTime = 15
    }
}
