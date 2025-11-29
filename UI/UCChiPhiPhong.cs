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
    public partial class UCChiPhiPhong : UserControl
    {
        ChiPhiPhongBLL chiPhiPhongBLL = new ChiPhiPhongBLL();
        Functions functions = new Functions();
        string trangThai = "";
        private decimal giaPhongHienTai = 0;
        private int soSinhVienHienTai = 0;

        public UCChiPhiPhong()
        {
            InitializeComponent();
        }

        private void EnableEdit(bool enable)
        {
            cbTenToa.Enabled = enable;
            cbTenPhong.Enabled = enable;
            txtThang.Enabled = enable;
            txtNam.Enabled = enable;
            txtSoDien.Enabled = enable;
            txtSoNuoc.Enabled = enable;
            txtTien1SoDien.Enabled = enable;
            txtTien1SoNuoc.Enabled = enable;
            dtpNgayDong.Enabled = enable;
            cbNhanVienTao.Enabled = enable;
            txtTienDien.Enabled = false;
            txtTienNuoc.Enabled = false;
            txtTongTien.Enabled = false;
        }

        private void UCChiPhiPhong_Load(object sender, EventArgs e)
        {
            EnableEdit(false);
            btnTimKiem_Click(sender, e);

            DataTable dsToa = chiPhiPhongBLL.TatCaToa();
            functions.FillCombox(cbLeftToa, dsToa, "TenToa", "MaToa");

            DataTable dsNhanVien = chiPhiPhongBLL.TatCaNhanVien();
            functions.FillCombox(cbNhanVienTao, dsNhanVien, "HoTen", "MaNhanVien");

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = false;
            txtSoDien.TextChanged += TinhTien_TextChanged;
            txtTien1SoDien.TextChanged += TinhTien_TextChanged;
            txtSoNuoc.TextChanged += TinhTien_TextChanged;
            txtTien1SoNuoc.TextChanged += TinhTien_TextChanged;
        }

        private void cbLeftToa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLeftToa.SelectedIndex > 0)
            {
                cbLeftPhong.Enabled = true;
                string maToa = cbLeftToa.SelectedValue.ToString();
                DataTable dsPhong = chiPhiPhongBLL.TatCaPhong(maToa);
                functions.FillCombox(cbLeftPhong, dsPhong, "TenPhong", "MaPhong");
            }
            else
            {
                cbLeftPhong.Text = "";
                cbLeftPhong.Enabled = false;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maToa = cbLeftToa.SelectedValue?.ToString();
            string maPhong = cbLeftPhong.SelectedValue?.ToString();
            string thang = txtLeftThang.Text;
            string nam = txtLeftNam.Text;
            string maNhanVien = txtLeftMaNhanVien.Text;

            DataTable dsHoaDon = chiPhiPhongBLL.TimKiemHoaDon(maToa, maPhong, thang, nam, maNhanVien);
            dgvDanhSachHoaDon.DataSource = dsHoaDon;
            dgvDanhSachHoaDon.RowHeadersVisible = false;
            dgvDanhSachHoaDon.Columns[0].HeaderText = "Mã Phòng";
            dgvDanhSachHoaDon.Columns[1].HeaderText = "Tên Phòng";
            dgvDanhSachHoaDon.Columns[2].HeaderText = "Tên Tòa";
            dgvDanhSachHoaDon.Columns[3].HeaderText = "Tháng";
            dgvDanhSachHoaDon.Columns[4].HeaderText = "Năm";
            dgvDanhSachHoaDon.Columns[5].HeaderText = "Tổng Tiền";
            dgvDanhSachHoaDon.Columns[6].HeaderText = "Ngày Đóng";
            dgvDanhSachHoaDon.Columns[7].HeaderText = "Người Lập";

            decimal doanhThu = 0;
            for (int i = 0; i < dsHoaDon.Rows.Count; i++)
            {
                doanhThu += Convert.ToDecimal(dsHoaDon.Rows[i]["TongTien"]);
            }
            txtLeftDoanhThu.Text = doanhThu.ToString("N0");
        }

        private void dgvDanhSachHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThoat.Enabled = true;
            EnableEdit(false);

            DataGridViewRow row = dgvDanhSachHoaDon.Rows[e.RowIndex];
            string maPhong = row.Cells["MaPhong"].Value.ToString();
            int thang = Convert.ToInt32(row.Cells["Thang"].Value);
            int nam = Convert.ToInt32(row.Cells["Nam"].Value);
            ChiPhiPhongDTO cpp = chiPhiPhongBLL.ChiTietChiPhiPhong(maPhong, thang, nam);
            HienThiChiTiet(cpp);
        }

        private void HienThiChiTiet(ChiPhiPhongDTO cpp)
        {
            if (cpp == null) return;
            giaPhongHienTai = cpp.GiaPhong;
            soSinhVienHienTai = cpp.SoLuongSinhVien;
            cbTenToa.Text = cpp.TenToa;
            if (cbTenToa.SelectedValue != null)
            {
                DataTable dsPhong = chiPhiPhongBLL.TatCaPhong(cbTenToa.SelectedValue.ToString());
                functions.FillCombox(cbTenPhong, dsPhong, "TenPhong", "MaPhong");
            }
            cbTenPhong.Text = cpp.TenPhong;

            txtThang.Text = cpp.Thang.ToString();
            txtNam.Text = cpp.Nam.ToString();

            txtSoDien.Text = cpp.SoDien.ToString();
            txtSoNuoc.Text = cpp.SoNuoc.ToString("N2");
            txtTien1SoDien.Text = cpp.Tien1SoDien.ToString("N0");
            txtTien1SoNuoc.Text = cpp.Tien1SoNuoc.ToString("N0");

            dtpNgayDong.Value = cpp.NgayDong;
            cbNhanVienTao.SelectedValue = cpp.MaNhanVien;
            txtTienDien.Text = cpp.TienDien.ToString("N0");
            txtTienNuoc.Text = cpp.TienNuoc.ToString("N0");
            txtTongTien.Text = cpp.TongTien.ToString("N0");
            dgvSinhVienTrongPhong.DataSource = cpp.DanhSachSinhVien;
            dgvSinhVienTrongPhong.RowHeadersVisible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
            btnLamMoi_Click(sender, e);
            DataTable dsToa = chiPhiPhongBLL.TatCaToa();
            functions.FillCombox(cbTenToa, dsToa, "TenToa", "MaToa");

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
            cbTenToa.Enabled = false;
            cbTenPhong.Enabled = false;
            txtThang.Enabled = false;
            txtNam.Enabled = false;

            btnThem.Enabled = false;
            btnLamMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnLuu.Enabled = true;
            trangThai = "SUA";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cbTenPhong.SelectedValue == null && trangThai == "THEM")
            {
                MessageBox.Show("Vui lòng chọn Phòng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtThang.Text) || string.IsNullOrEmpty(txtNam.Text))
            {
                MessageBox.Show("Vui lòng nhập Tháng và Năm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbNhanVienTao.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên lập phiếu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int thang = int.Parse(txtThang.Text.Trim());
            int nam = int.Parse(txtNam.Text.Trim());
            int soDien = int.TryParse(txtSoDien.Text, out int sd) ? sd : 0;
            decimal soNuoc = decimal.TryParse(txtSoNuoc.Text, out decimal sn) ? sn : 0;
            decimal tien1Dien = decimal.TryParse(txtTien1SoDien.Text, out decimal t1d) ? t1d : 0;
            decimal tien1Nuoc = decimal.TryParse(txtTien1SoNuoc.Text, out decimal t1n) ? t1n : 0;

            decimal tienDien = soDien * tien1Dien;
            decimal tienNuoc = soNuoc * tien1Nuoc;

            decimal tienPhong = giaPhongHienTai * soSinhVienHienTai;


            ChiPhiPhongDTO cpp = new ChiPhiPhongDTO
            {
                MaPhong = trangThai == "SUA" ? dgvDanhSachHoaDon.CurrentRow.Cells["MaPhong"].Value.ToString() : cbTenPhong.SelectedValue.ToString(),
                Thang = thang,
                Nam = nam,
                SoDien = soDien,
                SoNuoc = soNuoc,
                Tien1SoDien = tien1Dien,
                Tien1SoNuoc = tien1Nuoc,
                TienDien = tienDien,
                TienNuoc = tienNuoc,
                TienPhong = tienPhong,
                NgayDong = dtpNgayDong.Value,
                NgayHetHan = dtpNgayDong.Value.AddMonths(1).AddDays(-1),
                MaNhanVien = cbNhanVienTao.SelectedValue.ToString()
            };

            bool ketQua = false;
            if (trangThai == "THEM")
            {
                ketQua = chiPhiPhongBLL.ThemChiPhiPhong(cpp);
                if (!ketQua)
                    MessageBox.Show("Thất bại! Có thể hóa đơn cho phòng này trong tháng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            else if (trangThai == "SUA")
            {
                ketQua = chiPhiPhongBLL.SuaChiPhiPhong(cpp);
                if (ketQua) MessageBox.Show("Cập nhật thành công!", "Thông báo");
                else MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ketQua)
            {
                btnTimKiem_Click(sender, e);
                btnThoat_Click(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPhong = dgvDanhSachHoaDon.CurrentRow.Cells["MaPhong"].Value.ToString();
            int thang = Convert.ToInt32(dgvDanhSachHoaDon.CurrentRow.Cells["Thang"].Value);
            int nam = Convert.ToInt32(dgvDanhSachHoaDon.CurrentRow.Cells["Nam"].Value);

            if (MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn phòng {maPhong} tháng {thang}/{nam}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (chiPhiPhongBLL.XoaChiPhiPhong(maPhong, thang, nam))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    btnTimKiem_Click(sender, e);
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi");
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbTenToa.SelectedIndex = -1;
            cbTenPhong.DataSource = null;
            cbTenPhong.Text = "";

            txtThang.Text = DateTime.Now.Month.ToString();
            txtNam.Text = DateTime.Now.Year.ToString();

            txtSoDien.Text = "0";
            txtSoNuoc.Text = "0";
            txtTien1SoDien.Text = "0";
            txtTien1SoNuoc.Text = "0";

            dtpNgayDong.Value = DateTime.Now;
            cbNhanVienTao.SelectedIndex = -1;

            txtTienDien.Text = "0";
            txtTienNuoc.Text = "0";
            txtTongTien.Text = "0";

            dgvSinhVienTrongPhong.DataSource = null;

            giaPhongHienTai = 0;
            soSinhVienHienTai = 0;
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

        private void cbTenToa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenToa.SelectedIndex > 0 && cbTenToa.SelectedValue != null)
            {
                string maToa = cbTenToa.SelectedValue.ToString();
                DataTable dsPhong = chiPhiPhongBLL.LayDsPhongCoSinhVien(maToa);
                functions.FillCombox(cbTenPhong, dsPhong, "TenPhong", "MaPhong");
            }
            else
            {
                cbTenPhong.DataSource = null;
            }
        }

        private void cbTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trangThai == "THEM" && cbTenPhong.SelectedValue != null)
            {
                string maPhong = cbTenPhong.SelectedValue.ToString();
                ChiPhiPhongDTO info = chiPhiPhongBLL.LayThongTinTinhToan(maPhong);
                giaPhongHienTai = info.GiaPhong;
                soSinhVienHienTai = info.SoLuongSinhVien;
                dgvSinhVienTrongPhong.DataSource = info.DanhSachSinhVien;
                TinhTien_TextChanged(null, null);
            }
        }

        private void TinhTien_TextChanged(object sender, EventArgs e)
        {
            if (trangThai == "") return;

            int.TryParse(txtSoDien.Text, out int sd);
            decimal.TryParse(txtTien1SoDien.Text, out decimal t1d);
            decimal.TryParse(txtSoNuoc.Text, out decimal sn);
            decimal.TryParse(txtTien1SoNuoc.Text, out decimal t1n);

            decimal tienDien = sd * t1d;
            decimal tienNuoc = sn * t1n;
            decimal tienPhong = giaPhongHienTai * soSinhVienHienTai;
            txtTienDien.Text = tienDien.ToString("N0");
            txtTienNuoc.Text = tienNuoc.ToString("N0");
            decimal tongTien = tienDien + tienNuoc + tienPhong;
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            functions.XuatFileExcel(dgvDanhSachHoaDon, "Danh sách chi phí phòng", "DanhSachChiPhiPhong");
        }

        private void btnThemExcel_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Chọn file Excel để nhập liệu";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dtExel = functions.DocFileExcel(openFileDialog.FileName);
                    int thanhCong = 0;
                    int thatBai = 0;
                    string chiTietLoi = "";

                    foreach (DataRow row in dtExel.Rows)
                    {
                        string maPhong = row["MaPhong"].ToString();
                        try
                        {
                            ChiPhiPhongDTO thongTinPhong = chiPhiPhongBLL.LayThongTinTinhToan(maPhong);
                            if (thongTinPhong.SoLuongSinhVien == 0)
                            {
                                thatBai++;
                                chiTietLoi += $"- Phòng {maPhong}: Không có sinh viên ở.\n";
                                continue;
                            }

                            int thang = Convert.ToInt32(row["Thang"]);
                            int nam = Convert.ToInt32(row["Nam"]);

                            ChiPhiPhongDTO cpp = new ChiPhiPhongDTO();
                            cpp.MaPhong = maPhong;
                            cpp.Thang = thang;
                            cpp.Nam = nam;

                            cpp.SoDien = Convert.ToInt32(row["SoDien"]);
                            cpp.SoNuoc = Convert.ToDecimal(row["SoNuoc"]);
                            cpp.Tien1SoDien = Convert.ToDecimal(row["Tien1SoDien"]);
                            cpp.Tien1SoNuoc = Convert.ToDecimal(row["Tien1SoNuoc"]);
                            cpp.MaNhanVien = row["MaNhanVien"].ToString();

                            cpp.TienDien = cpp.SoDien * cpp.Tien1SoDien;
                            cpp.TienNuoc = cpp.SoNuoc * cpp.Tien1SoNuoc;
                            cpp.TienPhong = thongTinPhong.GiaPhong * thongTinPhong.SoLuongSinhVien;

                            if (dtExel.Columns.Contains("NgayDong"))
                                cpp.NgayDong = DateTime.FromOADate(Convert.ToDouble(row["NgayDong"]));
                            else
                                cpp.NgayDong = DateTime.Now;
                            cpp.NgayHetHan = cpp.NgayDong.AddMonths(1).AddDays(-1);

                            if (chiPhiPhongBLL.ThemChiPhiPhong(cpp))
                            {
                                thanhCong++;
                            }
                            else
                            {
                                thatBai++;
                                chiTietLoi += $"- Phòng {maPhong}: Đã tồn tại hóa đơn tháng {thang}/{nam}.\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            thatBai++;
                            chiTietLoi += $"- Phòng {maPhong}: Lỗi dữ liệu ({ex.Message}).\n";
                        }
                    }

                    string thongBao = $"Hoàn tất nhập liệu!\n- Thành công: {thanhCong}\n- Thất bại: {thatBai}";
                    if (!string.IsNullOrEmpty(chiTietLoi))
                    {
                        thongBao += $"\n\nChi tiết lỗi:\n{chiTietLoi}";
                    }
                    MessageBox.Show(thongBao, "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnThoat_Click(sender, e);
                    btnTimKiem_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
