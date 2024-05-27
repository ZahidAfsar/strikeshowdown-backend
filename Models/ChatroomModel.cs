using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models
{
    public class ChatroomModel
    {
        public int ID { get; set; }
        public int FirstUserID { get; set; }
        public int SecondUserID { get; set; }
        public string? ChatroomName { get; set; }
        public List<MessageModel>? Messages { get; set; }
        public ChatroomModel(string name)
        {
            ChatroomName = name;
            Messages = new List<MessageModel>();
        }

        public ChatroomModel(){ Messages = new List<MessageModel>(); }

    }
}