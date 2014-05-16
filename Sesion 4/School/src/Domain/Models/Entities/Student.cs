using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
   public class Student:IEntity
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime Registered { get; set; }
        public String Class { get; set; }
        public Int32 Age { get; set; }
    }
   
}
