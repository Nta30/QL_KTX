using QL_KTX.BLL;
using QL_KTX.DTO;
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
    public partial class UCDSToa : UserControl
    {
        ToaBLL toaBLL = new ToaBLL();
        private string trangThai = "";
        private string maToaDangChon = "";
        public UCDSToa()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtTenToa.Enabled = enable;
            txtSoTang.Enabled = enable;
            txtSoPhongToiDa.Enabled = enable;

            txtSoPhongHienTai.Enabled = false;
            txtSoPhongTrong.Enabled = false;
        }

        private void UCDSToa_Load(object sender, EventArgs e)
        {
            LoadDataGrid("", "");
            EnableEdit(false);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void LoadDataGrid(string tenToa, string soTang)
        {
            dgvDsToa.DataSource = toaBLL.TimKiem(tenToa, soTang);
            dgvDsToa.Columns["MaToa"].HeaderText = "Mã Tòa";
            dgvDsToa.Columns["TenToa"].HeaderText = "Tên Tòa";
            dgvDsToa.Columns["SoTang"].HeaderText = "Số Tầng";
            dgvDsToa.Columns["SoPhongToiDa"].HeaderText = "Tổng Số Phòng";
            dgvDsToa.Columns["SoPhongHienTai"].HeaderText = "Phòng Đang Ở";
            dgvDsToa.RowHeadersVisible = false;

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtLeftTenToa.Text.Trim();
            string tang = txtLeftSoTang.Text.Trim();
            LoadDataGrid(ten, tang);
        }

        private void dgvDsToa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            btnLamMoi.Enabled = true;
            btnIn.Enabled = true;
            EnableEdit(false);

            DataGridViewRow row = dgvDsToa.Rows[e.RowIndex];
            maToaDangChon = row.Cells["MaToa"].Value.ToString();

            txtTenToa.Text = row.Cells["TenToa"].Value.ToString();
            txtSoTang.Text = row.Cells["SoTang"].Value.ToString();
            txtSoPhongToiDa.Text = row.Cells["SoPhongToiDa"].Value.ToString();

            int soPhongMax = Convert.ToInt32(row.Cells["SoPhongToiDa"].Value);
            int soPhongDangO = Convert.ToInt32(row.Cells["SoPhongHienTai"].Value);

            txtSoPhongHienTai.Text = soPhongDangO.ToString();
            txtSoPhongTrong.Text = (soPhongMax - soPhongDangO).ToString();

            LoadGridPhong(maToaDangChon);
        }

        private void LoadGridPhong(string maToa)
        {
            dgvDsPhong.DataSource = toaBLL.LayDsPhong(maToa);
            if (dgvDsPhong.Columns.Count > 0)
            {
                dgvDsPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
                dgvDsPhong.Columns["TenPhong"].HeaderText = "Tên Phòng";
                dgvDsPhong.Columns["TenLoaiPhong"].HeaderText = "Loại";
                dgvDsPhong.Columns["GiaPhong"].HeaderText = "Giá";
                dgvDsPhong.Columns["SoNguoiDangO"].HeaderText = "SV Đang Ở";

                dgvDsPhong.RowHeadersVisible = false;
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
            btnIn.Enabled = false;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;

            txtTenToa.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = "SUA";
            EnableEdit(true);

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (toaBLL.KiemTraRangBuoc(maToaDangChon))
            {
                MessageBox.Show("Không thể xóa tòa này vì đang có phòng đã được tạo!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn chắc chắn xóa tòa {txtTenToa.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (toaBLL.XoaToa(maToaDangChon))
                {
                    MessageBox.Show("Xóa thành công!");
                    UCDSToa_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int soTang = 0;
            if (!int.TryParse(txtSoTang.Text, out soTang) || soTang <= 0) { MessageBox.Show("Số tầng không hợp lệ!"); return; }

            int soPhongMax = 0;
            if (!int.TryParse(txtSoPhongToiDa.Text, out soPhongMax) || soPhongMax <= 0) { MessageBox.Show("Số phòng tối đa không hợp lệ!"); return; }

            ToaDTO toa = new ToaDTO
            {
                MaToa = maToaDangChon,
                TenToa = txtTenToa.Text.Trim(),
                SoTang = soTang,
                SoPhongToiDa = soPhongMax
            };

            bool kq = false;
            if (trangThai == "THEM")
                kq = toaBLL.ThemToa(toa);
            else if (trangThai == "SUA")
                kq = toaBLL.SuaToa(toa);

            if (kq)
            {
                MessageBox.Show("Lưu thành công!");
                UCDSToa_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Lưu thất bại!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenToa.Text = "";
            txtSoTang.Text = "";
            txtSoPhongToiDa.Text = "";
            txtSoPhongHienTai.Text = "";
            txtSoPhongTrong.Text = "";
            dgvDsPhong.DataSource = null;
            maToaDangChon = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            EnableEdit(false);
            trangThai = "";
            maToaDangChon = "";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }
    }
}
