using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;

namespace DAL
{
    public class ADONETDMv3
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
                            student = MAPHelperV3.GetAs<Student>(reader);
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

    public static class MAPHelperV3
    {
        // Dictionary to store cached properites
        private static IDictionary<string, PropertyInfo[]> propertiesCache = new Dictionary<string, PropertyInfo[]>();
        // Help with locking
        private static ReaderWriterLockSlim propertiesCacheLock = new ReaderWriterLockSlim();
        /// <summary>
        /// Get an array of PropertyInfo for this type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>PropertyInfo[] for this type</returns>
        public static PropertyInfo[] GetCachedProperties<T>()
        {
            PropertyInfo[] props = new PropertyInfo[0];
            if (propertiesCacheLock.TryEnterUpgradeableReadLock(100))
            {
                try
                {
                    if (!propertiesCache.TryGetValue(typeof(T).FullName, out props))
                    {
                        props = typeof(T).GetProperties();
                        if (propertiesCacheLock.TryEnterWriteLock(100))
                        {
                            try
                            {
                                propertiesCache.Add(typeof(T).FullName, props);
                            }
                            finally
                            {
                                propertiesCacheLock.ExitWriteLock();
                            }
                        }
                    }
                }
                finally
                {
                    propertiesCacheLock.ExitUpgradeableReadLock();
                }
                return props;
            }
            else
            {
                return typeof(T).GetProperties();
            }
        }


        public static List<string> GetColumnList(DbDataReader reader)
        {
            List<string> columnList = new List<string>();
            System.Data.DataTable readerSchema = reader.GetSchemaTable();
            for (int i = 0; i < readerSchema.Rows.Count; i++)
                columnList.Add(readerSchema.Rows[i]["ColumnName"].ToString());
            return columnList;
        }

        public static T GetAs<T>(DbDataReader reader)
        {
            T newObjectToReturn = Activator.CreateInstance<T>();
            // Get all the properties in our Object
            PropertyInfo[] props = GetCachedProperties<T>();
            // For each property get the data from the reader to the object
            List<string> columnList = GetColumnList(reader);
            for (int i = 0; i < props.Length; i++)
            {
                if (columnList.Contains(props[i].Name) && reader[props[i].Name] != DBNull.Value)
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