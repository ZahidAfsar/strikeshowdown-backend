using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class ChatroomService
    {
        public DataContext _context;

        public ChatroomService(DataContext c)
        {
            _context = c;
        }

        public bool AddMessage(MessageDTO message)
        {
            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.ID == message.UserID);

            MessageModel newMessage = new MessageModel();
            newMessage.Message = message.Message;
            newMessage.UserID = message.UserID;
            newMessage.PublisherName = foundUser.Username;

            if (DoesChatroomExist(message.ChatRoomName))
            {
                newMessage.ChatRoomName = message.ChatRoomName;
                _context.Add(newMessage);
            }
            return _context.SaveChanges() != 0;

        }

        public ChatroomModel GetChatroomByName(string name)
        {
            return _context.Chatrooms.FirstOrDefault(room => room.ChatroomName == name);
        }

        public IEnumerable<MessageModel> GetChatroomMessagesFrom(string name)
        {
            ChatroomModel c = GetChatroomByName(name);
            if (DoesChatroomExist(name))
            {
                return _context.Messages.Where(m => m.ChatRoomName == c.ChatroomName);
            }
            else
                return null;
        }

        public bool DeleteMessageFrom(int messageID, string chat)
        {
            if (DoesChatroomExist(chat))
            {
                _context.Messages.Remove(_context.Messages.FirstOrDefault(m => m.ID == messageID));
            }
            return _context.SaveChanges() != 0;
        }

        public bool AddChatroom(int yourID, int userID, string name)
        {
            if (!DoesChatroomExist(name))
            {
                ChatroomModel newChat = new ChatroomModel();
                newChat.ChatroomName = name;
                newChat.FirstUserID = yourID;
                newChat.SecondUserID = userID;
                _context.Add(newChat);
            }
            return _context.SaveChanges() != 0;
        }

        public ChatroomModel GetChatroomByUserIDs(int firstID, int secondID) {
            return _context.Chatrooms.SingleOrDefault(c =>( c.FirstUserID == firstID || c.FirstUserID == secondID) && (c.SecondUserID == secondID || c.SecondUserID == firstID));
        }

        public ChatRoomNameDTO JoinChatRoom (int yourID, int userID){
            ChatRoomNameDTO name = new ChatRoomNameDTO();
            
            ChatroomModel chat = GetChatroomByUserIDs(yourID, userID);

            if(chat != null){
                name.ChatroomName = chat.ChatroomName;
                return name;
            } else {
                string chatName = "SS" + yourID.ToString() + "SS" + userID.ToString();
                if(AddChatroom(yourID, userID, chatName)){
                    name.ChatroomName = chatName;
                    return name;
                }
            }

            return null;
        }

        public bool DeleteChatroom(string name)
        {

            if (DoesChatroomExist(name))
            {
                ChatroomModel c = GetChatroomByName(name);
                _context.Remove<ChatroomModel>(c);
                return _context.SaveChanges() != 0;
            }
            return false;
        }

        public bool DoesChatroomExist(string name)
        {
            return GetChatroomByName(name) != null;
        }
    }
}