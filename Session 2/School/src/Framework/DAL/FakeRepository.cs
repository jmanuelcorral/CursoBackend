using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DAL
{
    public class FakeRepository : IRepository 
    {
        private Hashtable Storage { get; set; }

        public FakeRepository()
        {
            Storage = new Hashtable();
        }

        public T GetById<T>(object primaryKey) 
        {
            return (T)Storage[primaryKey];
        }

        public IEnumerable<T> Query<T>()  
        {
            return (IEnumerable<T>)Storage;
        }

        public Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            throw new NotImplementedException();
        }

        public int Insert(object itemToAdd)
        {
            PropertyInfo pId = itemToAdd.GetType().GetProperty("Id");
            if (pId != null)
            {
                Storage.Add(pId.GetValue(itemToAdd), itemToAdd);
                return Storage.Count;
            }
            else return 0;
        }

        public int Update(object itemToUpdate, object primaryKeyValue)
        {
            Storage[primaryKeyValue]=itemToUpdate;
            return Storage.Count;
        }

        public int Delete<T>(object primaryKeyValue) 
        {
            Storage.Remove(primaryKeyValue);
            return Storage.Count;
        }
    }
}
