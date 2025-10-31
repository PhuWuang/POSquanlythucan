using QLBanDoAnNhanh.DAL.Models;
using QLBanDoAnNhanh.Models;
using System.Collections.Generic;
using System.Linq;

namespace QLBanDoAnNhanh.DAL
{
    public class ProductDAL
    {
        private PosFastFood _context;

        public ProductDAL()
        {
            _context = new PosFastFood();
        }

        // Hàm này lấy tất cả sản phẩm thuộc một loại nhất định
        public List<Product> GetByTypeId(int typeId)
        {
            return _context.Products.Where(p => p.IdTypeProduct == typeId).ToList();
        }

        // HÀM MỚI: Tìm sản phẩm theo tên (không phân biệt hoa thường) và theo loại
        public List<Product> SearchByNameAndTypeId(string name, int typeId)
        {
            return _context.Products
                           .Where(p => p.IdTypeProduct == typeId && p.NameProduct.ToLower() == name.ToLower())
                           .ToList();
        }
    }
}