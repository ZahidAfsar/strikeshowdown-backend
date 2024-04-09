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

        [HttpPut]
        [Route("UpdateStats/{UsernameOrEmail}/{username}/{Email}/{FullName}/{Pronouns}/{ProfileImage}/{Wins}/{Loses}/{Style}/{Average}/{MainCenter}/{Earnings}")]
        public bool UpdateStats(string UsernameOrEmail, string username, string Email, string FullName, string Pronouns, string ProfileImage, int Wins, int Loses, string Style, string Average, string MainCenter, string Earnings){
            return _data.UpdateStats(UsernameOrEmail, username, Email, FullName, Pronouns, ProfileImage, Wins, Loses, Style, Average, MainCenter, Earnings);
        }

        [HttpPut]
        [Route("ForgotPassword/{UsernameOrEmail}/{password}")]
        public bool ForgotPassword(string UsernameOrEmail, string password){
            return _data.ForgotPassword(UsernameOrEmail, password);
        }

        [HttpGet]
        [Route("GetSecurity/{UsernameOrEmail}/{SecurityQuestion}/{SecurityAnswer}")]
        public bool GetSecurity(string UsernameOrEmail, string SecurityQuestion, string SecurityAnswer){
            return _data.GetSecurity(UsernameOrEmail, SecurityQuestion, SecurityAnswer);
        }

        // DeleteUser Endpoint
        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete){
            return _data.DeleteUser(userToDelete);
        }

        [HttpGet]
        [Route("GetUserByUsernameOrEmail/{usernameOrEmail}")]
        public UserWithoutSaltHashDTO GetUserByUsername(string usernameOrEmail){
            
            return _data.GetUserByUsernameOrEmail(usernameOrEmail);
        }
        
    }
}