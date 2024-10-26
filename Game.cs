namespace MyApp
{
    public class GameInfo
    {
        public int GameId { get; private set; }
        public string Player1 { get; private set; }
        public string Player2 { get; private set; }
        public decimal Rating { get; private set; }

        public GameInfo(int gameId, string player1, string player2, decimal rating)
        {
            GameId = gameId;
            Player1 = player1;
            Player2 = player2;
            Rating = rating;
        }

        public string GetResult()
        {
            return $"GameInfo ID: {GameId}, Winner: {Player1}, loser: {Player2}, Rating: {Rating}";
        }
    }
}