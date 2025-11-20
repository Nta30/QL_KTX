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
    public partial class UCViPham : UserControl
    {
        ViPhamBLL viPhamBLL = new ViPhamBLL();
        SinhVienBLL sinhVienBLL = new SinhVienBLL();
        Functions functions = new Functions();
        string trangThai = "";
        public UCViPham()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            txtMaSinhVien.Enabled = enable;
            rtbNdViPham.Enabled = enable;
            dtpNgayViPham.Enabled = enable;
            txtTienPhat.Enabled = enable;
        }

        private void UCViPham_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            dtpLeftNgayViPham.CustomFormat = " ";
            DataTable dsViPham = viPhamBLL.TimKiem(null, "");
            dgvViPham.DataSource = dsViPham;
            dgvViPham.Columns[0].HeaderText = "Mã Vi Phạm";
            dgvViPham.Columns[1].HeaderText = "Mã Sinh Viên";
            dgvViPham.Columns[2].HeaderText = "Họ Tên";
            dgvViPham.Columns[3].HeaderText = "Ngày Vi Phạm";
            dgvViPham.Columns[4].HeaderText = "Tiền Vi Phạm";
            dgvViPham.RowHeadersVisible = false;

            txtSoViPham.Text = dsViPham.Rows.Count.ToString();
            decimal tongTien = dsViPham.AsEnumerable().Sum(r => r.Field<decimal>("TienViPham"));
            txtTongTien.Text = tongTien.ToString();

            btnSua.Enabled = false;
            btnLamMoi.Enabled = false;
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThoat.Enabled = false;
        }

        private void dtpLeftNgayViPham_ValueChanged(object sender, EventArgs e)
        {
            dtpLeftNgayViPham.CustomFormat = "MM/dd/yyyy";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime? ngayViPham = dtpLeftNgayViPham.Value;
            if(dtpLeftNgayViPham.CustomFormat == " ")
            {
                ngayViPham = null;
            }
            string tenSinhVien = txtLeftHoTen.Text;

            DataTable dsViPham = viPhamBLL.TimKiem(ngayViPham, tenSinhVien);
            dgvViPham.DataSource = dsViPham;
            dgvViPham.Columns[0].HeaderText = "Mã Vi Phạm";
            dgvViPham.Columns[1].HeaderText = "Mã Sinh Viên";
            dgvViPham.Columns[2].HeaderText = "Họ Tên";
            dgvViPham.Columns[3].HeaderText = "Ngày Vi Phạm";
            dgvViPham.Columns[4].HeaderText = "Tiền Vi Phạm";
            dgvViPham.RowHeadersVisible = false;

            txtSoViPham.Text = dsViPham.Rows.Count.ToString();
            decimal tongTien = dsViPham.AsEnumerable().Sum(r => r.Field<decimal>("TienViPham"));
            txtTongTien.Text = tongTien.ToString();
        }

        private void dgvViPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
                btnTimKiem_Click(sender, e);
                return;
            }
            string maViPham = dgvViPham.Rows[e.RowIndex].Cells["MaViPham"].Value.ToString();
            ViPhamDTO vipham = viPhamBLL.ChiTietViPham(maViPham);
            HienThiChiTietViPham(vipham);
        }

        private void HienThiChiTietViPham(ViPhamDTO viPham)
        {
            txtMaSinhVien.Text = viPham.maSinhVien;
            txtHoTen.Text = viPham.hoTen;
            txtSdt.Text = viPham.soDienThoai;
            cbGioiTinh.Text = viPham.gioiTinh;
            cbPhong.Text = viPham.tenPhong;
            cbToa.Text = viPham.tenToa;
            txtEmail.Text = viPham.email;
            dtpNgayViPham.Value = viPham.ngayViPham;
            rtbNdViPham.Text = viPham.noiDungViPham;
            txtTienPhat.Text = viPham.tienViPham.ToString();

            try
            {
                pbAnh.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\SinhVien\" + viPham.anhSinhVien));
                pbAnh.Tag = viPham.anhSinhVien;
            }
            catch
            {
                pbAnh.Image = null;
            }
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Text = "";
            txtHoTen.Text = "";
            txtSdt.Text = "";
            cbGioiTinh.Text = "";
            cbPhong.Text = "";
            cbToa.Text = "";
            txtEmail.Text = "";
            dtpNgayViPham.Value = DateTime.Now;
            rtbNdViPham.Text = "";
            txtTienPhat.Text = "";
            pbAnh.Image = null;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            btnLamMoi_Click(sender, e);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnLuu.Enabled = true;
            btnLamMoi.Enabled = true;
            btnThoat.Enabled = true;
            trangThai = "THEM";
        }

        private void txtMaSinhVien_TextChanged(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text;
            if (maSinhVien.Length < 4)
            {
                txtHoTen.Text = "";
                txtSdt.Text = "";
                cbGioiTinh.Text = "";
                cbPhong.Text = "";
                cbToa.Text = "";
                txtEmail.Text = "";
            }
            SinhVienDTO sv = sinhVienBLL.ChiTietSinhVien(maSinhVien);
            if (sv != null)
            {
                txtMaSinhVien.Text = sv.maSinhVien;
                txtHoTen.Text = sv.hoTen;
                txtSdt.Text = sv.soDienThoai;
                cbGioiTinh.Text = sv.gioiTinh;
                cbPhong.Text = sv.tenPhong;
                cbToa.Text = sv.tenToa;
                txtEmail.Text = sv.email;
                pbAnh.Image = null;
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
            trangThai = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSinhVien.Text.Trim()))
            {
                MessageBox.Show("Mã Sinh Viên không được để trống.", "Lỗi Dữ Liệu");
                txtMaSinhVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(rtbNdViPham.Text.Trim()))
            {
                MessageBox.Show("Nội dung Vi Phạm không được để trống.", "Lỗi Dữ Liệu");
                rtbNdViPham.Focus();
                return;
            }
            if (!decimal.TryParse(txtTienPhat.Text.Trim(), out _))
            {
                MessageBox.Show("Tiền Phạt không hợp lệ.", "Lỗi Dữ Liệu");
                txtTienPhat.Focus();
                return;
            }
            DataTable viPham = viPhamBLL.TimKiem(null, "");
            string maViPhamMoi = "VP00";
            ViPhamDTO vp = new ViPhamDTO
            {
                maViPham = trangThai == "SUA" ? dgvViPham.CurrentRow.Cells["MaViPham"].Value.ToString() : (maViPhamMoi + (viPham.Rows.Count + 1)),
                maSinhVien = txtMaSinhVien.Text.Trim(),
                ngayViPham = dtpNgayViPham.Value,
                noiDungViPham = rtbNdViPham.Text.Trim(),
                tienViPham = Convert.ToDecimal(txtTienPhat.Text.Trim())
            };
            bool ketQua = false;
            if (trangThai == "THEM")
            {
                ketQua = viPhamBLL.ThemViPham(vp);
                MessageBox.Show(ketQua ? "Thêm Vi Phạm thành công!" : "Thêm Vi Phạm thất bại!", "Thông Báo");
            }
            else if (trangThai == "SUA")
            {
                ketQua = viPhamBLL.SuaViPham(vp);
                MessageBox.Show(ketQua ? "Cập nhật Vi Phạm thành công!" : "Cập nhật Vi Phạm thất bại!", "Thông Báo");
            }

            if (ketQua)
            {
                btnTimKiem_Click(sender, e);
                btnThoat_Click(sender, e);
                UCViPham_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maViPham = dgvViPham.CurrentRow.Cells["MaViPham"].Value.ToString();
            string maSinhVien = txtMaSinhVien.Text;
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa vi phạm của sinh viên {txtHoTen.Text} có Mã là {maSinhVien} không?",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                bool ketQua = viPhamBLL.XoaViPham(maViPham);
                if (ketQua)
                {
                    MessageBox.Show("Xóa Vi Phạm thành công!", "Thông Báo");
                    btnTimKiem_Click(sender, e);
                    btnThoat_Click(sender, e);
                    UCViPham_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa Vi Phạm thất bại!", "Thông Báo Lỗi");
                }
            }
        }
    }
}
