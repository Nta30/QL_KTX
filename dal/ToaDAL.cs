using QL_KTX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ToaDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaToa()
        {
            string sql = "Select * from Toa";
            return data.ReadData(sql);
        }

        public DataTable TimKiemToa(string tenToa, string soTang)
        {
            string sql = @"
                SELECT 
                    T.MaToa, 
                    T.TenToa, 
                    T.SoTang, 
                    T.SoPhongToiDa,
                    (
                        SELECT COUNT(DISTINCT PDK.MaPhong) 
                        FROM PhieuDangKy PDK
                        JOIN Phong P ON PDK.MaPhong = P.MaPhong
                        WHERE P.MaToa = T.MaToa
                        AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)
                    ) AS SoPhongHienTai
                FROM Toa T
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(tenToa))
                sql += $" AND T.TenToa LIKE N'%{tenToa}%' ";

            if (!string.IsNullOrEmpty(soTang))
                sql += $" AND T.SoTang = {soTang} ";

            return data.ReadData(sql);
        }

        public bool ThemToa(ToaDTO toa)
        {
            string sql = string.Format("INSERT INTO Toa (MaToa, TenToa, SoTang, SoPhongToiDa) VALUES ('{0}', N'{1}', {2}, {3})",
                toa.MaToa, toa.TenToa, toa.SoTang, toa.SoPhongToiDa);
            return data.WriteData(sql);
        }

        public bool SuaToa(ToaDTO toa)
        {
            string sql = string.Format("UPDATE Toa SET TenToa = N'{1}', SoTang = {2}, SoPhongToiDa = {3} WHERE MaToa = '{0}'",
                toa.MaToa, toa.TenToa, toa.SoTang, toa.SoPhongToiDa);
            return data.WriteData(sql);
        }

        public bool KiemTraXoa(string maToa)
        {
            string sql = $"SELECT 1 FROM Phong WHERE MaToa = '{maToa}'";
            DataTable dt = data.ReadData(sql);
            return dt.Rows.Count > 0;
        }

        public bool XoaToa(string maToa)
        {
            if (KiemTraXoa(maToa)) return false;
            string sql = $"DELETE FROM Toa WHERE MaToa = '{maToa}'";
            return data.WriteData(sql);
        }

        public DataTable LayDsPhongTheoToa(string maToa)
        {
            string sql = $@"
                SELECT 
                    P.MaPhong, 
                    P.TenPhong, 
                    LP.TenLoaiPhong,
                    LP.GiaPhong,
                    (SELECT COUNT(*) FROM PhieuDangKy PDK 
                     WHERE PDK.MaPhong = P.MaPhong 
                     AND NOT EXISTS (SELECT 1 FROM TraPhong TP WHERE TP.MaPhieuDangKy = PDK.MaPhieuDangKy)) AS SoNguoiDangO
                FROM Phong P
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE P.MaToa = '{maToa}'";
            return data.ReadData(sql);
        }
    }
}
