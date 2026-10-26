
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CRUD
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CRUD(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<ProductModel>> GetAllDataAsync()
        {
            await using var db =
                await _factory.CreateDbContextAsync();

            return await db.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<ProductModel?> GetDataByIdAsync(Guid id)
        {
            await using var db =
                await _factory.CreateDbContextAsync();

            return await db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<bool> InsertDataAsync(ProductModel model)
        {
            await using var db =
                await _factory.CreateDbContextAsync();

            db.Products.Add(model);

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDataAsync(ProductModel model)
        {
            await using var db =
                await _factory.CreateDbContextAsync();

            var existing = await db.Products
                .FindAsync(model.ProductId);

            if (existing == null)
                return false;

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Price = model.Price;
            existing.StockQuantity = model.StockQuantity;

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDataAsync(Guid id)
        {
            await using var db =
                await _factory.CreateDbContextAsync();

            var existing = await db.Products.FindAsync(id);

            if (existing == null)
                return false;

            db.Products.Remove(existing);

            return await db.SaveChangesAsync() > 0;
        }
    }
}
