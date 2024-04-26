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
        public MatchItemModel GetMatchItemModel(int ID, string publisher)
        {
            return _context.MatchInfo.SingleOrDefault(match => match.ID == ID && match.Publisher == publisher);
        }
        public bool CreateMatch(CreateMatchItemDTO MatchItem, string Publisher)
        {
            var newMatch = new MatchItemModel();
            var foundUser = GetUserByUsernameOrEmail(Publisher);
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

        if (MatchItem.InvitedUserIds != null && MatchItem.InvitedUserIds.Any())
{
    foreach (var userId in MatchItem.InvitedUserIds)
    {
        var invitedUser = GetUserByUsernameOrEmail(userId);
        if (invitedUser != null)
        {
            var invitedUserDTO = new UseridDTO
            {
                UserId = invitedUser.ID
            };
            newMatch.InvitedUsers.Add(invitedUserDTO);
        }
    }
}
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
            return _context.MatchInfo.Where(item => item.State == state);
        }
    }
}