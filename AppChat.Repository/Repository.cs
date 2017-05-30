using AppChat.ORM;
using AppChat.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Repository
{

    public partial class Repository<T> : IRepository<T> where T : class, new()
    {
        private SqlSugarClient _dbContext;
        public Repository(SqlSugarClient dbContext)
        {
            _dbContext = dbContext;
        }

        public ISugarQueryable<T> Query()
        {
            return _dbContext.Queryable<T>();
        }

        public async Task<ISugarQueryable<T>> Query()
        {
            await Task.Yield();
            return _dbContext.Queryable<T>();
        }

        public int Insert(T entity, Expression<Func<T, object>> express)
        {
            return _dbContext.Insertable(entity).InsertColumns(express).ExecuteCommand();
        }

        public IDeleteable<T> Insert(T entity)
        {
            return _dbContext.Deleteable<T>();
        }

        public IUpdateable<T> Insert(T entity)
        {
            return _dbContext.Updateable(entity);
        }
    }
}

