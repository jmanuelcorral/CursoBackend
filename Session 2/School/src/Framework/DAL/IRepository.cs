using System.Collections.Generic;
namespace Framework.DAL
{
    public interface IRepository 
    {
        T GetById<T>(object primaryKey);
        IEnumerable<T> Query<T>();
        Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args);
        int Insert(object itemToAdd);
        int Update(object itemToUpdate, object primaryKeyValue);
        int Delete<T>(object primaryKeyValue);
    }
    
}