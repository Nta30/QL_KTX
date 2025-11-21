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
    internal class ChucVuBLL
    {
        ChucVuDAL chucVuDAL = new ChucVuDAL();

        public DataTable TatCaChucVu()
        {
            return chucVuDAL.TatCaChucVu();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return chucVuDAL.TimKiemChucVu(tuKhoa);
        }

        public DataTable NhanVienTheoChucVu(string maChucVu)
        {
            return chucVuDAL.NhanVienTheoChucVu(maChucVu);
        }

        public bool ThemChucVu(ChucVuDTO cv)
        {
            return chucVuDAL.ThemChucVu(cv);
        }

        public bool SuaChucVu(ChucVuDTO cv)
        {
            return chucVuDAL.SuaChucVu(cv);
        }

        public bool XoaChucVu(string maChucVu)
        {
            return chucVuDAL.XoaChucVu(maChucVu);
        }

        public bool KiemTraRangBuoc(string maChucVu)
        {
            return chucVuDAL.KiemTraRangBuoc(maChucVu);
        }
    }
}
