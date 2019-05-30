using System.Collections.Generic;
using System.Web;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class MessageViewModel
    {
        public List<Message> Messages { get; set; }
        public string ContenuMessage { get; set; }
        public HttpPostedFileBase[] PiecesJointes { get; set; }

        // Pour les labels
        public Message Message { get; set; }
    }
}