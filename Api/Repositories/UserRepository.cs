using TemplateProject.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace TemplateProject.Repositories;

public class UserRepository(TemplateProjectDbContext dbContext) : IUserRepository
{
    public async Task<User?> Create(User model)
    {
        await dbContext.Users.AddAsync(model);
        await dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<User?> GetById(int id)
    {
        return await dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await dbContext.Users.ToListAsync();
    }
}