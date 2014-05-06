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
    public class ADONETDataMapper
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
                StudentIDDbParameter.ParameterName = "@StudentID";
                StudentIDDbParameter.DbType = DbType.Int32;
                StudentIDDbParameter.Value = StudentID;

                conn.Open();
                    var Command = factory.CreateCommand();
                    Command.Connection = conn;
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "GetStudent";
                    Command.Parameters.Add(StudentIDDbParameter);

                    using (DbDataReader reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            student = (Student) MAPHelper.GetAs(reader, typeof (Student));
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Error getting customer", e);
            }
            return student;
        }
    }

    public static class MAPHelper
    {
        /// <summary>
        /// Return the current row in the reader as an object
        /// </summary>
        /// <param name="reader">The Reader</param>
        /// <param name="objectToReturnType">The type of object to return</param>
        /// <returns>Object</returns>
        public static Object GetAs(DbDataReader reader, Type objectToReturnType)
        {
            // Create a new Object
            Object newObjectToReturn = Activator.CreateInstance(objectToReturnType);
            // Get all the properties in our Object
            PropertyInfo[] props = objectToReturnType.GetProperties();
            // For each property get the data from the reader to the object
            for (int i = 0; i < props.Length; i++)
            {
                if (ColumnExists(reader, props[i].Name) && reader[props[i].Name] != DBNull.Value)
                    objectToReturnType.InvokeMember(props[i].Name, BindingFlags.SetProperty, null, newObjectToReturn, new Object[] { reader[props[i].Name] });
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
    
    public class Student
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime Registered { get; set; }
        public String Class { get; set; }
        public Int32 Age { get; set; }

        public object ToLogString()
        {
            return String.Format("Estudiante Id: {0} Name {1} Surname {2} Registered {3} Class {4} Age {5}", this.Id, this.Name, this.Surname, this.Registered, this.Class, this.Age);
        }
    }
}
