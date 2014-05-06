using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class BulkInsert
    {
       public static void InsertData(IList<Student> students)
        {
            DataTable StudentsData = new DataTable("Students");

            // Create Column 1: SaleDate
            DataColumn IdColumn = new DataColumn();
            IdColumn.DataType = Type.GetType("System.Int32");
            IdColumn.ColumnName = "Id";

            // Create Column 2: ProductName
            DataColumn StudentsNameColumn = new DataColumn();
            StudentsNameColumn.DataType = Type.GetType("System.String");
            StudentsNameColumn.ColumnName = "Name";

            // Create Column 3: TotalSales
            DataColumn StudentsSurnameColumn = new DataColumn();
            StudentsSurnameColumn.DataType = Type.GetType("System.String");
            StudentsSurnameColumn.ColumnName = "Surname";

            // Create Column 3: TotalSales
            DataColumn RegisteredColumn = new DataColumn();
            RegisteredColumn.DataType = Type.GetType("System.DateTime");
            RegisteredColumn.ColumnName = "Registered";

            // Create Column 3: TotalSales
            DataColumn ClassColumn = new DataColumn();
            ClassColumn.DataType = Type.GetType("System.String");
            ClassColumn.ColumnName = "Class";

            // Add the columns to the ProductSalesData DataTable
            StudentsData.Columns.Add(IdColumn);
            StudentsData.Columns.Add(StudentsNameColumn);
            StudentsData.Columns.Add(StudentsSurnameColumn);
            StudentsData.Columns.Add(RegisteredColumn);
            StudentsData.Columns.Add(ClassColumn);

            // Let's populate the datatable with our stats.
            // You can add as many rows as you want here!

           foreach (Student student in students)
           {
                DataRow StudentRow = StudentsData.NewRow();
                StudentRow["Id"] = student.Id;
               StudentRow["Name"] = student.Name;
                StudentRow["Surname"] = student.Surname;
                StudentRow["Registered"] = student.Registered;
                StudentRow["Class"] = student.Class;

                // Add the row to the ProductSalesData DataTable
                StudentsData.Rows.Add(StudentRow);
           }
            // Copy the DataTable to SQL Server using SqlBulkCopy
           using (SqlConnection dbConnection = new SqlConnection("Data Source=WIN-HCCP7SMIVL3;Initial Catalog=school;Integrated Security=True"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = StudentsData.TableName;

                    foreach (var column in StudentsData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(StudentsData);
                }
            }
        }
    }
}
