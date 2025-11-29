using QL_KTX.BLL;
using QL_KTX.DTO;
using QL_KTX.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX.UI
{
    public partial class UCLoaiPhong : UserControl
    {
        LoaiPhongBLL bll = new LoaiPhongBLL();
        Functions functions = new Functions();
        private string trangThai = "";
        private BindingList<LoaiPhongThietBiDTO> danhSachThietBiTam = new BindingList<LoaiPhongThietBiDTO>();

        public UCLoaiPhong()
        {
            InitializeComponent();
        }

        public void EnableEdit(bool enable)
        {
            txtTenLoaiPhong.Enabled = enable;
            txtGiaPhong.Enabled = enable;
            txtSoNguoiToiDa.Enabled = enable;
            rtbGhiChu.Enabled = enable;

            cbThietBi.Visible = enable;
            btnThemThietBi.Visible = enable;
            btnXoaThietBi.Visible = enable;
            lblThietBi.Visible = enable;

            dgvDsThietBi.ReadOnly = !enable;
            if (enable)
            {
                dgvDsThietBi.Columns["TenThietBi"].ReadOnly = true;
                dgvDsThietBi.Columns["SoLuong"].ReadOnly = false;
                dgvDsThietBi.Columns["GhiChu"].ReadOnly = false;
            }
        }

        private void LoadComboBoxThietBi()
        {
            DataTable dt = bll.TatCaThietBi();
            functions.FillCombox(cbThietBi, dt, "TenThietBi", "MaThietBi");
        }

        private void ConfigGridThietBi()
        {
            if (dgvDsThietBi.Columns.Count > 0)
            {
                dgvDsThietBi.Columns["MaThietBi"].Visible = false;
                dgvDsThietBi.Columns["TenThietBi"].HeaderText = "Tên Thiết Bị";
                dgvDsThietBi.Columns["TenThietBi"].ReadOnly = true;

                dgvDsThietBi.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvDsThietBi.Columns["SoLuong"].ReadOnly = false;

                dgvDsThietBi.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgvDsThietBi.RowHeadersVisible = false;
            }
        }

        private void UCLoaiPhong_Load(object sender, EventArgs e)
        {
            LoadDataGrid("", "", "");
            LoadComboBoxThietBi();

            dgvDsThietBi.DataSource = danhSachThietBiTam;
            ConfigGridThietBi();

            EnableEdit(false);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void LoadDataGrid(string ten, string gia, string soNguoi)
        {
            dgvDsLoaiPhong.DataSource = bll.TimKiem(ten, gia, soNguoi);
            dgvDsLoaiPhong.RowHeadersVisible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtLeftTenLoaiPhong.Text.Trim();
            string gia = txtLeftGiaPhong.Text.Trim();
            string songuoi = txtLeftSoNguoiToiDa.Text.Trim();
            LoadDataGrid(ten, gia, songuoi);
        }

        private void dgvDsLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            EnableEdit(false);
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
            btnLamMoi.Enabled = false;

            DataGridViewRow row = dgvDsLoaiPhong.Rows[e.RowIndex];
            string maLP = row.Cells["MaLoaiPhong"].Value.ToString();

            txtMaLoaiPhong.Text = maLP;
            txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value.ToString();
            txtGiaPhong.Text = row.Cells["GiaPhong"].Value.ToString();
            txtSoNguoiToiDa.Text = row.Cells["SoNguoiToiDa"].Value.ToString();
            rtbGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            List<LoaiPhongThietBiDTO> listDB = bll.LayThietBi(maLP);
            danhSachThietBiTam.Clear();
            foreach (var item in listDB)
            {
                danhSachThietBiTam.Add(item);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = "THEM";
            btnLamMoi_Click(sender, e);
            txtMaLoaiPhong.Text = functions.SinhMaTuDong("LP");
            EnableEdit(true);
            cbThietBi.SelectedIndex = -1;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;

            txtMaLoaiPhong.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenLoaiPhong.Text = "";
            txtGiaPhong.Text = "";
            txtSoNguoiToiDa.Text = "";
            rtbGhiChu.Text = "";
            cbThietBi.SelectedIndex = -1;

            danhSachThietBiTam.Clear();
        }

        private void btnThemThietBi_Click(object sender, EventArgs e)
        {
            if (cbThietBi.SelectedIndex == -1) return;

            string maTB = cbThietBi.SelectedValue.ToString();
            string tenTB = cbThietBi.Text;

            var itemTonTai = danhSachThietBiTam.FirstOrDefault(x => x.MaThietBi == maTB);

            if (itemTonTai != null)
            {
                itemTonTai.SoLuong++;
                dgvDsThietBi.Refresh();
            }
            else
            {
                danhSachThietBiTam.Add(new LoaiPhongThietBiDTO
                {
                    MaThietBi = maTB,
                    TenThietBi = tenTB,
                    SoLuong = 1,
                    GhiChu = ""
                });
            }
        }

        private void btnXoaThietBi_Click(object sender, EventArgs e)
        {
            if (dgvDsThietBi.CurrentRow != null)
            {
                LoaiPhongThietBiDTO item = (LoaiPhongThietBiDTO)dgvDsThietBi.CurrentRow.DataBoundItem;
                danhSachThietBiTam.Remove(item);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = "SUA";
            EnableEdit(true);
            txtMaLoaiPhong.Enabled = false;
            cbThietBi.SelectedIndex = -1;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            decimal gia = 0;
            if (!decimal.TryParse(txtGiaPhong.Text, out gia)) { MessageBox.Show("Giá phòng không hợp lệ!"); return; }

            int songuoi = 0;
            if (!int.TryParse(txtSoNguoiToiDa.Text, out songuoi)) { MessageBox.Show("Số người tối đa không hợp lệ!"); return; }

            LoaiPhongDTO lp = new LoaiPhongDTO
            {
                MaLoaiPhong = txtMaLoaiPhong.Text.Trim(),
                TenLoaiPhong = txtTenLoaiPhong.Text.Trim(),
                GiaPhong = gia,
                SoNguoiToiDa = songuoi,
                GhiChu = rtbGhiChu.Text.Trim()
            };

            List<LoaiPhongThietBiDTO> listTB = danhSachThietBiTam.ToList();

            bool kq = false;
            if (trangThai == "THEM")
                kq = bll.ThemLoaiPhong(lp, listTB);
            else if (trangThai == "SUA")
                kq = bll.SuaLoaiPhong(lp, listTB);

            if (kq)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThoat_Click(sender, e);
                UCLoaiPhong_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Lưu thất bại! Có thể trùng mã hoặc lỗi hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiPhong.Text)) return;

            if (bll.KiemTraRangBuoc(txtMaLoaiPhong.Text))
            {
                MessageBox.Show("Không thể xóa loại phòng này vì đang có phòng sử dụng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bll.XoaLoaiPhong(txtMaLoaiPhong.Text))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThoat_Click(sender, e);
                    UCLoaiPhong_Load(sender, e);
                }
                else MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            EnableEdit(false);
            btnLamMoi_Click(sender, e);
            txtMaLoaiPhong.Text = "";
            trangThai = "";
            LoadDataGrid("", "", "");
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDsLoaiPhong, "Danh sách loại phòng", "DanhSachLoaiPhong");
        }
    }
}
