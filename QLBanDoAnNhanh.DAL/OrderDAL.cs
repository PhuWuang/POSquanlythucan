using QLBanDoAnNhanh.DAL.Models;
using QLBanDoAnNhanh.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLBanDoAnNhanh.DAL
{
    public class OrderDAL
    {
        private PosFastFood _context;

        public OrderDAL()
        {
            _context = new PosFastFood();
        }

        // Hàm này nhận một đối tượng Order đã được xử lý ở BLL và chỉ việc lưu nó
        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            // SaveChanges() trả về số dòng bị ảnh hưởng, > 0 là thành công
            return _context.SaveChanges() > 0;
        }
        // HÀM MỚI: Lấy tất cả các hóa đơn, kèm theo thông tin nhân viên đã tạo hóa đơn đó
        public List<Order> GetAllOrders()
        {
            // .Include("Employee") sẽ tự động join với bảng Employee
            return _context.Orders.Include("Employee").OrderByDescending(o => o.CreateDate).ToList();
        }
    }

}