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
    public partial class UCNhanVien : UserControl
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        Functions functions = new Functions();
        private string trangThai = "";
        public UCNhanVien()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtHoTen.Enabled = enable;
            txtLuong.Enabled = enable;
            cbChucVu.Enabled = enable;
            txtCanCuocCongDan.Enabled = enable;
            txtSdt.Enabled = enable;
            cbQueQuan.Enabled = enable;
        }

        private void UCNhanVien_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            DataTable chucVu = nhanVienBLL.TatCaChucVu();
            functions.FillCombox(cbLeftChucVu, chucVu, "TenChucVu", "MaChucVu");
            functions.FillCombox(cbChucVu, chucVu, "TenChucVu", "MaChucVu");

            DataTable queQuan = nhanVienBLL.TatCaQue();
            functions.FillCombox(cbLeftQueQuan, queQuan, "TenQue", "MaQue");
            functions.FillCombox(cbQueQuan, queQuan, "TenQue", "MaQue");
            LoadDataGrid("", "", "");

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void LoadDataGrid(string maSinhVien ,string maChucVu, string maQue)
        {
            DataTable dsNhanVien = nhanVienBLL.TimKiem(maSinhVien ,maChucVu, maQue);
            dgvDSNhanVien.DataSource = dsNhanVien;
            dgvDSNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV";
            dgvDSNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDSNhanVien.Columns["CCCD"].HeaderText = "CCCD";
            dgvDSNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvDSNhanVien.Columns["TenChucVu"].HeaderText = "Chức Vụ";
            dgvDSNhanVien.Columns["TenQue"].HeaderText = "Quê Quán";
            dgvDSNhanVien.Columns["Luong"].HeaderText = "Lương";

            dgvDSNhanVien.RowHeadersVisible = false;
            TinhThongKe(dsNhanVien);
        }

        private void TinhThongKe(DataTable dt)
        {
            txtLeftTongSoNhanVien.Text = dt.Rows.Count.ToString();
            decimal tongLuong = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Luong"] != DBNull.Value)
                {
                    tongLuong += Convert.ToDecimal(row["Luong"]);
                }
            }
            txtLeftTongLuong.Text = tongLuong.ToString("N0");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtLeftMaNhanVien.Text;
            string maChucVu = cbLeftChucVu.SelectedValue?.ToString() ?? "";
            string maQue = cbLeftQueQuan.SelectedValue?.ToString() ?? "";

            LoadDataGrid(maNhanVien, maChucVu, maQue);
        }

        private void dgvDSNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            EnableEdit(false);

            string maNhanVien = dgvDSNhanVien.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
            NhanVienDTO nv = nhanVienBLL.ChiTietNhanVien(maNhanVien);
            HienThiChiTiet(nv);
        }
        private void HienThiChiTiet(NhanVienDTO nv)
        {
            if (nv == null) return;
            txtMaNhanVien.Text = nv.MaNhanVien;
            txtHoTen.Text = nv.HoTen;
            txtLuong.Text = nv.Luong.ToString("N0");
            cbChucVu.SelectedValue = nv.MaChucVu;
            txtCanCuocCongDan.Text = nv.CCCD;
            txtSdt.Text = nv.SoDienThoai;
            cbQueQuan.SelectedValue = nv.MaQue;

            try
            {
                if (!string.IsNullOrEmpty(nv.AnhNhanVien))
                {
                    string imagePath = Path.Combine(Application.StartupPath, nv.AnhNhanVien);
                    if (File.Exists(imagePath))
                    {
                        pbAnh.Image = Image.FromFile(imagePath);
                        pbAnh.Tag = nv.AnhNhanVien;
                    }
                    else
                    {
                        string folderPath = Path.Combine(Application.StartupPath, "Images", "NhanVien", nv.AnhNhanVien);
                        if (File.Exists(folderPath))
                        {
                            pbAnh.Image = Image.FromFile(folderPath);
                            pbAnh.Tag = nv.AnhNhanVien;
                        }
                        else
                        {
                            pbAnh.Image = null;
                        }
                    }
                }
                else
                {
                    pbAnh.Image = null;
                }
            }
            catch
            {
                pbAnh.Image = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            txtMaNhanVien.Text = functions.SinhMaTuDong("NV");
            EnableEdit(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnThoat.Enabled = true;
            btnLamMoi.Enabled = true;
            btnAnh.Visible = true;

            txtLeftMaNhanVien.Enabled = false;
            cbLeftChucVu.Enabled = false;
            cbLeftQueQuan.Enabled = false;

            trangThai = "THEM";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            txtMaNhanVien.Enabled = false;

            btnThem.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnLuu.Enabled = true;
            btnAnh.Visible = true;

            txtLeftMaNhanVien.Enabled = false;
            cbLeftChucVu.Enabled = false;
            cbLeftQueQuan.Enabled = false;
            trangThai = "SUA";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên nhân viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập Lương!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuong.Focus();
                return;
            }
            if (cbChucVu.SelectedIndex == 0 || cbChucVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Chức vụ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbChucVu.Focus();
                cbChucVu.DroppedDown = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCanCuocCongDan.Text))
            {
                MessageBox.Show("Vui lòng nhập Căn cước công dân!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCanCuocCongDan.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdt.Focus();
                return;
            }
            if (cbQueQuan.SelectedIndex == 0 || cbQueQuan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Quê quán!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbQueQuan.Focus();
                cbQueQuan.DroppedDown = true;
                return;
            }
            if (pbAnh.Image == null)
            {
                MessageBox.Show("Vui lòng chọn Ảnh đại diện cho nhân viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal luong = 0;
            if (!decimal.TryParse(txtLuong.Text, out luong))
            {
                MessageBox.Show("Lương phải là một con số hợp lệ!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLuong.Focus();
                txtLuong.SelectAll();
                return;
            }

            NhanVienDTO nv = new NhanVienDTO
            {
                MaNhanVien = txtMaNhanVien.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Luong = luong,
                CCCD = txtCanCuocCongDan.Text.Trim(),
                SoDienThoai = txtSdt.Text.Trim(),
                MaChucVu = cbChucVu.SelectedValue.ToString(),
                MaQue = cbQueQuan.SelectedValue.ToString(),
                AnhNhanVien = pbAnh.Tag != null ? pbAnh.Tag.ToString() : ""
            };

            bool ketQua = false;

            if (trangThai == "THEM")
            {
                NhanVienDTO checkTonTai = nhanVienBLL.ChiTietNhanVien(nv.MaNhanVien);
                if (checkTonTai != null)
                {
                    MessageBox.Show($"Mã nhân viên {nv.MaNhanVien} đã tồn tại!", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNhanVien.Focus();
                    return;
                }

                ketQua = nhanVienBLL.ThemNhanVien(nv);
            }
            else if (trangThai == "SUA")
            {
                ketQua = nhanVienBLL.SuaNhanVien(nv);
            }

            if (ketQua)
            {
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThoat_Click(sender, e);
                UCNhanVien_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Lưu thất bại! Vui lòng kiểm tra lại hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileGoc = openFileDialog.FileName;
                string tenAnh = Path.GetFileName(fileGoc);

                string folder = Path.Combine(Application.StartupPath, "Images", "NhanVien");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileDich = Path.Combine(folder, tenAnh);
                try
                {
                    File.Copy(fileGoc, fileDich, true);
                    pbAnh.Image = Image.FromFile(fileDich);
                    pbAnh.Tag = tenAnh;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu ảnh: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Text = "";
            txtLuong.Text = "";
            txtCanCuocCongDan.Text = "";
            txtSdt.Text = "";
            cbChucVu.SelectedIndex = -1;
            cbQueQuan.SelectedIndex = -1;
            pbAnh.Image = null;
            pbAnh.Tag = null;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien)) return;

            if (nhanVienBLL.KiemTraRangBuoc(maNhanVien))
            {
                MessageBox.Show(
                   "Không thể xóa nhân viên này vì dữ liệu đang được sử dụng",
                   "Lỗi Xóa Dữ Liệu",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên {txtHoTen.Text}?",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (nhanVienBLL.XoaNhanVien(maNhanVien))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThoat_Click(sender, e);
                    UCNhanVien_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            txtMaNhanVien.Text = "";
            EnableEdit(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnAnh.Visible = false;

            txtLeftMaNhanVien.Enabled = true;
            cbLeftChucVu.Enabled = true;
            cbLeftQueQuan.Enabled = true;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDSNhanVien, "Danh sách nhân viên", "DanhSachNhanVien");
        }
    }
}
