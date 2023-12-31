// Copyright (c) Angel-of-Dev. All rights reserved.

using Bogus;

namespace AngelOfDev.Testing;

/// <summary> Provides means of generating test data. </summary>
public sealed class TestDataProvider
{
    private const string AlphaNumericCharacters = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
    private readonly Faker _faker;

    /// <summary> Initializes a new instance of <see cref="TestDataProvider"/>. </summary>
    /// <param name="random"> The pseudo-random number generator to use when generating test data. </param>
    public TestDataProvider(Random random)
    {
        _faker = new Faker();
        Rand = random;
    }

    /// <summary> Initializes a new instance of <see cref="TestDataProvider"/>. </summary>
    internal TestDataProvider()
        : this(Random.Shared) { }

    /// <summary> Gets the pseudo-random number generator. </summary>
    public Random Rand { get; }

    /// <summary> Returns a random string containing alpha-numeric characters. </summary>
    /// <param name="minLength"> The minimum length of string to return. </param>
    /// <param name="maxLength"> The maximum length of string to return. </param>
    public string AlphaNumericString(int minLength = 1, int maxLength = 20) => _faker.Random.String2(minLength, maxLength, AlphaNumericCharacters);

    /// <summary> Generates a random integer. </summary>
    /// <param name="min"> The inclusive lower bound of the random number returned. </param>
    /// <param name="max"> The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to
    ///     <paramref name="min"/>. </param>
    public int Int32(int min = int.MinValue, int max = int.MaxValue) => Rand.Next(min, max);
}
