using QLBanDoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using QLBanDoAnNhanh.BLL; // Để sử dụng EmployeeService
using QLBanDoAnNhanh.Models; // Để sử dụng model Employee

namespace QLBanDoAnNhanh
{
    public partial class frmlogin : Form
    {
        private PosFastFood _posFastFood;
        public bool Valid { get; set; } = false;
        public frmlogin()
        {
            InitializeComponent();
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {
            tbPass.PasswordChar = '*';
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.\w{2,3})+)$";
            Regex regex = new Regex(pattern);
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                errorCheck.SetError(tbEmail, "Cannot empty!");
            }
            else if (regex.IsMatch(tbEmail.Text) == false)
            {
                errorCheck.SetError(tbEmail, "Not formating email!");
            }
            else
            {
                errorCheck.SetError(tbEmail, "");
            }
        }

        private void tbPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPass.Text))
            {
                errorCheck.SetError(tbPass, "Cannot empty!");
            }
            else
            {
                errorCheck.SetError(tbPass, "");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Các bước kiểm tra ô nhập trống vẫn giữ nguyên
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.\w{2,3})+)$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                errorCheck.SetError(tbEmail, "Cannot empty!");
                return; // Dừng lại nếu có lỗi
            }
            else if (regex.IsMatch(tbEmail.Text) == false)
            {
                errorCheck.SetError(tbEmail, "Not formating email!");
                return;
            }
            else
            {
                errorCheck.SetError(tbEmail, "");
            }
            if (string.IsNullOrEmpty(tbPass.Text))
            {
                errorCheck.SetError(tbPass, "Cannot empty!");
                return;
            }
            else
            {
                errorCheck.SetError(tbPass, "");
            }

            // ---- PHẦN LOGIC MỚI ----
            // 1. Khởi tạo EmployeeService từ tầng BLL
            var employeeService = new EmployeeService();

            // 2. Gọi hàm ValidateLogin để kiểm tra
            //    Hàm này sẽ trả về thông tin Employee nếu thành công, hoặc null nếu thất bại
            Employee loggedInEmployee = employeeService.ValidateLogin(tbEmail.Text, tbPass.Text);

            // 3. Kiểm tra kết quả
            if (loggedInEmployee != null)
            {
                // Đăng nhập thành công!
                // Chúng ta có được Id của nhân viên từ loggedInEmployee.IdEmployee
                Form frmmain = new frmMain(loggedInEmployee.IdEmployee, this);
                this.Hide();
                frmmain.Show();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Login unsuccessful. Please check your username and password!");
            }
        }

        private void tbEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShow.Checked == true)
            {
                cbShow.Text = "Hide";
                tbPass.PasswordChar = '\0';
            }
            else
            {
                cbShow.Text = "Show";
                tbPass.PasswordChar = '*';
            }
        }
    }
}
