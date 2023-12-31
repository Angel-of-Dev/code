// Copyright (c) Angel-of-Dev. All rights reserved.

using System.Globalization;
using System.Text;

namespace AngelOfDev;

/// <summary> Represents the exception that is thrown when some run-time expectation is not met. </summary>
public sealed class ExpectationException : InvalidOperationException
{
    /// <summary> Initializes a new instance of <see cref="ExpectationException"/>. </summary>
    public ExpectationException() { }

    /// <summary> Initializes a new instance of <see cref="ExpectationException"/>. </summary>
    public ExpectationException(string message)
        : base(message) { }

    /// <summary> Initializes a new instance of <see cref="ExpectationException"/>. </summary>
    public ExpectationException(string message, Exception innerException)
        : base(message, innerException) { }

    /// <summary> Initializes a new instance of <see cref="ExpectationException"/>. </summary>
    /// <param name="expected"> The description of what was expected. </param>
    /// <param name="actual"> The description of what actually happened. </param>
    /// <param name="additionalContext"> Optionally, any additional context or information to include in exception. </param>
    public ExpectationException(string expected, string actual, params string[] additionalContext)
        : base(GetMessage(expected, actual, additionalContext)) { }

    private static string GetMessage(string expected, string actual, params string[] additionalContext)
    {
        if (additionalContext.Length == 0)
        {
            return $"Expected: {expected}" + Environment.NewLine + $"Actual:  {actual}";
        }

        var sb = new StringBuilder();
        sb.AppendLine(CultureInfo.InvariantCulture, $"Expected: {expected}");
        sb.AppendLine(CultureInfo.InvariantCulture, $"Actual:  {actual}");
        foreach (var item in additionalContext)
        {
            sb.AppendLine(CultureInfo.InvariantCulture, $"{item}");
        }
        return sb.ToString();
    }
}
