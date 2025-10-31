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

        // Hàm này tìm sản phẩm theo tên (không phân biệt hoa thường) và theo loại
        public List<Product> SearchByNameAndTypeId(string name, int typeId)
        {
            // THAY ĐỔI DUY NHẤT NẰM Ở ĐÂY: Dùng .Contains() thay vì ==
            return _context.Products
                           .Where(p => p.IdTypeProduct == typeId && p.NameProduct.ToLower().Contains(name.ToLower()))
                           .ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }
        // HÀM MỚI: Thêm một sản phẩm mới vào CSDL
        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges() > 0;
        }

        // HÀM MỚI: Kiểm tra xem tên sản phẩm đã tồn tại chưa (không phân biệt hoa thường)
        public bool ProductExists(string name)
        {
            return _context.Products.Any(p => p.NameProduct.ToLower() == name.ToLower());
        }
    }
}