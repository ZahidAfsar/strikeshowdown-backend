using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services;

namespace strikeshowdown_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _noti;
        public NotificationController(NotificationService noti)
        {
            _noti = noti;
        }

        [HttpGet]
        [Route("GetNotificationsByUserID/{id}")]
        public IEnumerable<NotificationModel> GetNotificationsByUserID(int id){
            return _noti.GetNotificationsByUserID(id);
        }

        [HttpPost]
        [Route("CreateNotification")]
        public bool CreateNotification(CreateNotificationDTO noti){
            return _noti.CreateNotification(noti);
        }

        [HttpPut]
        [Route("DeleteNotification")]
        public bool DeleteNotification(NotificationModel noti){
            return _noti.DeleteNotification(noti);
        }

        [HttpPut]
        [Route("MakeNotificationRead")]
        public bool MakeNotificationRead(NotificationModel noti){
            return _noti.MakeNotificationRead(noti);
        }

    }
}