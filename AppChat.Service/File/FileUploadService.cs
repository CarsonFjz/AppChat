using System;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using AppChat.Service._Interface;
using System.Web.Http;

namespace AppChat.Service.File
{
    public class FileUploadService : ApiController, IFileUploadService
    {
        private string date;//按日期分文件夹
        private string host = System.Web.Hosting.HostingEnvironment.MapPath("~/AppData");//根目录

        private string[] radio = { ".mp3", ".mp4", ".aiff" };
        private string[] image = {".jpg",".jpeg",".png",".bmp",".gif" };
        private string[] video = {".avi",".mp4",".rmvb",".mpge",".wmv" };
        private string[] file =  {".jpg",".jpeg",".png",".doc",".docx",".xls",".xlsx",".ppt",".pptx",".vsd",".vsdx",".pdf",".txt",".zip",".rar" ,".mp3",".mp4",".avi",".rmvb"};
        public FileUploadService()
        {
            date = DateTime.Now.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 获取File集容器
        /// </summary>
        /// <param name="path">File域</param>
        /// <returns></returns>
        public AllowFileMultipartMemoryStreamProvider FileProvider(string fileArea, FileType type)
        {
            //如果不是form-data,返回415状态才码
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = string.Format("{0}/{1}/{2}", host, fileArea, date);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            string[] allowedExtension = null;

            //允许文件类型
            switch(type)
            {
                case FileType.image:
                    allowedExtension = image; break;
                case FileType.radio:
                    allowedExtension = radio; break;
                case FileType.video:
                    allowedExtension = video; break;
                case FileType.file:
                    allowedExtension = file; break;
                default:
                    break;
            }


            return new AllowFileMultipartMemoryStreamProvider(rootPath, allowedExtension);

        }
        public string GetLocalFilePath(string fileArea, MultipartFileData file)
        {
            return string.Format("AppData/{0}/{1}/{2}", fileArea, date, Path.GetFileName(file.LocalFileName));
        }
    }

    
    public class AllowFileMultipartMemoryStreamProvider : MultipartFormDataStreamProvider
    {
        public string[] allowedExtension = null;

        public AllowFileMultipartMemoryStreamProvider(string path, string [] allowedExtensionSet)
            : base(path) 
        {
             allowedExtension = allowedExtensionSet;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return Path.GetRandomFileName() + Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", string.Empty));
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (headers.ContentDisposition.FileName != null)
            {
                var fileExtension = Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", string.Empty));

                return allowedExtension == null || allowedExtension.Any(x => x.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)) ? base.GetStream(parent, headers) : Stream.Null;
            }

            return base.GetStream(parent, headers);
        }
    }

    public enum FileType
    { 
        image = 1,
        radio = 2,
        video = 4,
        file = 8,
        other = 16,
    }
}
