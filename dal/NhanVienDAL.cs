using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class NhanVienDAL
    {
        DataProcesser data = new DataProcesser();
        public DataTable TatCaNhanVien()
        {
            string sql = $"Select * from NhanVien";
            return data.ReadData(sql);
        }
    }
}
