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
            var hasError = false;
            var product = new Product();

            // Ảnh (nếu bắt buộc có ảnh thì validate thêm)
            if (picProduct.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    picProduct.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    product.Images = ms.ToArray();
                }
            }

            // NAME
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                errorCheck.SetError(tbName, "Cannot empty!");
                hasError = true;
            }
            else
            {
                product.NameProduct = tbName.Text.Trim();
                errorCheck.SetError(tbName, "");
            }

            // PRICE (xử lý dấu ,/.)
            decimal price;
            var priceText = tbPrice.Text.Trim().Replace(",", "."); // nếu người dùng gõ dấu phẩy
            if (string.IsNullOrWhiteSpace(priceText))
            {
                errorCheck.SetError(tbPrice, "Cannot empty!");
                hasError = true;
            }
            else if (!decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out price))
            {
                errorCheck.SetError(tbPrice, "Only number!");
                hasError = true;
            }
            else
            {
                product.PriceProduct = price;
                errorCheck.SetError(tbPrice, "");
            }

            // TYPE
            if (cbType.SelectedValue == null || !int.TryParse(cbType.SelectedValue.ToString(), out var typeId))
            {
                MessageBox.Show("Chưa chọn Type hợp lệ");
                return;
            }
            product.IdTypeProduct = typeId;

            // EMP + DESC + ACTIVE
            product.IdEmployee = _idEmployee;
            if (!string.IsNullOrWhiteSpace(tbDecript.Text))
                product.Descriptions = tbDecript.Text.Trim();
            product.IsActive = true;

            // Nếu còn lỗi thì dừng
            if (hasError) return;

            // Lưu DB xong mới thông báo
            try
            {
                _posFastFood.Products.Add(product);
                var affected = _posFastFood.SaveChanges();
                if (affected > 0)
                {
                    this.DialogResult = DialogResult.OK; // báo cho form gọi biết: đã thêm xong
                    this.Close();                         // đóng form Add
                }
                else
                {
                    MessageBox.Show("Insert failed (no rows affected).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed: " + ex.Message);
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
