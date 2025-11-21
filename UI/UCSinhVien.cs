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
using Excel = Microsoft.Office.Interop.Excel;

namespace QL_KTX.UI
{
    public partial class UCSinhVien : UserControl
    {
        SinhVienBLL sinhVienBLL = new SinhVienBLL();
        Functions functions = new Functions();

        private string trangThai = "";
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
            functions.FillCombox(cbDienSinhVien, dienSinhVien, "TenDienSinhVien", "MaDienSinhVien");
            DataTable queQuan = sinhVienBLL.TatCaQue();
            functions.FillCombox(cbLeftQueQuan, queQuan, "TenQue", "MaQue");
            functions.FillCombox(cbQueQuan, queQuan, "TenQue", "MaQue");
            DataTable khoa = sinhVienBLL.TatCaKhoa();
            functions.FillCombox(cbLeftKhoa, khoa, "TenKhoa", "MaKhoa");
            functions.FillCombox(cbKhoa, khoa, "TenKhoa", "MaKhoa");
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");

            DataTable dsSinhVien = sinhVienBLL.TimKiem("","","","");
            dgvSinhVien.DataSource = dsSinhVien;
            dgvSinhVien.Columns[0].HeaderText = "Mã Sinh Viên";
            dgvSinhVien.Columns[1].HeaderText = "Họ Tên";
            dgvSinhVien.Columns[2].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns[3].HeaderText = "Giới Tính";
            dgvSinhVien.Columns[4].HeaderText = "Tên Lớp";
            dgvSinhVien.Columns[5].HeaderText = "Tên Khoa";
            dgvSinhVien.RowHeadersVisible = false;

            btnSua.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
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
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;    
            EnableEdit(false);
            if(e.RowIndex == -1)
            {
                btnTimKiem_Click(sender, e);
                return;
            }
            string maSinhVien = dgvSinhVien.Rows[e.RowIndex].Cells["MaSinhVien"].Value.ToString();
            SinhVienDTO sv = sinhVienBLL.ChiTietSinhVien(maSinhVien);
            HienThiChiTietSinhVien(sv);
        }

        private void HienThiChiTietSinhVien(SinhVienDTO sv)
        {
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

            try
            {
                pbAnh.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\SinhVien\" + sv.anhSinhVien));
                pbAnh.Tag = sv.anhSinhVien;
            }
            catch
            {
                pbAnh.Image = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cbLeftDienSinhVien.Enabled = false;
            cbLeftDienSinhVien.SelectedIndex = 0;
            cbLeftQueQuan.Enabled = false;
            cbLeftQueQuan.SelectedIndex = 0;
            cbLeftKhoa.Enabled = false;
            cbLeftKhoa.SelectedIndex = 0;

            btnLamMoi_Click(sender, e);
            EnableEdit(true);
            cbLop.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThoat.Enabled = true;
            btnLamMoi.Enabled = true;
            btnAnh.Visible = true;
            trangThai = "THEM";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Text = "";
            cbGioiTinh.Text = "";
            txtSdt.Text = "";
            cbQueQuan.Text = "";
            cbDienSinhVien.Text = "";
            txtEmail.Text = "";
            cbKhoa.Text = "";
            cbLop.Text = "";
            txtHtNguoiThan.Text = "";
            txtQuanHe.Text = "";
            txtSdtNguoiThan.Text = "";
            pbAnh.Image = null;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnAnh.Visible = false;
            cbLeftDienSinhVien.Enabled = true;
            cbLeftDienSinhVien.SelectedIndex = 0;
            cbLeftQueQuan.Enabled = true;
            cbLeftQueQuan.SelectedIndex = 0;
            cbLeftKhoa.Enabled = true;
            cbLeftKhoa.SelectedIndex = 0;
            trangThai = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            btnThem.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnLuu.Enabled=true;
            btnAnh.Visible = true;

            cbLeftDienSinhVien.Enabled = false;
            cbLeftDienSinhVien.SelectedIndex = 0;
            cbLeftQueQuan.Enabled = false;
            cbLeftQueQuan.SelectedIndex = 0;
            cbLeftKhoa.Enabled = false;
            cbLeftKhoa.SelectedIndex = 0;

            string maSinhVien = txtMaSinhVien.Text;
            SinhVienDTO sv = sinhVienBLL.ChiTietSinhVien(maSinhVien);
            HienThiChiTietSinhVien(sv);
            trangThai = "SUA";
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileGoc = openFileDialog.FileName;
                string tenAnh = Path.GetFileName(fileGoc);

                string folder = Path.Combine(Application.StartupPath, "Images", "SinhVien");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string file = Path.Combine(folder, tenAnh);
                File.Copy(fileGoc, file, true);
                pbAnh.Image = Image.FromFile(file);
                pbAnh.Tag = tenAnh;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSinhVien.Text.Trim()))
            {
                MessageBox.Show("Mã Sinh Viên không được để trống.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSinhVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Họ Tên không được để trống.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSdt.Text.Trim()))
            {
                MessageBox.Show("Số Điện Thoại không được để trống.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdt.Focus();
                return;
            }

            if (cbGioiTinh.SelectedIndex == -1 || string.IsNullOrEmpty(cbGioiTinh.Text))
            {
                MessageBox.Show("Vui lòng chọn Giới Tính.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbGioiTinh.Focus();
                return;
            }
            if (cbQueQuan.SelectedValue == null || string.IsNullOrEmpty(cbQueQuan.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Quê Quán.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbQueQuan.Focus();
                return;
            }
            if (cbDienSinhVien.SelectedValue == null || string.IsNullOrEmpty(cbDienSinhVien.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Diện Sinh Viên.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbDienSinhVien.Focus();
                return;
            }
            if (cbKhoa.SelectedValue == null || string.IsNullOrEmpty(cbKhoa.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Khoa.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbKhoa.Focus();
                return;
            }
            if (cbLop.SelectedValue == null || string.IsNullOrEmpty(cbLop.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Lớp.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLop.Focus();
                return;
            }

            if (pbAnh == null || pbAnh.Tag == null)
            {
                MessageBox.Show("Vui lòng chọn Ảnh.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tenanh = pbAnh.Tag.ToString();

            SinhVienDTO sv = new SinhVienDTO
            {
                maSinhVien = txtMaSinhVien.Text.Trim(),
                hoTen = txtHoTen.Text.Trim(),
                ngaySinh = dtpNgaySinh.Value,
                gioiTinh = cbGioiTinh.Text,
                soDienThoai = txtSdt.Text.Trim(),
                email = txtEmail.Text.Trim(),
                anhSinhVien = pbAnh.Tag.ToString(),
                maQue = cbQueQuan.SelectedValue != null ? cbQueQuan.SelectedValue.ToString() : "",
                maDienSinhVien = cbDienSinhVien.SelectedValue != null ? cbDienSinhVien.SelectedValue.ToString() : "",
                maLop = cbLop.SelectedValue != null ? cbLop.SelectedValue.ToString() : "",
                hoTenNguoiThan = txtHtNguoiThan.Text.Trim(),
                quanHe = txtQuanHe.Text.Trim(),
                soDienThoaiNguoiThan = txtSdtNguoiThan.Text.Trim()
            };

            bool ketQua = false;

            if (trangThai == "THEM")
            {
                ketQua = sinhVienBLL.ThemSinhVien(sv);
                if (ketQua)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (trangThai == "SUA")
            {
                ketQua = sinhVienBLL.SuaSinhVien(sv);
                if (ketQua)
                {
                    MessageBox.Show("Cập nhật sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật sinh viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (ketQua)
            {
                btnTimKiem_Click(sender, e);
                btnThoat_Click(sender, e);
                UCSinhVien_Load(sender, e);
            }
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKhoa.SelectedIndex > 0 && trangThai!="")
            {
                cbLop.Enabled = true;
                DataTable lop = sinhVienBLL.TatCaLop(cbKhoa.SelectedValue.ToString());
                functions.FillCombox(cbLop, lop, "TenLop", "MaLop");
            }
            else
            {
                cbLop.Enabled = false;
                cbLop.DataSource = null;
                cbLop.Items.Clear();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.Trim();

            if (sinhVienBLL.KiemTraRangBuoc(maSinhVien))
            {
                MessageBox.Show(
                    "Không thể xóa sinh viên này vì họ đã có dữ liệu đăng ký phòng hoặc vi phạm.",
                    "Lỗi Xóa Dữ Liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên {txtHoTen.Text} có Mã là {maSinhVien} không?",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                bool ketQua = sinhVienBLL.XoaSinhVien(maSinhVien);

                if (ketQua)
                {
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnTimKiem_Click(sender, e);
                    btnThoat_Click(sender, e);
                    UCSinhVien_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa sinh viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvSinhVien, "DANH SÁCH SINH VIÊN", "DanhSachSinhVien");
        }
    }
}
