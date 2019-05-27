using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Oukilestkiki.ViewModels
{
    public class PhotoCreateViewModel
    {

        [DisplayName("Photo")]
        public HttpPostedFileBase[] ImageFile { get; set; }
    }
}