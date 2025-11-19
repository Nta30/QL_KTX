using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class QueQuanDAL
    {
        DataProcesser data = new DataProcesser();
        public DataTable TatCaQue()
        {
            string sql = "Select * from QueQuan";
            return data.ReadData(sql);
        }

        public DataTable TimTheoMaQue(string maQue)
        {
            string sql = "Select * from QueQuan where MaQue = '" + maQue + "'";
            return data.ReadData(sql);
        }
    }
}
