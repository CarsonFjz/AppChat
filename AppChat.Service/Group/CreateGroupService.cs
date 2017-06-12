using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service
{
    #region Interface
    public interface ICreateGroupService
    {
        /// <summary>
        /// 生成组名的方法，因为有很多种，这里用接口，可以选择不同的生成方式
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        string CreateName(Guid from, Guid to);
        string CreateName(Guid gid);
    }
    #endregion

    #region Method
    public class CreateGroupService : ICreateGroupService
    {
        public string CreateName(Guid gid)
        {
            throw new NotImplementedException();
        }

        public string CreateName(Guid from, Guid to)
        {
            throw new NotImplementedException();
        }
    } 
    #endregion
}
