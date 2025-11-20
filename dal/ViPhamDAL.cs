using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ViPhamDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimKiem(DateTime? ngayViPham = null, string tenSinhVien = "")
        {
            string sql = @"
                SELECT 
                    VP.MaViPham, 
                    VP.MaSinhVien, 
                    SV.HoTen, 
                    VP.NgayViPham, 
                    VP.TienViPham
                FROM 
                    ViPham AS VP
                LEFT JOIN SinhVien AS SV ON VP.MaSinhVien = SV.MaSinhVien
                WHERE 1=1 ";

            if (ngayViPham.HasValue)
            {
                sql += $" AND VP.NgayViPham = '{ngayViPham.Value.ToString("yyyy-MM-dd")}' ";
            }
            if (!string.IsNullOrEmpty(tenSinhVien))
            {
                sql += $" AND SV.HoTen LIKE N'%{tenSinhVien}%' ";
            }

            return data.ReadData(sql);
        }

        public DataTable ChiTietViPham(string maViPham)
        {
            string sql = $@"
                SELECT 
                    VP.MaViPham, 
                    VP.MaSinhVien, 
                    VP.NgayViPham, 
                    VP.NoiDungViPham, 
                    VP.TienViPham,
                    SV.HoTen,
                    SV.SoDienThoai,
                    SV.Email,
                    SV.AnhSinhVien,
                    CASE WHEN SV.GioiTinh=1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                    P.TenPhong,
                    T.TenToa
                FROM 
                    ViPham AS VP
                JOIN SinhVien AS SV ON VP.MaSinhVien = SV.MaSinhVien
                LEFT JOIN PhieuDangKy AS PDK ON SV.MaSinhVien = PDK.MaSinhVien
                LEFT JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                LEFT JOIN Toa AS T ON P.MaToa = T.MaToa
                WHERE VP.MaViPham = '{maViPham}'
                ORDER BY PDK.ThoiGianDangKy DESC
                ";
            return data.ReadData(sql);
        }
        public bool ThemViPham(ViPhamDTO vp)
        {
            string sql = string.Format(@"
                INSERT INTO ViPham (MaViPham, MaSinhVien, NgayViPham, NoiDungViPham, TienViPham)
                VALUES ('{0}', '{1}', '{2}', N'{3}', {4})
            ",
                vp.maViPham,
                vp.maSinhVien,
                vp.ngayViPham.ToString("yyyy-MM-dd"),
                vp.noiDungViPham,
                vp.tienViPham
            );
            return data.WriteData(sql);
        }

        public bool SuaViPham(ViPhamDTO vp)
        {
            string sql = string.Format(@"
                UPDATE ViPham
                SET 
                    MaSinhVien = '{1}', 
                    NgayViPham = '{2}', 
                    NoiDungViPham = N'{3}', 
                    TienViPham = {4}
                WHERE MaViPham = '{0}'
            ",
                vp.maViPham,
                vp.maSinhVien,
                vp.ngayViPham.ToString("yyyy-MM-dd"),
                vp.noiDungViPham,
                vp.tienViPham
            );
            return data.WriteData(sql);
        }

        public bool XoaViPham(string maViPham)
        {
            string sql = $"DELETE FROM ViPham WHERE MaViPham = '{maViPham}'";
            return data.WriteData(sql);
        }
    }
}
