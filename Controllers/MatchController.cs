using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services;

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

        [HttpGet]
        [Route("GetPublicMatchesByState/{state}")]
        public IEnumerable<MatchItemModel> GetPublicMatchesByState(string state){
            return _data.GetPublicMatchesByState(state);
        }

        [HttpGet]
        [Route("GetMatchesByID/{userID}")]
        public IEnumerable<MatchItemModel> GetAllMatchesByID(int userID){
            return _data.GetAllMatchesByUserID(userID);
        }
        
        [HttpGet]
        [Route("GetMatchByPostID/{id}")]
        public MatchItemModel GetMatchByPostID(int id){
            return _data.GetMatchItemModel(id);
        }
        
        [HttpPut]
        [Route("DeleteMatch")]
        public bool DeleteMatch(MatchItemModel match){
            return _data.DeleteMatch(match);
        }

        [HttpPut]
        [Route("UpdateMatch")]
        public bool Update(MatchItemModel match){
            return _data.UpdateMatchItem(match);
        }

        [HttpPut]
        [Route("AddUserToMatch/{userID}")]
        public bool AddUserToMatch(int userID, MatchItemModel match){
            return _data.AddUserToMatch(userID, match);
        }
    }
}