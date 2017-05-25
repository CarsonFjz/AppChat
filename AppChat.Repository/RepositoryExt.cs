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
    public partial interface IJoinRepository<T> where T : class, new()
    {
    }
    public partial class JoinRepository<T> : IJoinRepository<T> where T : class, new()
    {
        public List<T> QueryJ(int skipNum, int takeNum, Expression<Func<T, bool>> whereExpression, string orderbyStr)
        {
            using (SqlSugarClient dbClient = SqlSugarInstance.GetInstance())
            {
                return null;
            }
        }
    }
}
