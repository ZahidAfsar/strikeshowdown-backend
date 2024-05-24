using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strikeshowdown_backend.Models.DTO
{
    public class MessageDTO
    {
        public int UserID { get; set; }
        public string? Message { get; set; }
        public string? PublisherName { get; set; }
    }
}