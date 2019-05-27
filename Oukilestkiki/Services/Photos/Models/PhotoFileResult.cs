using System.IO;

namespace Oukilestkiki.Services.Photos.Models
{
    public class PhotoFileResult
    {
        public byte[] FileContent { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
    }
}