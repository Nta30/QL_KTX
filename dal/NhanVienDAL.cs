using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class NhanVienDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaNhanVien()
        {
            string sql = $"Select * from NhanVien";
            return data.ReadData(sql);
        }

        public DataTable TimKiemNhanVien(string maNhanVien,string maChucVu, string maQue)
        {
            string sql = @"
                SELECT 
                    NV.MaNhanVien,
                    NV.HoTen,
                    NV.CCCD,
                    NV.SoDienThoai,
                    CV.TenChucVu,
                    Q.TenQue,
                    NV.Luong
                FROM 
                    NhanVien AS NV
                LEFT JOIN ChucVu AS CV ON NV.MaChucVu = CV.MaChucVu
                LEFT JOIN QueQuan AS Q ON NV.MaQue = Q.MaQue
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(maNhanVien))
            {
                sql += $"AND NV.MaNhanVien = '{maNhanVien}'";
            }
            if (!string.IsNullOrEmpty(maChucVu))
            {
                sql += "AND NV.MaChucVu = '" + maChucVu + "' ";
            }
            if (!string.IsNullOrEmpty(maQue))
            {
                sql += "AND NV.MaQue = '" + maQue + "' ";
            }

            return data.ReadData(sql);
        }

        public DataTable ChiTietNhanVien(string maNhanVien)
        {
            string sql = @"
                SELECT 
                    NV.*,
                    Q.TenQue,
                    CV.TenChucVu
                FROM 
                    NhanVien AS NV
                LEFT JOIN ChucVu AS CV ON NV.MaChucVu = CV.MaChucVu
                LEFT JOIN QueQuan AS Q ON NV.MaQue = Q.MaQue
                WHERE NV.MaNhanVien = '" + maNhanVien + "'";
            return data.ReadData(sql);
        }

        public bool ThemNhanVien(NhanVienDTO nv)
        {
            string sql = string.Format(@"
                INSERT INTO NhanVien (MaNhanVien, CCCD, HoTen, SoDienThoai, Luong, AnhNhanVien, MaQue, MaChucVu)
                VALUES ('{0}', '{1}', N'{2}', '{3}', {4}, '{5}', '{6}', '{7}')
            ",
                nv.MaNhanVien,
                nv.CCCD,
                nv.HoTen,
                nv.SoDienThoai,
                nv.Luong,
                nv.AnhNhanVien,
                nv.MaQue,
                nv.MaChucVu
            );
            return data.WriteData(sql);
        }

        public bool SuaNhanVien(NhanVienDTO nv)
        {
            string sql = string.Format(@"
                UPDATE NhanVien
                SET 
                    CCCD = '{1}',
                    HoTen = N'{2}', 
                    SoDienThoai = '{3}', 
                    Luong = {4}, 
                    AnhNhanVien = '{5}', 
                    MaQue = '{6}', 
                    MaChucVu = '{7}'
                WHERE MaNhanVien = '{0}'
            ",
                nv.MaNhanVien,
                nv.CCCD,
                nv.HoTen,
                nv.SoDienThoai,
                nv.Luong,
                nv.AnhNhanVien,
                nv.MaQue,
                nv.MaChucVu
            );
            return data.WriteData(sql);
        }

        public bool KiemTraRangBuoc(string maNhanVien)
        {
            // Kiểm tra bảng TaiKhoan
            string sqlTaiKhoan = $"SELECT 1 FROM TaiKhoan WHERE MaNhanVien = '{maNhanVien}'";
            DataTable dtTaiKhoan = data.ReadData(sqlTaiKhoan);
            if (dtTaiKhoan.Rows.Count > 0) return true;
            // Kiểm tra bảng ChiPhiPhong
            string sqlChiPhi = $"SELECT 1 FROM ChiPhiPhong WHERE MaNhanVien = '{maNhanVien}'";
            DataTable dtChiPhi = data.ReadData(sqlChiPhi);
            if (dtChiPhi.Rows.Count > 0) return true;

            return false;
        }

        public bool XoaNhanVien(string maNhanVien)
        {
            if (KiemTraRangBuoc(maNhanVien))
            {
                return false;
            }
            string sql = "DELETE FROM NhanVien WHERE MaNhanVien = '" + maNhanVien + "'";
            return data.WriteData(sql);
        }
    }
}
