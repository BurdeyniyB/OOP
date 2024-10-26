using System;
using System.Collections.Generic;

namespace MyApp
{
    public class PlayGame
    {
        public int gameId = 0;
        private GameManager gameManager = new GameManager();
        private List<GameAccount> players = new List<GameAccount>();

        public void Run()
        {
            string? choice;
            bool running = true;

            while (running)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Player");
                Console.WriteLine("2. Simulate GameInfo");
                Console.WriteLine("3. Show All Players' Statistics");
                Console.WriteLine("4. Show Player Information");
                Console.WriteLine("5. Show All Games");
                Console.WriteLine("6. Show Games for Specific Player");
                Console.WriteLine("7. Exit");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        SimulateGame();
                        break;
                    case "3":
                        ShowAllPlayerStats();
                        break;
                    case "4":
                        ShowPlayerInfo();
                        break;
                    case "5":
                        gameManager.ShowAllGames();
                        break;
                    case "6":
                        ShowGamesForPlayer();
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void AddPlayer()
        {
            Console.WriteLine("Enter player name:");
            string? userName = Console.ReadLine();

            Console.WriteLine("Select account type:");
            Console.WriteLine("1. Standard Account");
            Console.WriteLine("2. Reduced Loss Account");
            Console.WriteLine("3. Streak Bonus Account");

            string? accountTypeChoice = Console.ReadLine();

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(accountTypeChoice))
            {
                GameAccount player;

                switch (accountTypeChoice)
                {
                    case "1":
                        player = new StandardAccount(userName);
                        break;
                    case "2":
                        player = new ReducedLossAccount(userName);
                        break;
                    case "3":
                        player = new StreakBonusAccount(userName);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, defaulting to Standard Account.");
                        player = new StandardAccount(userName);
                        break;
                }

                players.Add(player);
                Console.WriteLine($"Player {userName} added with {player.AccountType}.");
            }
            else
            {
                Console.WriteLine("Player name cannot be empty.");
            }
        }


        private void SimulateGame()
        {
            var player1 = SelectPlayer();
            var player2 = SelectPlayer();

            if (player1 != null && player2 != null && player1 != player2)
            {
                Random random = new Random();
                decimal rating = random.Next(1, 6);

                gameId++;
                GameAccount winner, loser;
                DetermineWinnerAndLoser(player1, player2, out winner, out loser);

                winner.PlayGame(gameId, loser.UserName, rating, true);
                loser.PlayGame(gameId, winner.UserName, rating, false);

                gameManager.AddGame(new GameInfo(gameId, winner.UserName, loser.UserName, rating));
            }
            else
            {
                Console.WriteLine("You must select two different players.");
            }
        }

        private void DetermineWinnerAndLoser(GameAccount player1, GameAccount player2, out GameAccount winner, out GameAccount loser)
        {
            Random random = new Random();
            int result = random.Next(0, 2);

            if (result == 0)
            {
                winner = player1;
                loser = player2;
            }
            else
            {
                winner = player2;
                loser = player1;
            }
        }

        private GameAccount? SelectPlayer()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].UserName} ({players[i].GetType().Name})");
            }

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= players.Count)
            {
                return players[choice - 1];
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }
        }

        private void ShowAllPlayerStats()
        {
            foreach (var player in players)
            {
                player.GetStats();
            }
        }


        private void ShowPlayerInfo()
        {
            GameAccount? player = SelectPlayer();
            if (player != null)
            {
                player.ShowPlayerInfo();
            }
            else
            {
                Console.WriteLine("User not found!");
            }
        }

        private void ShowGamesForPlayer()
        {
            Console.WriteLine("Select a player to view their games:");
            var player = SelectPlayer();

            if (player != null)
            {
                gameManager.ShowGamesForPlayer(player.UserName);
            }
        }
    }
}
