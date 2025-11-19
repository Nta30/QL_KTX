using QL_KTX.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.BLL
{
    internal class TaiKhoanBLL
    {
        TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
        public bool CheckLogin(string username, string password)
        {
            return taiKhoanDAL.CheckLogin(username, password);
        }
    }
}
