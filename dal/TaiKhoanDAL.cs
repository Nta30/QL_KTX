using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class TaiKhoanDAL
    {
        DataProcesser data = new DataProcesser();

        public bool CheckLogin(string username, string password)
        {
            DataTable dsTaiKhoan = new DataTable();
            dsTaiKhoan = data.ReadData("Select * from TaiKhoan where TenDangNhap = '" + username + "' and MatKhau = '" + password + "'");
            if (dsTaiKhoan.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable TatCaTaiKhoan()
        {
            string sql = @"
                SELECT TK.MaNhanVien, NV.HoTen, TK.TenDangNhap, TK.MatKhau 
                FROM TaiKhoan TK
                INNER JOIN NhanVien NV ON TK.MaNhanVien = NV.MaNhanVien";
            return data.ReadData(sql);
        }

        public DataTable TimKiemTaiKhoan(string maNhanVien)
        {
            string sql = $@"
                SELECT TK.MaNhanVien, NV.HoTen, TK.TenDangNhap, TK.MatKhau 
                FROM TaiKhoan TK
                INNER JOIN NhanVien NV ON TK.MaNhanVien = NV.MaNhanVien
                WHERE TK.MaNhanVien LIKE '%{maNhanVien}%'";
            return data.ReadData(sql);
        }

        public bool ThemTaiKhoan(TaiKhoanDTO tk)
        {
            string sql = string.Format("INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau) VALUES ('{0}', '{1}', '{2}')",
                tk.maNhanVien, tk.tenDangNhap, tk.matKhau);
            return data.WriteData(sql);
        }

        public bool SuaTaiKhoan(TaiKhoanDTO tk)
        {
            string sql = string.Format("UPDATE TaiKhoan SET MatKhau = '{1}' WHERE MaNhanVien = '{0}'",
                tk.maNhanVien, tk.matKhau);
            return data.WriteData(sql);
        }

        public bool XoaTaiKhoan(string maNhanVien)
        {
            string sql = $"DELETE FROM TaiKhoan WHERE MaNhanVien = '{maNhanVien}'";
            return data.WriteData(sql);
        }

        public bool KiemTraTonTai(string maNhanVien)
        {
            string sql = $"SELECT 1 FROM TaiKhoan WHERE MaNhanVien = '{maNhanVien}'";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }

        public DataTable LayThongTinTaiKhoan(string tenDangNhap)
        {
            string sql = $@"
                SELECT TK.MaNhanVien, NV.HoTen, TK.TenDangNhap, TK.MatKhau, NV.AnhNhanVien
                FROM TaiKhoan TK
                INNER JOIN NhanVien NV ON TK.MaNhanVien = NV.MaNhanVien
                WHERE TK.TenDangNhap = '{tenDangNhap}'";
            return data.ReadData(sql);
        }
    }
}
