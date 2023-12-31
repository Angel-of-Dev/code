// Copyright (c) Angel-of-Dev. All rights reserved.

using System.Diagnostics.CodeAnalysis;
using CAE = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace AngelOfDev;

/// <summary> Provides static methods for validating runtime conditions. All methods will throw <see cref="InvalidOperationException"/> when
///     condition checks fail. </summary>
public static class Expect
{
    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="condition"/> evaluates to <c> false </c>. </summary>
    /// <param name="condition"> The condition to check. </param>
    /// <param name="conditionDescription"> The description of the condition used in exception message when validation fails. </param>
    public static void Condition(bool condition, [CAE(nameof(condition))] string? conditionDescription = null)
    {
        if (!condition)
        {
            throw new ExpectationException("condition evaluates to `true`", "condition evaluates to `false`", $"condition: `{conditionDescription}`");
        }
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="dictionary"/> does not contain specified <paramref name="key"/> or
    ///     if value associated with the key is <c> null </c>. </summary>
    /// <param name="dictionary"> The dictionary to check. </param>
    /// <param name="key"> The key to check. </param>
    /// <param name="value"> The value associated with <paramref name="key"/>. </param>
    public static void Contains<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
    {
        Guard.NotNull(dictionary);

        if (!dictionary.TryGetValue(key, out var valueCandidate) ||
            valueCandidate is null)
        {
            throw new ExpectationException("dictionary contains key and value associated with the key is not null",
                                           "dictionary does not contain key or value associated with the key is null",
                                           $"{key}");
        }

        value = valueCandidate;
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="a"/> and <paramref name="b"/> are not equal. </summary>
    /// <param name="a"> The first item to check. </param>
    /// <param name="b"> The second item to check. </param>
    /// <param name="equalityComparer"> Optionally, the <see cref="IEqualityComparer{T}"/> to use for check. When not specified,
    ///     <see cref="EqualityComparer{T}.Default"/> will be used instead. </param>
    /// <typeparam name="T"> </typeparam>
    public static void Equal<T>(T a, T b, IEqualityComparer<T>? equalityComparer = null)
    {
        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        if (!actualComparer.Equals(a, b))
        {
            throw new ExpectationException("items are equal", "items are not equal", $"a: {a}, b: {b}");
        }
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="arg"/> is not in range [<paramref name="min"/>;
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
            throw new ExpectationException($"argument in range `[{min}, {max}]`", $"argument outside of range `[{min}, {max}]`", $"{parameterName}={arg}");
        }
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="a"/> and <paramref name="b"/> are equal. </summary>
    /// <param name="a"> The first item to check. </param>
    /// <param name="b"> The second item to check. </param>
    /// <param name="equalityComparer"> Optionally, the <see cref="IEqualityComparer{T}"/> to use for check. When not specified,
    ///     <see cref="EqualityComparer{T}.Default"/> will be used instead. </param>
    /// <typeparam name="T"> </typeparam>
    public static void NotEqual<T>(T a, T b, IEqualityComparer<T>? equalityComparer)
    {
        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        if (actualComparer.Equals(a, b))
        {
            throw new ExpectationException("items are not equal", "items are equal", $"a: {a}, b: {b}");
        }
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="arg"/> is <c> null </c>. </summary>
    /// <param name="arg"> The argument to validate. </param>
    /// <param name="parameterName"> The name of the argument used in exception message when validation fails. </param>
    /// <typeparam name="T"> The type of argument to validate. </typeparam>
    /// <exception cref="ExpectationException"> <paramref name="arg"/> is <c> null </c>. </exception>
    public static void NotNull<T>([NotNull] T? arg, [CAE(nameof(arg))] string? parameterName = null)
    {
        if (arg is null)
        {
            throw new ExpectationException("value is not null", "value is null", $"{parameterName}={arg}");
        }
    }

    /// <summary> Throws <see cref="ExpectationException"/> when <paramref name="arg"/> is not of specified type. </summary>
    /// <param name="arg"> The object to check. </param>
    /// <typeparam name="T"> The expected type of the object. </typeparam>
    /// <returns> <paramref name="arg"/> as <typeparamref name="T"/>. </returns>
    /// <exception cref="ArgumentNullException"> <paramref name="arg"/> is <c> null </c>. </exception>
    /// <exception cref="ExpectationException"> <paramref name="arg"/> is not of type <typeparamref name="T"/>. </exception>
    public static T OfType<T>(object arg)
    {
        Guard.NotNull(arg);

        if (arg is T t)
        {
            return t;
        }

        throw new ExpectationException($"instance of of type `{typeof(T).FullName}`", "instance is not of desired type", $"source type: `{arg.GetType().FullName}`");
    }

    /// <summary> Returns <see cref="ExpectationException"/> that should be thrown by the caller. </summary>
    public static ExpectationException Unreachable(string message) => new("code that cannot be reached", "code was reached", message);
}
