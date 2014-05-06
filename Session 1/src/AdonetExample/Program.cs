using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using AutoPoco.Engine;
using AutoPoco;
using AutoPoco.DataSources;

namespace AdonetExample
{
    class Program
    {

        private static IList<Student> GenerateDBData()
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Student>();
                x.Include<Student>()
                   .Setup(c=>c.Id).Use<IntegerIdSource>()
                   .Setup(c => c.Name).Use<FirstNameSource>()
                   .Setup(c => c.Surname).Use<LastNameSource>()
                   .Setup(c=> c.Registered).Use<DateOfBirthSource>();
                
            });
 
            IGenerationSession session = factory.CreateSession();
 
            IList<Student> students = session.List<Student>(1000000)
                    .First(50).Impose(x=> x.Class, "clase1")
                    .All().Get();
            return students;
        }

        static void Main(string[] args)
        {
            //Insercion Masiva en BBBDD
            // BulkInsert.InsertData(GenerateDBData());
            // Console.ReadKey();

            //Ejemplos
            Console.Write("Primer Ejemplo / Ejercicio de ADONET");

            String ConnectionString = "Data Source=WIN-HCCP7SMIVL3;Initial Catalog=school;Integrated Security=True";
            Stopwatch sw = new Stopwatch();

            //Ejemplo 1 Aplicación de ADONET Begginer
            DAL.ADONETSimple sample1 = new ADONETSimple();
            sw.Start();
            sample1.showCurrentClassStudentList("clase1", ConnectionString);
            sw.Stop();
            Console.WriteLine("Hemos tardado {0} milisegundos", sw.ElapsedMilliseconds);
            sw.Reset();
            Console.ReadKey();
            ///Ejemplo 2 Aplicación de ADONET Begginer
            
            sw.Start();
            DataTable dt = sample1.getAllStudentList(ConnectionString);
            sw.Stop();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(String.Format("Nombre: {0} Apellidos: {1} Clase: {2}", row["name"].ToString(), row["surname"].ToString(), row["class"].ToString()));
            }
            Console.WriteLine("Hemos tardado {0} milisegundos", sw.ElapsedMilliseconds);
            sw.Reset();
            Console.ReadKey();
            
            ////////////////REFACTORING MEJORANDO LAS PRACTICAS QUE CONOCEMOS
            /// Ejemplo 3 Aplicación de ADONET DbFactory

            String Provider = "System.Data.SqlClient";
            DAL.ADONETFactory sample2 = new ADONETFactory();
            sw.Start();
            sample2.showCurrentClassStudentList("clase1", Provider, ConnectionString);
            sw.Stop();
            Console.WriteLine("Hemos tardado {0} milisegundos", sw.ElapsedMilliseconds);
            sw.Reset();
            Console.ReadKey();
            
            ///Ejemplo 4 Aplicación Aplicación de ADONET DbFactory
            
            sw.Start();
            DataTable dt2 = sample2.getAllStudentList(Provider, ConnectionString);
            sw.Stop();
            foreach (DataRow row in dt2.Rows)
            {
                Console.WriteLine(String.Format("Nombre: {0} Apellidos: {1} Clase: {2}", row["name"].ToString(), row["surname"].ToString(), row["class"].ToString()));
            }
            Console.WriteLine("Hemos tardado {0} milisegundos", sw.ElapsedMilliseconds);
            sw.Reset();
            Console.ReadKey();
            

            ////////////////Y si Migramos a un DataMapper?
            /// Ejemplo 5 Primera versión de DataMapper
            
            DAL.ADONETDataMapper sampleMapper = new ADONETDataMapper();
            sampleMapper.CNNString = ConnectionString;
            sampleMapper.ProviderName = Provider;
            sw.Start();
            Student myStudent = sampleMapper.GetStudent(5);
            sw.Stop();
            Console.WriteLine(myStudent.ToLogString());
            Console.WriteLine("Hemos tardado {0} milisegundos", sw.ElapsedMilliseconds);
            sw.Reset();
            Console.ReadKey();
            
        }
    }
}
