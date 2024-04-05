using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;

        public UserService(DataContext context){
            _context = context;
        }
        public bool DoesUserExist(string Username){
            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;

        }
          public bool AddUser(CreateAccountDTO UserToAdd){
            bool result = false;

            if(!DoesUserExist(UserToAdd.Username)){
                 UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.Email = UserToAdd.Email;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;
                newUser.SecurityQuestion = UserToAdd.SecurityQuestionTwo;
                newUser.SecurityQuestionTwo = UserToAdd.SecurityQuestionTwo;
                newUser.SecurityQuestionThree = UserToAdd.SecurityQuestionThree;
                newUser.SecurityAnswer = UserToAdd.SecurityAnswer;
                newUser.SecurityAnswerTwo = UserToAdd.SecurityAnswerTwo;
                newUser.SecurityAnswerThree = UserToAdd.SecurityAnswerThree;
                newUser.FullName = UserToAdd.FullName;
                newUser.ProfileImage = UserToAdd.ProfileImage;
                newUser.Pronouns = UserToAdd.Pronouns;
                newUser.Wins = UserToAdd.Wins;
                newUser.Loses = UserToAdd.Loses;
                newUser.Style = UserToAdd.Style;
                newUser.MainCenter = UserToAdd.MainCenter;
                newUser.Average = UserToAdd.Average;
                newUser.Earnings = UserToAdd.Earnings;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public PasswordDTO HashPassword(string password){

            PasswordDTO newHashPassword = new PasswordDTO();

            byte[] SaltByte = new byte[64];

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            provider.GetNonZeroBytes(SaltByte);

            string salt = Convert.ToBase64String(SaltByte);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;

            return newHashPassword;

        }

        // verify users password 
        public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt){

            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;

        }

          public IActionResult Login(LoginDTO User)
          {
            IActionResult Result = Unauthorized();

            if(DoesUserExist(User.UsernameOrEmail)){

                UserModel foundUser = GetUserByUsername(User.UsernameOrEmail);
                

                if(VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(), 
                        expires: DateTime.Now.AddMinutes(30), 
                        signingCredentials: signinCredentials 
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    Result = Ok(new { Token = tokenString });
                }

            }else{
                // UserModel foundUser = GetUserByEmail(User.Email);

                //  if(VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
                // {
                //     var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                //     var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //     var tokeOptions = new JwtSecurityToken(
                //         issuer: "http://localhost:5000",
                //         audience: "http://localhost:5000",
                //         claims: new List<Claim>(), 
                //         expires: DateTime.Now.AddMinutes(30), 
                //         signingCredentials: signinCredentials 
                //     );

                //     var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                //     Result = Ok(new { Token = tokenString });
                // }
            }
            return Result;
          }


        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username || user.Email == username );
        }


        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateUsername(int id, string username)
        {
            UserModel  foundUser = GetUserById(id);

            bool result = false;

            if(foundUser != null)
            {
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool UpdateStats(string username, string FullName, string Pronouns, string ProfileImage, int Wins, int Loses, string Style, string Average, string MainCenter, string Earnings)
        {
            UserModel  foundUser = GetUserByUsername(username);

            bool result = false;

            if(foundUser != null)
            {
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        public bool DeleteUser(string userToDelete)
        {
            UserModel foundUser = GetUserByUsername(userToDelete);

            bool result = false;

            if(foundUser != null)
            {
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UseridDTO GetUserIdDTObyUsername(string username){

            UseridDTO UserInfo = new UseridDTO();

            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);

            UserInfo.UserId = foundUser.ID;

            UserInfo.PublisherName = foundUser.Username;

            return UserInfo;
        }

    }
}