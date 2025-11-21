using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class LoaiPhongDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimKiem(string ten, string gia, string soNguoi)
        {
            string sql = "SELECT * FROM LoaiPhong WHERE 1=1";
            if (!string.IsNullOrEmpty(ten)) sql += $" AND TenLoaiPhong LIKE N'%{ten}%'";
            if (!string.IsNullOrEmpty(gia)) sql += $" AND GiaPhong <= {gia}";
            if (!string.IsNullOrEmpty(soNguoi)) sql += $" AND SoNguoiToiDa = {soNguoi}";
            return data.ReadData(sql);
        }

        public DataTable TatCaThietBi()
        {
            return data.ReadData("SELECT MaThietBi, TenThietBi FROM ThietBi");
        }

        public List<LoaiPhongThietBiDTO> LayThietBiCuaLoaiPhong(string maLoaiPhong)
        {
            string sql = $@"
                SELECT T.MaThietBi, T.TenThietBi, LPTB.SoLuong, LPTB.GhiChu
                FROM LoaiPhong_ThietBi LPTB
                JOIN ThietBi T ON LPTB.MaThietBi = T.MaThietBi
                WHERE LPTB.MaLoaiPhong = '{maLoaiPhong}'";

            DataTable dt = data.ReadData(sql);
            List<LoaiPhongThietBiDTO> list = new List<LoaiPhongThietBiDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new LoaiPhongThietBiDTO
                {
                    MaThietBi = row["MaThietBi"].ToString(),
                    TenThietBi = row["TenThietBi"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    GhiChu = row["GhiChu"].ToString()
                });
            }
            return list;
        }

        public bool ThemLoaiPhong(LoaiPhongDTO lp, List<LoaiPhongThietBiDTO> listThietBi)
        {
            string sqlLP = string.Format(@"INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, GiaPhong, SoNguoiToiDa, GhiChu) 
                                           VALUES ('{0}', N'{1}', {2}, {3}, N'{4}')",
                                           lp.MaLoaiPhong, lp.TenLoaiPhong, lp.GiaPhong, lp.SoNguoiToiDa, lp.GhiChu);
            if (!data.WriteData(sqlLP)) return false;

            return LuuChiTietThietBi(lp.MaLoaiPhong, listThietBi);
        }

        public bool SuaLoaiPhong(LoaiPhongDTO lp, List<LoaiPhongThietBiDTO> listThietBi)
        {
            string sqlLP = string.Format(@"UPDATE LoaiPhong 
                                           SET TenLoaiPhong = N'{1}', GiaPhong = {2}, SoNguoiToiDa = {3}, GhiChu = N'{4}'
                                           WHERE MaLoaiPhong = '{0}'",
                                           lp.MaLoaiPhong, lp.TenLoaiPhong, lp.GiaPhong, lp.SoNguoiToiDa, lp.GhiChu);
            if (!data.WriteData(sqlLP)) return false;

            return LuuChiTietThietBi(lp.MaLoaiPhong, listThietBi);
        }

        private bool LuuChiTietThietBi(string maLoaiPhong, List<LoaiPhongThietBiDTO> listThietBi)
        {
            string sqlDel = $"DELETE FROM LoaiPhong_ThietBi WHERE MaLoaiPhong = '{maLoaiPhong}'";
            data.WriteData(sqlDel);

            foreach (var item in listThietBi)
            {
                string sqlInsert = string.Format(@"INSERT INTO LoaiPhong_ThietBi (MaLoaiPhong, MaThietBi, SoLuong, GhiChu)
                                                   VALUES ('{0}', '{1}', {2}, N'{3}')",
                                                   maLoaiPhong, item.MaThietBi, item.SoLuong, item.GhiChu);
                data.WriteData(sqlInsert);
            }
            return true;
        }

        public bool XoaLoaiPhong(string maLoaiPhong)
        {
            string sqlDelChild = $"DELETE FROM LoaiPhong_ThietBi WHERE MaLoaiPhong = '{maLoaiPhong}'";
            data.WriteData(sqlDelChild);

            string sqlDelParent = $"DELETE FROM LoaiPhong WHERE MaLoaiPhong = '{maLoaiPhong}'";
            return data.WriteData(sqlDelParent);
        }

        public bool KiemTraRangBuoc(string maLoaiPhong)
        {
            string sql = $"SELECT 1 FROM Phong WHERE MaLoaiPhong = '{maLoaiPhong}'";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }
    }

}
