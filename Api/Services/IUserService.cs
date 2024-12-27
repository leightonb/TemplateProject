using TemplateProject.Repositories.Models;
using TemplateProject.Models;

namespace TemplateProject.Services;

public interface IUserService
{
    Task<User?> Register(RegisterUser userModel);
    Task<User?> Authenticate(string username, string password);
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByUsername(string username);
    Task<IEnumerable<User>> GetAll();
}