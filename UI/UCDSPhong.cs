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
    public partial class UCDSPhong : UserControl
    {
        PhongBLL phongBLL = new PhongBLL();
        Functions functions = new Functions();
        private string trangThai = "";
        public UCDSPhong()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtTenPhong.Enabled = enable;
            cbToa.Enabled = enable;
            cbLoaiPhong.Enabled = enable;
        }

        private void UCDSPhong_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDataGrid("", "", "");
            EnableEdit(false);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void LoadComboBox()
        {
            DataTable dtToa = phongBLL.TatCaToa();
            functions.FillCombox(cbToa, dtToa, "TenToa", "MaToa");

            DataTable dtLoai = phongBLL.TatCaLoaiPhong();
            functions.FillCombox(cbLoaiPhong, dtLoai, "TenLoaiPhong", "MaLoaiPhong");
            functions.FillCombox(cbLeftLoaiPhong, dtLoai, "TenLoaiPhong", "MaLoaiPhong");
        }

        private void LoadDataGrid(string tenToa, string tenPhong, string maLoaiPhong)
        {
            dgvDsPhong.DataSource = phongBLL.TimKiem(tenToa, tenPhong, maLoaiPhong);
            dgvDsPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dgvDsPhong.Columns["TenPhong"].HeaderText = "Tên Phòng";
            dgvDsPhong.Columns["TenToa"].HeaderText = "Tòa";
            dgvDsPhong.Columns["TenLoaiPhong"].HeaderText = "Loại";
            dgvDsPhong.Columns["GiaPhong"].HeaderText = "Giá";
            dgvDsPhong.Columns["SoNguoiDangO"].HeaderText = "Đang Ở";
            dgvDsPhong.Columns["SoNguoiToiDa"].HeaderText = "Tối Đa";

            if (dgvDsPhong.Columns.Contains("MaToa")) dgvDsPhong.Columns["MaToa"].Visible = false;
            if (dgvDsPhong.Columns.Contains("MaLoaiPhong")) dgvDsPhong.Columns["MaLoaiPhong"].Visible = false;
            if (dgvDsPhong.Columns.Contains("AnhPhong")) dgvDsPhong.Columns["AnhPhong"].Visible = false;

            dgvDsPhong.RowHeadersVisible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string toa = txtLeftTenToa.Text.Trim();
            string phong = txtLeftTenPhong.Text.Trim();
            string loai = cbLeftLoaiPhong.SelectedValue?.ToString() ?? "";
            LoadDataGrid(toa, phong, loai);
        }

        private void dgvDsPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            btnLamMoi.Enabled = false;
            EnableEdit(false);

            DataGridViewRow row = dgvDsPhong.Rows[e.RowIndex];
            string maPhong = row.Cells["MaPhong"].Value.ToString();

            txtMaPhong.Text = maPhong;
            txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
            cbToa.SelectedValue = row.Cells["MaToa"].Value.ToString();
            cbLoaiPhong.SelectedValue = row.Cells["MaLoaiPhong"].Value.ToString();

            string tenAnh = row.Cells["AnhPhong"].Value?.ToString();
            try
            {
                if (!string.IsNullOrEmpty(tenAnh))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", "Phong", tenAnh);
                    if (File.Exists(path))
                    {
                        pbAnh.Image = Image.FromFile(path);
                        pbAnh.Tag = tenAnh;
                    }
                    else pbAnh.Image = null;
                }
                else pbAnh.Image = null;
            }
            catch { pbAnh.Image = null; }

            LoadGridSinhVien(maPhong);
        }

        private void LoadGridSinhVien(string maPhong)
        {
            dgvDsSinhVien.DataSource = phongBLL.LayDSSinhVien(maPhong);
            if (dgvDsSinhVien.Columns.Count > 0)
            {
                dgvDsSinhVien.Columns["MaSinhVien"].HeaderText = "Mã SV";
                dgvDsSinhVien.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvDsSinhVien.Columns["TenLop"].HeaderText = "Lớp";
                dgvDsSinhVien.Columns["SoDienThoai"].HeaderText = "SĐT";
                dgvDsSinhVien.RowHeadersVisible = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = "THEM";
            btnLamMoi_Click(sender, e);
            txtMaPhong.Text = functions.SinhMaTuDong("P");
            EnableEdit(true);

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
            btnAnh.Visible = true;

            txtMaPhong.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            cbToa.SelectedIndex = -1;
            cbLoaiPhong.SelectedIndex = -1;
            pbAnh.Image = null;
            pbAnh.Tag = null;
            dgvDsSinhVien.DataSource = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = "SUA";
            EnableEdit(true);
            txtMaPhong.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = true;
            btnAnh.Visible = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (phongBLL.KiemTraRangBuoc(txtMaPhong.Text))
            {
                MessageBox.Show("Không thể xóa phòng này vì đang có sinh viên ở!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn chắc chắn xóa phòng {txtTenPhong.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (phongBLL.XoaPhong(txtMaPhong.Text))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThoat_Click(sender, e);
                    UCDSPhong_Load(sender, e);
                }
                else MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhong.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập mã phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtTenPhong.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            if (cbToa.SelectedIndex == -1 || cbToa.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tòa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbToa.Focus();
                cbToa.DroppedDown = true;
                return;
            }
            if (cbLoaiPhong.SelectedIndex == -1 || cbLoaiPhong.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiPhong.Focus();
                cbLoaiPhong.DroppedDown = true;
                return;
            }

            if (pbAnh.Tag == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh minh họa cho phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAnh.Focus();
                return;
            }
            DataTable checkTen = phongBLL.TimKiem(cbToa.Text.Trim(), txtTenPhong.Text.Trim(), "");
            if(checkTen.Rows.Count > 0 && trangThai == "THEM")
            {
                MessageBox.Show("Phòng này đã tồn tại trong tòa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            PhongDTO p = new PhongDTO
            {
                MaPhong = txtMaPhong.Text.Trim(),
                TenPhong = txtTenPhong.Text.Trim(),
                MaToa = cbToa.SelectedValue.ToString(),
                MaLoaiPhong = cbLoaiPhong.SelectedValue.ToString(),
                AnhPhong = pbAnh.Tag?.ToString() ?? ""
            };
            bool kq = false;
            if (trangThai == "THEM")
                kq = phongBLL.ThemPhong(p);
            else if (trangThai == "SUA")
                kq = phongBLL.SuaPhong(p);
            if (kq)
            {
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThoat_Click(sender, e);
                UCDSPhong_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Lưu thất bại! Có thể mã phòng đã tồn tại.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileGoc = openFileDialog.FileName;
                string tenAnh = Path.GetFileName(fileGoc);
                string folder = Path.Combine(Application.StartupPath, "Images", "Phong");

                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string dest = Path.Combine(folder, tenAnh);
                try
                {
                    File.Copy(fileGoc, dest, true);
                    pbAnh.Image = Image.FromFile(dest);
                    pbAnh.Tag = tenAnh;
                }
                catch { MessageBox.Show("Lỗi tải ảnh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            EnableEdit(false);
            trangThai = "";
            LoadDataGrid("", "", "");
            btnLamMoi_Click(sender, e);
            txtMaPhong.Text = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDsPhong, "Danh sách phòng", "DanhSachPhong");
        }
    }
}
