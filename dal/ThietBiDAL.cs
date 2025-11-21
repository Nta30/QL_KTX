using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ThietBiDAL
    {
        DataProcesser data = new DataProcesser();
        public DataTable TatCaLoaiPhong()
        {
            return data.ReadData("SELECT MaLoaiPhong, TenLoaiPhong FROM LoaiPhong");
        }

        public DataTable TimKiemThietBi(string maLoaiPhong, string tenTB)
        {
            string sql = "SELECT * FROM ThietBi WHERE 1=1 ";

            if (!string.IsNullOrEmpty(tenTB))
                sql += $" AND TenThietBi LIKE N'%{tenTB}%'";

            if (!string.IsNullOrEmpty(maLoaiPhong))
            {
                sql += $@" AND MaThietBi IN (
                            SELECT MaThietBi FROM LoaiPhong_ThietBi 
                            WHERE MaLoaiPhong = '{maLoaiPhong}')";
            }

            return data.ReadData(sql);
        }

        public DataTable LayDsPhongCoThietBi(string maThietBi)
        {
            string sql = $@"
                SELECT 
                    P.MaPhong, P.TenPhong, T.TenToa, LP.TenLoaiPhong, 
                    LPTB.SoLuong AS SoLuongThietBi
                FROM Phong P
                JOIN Toa T ON P.MaToa = T.MaToa
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                JOIN LoaiPhong_ThietBi LPTB ON LP.MaLoaiPhong = LPTB.MaLoaiPhong
                WHERE LPTB.MaThietBi = '{maThietBi}'";
            return data.ReadData(sql);
        }

        public string TaoMaMoi()
        {
            string sql = "SELECT TOP 1 MaThietBi FROM ThietBi ORDER BY MaThietBi DESC";
            DataTable dt = data.ReadData(sql);
            if (dt.Rows.Count > 0)
            {
                string maCu = dt.Rows[0]["MaThietBi"].ToString();
                string so = maCu.Substring(2);
                if (int.TryParse(so, out int stt))
                    return "TB" + (stt + 1).ToString("D2");
            }
            return "TB01";
        }

        public bool ThemThietBi(ThietBiDTO tb)
        {
            string sql = string.Format("INSERT INTO ThietBi (MaThietBi, TenThietBi, GiaTien) VALUES ('{0}', N'{1}', {2})",
                tb.MaThietBi, tb.TenThietBi, tb.GiaTien);
            return data.WriteData(sql);
        }

        public bool SuaThietBi(ThietBiDTO tb)
        {
            string sql = string.Format("UPDATE ThietBi SET TenThietBi = N'{1}', GiaTien = {2} WHERE MaThietBi = '{0}'",
                tb.MaThietBi, tb.TenThietBi, tb.GiaTien);
            return data.WriteData(sql);
        }

        public bool KiemTraXoa(string maThietBi)
        {
            string sql = $"SELECT 1 FROM LoaiPhong_ThietBi WHERE MaThietBi = '{maThietBi}'";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }

        public bool XoaThietBi(string maThietBi)
        {
            if (KiemTraXoa(maThietBi)) return false;
            string sql = $"DELETE FROM ThietBi WHERE MaThietBi = '{maThietBi}'";
            return data.WriteData(sql);
        }
    }
}
