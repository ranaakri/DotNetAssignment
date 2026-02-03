using InventoryManagementSystem.DataAccess.Context;
using InventoryManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.DataAccess.Repository
{
    public interface IProductRepo
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductById(int id);
        public Task InsertProduct(Product product);
        public Task<Product> Update(Product product);
        public Task Delete(int id);

    }

    public class ProductRepo : IProductRepo
    {
        private AppDbContext _appDbContext;

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _appDbContext.Products.ToListAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _appDbContext.Products.FindAsync(id);
        }
        public async Task InsertProduct(Product product)
        {
            await _appDbContext.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Product> Update(Product product)
        {
            Product? product1 = await _appDbContext.Products.FindAsync(product.Id);
            if(product1 == null)
                throw new KeyNotFoundException("Not valid Product");
            product1.Quentity = product.Quentity;
            product1.ProductName = product.ProductName;
            product1.Category = product.Category;
            product1.LastUpdated = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();
            return product1;
        }
        public async Task Delete(int id)
        {
            Product? product1 = await _appDbContext.Products.FindAsync(id);
            if (product1 == null)
                throw new KeyNotFoundException("Not valid Product");
            _appDbContext.Products.Remove(product1);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
