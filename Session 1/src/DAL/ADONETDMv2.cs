using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ADONETDMv2
    {
        public String CNNString { get; set; }
        public String ProviderName { get; set; }

        public Student GetStudent(int StudentID)
        {
            Student student = null; //Lazy Load
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                DbConnection conn = factory.CreateConnection();
                conn.ConnectionString = CNNString;
                using (conn)
                {
                    DbParameter StudentIDDbParameter = factory.CreateParameter();
                    StudentIDDbParameter.ParameterName = "@CustomerID";
                    StudentIDDbParameter.DbType = DbType.Int32;
                    StudentIDDbParameter.Value = StudentID;


                    var Command = factory.CreateCommand();
                    Command.Connection = factory.CreateConnection();
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "GetStudent";
                    Command.Parameters.Add(StudentIDDbParameter);

                    using (DbDataReader reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            student = MAPHelperV2.GetAs<Student>(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting customer", e);
            }
            return student;
        }
    }

    public static class MAPHelperV2
    {
        public static T GetAs<T>(DbDataReader reader)
        {
            // Create a new Object
            T newObjectToReturn = Activator.CreateInstance<T>();
            // Get all the properties in our Object
            PropertyInfo[] props = typeof(T).GetProperties();
            // For each property get the data from the reader to the object
            for (int i = 0; i < props.Length; i++)
            {
                if (ColumnExists(reader, props[i].Name) && reader[props[i].Name] != DBNull.Value)
                    typeof(T).InvokeMember(props[i].Name, BindingFlags.SetProperty, null, newObjectToReturn, new Object[] { reader[props[i].Name] });
            }
            return newObjectToReturn;
        }

        /// <summary>
        /// Check if an DbDataReader contains a field
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns></returns>
        public static bool ColumnExists(DbDataReader reader, string columnName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }
    }


}
