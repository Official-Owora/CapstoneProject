using System.Runtime.Serialization;

namespace CapstoneProject.Domain.Enums
{
    public enum ProgrammingLanguage
    {
        [EnumMember(Value = "C#")]
        CSharp = 1,
        [EnumMember(Value = "Java")]
        Java = 2,
        [EnumMember(Value = "React")]
        React = 3,
        [EnumMember(Value = "Python")]
        Python = 4,
        [EnumMember(Value = "JavaScript")]
        JavaScript = 5,
        [EnumMember(Value = "C++")]
        CPlusPlus = 6,
        [EnumMember(Value = "Django")]
        Django = 7,
        [EnumMember(Value = "PHP")]
        PHP = 8,
        [EnumMember(Value = "NodeJs")]
        NodeJs = 9,
        [EnumMember(Value = "R")]
        R = 10,
        [EnumMember(Value = "Unreal")]
        Unreal = 11,
        [EnumMember(Value = "Kotlin")]
        Kotlin = 12,
        [EnumMember(Value = "Flutter")]
        Flutter = 13,
        [EnumMember(Value = "Swift")]
        Swift = 14,
        [EnumMember(Value = "Others")]
        Others = 15
    }
}
