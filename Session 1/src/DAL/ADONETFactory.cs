using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ADONETFactory
    {
        public void showCurrentClassStudentList(String StudentClass, String providerName, String connectionString)
        {
            #region ejemplo1

            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            String Query = "SELECT * FROM Students where class=@class";

            using (connection)
            {
                var command = factory.CreateCommand();
                command.CommandText = Query;
                command.Connection = connection;
                var parameter = factory.CreateParameter();
                parameter.ParameterName = "@class";
                parameter.Value = StudentClass;
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    DbDataReader reader = command.ExecuteReader();
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

        public DataTable getAllStudentList(String providerName, String connectionString)
        {
            #region ejemplo2 DataAdapter
            DataTable table = new DataTable();
            try
            {
                // Create the DbProviderFactory and DbConnection.
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;

                using (connection)
                {
                    // Definimos la query
                    string queryString = "SELECT top 1000 * FROM Students";

                    // Creamos el DbCommand.
                    DbCommand command = factory.CreateCommand();
                    command.CommandText = queryString;
                    command.Connection = connection;

                    // Creamos el DbDataAdapter.
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Rellenamos la DataTable.
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
