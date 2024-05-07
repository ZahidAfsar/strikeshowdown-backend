using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace strikeshowdown_backend.Models.DTO
{
    public class CreateMatchItemDTO
    {
        public string? Title { get; set; }
        public bool? IsVisible { get; set; }
        public string? State { get; set; }
        public string? Locations { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int? MaxPpl { get; set; }
        public int? CurrentPpl { get; set; }
        public string? Description { get; set; }
        public bool? IsFinished { get; set; }
        // public List<string>? InvitedUserIds { get; set; }
    }
}