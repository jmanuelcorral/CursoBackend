using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Core.TOOLS;
using School.Domain.Models.DTOs;
using School.TestingTools;
using School.Domain.Models.Entities;

namespace FrameworkTests
{
    [TestClass]
    public class TestMAP : TestBase
    {

        [TestMethod]
        public void TestMappings()
        {
            var stud = POCOFactory.GenerateSingleStudent();
            //AutoMapper.Mapper.CreateMap<Student, StudentDTO>();
            //var dto = AutoMapper.Mapper.Map<Student, StudentDTO>(stud);
            var mapper = new Mapping();
            var dto  = mapper.MapEntity<StudentDTO, Student>(stud);
            Assert.IsNotNull(dto);
        }
    }

}
