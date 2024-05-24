using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models.DTO;
namespace strikeshowdown_backend.Models
{
    public class MatchItemModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? Title { get; set; }
        public bool? IsVisible { get; set; }
        public string? State { get; set; }
        public string? Locations { get; set; }
        public string? ChallengeLocation { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int? MaxPpl { get; set; }
        public int? CurrentPpl { get; set; }
        public string MatchUsersIDs { get; set; }
        public string? Description { get; set; }
        public bool? IsFinished { get; set; }
        public string? Publisher { get; set; }
        public string? Image { get; set; }
        public int? Wins { get; set; }
        public string? Average { get; set; }
        public string? Style { get; set; }
        public int? Streak { get; set; }
        // public List<UseridDTO> InvitedUsers { get; set; } = new List<UseridDTO>();
        public MatchItemModel()
        {
        }
    }
}