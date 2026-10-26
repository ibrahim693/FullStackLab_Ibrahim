
using Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ProductService : IProductService
    {
        private readonly CRUD _crud;

        public ProductService(CRUD crud)
        {
            _crud = crud;
        }

        public async Task<List<ProductModel>> GetAllDataAsync()
        {
            return await _crud.GetAllDataAsync();
        }

        public async Task<ProductModel?> GetDataByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await _crud.GetDataByIdAsync(id);
        }

        public async Task<bool> InsertDataAsync(ProductModel model)
        {
            ValidateProduct(model);

            return await _crud.InsertDataAsync(model);
        }

        public async Task<bool> UpdateDataAsync(ProductModel model)
        {
            if (model == null)
                throw new ArgumentException(
                    "Product is required.");

            if (model.ProductId == Guid.Empty)
                throw new ArgumentException(
                    "Invalid product ID.");

            ValidateProduct(model);

            return await _crud.UpdateDataAsync(model);
        }

        public async Task<bool> DeleteDataAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(
                    "Invalid product ID.");

            return await _crud.DeleteDataAsync(id);
        }

        private static void ValidateProduct(ProductModel model)
        {
            if (model == null)
                throw new ArgumentException(
                    "Product is required.");

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentException(
                    "Product name is required.");

            model.Name = model.Name.Trim();

            if (model.Name.Length > 100)
                throw new ArgumentException(
                    "Product name cannot exceed 100 characters.");

            if (model.Description != null)
            {
                model.Description = model.Description.Trim();

                if (model.Description.Length > 500)
                    throw new ArgumentException(
                        "Description cannot exceed 500 characters.");
            }

            if (model.Price < 0 || model.Price > 99999999.99m)
                throw new ArgumentException(
                    "Price must be between 0 and 99999999.99.");

            if (model.StockQuantity < 0)
                throw new ArgumentException(
                    "Stock quantity cannot be negative.");
        }
    }
}
