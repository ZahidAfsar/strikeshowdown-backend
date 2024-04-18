using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models
{
    public class MatchItemModel
    {
        public int ID { get; set; }
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
        public string? Publisher { get; set; }
        public string? Image { get; set; }
        public int? Wins { get; set; }
        public string? Average { get; set; }
        public string? Style { get; set; }
        public int? Streak { get; set; }

        public MatchItemModel()
        {
            
        }
    }
}