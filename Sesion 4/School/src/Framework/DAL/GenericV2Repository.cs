using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DAL
{
    public class GenericV2Repository
    {
        private IUnitOfWork uw;
        private StandardMapper mp;

        public GenericV2Repository(IUnitOfWork unitofWork)
        {
            uw = unitofWork;
            mp = new StandardMapper();
        }
        
        public T GetById<T>(object primaryKey)
        {
            return uw.Db.Single<T>(primaryKey);
        }

        public IEnumerable<T> Query<T>()
        {
            var pd = mp.GetTableInfo(typeof(T));
            var sql = "SELECT * FROM " + pd.TableName;
            return uw.Db.Query<T>(sql);
        }

        public List<T> Fetch<T>()
        {
            var pd = mp.GetTableInfo(typeof(T));
            var sql = "SELECT * FROM " + pd.TableName;
            return uw.Db.Fetch<T>(sql);
        }
        public List<TPassType> Fetch<TPassType>(string sql, params object[] args)
        {
            return uw.Db.Fetch<TPassType>(sql, args);
        }

        public Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            return uw.Db.Page<T>(pageNumber, itemsPerPage, sql, args) as Page<T>;
        }

        public int Insert(object itemToAdd)
        {
            return Convert.ToInt32(uw.Db.Insert(itemToAdd));
        }

        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return Convert.ToInt32(uw.Db.Insert(tableName, primaryKeyName, autoIncrement, poco));
        }
        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return Convert.ToInt32(uw.Db.Insert(tableName, primaryKeyName, poco));
        }

        public int Update(object poco)
        {
            return uw.Db.Update(poco);
        }

        public int Update(object poco, object primaryKeyValue)
        {
            return uw.Db.Update(poco, primaryKeyValue);
        }

        public int Update(object poco, IEnumerable<string> columns)
        {
            return uw.Db.Update(poco, columns);
        }

        public int Delete<T>(object pocoOrPrimaryKey)
        {
            return uw.Db.Delete<T>(pocoOrPrimaryKey);
        }

    }
}
