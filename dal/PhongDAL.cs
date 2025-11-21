using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class PhongDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimTheoToa(string maToa)
        {
            string sql = $"Select * from Phong where MaToa = '{maToa}'";
            return data.ReadData(sql);
        }

        public DataTable TatCaToa() => data.ReadData("SELECT MaToa, TenToa FROM Toa");
        public DataTable TatCaLoaiPhong() => data.ReadData("SELECT MaLoaiPhong, TenLoaiPhong FROM LoaiPhong");

        public DataTable TimKiemPhong(string tenToa, string tenPhong, string maLoaiPhong)
        {
            string sql = @"
                SELECT 
                    P.MaPhong, P.TenPhong, P.AnhPhong, 
                    T.TenToa, LP.TenLoaiPhong, LP.GiaPhong, LP.SoNguoiToiDa,
                    (SELECT COUNT(*) FROM PhieuDangKy PDK 
                     WHERE PDK.MaPhong = P.MaPhong 
                     AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)
                    ) AS SoNguoiDangO,
                    P.MaToa, P.MaLoaiPhong
                FROM Phong P
                JOIN Toa T ON P.MaToa = T.MaToa
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(tenToa)) sql += $" AND T.TenToa LIKE N'%{tenToa}%' ";
            if (!string.IsNullOrEmpty(tenPhong)) sql += $" AND P.TenPhong LIKE N'%{tenPhong}%' ";
            if (!string.IsNullOrEmpty(maLoaiPhong)) sql += $" AND P.MaLoaiPhong = '{maLoaiPhong}' ";

            return data.ReadData(sql);
        }

        public DataTable LayDSSinhVienTrongPhong(string maPhong)
        {
            string sql = $@"
                SELECT SV.MaSinhVien, SV.HoTen, L.TenLop, SV.SoDienThoai
                FROM SinhVien SV
                JOIN PhieuDangKy PDK ON SV.MaSinhVien = PDK.MaSinhVien
                JOIN Lop L ON SV.MaLop = L.MaLop
                WHERE PDK.MaPhong = '{maPhong}'
                AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)";
            return data.ReadData(sql);
        }

        public bool ThemPhong(PhongDTO p)
        {
            string sql = string.Format("INSERT INTO Phong (MaPhong, TenPhong, AnhPhong, MaLoaiPhong, MaToa) VALUES ('{0}', N'{1}', '{2}', '{3}', '{4}')",
                p.MaPhong, p.TenPhong, p.AnhPhong, p.MaLoaiPhong, p.MaToa);
            return data.WriteData(sql);
        }

        public bool SuaPhong(PhongDTO p)
        {
            string sql = string.Format("UPDATE Phong SET TenPhong = N'{1}', AnhPhong = '{2}', MaLoaiPhong = '{3}', MaToa = '{4}' WHERE MaPhong = '{0}'",
                p.MaPhong, p.TenPhong, p.AnhPhong, p.MaLoaiPhong, p.MaToa);
            return data.WriteData(sql);
        }

        public bool KiemTraXoa(string maPhong)
        {
            string sql = $@"SELECT 1 FROM PhieuDangKy PDK 
                            WHERE MaPhong = '{maPhong}' 
                            AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }

        public bool XoaPhong(string maPhong)
        {
            if (KiemTraXoa(maPhong)) return false;
            string sql = $"DELETE FROM Phong WHERE MaPhong = '{maPhong}'";
            return data.WriteData(sql);
        }
    }
}
