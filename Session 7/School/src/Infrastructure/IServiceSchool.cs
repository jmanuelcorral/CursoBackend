using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using School.Domain.Models.DTOs;

namespace School.Infrastructure
{

    [ServiceContract]
    public interface IServiceSchool
    {
        [OperationContract]
        StudentDTO GetStudent(int id);
    }

  
}
