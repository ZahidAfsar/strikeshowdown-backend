using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using strikeshowdown_backend.Models;

namespace strikeshowdown_backend.Services.Context
{
    public class DataContext : DbContext
    {
       public DbSet<UserModel> UserInfo { get; set; }
       public DbSet<MatchItemModel> MatchInfo { get; set; } 
       public DbSet<RecentWinnerModel> RecentWinnerInfo { get; set; }

       public DataContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}