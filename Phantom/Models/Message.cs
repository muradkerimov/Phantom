using System;

namespace Phantom.Models
{
    public class Message:Base
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Text { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
