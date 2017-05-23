using AppChat.ORM;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Repository
{
    public partial interface IBaseRepository<T> where T : class, new()
    {
    }
    public partial class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {

    }
}
