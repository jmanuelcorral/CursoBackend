using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using School.Core.DAL;

namespace School.Core.DAL
{
    public class GenericRepository : IRepository
    {
        private Database db;
        private StandardMapper mp;

        public GenericRepository()
        {
            db = new Database("ConnectionString");
            mp = new StandardMapper();
        }
        
        public T GetById<T>(object primaryKey)
        {
            return db.Single<T>(primaryKey);
        }

        public IEnumerable<T> Query<T>()
        {
            var pd = mp.GetTableInfo(typeof(T));
            var sql = "SELECT * FROM " + pd.TableName;
            return db.Query<T>(sql);
        }

        public List<T> Fetch<T>()
        {
            var pd = mp.GetTableInfo(typeof(T));
            var sql = "SELECT * FROM " + pd.TableName;
            return db.Fetch<T>(sql);
        }
        public List<TPassType> Fetch<TPassType>(string sql, params object[] args)
        {
            return db.Fetch<TPassType>(sql, args);
        }

        public Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            return db.Page<T>(pageNumber, itemsPerPage, sql, args) as Page<T>;
        }

        public int Insert(object itemToAdd)
        {
            return Convert.ToInt32(db.Insert(itemToAdd));
        }

        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return Convert.ToInt32(db.Insert(tableName, primaryKeyName, autoIncrement, poco));
        }
        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return Convert.ToInt32(db.Insert(tableName, primaryKeyName, poco));
        }

        public int Update(object poco)
        {
            return db.Update(poco);
        }

        public int Update(object poco, object primaryKeyValue)
        {
            return db.Update(poco, primaryKeyValue);
        }

        public int Update(object poco, IEnumerable<string> columns)
        {
            return db.Update(poco, columns);
        }

        public int Delete<T>(object pocoOrPrimaryKey)
        {
            return db.Delete<T>(pocoOrPrimaryKey);
        }

    }
}
