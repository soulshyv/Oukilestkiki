using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Oukilestkiki.ViewModels
{
    public class PhotoCreateViewModel
    {
        [DisplayName("Photos")]
        public HttpPostedFileBase[] ImageFiles { get; set; }
    }
}