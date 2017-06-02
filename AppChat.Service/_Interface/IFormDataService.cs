using AppChat.Service.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IFormDataService<T> where T : new()
    {
        /// <summary>
        /// 反射表单内容 方法
        /// </summary>
        /// <param name="provider">AllowFileMultipartMemoryStreamProvider对象</param>
        /// <returns>T</returns>
        T GetFormData(AllowFileMultipartMemoryStreamProvider provider);
    }
}
