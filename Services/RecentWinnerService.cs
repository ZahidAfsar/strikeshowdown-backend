using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class RecentWinnerService
    {
        private readonly DataContext _context;
        public RecentWinnerService(DataContext context)
        {
            _context = context;
        }

        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public IEnumerable<RecentWinnerModel> GetAllRecentWinners()
        {
            return _context.RecentWinnerInfo;
        }

        public IEnumerable<RecentWinnerModel> GetRecentWinnerModelsByState(string state)
        {
            return _context.RecentWinnerInfo.Where(winner => winner.Location == state);
        }

        public bool UpdatePastRecentWinners(string username)
        {
            List<RecentWinnerModel> winnerList = GetAllRecentWinners().ToList();

            foreach (RecentWinnerModel winner in winnerList)
            {
                if (winner.Username == username)
                {
                    winner.IsDeleted = true;
                }
                _context.Update<RecentWinnerModel>(winner);
            }
            return _context.SaveChanges() != 0;
        }

        public bool AddRecentWinner(string username)
        {
            DateTime currentDate = DateTime.Now;
            UpdatePastRecentWinners(username);
            UserModel foundUser = GetUserByUsername(username);
            RecentWinnerModel newWinner = new RecentWinnerModel();
            newWinner.Username = foundUser.Username;
            newWinner.Email = foundUser.Email;
            newWinner.Location = foundUser.Location;
            newWinner.FullName = foundUser.FullName;
            newWinner.ProfileImage = foundUser.ProfileImage;
            newWinner.Pronouns = foundUser.Pronouns;
            newWinner.Wins = foundUser.Wins;
            newWinner.Losses = foundUser.Losses;
            newWinner.Style = foundUser.Style;
            newWinner.MainCenter = foundUser.MainCenter;
            newWinner.Average = foundUser.Average;
            newWinner.Earnings = foundUser.Earnings;
            newWinner.HighGame = foundUser.HighGame;
            newWinner.HighSeries = foundUser.HighSeries;
            newWinner.IsDeleted = false;
            newWinner.Date = currentDate.ToString();
            _context.Add(newWinner);
            return _context.SaveChanges() != 0;
        }
    }
}