// Copyright (c) Angel-of-Dev. All rights reserved.

using System.Diagnostics.CodeAnalysis;
using CAE = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace AngelOfDev;

/// <summary> Provides static methods for validating arguments. All methods will throw <see cref="ArgumentException"/> when argument validation
///     fails. </summary>
public static class Guard
{
    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="condition"/> evaluates to <c> false </c>. </summary>
    /// <param name="arg"> The argument used in <paramref name="condition"/> check. </param>
    /// <param name="condition"> The condition that evaluates <paramref name="arg"/>. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="conditionDescription"> The description of the condition used in exception message when validation fails. </param>
    public static void Condition<T>(T arg,
                                    bool condition,
                                    [CAE(nameof(arg))] string? parameterName = null,
                                    [CAE(nameof(condition))] string? conditionDescription = null)
    {
        if (!condition)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, $"must satisfy `{conditionDescription}`");
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is not even. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is not even. </exception>
    public static void Even(int arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must be even")
    {
        if (arg % 2 != 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is not in range [<paramref name="min"/>;
    ///     <paramref name="max"/>] where both ends are included in the range. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="min"> The minimum value allowed for <paramref name="arg"/>. </param>
    /// <param name="max"> The maximum value allowed for <paramref name="arg"/>. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    public static void InRange(int arg,
                               int min,
                               int max,
                               [CAE(nameof(arg))] string? parameterName = null)
    {
        if (arg < min ||
            arg > max)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, $"must be in range `[{min}, {max}]`");
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> does not contain at least <paramref name="min"/>
    ///     elements or <see cref="ArgumentNullException"/> when <paramref name="arg"/> is <c> null </c>. </summary>
    /// <param name="arg"> The list to validate. </param>
    /// <param name="min"> The minimum number of elements required in <paramref name="arg"/>. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    public static void MinimumLength<T>(IReadOnlyList<T> arg, int min, [CAE(nameof(arg))] string? parameterName = null)
    {
        NotNull(arg);

        if (arg.Count < min)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg.Count, $"must contain at least `{min}` elements.");
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is negative. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is negative. </exception>
    public static void NotNegative(int arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must not be negative")
    {
        if (arg < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is negative. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is negative. </exception>
    public static void NotNegative(long arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must not be negative")
    {
        if (arg < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is negative. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is negative. </exception>
    public static void NullOrNotNegative(int? arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must be null or not negative")
    {
        if (arg < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentNullException"/> when <paramref name="arg"/> is <c> null </c>. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <typeparam name="T"> The type of argument to validate. </typeparam>
    /// <exception cref="ArgumentNullException"> <paramref name="arg"/> is <c> null </c>. </exception>
    public static void NotNull<T>([NotNull] T? arg, [CAE(nameof(arg))] string? parameterName = null)
    {
        if (arg is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is <c> null </c>, empty or consists only of
    ///     white-space characters. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is empty, null or consists only of white-space characters. </exception>
    public static void NotNullOrWhiteSpace(string arg,
                                           [CAE(nameof(arg))] string? parameterName = null,
                                           string? message = "must not be null, empty or consist only of white-space characters")
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is zero. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is zero. </exception>
    public static void NotZero(int arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must not be zero")
    {
        if (arg == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is zero. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is zero. </exception>
    public static void NotZero(byte arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must not be zero")
    {
        if (arg == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is not odd. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is not odd. </exception>
    public static void Odd(int arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must be odd")
    {
        if (arg % 2 == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is not odd. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is not odd. </exception>
    public static void Odd(byte arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must be odd")
    {
        if (arg % 2 == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }

    /// <summary> Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="arg"/> is not positive. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <param name="message"> Optionally, a custom message used in exception when validation fails. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="arg"/> is not positive. </exception>
    public static void Positive(int arg, [CAE(nameof(arg))] string? parameterName = null, string? message = "must be positive")
    {
        if (arg <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, arg, message);
        }
    }
}
