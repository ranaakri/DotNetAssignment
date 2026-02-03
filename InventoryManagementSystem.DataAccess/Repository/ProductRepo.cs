using InventoryManagementSystem.DataAccess.Context;
using InventoryManagementSystem.Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public Task<int> GetTotalProduct();
    }

    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task InsertProduct(Product product)
        {
            if (product.EntryTime == default) product.EntryTime = DateTime.UtcNow;
            product.LastUpdated = DateTime.UtcNow;

            product.Logs ??= new List<LogTable>();

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

        public async Task<int> GetTotalProduct()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(_appDbContext.Database.GetConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM Products";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();
                    count = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return count;
        }
    }
}
