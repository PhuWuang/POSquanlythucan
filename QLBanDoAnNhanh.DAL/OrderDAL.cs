using QLBanDoAnNhanh.DAL.Models;
using QLBanDoAnNhanh.Models;

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
    }
}