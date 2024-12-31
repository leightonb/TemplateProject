using Api.Tests.WebMinRouteGroup.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.WebMinRouteGroup.Services;

public class TodoService(TodoGroupDbContext dbContext, IEmailService emailService) : ITodoService
{
    public async ValueTask<Data.Todo?> Find(int id)
    {
        return await dbContext.Todos.FindAsync(id);
    }

    public async Task<List<Data.Todo>> GetAll()
    {
        return await dbContext.Todos.ToListAsync();
    }

    public async Task Add(Data.Todo todo)
    {
        await dbContext.Todos.AddAsync(todo);

        if (await dbContext.SaveChangesAsync() > 0)
            await emailService.Send("hello@microsoft.com", $"New todo has been added: {todo.Title}");
    }

    public async Task Update(Data.Todo todo)
    {
        dbContext.Todos.Update(todo);
        await dbContext.SaveChangesAsync();
    }

    public async Task Remove(Data.Todo todo)
    {
        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync();
    }

    public Task<List<Data.Todo>> GetIncompleteTodos()
    {
        return dbContext.Todos.Where(t => t.IsDone == false).ToListAsync();
    }
}
