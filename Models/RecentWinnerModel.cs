using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models
{
    public class RecentWinnerModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string FullName { get; set; } = "N/A";
        public string ProfileImage { get; set; } = "N/A";
        public string Pronouns { get; set; } = "N/A";
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public string Style { get; set; } = "N/A";
        public string MainCenter { get; set; } = "N/A";
        public string Average { get; set; } = "N/A";
        public string Earnings { get; set; } = "N/A";
        public string HighGame { get; set; } = "N/A";
        public string HighSeries { get; set; } = "N/A";
        public bool IsDeleted { get; set; }
        public string Date { get; set; }
        public RecentWinnerModel()
        {
        }
    }
}