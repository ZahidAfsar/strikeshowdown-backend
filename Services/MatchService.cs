using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models;
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

        // public UserModel GetUserByUsername(string username)
        // {
        //     return _context.UserInfo.SingleOrDefault(user => user.Username == username || user.Email == username);
        // }

        public UserModel GetUserByUsername(string username) {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username || user.Email == username);
        }

        public bool CreateMatch(MatchItemModel MatchItem, string Publisher){

            var newMatch = new MatchItemModel();

            var foundUser = GetUserByUsername(Publisher);

            newMatch.ID = MatchItem.ID;
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
            newMatch.Image = MatchItem.Image;
            newMatch.Wins = foundUser.Wins;
            newMatch.Average = foundUser.Average;
            newMatch.Style = foundUser.Style;
            newMatch.Streak = foundUser.Streak;

            _context.Add(newMatch);

            return _context.SaveChanges() != 0;
        }
    }
}