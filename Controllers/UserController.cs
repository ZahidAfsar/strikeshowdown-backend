using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services;

namespace strikeshowdown_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;

        public UserController(UserService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO User){
            return _data.Login(User);
        }

        [HttpPost]
        [Route("AddUser")] 
        public bool AddUser(CreateAccountDTO UserToAdd){
            return _data.AddUser(UserToAdd);
        }
        
        [HttpPut]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate){
            return _data.UpdateUser(userToUpdate);
        }

        [HttpPut]
        [Route("UpdateUser/{id}/{username}")]
        public bool UpdateUser(int id, string username){
            return _data.UpdateUsername(id, username);
        }

        // DeleteUser Endpoint
        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete){
            return _data.DeleteUser(userToDelete);
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public UseridDTO GetUserByUsername(string username){
            return _data.GetUserIdDTObyUsername(username);
        }


    }
}