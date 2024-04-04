using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models.DTO
{
    public class CreateAccountDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion { get; set; }
    }
}