using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using strikeshowdown_backend.Models;
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
            } else {
                
            }

            return _context.SaveChanges() != 0;
        }

    }
}