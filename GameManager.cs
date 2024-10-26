using System;
using System.Collections.Generic;

namespace MyApp
{
    public class GameManager
    {
        private List<GameInfo> games = new List<GameInfo>();

        public void AddGame(GameInfo game)
        {
            games.Add(game);
        }

        public void ShowGamesForPlayer(string playerName)
        {
            foreach (var game in games)
            {
                if (game.Player1 == playerName)
                {
                    Console.WriteLine($"{game.Player1} vs {game.Player2} - Rating: {game.Rating} - Win!");
                }
                else if(game.Player2 == playerName)
                {
                    Console.WriteLine($"{game.Player2} vs {game.Player1} - Rating: {game.Rating} - Lose!");
                }
            }
        }

        public void ShowAllGames()
        {
            foreach (var game in games)
            {
                Console.WriteLine(game.GetResult());
            }
        }
    }

}









