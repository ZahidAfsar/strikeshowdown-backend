using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace strikeshowdown_backend.Models.DTO
{
    public class UserWithoutSaltHashDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityQuestionTwo { get; set; }
        public string? SecurityQuestionThree { get; set; }
        public string FullName { get; set; } = "N/A";
        public string ProfileImage { get; set; } = "N/A";
        public string Pronouns { get; set; } = "N/A";
        public int Wins { get; set; } = 0;
        public int Loses { get; set; } = 0;
        public string Style { get; set; } = "N/A";
        public string MainCenter { get; set; } = "N/A";
        public string Average { get; set; } = "N/A";
        public string Earnings { get; set; } = "N/A";
        public string HighGame { get; set; } = "N/A";
        public string HighSeries { get; set; } = "N/A";   
        public int Streak { get; set; }
    }
}