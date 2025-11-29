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
    internal class ThietBiBLL
    {
        ThietBiDAL dal = new ThietBiDAL();

        public DataTable TatCaLoaiPhong() => dal.TatCaLoaiPhong();

        public DataTable TimKiem(string maLP, string tenTB) => dal.TimKiemThietBi(maLP, tenTB);

        public DataTable LayDsPhong(string maTB) => dal.LayDsPhongCoThietBi(maTB);

        public bool ThemThietBi(ThietBiDTO tb)
        {
            return dal.ThemThietBi(tb);
        }

        public bool SuaThietBi(ThietBiDTO tb) => dal.SuaThietBi(tb);

        public bool XoaThietBi(string maTB) => dal.XoaThietBi(maTB);

        public bool KiemTraRangBuoc(string maTB) => dal.KiemTraXoa(maTB);
    }
}
