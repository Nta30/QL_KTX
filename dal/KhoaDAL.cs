using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class KhoaDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaKhoa()
        {
            string sql = "Select * from Khoa";
            return data.ReadData(sql);
        }

        public DataTable TimTheoMaKhoa(string maKhoa)
        {
            string sql = "Select * from Khoa where MaKhoa = '" + maKhoa + "'";
            return data.ReadData(sql);
        }
    }
}
