using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class LopDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimTheoMaLop(string maLop)
        {
            string sql = "Select * from Lop where MaLop = '" + maLop + "'";
            return data.ReadData(sql);
        }

        public DataTable TimTheoMaKhoa(string maKhoa)
        {
            string sql = "Select * from Lop where MaKhoa = '" + maKhoa + "'";
            return data.ReadData(sql);
        }
    }
}
