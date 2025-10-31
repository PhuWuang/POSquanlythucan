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

        // Hàm này chỉ đơn giản là gọi xuống DAL để lấy danh sách sản phẩm
        public List<Product> GetByTypeId(int typeId)
        {
            return _productDAL.GetByTypeId(typeId);
        }
    }
}