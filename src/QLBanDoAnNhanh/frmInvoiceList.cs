using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanDoAnNhanh
{
    public partial class frmInvoiceList : Form
    {
        public frmInvoiceList()
        {
            InitializeComponent();
        }

        private void frmInvoiceList_Load(object sender, EventArgs e)
        {
            // 1. Gọi xuống BLL để lấy dữ liệu
            var orderService = new QLBanDoAnNhanh.BLL.OrderService();
            var allOrders = orderService.GetAllOrders();

            // 2. Xử lý và hiển thị dữ liệu lên DataGridView
            // Chúng ta tạo một danh sách tạm (anonymous list) chỉ chứa các cột cần thiết
            var displayData = allOrders.Select(o => new {
                MaHD = o.IdOrder,
                NgayTao = o.CreateDate,
                NhanVien = o.Employee?.NameEmployee, // Dùng "?." để tránh lỗi nếu nhân viên null
                TongTien = o.Total
            }).ToList();

            dgvInvoices.DataSource = displayData;
        }
    }
}
