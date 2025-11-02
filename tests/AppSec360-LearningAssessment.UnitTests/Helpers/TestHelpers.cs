using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Moq;

namespace AppSec360_LearningAssessment.UnitTests.Helpers;

/// <summary>
/// Helper methods for unit testing with MongoDB
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Creates a mocked IMongoCollection from a list of data for testing purposes
    /// </summary>
    public static Mock<IMongoCollection<T>> CreateMockMongoCollection<T>(List<T> data) where T : class
    {
        var mockCollection = new Mock<IMongoCollection<T>>();

        // Setup for AsQueryable() operations
        var queryable = data.AsQueryable();
        mockCollection.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockCollection.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockCollection.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockCollection.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        return mockCollection;
    }

    /// <summary>
    /// Creates a mocked IAsyncCursor for async enumeration
    /// </summary>
    public static Mock<IAsyncCursor<T>> CreateMockAsyncCursor<T>(List<T> data)
    {
        var mockCursor = new Mock<IAsyncCursor<T>>();
        var moveNextIndex = 0;

        mockCursor.Setup(c => c.Current).Returns(data);
        mockCursor.Setup(c => c.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
            .Returns(() => moveNextIndex++ < 1);
        mockCursor.Setup(c => c.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(() => moveNextIndex++ < 1);

        return mockCursor;
    }
}
