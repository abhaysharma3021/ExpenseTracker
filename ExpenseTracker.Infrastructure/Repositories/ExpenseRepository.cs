using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Infrastructure.Repositories;

public class ExpenseRepository : Repository<Expense>, IExpenseRepository
{
    private readonly ApplicationDbContext _context;
    public ExpenseRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Expense>> GetAllAsync()
    {
        return await _context.Set<Expense>()
            .Include(e => e.Category)
            .Include(e => e.User)
            .ToListAsync();
    }

    public override async Task<Expense?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Expense>()
            .Where(e => e.Id == id)
            .Include(e => e.Category)
            .Include(e => e.User)
            .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<Expense>> FindAllAsync(Expression<Func<Expense, bool>> predicate)
    {
        return await _context.Set<Expense>()
            .Where(predicate)
            .Include(e => e.Category)
            .Include(e => e.User)
            .ToListAsync();
    }

    public override async Task<Expense?> FindAsync(Expression<Func<Expense, bool>> predicate)
    {
        return await _context.Set<Expense>()
            .Where(predicate)
            .Include(e => e.Category)
            .Include(e => e.User)
            .FirstOrDefaultAsync();
    }
}
