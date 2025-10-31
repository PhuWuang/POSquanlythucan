using QLBanDoAnNhanh.Models;
using System.Linq;

namespace QLBanDoAnNhanh.DAL
{
    public class EmployeeDAL
    {
        // Khởi tạo đối tượng DbContext để làm việc với CSDL
        private PosFastFood _context;

        public EmployeeDAL()
        {
            _context = new PosFastFood();
        }

        // Hàm này chỉ có một nhiệm vụ: tìm kiếm Employee dựa trên email
        public Employee GetByEmail(string email)
        {
            // Sử dụng LINQ để truy vấn CSDL
            // FirstOrDefault sẽ trả về Employee đầu tiên tìm thấy, hoặc null nếu không có
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }
    }
}