using QLBanDoAnNhanh.DAL;
using QLBanDoAnNhanh.DAL.Models;
using QLBanDoAnNhanh.Models;
using System.Collections.Generic;

namespace QLBanDoAnNhanh.BLL
{
    public class ProductService
    {
        private ProductDAL _productDAL;

        public ProductService()
        {
            _productDAL = new ProductDAL();
        }

        // Hàm này gọi xuống DAL để lấy danh sách sản phẩm
        public List<Product> GetByTypeId(int typeId)
        {
            return _productDAL.GetByTypeId(typeId);
        }

        // HÀM MỚI: Gọi xuống DAL để thực hiện tìm kiếm
        public List<Product> SearchByNameAndTypeId(string name, int typeId)
        {
            return _productDAL.SearchByNameAndTypeId(name, typeId);
        }
    }
}