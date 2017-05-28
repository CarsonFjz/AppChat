using AppChat.ORM;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Repository
{
    public partial interface IBaseRepository<T> where T : class, new()
    {

        List<T> QueryByInAsync(Expression<Func<T,object>> expression);
    }
    public partial class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {

        #region 方法：条件查询 + public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Where"></typeparam>
        /// <typeparam name="FieldType"></typeparam>
        /// <param name="expression"></param>
        /// <param name="inValues"></param>
        /// <returns></returns>
        public List<T> QueryByInAsync(Expression<Func<T,object>> expression)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().In(expression).ToList();
            }
        }
        #endregion
    }
}
