using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class DashboardDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable LayThongKeTongQuat()
        {
            int thang = DateTime.Now.Month;
            int nam = DateTime.Now.Year;

            string sql = $@"
                SELECT 
                    (SELECT COUNT(*) FROM SinhVien) AS TongSinhVien,
                    
                    (SELECT COUNT(*) FROM Phong) AS TongSoPhong,
                    
                    (SELECT COUNT(*) FROM Phong P 
                     WHERE P.MaPhong NOT IN (SELECT DISTINCT MaPhong FROM PhieuDangKy WHERE NOT EXISTS (SELECT 1 FROM TraPhong WHERE MaPhieuDangKy = PhieuDangKy.MaPhieuDangKy))
                    ) AS SoPhongTrong,

                    (SELECT ISNULL(SUM(TienDien + TienNuoc + TienPhong), 0) 
                     FROM ChiPhiPhong 
                     WHERE Thang = {thang} AND Nam = {nam}) AS DoanhThuDienNuoc,

                    (SELECT COUNT(*) FROM ViPham WHERE MONTH(NgayViPham) = {thang} AND YEAR(NgayViPham) = {nam}) AS SoViPhamThang
            ";
            return data.ReadData(sql);
        }
        public DataTable LayDoanhThuTheoNam(int nam)
        {
            string sql = $@"
                SELECT 
                    Thang, 
                    SUM(TienDien + TienNuoc + TienPhong) as TongTien
                FROM ChiPhiPhong
                WHERE Nam = {nam}
                GROUP BY Thang
                ORDER BY Thang ASC
            ";
            return data.ReadData(sql);
        }

        public DataTable LayTyLeLapDayPhong()
        {
            string sql = @"
                SELECT 
                    CASE 
                        WHEN SoNguoiDangO = 0 THEN N'Phòng Trống'
                        WHEN SoNguoiDangO >= LP.SoNguoiToiDa THEN N'Đã Đầy'
                        ELSE N'Còn Chỗ'
                    END AS TrangThai,
                    COUNT(*) as SoLuong
                FROM (
                    SELECT 
                        P.MaPhong, 
                        P.MaLoaiPhong,
                        (SELECT COUNT(*) FROM PhieuDangKy PDK 
                         WHERE PDK.MaPhong = P.MaPhong 
                         AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)
                        ) AS SoNguoiDangO
                    FROM Phong P
                ) AS TinhTrang
                JOIN LoaiPhong LP ON TinhTrang.MaLoaiPhong = LP.MaLoaiPhong
                GROUP BY 
                    CASE 
                        WHEN SoNguoiDangO = 0 THEN N'Phòng Trống'
                        WHEN SoNguoiDangO >= LP.SoNguoiToiDa THEN N'Đã Đầy'
                        ELSE N'Còn Chỗ'
                    END
            ";
            return data.ReadData(sql);
        }

        public DataTable TopPhongTieuThuDien(int thang, int nam)
        {
            string sql = $@"
                SELECT TOP 5
                    T.TenToa,
                    P.TenPhong,
                    CPP.SoDien
                FROM ChiPhiPhong CPP
                JOIN Phong P ON CPP.MaPhong = P.MaPhong
                JOIN Toa T ON P.MaToa = T.MaToa
                WHERE CPP.Thang = {thang} AND CPP.Nam = {nam}
                ORDER BY CPP.SoDien DESC
            ";
            return data.ReadData(sql);
        }
    }
}
