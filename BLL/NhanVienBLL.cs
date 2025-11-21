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
    internal class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        QueQuanDAL queQuanDAL = new QueQuanDAL();
        ChucVuDAL chucVuDAL = new ChucVuDAL();
        public DataTable TatCaChucVu()
        {
            return chucVuDAL.TatCaChucVu();
        }

        public DataTable TatCaQue()
        {
            return queQuanDAL.TatCaQue();
        }
        public DataTable TimKiem(string maNhanVien, string maChucVu, string maQue)
        {
            return nhanVienDAL.TimKiemNhanVien(maNhanVien, maChucVu, maQue);
        }

        public NhanVienDTO ChiTietNhanVien(string maNhanVien)
        {
            DataTable data = nhanVienDAL.ChiTietNhanVien(maNhanVien);
            if (data.Rows.Count == 0) return null;
            DataRow row = data.Rows[0];

            NhanVienDTO nv = new NhanVienDTO
            {
                MaNhanVien = row["MaNhanVien"].ToString(),
                HoTen = row["HoTen"].ToString(),
                CCCD = row["CCCD"].ToString(),
                SoDienThoai = row["SoDienThoai"].ToString(),
                Luong = Convert.ToDecimal(row["Luong"]),
                AnhNhanVien = row["AnhNhanVien"].ToString(),
                MaQue = row["MaQue"].ToString(),
                MaChucVu = row["MaChucVu"].ToString(),
                TenQue = row["TenQue"].ToString(),
                TenChucVu = row["TenChucVu"].ToString()
            };
            return nv;
        }
        public bool ThemNhanVien(NhanVienDTO nv)
        {
            return nhanVienDAL.ThemNhanVien(nv);
        }

        public bool SuaNhanVien(NhanVienDTO nv)
        {
            return nhanVienDAL.SuaNhanVien(nv);
        }

        public bool XoaNhanVien(string maNhanVien)
        {
            return nhanVienDAL.XoaNhanVien(maNhanVien);
        }

        public bool KiemTraRangBuoc(string maNhanVien)
        {
            return nhanVienDAL.KiemTraRangBuoc(maNhanVien);
        }
    }
}
