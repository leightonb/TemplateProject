using TemplateProject.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace TemplateProject.Repositories;

public class UserRepository(TemplateProjectDbContext dbContext) : IUserRepository
{
    private readonly TemplateProjectDbContext _dbContext = dbContext;

    public async Task<User?> Create(User model)
    {
        await _dbContext.Users.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }
}