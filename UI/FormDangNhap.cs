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
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            picDangNhap.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\Anh_Form_Login.jpg"));
            picUser.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\user.jpg"));
            picPassword.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\DangNhap\password.jpg"));
        }
    }
}
