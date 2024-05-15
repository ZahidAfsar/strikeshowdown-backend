using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class NotificationService
    {
        private readonly DataContext _context;
        public NotificationService(DataContext context)
        {
            _context = context;
        }

        public NotificationModel GetNotifcationByID(int id){
            return _context.NotificationInfo.SingleOrDefault(noti => noti.ID == id);
        }

        public IEnumerable<NotificationModel> GetNotificationsByUserID(int id){
            return _context.NotificationInfo.Where(noti => noti.RecieverID == id && noti.IsDeleted == false);
        }

        public bool CreateNotification(CreateNotificationDTO createdNoti){
            NotificationModel newNoti = new NotificationModel();
            newNoti.RecieverID = createdNoti.RecieverID;
            newNoti.SenderID = createdNoti.SenderID;
            newNoti.Type = createdNoti.Type;
            newNoti.Content = createdNoti.Content;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteNotification(int id){
            NotificationModel foundNoti = GetNotifcationByID(id);
            foundNoti.IsDeleted = true;

            _context.Update<NotificationModel>(foundNoti);

            return _context.SaveChanges() != 0;
        }
    }
}