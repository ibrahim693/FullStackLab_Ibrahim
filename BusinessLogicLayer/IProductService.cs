
using Models;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public interface IProductService
    {
        List<ProductModel> GetAllData();

        ProductModel? GetDataById(int id);

        bool InsertData(ProductModel model);

        bool UpdateData(ProductModel model);

        bool DeleteData(int id);
    }
}
