using QL_KTX.BLL;
using QL_KTX.DTO;
using QL_KTX.Utils;
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
    public partial class UCSinhVien : UserControl
    {
        SinhVienBLL sinhVienBLL = new SinhVienBLL();
        Functions functions = new Functions();
        public UCSinhVien()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtMaSinhVien.Enabled = enable;
            txtHoTen.Enabled = enable;
            dtpNgaySinh.Enabled = enable;
            cbGioiTinh.Enabled = enable;
            txtSdt.Enabled = enable;
            cbQueQuan.Enabled = enable;
            cbDienSinhVien.Enabled = enable;
            txtEmail.Enabled = enable;
            cbKhoa.Enabled = enable;
            cbLop.Enabled = enable;
            txtHtNguoiThan.Enabled = enable;
            txtQuanHe.Enabled = enable;
            txtSdtNguoiThan.Enabled = enable;
        }

        private void UCSinhVien_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            cbLeftLop.Enabled = false;
            DataTable dienSinhVien = sinhVienBLL.TatCaDienSinhVien();
            functions.FillCombox(cbLeftDienSinhVien, dienSinhVien, "TenDienSinhVien", "MaDienSinhVien");
            DataTable queQuan = sinhVienBLL.TatCaQue();
            functions.FillCombox(cbLeftQueQuan, queQuan, "TenQue", "MaQue");
            DataTable khoa = sinhVienBLL.TatCaKhoa();
            functions.FillCombox(cbLeftKhoa, khoa, "TenKhoa", "MaKhoa");

            DataTable dsSinhVien = sinhVienBLL.TimKiem("","","","");
            dgvSinhVien.DataSource = dsSinhVien;
            dgvSinhVien.Columns[0].HeaderText = "Mã Sinh Viên";
            dgvSinhVien.Columns[1].HeaderText = "Họ Tên";
            dgvSinhVien.Columns[2].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns[3].HeaderText = "Giới Tính";
            dgvSinhVien.Columns[4].HeaderText = "Tên Lớp";
            dgvSinhVien.Columns[5].HeaderText = "Tên Khoa";
            dgvSinhVien.RowHeadersVisible = false;
        }

        private void cbLeftKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLeftKhoa.SelectedIndex > 0)
            {
                cbLeftLop.Enabled = true;
                DataTable lop = sinhVienBLL.TatCaLop(cbLeftKhoa.SelectedValue.ToString());
                functions.FillCombox(cbLeftLop, lop, "TenLop", "MaLop");
            }
            else
            {
                cbLeftLop.Enabled = false;
                cbLeftLop.DataSource = null;
                cbLeftLop.Items.Clear();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maDienSinhVien = cbLeftDienSinhVien.SelectedValue.ToString();
            string maKhoa = cbLeftKhoa.SelectedValue.ToString();
            string maQue = cbLeftQueQuan.SelectedValue.ToString();
            string maLop = "";
            if (!string.IsNullOrEmpty(maKhoa))
            {
                maLop = cbLeftLop.SelectedValue.ToString();
            }
            DataTable dsSinhVien = sinhVienBLL.TimKiem(maDienSinhVien, maQue, maKhoa, maLop);
            dgvSinhVien.DataSource = dsSinhVien;
            dgvSinhVien.Columns[0].HeaderText = "Mã Sinh Viên";
            dgvSinhVien.Columns[1].HeaderText = "Họ Tên";
            dgvSinhVien.Columns[2].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns[3].HeaderText = "Giới Tính";
            dgvSinhVien.Columns[4].HeaderText = "Tên Lớp";
            dgvSinhVien.Columns[5].HeaderText = "Tên Khoa";
            dgvSinhVien.RowHeadersVisible = false;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maSinhVien = dgvSinhVien.Rows[e.RowIndex].Cells["MaSinhVien"].Value.ToString();
            SinhVienDTO sv = sinhVienBLL.ChiTietSinhVien(maSinhVien);

            txtMaSinhVien.Text = sv.maSinhVien;
            txtHoTen.Text = sv.hoTen;
            dtpNgaySinh.Text = sv.ngaySinh.ToString();
            cbGioiTinh.Text = sv.gioiTinh;
            txtSdt.Text = sv.soDienThoai;
            cbQueQuan.Text = sv.tenQue;
            cbDienSinhVien.Text = sv.tenDienSinhVien;
            txtEmail.Text = sv.email;
            cbKhoa.Text = sv.tenKhoa;
            cbLop.Text = sv.tenLop;
            txtHtNguoiThan.Text = sv.hoTenNguoiThan;
            txtQuanHe.Text = sv.quanHe;
            txtSdtNguoiThan.Text = sv.soDienThoaiNguoiThan;

            pbAnh.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\SinhVien\" + sv.anhSinhVien));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }
    }
}
