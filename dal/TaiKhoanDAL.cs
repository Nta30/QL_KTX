using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class TaiKhoanDAL
    {
        DataProcesser database = new DataProcesser();

        public bool CheckLogin(string username, string password)
        {
            DataTable data = new DataTable();
            data = database.ReadData("Select * from TaiKhoan where TenDangNhap = '" + username + "' and MatKhau = '" + password + "'");
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
