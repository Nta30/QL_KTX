using QL_KTX.DAL;
using QL_KTX.DTO;
using QL_KTX.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QL_KTX.BLL
{
    internal class SinhVienBLL
    {
        DienSinhVienDAL dienSinhVienDAL = new DienSinhVienDAL();
        QueQuanDAL queQuanDAL = new QueQuanDAL();
        KhoaDAL khoaDAL = new KhoaDAL();
        LopDAL lopDAL = new LopDAL();
        SinhVienDAL sinhVienDAL = new SinhVienDAL();

        public DataTable TatCaDienSinhVien()
        {
            return dienSinhVienDAL.TatCaDienSinhVien();
        }

        public DataTable TatCaQue()
        {
            return queQuanDAL.TatCaQue();
        }

        public DataTable TatCaKhoa()
        {
            return khoaDAL.TatCaKhoa();
        }

        public DataTable TatCaLop(string maKhoa)
        {
            return lopDAL.TimTheoMaKhoa(maKhoa);
        }

        public DataTable TimKiem(string maDienSinhVien, string maQue, string maKhoa, string maLop, string maSinhVien)
        {
            return sinhVienDAL.TimKiemSinhVien(maDienSinhVien, maQue, maKhoa, maLop, maSinhVien);
        }

        public SinhVienDTO ChiTietSinhVien(string maSinhVien)
        {
            DataTable data = sinhVienDAL.TimTheoMaSV(maSinhVien);
            if (data.Rows.Count == 0) return null;
            DataRow row = data.Rows[0];
            SinhVienDTO sv = new SinhVienDTO
            {
                maSinhVien = row["MaSinhVien"].ToString(),
                hoTen = row["HoTen"].ToString(),
                ngaySinh = Convert.ToDateTime(row["NgaySinh"]),
                gioiTinh = row["GioiTinh"].ToString(),
                soDienThoai = row["SoDienThoai"].ToString(),
                email = row["Email"].ToString(),
                anhSinhVien = row["AnhSinhVien"].ToString(),
                tenQue = row["TenQue"].ToString(),
                tenDienSinhVien = row["TenDienSinhVien"].ToString(),
                tenLop = row["TenLop"].ToString(),
                tenKhoa = row["TenKhoa"].ToString(),
                hoTenNguoiThan = row["HoTenNguoiThan"].ToString(),
                quanHe = row["QuanHe"].ToString(),
                soDienThoaiNguoiThan = row["SdtNguoiThan"].ToString(),
                tenPhong = row["TenPhong"].ToString(),
                tenToa = row["TenToa"].ToString()
            };

            return sv;
        }

        public bool ThemSinhVien(SinhVienDTO sv)
        {
            bool ketQuaSV = sinhVienDAL.ThemSinhVien(sv);

            bool ketQuaNT = true;
            if (!string.IsNullOrEmpty(sv.hoTenNguoiThan))
            {
                ketQuaNT = sinhVienDAL.ThemNguoiThan(
                    sv.maSinhVien,
                    sv.hoTenNguoiThan,
                    sv.quanHe,
                    sv.soDienThoaiNguoiThan
                );
            }

            return ketQuaSV && ketQuaNT;
        }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            bool ketQuaSV = sinhVienDAL.SuaSinhVien(sv);

            bool ketQuaNT = sinhVienDAL.CapNhatNguoiThan(
                sv.maSinhVien,
                sv.hoTenNguoiThan,
                sv.quanHe,
                sv.soDienThoaiNguoiThan
            );

            return ketQuaSV && ketQuaNT;
        }

        public bool KiemTraRangBuoc(string maSinhVien)
        {
            return sinhVienDAL.KiemTraRangBuoc(maSinhVien);
        }

        public bool XoaSinhVien(string maSinhVien)
        {
            return sinhVienDAL.XoaSinhVien(maSinhVien);
        }
    }
}
