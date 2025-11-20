using QL_KTX.DAL;
using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.BLL
{
    internal class ChiPhiPhongBLL
    {
        ChiPhiPhongDAL chiPhiPhongDAL = new ChiPhiPhongDAL();
        ToaDAL toaDAL = new ToaDAL();
        PhongDAL phongDAL = new PhongDAL();
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        SinhVienDAL sinhVienDAL = new SinhVienDAL();

        public DataTable TimKiemHoaDon(string maToa, string maPhong, int? thang, int? nam, string maNhanVien)
        {
            return chiPhiPhongDAL.TimKiemHoaDon(maToa, maPhong, thang, nam, maNhanVien);
        }
        public DataTable TatCaToa()
        {
            return toaDAL.TatCaToa();
        }
        public DataTable TatCaPhong(string maToa)
        {
            return phongDAL.TimTheoToa(maToa);
        }
        public DataTable TatCaNhanVien()
        {
            return nhanVienDAL.TatCaNhanVien();
        }

        public DataTable SinhVienTrongPhong(string maPhong)
        {
            SinhVienDAL svDAL = new SinhVienDAL();
            return svDAL.SinhVienTrongPhong(maPhong);
        }

        public ChiPhiPhongDTO ChiTietChiPhiPhong(string maPhong, int thang, int nam)
        {
            DataTable data = chiPhiPhongDAL.ChiTietChiPhiPhong(maPhong, thang, nam);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            ChiPhiPhongDTO cpp = new ChiPhiPhongDTO
            {
                MaPhong = row["MaPhong"].ToString(),
                Thang = Convert.ToInt32(row["Thang"]),
                Nam = Convert.ToInt32(row["Nam"]),
                TienDien = Convert.ToDecimal(row["TienDien"]),
                TienNuoc = Convert.ToDecimal(row["TienNuoc"]),
                TienDichVu = row["TienDichVu"] != DBNull.Value ? Convert.ToDecimal(row["TienDichVu"]) : 0,
                SoDien = Convert.ToInt32(row["SoDien"]),
                SoNuoc = Convert.ToDecimal(row["SoNuoc"]),
                Tien1SoDien = Convert.ToDecimal(row["Tien1SoDien"]),
                Tien1SoNuoc = Convert.ToDecimal(row["Tien1SoNuoc"]),
                NgayDong = Convert.ToDateTime(row["NgayDong"]),
                NgayHetHan = row["NgayHetHan"] != DBNull.Value ? Convert.ToDateTime(row["NgayHetHan"]) : DateTime.Now,
                MaNhanVien = row["MaNhanVien"].ToString(),
                TenPhong = row["TenPhong"].ToString(),
                TenToa = row["TenToa"].ToString(),
                HoTenNhanVien = row["HoTenNhanVien"].ToString()
            };
            cpp.TongTien = cpp.TienDien + cpp.TienNuoc + cpp.TienDichVu;
            cpp.DanhSachSinhVien = chiPhiPhongDAL.SinhVienTrongPhong(maPhong);
            DataTable dtPhong = chiPhiPhongDAL.LayThongTinPhongDeTinhTien(maPhong);
            if (dtPhong.Rows.Count > 0)
            {
                cpp.GiaPhong = Convert.ToDecimal(dtPhong.Rows[0]["GiaPhong"]);
                cpp.SoLuongSinhVien = Convert.ToInt32(dtPhong.Rows[0]["SoLuongSinhVien"]);
            }

            return cpp;
        }

        public ChiPhiPhongDTO LayThongTinTinhToan(string maPhong)
        {
            ChiPhiPhongDTO info = new ChiPhiPhongDTO { MaPhong = maPhong };
            DataTable dt = chiPhiPhongDAL.LayThongTinPhongDeTinhTien(maPhong);
            if (dt != null && dt.Rows.Count > 0)
            {
                info.GiaPhong = Convert.ToDecimal(dt.Rows[0]["GiaPhong"]);
                info.SoLuongSinhVien = Convert.ToInt32(dt.Rows[0]["SoLuongSinhVien"]);
            }
            info.DanhSachSinhVien = chiPhiPhongDAL.SinhVienTrongPhong(maPhong);

            return info;
        }

        public bool ThemChiPhiPhong(ChiPhiPhongDTO cpp)
        {
            if (chiPhiPhongDAL.KiemTraTonTai(cpp.MaPhong, cpp.Thang, cpp.Nam))
            {
                return false;
            }
            return chiPhiPhongDAL.ThemChiPhiPhong(cpp);
        }

        public bool SuaChiPhiPhong(ChiPhiPhongDTO cpp)
        {
            return chiPhiPhongDAL.SuaChiPhiPhong(cpp);
        }

        public bool XoaChiPhiPhong(string maPhong, int thang, int nam)
        {
            return chiPhiPhongDAL.XoaChiPhiPhong(maPhong, thang, nam);
        }
    }
}
