using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
namespace Squiddler.Server
{
    public class Game
    {
        public readonly Guid GUID = Guid.NewGuid();
        private readonly List<Player> Players = new List<Player>();
        public int PlayerCount => Players.Count;
        public readonly Deck _Deck;
        private readonly DiscardPile Discard = new DiscardPile();
        public bool Started { get; private set; }
        public bool Ended { get; private set; }
        private GameDictionary _Dictionary { get; } = new GameDictionary();
        public int ActivePlayer { get; private set; } = 0;
        public int StartingPlayer { get; private set; } = 0;
        public int RoundNumber { get; private set; } = 0;
        public readonly int StartingRound = 3;
        public readonly int FinalRound = 10;
        private static List<Game> Active { get; } = new List<Game>();
        /// <summary>
        /// Searches active games for one matching the given GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if guid not found</exception>
        public static Game GetGame(Guid guid)
        {
            try
            {
                return Active.First(game => game.GUID == guid);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("GUID not found", nameof(guid));
            }
        }
        public static Game GetGame(string guid)
        {
            try
            {
                return GetGame(Guid.Parse(guid));
            }
            catch (FormatException)
            {
                throw new ArgumentException("GUID not found", nameof(guid));
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }
        public Player GetPlayer(Guid guid)
        {
            try
            {
                return Players.First(p => p.GUID == guid);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(nameof(guid), "GUID not found");
            }
        }
        public Game(bool register = true, string? dictionary=null)
        {
            if (register)
                Active.Add(this);
            if (dictionary != null)
                _Dictionary.Load(dictionary);
            else
                _Dictionary.Load("./assets/word-lists/scrabble.txt");
            _Deck = NewDefaultDeck();
        }
        public void Close()
        {
            Active.Remove(this);
        }
        //first call starts the game
        public void NextRound()
        {
            if (Ended)
            {
                throw new InvalidOperationException("Game has already ended");
            }
            if (!Started)
            {
                StartGame();
                SetupRound();
            }
            else if (RoundNumber == FinalRound)
            {
                EndGame();
            }
            else
            {
                RoundNumber++;
                StartingPlayer = NextPlayer(StartingPlayer);
                SetupRound();
            }
        }
        private void StartGame()
        {
            Started = true;
            RoundNumber = StartingRound;
            var rnd = new Random();
            StartingPlayer = rnd.Next(0, PlayerCount);
        }
        private void EndGame()
        {
            Ended = true;
            _Deck.Clear();
            Discard.Clear();
        }
        private void SetupRound()
        {
            ActivePlayer = StartingPlayer;
            _Deck.Reset();
            _Deck.Shuffle();
            Discard.Clear();
            foreach (var player in Players)
            {
                player._Hand.Clear();
                player._Hand.DrawFrom(_Deck, RoundNumber);
            }
            var firstDiscard = _Deck.DrawOne();
            Discard.Push(firstDiscard);
        }
        private int NextPlayer(int player)
        {
            return (player + 1) % Players.Count;
        }
        public static Deck NewDefaultDeck()
        {
            var builder = new Deck.DeckBuilder();
            builder.Add(2, 10, "A");
            builder.Add(8, 2, "B");
            builder.Add(8, 2, "C");
            builder.Add(5, 4, "D");
            builder.Add(2, 12, "E");
            builder.Add(6, 2, "F");
            builder.Add(6, 4, "G");
            builder.Add(7, 2, "H");
            builder.Add(2, 8, "I");
            builder.Add(13, 2, "J");
            builder.Add(8, 2, "K");
            builder.Add(3, 4, "L");
            builder.Add(5, 2, "M");
            builder.Add(5, 6, "N");
            builder.Add(2, 8, "O");
            builder.Add(6, 2, "P");
            builder.Add(15, 2, "Q");
            builder.Add(5, 6, "R");
            builder.Add(3, 4, "S");
            builder.Add(3, 6, "T");
            builder.Add(4, 6, "U");
            builder.Add(11, 2, "V");
            builder.Add(10, 2, "W");
            builder.Add(12, 2, "X");
            builder.Add(4, 4, "Y");
            builder.Add(14, 2, "Z");
            builder.Add(9, 2, "QU");
            builder.Add(7, 2, "IN");
            builder.Add(7, 2, "ER");
            builder.Add(10, 2, "CL");
            builder.Add(9, 2, "TH");
            return builder.MakeDeck();
        }
    }
}