using QL_KTX.DAL;
using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.BLL
{
    internal class TaiKhoanBLL
    {
        TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
        public bool CheckLogin(string username, string password)
        {
            return taiKhoanDAL.CheckLogin(username, password);
        }

        public DataTable TatCaTaiKhoan()
        {
            return taiKhoanDAL.TatCaTaiKhoan();
        }

        public DataTable TimKiem(string maNhanVien)
        {
            return taiKhoanDAL.TimKiemTaiKhoan(maNhanVien);
        }

        public bool ThemTaiKhoan(TaiKhoanDTO tk)
        {
            if (taiKhoanDAL.KiemTraTonTai(tk.maNhanVien))
            {
                return false;
            }
            return taiKhoanDAL.ThemTaiKhoan(tk);
        }

        public bool SuaTaiKhoan(TaiKhoanDTO tk)
        {
            return taiKhoanDAL.SuaTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(string maNhanVien)
        {
            return taiKhoanDAL.XoaTaiKhoan(maNhanVien);
        }

        public DataTable ThongTinTaiKhoan(string tenDangNhap)
        {
            return taiKhoanDAL.LayThongTinTaiKhoan(tenDangNhap);
        }
    }
}
