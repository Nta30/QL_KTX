using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX.DAL
{
    internal class DataProcesser
    {
        string connectionString = "Data Source=DESKTOP-HQ9QGMG\\SQLEXPRESS;Initial Catalog=QL_KTX;Integrated Security=True";
        SqlConnection sqlConnect = null;

        private void ConnectDatabase()
        {
            if (sqlConnect == null)
            {
                sqlConnect = new SqlConnection(connectionString);
            }
            if (sqlConnect.State != ConnectionState.Open)
            {
                sqlConnect.Open();
            }
        }

        private void DisconnectDatabase()
        {
            if(sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
            }
            sqlConnect.Dispose();
            sqlConnect = null;
        }

        public DataTable ReadData(string sql)
        {
            DataTable data = new DataTable();
            ConnectDatabase();

            // Create adapter to fill dataTable
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnect);
            adapter.Fill(data);

            // Close connection
            DisconnectDatabase();
            adapter.Dispose();

            return data;
        }

        public void UpdateData(string sql)
        {
            ConnectDatabase();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnect;
            command.CommandText = sql;
            command.ExecuteNonQuery();

            DisconnectDatabase();
            command.Dispose();
        }
    }
}
