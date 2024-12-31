using Api.Tests.WebMinRouteGroup.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Helpers;

public class MockDb : IDbContextFactory<TodoGroupDbContext>
{
    public TodoGroupDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TodoGroupDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new TodoGroupDbContext(options);
    }
}
