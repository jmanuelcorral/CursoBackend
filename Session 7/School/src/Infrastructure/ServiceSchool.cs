using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using School.Core.DAL;
using School.Core.TOOLS;
using School.Domain.Models.DTOs;
using School.Infrastructure;
using School.Domain.Models.Entities;

namespace School.Infrastructure
{
    public class ServiceSchool : IServiceSchool
    {

        FakeRepository repository { get; set; }

        public ServiceSchool()
        {
            repository = new FakeRepository();
            foreach (var stud in TestingTools.POCOFactory.GenerateStudents(100))
            {
                repository.Insert(stud);
            }
            
        }

        public StudentDTO GetStudent(int value)
        {
            Mapping mp = new Mapping();
            return mp.MapEntity<StudentDTO,Student>(repository.GetById<Student>(value));
        }
    }
}
