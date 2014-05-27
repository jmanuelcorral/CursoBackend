using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using School.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.TestingTools
{
    public static class POCOFactory
    {
        public static IList<Student> GenerateStudents(int numregisters)
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

        public static Student GenerateSingleStudent()
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

            return session.Single<Student>().Get();
            }
    }
}
