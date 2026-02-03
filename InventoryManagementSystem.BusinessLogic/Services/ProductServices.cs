using InventoryManagementSystem.DataAccess.Repository;
using InventoryManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.BusinessLogic.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<Product> GetProductByIdAsync(int id);
        public Task InsertProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<int> GetTotalProductAsync();
    }
    public class ProductServices : IProductService
    {
        private readonly IProductRepo _service;
        private readonly ILogRepo _logRepo;

        public ProductServices(IProductRepo service, ILogRepo logRepo)
        {
            _service = service;
            _logRepo = logRepo;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _service.GetProducts();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _service.GetProductById(id);
        }

        public async Task InsertProductAsync(Product product)
        {
            if (product.Quentity < 0)
            {
                throw new ArgumentException("Invalid Quentity In Insert");
            }
            await _service.InsertProduct(product);
            await _logRepo.InsertLog(new LogTable 
            { 
                ProductId = product.Id,
                Description = "Product added",
                Action = "Inesrt",
                ActionTime = DateTime.UtcNow 
            });
        }

        public async Task<int> GetTotalProductAsync()
        {
            return await _service.GetTotalProduct();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            if (product.Quentity < 0)
            {
                throw new ArgumentException("Invalid Quentity in Update");
            }
            var data = await _service.Update(product);
            await _logRepo.InsertLog(new LogTable 
            { 
                ProductId = product.Id, 
                Description = "Product updated", 
                Action = "Update", 
                ActionTime = DateTime.UtcNow 
            });
            return data;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _service.Delete(id);
            await _logRepo.InsertLog(new LogTable 
            { ProductId = id, 
                Description = "Product deleted", 
                Action = "Delete", 
                ActionTime = DateTime.UtcNow 
            });
        }
    }
}
