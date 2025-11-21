using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DTO
{
    public class LoaiPhongDTO
    {
        public string MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal GiaPhong { get; set; }
        public int SoNguoiToiDa { get; set; }
        public string GhiChu { get; set; }
    }
    public class LoaiPhongThietBiDTO
    {
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
