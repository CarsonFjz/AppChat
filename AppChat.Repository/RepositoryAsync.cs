using AppChat.ORM;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Repository
{
        #region Interface
    public async partial interface IBaseRepository<T> where T : class, new()
    {
        #region 方法：插入数据 + object InsertAsync(T entity, bool isIdentity = true)
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        Task<object> InsertAsync(T entity, bool isIdentity = true);

        #endregion

        #region 方法：批量插入数据 + List<object> InsertRange(Task<List<T>> entites, bool isIdentity = true)
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entites">实体集合</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        Task<List<object>> InsertRangeAsync(Task<List<T>> entites, bool isIdentity = true);
        #endregion

        #region 方法：更新实体所有的列 + bool Update(T model)
        /// <summary>
        /// 更新实体所有的列
        /// </summary>
        /// <param name="model">实体对象，主键必须是第一位</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T model);
        #endregion

        #region 方法：更新数据 + bool Update<FiledType>(object model, params FiledType[] whereIn)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        Task<bool> UpdateAsync<FiledType>(object model, params FiledType[] whereIn);
        #endregion

        #region 方法：更新数据 + bool Update(object model, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(object model, Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：删除数据 + bool Delete<FiledType>(params FiledType[] whereIn)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        Task<bool> DeleteAsync<FiledType>(params FiledType[] whereIn);
        #endregion

        #region 方法：删除数据 + bool Delete(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：假删除数据 + bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="whereIn">过滤数组</param>
        /// <returns></returns>
        Task<bool> FalseDeleteAsync<FiledType>(string filed, params FiledType[] whereIn);
        #endregion

        #region 方法：假删除数据 + bool FalseDelete(string filed, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        Task<bool> FalseDeleteAsync(string filed, Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：查询一个 + T QuerySingle()
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <returns></returns>
        Task<T> QuerySingleAsync();
        #endregion

        #region 方法：查询一个 + T QuerySingle(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        Task<T> QuerySingleAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：查询所有 + Task<List<T>> QueryAll()
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync();
        #endregion

        #region 方法：查询所有 +  Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字符串</param>
        /// <returns></returns>
        Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：条件查询 + public async Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：查询所有 + Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：单表分页查询 + Task<List<T>> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<T>> QueryByWherePageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：单表分页查询 + Task<List<T>> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<T>> QueryByWherePageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：查询索引多少到多少条 + Task<List<T>> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引多少到多少条
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="takeNum">结束索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<T>> QueryByRangeAsync(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询索引后的所有数据 + Task<List<T>> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引后的所有数据
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<T>> QuerySkipfterIndexAsync(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询指定个数的数据 + Task<List<T>> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询指定个数的数据
        /// </summary>
        /// <param name="takeNum">指定个数</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<T>> QueryTakeIndexAsync(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询条数 + int QueryCount(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        Task<int> QueryCountAsync(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：查询是否存在记录 + bool Any(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询是否存在记录
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：分组查询 + Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectStr">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：分组查询 + Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr,string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectStr">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：分组查询 + Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectExpression">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：分组查询 + Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectExpression">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：执行sql语句或存储过程 + Task<List<TResult>> QueryBySql<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<TResult>> QueryBySqlAsync<TResult>(string sql, object whereObj = null);
        #endregion

        #region 方法：事务执行sql语句或存储过程 + Task<List<TResult>> QueryBySqlTransactions<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 事务执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        Task<List<TResult>> QueryBySqlTransactionsAsync<TResult>(string sql, object whereObj = null);
        #endregion
    }
    #endregion

    #region Method
    public async partial class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        #region 方法：插入数据 + public async Task<object> InsertAsync(T entity, bool isIdentity = true)
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        public async Task<object> InsertAsync(T entity, bool isIdentity = true)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Insert<T>(entity, isIdentity);
                }
            });
        }
        #endregion

        #region 方法：批量插入数据 + public async List<object> InsertRangeAsync(List<T> entites, bool isIdentity = true)
        /// <returns></returns>
        public async Task<List<object>> InsertRangeAsync(List<T> entites, bool isIdentity = true)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.InsertRange<T>(entites, isIdentity);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：更新实体所有的列 + public async bool Update(T model)
        public async Task<bool> UpdateAsync(T model)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Update(model);
                }
            });
        }
        #endregion

        #region 方法：更新数据 + public async bool Update<FiledType>(object model, params FiledType[] whereIn)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<FiledType>(object model, params FiledType[] whereIn)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.Update<T, FiledType>(model, whereIn);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：更新数据 + public async bool Update(object model, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(object model, Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.Update<T>(model, expression);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：删除数据 + public async bool Delete<FiledType>(params FiledType[] whereIn)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<FiledType>(params FiledType[] whereIn)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.Delete<T, FiledType>(whereIn);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：删除数据 + public async bool Delete(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.Delete<T>(expression);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：假删除数据 + public async bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="whereIn">过滤数组</param>
        /// <returns></returns>
        public async Task<bool> FalseDeleteAsync<FiledType>(string filed, params FiledType[] whereIn)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.FalseDelete<T, FiledType>(filed, whereIn);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：假删除数据 + public async bool FalseDelete(string filed, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public async Task<bool> FalseDeleteAsync(string filed, Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.FalseDelete<T>(filed, expression);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
        }
        #endregion

        #region 方法：查询一个 + public async T QuerySingle()
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync()
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Single();
                }
            });
        }
        #endregion

        #region 方法：查询一个 + public async T QuerySingle(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Single(expression);
                }
            });
        }
        #endregion

        #region 方法：查询所有 + public async Task<List<T>> QueryAll()
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync()
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().ToList();
                }
            });
        }
        #endregion

        #region 方法：查询所有 + public async Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字符串</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：条件查询 + public async Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).ToList();
                }
            });
        }
        #endregion

        #region 方法：查询所有 + public async Task<List<T>> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：单表分页查询 + public async Task<List<T>> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByWherePageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).ToPageList(pageIndex, pageSize);
                }
            });
        }
        #endregion

        #region 方法：单表分页查询 + public async Task<List<T>> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByWherePageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).OrderBy(orderbyStr).ToPageList(pageIndex, pageSize);
                }
            });
        }
        #endregion

        #region 方法：查询索引多少到多少条 + public async Task<List<T>> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引多少到多少条
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="takeNum">结束索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public async Task<List<T>> QueryByRangeAsync(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Skip(skipNum).Take(takeNum).ToList();
                }
            });
        }
        #endregion

        #region 方法：查询索引后的所有数据 + public async Task<List<T>> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引后的所有数据
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public async Task<List<T>> QuerySkipfterIndexAsync(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Skip(skipNum).ToList();
                }
            });
        }
        #endregion

        #region 方法：查询指定个数的数据 + public async Task<List<T>> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询指定个数的数据
        /// </summary>
        /// <param name="takeNum">指定个数</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public async Task<List<T>> QueryTakeIndexAsync(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Take(takeNum).ToList();
                }
            });
        }
        #endregion

        #region 方法：查询条数 + public async int QueryCount(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public async Task<int> QueryCountAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).Count();
                }
            });
        }
        #endregion

        #region 方法：查询是否存在记录 + public async bool Any(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询是否存在记录
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Any(whereExpression);
                }
            });
        }
        #endregion

        #region 方法：分组查询 + public async Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectStr">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).GroupBy(groupbyFiles).Select<T, TResult>(selectStr).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：分组查询 + public async Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr,string whereString = "1=1", object whereObj = null)

        public async Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).GroupBy(groupbyFiles).Select<T, TResult>(selectStr).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：分组查询 + public async Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)

        public async Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).GroupBy(groupbyFiles).Select<T, TResult>(selectExpression).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：分组查询 + public async Task<List<TResult>> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)

        public async Task<List<TResult>> QueryByGroupAsync<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).GroupBy(groupbyFiles).Select<T, TResult>(selectExpression).OrderBy(orderbyStr).ToList();
                }
            });
        }
        #endregion

        #region 方法：执行sql语句或存储过程 + public async Task<List<TResult>> QueryBySql<TResult>(string sql, object whereObj = null)

        public async Task<List<TResult>> QueryBySqlAsync<TResult>(string sql, object whereObj = null)
        {
            return await Task.Run(() =>
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    return dbClient.SqlQuery<TResult>(sql, whereObj);
                }
            });
        }
        #endregion

        #region 方法：事务执行sql语句或存储过程 + public async Task<List<TResult>> QueryBySqlTransactions<TResult>(string sql, object whereObj = null)

        public async Task<List<TResult>> QueryBySqlTransactionsAsync<TResult>(string sql, object whereObj = null)
        {
            return await Task.Run(()=> 
            {
                using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
                {
                    try
                    {
                        dbClient.BeginTran();
                        return dbClient.SqlQuery<TResult>(sql, whereObj);
                    }
                    catch (Exception ex)
                    {
                        dbClient.RollbackTran();
                        throw ex;
                    }
                }
            });
            
        }
        #endregion


    }

    #endregion
}
