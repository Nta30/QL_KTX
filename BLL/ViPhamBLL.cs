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
    internal class ViPhamBLL
    {
        ViPhamDAL viPhamDAL = new ViPhamDAL(); 
        public DataTable TimKiem(DateTime? ngayViPham, string tenSinhVien)
        {
            return viPhamDAL.TimKiem(ngayViPham, tenSinhVien);
        }

        public ViPhamDTO ChiTietViPham(string maViPham)
        {
            DataTable data = viPhamDAL.ChiTietViPham(maViPham);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            ViPhamDTO vp = new ViPhamDTO
            {
                maViPham = row["MaViPham"].ToString(),
                maSinhVien = row["MaSinhVien"].ToString(),
                hoTen = row["HoTen"].ToString(),
                ngayViPham = Convert.ToDateTime(row["NgayViPham"]),
                noiDungViPham = row["NoiDungViPham"].ToString(),
                tienViPham = Convert.ToDecimal(row["TienViPham"]),
                soDienThoai = row["SoDienThoai"].ToString(),
                gioiTinh = row["GioiTinh"].ToString(),
                email = row["Email"].ToString(),
                tenPhong = row["TenPhong"].ToString(),
                tenToa = row["TenToa"].ToString(),
                anhSinhVien = row["AnhSinhVien"].ToString()
            };
            return vp;
        }

        public bool ThemViPham(ViPhamDTO vp)
        {
            return viPhamDAL.ThemViPham(vp);
        }

        public bool SuaViPham(ViPhamDTO vp)
        {
            return viPhamDAL.SuaViPham(vp);
        }

        public bool XoaViPham(string maViPham)
        {
            return viPhamDAL.XoaViPham(maViPham);
        }
    }
}
