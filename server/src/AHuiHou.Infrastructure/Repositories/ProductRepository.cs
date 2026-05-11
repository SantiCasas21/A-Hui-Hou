using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AHuiHouDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }
}

