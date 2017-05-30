using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Repository
{
    public partial class IRepository<T> where T : class, new()
    {
        ISugarQueryable<T> Query();
        IDeleteable<T> Insert(T entity);
        int Insert(T entity, Expression<Func<T, object>> express);
        IUpdateable<T> Insert(T entity);
    }

    public partial class IRepository<T> where T : class, new()
    {
        Task<ISugarQueryable<T>> Query();
    }
}
