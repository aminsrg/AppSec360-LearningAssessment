using System.ComponentModel;

namespace AppSec360_LearningAssessment.Application.Common.Extensions;

/// <summary>
/// Extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description attribute value of an enum value.
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }
}
