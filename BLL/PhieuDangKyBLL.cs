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
    internal class PhieuDangKyBLL
    {
        PhieuDangKyDAL phieuDangKyDAL = new PhieuDangKyDAL();
        ToaDAL toaDAL = new ToaDAL();
        PhongDAL phongDAL = new PhongDAL();

        public DataTable TimKiem(string maSinhVien, DateTime? ngayDangKy, string namHoc)
        {
            return phieuDangKyDAL.TimKiem(maSinhVien, ngayDangKy, namHoc);
        }

        public PhieuDangKyDTO ChiTietPhieuDangKy(string maPhieu)
        {
            DataTable data = phieuDangKyDAL.ChiTietPhieuDangKy(maPhieu);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            PhieuDangKyDTO p = new PhieuDangKyDTO
            {
                MaPhieuDangKy = row["MaPhieuDangKy"].ToString(),
                MaSinhVien = row["MaSinhVien"].ToString(),
                MaPhong = row["MaPhong"].ToString(),
                ThoiGianDangKy = Convert.ToDateTime(row["ThoiGianDangKy"]),
                HocKy = row["HocKy"].ToString(),
                NamHoc = row["NamHoc"].ToString(),
                ThoiHan = Convert.ToInt32(row["ThoiHan"]),
                NgayVaoPhong = Convert.ToDateTime(row["NgayVaoPhong"]),
                TienCoc = Convert.ToDecimal(row["TienCoc"]),
                HoTen = row["HoTen"].ToString(),
                NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                GioiTinh = row["GioiTinh"].ToString(),
                TenKhoa = row["TenKhoa"].ToString(),
                TenLop = row["TenLop"].ToString(),
                TenToa = row["TenToa"].ToString(),
                TenPhong = row["TenPhong"].ToString()
            };
            return p;
        }

        public DataTable TatCaToa()
        {
            return toaDAL.TatCaToa();
        }

        public DataTable TatCaPhong(string maToa)
        {
            return phongDAL.TimTheoToa(maToa);
        }

        public bool SinhVienDuocPhepDangKy(string maSinhVien)
        {
            int soPhieuChuaTraPhong = phieuDangKyDAL.KiemTraSinhVienChuaTraPhong(maSinhVien);
            return soPhieuChuaTraPhong == 0;
        }

        public bool ThemPhieuDangKy(PhieuDangKyDTO p)
        {
            return phieuDangKyDAL.ThemPhieuDangKy(p);
        }

        public bool SuaPhieuDangKy(PhieuDangKyDTO p)
        {
            return phieuDangKyDAL.SuaPhieuDangKy(p);
        }

        public bool XoaPhieuDangKy(string maPhieu)
        {
            return phieuDangKyDAL.XoaPhieuDangKy(maPhieu);
        }

        public DataTable LayDsToaConTrong()
        {
            return phieuDangKyDAL.LayDsToaConTrong();
        }

        public DataTable LayDsPhongConTrong(string maToa)
        {
            return phieuDangKyDAL.LayDsPhongConTrong(maToa);
        }
    }
}
