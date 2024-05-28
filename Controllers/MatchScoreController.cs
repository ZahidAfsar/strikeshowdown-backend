using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Services;

namespace strikeshowdown_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchScoreController : ControllerBase
    {
        private readonly MatchScoresService _data;
        public MatchScoreController(MatchScoresService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddMatchScore")]
        public bool AddMatchScore(MatchScoresModel match){
            return _data.AddMatchScore(match);
        }
    }
}