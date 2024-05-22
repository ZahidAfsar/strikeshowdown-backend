using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models;
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

        public UserModel GetUserByID(int id ){
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        public NotificationModel GetNotifcationByID(int id){
            return _context.NotificationInfo.SingleOrDefault(noti => noti.ID == id);
        }

        public IEnumerable<NotificationModel> GetNotificationsByUserID(int id){
            return _context.NotificationInfo.Where(noti => noti.RecieverID == id && noti.IsDeleted == false);
        }

        public bool CreateNotification(CreateNotificationDTO createdNoti){
            UserModel sender = GetUserByID(createdNoti.SenderID);
            UserModel reciever = GetUserByID(createdNoti.RecieverID);

            NotificationModel newNoti = new NotificationModel();

            newNoti.RecieverID = createdNoti.RecieverID;
            newNoti.RecieverUsername = reciever.Username;
            newNoti.SenderID = createdNoti.SenderID;
            newNoti.SenderUsername = sender.Username;
            newNoti.PostID = createdNoti.PostID;
            newNoti.Image = sender.ProfileImage;
            newNoti.Type = createdNoti.Type;
            newNoti.Content = createdNoti.Content;
            newNoti.IsRead = false;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);
            return _context.SaveChanges() != 0;
        }

        public NotificationModel GetFriendRequestNotification(int yourID, int userID){
            return _context.NotificationInfo.SingleOrDefault(noti => noti.Type == "Inbox FriendRequest" && noti.SenderID == userID && noti.RecieverID == yourID && noti.IsDeleted == false);
        }

        public bool DeleteNotification(NotificationModel noti){
            noti.IsDeleted = true;

            _context.Update<NotificationModel>(noti);

            return _context.SaveChanges() != 0;
        }

        public bool MakeNotificationRead(NotificationModel noti){
            noti.IsRead = true;

            _context.Update<NotificationModel>(noti);

            return _context.SaveChanges() != 0;
        }
    }
}