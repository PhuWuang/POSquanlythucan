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
        // HÀM MỚI: Xử lý nghiệp vụ thêm sản phẩm
        public bool CreateProduct(Product product)
        {
            // Logic nghiệp vụ: không cho phép thêm sản phẩm có tên đã tồn tại
            if (_productDAL.ProductExists(product.NameProduct))
            {
                // Trả về false nếu tên đã tồn tại
                return false;
            }

            // Nếu tên hợp lệ, gọi xuống DAL để lưu
            return _productDAL.CreateProduct(product);
        }
    }
}