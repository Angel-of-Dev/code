// Copyright (c) Angel-of-Dev. All rights reserved.

using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Logging;

namespace AngelOfDev.Testing;

/// <summary> Provides common infrastructure for classes with xUnit tests. </summary>
public abstract class TestContext
{
    /// <summary> Initializes a new instance of <see cref="TestContext"/>. </summary>
    /// <param name="outputHelper"> The test output helper normally injected by xUnit framework. </param>
    protected TestContext(ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;
        LoggerFactory = new LoggerFactory(new[] {new XunitLoggerProvider(outputHelper, (_, _) => true)});
        Logger = new XunitLogger(outputHelper, GetType().FullName, (_, _) => true);
        A = new TestDataProvider();
    }

    /// <summary> Gets the <see cref="LoggerFactory"/> instance associated with this test context. </summary>
    /// <remarks> The factory uses <see cref="XunitLoggerProvider"/> that forwards logs to <see cref="OutputHelper"/>. </remarks>
    public LoggerFactory LoggerFactory { get; }

    /// <summary> Gets the <see cref="TestDataProvider"/> associated with this test context. </summary>
    public TestDataProvider A { get; }

    /// <summary> Gets the <see cref="ILogger"/> associated with this test context. </summary>
    /// <remarks> The logger is implemented as <see cref="XunitLogger"/> that forwards logs to <see cref="OutputHelper"/>. </remarks>
    protected ILogger Logger { get; }

    /// <summary> Gets the <see cref="ITestOutputHelper"/> associated with this test context. </summary>
    protected ITestOutputHelper OutputHelper { get; }

    /// <summary> Creates an <see cref="object"/> array with given elements. </summary>
    /// <remarks> The motivation for this method is to simplify array creation code that is used frequently with xUnit. </remarks>
    /// <param name="elements"> The elements of array to create. </param>
    public static object[] Array(params object[] elements) => elements.ToArray();

    /// <summary> Creates an typed array with given elements. </summary>
    /// <remarks> The motivation for this method is to simplify array creation code that is used frequently with xUnit. </remarks>
    /// <param name="elements"> The elements of array to create. </param>
    /// <typeparam name="TElement"> The type of elements in the array. </typeparam>
    public static TElement[] ArrayOf<TElement>(params TElement[] elements) => elements.ToArray();
}
