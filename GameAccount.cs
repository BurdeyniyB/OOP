using System;

namespace MyApp
{
    abstract class GameAccount
    {
        public string UserName { get; private set; }
        public string AccountType { get; protected set; }
        private decimal currentRating = 25.0M;
        private int GamesCount { get; set; } = 0;

        public decimal CurrentRating
        {
            get => currentRating;
            protected set
            {
                currentRating = value < 1.0M ? 1.0M : value;
            }
        }

        public GameAccount(string userName, string accountType)
        {
            UserName = userName;
            AccountType = accountType;
        }

        protected abstract decimal CalculatePoints(decimal rating, bool isWin);

        public void PlayGame(int id, string opponentName, decimal rating, bool isWin)
        {
            if (rating < 0)
                throw new ArgumentException("Rating cannot be negative");

            decimal points = CalculatePoints(rating, isWin);
            CurrentRating += points;
            GamesCount++;
            string result = isWin ? "Win" : "Loss";
            Console.WriteLine($"{result}: {UserName} ({AccountType})! Opponent: {opponentName}, Game Rating: {rating}, Points: {points}");
        }

        public void GetStats()
        {
            Console.WriteLine($"Statistics for {UserName} ({AccountType}):");
            Console.WriteLine($"Rating: {CurrentRating}, Number of Games: {GamesCount}");
        }

        public void ShowPlayerInfo()
        {
            Console.WriteLine($"Player Name: {UserName}, Account Type: {AccountType}, Rating: {CurrentRating}, Number of Games: {GamesCount}");
        }
    }

    class StandardAccount : GameAccount
    {
        public StandardAccount(string userName) : base(userName, "Standard") { }

        protected override decimal CalculatePoints(decimal rating, bool isWin)
        {
            return isWin ? rating : -rating;
        }
    }

    class ReducedLossAccount : GameAccount
    {
        public ReducedLossAccount(string userName) : base(userName, "Reduced Loss") { }

        protected override decimal CalculatePoints(decimal rating, bool isWin)
        {
            return isWin ? rating : -rating / 2;
        }
    }

    class StreakBonusAccount : GameAccount
    {
        private int winStreak = 0;

        public StreakBonusAccount(string userName) : base(userName, "Streak Bonus") { }

        protected override decimal CalculatePoints(decimal rating, bool isWin)
        {
            if (isWin)
            {
                winStreak++;
                return rating + (winStreak >= 3 ? 5.0M : 0);
            }
            else
            {
                winStreak = 0;
                return -rating;
            }
        }
    }
}
