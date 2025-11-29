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
    public partial class UCChucVu : UserControl
    {
        ChucVuBLL chucVuBLL = new ChucVuBLL();
        Functions functions = new Functions();
        private string trangThai = "";
        public UCChucVu()
        {
            InitializeComponent();
        }
        private void EnableEdit(bool enable)
        {
            txtMaChucVu.Enabled = enable;
            txtTenChucVu.Enabled = enable;
            txtSoNhanVien.Enabled = false;
        }

        private void UCChucVu_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            LoadDataGrid("");

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }
        private void LoadDataGrid(string tuKhoa)
        {
            DataTable dt = chucVuBLL.TimKiem(tuKhoa);
            dgvDSChucVu.DataSource = dt;

            dgvDSChucVu.Columns["MaChucVu"].HeaderText = "Mã Chức Vụ";
            dgvDSChucVu.Columns["TenChucVu"].HeaderText = "Tên Chức Vụ";
            dgvDSChucVu.RowHeadersVisible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtLeftTuKhoa.Text.Trim();
            LoadDataGrid(tuKhoa);
        }

        private void dgvDSChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLamMoi.Enabled = false;

            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            EnableEdit(false);

            string maChucVu = dgvDSChucVu.Rows[e.RowIndex].Cells["MaChucVu"].Value.ToString();
            string tenChucVu = dgvDSChucVu.Rows[e.RowIndex].Cells["TenChucVu"].Value.ToString();

            txtMaChucVu.Text = maChucVu;
            txtTenChucVu.Text = tenChucVu;
            DataTable dtNhanVien = chucVuBLL.NhanVienTheoChucVu(maChucVu);
            sgvDSNhanVien.DataSource = dtNhanVien;
            sgvDSNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV";
            sgvDSNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            sgvDSNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
            sgvDSNhanVien.Columns["Luong"].HeaderText = "Lương";
            sgvDSNhanVien.RowHeadersVisible = false;

            txtSoNhanVien.Text = dtNhanVien.Rows.Count.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(true);
            txtSoNhanVien.Text = "0";

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;

            trangThai = "THEM";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            txtMaChucVu.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnThoat.Enabled = true;

            trangThai = "SUA";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maChucVu = txtMaChucVu.Text.Trim();
            if (string.IsNullOrEmpty(maChucVu)) return;

            if (chucVuBLL.KiemTraRangBuoc(maChucVu))
            {
                MessageBox.Show("Không thể xóa chức vụ này vì đang có nhân viên đảm nhận!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn xóa chức vụ {txtTenChucVu.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (chucVuBLL.XoaChucVu(maChucVu))
                {
                    MessageBox.Show("Xóa thành công!");
                    btnThoat_Click(sender, e);
                    LoadDataGrid("");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChucVu.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã chức vụ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaChucVu.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtTenChucVu.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên chức vụ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChucVu.Focus();
                return;
            }

            ChucVuDTO cv = new ChucVuDTO
            {
                MaChucVu = txtMaChucVu.Text.Trim(),
                TenChucVu = txtTenChucVu.Text.Trim()
            };

            bool ketQua = false;
            if (trangThai == "THEM")
            {
                ketQua = chucVuBLL.ThemChucVu(cv);
            }
            else if (trangThai == "SUA")
            {
                ketQua = chucVuBLL.SuaChucVu(cv);
            }

            if (ketQua)
            {
                MessageBox.Show("Lưu dữ liệu thành công!");
                LoadDataGrid("");
                btnThoat_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Lưu thất bại!Mã chức vụ đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenChucVu.Text = "";
            txtSoNhanVien.Text = "";
            sgvDSNhanVien.DataSource = null;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            txtMaChucVu.Text = "";
            EnableEdit(false);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;

            trangThai = "";
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDSChucVu, "Danh sách chức vụ", "DanhSachChucVu");
        }
    }
}
