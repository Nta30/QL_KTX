using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DTO
{
    internal class ChiPhiPhongDTO
    {
        public string MaPhong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal TienDien { get; set; }
        public decimal TienNuoc { get; set; }
        public int SoDien { get; set; }
        public decimal SoNuoc { get; set; }
        public decimal Tien1SoDien { get; set; }
        public decimal Tien1SoNuoc { get; set; }

        public DateTime NgayDong { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string MaNhanVien { get; set; }
        public string TenPhong { get; set; }
        public string TenToa { get; set; }
        public string HoTenNhanVien { get; set; }
        public decimal GiaPhong { get; set; }
        public int SoLuongSinhVien { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TongTien { get; set; }
        public DataTable DanhSachSinhVien { get; set; }
    }
}
