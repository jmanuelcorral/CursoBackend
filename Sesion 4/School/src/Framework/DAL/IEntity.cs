using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DAL
{
    public interface IEntity
    {
        Int32 Id { get; set; }
    }
}
