using System.Runtime.Serialization;

namespace CapstoneProject.Domain.Enums
{
    public enum TechTrack
    {
        [EnumMember(Value = "DotNet")]
        DotNET = 1,
        [EnumMember(Value = "Java")]
        Java = 2,
        [EnumMember(Value = "React")]
        React = 3,
        [EnumMember(Value = "Machine Learning and Artificial Intelligence")]
        MLandAI = 4,
        [EnumMember(Value = "CyberSecurity")]
        CyberSecurity = 5,
        [EnumMember(Value = "DevOps")]
        DevOps = 6,
        [EnumMember(Value = "DataScience and Analytics")]
        DataScience = 7,
        [EnumMember(Value = "DatabaseManagement")]
        DatabaseManagement = 8,
        [EnumMember(Value = "NetworkSecurity")]
        NetworkSecurity = 9,
        [EnumMember(Value = "Product Management")]
        ProductManagement = 10,
        [EnumMember(Value = "Software Project Management")]
        ProjectManagement = 11,
        [EnumMember(Value = "Cloud Computing")]
        CloudComputing = 12,
        [EnumMember(Value = "UI/UX")]
        UIUX = 13
    }
}
