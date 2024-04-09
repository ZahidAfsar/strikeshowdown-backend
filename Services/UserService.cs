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

        public UserService(DataContext context)
        {
            _context = context;
        }
        public bool DoesUserExist(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username || user.Email == username) != null;

        }
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;

            if (!DoesUserExist(UserToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);
                var hashAnswers = HashSecurity(UserToAdd.SecurityAnswer, UserToAdd.SecurityAnswerTwo, UserToAdd.SecurityAnswerThree);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.Email = UserToAdd.Email;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;
                newUser.SecurityQuestion = UserToAdd.SecurityQuestion;
                newUser.SecurityQuestionTwo = UserToAdd.SecurityQuestionTwo;
                newUser.SecurityQuestionThree = UserToAdd.SecurityQuestionThree;
                newUser.SecuritySalt = hashAnswers.Salt;
                newUser.SecurityHash = hashAnswers.Hash;
                newUser.SecuritySaltTwo = hashAnswers.SaltTwo;
                newUser.SecurityHashTwo = hashAnswers.HashTwo;
                newUser.SecuritySaltThree = hashAnswers.SaltThree;
                newUser.SecurityHashThree = hashAnswers.HashThree;
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

        public PasswordDTO HashPassword(string password)
        {

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

        public SecurityAnswersDTO HashSecurity(string SecurityAnswer, string SecurityAnswerTwo, string SecurityAnswerThree)
        {
            SecurityAnswersDTO newHashSecurity = new SecurityAnswersDTO();

            byte[] SaltByte = new byte[64];
            byte[] SaltByteTwo = new byte[64];
            byte[] SaltByteThree = new byte[64];

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            provider.GetNonZeroBytes(SaltByte);
            provider.GetNonZeroBytes(SaltByteTwo);
            provider.GetNonZeroBytes(SaltByteThree);

            string salt = Convert.ToBase64String(SaltByte);
            string saltTwo = Convert.ToBase64String(SaltByteTwo);
            string saltThree = Convert.ToBase64String(SaltByteThree);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(SecurityAnswer, SaltByte, 10000);
            Rfc2898DeriveBytes rfc2898DeriveBytesTwo = new Rfc2898DeriveBytes(SecurityAnswerTwo, SaltByteTwo, 10000);
            Rfc2898DeriveBytes rfc2898DeriveBytesThree = new Rfc2898DeriveBytes(SecurityAnswerThree, SaltByteThree, 10000);

            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            string hashTwo = Convert.ToBase64String(rfc2898DeriveBytesTwo.GetBytes(256));
            string hashThree = Convert.ToBase64String(rfc2898DeriveBytesThree.GetBytes(256));

            newHashSecurity.Salt = salt;
            newHashSecurity.Hash = hash;

            newHashSecurity.SaltTwo = saltTwo;
            newHashSecurity.HashTwo = hashTwo;

            newHashSecurity.SaltThree = saltThree;
            newHashSecurity.HashThree = hashThree;

            return newHashSecurity;

        }

        // verify users password 
        public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt)
        {

            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;

        }

        public bool VerifySecurity(string? SecurityAnswer, string? storedHash, string? storedSalt)
        {

            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(SecurityAnswer, SaltBytes, 10000);

            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;


        }


        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized();

            if (DoesUserExist(User.UsernameOrEmail))
            {

                UserModel foundUser = GetUserByUsername(User.UsernameOrEmail);


                if (VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
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

            }
            return Result;
        }


        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username || user.Email == username);
        }

        public UserWithoutSaltHashDTO GetUserByUsernameOrEmail(string usernameOrEmail){
            
            if(DoesUserExist(usernameOrEmail)){
                
                var foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
                UserWithoutSaltHashDTO user = new UserWithoutSaltHashDTO();
                user.Username = foundUser.Username;
                user.Email = foundUser.Email;
                user.SecurityQuestion = foundUser.SecurityQuestion;
                user.SecurityQuestionTwo = foundUser.SecurityQuestionTwo;
                user.SecurityQuestionThree = foundUser.SecurityQuestionThree;
                user.FullName = foundUser.FullName;
                user.ProfileImage = foundUser.ProfileImage;
                user.Pronouns = foundUser.Pronouns;
                user.Wins = foundUser.Wins;
                user.Loses = foundUser.Loses;
                user.Style = foundUser.Style;
                user.MainCenter = foundUser.MainCenter;
                user.Average = foundUser.Average;
                user.Earnings = foundUser.Earnings;
                return user;
            } 

            return null;
        }


        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateUsername(int id, string username)
        {
            UserModel foundUser = GetUserById(id);

            bool result = false;

            if (foundUser != null)
            {
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool ForgotPassword(string UsernameOrEmail, string Password)
        {
            UserModel foundUser = GetUserByUsername(UsernameOrEmail);

            var password = HashPassword(Password);

            bool result = false;
            if (foundUser != null)
            {
                foundUser.Salt = password.Salt;
                foundUser.Hash = password.Hash;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool UpdateStats(string UsernameOrEmail, string username, string Email, string FullName, string Pronouns, string ProfileImage, int Wins, int Loses, string Style, string Average, string MainCenter, string Earnings)
        {
            UserModel foundUser = GetUserByUsername(UsernameOrEmail);

            bool result = false;

            if (foundUser != null)
            {
                foundUser.Username = username;
                foundUser.Email = Email;
                foundUser.FullName = FullName;
                foundUser.Pronouns = Pronouns;
                foundUser.ProfileImage = ProfileImage;
                foundUser.Wins = Wins;
                foundUser.Loses = Loses;
                foundUser.Style = Average;
                foundUser.MainCenter = MainCenter;
                foundUser.Earnings = Earnings;
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

            if (foundUser != null)
            {
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UseridDTO GetUserIdDTObyUsername(string username)
        {

            UseridDTO UserInfo = new UseridDTO();

            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);

            UserInfo.UserId = foundUser.ID;

            UserInfo.PublisherName = foundUser.Username;

            return UserInfo;
        }
        public bool GetSecurity(string UsernameOrEmail, string SecurityQuestion, string SecurityAnswer)
        {
            bool result = false;

            if (DoesUserExist(UsernameOrEmail))
            {

                UserModel foundUser = GetUserByUsername(UsernameOrEmail);

                if (SecurityQuestion == foundUser.SecurityQuestion)
                {
                    if (VerifySecurity(SecurityAnswer, foundUser.SecurityHash, foundUser.SecuritySalt))
                    {
                        return true;
                    }

                }
                else if (SecurityQuestion == foundUser.SecurityQuestionTwo)
                {

                    if (VerifySecurity(SecurityAnswer, foundUser.SecurityHashTwo, foundUser.SecuritySaltTwo))
                    {
                        return true;
                    }

                }
                else if (SecurityQuestion == foundUser.SecurityQuestionThree)
                {
                    if (VerifySecurity(SecurityAnswer, foundUser.SecurityHashThree, foundUser.SecuritySaltThree))
                    {
                        return true;
                    }

                }

            }
            return result;
        }


    }
}