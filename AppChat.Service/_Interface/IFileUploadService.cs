using AppChat.Service.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IFileUploadService
    {
        /// <summary>
        /// File容器存储方法
        /// </summary>
        /// <param name="path">File域</param>
        /// <param name="FileType">允许的File后缀enum</param>
        /// <returns>AllowFileMultipartMemoryStreamProvider 对象</returns>
        /// <use>1.var provider = FileProvider(fileArea, allowedExtension); 2.foreach (MultipartFileData file in provider.FileData) file就是文件对象</use>
        AllowFileMultipartMemoryStreamProvider FileProvider(string path, FileType type);

        /// <summary>
        /// 获取文件服务器地址
        /// </summary>
        /// <param name="fileArea">File域</param>
        /// <param name="file">File对象</param>
        /// <returns></returns>
        string GetLocalFilePath(string fileArea, MultipartFileData file);
    }
}
