namespace MyApp{
    class Game
    {
        private int idCounter = 0;
        public int GameId { get; private set; }
        public string OpponentName { get; private set; }
        public bool IsWin { get; private set; }
        public decimal Rating { get; private set; }

        public Game(string opponentName, bool isWin, decimal rating)
        {
            this.GameId = ++idCounter;
            this.OpponentName = opponentName;
            this.IsWin = isWin;
            this.Rating = rating;
        }
    }
}