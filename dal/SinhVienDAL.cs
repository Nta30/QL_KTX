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
	                NT.SoDienThoai as SdtNguoiThan
                FROM 
                    SinhVien AS SV
                LEFT JOIN Lop AS L ON SV.MaLop = L.MaLop
                LEFT JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa
                LEFT JOIN QueQuan AS Q ON SV.MaQue = Q.MaQue
                LEFT JOIN DienSinhVien AS DSV ON SV.MaDienSinhVien = DSV.MaDienSinhVien
                LEFT JOIN NguoiThan AS NT ON SV.MaSinhVien = NT.MaSinhVien
                WHERE SV.MaSinhVien = '" + maSV + "'";
            return data.ReadData(sql);
        }

        public DataTable TimKiemSinhVien(string maDienSinhVien, string maQue, string maKhoa, string maLop)
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

            return data.ReadData(sql);
        }
    }
}
