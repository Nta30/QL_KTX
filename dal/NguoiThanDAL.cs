using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class NguoiThanDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimTheoMaSV(string maSV)
        {
            string sql = "Select * from NguoiThan where MaSinhVien = '" + maSV + "'";
            return data.ReadData(sql);
        }
    }
}