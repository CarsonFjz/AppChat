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
    #region Interface
    public partial interface IBaseRepository<T> where T : class, new()
    {
        #region 方法：插入数据 + object Insert(T entity, bool isIdentity = true)
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        object Insert(T entity, bool isIdentity = true);

        #endregion

        #region 方法：批量插入数据 + List<object> InsertRange(List<T> entites, bool isIdentity = true)
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entites">实体集合</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        List<object> InsertRange(List<T> entites, bool isIdentity = true);
        #endregion

        #region 方法：更新实体所有的列 + bool Update(T model)
        /// <summary>
        /// 更新实体所有的列
        /// </summary>
        /// <param name="model">实体对象，主键必须是第一位</param>
        /// <returns></returns>
        bool Update(T model);
        #endregion

        #region 方法：更新数据 + bool Update<FiledType>(object model, params FiledType[] whereIn)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        bool Update<FiledType>(object model, params FiledType[] whereIn);
        #endregion

        #region 方法：更新数据 + bool Update(object model, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        bool Update(object model, Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：删除数据 + bool Delete<FiledType>(params FiledType[] whereIn)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        bool Delete<FiledType>(params FiledType[] whereIn);
        #endregion

        #region 方法：删除数据 + bool Delete(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：假删除数据 + bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="whereIn">过滤数组</param>
        /// <returns></returns>
        bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn);
        #endregion

        #region 方法：假删除数据 + bool FalseDelete(string filed, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        bool FalseDelete(string filed, Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：查询一个 + T QuerySingle()
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <returns></returns>
        T QuerySingle();
        #endregion

        #region 方法：查询一个 + T QuerySingle(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        T QuerySingle(Expression<Func<T, bool>> expression);
        #endregion

        #region 方法：查询所有 + List<T> QueryAll()
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<T> QueryAll();
        #endregion

        #region 方法：查询所有 +  List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字符串</param>
        /// <returns></returns>
        List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：条件查询 + public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：查询所有 + List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：单表分页查询 + List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：单表分页查询 + List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
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
        List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：查询索引多少到多少条 + List<T> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引多少到多少条
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="takeNum">结束索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<T> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询索引后的所有数据 + List<T> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引后的所有数据
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<T> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询指定个数的数据 + List<T> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询指定个数的数据
        /// </summary>
        /// <param name="takeNum">指定个数</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<T> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：查询条数 + int QueryCount(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        int QueryCount(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：查询是否存在记录 + bool Any(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询是否存在记录
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region 方法：分组查询 + List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectStr">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：分组查询 + List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr,string whereString = "1=1", object whereObj = null)
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
        List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：分组查询 + List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectExpression">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr);
        #endregion

        #region 方法：分组查询 + List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
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
        List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null);
        #endregion

        #region 方法：执行sql语句或存储过程 + List<TResult> QueryBySql<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        List<TResult> QueryBySql<TResult>(string sql, object whereObj = null);
        #endregion

        #region 方法：事务执行sql语句或存储过程 + List<TResult> QueryBySqlTransactions<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 事务执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        List<TResult> QueryBySqlTransactions<TResult>(string sql, object whereObj = null);
        #endregion
    }
    #endregion

    #region Method
    public partial class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        #region 方法：插入数据 + public object Insert(T entity, bool isIdentity = true)
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        public object Insert(T entity, bool isIdentity = true)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Insert<T>(entity, isIdentity);
            }
        }
        #endregion

        #region 方法：批量插入数据 + public List<object> InsertRange(List<T> entites, bool isIdentity = true)
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entites">实体集合</param>
        /// <param name="isIdentity">是否包含主键</param>
        /// <returns></returns>
        public List<object> InsertRange(List<T> entites, bool isIdentity = true)
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
        }
        #endregion

        #region 方法：更新实体所有的列 + public bool Update(T model)
        /// <summary>
        /// 更新实体所有的列
        /// </summary>
        /// <param name="model">实体对象，主键必须是第一位</param>
        /// <returns></returns>
        public bool Update(T model)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Update(model);
            }
        }
        #endregion

        #region 方法：更新数据 + public bool Update<FiledType>(object model, params FiledType[] whereIn)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        public bool Update<FiledType>(object model, params FiledType[] whereIn)
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
        }
        #endregion

        #region 方法：更新数据 + public bool Update(object model, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">entity为T类型将更新该实体的非主键所有列，如果rowObj类型为匿名类将更新指定列</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public bool Update(object model, Expression<Func<T, bool>> expression)
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
        }
        #endregion

        #region 方法：删除数据 + public bool Delete<FiledType>(params FiledType[] whereIn)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="whereIn">条件数组</param>
        /// <returns></returns>
        public bool Delete<FiledType>(params FiledType[] whereIn)
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
        }
        #endregion

        #region 方法：删除数据 + public bool Delete(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> expression)
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
        }
        #endregion

        #region 方法：假删除数据 + public bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <typeparam name="FiledType"></typeparam>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="whereIn">过滤数组</param>
        /// <returns></returns>
        public bool FalseDelete<FiledType>(string filed, params FiledType[] whereIn)
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
        }
        #endregion

        #region 方法：假删除数据 + public bool FalseDelete(string filed, Expression<Func<T, bool>> expression)
        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <param name="filed">更新删除状态字段</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public bool FalseDelete(string filed, Expression<Func<T, bool>> expression)
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
        }
        #endregion

        #region 方法：查询一个 + public T QuerySingle()
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <returns></returns>
        public T QuerySingle()
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Single();
            }
        }
        #endregion

        #region 方法：查询一个 + public T QuerySingle(Expression<Func<T, bool>> expression)
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public T QuerySingle(Expression<Func<T, bool>> expression)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Single(expression);
            }
        }
        #endregion

        #region 方法：查询所有 + public List<T> QueryAll()
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<T> QueryAll()
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().ToList();
            }
        }
        #endregion

        #region 方法：查询所有 + public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字符串</param>
        /// <returns></returns>
        public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：条件查询 + public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).ToList();
            }
        }
        #endregion

        #region 方法：查询所有 + public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="whereString">where字符串</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        public List<T> QueryByWhere(Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：单表分页查询 + public List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).ToPageList(pageIndex, pageSize);
            }
        }
        #endregion

        #region 方法：单表分页查询 + public List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
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
        public List<T> QueryByWherePage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).OrderBy(orderbyStr).ToPageList(pageIndex, pageSize);
            }
        }
        #endregion

        #region 方法：查询索引多少到多少条 + public List<T> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引多少到多少条
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="takeNum">结束索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<T> QueryByRange(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Skip(skipNum).Take(takeNum).ToList();
            }
        }
        #endregion

        #region 方法：查询索引后的所有数据 + public List<T> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询索引后的所有数据
        /// </summary>
        /// <param name="skipNum">跳过的索引</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<T> QuerySkipfterIndex(int skipNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Skip(skipNum).ToList();
            }
        }
        #endregion

        #region 方法：查询指定个数的数据 + public List<T> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 查询指定个数的数据
        /// </summary>
        /// <param name="takeNum">指定个数</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<T> QueryTakeIndex(int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).OrderBy(orderbyStr).Take(takeNum).ToList();
            }
        }
        #endregion

        #region 方法：查询条数 + public int QueryCount(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public int QueryCount(Expression<Func<T, bool>> whereExpression)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).Count();
            }
        }
        #endregion

        #region 方法：查询是否存在记录 + public bool Any(Expression<Func<T, bool>> whereExpression)
        /// <summary>
        /// 查询是否存在记录
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> whereExpression)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Any(whereExpression);
            }
        }
        #endregion

        #region 方法：分组查询 + public List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectStr">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).GroupBy(groupbyFiles).Select<T, TResult>(selectStr).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：分组查询 + public List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr,string whereString = "1=1", object whereObj = null)
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
        public List<TResult> QueryByGroup<TResult>(string groupbyFiles, string selectStr, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).GroupBy(groupbyFiles).Select<T, TResult>(selectStr).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：分组查询 + public List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="TResult">返回的新的实体数据</typeparam>
        /// <param name="groupbyFiles">分组字段</param>
        /// <param name="selectExpression">筛选字段</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <returns></returns>
        public List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).GroupBy(groupbyFiles).Select<T, TResult>(selectExpression).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：分组查询 + public List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
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
        public List<TResult> QueryByGroup<TResult>(string groupbyFiles, Expression<Func<T, TResult>> selectExpression, Expression<Func<T, bool>> whereExpression, string orderbyStr, string whereString = "1=1", object whereObj = null)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.Queryable<T>().Where(whereExpression).Where(whereString, whereObj).GroupBy(groupbyFiles).Select<T, TResult>(selectExpression).OrderBy(orderbyStr).ToList();
            }
        }
        #endregion

        #region 方法：执行sql语句或存储过程 + public List<TResult> QueryBySql<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        public List<TResult> QueryBySql<TResult>(string sql, object whereObj = null)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return dbClient.SqlQuery<TResult>(sql, whereObj);
            }
        }
        #endregion

        #region 方法：事务执行sql语句或存储过程 + public List<TResult> QueryBySqlTransactions<TResult>(string sql, object whereObj = null)
        /// <summary>
        /// 事务执行sql语句或存储过程
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="whereObj">命令参数对应匿名对象</param>
        /// <returns></returns>
        public List<TResult> QueryBySqlTransactions<TResult>(string sql, object whereObj = null)
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
        }


        #endregion


    }

    #endregion
}

