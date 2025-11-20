using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class TraPhongDAL
    {
        DataProcesser data = new DataProcesser();
        // Trong file TraPhongDAL.cs
        public DataTable LichSuTraPhong(string maSinhVien, DateTime? ngayTraPhong, string trangThaiCoc)
        {
            string sql = $@"
                    SELECT 
                        TP.MaTraPhong,
                        PDK.MaPhieuDangKy, 
                        SV.MaSinhVien,
                        SV.HoTen, 
                        P.TenPhong,
                        T.TenToa,
                        TP.NgayTraPhong,
                        case when TP.TrangThaiCoc=1 then N'Đã trả' else N'Chưa trả' end as TrangThaiCoc
                    FROM 
                    PhieuDangKy AS PDK
                    JOIN TraPhong AS TP ON PDK.MaPhieuDangKy = TP.MaPhieuDangKy
                    JOIN SinhVien AS SV ON PDK.MaSinhVien = SV.MaSinhVien
                    JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                    JOIN Toa AS T ON P.MaToa = T.MaToa
                    WHERE 1=1 ";
            if (ngayTraPhong.HasValue)
            {
                sql += $" AND TP.NgayTraPhong = '{ngayTraPhong.Value.ToString("yyyy-MM-dd")}' ";
            }
            if (!string.IsNullOrEmpty(maSinhVien))
            {
                sql += $" AND SV.MaSinhVien LIKE N'%{maSinhVien}%' ";
            }
            if (!string.IsNullOrEmpty(trangThaiCoc))
            {
                sql += $" AND TP.TrangThaiCoc = N'{trangThaiCoc}' ";
            }

            return data.ReadData(sql);
        }

        public DataTable ChiTietTraPhong(string maPhieuDangKy)
        {
            string sql = $"SELECT * FROM TraPhong WHERE MaPhieuDangKy = '{maPhieuDangKy}'";
            return data.ReadData(sql);
        }

        public bool ThemTraPhong(TraPhongDTO tp)
        {
            int ttcValue = tp.TrangThaiCoc.Equals("Đã trả", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            string sql = $@"
                INSERT INTO TraPhong (MaTraPhong, MaPhieuDangKy, NgayTraPhong, TrangThaiCoc)
                VALUES (
                    '{tp.MaTraPhong}', 
                    '{tp.MaPhieuDangKy}', 
                    '{tp.NgayTraPhong:yyyy-MM-dd}', 
                    {ttcValue}
                )
            ";
            return data.WriteData(sql);
        }

        public bool SuaTraPhong(TraPhongDTO tp)
        {
            int ttcValue = tp.TrangThaiCoc.Equals("Đã trả", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            string sql = $@"
                UPDATE TraPhong
                SET 
                    NgayTraPhong = '{tp.NgayTraPhong:yyyy-MM-dd}', 
                    TrangThaiCoc = {ttcValue}
                WHERE MaPhieuDangKy = '{tp.MaPhieuDangKy}'
            ";
            return data.WriteData(sql);
        }
        public bool XoaTraPhong(string maPhieuDangKy)
        {
            string sql = $"DELETE FROM TraPhong WHERE MaPhieuDangKy = '{maPhieuDangKy}'";
            return data.WriteData(sql);
        }


    }
}
