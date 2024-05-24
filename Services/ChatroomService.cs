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

        public ChatroomService(DataContext c){
            _context = c;
        }

        public bool AddMessage(MessageDTO message, string chatroom){

            MessageModel newMessage = new MessageModel(message.Message, message.PublisherName, message.UserID);

            if(DoesChatroomExist(chatroom)){
                ChatroomModel chat = GetChatroomByName(chatroom);
                chat.Messages.Add(newMessage);
                _context.Update<ChatroomModel>(chat);
            }
            return _context.SaveChanges() != 0;

        }

        public ChatroomModel GetChatroomByName(string name){
            return _context.Chatrooms.FirstOrDefault(room => room.ChatroomName == name);
        }

        public List<MessageModel> GetChatroomMessagesFrom(string name){
            ChatroomModel c = GetChatroomByName(name);
            if(DoesChatroomExist(name)){
                return _context.Messages.Where(m => m.ChatroomModelID == c.ID).ToList();
            }
            else
                return null;
        }

        public bool DeleteMessageFrom(int messageID, string chat){
            if(DoesChatroomExist(chat)){
                _context.Messages.Remove(_context.Messages.FirstOrDefault(m => m.ID == messageID));
            }
            return _context.SaveChanges() != 0;
        }

        public bool AddChatroom(string name){
            if(!DoesChatroomExist(name)){
                ChatroomModel newChat = new ChatroomModel(name);
                _context.Add<ChatroomModel>(newChat);
            }
            return _context.SaveChanges() != 0;
        }

        public bool DeleteChatroom(string name){

            if(DoesChatroomExist(name)){
                ChatroomModel c = GetChatroomByName(name);
                _context.Remove<ChatroomModel>(c);
                return _context.SaveChanges() != 0;
            }
            return false;
        }        

        public bool DoesChatroomExist(string name){
            return GetChatroomByName(name) != null;
        }
    }
}