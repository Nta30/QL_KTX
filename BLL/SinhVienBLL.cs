using QL_KTX.DAL;
using QL_KTX.DTO;
using QL_KTX.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QL_KTX.BLL
{
    internal class SinhVienBLL
    {
        DienSinhVienDAL dienSinhVienDAL = new DienSinhVienDAL();
        QueQuanDAL queQuanDAL = new QueQuanDAL();
        KhoaDAL khoaDAL = new KhoaDAL();
        LopDAL lopDAL = new LopDAL();
        SinhVienDAL sinhVienDAL = new SinhVienDAL();
        NguoiThanDAL nguoiThanDAL = new NguoiThanDAL();

        public DataTable TatCaDienSinhVien()
        {
            return dienSinhVienDAL.TatCaDienSinhVien();
        }

        public DataTable TatCaQue()
        {
            return queQuanDAL.TatCaQue();
        }

        public DataTable TatCaKhoa()
        {
            return khoaDAL.TatCaKhoa();
        }

        public DataTable TatCaLop(string maKhoa)
        {
            return lopDAL.TimTheoMaKhoa(maKhoa);
        }

        public DataTable TimKiem(string maDienSinhVien, string maQue, string maKhoa, string maLop)
        {
            return sinhVienDAL.TimKiemSinhVien(maDienSinhVien, maQue, maKhoa, maLop);
        }

        public SinhVienDTO ChiTietSinhVien(string maSinhVien)
        {
            DataTable data = sinhVienDAL.TimTheoMaSV(maSinhVien);
            DataRow row = data.Rows[0];
            SinhVienDTO sv = new SinhVienDTO
            {
                maSinhVien = row["MaSinhVien"].ToString(),
                hoTen = row["HoTen"].ToString(),
                ngaySinh = Convert.ToDateTime(row["NgaySinh"]),
                gioiTinh = row["GioiTinh"].ToString(),
                soDienThoai = row["SoDienThoai"].ToString(),
                email = row["Email"].ToString(),
                anhSinhVien = row["AnhSinhVien"].ToString(),
                tenQue = row["TenQue"].ToString(),
                tenDienSinhVien = row["TenDienSinhVien"].ToString(),
                tenLop = row["TenLop"].ToString(),
                tenKhoa = row["TenKhoa"].ToString(),
                hoTenNguoiThan = row["HoTenNguoiThan"].ToString(),
                quanHe = row["QuanHe"].ToString(),
                soDienThoaiNguoiThan = row["SdtNguoiThan"].ToString()
            };

            return sv;
        }
    }
}
