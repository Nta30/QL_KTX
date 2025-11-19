using QL_KTX.BLL;
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

namespace QL_KTX.UI
{
    public partial class FormDangNhap : Form
    {
        TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            picDangNhap.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\Anh_Form_Login.jpg"));
            picUser.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\user.jpg"));
            picPassword.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\password.jpg"));
            this.AcceptButton = btnDangNhap;
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }
            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
            string username = txtTenDangNhap.Text;
            string password = txtMatKhau.Text;
            bool isValid = taiKhoanBLL.CheckLogin(username, password);
            if (isValid)
            {
                FormTrangChu formTrangChu = new FormTrangChu();
                this.Hide();
                formTrangChu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Text = "";
                txtMatKhau.Focus();
            }
        }
    }
}
