using System;
using System.Collections.Generic;
using School.Core.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.Domain.Models.Entities;
using AutoPoco.DataSources;
using School.Core.DAL;
using  School.TestingTools;

namespace FrameworkTests
{
    [TestClass]
    public class TestDAL:TestBase
    {

        [TestMethod]
        public void RepositoryOneInsertTest()
        {
            var myrepository = DependencyFactory.Resolve<IRepository>();
            var MyStudent = POCOFactory.GenerateSingleStudent();
            var count = myrepository.Insert(MyStudent);
            Assert.IsTrue(count>0);
            

        }



        [TestMethod]
        public void RepositoryDeleteTest()
        {
            var myrepository = DependencyFactory.Resolve<IRepository>();
            var StudentList =POCOFactory.GenerateStudents(100);

            var count = myrepository.Insert(StudentList[0]);
            count = myrepository.Delete<Student>(StudentList[0].Id);
            Assert.IsTrue(count == 0);


        }
    }
}
