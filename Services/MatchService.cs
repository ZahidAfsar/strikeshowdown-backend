using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class MatchService
    {
        private readonly DataContext _context;
        
        public MatchService(DataContext context)
        {
            _context = context;
        }

        public UserModel GetUserByUsernameOrEmail(string usernameOrEmail) {
            return _context.UserInfo.SingleOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
        }

        public MatchItemModel GetMatchItemModel(int ID, string publisher){
            return _context.MatchInfo.SingleOrDefault(match => match.ID == ID && match.Publisher == publisher);
        }

        public bool CreateMatch(CreateMatchItemDTO MatchItem, string Publisher){

            var newMatch = new MatchItemModel();

            var foundUser = GetUserByUsernameOrEmail(Publisher);

            newMatch.ID = MatchItem.ID;
            newMatch.UserID = foundUser.ID;
            newMatch.Publisher = Publisher;
            newMatch.Title = MatchItem.Title;
            newMatch.IsVisible = MatchItem.IsVisible;
            newMatch.State = MatchItem.State;
            newMatch.Locations = MatchItem.Locations;
            newMatch.Date = MatchItem.Date;
            newMatch.Time = MatchItem.Time;
            newMatch.MaxPpl = MatchItem.MaxPpl;
            newMatch.CurrentPpl = MatchItem.CurrentPpl;
            newMatch.Description = MatchItem.Description;
            newMatch.IsFinished = MatchItem.IsFinished;
            newMatch.Image = foundUser.ProfileImage;
            newMatch.Wins = foundUser.Wins;
            newMatch.Average = foundUser.Average;
            newMatch.Style = foundUser.Style;
            newMatch.Streak = foundUser.Streak;

            _context.Add(newMatch);

            return _context.SaveChanges() != 0;
        }

        public bool UpdateMatchItem(MatchItemModel match){
            bool result = false;
            MatchItemModel foundMatch = GetMatchItemModel(match.ID, match.Publisher);
            UserModel foundUser = GetUserByUsernameOrEmail(match.Publisher);
            if (foundMatch != null){
                foundMatch.Title = match.Title;
                foundMatch.IsVisible = match.IsVisible;
                foundMatch.State = match.State;
                foundMatch.Locations = match.Locations;
                foundMatch.Date = match.Date;
                foundMatch.Time = match.Time;
                foundMatch.MaxPpl = match.MaxPpl;
                foundMatch.CurrentPpl = match.CurrentPpl;
                foundMatch.Description = match.Description;
                foundMatch.IsFinished = match.IsFinished;
                foundMatch.Publisher = foundUser.Username;
                foundMatch.Image = foundUser.ProfileImage;
                foundMatch.Wins = foundUser.Wins;
                foundMatch.Average = foundUser.Average;
                foundMatch.Style = foundUser.Style;
                foundMatch.Streak = foundUser.Streak;
                _context.Update<MatchItemModel>(foundMatch);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public IEnumerable<MatchItemModel> GetAllPublicMatchItems(){
            return _context.MatchInfo.Where(item => item.IsVisible == true);
        }

        public IEnumerable<MatchItemModel> GetAllMatchesByUserID(string username) {
            UserModel foundUser = GetUserByUsernameOrEmail(username);
            return _context.MatchInfo.Where(item => item.UserID == foundUser.ID);
        }
    }
}