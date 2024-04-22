using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services;

namespace strikeshowdown_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly MatchService _data;

        public MatchController(MatchService data)
        {
            _data = data;
        }
        [HttpPost]
        [Route("AddMatch/{publisher}")]
        public bool CreateMatch(CreateMatchItemDTO match, string publisher){
            return _data.CreateMatch(match, publisher);
        }

        [HttpGet]
        [Route("GetPublicMatches")]
        public IEnumerable<MatchItemModel> GetAllPublicMatches(){
            return _data.GetAllPublicMatchItems();
        }
    }
}