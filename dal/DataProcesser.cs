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

        public bool WriteData(string sql)
        {
            int recordsAffected = 0;
            ConnectDatabase();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnect;
                command.CommandText = sql;
                recordsAffected = command.ExecuteNonQuery();

                command.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thực thi lệnh SQL: " + ex.Message);
            }
            finally
            {
                DisconnectDatabase();
            }

            return recordsAffected > 0;
        }
    }
}
