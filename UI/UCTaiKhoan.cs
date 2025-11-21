using QL_KTX.BLL;
using QL_KTX.DTO;
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
    public partial class UCTaiKhoan : UserControl
    {
        TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private string trangThai = "";
        public UCTaiKhoan()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtMaNhanVien.Enabled = enable;
            txtTenTaiKhoan.Enabled = enable;
            txtMatKhau.Enabled = enable;
            txtNhaplaiMatKhau.Enabled = enable;
        }

        private void UCTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadDataGrid("");
            EnableEdit(false);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void LoadDataGrid(string maNhanVien)
        {
            dgvDSTaiKhoan.DataSource = taiKhoanBLL.TimKiem(maNhanVien);
            dgvDSTaiKhoan.Columns["MaNhanVien"].HeaderText = "Mã NV";
            dgvDSTaiKhoan.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDSTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
            dgvDSTaiKhoan.Columns["MatKhau"].HeaderText = "Mật Khẩu";

            dgvDSTaiKhoan.RowHeadersVisible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNV = txtLeftMaNhanVien.Text.Trim();
            LoadDataGrid(maNV);
        }

        private void dgvDSTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;

            EnableEdit(false);

            string maNV = dgvDSTaiKhoan.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
            txtMaNhanVien.Text = maNV;
            txtTenTaiKhoan.Text = dgvDSTaiKhoan.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
            txtMatKhau.Text = dgvDSTaiKhoan.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
            txtNhaplaiMatKhau.Text = txtMatKhau.Text;

            NhanVienDTO nv = nhanVienBLL.ChiTietNhanVien(maNV);
            if (nv != null)
            {
                txtHoTen.Text = nv.HoTen;
                txtLuong.Text = nv.Luong.ToString("N0");
                txtCanCuocCongDan.Text = nv.CCCD;
                txtSoDienThoai.Text = nv.SoDienThoai;
                cbChucVu.Text = nv.TenChucVu;
                cbQueQuan.Text = nv.TenQue;
                try
                {
                    string imagePath = Path.Combine(Application.StartupPath, "Images", "NhanVien", nv.AnhNhanVien);
                    if (File.Exists(imagePath)) pbAnh.Image = Image.FromFile(imagePath);
                    else pbAnh.Image = null;
                }
                catch { pbAnh.Image = null; }
            }
        }

        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (trangThai == "THEM" && txtMaNhanVien.Text.Length >= 3)
            {
                string maNV = txtMaNhanVien.Text.Trim();
                NhanVienDTO nv = nhanVienBLL.ChiTietNhanVien(maNV);

                if (nv != null)
                {
                    txtHoTen.Text = nv.HoTen;
                    txtLuong.Text = nv.Luong.ToString("N0");
                    txtCanCuocCongDan.Text = nv.CCCD;
                    txtSoDienThoai.Text = nv.SoDienThoai;
                    cbChucVu.Text = nv.TenChucVu;
                    cbQueQuan.Text = nv.TenQue;

                    try
                    {
                        string imagePath = Path.Combine(Application.StartupPath, "Images", "NhanVien", nv.AnhNhanVien);
                        if (File.Exists(imagePath)) pbAnh.Image = Image.FromFile(imagePath);
                        else pbAnh.Image = null;
                    }
                    catch { pbAnh.Image = null; }
                }
                else
                {
                    txtHoTen.Text = "";
                    txtLuong.Text = "";
                    txtCanCuocCongDan.Text = "";
                    txtSoDienThoai.Text = "";
                    cbChucVu.Text = "";
                    cbQueQuan.Text = "";
                    pbAnh.Image = null;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = "THEM";
            btnLamMoi_Click(sender, e);
            EnableEdit(true);

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;

            txtMaNhanVien.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = "";
            txtTenTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            txtNhaplaiMatKhau.Text = "";

            txtHoTen.Text = "";
            txtLuong.Text = "";
            txtCanCuocCongDan.Text = "";
            txtSoDienThoai.Text = "";
            cbChucVu.Text = "";
            cbQueQuan.Text = "";
            pbAnh.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = "SUA";
            EnableEdit(true);
            txtMaNhanVien.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản của nhân viên {txtHoTen.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (taiKhoanBLL.XoaTaiKhoan(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDataGrid("");
                    btnThoat_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtTenTaiKhoan.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên Đăng Nhập!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTaiKhoan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mật Khẩu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
            if (txtMatKhau.Text != txtNhaplaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhaplaiMatKhau.Focus();
                return;  
            }
            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                maNhanVien = txtMaNhanVien.Text.Trim(),
                tenDangNhap = txtTenTaiKhoan.Text.Trim(),
                matKhau = txtMatKhau.Text.Trim()
            };
            bool ketQua = false;
            if (trangThai == "THEM")
            {
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    MessageBox.Show("Mã nhân viên không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNhanVien.Focus();
                    return;
                }

                ketQua = taiKhoanBLL.ThemTaiKhoan(tk);
                if (!ketQua) MessageBox.Show("Tài khoản cho nhân viên này đã tồn tại hoặc Tên đăng nhập bị trùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (trangThai == "SUA")
            {
                ketQua = taiKhoanBLL.SuaTaiKhoan(tk);
            }

            if (ketQua)
            {
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo");
                LoadDataGrid("");
                btnThoat_Click(sender, e);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(false);
            trangThai = "";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }
    }
}
