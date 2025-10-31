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

        // Hàm này lấy tất cả sản phẩm thuộc một loại nhất định (ví dụ: Food, Drink...)
        public List<Product> GetByTypeId(int typeId)
        {
            return _context.Products.Where(p => p.IdTypeProduct == typeId).ToList();
        }
    }
}