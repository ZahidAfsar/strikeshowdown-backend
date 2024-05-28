using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using strikeshowdown_backend.Models;
using strikeshowdown_backend.Models.DTO;
using strikeshowdown_backend.Services.Context;

namespace strikeshowdown_backend.Services
{
    public class MatchScoresService
    {
        private readonly DataContext _context;
        public MatchScoresService(DataContext context)
        {
            _context = context;
        }

        public MatchItemModel GetMatchItemModel(int ID)
        {
            return _context.MatchInfo.SingleOrDefault(match => match.ID == ID);
        }

        public IEnumerable<MatchScoresModel> GetMatchScores(int postID)
        {
            return _context.MatchScoresModels.Where(score => score.PostID == postID && score.IsValid == true);
        }

        public bool AddMatchScore(MatchScoresModel matchScore)
        {
            List<MatchScoresModel> matchScores = GetMatchScores(matchScore.PostID).ToList();
            if (matchScores.Count == 0)
            {
                _context.Add(matchScore);
            }
            else
            {
                MatchScoresModel score = matchScores[0];

                if (score.ScoreOne == matchScore.ScoreTwo && score.ScoreTwo == matchScore.ScoreOne && score.UserID != matchScore.UserID)
                {
                    _context.Add(matchScore);
                    VerifyMatchScores(matchScore, score);
                    MatchItemModel match = GetMatchItemModel(matchScore.PostID);
                    match.IsFinished = true;
                    _context.Update<MatchItemModel>(match);
                }
                else if (score.UserID == matchScore.UserID)
                {
                    score.IsValid = false;
                    _context.Update<MatchScoresModel>(score);
                    _context.Add(matchScore);
                }
                else
                {
                    score.IsValid = false;
                    _context.Update<MatchScoresModel>(score);
                    EnterScoreInputsNotification(score, matchScore, matchScore.PostID);
                    EnterScoreInputsNotification(matchScore, score, matchScore.PostID);

                    return false;
                }
            }

            return _context.SaveChanges() != 0;
        }

        public bool VerifyMatchScores(MatchScoresModel matchOne, MatchScoresModel matchTwo)
        {
            UserModel userOne = GetUserByID(matchOne.UserID);
            UserModel userTwo = GetUserByID(matchTwo.UserID);

            if (matchOne.ScoreOne > matchOne.ScoreTwo)
            {
                CreateWinnerNotification(userOne, userTwo, matchOne.PostID);
                CreateLoserNotification(userTwo, userOne, matchOne.PostID);
                return true;
            }
            else if (matchOne.ScoreTwo > matchOne.ScoreOne)
            {
                CreateWinnerNotification(userTwo, userOne, matchOne.PostID);
                CreateLoserNotification(userOne, userTwo, matchOne.PostID);
                return true;
            }
            else if (matchOne.ScoreOne == matchOne.ScoreTwo)
            {
                CreateTieNotification(userTwo, userOne, matchOne.PostID);
                CreateTieNotification(userOne, userTwo, matchOne.PostID);
            }

            return false;
        }

        public bool EnterScoreInputsNotification(MatchScoresModel matchOne, MatchScoresModel matchTwo, int postID)
        {
            UserModel userOne = GetUserByID(matchOne.UserID);
            UserModel userTwo = GetUserByID(matchTwo.UserID);

            MatchItemModel match = GetMatchItemModel(postID);

            NotificationModel newNoti = new NotificationModel();
            newNoti.SenderID = 0;
            newNoti.RecieverID = userOne.ID;
            newNoti.SenderUsername = "Strike Showdown";
            newNoti.RecieverUsername = userOne.Username;
            newNoti.PostID = postID;
            newNoti.Image = "/images/blankpfp.png";
            newNoti.Type = "Winner Challenge";
            newNoti.Content = "The scores in your 1v1 challenge with " + userTwo.Username + ", did not match up. Please re-enter scores again";
            newNoti.IsRead = false;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);

            return _context.SaveChanges() != 0;
        }

        public bool CreateWinnerNotification(UserModel userOne, UserModel userTwo, int postID)
        {
            NotificationModel newNoti = new NotificationModel();
            newNoti.SenderID = 0;
            newNoti.RecieverID = userOne.ID;
            newNoti.SenderUsername = "Strike Showdown";
            newNoti.RecieverUsername = userOne.Username;
            newNoti.PostID = postID;
            newNoti.Image = "/images/blankpfp.png";
            newNoti.Type = "Challenge Winner";
            newNoti.Content = "Congratulations you won a 1v1 challenge against " + userTwo.Username + " . You are a winner!";
            newNoti.IsRead = false;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);

            userOne.Wins++;
            userOne.Streak++;

            _context.Update<UserModel>(userOne);

            AddRecentWinner(userOne.Username);

            return _context.SaveChanges() != 0;
        }

        public bool CreateLoserNotification(UserModel userOne, UserModel userTwo, int postID)
        {
            NotificationModel newNoti = new NotificationModel();
            newNoti.SenderID = 0;
            newNoti.RecieverID = userOne.ID;
            newNoti.SenderUsername = "Strike Showdown";
            newNoti.RecieverUsername = userOne.Username;
            newNoti.PostID = postID;
            newNoti.Image = "/images/blankpfp.png";
            newNoti.Type = "Challenge Winner";
            newNoti.Content = "Sorry, but you lost a 1v1 challenge against " + userTwo.Username + " . You are a loser!";
            newNoti.IsRead = false;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);

            userOne.Losses++;
            userOne.Streak = 0;

            _context.Update<UserModel>(userOne);

            return _context.SaveChanges() != 0;
        }

        public bool CreateTieNotification(UserModel userOne, UserModel userTwo, int postID)
        {
            NotificationModel newNoti = new NotificationModel();
            newNoti.SenderID = 0;
            newNoti.RecieverID = userOne.ID;
            newNoti.SenderUsername = "Strike Showdown";
            newNoti.RecieverUsername = userOne.Username;
            newNoti.PostID = postID;
            newNoti.Image = "/images/blankpfp.png";
            newNoti.Type = "Challenge Winner";
            newNoti.Content = "Sorry, but you tied with " + userTwo.Username + " in a 1v1 challenge. There is no winner";
            newNoti.IsRead = false;
            newNoti.IsDeleted = false;

            _context.Add(newNoti);

            return _context.SaveChanges() != 0;
        }

        public UserModel GetUserByID(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }

        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public IEnumerable<RecentWinnerModel> GetAllRecentWinners()
        {
            return _context.RecentWinnerInfo;
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
            newWinner.UserID = foundUser.ID;
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
            newWinner.Streak = foundUser.Streak;
            newWinner.IsDeleted = false;
            newWinner.Date = currentDate.ToString();
            _context.Add(newWinner);
            return _context.SaveChanges() != 0;
        }
    }
}