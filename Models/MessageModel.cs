using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace strikeshowdown_backend.Models
{
    public class MessageModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ChatroomModelID { get; set; }
        public string? Message { get; set; }
        public string? PublisherName { get; set; }

        public MessageModel(string m, string p, int userID)
        {
            Message = m;
            PublisherName = p;
            UserID = userID;
        }

        public MessageModel() {}
    }
}