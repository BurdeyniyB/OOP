using System;
using System.Collections.Generic;

namespace MyApp{
    class GameAccount
    {
        public string UserName { get; private set; }
        public decimal CurrentRating { get; private set; }
        public int GamesCount { get; private set; }
        private List<Game> gameHistory;

        public GameAccount(string userName)
        {
            this.UserName = userName;
            this.CurrentRating = 1.0M;
            this.GamesCount = 0;
            this.gameHistory = new List<Game>();
        }

        public void WinGame(string opponentName, decimal rating)
        {
            if (rating < 0)
                throw new ArgumentException("Рейтинг не може бути від'ємним");

            this.CurrentRating += rating;
            this.GamesCount++;
            this.gameHistory.Add(new Game(opponentName, true, rating));
            Console.WriteLine($"Перемога! Опонент: {opponentName}, Рейтинг гри: {rating}");
        }

        public void LoseGame(string opponentName, decimal rating)
        {
            if (rating < 0)
                throw new ArgumentException("Рейтинг не може бути від'ємним");

            this.CurrentRating = Math.Max(1.0M, this.CurrentRating - rating); // Рейтинг не може бути менше 1
            this.GamesCount++;
            this.gameHistory.Add(new Game(opponentName, false, rating));
            Console.WriteLine($"Поразка! Опонент: {opponentName}, Рейтинг гри: {rating}");
        }

        public void GetStats()
        {
            Console.WriteLine($"Статистика для {UserName}:");
            Console.WriteLine("Індекс | Опонент | Результат | Рейтинг гри");
            foreach (var game in gameHistory)
            {
                string result = game.IsWin ? "Перемога" : "Поразка";
                Console.WriteLine($"{game.GameId} | {game.OpponentName} | {result} | {game.Rating}");
            }
        }
    }
}