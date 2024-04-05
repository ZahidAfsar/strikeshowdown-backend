using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models.DTO
{
    public class SecurityAnswersDTO
    {
        public string Salt { get; set; }
        public string SaltTwo { get; set; }
        public string SaltThree { get; set; }
        public string Hash { get; set; }
        public string HashTwo { get; set; }
        public string HashThree { get; set; }
    }
}