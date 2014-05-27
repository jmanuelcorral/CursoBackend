using System.Configuration;
using School.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using School.Domain.Models.Entities;
using School.Domain.Models.DTOs;

namespace School.Core.TOOLS
{
    public class Mapping
    {
        public void Mapper()
        {
                      

        }

        public T MapEntity<T,M>(M EntityToMap) 
        {
            AutoMapper.Mapper.CreateMap<Student, StudentDTO>();  
            return AutoMapper.Mapper.Map<M,T>(EntityToMap);
        }
    }
}
