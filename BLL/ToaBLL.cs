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
    internal class ToaBLL
    {
        ToaDAL toaDAL = new ToaDAL();
        public DataTable TimKiem(string tenToa, string soTang)
        {
            return toaDAL.TimKiemToa(tenToa, soTang);
        }

        public DataTable LayDsPhong(string maToa)
        {
            return toaDAL.LayDsPhongTheoToa(maToa);
        }

        public bool ThemToa(ToaDTO toa)
        {
            return toaDAL.ThemToa(toa);
        }

        public bool SuaToa(ToaDTO toa)
        {
            return toaDAL.SuaToa(toa);
        }

        public bool XoaToa(string maToa)
        {
            return toaDAL.XoaToa(maToa);
        }

        public bool KiemTraRangBuoc(string maToa)
        {
            return toaDAL.KiemTraXoa(maToa);
        }
    }
}
