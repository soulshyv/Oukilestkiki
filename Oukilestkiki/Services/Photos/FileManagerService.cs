using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Web.Configuration;
using Oukilestkiki.ViewModels;
using HttpContext = System.Web.HttpContext;

namespace Oukilestkiki.Services.Photos
{
    public static class FileManagerService
    {
        private static string BasePath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["PhotoUploadDir"]);

        public static Photo Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            var basePath = BasePath;
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(basePath, fileName);
            file.SaveAs(path);

            return new Photo
            {
                FileName = fileName,
                FilePath = path,
            };
        }

        public static void DeleteByFileName(string fileName)
        {
            foreach (var file in Directory.GetFiles(BasePath))
            {
                if (Path.GetFileName(file) == fileName)
                {
                    Directory.Delete(file);
                }
            }
        }
    }
}