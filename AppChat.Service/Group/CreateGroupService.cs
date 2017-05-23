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
        string CreateName(int from, int to);
        string CreateName(int gid);
    }
    #endregion

    #region Method
    public class CreateGroupService : ICreateGroupService
    {
        public string CreateName(int gid)
        {
            throw new NotImplementedException();
        }

        public string CreateName(int from, int to)
        {
            throw new NotImplementedException();
        }
    } 
    #endregion
}
