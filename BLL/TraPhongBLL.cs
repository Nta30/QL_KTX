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
    internal class TraPhongBLL
    {
        TraPhongDAL traPhongDAL = new TraPhongDAL();
        PhieuDangKyDAL phieuDangKyDAL = new PhieuDangKyDAL();
        public DataTable LichSuTraPhong(string maSinhVien, DateTime? ngayTraPhong, string trangThaiCoc)
        {
            return traPhongDAL.LichSuTraPhong(maSinhVien, ngayTraPhong , trangThaiCoc);
        }

        public PhieuDangKyDTO ChiTietPhieuDangKy(string maPhieu)
        {
            DataTable data = phieuDangKyDAL.ChiTietPhieuDangKy(maPhieu);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            return new PhieuDangKyDTO
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
        }

        public TraPhongDTO ChiTietTraPhong(string maPhieuDangKy)
        {
            DataTable data = traPhongDAL.ChiTietTraPhong(maPhieuDangKy);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            return new TraPhongDTO
            {
                MaTraPhong = row["MaTraPhong"].ToString(),
                MaPhieuDangKy = row["MaPhieuDangKy"].ToString(),
                NgayTraPhong = Convert.ToDateTime(row["NgayTraPhong"]),
                TrangThaiCoc = Convert.ToInt32(row["TrangThaiCoc"]) == 1 ? "Đã trả" : "Trừ phí"
            };
        }

        public TraPhongChiTietDTO ChiTietDayDuTraPhong(string maPhieu)
        {
            PhieuDangKyDTO p = ChiTietPhieuDangKy(maPhieu);
            TraPhongDTO tp = ChiTietTraPhong(maPhieu);

            if (p == null || tp == null) return null;

            return new TraPhongChiTietDTO
            {
                MaTraPhong = tp.MaTraPhong,
                NgayTraPhong = tp.NgayTraPhong,
                TrangThaiCoc = tp.TrangThaiCoc,
                MaPhieuDangKy = p.MaPhieuDangKy,
                MaSinhVien = p.MaSinhVien,
                HoTen = p.HoTen,
                NgaySinh = p.NgaySinh,
                GioiTinh = p.GioiTinh,
                TenKhoa = p.TenKhoa,
                TenLop = p.TenLop,
                TenPhong = p.TenPhong,
                TenToa = p.TenToa,
                ThoiGianDangKy = p.ThoiGianDangKy,
                HocKy = p.HocKy,
                NamHoc = p.NamHoc,
                ThoiHan = p.ThoiHan,
                TienCoc = p.TienCoc
            };
        }

        public bool DaTraPhong(string maPhieu)
        {
            return traPhongDAL.ChiTietTraPhong(maPhieu).Rows.Count > 0;
        }

        public bool ThemTraPhong(TraPhongDTO tp)
        {
            return traPhongDAL.ThemTraPhong(tp);
        }

        public bool SuaTraPhong(TraPhongDTO tp)
        {
            return traPhongDAL.SuaTraPhong(tp);
        }

        public bool XoaTraPhong(string maPhieu)
        {
            return traPhongDAL.XoaTraPhong(maPhieu);
        }

        public PhieuDangKyDTO ChiTietPhieuDangKyDangO(string maSinhVien)
        {
            DataTable data = phieuDangKyDAL.ChiTietPhieuDangKyDangO(maSinhVien);
            if (data == null || data.Rows.Count == 0) return null;

            DataRow row = data.Rows[0];
            return new PhieuDangKyDTO
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
        }
    }
}
