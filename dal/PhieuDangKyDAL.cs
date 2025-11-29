using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class PhieuDangKyDAL
    {
        DataProcesser data = new DataProcesser();
        public DataTable TimKiem(string maSinhVien, DateTime? ngayDangKy, string namHoc)
        {
            string sql = $@"
                SELECT 
                    PDK.MaPhieuDangKy, 
                    PDK.MaSinhVien, 
                    SV.HoTen, 
                    P.TenPhong,
                    T.TenToa,
                    PDK.ThoiGianDangKy,
                    PDK.HocKy,
                    PDK.NamHoc
                FROM 
                PhieuDangKy AS PDK
                LEFT JOIN SinhVien AS SV ON PDK.MaSinhVien = SV.MaSinhVien
                LEFT JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                LEFT JOIN Toa AS T ON P.MaToa = T.MaToa
                WHERE 1=1 ";
            if (!string.IsNullOrEmpty(maSinhVien))
            {
                sql += $" AND PDK.MaSinhVien LIKE N'%{maSinhVien}%' ";
            }
            if (ngayDangKy.HasValue)
            {
                sql += $" AND PDK.ThoiGianDangKy = '{ngayDangKy.Value.ToString("yyyy-MM-dd")}' ";
            }
            if (!string.IsNullOrEmpty(namHoc))
            {
                sql += $" AND PDK.NamHoc = {namHoc} ";
            }

            return data.ReadData(sql);
        }

        public DataTable ChiTietPhieuDangKy(string maPhieu)
        {
            string sql = $@"
                SELECT 
                    PDK.*,
                    SV.HoTen, SV.NgaySinh, 
                    CASE WHEN SV.GioiTinh = 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                    K.TenKhoa, L.TenLop, P.TenPhong, T.MaToa, T.TenToa
                FROM 
                PhieuDangKy AS PDK
                JOIN SinhVien AS SV ON PDK.MaSinhVien = SV.MaSinhVien
                JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                JOIN Toa AS T ON P.MaToa = T.MaToa
                JOIN Lop AS L ON SV.MaLop = L.MaLop
                JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa
                WHERE PDK.MaPhieuDangKy = '{maPhieu}'
            ";
            return data.ReadData(sql);
        }

        public int KiemTraSinhVienChuaTraPhong(string maSinhVien)
        {
            string sql = $@"
                SELECT COUNT(PDK.MaPhieuDangKy)
                FROM PhieuDangKy AS PDK
                LEFT JOIN TraPhong AS TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                WHERE PDK.MaSinhVien = '{maSinhVien}' AND TP.MaPhieuDangKy IS NULL
            ";

            DataTable ds = data.ReadData(sql);
            if (data != null && ds.Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Rows[0][0]);
            }
            return 0;
        }

        public bool ThemPhieuDangKy(PhieuDangKyDTO p)
        {
            string sql = $@"
                INSERT INTO PhieuDangKy (MaPhieuDangKy, MaSinhVien, MaPhong, ThoiGianDangKy, HocKy, NamHoc, ThoiHan, NgayVaoPhong, TienCoc)
                VALUES (
                    '{p.MaPhieuDangKy}', 
                    '{p.MaSinhVien}', 
                    '{p.MaPhong}', 
                    '{p.ThoiGianDangKy:yyyy-MM-dd}', 
                    N'{p.HocKy}', 
                    N'{p.NamHoc}', 
                    {p.ThoiHan}, 
                    '{p.NgayVaoPhong:yyyy-MM-dd}', 
                    {p.TienCoc}
                )
            ";
            return data.WriteData(sql);
        }

        public bool SuaPhieuDangKy(PhieuDangKyDTO p)
        {
            string sql = $@"
                UPDATE PhieuDangKy
                SET 
                    MaSinhVien = '{p.MaSinhVien}', 
                    MaPhong = '{p.MaPhong}', 
                    ThoiGianDangKy = '{p.ThoiGianDangKy:yyyy-MM-dd}', 
                    HocKy = N'{p.HocKy}', 
                    NamHoc = N'{p.NamHoc}', 
                    ThoiHan = {p.ThoiHan}, 
                    NgayVaoPhong = '{p.NgayVaoPhong:yyyy-MM-dd}', 
                    TienCoc = {p.TienCoc}
                WHERE MaPhieuDangKy = '{p.MaPhieuDangKy}'
            ";
            return data.WriteData(sql);
        }

        public bool XoaPhieuDangKy(string maPhieu)
        {
            string sql = $"DELETE FROM PhieuDangKy WHERE MaPhieuDangKy = '{maPhieu}'";
            return data.WriteData(sql);
        }

        public DataTable ChiTietPhieuDangKyDangO(string maSinhVien)
        {
            string sql = $@"
                SELECT TOP 1
                    PDK.*,
                    SV.HoTen, SV.NgaySinh, 
                    CASE WHEN SV.GioiTinh = 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                    K.TenKhoa, L.TenLop, P.TenPhong, T.TenToa
                FROM 
                PhieuDangKy AS PDK
                JOIN SinhVien AS SV ON PDK.MaSinhVien = SV.MaSinhVien
                JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                JOIN Toa AS T ON P.MaToa = T.MaToa
                JOIN Lop AS L ON SV.MaLop = L.MaLop
                JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa
                LEFT JOIN TraPhong AS TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                WHERE PDK.MaSinhVien = '{maSinhVien}' AND TP.MaTraPhong IS NULL
                ORDER BY PDK.ThoiGianDangKy DESC
            ";
            return data.ReadData(sql);
        }

        public DataTable LayDsToaConTrong()
        {
            string sql = @"
                SELECT DISTINCT T.MaToa, T.TenToa
                FROM Toa T
                JOIN Phong P ON T.MaToa = P.MaToa
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE (
                    SELECT COUNT(PDK.MaPhieuDangKy)
                    FROM PhieuDangKy PDK
                    LEFT JOIN TraPhong TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                    WHERE PDK.MaPhong = P.MaPhong AND TP.MaTraPhong IS NULL
                ) < LP.SoNguoiToiDa";

            return data.ReadData(sql);
        }

        public DataTable LayDsPhongConTrong(string maToa)
        {
            string sql = $@"
        SELECT P.MaPhong, P.TenPhong
        FROM Phong P
        JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
        WHERE P.MaToa = '{maToa}' 
        AND (
            SELECT COUNT(PDK.MaPhieuDangKy)
            FROM PhieuDangKy PDK
            LEFT JOIN TraPhong TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
            WHERE PDK.MaPhong = P.MaPhong AND TP.MaTraPhong IS NULL
        ) < LP.SoNguoiToiDa";

            return data.ReadData(sql);
        }
    }
}
