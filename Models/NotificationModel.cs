using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models.DTO
{
    public class NotificationModel
    {
        public int ID {get; set;}
        public int SenderID {get; set;}
        public string SenderUsername { get; set;}
        public int RecieverID { get; set; }
        public int PostID { get; set; }
        public string RecieverUsername { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set;}

        public NotificationModel() {
            
        }
        
    }


}