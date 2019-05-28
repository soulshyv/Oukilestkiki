using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Web.Configuration;
using BO;
using Oukilestkiki.ViewModels;
using HttpContext = System.Web.HttpContext;

namespace Oukilestkiki.Services.Photos
{
    public static class FileManagerService
    {
        private static string PhotosBaseDir = WebConfigurationManager.AppSettings["PhotoUploadDir"];

        public static Photo Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            var fileName = Path.GetFileName(file.FileName);
            var realPath = HttpContext.Current.Server.MapPath(Path.Combine(PhotosBaseDir, fileName));
            var path = Path.Combine(PhotosBaseDir, fileName);
            file.SaveAs(realPath);

            return new Photo
            {
                FileName = fileName,
                FilePath = path,
            };
        }

        public static void DeleteByFileName(string fileName)
        {
            foreach (var file in Directory.GetFiles(HttpContext.Current.Server.MapPath(PhotosBaseDir)))
            {
                if (Path.GetFileName(file) == fileName)
                {
                    Directory.Delete(file);
                }
            }
        }
    }
}