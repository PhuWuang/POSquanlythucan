using QLBanDoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanDoAnNhanh.BLL;

namespace QLBanDoAnNhanh
{
    public partial class frmAddItem : Form
    {
        private PosFastFood _posFastFood;
        private int _idEmployee;
        public frmAddItem(int idEmployee)
        {
            InitializeComponent();
            _idEmployee = idEmployee;
        }

        private void frmAddItem_Load(object sender, EventArgs e)
        {
            _posFastFood = new PosFastFood();
            var typeProduct = _posFastFood.TypeProducts.ToList();
            cbType.DataSource = typeProduct;
            cbType.DisplayMember = "NameType";
            cbType.ValueMember = "IdTypeProduct";
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    picProduct.Image = System.Drawing.Image.FromFile(imagePath);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu từ Form và kiểm tra (phần này tương tự code cũ)
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                errorCheck.SetError(tbName, "Tên không được để trống!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPrice.Text) || !decimal.TryParse(tbPrice.Text, out decimal price))
            {
                errorCheck.SetError(tbPrice, "Giá không hợp lệ!");
                return;
            }
            if (picProduct.Image == null)
            {
                MessageBox.Show("Vui lòng chọn hình ảnh cho sản phẩm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Tạo đối tượng Product từ dữ liệu đã thu thập
            var product = new Product
            {
                NameProduct = tbName.Text.Trim(),
                PriceProduct = price,
                Descriptions = tbDecript.Text.Trim(),
                IdTypeProduct = (int)cbType.SelectedValue,
                IdEmployee = _idEmployee,
                IsActive = true
            };

            // Chuyển đổi hình ảnh thành byte array
            using (var ms = new System.IO.MemoryStream())
            {
                picProduct.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                product.Images = ms.ToArray();
            }

            // 3. Gọi xuống BLL để xử lý
            var productService = new ProductService();
            bool success = productService.CreateProduct(product);

            // 4. Xử lý kết quả trả về
            if (success)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo cho form chính biết để tải lại danh sách
                this.Close();
            }
            else
            {
                // Lỗi có thể do tên trùng lặp hoặc do lỗi lưu CSDL
                MessageBox.Show("Thêm sản phẩm thất bại. Tên sản phẩm có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                errorCheck.SetError(tbName, "Cannot empty!");
            }
            else
            {
                errorCheck.SetError(tbName, "");
            }
        }

        private void tbPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrice.Text))
            {
                errorCheck.SetError(tbPrice, "Cannot empty!");
            }
            else if (decimal.TryParse(tbPrice.Text, out _) == false)
            {
                errorCheck.SetError(tbPrice, "Only number!");
            }
            else
            {
                errorCheck.SetError(tbPrice, "");
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                btnAdd.PerformClick();
            }
        }

        private void tbPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                btnAdd.PerformClick();
            }
        }
    }
}
