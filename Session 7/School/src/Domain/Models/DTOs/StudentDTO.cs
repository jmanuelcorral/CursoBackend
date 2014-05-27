using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.DTOs
{
    [DataContract]
    public class StudentDTO
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Surname { get; set; }
        [DataMember]
        public DateTime Registered { get; set; }
        [DataMember]
        public String Class { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
    }
}
