using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Web.Configuration;
using Oukilestkiki.ViewModels;
using HttpContext = System.Web.HttpContext;

namespace Oukilestkiki.Services.Photos
{
    public static class UploaderService
    {
        public static Photo Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            var basePath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["PhotoUploadDir"]);
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(basePath, fileName);
            file.SaveAs(path);

            return new Photo
            {
                FileName = fileName,
                FilePath = path,
            };
        }
    }
}