using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Set<Category>().Include(c => c.Expense).ToListAsync();
    }

    public override async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Category>()
            .Where(c => c.Id == id)
            .Include(c => c.Expense)
            .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<Category>> FindAllAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _context.Set<Category>()
            .Where(predicate)
            .Include(c => c.Expense).ToListAsync();
    }

    public override async Task<Category?> FindAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _context.Set<Category>()
            .Where(predicate)
            .Include(c => c.Expense)
            .FirstOrDefaultAsync();
    }
}
