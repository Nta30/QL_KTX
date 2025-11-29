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
    public partial class UCThietBi : UserControl
    {
        ThietBiBLL bll = new ThietBiBLL();
        Functions functions = new Functions();
        private string trangThai = "";
        private string maThietBiDangChon = "";

        public UCThietBi()
        {
            InitializeComponent();
        }

        private void UCThietBi_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDataGrid("", "");
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
            DataTable dt = bll.TatCaLoaiPhong();
            functions.FillCombox(cbLeftLoaiPhong, dt, "TenLoaiPhong", "MaLoaiPhong");
        }

        private void EnableEdit(bool enable)
        {
            txtTenThietBi.Enabled = enable;
            txtGiaTien.Enabled = enable;
        }
        private void LoadDataGrid(string maLP, string tenTB)
        {
            DataTable dt = bll.TimKiem(maLP, tenTB);
            dgvDsThietBi.DataSource = dt;

            if (dgvDsThietBi.Columns.Count > 0)
            {
                dgvDsThietBi.Columns["MaThietBi"].HeaderText = "Mã TB";
                dgvDsThietBi.Columns["TenThietBi"].HeaderText = "Tên Thiết Bị";
                dgvDsThietBi.Columns["GiaTien"].HeaderText = "Giá Tiền";
                dgvDsThietBi.RowHeadersVisible = false;
            }

            TinhTong(dt);
        }

        private void TinhTong(DataTable dt)
        {
            txtTongSoThietBi.Text = dt.Rows.Count.ToString();
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                tong += Convert.ToDecimal(row["GiaTien"]);
            }
            txtTongTien.Text = tong.ToString("N0");
        }

        private void dgvDsThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            btnLamMoi.Enabled = true;
            EnableEdit(false);

            DataGridViewRow row = dgvDsThietBi.Rows[e.RowIndex];
            maThietBiDangChon = row.Cells["MaThietBi"].Value.ToString();

            txtMaThietBi.Text = maThietBiDangChon;
            txtTenThietBi.Text = row.Cells["TenThietBi"].Value.ToString();
            txtGiaTien.Text = Convert.ToDecimal(row.Cells["GiaTien"].Value).ToString("N0");

            LoadGridPhong(maThietBiDangChon);
        }

        private void LoadGridPhong(string maTB)
        {
            dgvDsPhong.DataSource = bll.LayDsPhong(maTB);
            if (dgvDsPhong.Columns.Count > 0)
            {
                dgvDsPhong.Columns["MaPhong"].HeaderText = "Phòng";
                dgvDsPhong.Columns["TenPhong"].HeaderText = "Tên Phòng";
                dgvDsPhong.Columns["TenToa"].HeaderText = "Tòa";
                dgvDsPhong.Columns["TenLoaiPhong"].HeaderText = "Loại";
                dgvDsPhong.Columns["SoLuongThietBi"].HeaderText = "SL";

                dgvDsPhong.RowHeadersVisible = false;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenThietBi.Text = "";
            txtGiaTien.Text = "";
            maThietBiDangChon = "";
            dgvDsPhong.DataSource = null;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
            trangThai = "THEM";
            txtMaThietBi.Text = functions.SinhMaTuDong("TB");
            EnableEdit(true);

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;

            txtTenThietBi.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = "SUA";
            EnableEdit(true);

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bll.KiemTraRangBuoc(maThietBiDangChon))
            {
                MessageBox.Show("Không thể xóa thiết bị đang được sử dụng trong loại phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn chắc chắn xóa {txtTenThietBi.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bll.XoaThietBi(maThietBiDangChon))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThoat_Click(sender, e);
                    UCThietBi_Load(sender, e);
                }
                else MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            decimal gia = 0;
            if (!decimal.TryParse(txtGiaTien.Text, out gia))
            {
                MessageBox.Show("Giá tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ThietBiDTO tb = new ThietBiDTO
            {
                MaThietBi = txtMaThietBi.Text,
                TenThietBi = txtTenThietBi.Text.Trim(),
                GiaTien = gia
            };

            bool kq = false;
            if (trangThai == "THEM") kq = bll.ThemThietBi(tb);
            else if (trangThai == "SUA") kq = bll.SuaThietBi(tb);

            if (kq)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThoat_Click(sender, e);
                UCThietBi_Load(sender, e);
            }
            else MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            EnableEdit(false);
            btnLamMoi_Click(sender, e);
            txtMaThietBi.Text = "";
            trangThai = "";
            maThietBiDangChon = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenTB = txtLeftTenThietBi.Text.Trim();
            string maLP = cbLeftLoaiPhong.SelectedValue?.ToString() ?? "";
            LoadDataGrid(maLP, tenTB);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDsThietBi, "Danh sách thiết bị", "DanhSachThietBi");
        }
    }
}

