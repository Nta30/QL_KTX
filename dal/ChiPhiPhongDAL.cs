using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ChiPhiPhongDAL
    {
        DataProcesser data = new DataProcesser();
        public DataTable TimKiemHoaDon(string maToa, string maPhong, string thang, string nam, string maNhanVien)
        {
            string sql = $@"
                SELECT 
                    CPP.MaPhong, P.TenPhong, T.TenToa, CPP.Thang, CPP.Nam,
                    (CPP.TienDien + CPP.TienNuoc + CPP.TienPhong) AS TongTien,
                    CPP.NgayDong, NV.HoTen AS NguoiTao
                FROM ChiPhiPhong AS CPP
                JOIN Phong AS P ON CPP.MaPhong = P.MaPhong
                JOIN Toa AS T ON P.MaToa = T.MaToa
                LEFT JOIN NhanVien AS NV ON CPP.MaNhanVien = NV.MaNhanVien
                WHERE 1=1 
            ";
            if (!string.IsNullOrEmpty(maToa)) { sql += $" AND T.MaToa = '{maToa}' "; }
            if (!string.IsNullOrEmpty(maPhong)) { sql += $" AND P.MaPhong = '{maPhong}' "; }
            if (!string.IsNullOrEmpty(thang)) { sql += $" AND CPP.Thang = {thang} "; }
            if (!string.IsNullOrEmpty(nam)) { sql += $" AND CPP.Nam = {nam} "; }
            if (!string.IsNullOrEmpty(maNhanVien)) { sql += $" AND (CPP.MaNhanVien LIKE N'%{maNhanVien}%' OR NV.HoTen LIKE N'%{maNhanVien}%') "; }

            return data.ReadData(sql);
        }
        public DataTable ChiTietChiPhiPhong(string maPhong, int thang, int nam)
        {
            string sql = $@"
                SELECT 
                    CPP.*,
                    P.TenPhong, 
                    T.TenToa,
                    NV.HoTen AS HoTenNhanVien
                FROM ChiPhiPhong AS CPP
                JOIN Phong AS P ON CPP.MaPhong = P.MaPhong
                JOIN Toa AS T ON P.MaToa = T.MaToa
                LEFT JOIN NhanVien AS NV ON CPP.MaNhanVien = NV.MaNhanVien
                WHERE CPP.MaPhong = '{maPhong}' AND CPP.Thang = {thang} AND CPP.Nam = {nam}
            ";
            return data.ReadData(sql);
        }

        public DataTable LayThongTinPhongDeTinhTien(string maPhong)
        {
            string sql = $@"
                SELECT 
                    P.MaPhong,
                    LP.GiaPhong,
                    (SELECT COUNT(*) 
                     FROM PhieuDangKy PDK 
                     LEFT JOIN TraPhong TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                     WHERE PDK.MaPhong = P.MaPhong AND TP.MaTraPhong IS NULL
                    ) AS SoLuongSinhVien
                FROM Phong P
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE P.MaPhong = '{maPhong}'
            ";
            return data.ReadData(sql);
        }
        public DataTable SinhVienTrongPhong(string maPhong)
        {
            string sql = $@"
                SELECT SV.MaSinhVien, SV.HoTen, L.TenLop, LP.GiaPhong
                FROM SinhVien AS SV
                JOIN PhieuDangKy AS PDK ON SV.MaSinhVien = PDK.MaSinhVien
                JOIN Lop AS L ON SV.MaLop = L.MaLop
                LEFT JOIN TraPhong AS TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                JOIN LoaiPhong AS LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE PDK.MaPhong = '{maPhong}' AND TP.MaTraPhong IS NULL
            ";
            return data.ReadData(sql);
        }
        public bool KiemTraTonTai(string maPhong, int thang, int nam)
        {
            string sql = $"SELECT COUNT(*) FROM ChiPhiPhong WHERE MaPhong = '{maPhong}' AND Thang = {thang} AND Nam = {nam}";
            DataTable dt = data.ReadData(sql);
            return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public bool ThemChiPhiPhong(ChiPhiPhongDTO cpp)
        {
            string sql = $@"
                INSERT INTO ChiPhiPhong (
                    MaPhong, Thang, Nam, 
                    TienDien, TienNuoc, TienPhong, 
                    SoDien, SoNuoc, Tien1SoDien, Tien1SoNuoc, 
                    NgayDong, NgayHetHan, MaNhanVien
                )
                VALUES (
                    '{cpp.MaPhong}', {cpp.Thang}, {cpp.Nam}, 
                    {cpp.TienDien}, {cpp.TienNuoc}, {cpp.TienPhong}, 
                    {cpp.SoDien}, {cpp.SoNuoc}, 
                    {cpp.Tien1SoDien}, {cpp.Tien1SoNuoc}, 
                    '{cpp.NgayDong:yyyy-MM-dd}', '{cpp.NgayHetHan:yyyy-MM-dd}',
                    '{cpp.MaNhanVien}'
                )
            ";
            return data.WriteData(sql);
        }

        public bool SuaChiPhiPhong(ChiPhiPhongDTO cpp)
        {
            string sql = $@"
                UPDATE ChiPhiPhong
                SET 
                    TienDien = {cpp.TienDien}, 
                    TienNuoc = {cpp.TienNuoc}, 
                    TienPhong = {cpp.TienPhong},
                    SoDien = {cpp.SoDien}, 
                    SoNuoc = {cpp.SoNuoc},
                    Tien1SoDien = {cpp.Tien1SoDien}, 
                    Tien1SoNuoc = {cpp.Tien1SoNuoc},
                    NgayDong = '{cpp.NgayDong:yyyy-MM-dd}',
                    NgayHetHan = '{cpp.NgayHetHan:yyyy-MM-dd}',
                    MaNhanVien = '{cpp.MaNhanVien}'
                WHERE MaPhong = '{cpp.MaPhong}' AND Thang = {cpp.Thang} AND Nam = {cpp.Nam}
            ";
            return data.WriteData(sql);
        }

        public bool XoaChiPhiPhong(string maPhong, int thang, int nam)
        {
            string sql = $"DELETE FROM ChiPhiPhong WHERE MaPhong = '{maPhong}' AND Thang = {thang} AND Nam = {nam}";
            return data.WriteData(sql);
        }

        public DataTable LayDsPhongCoSinhVien(string maToa)
        {
            
            string sql = $@"
                SELECT DISTINCT P.MaPhong, P.TenPhong
                FROM Phong P
                JOIN PhieuDangKy PDK ON P.MaPhong = PDK.MaPhong
                LEFT JOIN TraPhong TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                WHERE P.MaToa = '{maToa}' 
                AND TP.MaTraPhong IS NULL
            ";
            return data.ReadData(sql);
        }
    }

}
