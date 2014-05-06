using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ADONETSimple
    {
       
        public ADONETSimple()
        {
            
        }

        public void showCurrentClassStudentList(String StudentClass, String connectionString)
        {
            #region Ejemplo 1 
            SqlConnection connection = new SqlConnection(connectionString);
            String Query = "SELECT * FROM Students where class='" + StudentClass +"'";

            using (connection)
            {
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = Query;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}", reader[0], reader[1], reader[2]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            #endregion
        }


        public DataTable getAllStudentList(String connectionString)
        {
            #region ejemplo2 DataAdapter
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String Query = "SELECT top 1000 * FROM Students";
                    SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(Query, connection));
                    adapter.Fill(table);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //la devolvemos
            return table;

            #endregion
        }
  

   


    }
}
