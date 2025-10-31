using QLBanDoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanDoAnNhanh.BLL;

namespace QLBanDoAnNhanh
{
    public partial class frmItemDetail : Form
    {
        private int _idProduct;
        private int _idEmployee;
        private PosFastFood _posFastFood;
        public frmItemDetail(int idProduct, int idEmployee)
        {
            InitializeComponent();
            _idProduct = idProduct;
            _idEmployee = idEmployee;
        }

        private void frmItemDetail_Load(object sender, EventArgs e)
        {
            _posFastFood = new PosFastFood();
            var productItems = _posFastFood.Products.Find(_idProduct);
            if (productItems != null)
            {
                using (MemoryStream ms = new MemoryStream(productItems.Images))
                {
                    Image image = Image.FromStream(ms);
                    picItem.Image = image;
                }
                lbName.Text = productItems.NameProduct;
                lbPrice.Text = productItems.PriceProduct.Value.ToString("0.0") + "$";
                if (string.IsNullOrEmpty(productItems.Descriptions) == false)
                {
                    lbDecript.Text = productItems.Descriptions;
                }
                else
                {
                    lbDecript.Text = "N/A";
                }
                if (productItems.IsActive == false)
                {
                    btnSoldOut.Text = "UnSold";
                }
            }
        }

        private void btnSoldOut_Click(object sender, EventArgs e)
        {
            var productItems = _posFastFood.Products.Find(_idProduct);
            if (productItems != null)
            {
                using (MemoryStream ms = new MemoryStream(productItems.Images))
                {
                    Image image = Image.FromStream(ms);
                    picItem.Image = image;
                }
                lbName.Text = productItems.NameProduct;
                lbPrice.Text = productItems.PriceProduct.Value.ToString("0.0") + "$";
                if (string.IsNullOrEmpty(productItems.Descriptions) == false)
                {
                    lbDecript.Text = productItems.Descriptions;
                }
                else
                {
                    lbDecript.Text = "N/A";
                }
                if (productItems.IsActive == false)
                {
                    productItems.IsActive = true;
                }
                else
                {
                    productItems.IsActive = false;
                }
                _posFastFood.SaveChanges();
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form frmedit = new frmEditItem(_idProduct, _posFastFood);
            frmedit.ShowDialog();
            frmItemDetail_Load(sender, e);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền admin (giữ nguyên)
            var employee = _posFastFood.Employees.Find(_idEmployee); // Ta sẽ tái cấu trúc phần này sau
            if (employee.IdRole != 1)
            {
                MessageBox.Show("Chỉ có tài khoản quản lý mới có thể xóa sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // 1. Gọi xuống BLL để xử lý
                var productService = new ProductService();
                string result = productService.DeleteProduct(_idProduct);

                // 2. Xử lý kết quả trả về
                switch (result)
                {
                    case "Deleted":
                        MessageBox.Show("Đã xóa vĩnh viễn sản phẩm.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form chi tiết sau khi xóa
                        break;
                    case "Deactivated":
                        MessageBox.Show("Sản phẩm này đã tồn tại trong hóa đơn nên đã được ẩn đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form chi tiết
                        break;
                    case "NotFound":
                        MessageBox.Show("Không tìm thấy sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default: // "Error"
                        MessageBox.Show("Đã có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
    }
}
