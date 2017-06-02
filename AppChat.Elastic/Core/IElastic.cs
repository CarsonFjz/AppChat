using AppChat.ElasticSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.ElasticSearch.Core
{
    public interface IElastic<T> where T : BaseEntity, new()
    {
        //初始化需要执行
        void SetIndexInfo(string indexName, string indexType);

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonDocument"></param>
        /// <returns></returns>
        bool Index(string id, string jsonDocument);
        bool Index(T entity);

        /// <summary>
        /// 根据 Id获取某条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T QuerySingle(string id);


        /// <summary>
        /// 根据具体条件查询（单一条件）
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        BaseQueryEntity<T> QueryInTerm(string field, string value);


        /// <summary>
        /// 根据query条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        BaseQueryEntity<T> QueryBayConditions(string query);


        /// <summary>
        /// 根据单一条件查询删除
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool DeleteByQuery(string query);


        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        bool DeleteIndex();
        string DeleteIndex(string indexName);


        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderField"></param>
        /// <param name="orderType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        BaseQueryEntity<T> QueryInFields(string value, string orderField = "", string orderType = "asc", int pageIndex = 1, int pageSize = 20);


        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool IndexBulk(IEnumerable<T> list);
    } 
}
