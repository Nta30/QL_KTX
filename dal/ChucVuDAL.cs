using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ChucVuDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaChucVu()
        {
            string sql = "SELECT * FROM ChucVu";
            return data.ReadData(sql);
        }

        public DataTable TimKiemChucVu(string tuKhoa)
        {
            string sql = $"SELECT * FROM ChucVu WHERE MaChucVu LIKE N'%{tuKhoa}%' OR TenChucVu LIKE N'%{tuKhoa}%'";
            return data.ReadData(sql);
        }

        public bool ThemChucVu(ChucVuDTO cv)
        {
            string sql = string.Format("INSERT INTO ChucVu (MaChucVu, TenChucVu) VALUES ('{0}', N'{1}')", cv.MaChucVu, cv.TenChucVu);
            return data.WriteData(sql);
        }

        public bool SuaChucVu(ChucVuDTO cv)
        {
            string sql = string.Format("UPDATE ChucVu SET TenChucVu = N'{1}' WHERE MaChucVu = '{0}'", cv.MaChucVu, cv.TenChucVu);
            return data.WriteData(sql);
        }

        public bool KiemTraRangBuoc(string maChucVu)
        {
            string sql = $"SELECT 1 FROM NhanVien WHERE MaChucVu = '{maChucVu}'";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }

        public bool XoaChucVu(string maChucVu)
        {
            if (KiemTraRangBuoc(maChucVu)) return false;
            string sql = $"DELETE FROM ChucVu WHERE MaChucVu = '{maChucVu}'";
            return data.WriteData(sql);
        }

        public DataTable NhanVienTheoChucVu(string maChucVu)
        {
            string sql = $@"
                SELECT 
                    NV.MaNhanVien, 
                    NV.HoTen, 
                    NV.SoDienThoai, 
                    NV.Luong 
                FROM NhanVien NV 
                WHERE NV.MaChucVu = '{maChucVu}'";
            return data.ReadData(sql);
        }
    }
}
