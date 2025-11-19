using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class DienSinhVienDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaDienSinhVien()
        {
            string sql = "Select * from DienSinhVien";
            return data.ReadData(sql);
        }

        public DataTable TimTheoMaDien(string maDien)
        {
            string sql = "Select * from DienSinhVien where MaDienSinhVien = '" + maDien + "'";
            return data.ReadData(sql);
        }
    }
}
