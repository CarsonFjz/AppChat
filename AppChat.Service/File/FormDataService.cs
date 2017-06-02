using AppChat.Service._Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.File
{
    public class FormDataService<T> where T : IFormDataService<T>, new()
    {
        /// <summary>
        /// 反射表单内容 方法
        /// </summary>
        /// <param name="provider">AllowFileMultipartMemoryStreamProvider对象</param>
        /// <returns>T</returns>
        public T GetFormData(AllowFileMultipartMemoryStreamProvider provider)
        {
            T model = Activator.CreateInstance<T>();
            var type = typeof(T);
            var modelPropertyInfos = type.GetProperties();//获取T的属性集合

            foreach (var item in provider.FormData)
            {
                foreach (var pi in modelPropertyInfos)
                {
                    var name = pi.Name;//获取model的字段名

                    if (name == item)
                    {
                        pi.SetValue(model, provider.FormData[name]);//把对应的值value放进该字段,可以设置下标
                    }
                }
            }

            return model;
        }
    }
}
