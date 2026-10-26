
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllDataAsync();

        Task<ProductModel?> GetDataByIdAsync(Guid id);

        Task<bool> InsertDataAsync(ProductModel model);

        Task<bool> UpdateDataAsync(ProductModel model);

        Task<bool> DeleteDataAsync(Guid id);
    }
}
