using System;
using System.Collections.Generic;
using Framework.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoPoco.Engine;
using AutoPoco;
using Domain.Models.Entities;
using AutoPoco.DataSources;

namespace FrameworkTests
{
    [TestClass]
    public class TestDAL:TestBase
    {
        
        private static IList<Student> GenerateData(int numregisters)
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Student>();
                x.Include<Student>()
                   .Setup(c => c.Id).Use<IntegerIdSource>()
                   .Setup(c => c.Name).Use<FirstNameSource>()
                   .Setup(c => c.Surname).Use<LastNameSource>()
                   .Setup(c => c.Registered).Use<DateOfBirthSource>();

            });

            IGenerationSession session = factory.CreateSession();

            IList<Student> students = session.List<Student>(numregisters)
                    .First(50).Impose(x => x.Class, "clase1")
                    .All().Get();
            return students;
        }



        [TestMethod]
        public void RepositoryTest()
        {
            var myrepository = DependencyFactory.Resolve<Framework.DAL.IRepository>();
            var StudentList = GenerateData(10);

            var count = myrepository.Insert(StudentList[0]);
            Assert.IsTrue(count>0);


        }
    }
}
