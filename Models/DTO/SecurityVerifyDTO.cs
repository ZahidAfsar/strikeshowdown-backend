using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models.DTO
{
    public class SecurityVerifyDTO
    {
        public string Salt { get; set; }
        public string SaltTwo { get; set; }
        public string SaltThree { get; set; }
        public string Hash { get; set; }
        public string HashTwo { get; set; }
        public string HashThree { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityQuestionTwo { get; set; }
        public string SecurityQuestionThree { get; set; }
        public string SecurityAnswer { get; set; }
        public string SecurityAnswerTwo { get; set; }
        public string SecurityAnswerThree { get; set; }
    }
}