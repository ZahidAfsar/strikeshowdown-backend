using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace strikeshowdown_backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPpl = table.Column<int>(type: "int", nullable: true),
                    CurrentPpl = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wins = table.Column<int>(type: "int", nullable: true),
                    Average = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Streak = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityQuestionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityQuestionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecuritySalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecuritySaltTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecuritySaltThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityHashTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityHashThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pronouns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Average = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Earnings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighGame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighSeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Streak = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
