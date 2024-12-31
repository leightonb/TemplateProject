using Microsoft.EntityFrameworkCore;

namespace Api.Tests.WebMinRouteGroup.Data;

public class TodoGroupDbContext : DbContext
{
    public DbSet<Api.Tests.WebMinRouteGroup.Data.Todo> Todos => Set<Api.Tests.WebMinRouteGroup.Data.Todo>();

    public TodoGroupDbContext(DbContextOptions<TodoGroupDbContext> options)
        : base(options)
    {
    }
}
