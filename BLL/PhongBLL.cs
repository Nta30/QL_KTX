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
    internal class PhongBLL
    {
        PhongDAL phongDAL = new PhongDAL();
        public DataTable TatCaToa() => phongDAL.TatCaToa();
        public DataTable TatCaLoaiPhong() => phongDAL.TatCaLoaiPhong();

        public DataTable TimKiem(string tenToa, string tenPhong, string maLoaiPhong)
        {
            return phongDAL.TimKiemPhong(tenToa, tenPhong, maLoaiPhong);
        }

        public DataTable LayDSSinhVien(string maPhong)
        {
            return phongDAL.LayDSSinhVienTrongPhong(maPhong);
        }

        public bool ThemPhong(PhongDTO p)
        {
            return phongDAL.ThemPhong(p);
        }

        public bool SuaPhong(PhongDTO p)
        {
            return phongDAL.SuaPhong(p);
        }

        public bool XoaPhong(string maPhong)
        {
            return phongDAL.XoaPhong(maPhong);
        }

        public bool KiemTraRangBuoc(string maPhong)
        {
            return phongDAL.KiemTraXoa(maPhong);
        }
    }
}
