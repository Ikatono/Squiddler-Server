namespace Squiddler
{
    public class Scorecard
    {
        public int StartingRound { get; set; }
        public List<Player> Players = new List<Player>();
        public Scorecard(IEnumerable<string> players, int startingRound = 3)
        {
            Players.AddRange(players.Select(p => new Player(p)));
            StartingRound = startingRound;
        }
        public Scorecard(Scorecard scorecard)
        {
            StartingRound = scorecard.StartingRound;
            Players = scorecard.Players.Select(p => new Player(p)).ToList();
        }
        public class Player
        {
            public string Name { get; set; }
            public List<int> Scores = new List<int>();
            public Player(string name)
            {
                Name = name;
            }
            public Player(Player player)
            {
                Name = player.Name;
                Scores = player.Scores.ToList();
            }
            public void AddScore(int score)
            {
                Scores.Add(score);
            }
        }
    }
}