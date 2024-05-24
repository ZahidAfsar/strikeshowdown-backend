using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace strikeshowdown_backend.Controllers
{
    [ApiController]
    [Route("MessageController")]
    public class MessageController
    {
        private readonly ChatroomService _service;

        public MessageController(ChatroomService s){
            _service = s;
        }

        [HttpPost]
        [Route("SendMessage/{name}")]
        public bool SendMessage(MessageDTO message, string name){
            return _service.AddMessage(message, name);
        }

        [HttpPost]
        [Route("CreateChatroom/{name}")]
        public bool CreateChatroom(string name){
            return _service.AddChatroom(name);
        }

        [HttpGet]
        [Route("GetMessagesFromChatroom/{name}")]
        public List<MessageModel> GetMessages(string name){
            return _service.GetChatroomMessagesFrom(name);
        }

        [HttpDelete]
        [Route("DeleteChatroom/{name}")]
        public bool Delete(string name){
            return _service.DeleteChatroom(name);
        }

        [HttpDelete]
        [Route("DeleteMessage/{messageID}/{chat}")]
        public bool DeleteMessage(int messageID, string chat){
            return _service.DeleteMessageFrom(messageID, chat);
        }
    }
}