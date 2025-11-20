using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX.Utils
{
    internal class Functions
    {
        public void FillCombox(ComboBox cb, DataTable dt, string display, string value)
        {
            if (dt.Rows.Count == 0) return;
            if (dt.Rows[0][display].ToString() != "")
            {
                DataRow row = dt.NewRow();
                row[display] = "";
                row[value] = "";
                dt.Rows.InsertAt(row, 0);
            }

            cb.DataSource = dt;
            cb.DisplayMember = display;
            cb.ValueMember = value;
        }
    }
}
