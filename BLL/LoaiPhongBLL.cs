using QL_KTX.DAL;
using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX.BLL
{
    internal class LoaiPhongBLL
    {
        LoaiPhongDAL dal = new LoaiPhongDAL();

        public DataTable TimKiem(string ten, string gia, string soNguoi) => dal.TimKiem(ten, gia, soNguoi);
        public DataTable TatCaThietBi() => dal.TatCaThietBi();

        public List<LoaiPhongThietBiDTO> LayThietBi(string maLoaiPhong) => dal.LayThietBiCuaLoaiPhong(maLoaiPhong);

        public bool ThemLoaiPhong(LoaiPhongDTO lp, List<LoaiPhongThietBiDTO> listTB)
        {
            return dal.ThemLoaiPhong(lp, listTB);
        }

        public bool SuaLoaiPhong(LoaiPhongDTO lp, List<LoaiPhongThietBiDTO> listTB)
        {
            return dal.SuaLoaiPhong(lp, listTB);
        }

        public bool XoaLoaiPhong(string maLoaiPhong)
        {
            return dal.XoaLoaiPhong(maLoaiPhong);
        }

        public bool KiemTraRangBuoc(string maLoaiPhong)
        {
            return dal.KiemTraRangBuoc(maLoaiPhong);
        }
    }
}
