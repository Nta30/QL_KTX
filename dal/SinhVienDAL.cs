using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class SinhVienDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaSinhVien()
        {
            string sql = "Select * from SinhVien";
            return data.ReadData(sql);
        }

        public DataTable TimTheoMaSV(string maSV)
        {
            string sql = @"
                SELECT 
                    SV.MaSinhVien,
                    SV.HoTen,
                    SV.NgaySinh,
                    case when SV.GioiTinh=1 then N'Nam' else N'Nữ' end as GioiTinh,
                    SV.SoDienThoai,
                    SV.Email,
                    SV.AnhSinhVien,
                    Q.TenQue,
                    DSV.TenDienSinhVien,
                    L.TenLop,
                    K.TenKhoa,
                    NT.HoTen as HoTenNguoiThan,
	                NT.QuanHe,
	                NT.SoDienThoai as SdtNguoiThan,
                    P.TenPhong,
                    T.TenToa
                FROM 
                    SinhVien AS SV
                LEFT JOIN Lop AS L ON SV.MaLop = L.MaLop
                LEFT JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa
                LEFT JOIN QueQuan AS Q ON SV.MaQue = Q.MaQue
                LEFT JOIN DienSinhVien AS DSV ON SV.MaDienSinhVien = DSV.MaDienSinhVien
                LEFT JOIN NguoiThan AS NT ON SV.MaSinhVien = NT.MaSinhVien
                LEFT JOIN PhieuDangKy AS PDK ON SV.MaSinhVien = PDK.MaSinhVien
                LEFT JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                LEFT JOIN Toa AS T ON P.MaToa = T.MaToa
                WHERE SV.MaSinhVien = '" + maSV + "'";
            return data.ReadData(sql);
        }

        public DataTable TimKiemSinhVien(string maDienSinhVien, string maQue, string maKhoa, string maLop, string maSinhVien)
        {
            string sql = @"
                SELECT 
                    SV.MaSinhVien,
                    SV.HoTen,
                    SV.NgaySinh,
                    case when SV.GioiTinh=1 then N'Nam' else N'Nữ' end as GioiTinh,
                    L.TenLop,
                    K.TenKhoa
                FROM 
                    SinhVien AS SV
                LEFT JOIN Lop AS L ON SV.MaLop = L.MaLop
                LEFT JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa
                WHERE 1=1 ";
            if (!string.IsNullOrEmpty(maDienSinhVien))
            {
                sql += "AND SV.MaDienSinhVien = '" + maDienSinhVien + "' ";
            }
            if (!string.IsNullOrEmpty(maQue))
            {
                sql += "AND SV.MaQue = '" + maQue + "' ";
            }
            if (!string.IsNullOrEmpty(maKhoa))
            {
                sql += "AND K.MaKhoa = '" + maKhoa + "' ";
            }
            if (!string.IsNullOrEmpty(maLop))
            {
                sql += "AND L.MaLop = '" + maLop + "' ";
            }
            if (!string.IsNullOrEmpty(maSinhVien))
            {
                sql += "AND SV.MaSinhVien LIKE '%" + maSinhVien + "%' ";
            }

            return data.ReadData(sql);
        }

        public bool ThemSinhVien(SinhVienDTO sv)
        {
            string sql = string.Format(@"
                INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, AnhSinhVien, MaQue, MaDienSinhVien, MaLop)
                VALUES ('{0}', N'{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')
            ",
                sv.maSinhVien,
                sv.hoTen,
                sv.ngaySinh.ToString("yyyy-MM-dd"),
                (sv.gioiTinh == "Nam" ? 1 : 0),
                sv.soDienThoai,
                sv.email,
                sv.anhSinhVien,
                sv.maQue,
                sv.maDienSinhVien,
                sv.maLop
            );

            return data.WriteData(sql);
        }

        public bool ThemNguoiThan(string maSinhVien, string hoTen, string quanHe, string soDienThoai)
        {
            if (string.IsNullOrEmpty(hoTen))
            {
                return true;
            }
            string sql = string.Format(@"
                INSERT INTO NguoiThan (HoTen, QuanHe, SoDienThoai, MaSinhVien)
                VALUES (N'{0}', N'{1}', '{2}', '{3}')
            ",
                hoTen,
                quanHe,
                soDienThoai,
                maSinhVien
            );
            return data.WriteData(sql);
        }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            string sql = string.Format(@"
                UPDATE SinhVien
                SET 
                    HoTen = N'{1}', 
                    NgaySinh = '{2}', 
                    GioiTinh = {3}, 
                    SoDienThoai = '{4}', 
                    Email = '{5}', 
                    AnhSinhVien = '{6}', 
                    MaQue = '{7}', 
                    MaDienSinhVien = '{8}', 
                    MaLop = '{9}'
                WHERE MaSinhVien = '{0}'
            ",
                sv.maSinhVien,
                sv.hoTen,
                sv.ngaySinh.ToString("yyyy-MM-dd"),
                (sv.gioiTinh == "Nam" ? 1 : 0),
                sv.soDienThoai,
                sv.email,
                sv.anhSinhVien,
                sv.maQue,
                sv.maDienSinhVien,
                sv.maLop
            );

            return data.WriteData(sql);
        }

        public bool CapNhatNguoiThan(string maSinhVien, string hoTen, string quanHe, string soDienThoai)
        {
            if (string.IsNullOrEmpty(hoTen))
            {

                return true;
            }

            string sql;
            sql = string.Format(@"
                    UPDATE NguoiThan
                    SET 
                        HoTen = N'{0}', 
                        QuanHe = N'{1}', 
                        SoDienThoai = '{2}'
                    WHERE MaSinhVien = '{3}'
                ",
                hoTen,
                quanHe,
                soDienThoai,
                maSinhVien
            );

            return data.WriteData(sql);
        }

        public bool XoaNguoiThan(string maSinhVien)
        {
            string sql = "DELETE FROM NguoiThan WHERE MaSinhVien = '" + maSinhVien + "'";
            return data.WriteData(sql);
        }

        public bool KiemTraRangBuoc(string maSinhVien)
        {
            string sqlPhieuDK = $"SELECT 1 FROM PhieuDangKy WHERE MaSinhVien = '{maSinhVien}'";
            DataTable dtPhieuDK = data.ReadData(sqlPhieuDK);

            if (dtPhieuDK.Rows.Count > 0)
            {
                return true;
            }
            string sqlViPham = $"SELECT 1 FROM ViPham WHERE MaSinhVien = '{maSinhVien}'";
            DataTable dtViPham = data.ReadData(sqlViPham);

            if (dtViPham.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool XoaSinhVien(string maSinhVien)
        {
            if (KiemTraRangBuoc(maSinhVien))
            {
                return false;
            }

            XoaNguoiThan(maSinhVien);

            string sql = "DELETE FROM SinhVien WHERE MaSinhVien = '" + maSinhVien + "'";
            bool ketQuaSV = data.WriteData(sql);

            return ketQuaSV;
        }
    

    public DataTable SinhVienTrongPhong(string maPhong)
        {
            string sql = $@"
                SELECT 
                SV.MaSinhVien, SV.HoTen, L.TenLop
                FROM SinhVien AS SV
                JOIN PhieuDangKy AS PDK ON SV.MaSinhVien = PDK.MaSinhVien
                JOIN Lop AS L ON SV.MaLop = L.MaLop
                JOIN Phong AS P ON PDK.MaPhong = P.MaPhong
                WHERE 
                    PDK.MaPhong = '{maPhong}' AND 
                    NOT EXISTS (SELECT 1 FROM TraPhong AS TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)
            ";
            return data.ReadData(sql);
        }
    }
}
