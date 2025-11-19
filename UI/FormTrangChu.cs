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
    public partial class FormTrangChu : Form
    {
        public FormTrangChu()
        {
            InitializeComponent();
        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            LoadUserControl(new UCTrangChu());
        }

        private void LoadUserControl(UserControl newUserControl)
        {
            for (int i = 0; i < panelMain.Controls.Count; i++)
            {
                Control oldControl = panelMain.Controls[i];
                panelMain.Controls.Remove(oldControl);
                oldControl.Dispose();
            }

            panelMain.Controls.Add(newUserControl);
            newUserControl.Dock = DockStyle.Fill;
        }

        private void danhSáchSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCSinhVien());
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCTrangChu());
        }

        private void viPhạmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCViPham());
        }

        private void danhSáchTòaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCDSToa());
        }

        private void danhSáchPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCDSPhong());
        }

        private void loạiPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCLoaiPhong());
        }

        private void thiếtBịToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCThietBi());
        }

        private void phiếuĐăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCPhieuDangKy());
        }

        private void trảPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCTraPhong());
        }

        private void danhSáchNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCNhanVien());
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCChucVu());
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCTaiKhoan());
        }

        private void chiPhíPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCChiPhiPhong());
        }
    }
}