
using AppChat.Utils.JsonResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AppChat.Model;

namespace AppChat.Utils.FileUpload
{
    public class FileUploadHelper
    {
        public static JsonResultModel Upload(HttpPostedFileBase file, string fullPath, bool isImg = true)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string fileName = Guid.NewGuid().ToString();
                string fullFileName = string.Format("{0}{1}", fileName, fileExtension);
                string oldFileName = Path.GetFileName(file.FileName);

                string fileSavePath = string.Format("{0}{1}", fullPath, fullFileName);
                string url = "/upload/" + fullFileName;
                file.SaveAs(fileSavePath);
                object imgObj = new { src = url };
                object fileObj = new { src = url, name = oldFileName };
                return JsonResultHelper.CreateJson(isImg ? imgObj : fileObj);
            }
            return JsonResultHelper.CreateJson(null, false, "请添加一个文件");

        }
    }
}
