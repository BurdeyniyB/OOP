using System;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount player1 = new GameAccount("Player1");
            GameAccount player2 = new GameAccount("Player2");

            player1.WinGame("Player2", 10);
            player2.LoseGame("Player1", 10);

            player1.LoseGame("Player2", 5);
            player2.WinGame("Player1", 5);

            player1.GetStats();
            player2.GetStats();
        }
    }
}
