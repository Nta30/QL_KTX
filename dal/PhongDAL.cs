using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class PhongDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TimTheoToa(string maToa)
        {
            string sql = $"Select * from Phong where MaToa = '{maToa}'";
            return data.ReadData(sql);
        }
    }
}
