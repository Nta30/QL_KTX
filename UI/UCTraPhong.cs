using QL_KTX.BLL;
using QL_KTX.DTO;
using QL_KTX.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX.UI
{
    public partial class UCTraPhong : UserControl
    {
        TraPhongBLL traPhongBLL = new TraPhongBLL();
        SinhVienBLL sinhVienBLL = new SinhVienBLL();
        Functions functions = new Functions();
        string trangThai = "";
        public UCTraPhong()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtMaSinhVien.Enabled = enable;
            dtpNgayTraPhong.Enabled = enable;
            cbTrangThaiCoc.Enabled = enable;
        }
        private void LoadTrangThaiCoc()
        {
            string[] trangThaiValues = { "Tất cả", "Đã trả", "Chưa trả" };
            cbLeftTrangThaiCoc.DataSource = trangThaiValues.Clone();
            cbTrangThaiCoc.DataSource = new string[] { "Đã trả", "Chưa trả" };
            cbLeftTrangThaiCoc.SelectedIndex = 0;
        }

        private void UCTraPhong_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            dtpLeftNgayTraPhong.CustomFormat = " ";
            DataTable lichSu = traPhongBLL.LichSuTraPhong("", null, "");
            dgvLichSu.DataSource = lichSu;
            dgvLichSu.RowHeadersVisible = false;
            dgvLichSu.Columns[0].HeaderText = "Mã Trả Phòng";
            dgvLichSu.Columns[1].HeaderText = "Mã Phiếu Đăng Ký";
            dgvLichSu.Columns[2].HeaderText = "Mã Sinh Viên";
            dgvLichSu.Columns[3].HeaderText = "Họ Tên";
            dgvLichSu.Columns[4].HeaderText = "Tên Phòng";
            dgvLichSu.Columns[5].HeaderText = "Tên Tòa";
            dgvLichSu.Columns[6].HeaderText = "Ngày Trả Phòng";
            dgvLichSu.Columns[7].HeaderText = "Trạng Thái Cọc";

            LoadTrangThaiCoc();
            btnThem.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnThoat.Enabled = btnIn.Enabled = btnLamMoi.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtLeftMaSinhVien.Text.Trim();

            DateTime? ngayTraPhong = dtpLeftNgayTraPhong.Value;
            if (dtpLeftNgayTraPhong.CustomFormat == " ")
            {
                ngayTraPhong = null;
            }

            string trangThaiCoc = cbLeftTrangThaiCoc.Text;
            if (cbLeftTrangThaiCoc.Text == "Tất cả")
            {
                trangThaiCoc = "";
            }else if(cbLeftTrangThaiCoc.Text == "Đã trả")
            {
                trangThaiCoc = "1";
            }
            else
            {
                trangThaiCoc = "0";
            }

            DataTable lichSu = traPhongBLL.LichSuTraPhong(maSinhVien, ngayTraPhong, trangThaiCoc);
            dgvLichSu.DataSource = lichSu;
            dgvLichSu.RowHeadersVisible = false;
            dgvLichSu.Columns[0].HeaderText = "Mã Trả Phòng";
            dgvLichSu.Columns[1].HeaderText = "Mã Phiếu ĐK";
            dgvLichSu.Columns[2].HeaderText = "Mã Sinh Viên";
            dgvLichSu.Columns[3].HeaderText = "Họ Tên";
            dgvLichSu.Columns[4].HeaderText = "Phòng";
            dgvLichSu.Columns[5].HeaderText = "Tòa";
            dgvLichSu.Columns[6].HeaderText = "Ngày Trả";
            dgvLichSu.Columns[7].HeaderText = "Trạng Thái Cọc";
        }

        private void dtpLeftNgayTraPhong_ValueChanged(object sender, EventArgs e)
        {
            dtpLeftNgayTraPhong.CustomFormat = "MM/dd/yyyy";
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            trangThai = "";
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnIn.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = true;
            EnableEdit(false);

            if (e.RowIndex == -1)
            {
                dtpLeftNgayTraPhong.CustomFormat = " ";
                txtLeftMaSinhVien.Text = "";
                cbTrangThaiCoc.Text = "";
                btnTimKiem_Click(sender, e);
                return;
            }
            string maPhieuDangKy = dgvLichSu.Rows[e.RowIndex].Cells["MaPhieuDangKy"].Value.ToString();

            TraPhongChiTietDTO chiTiet = traPhongBLL.ChiTietDayDuTraPhong(maPhieuDangKy);
            if (chiTiet != null)
            {
                HienThiChiTiet(chiTiet);
            }
        }

        private void HienThiChiTiet(TraPhongChiTietDTO data)
        {
            txtMaSinhVien.Text = data.MaSinhVien;
            txtTenSinhVien.Text = data.HoTen;
            dtpNgaySinh.Value = data.NgaySinh;
            cbGioiTinh.Text = data.GioiTinh;
            cbKhoa.Text = data.TenKhoa;
            cbLop.Text = data.TenLop;
            txtMaPhieuDangKy.Text = data.MaPhieuDangKy;
            cbTenPhong.Text = data.TenPhong;
            cbTenToa.Text = data.TenToa;
            txtHocKy.Text = data.HocKy;
            txtNamHoc.Text = data.NamHoc;
            txtThoiHan.Text = data.ThoiHan.ToString();
            txtTienCoc.Text = data.TienCoc.ToString("N0");
            dtpNgayTraPhong.Value = data.NgayTraPhong;
            cbTrangThaiCoc.Text = data.TrangThaiCoc;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Text = "";
            txtTenSinhVien.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cbGioiTinh.Text = "";
            cbKhoa.Text = "";
            cbLop.Text = "";
            txtMaPhieuDangKy.Text = "";
            cbTenPhong.Text = "";
            cbTenToa.Text = "";
            txtHocKy.Text = "";
            txtNamHoc.Text = "";
            txtThoiHan.Text = "";
            txtTienCoc.Text = "";
            dtpNgayTraPhong.Value = DateTime.Now;
            cbTrangThaiCoc.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(true);

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
            trangThai = "THEM";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            btnThem.Enabled = false;
            btnLamMoi.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnLuu.Enabled = true;
            trangThai = "SUA";
        }

        private void txtMaSinhVien_TextChanged(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.Trim();
            if (trangThai == "THEM" && maSinhVien.Length >= 5) 
            {
                PhieuDangKyDTO p = traPhongBLL.ChiTietPhieuDangKyDangO(maSinhVien);

                if (p != null)
                {
                    txtTenSinhVien.Text = p.HoTen;
                    dtpNgaySinh.Value = p.NgaySinh;
                    cbGioiTinh.Text = p.GioiTinh;
                    cbKhoa.Text = p.TenKhoa;
                    cbLop.Text = p.TenLop;
                    txtMaPhieuDangKy.Text = p.MaPhieuDangKy;
                    cbTenPhong.Text = p.TenPhong;
                    cbTenToa.Text = p.TenToa;
                    txtHocKy.Text = p.HocKy;
                    txtNamHoc.Text = p.NamHoc;
                    txtThoiHan.Text = p.ThoiHan.ToString();
                    txtTienCoc.Text = p.TienCoc.ToString("N0");
                    dtpNgayTraPhong.Value = DateTime.Now;
                    cbTrangThaiCoc.Text = "";
                    btnLuu.Enabled = true;
                }
                else
                {
                    txtTenSinhVien.Text = "";
                    dtpNgaySinh.Value = DateTime.Now;
                    cbGioiTinh.Text = "";
                    cbKhoa.Text = "";
                    cbLop.Text = "";
                    txtMaPhieuDangKy.Text = "";
                    cbTenPhong.Text = "";
                    cbTenToa.Text = "";
                    txtHocKy.Text = "";
                    txtNamHoc.Text = "";
                    txtThoiHan.Text = "";
                    txtTienCoc.Text = "";
                    dtpNgayTraPhong.Value = DateTime.Now;
                    cbTrangThaiCoc.Text = "";
                    btnLuu.Enabled = false;

                    if (maSinhVien.Length > 0)
                    {
                        MessageBox.Show("Sinh viên này không có Phiếu Đăng Ký đang ở hoặc MSV chưa đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (trangThai == "THEM" && maSinhVien.Length == 0)
            {
                btnLamMoi_Click(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSinhVien.Text.Trim()))
            {
                MessageBox.Show("Mã Sinh Viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMaPhieuDangKy.Text.Trim()))
            {
                MessageBox.Show("Không tìm thấy Mã Phiếu Đăng Ký. Vui lòng nhập đúng Mã Sinh Viên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(cbTrangThaiCoc.Text.Trim()))
            {
                MessageBox.Show("Trạng Thái Cọc không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieuDangKy = txtMaPhieuDangKy.Text.Trim();

            string maTraPhong = "";
            if (trangThai == "THEM")
            {
                DataTable dsLichSu = traPhongBLL.LichSuTraPhong("", null, "");
                int count = dsLichSu != null ? dsLichSu.Rows.Count : 0;
                maTraPhong = "TP" + (count + 1).ToString("D3");
            }
            else
            {
                maTraPhong = dgvLichSu.CurrentRow.Cells["MaTraPhong"].Value.ToString();
            }

            TraPhongDTO tp = new TraPhongDTO
            {
                MaTraPhong = maTraPhong,
                MaPhieuDangKy = maPhieuDangKy,
                NgayTraPhong = dtpNgayTraPhong.Value,
                TrangThaiCoc = cbTrangThaiCoc.Text.Trim()
            };

            bool ketQua = false;
            if (trangThai == "THEM")
            {
                if (traPhongBLL.DaTraPhong(tp.MaPhieuDangKy))
                {
                    MessageBox.Show("Phiếu Đăng Ký này đã được lập Phiếu Trả Phòng rồi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ketQua = traPhongBLL.ThemTraPhong(tp);
                MessageBox.Show(ketQua ? "Thêm Trả Phòng thành công!" : "Thêm Trả Phòng thất bại!", "Thông Báo");
            }
            else if (trangThai == "SUA")
            {
                ketQua = traPhongBLL.SuaTraPhong(tp);
                MessageBox.Show(ketQua ? "Cập nhật Trả Phòng thành công!" : "Cập nhật Trả Phòng thất bại!", "Thông Báo");
            }

            if (ketQua)
            {
                UCTraPhong_Load(sender, e);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(false);
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            trangThai = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPhieuDangKy = dgvLichSu.CurrentRow.Cells["MaPhieuDangKy"].Value.ToString();
            string maSinhVien = dgvLichSu.CurrentRow.Cells["MaSinhVien"].Value.ToString();
            string hoTen = dgvLichSu.CurrentRow.Cells["HoTen"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa lịch sử trả phòng của sinh viên {hoTen} có Mã là {maSinhVien} không? Thao tác này sẽ xóa phiếu Trả Phòng có Mã Phiếu ĐK là {maPhieuDangKy}.",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool ketQua = traPhongBLL.XoaTraPhong(maPhieuDangKy);
                if (ketQua)
                {
                    MessageBox.Show("Xóa Lịch Sử Trả Phòng thành công!", "Thông Báo");
                    btnLamMoi_Click(sender, e);
                    UCTraPhong_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa Lịch Sử Trả Phòng thất bại! Vui lòng kiểm tra lại.", "Thông Báo Lỗi");
                }
            }
        }
    }
}
