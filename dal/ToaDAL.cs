using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class ToaDAL
    {
        DataProcesser data = new DataProcesser();

        public DataTable TatCaToa()
        {
            string sql = "Select * from Toa";
            return data.ReadData(sql);
        }
    }
}
