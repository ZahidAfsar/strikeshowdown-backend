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
        public UserModel GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
        }

        public UserModel GetUserByID(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        public MatchItemModel GetMatchItemModel(int ID)
        {
            return _context.MatchInfo.SingleOrDefault(match => match.ID == ID);
        }
        public bool CreateMatch(CreateMatchItemDTO MatchItem, string Publisher)
        {
            var newMatch = new MatchItemModel();
            var foundUser = GetUserByUsernameOrEmail(Publisher);
            newMatch.UserID = foundUser.ID;
            newMatch.Publisher = foundUser.Username;
            newMatch.Title = MatchItem.Title;
            newMatch.IsVisible = MatchItem.IsVisible;
            newMatch.State = MatchItem.State;
            newMatch.Locations = MatchItem.Locations;
            newMatch.Date = MatchItem.Date;
            newMatch.Time = MatchItem.Time;
            newMatch.MaxPpl = MatchItem.MaxPpl;
            newMatch.CurrentPpl = MatchItem.CurrentPpl;
            newMatch.MatchUsersIDs = foundUser.ID + "-";
            newMatch.Description = MatchItem.Description;
            newMatch.IsFinished = MatchItem.IsFinished;
            newMatch.Image = foundUser.ProfileImage;
            newMatch.Wins = foundUser.Wins;
            newMatch.Average = foundUser.Average;
            newMatch.Style = foundUser.Style;
            newMatch.Streak = foundUser.Streak;
            newMatch.ChallengeLocation = "";

            _context.Add(newMatch);
            return _context.SaveChanges() != 0;
        }
        public bool UpdateMatchItem(MatchItemModel match)
        {
            _context.Update<MatchItemModel>(match);
            return _context.SaveChanges() != 0;
        }
        public bool DeleteMatch(MatchItemModel match)
        {
            match.IsFinished = true;
            _context.Update<MatchItemModel>(match);
            return _context.SaveChanges() != 0;
        }
        public IEnumerable<MatchItemModel> GetAllMatchesByUserID(int userID)
        {
            return _context.MatchInfo.Where(item => item.UserID == userID);
        }
        public IEnumerable<MatchItemModel> GetAllMatchesByStyle(string style)
        {
            return _context.MatchInfo.Where(item => item.Style == style);
        }
        public IEnumerable<MatchItemModel> GetAllMatchesByAvg(string avg)
        {
            return _context.MatchInfo.Where(item => item.Average == avg);
        }
        public IEnumerable<MatchItemModel> GetAllMatchesByUsername(string username)
        {
            return _context.MatchInfo.Where(item => item.Publisher == username);
        }
        public IEnumerable<MatchItemModel> GetAllPublicMatchItems()
        {
            return _context.MatchInfo.Where(item => item.IsVisible == true);
        }
        public IEnumerable<MatchItemModel> GetPublicMatchesByState(string state)
        {
            return _context.MatchInfo.Where(item => item.State == state && item.IsFinished == false);
        }
        public bool AddUserToMatch(int userID, MatchItemModel match)
        {
            if (GetUserByID(userID) != null && match.CurrentPpl < match.MaxPpl)
            {
                match.MatchUsersIDs += userID.ToString() + "-";
                match.CurrentPpl++;
                _context.Update<MatchItemModel>(match);
            }

            return _context.SaveChanges() != 0;
        }

        public bool RemoveUserFromMatch(int userID, MatchItemModel match){
            if (GetUserByID(userID) != null)
            {
                List<string> matchUsers = match.MatchUsersIDs.Split('-').ToList();
                List<string> newMatchUsers = new List<string>();

                foreach (string id in matchUsers){
                    if(Int32.Parse(id) == userID){
                        newMatchUsers.Add(id);
                    }
                }
                foreach(string id in newMatchUsers){
                    matchUsers.Remove(id);
                }
                match.CurrentPpl--;
                match.MatchUsersIDs = string.Join('-', matchUsers);
                _context.Update<MatchItemModel>(match);
            }

            return _context.SaveChanges() != 0;
        }
        public int GetRecentMatchIDByUserID(int id){
            List<MatchItemModel> matchList = GetAllMatchesByUserID(id).ToList();

            if(matchList.Count != 0){
                return matchList[matchList.Count - 1].ID;
            }

            return 0;
        }
    }
}