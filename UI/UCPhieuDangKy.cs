using QL_KTX.BLL;
using QL_KTX.DAL;
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
    public partial class UCPhieuDangKy : UserControl
    {
        PhieuDangKyBLL phieuDangKyBLL = new PhieuDangKyBLL();
        SinhVienBLL sinhVienBLL = new SinhVienBLL();   
        Functions functions = new Functions();
        string trangThai = "";
        public UCPhieuDangKy()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtMaSinhVien.Enabled = enable;
            cbToa.Enabled = enable;
            cbPhong.Enabled = enable;
            dtpThoiGianDangKy.Enabled = enable;
            txtHocKy.Enabled = enable;
            txtNamHoc.Enabled = enable;
            txtThoiHan.Enabled = enable;
            dtpNgayVaoPhong.Enabled = enable;
            txtTienCoc.Enabled = enable;
        }

        private void UCPhieuDangKy_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            dtpLeftNgayDangKy.CustomFormat = " ";
            DataTable dsPhieu = phieuDangKyBLL.TimKiem("", null, "");
            dgvDSPhieu.DataSource = dsPhieu;
            dgvDSPhieu.RowHeadersVisible = false;
            dgvDSPhieu.Columns[0].HeaderText = "Mã Phiếu";
            dgvDSPhieu.Columns[1].HeaderText = "Mã Sinh Viên";
            dgvDSPhieu.Columns[2].HeaderText = "Họ Tên";
            dgvDSPhieu.Columns[3].HeaderText = "Tên Phòng";
            dgvDSPhieu.Columns[4].HeaderText = "Tên Tòa";
            dgvDSPhieu.Columns[5].HeaderText = "Thời Gian Đăng Ký";
            dgvDSPhieu.Columns[6].HeaderText = "Học Kỳ";
            dgvDSPhieu.Columns[7].HeaderText = "Năm Học";

            DataTable dsToa = phieuDangKyBLL.TatCaToa();
            functions.FillCombox(cbToa, dsToa, "TenToa", "MaToa");

            btnSua.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void dtpLeftNgayDangKy_ValueChanged(object sender, EventArgs e)
        {
            dtpLeftNgayDangKy.CustomFormat = "MM/dd/yyyy";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime? ngayDangKy = dtpLeftNgayDangKy.Value;
            if (dtpLeftNgayDangKy.CustomFormat == " ")
            {
                ngayDangKy = null;
            }
            string maSinhVien = txtLeftMaSinhVien.Text;
            string namHoc = txtLeftNamHoc.Text;
            DataTable dsPhieu = phieuDangKyBLL.TimKiem(maSinhVien, ngayDangKy, namHoc);
            dgvDSPhieu.DataSource = dsPhieu;
            dgvDSPhieu.RowHeadersVisible = false;
            dgvDSPhieu.Columns[0].HeaderText = "Mã Phiếu";
            dgvDSPhieu.Columns[1].HeaderText = "Mã Sinh Viên";
            dgvDSPhieu.Columns[2].HeaderText = "Họ Tên";
            dgvDSPhieu.Columns[3].HeaderText = "Tên Phòng";
            dgvDSPhieu.Columns[4].HeaderText = "Tên Tòa";
            dgvDSPhieu.Columns[5].HeaderText = "Thời Gian Đăng Ký";
            dgvDSPhieu.Columns[6].HeaderText = "Học Kỳ";
            dgvDSPhieu.Columns[7].HeaderText = "Năm Học";
        }

        private void dgvDSPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = true;
            EnableEdit(false);

            if (e.RowIndex == -1)
            {
                dtpLeftNgayDangKy.CustomFormat = " ";
                txtLeftMaSinhVien.Text = "";
                txtLeftNamHoc.Text = "";
                btnTimKiem_Click(sender, e);
                return;
            }

            string maPhieuDangKy = dgvDSPhieu.Rows[e.RowIndex].Cells["MaPhieuDangKy"].Value.ToString();
            PhieuDangKyDTO phieuDangKy = phieuDangKyBLL.ChiTietPhieuDangKy(maPhieuDangKy);
            HienThiChiTietPhieu(phieuDangKy);
        }

        private void HienThiChiTietPhieu(PhieuDangKyDTO p)
        {
            txtMaSinhVien.Text = p.MaSinhVien;
            txtTenSinhVien.Text = p.HoTen;
            dtpNgaySinh.Value = p.NgaySinh;
            cbGioiTinh.Text = p.GioiTinh;
            cbKhoa.Text = p.TenKhoa;
            cbLop.Text = p.TenLop;
            cbToa.Text = p.TenToa;
            cbPhong.Text = p.TenPhong;
            dtpThoiGianDangKy.Value = p.ThoiGianDangKy;
            txtHocKy.Text = p.HocKy;
            txtNamHoc.Text = p.NamHoc;
            txtThoiHan.Text = p.ThoiHan.ToString();
            dtpNgayVaoPhong.Value = p.NgayVaoPhong;
            txtTienCoc.Text = p.TienCoc.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            cbPhong.Enabled = false;
            btnLamMoi_Click(sender, e);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
            trangThai = "THEM";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Text = "";
            txtTenSinhVien.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cbGioiTinh.Text = "";
            cbKhoa.Text = "";
            cbLop.Text = "";

            DataTable dsToa = phieuDangKyBLL.TatCaToa();
            functions.FillCombox(cbToa, dsToa, "TenToa", "MaToa");

            cbPhong.Text = "";
            dtpThoiGianDangKy.Value = DateTime.Now;
            txtHocKy.Text = "";
            txtNamHoc.Text = "";
            txtThoiHan.Text = "";
            dtpNgayVaoPhong.Value = DateTime.Now;
            txtTienCoc.Text = "";
        }

        private void cbToa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbToa.SelectedIndex > 0)
            {
                cbPhong.Enabled = true;
                string maToa = cbToa.SelectedValue.ToString();
                DataTable dsPhong = phieuDangKyBLL.TatCaPhong(maToa);
                functions.FillCombox(cbPhong, dsPhong, "TenPhong", "MaPhong");
            }
            else
            {
                cbPhong.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            btnThem.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnLuu.Enabled = true;
            trangThai = "SUA";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSinhVien.Text.Trim()))
            {
                MessageBox.Show("Mã Sinh Viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbPhong.SelectedValue == null || string.IsNullOrEmpty(cbToa.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Tòa.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbPhong.SelectedValue == null || string.IsNullOrEmpty(cbPhong.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Phòng.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtHocKy.Text.Trim()))
            {
                MessageBox.Show("Học Kỳ không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtNamHoc.Text.Trim()))
            {
                MessageBox.Show("Năm Học không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtThoiHan.Text.Trim()))
            {
                MessageBox.Show("Thời Hạn không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtTienCoc.Text.Trim()))
            {
                MessageBox.Show("Tiền Cọc không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int thoiHan;
            if (!int.TryParse(txtThoiHan.Text.Trim(), out thoiHan) || thoiHan <= 0)
            {
                MessageBox.Show("Thời Hạn phải là số nguyên dương.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal tienCoc;
            if (!decimal.TryParse(txtTienCoc.Text.Trim(), out tienCoc) || tienCoc < 0)
            {
                MessageBox.Show("Tiền Cọc phải là số và không được âm.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dsPhieu = dsPhieu = phieuDangKyBLL.TimKiem("", null, "");
            string maPhieuMoi = "PDK00";
            PhieuDangKyDTO p = new PhieuDangKyDTO
            {
                MaPhieuDangKy = trangThai=="SUA"  ?dgvDSPhieu.CurrentRow.Cells["MaPhieuDangKy"].Value.ToString() : (maPhieuMoi + (dsPhieu.Rows.Count+1)),
                MaSinhVien = txtMaSinhVien.Text.Trim(),
                MaToa = cbToa.SelectedValue.ToString(),
                MaPhong = cbPhong.SelectedValue.ToString(),
                ThoiGianDangKy = dtpThoiGianDangKy.Value,
                HocKy = txtHocKy.Text.Trim(),
                NamHoc = txtNamHoc.Text.Trim(),
                ThoiHan = thoiHan,
                NgayVaoPhong = dtpNgayVaoPhong.Value,
                TienCoc = tienCoc
            };
            bool ketQua = false;
            if (trangThai == "THEM")
            {
                if (!phieuDangKyBLL.SinhVienDuocPhepDangKy(p.MaSinhVien))
                {
                    MessageBox.Show("Sinh viên này đang ở trong KTX, không thể đăng ký", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ketQua = phieuDangKyBLL.ThemPhieuDangKy(p);
                MessageBox.Show(ketQua ? "Thêm Phiếu Đăng Ký thành công!" : "Thêm Phiếu Đăng Ký thất bại!", "Thông Báo");
            }
            else if (trangThai == "SUA")
            {
                ketQua = phieuDangKyBLL.SuaPhieuDangKy(p);
                MessageBox.Show(ketQua ? "Cập nhật Phiếu Đăng Ký thành công!" : "Cập nhật Phiếu Đăng Ký thất bại!", "Thông Báo");
            }
            if (ketQua)
            {
                UCPhieuDangKy_Load(sender,e);
            }
        }

        private void txtMaSinhVien_TextChanged(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text;
            if (maSinhVien.Length < 5)
            {
                txtTenSinhVien.Text = "";
                dtpNgaySinh.CustomFormat = " ";
                cbGioiTinh.Text = "";
                cbKhoa.Text = "";
                cbLop.Text = "";
                return;
            }
            SinhVienDTO sv = sinhVienBLL.ChiTietSinhVien(maSinhVien);
            if (sv != null)
            {
                txtTenSinhVien.Text = sv.hoTen;
                dtpNgaySinh.CustomFormat = "MM/dd/yyyy";
                dtpNgaySinh.Value = sv.ngaySinh;
                cbGioiTinh.Text = sv.gioiTinh;
                cbKhoa.Text = sv.tenKhoa;
                cbLop.Text = sv.tenLop;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(false);
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            trangThai = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPhieu = dgvDSPhieu.CurrentRow.Cells["MaPhieuDangKy"].Value.ToString();
            string maSinhVien = dgvDSPhieu.CurrentRow.Cells["MaSinhVien"].Value.ToString();
            string hoTen = dgvDSPhieu.CurrentRow.Cells["HoTen"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phiếu đăng ký của sinh viên {hoTen} có Mã là {maSinhVien} không?",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool ketQua = phieuDangKyBLL.XoaPhieuDangKy(maPhieu);
                if (ketQua)
                {
                    MessageBox.Show("Xóa Phiếu Đăng Ký thành công!", "Thông Báo");
                    UCPhieuDangKy_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa Phiếu Đăng Ký thất bại!", "Thông Báo Lỗi");
                }
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDSPhieu, "Danh sách phiếu đăng ký", "DanhSachPhieuDangKy");
        }
    }
}
