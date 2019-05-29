using System.Collections.Generic;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class MessageViewModel
    {
        public List<Message> Messages { get; set; }
        public Message NewMessage { get; set; }
    }
}