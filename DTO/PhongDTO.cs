using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DTO
{
    internal class PhongDTO
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string AnhPhong { get; set; }
        public string MaLoaiPhong { get; set; }
        public string MaToa { get; set; }
        public string TenLoaiPhong { get; set; }
        public string TenToa { get; set; }
        public decimal GiaPhong { get; set; }
        public int SoNguoiDangO { get; set; }
        public int SoNguoiToiDa { get; set; }
    }
}
