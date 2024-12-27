using TemplateProject.Repositories.Models;

namespace TemplateProject.Repositories;

public interface IUserRepository
{
    Task<User?> Create(User model);
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByUsername(string username);
    Task<IEnumerable<User>> GetAll();
}