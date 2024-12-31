namespace Api.Tests.WebMinRouteGroup.Services;
using Todo = Api.Tests.WebMinRouteGroup.Data.Todo;

public interface ITodoService
{
    Task<List<Todo>> GetAll();

    Task<List<Todo>> GetIncompleteTodos();

    ValueTask<Todo?> Find(int id);

    Task Add(Todo todo);

    Task Update(Todo todo);

    Task Remove(Todo todo);
}
