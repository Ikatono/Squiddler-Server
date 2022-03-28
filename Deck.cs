using System.Collections.ObjectModel;

namespace Squiddler
{
    public class Deck
    {
        //[0] is the top of the deck
        private List<Card> Items;
        //keep a copy of cards to reset
        private ReadOnlyCollection<Card> initializer;
        public int Count => Items.Count;
        public Deck(IEnumerable<Card> cards)
        {
            initializer = new List<Card>(cards).AsReadOnly();
            Items = new List<Card>(initializer);
        }
        public static Deck CreateEmpty()
        {
            return new Deck(new Card[]{});
        }
        //DOESN'T SHUFFLE!
        public void Reset()
        {
            Items = new List<Card>(initializer);
        }
        public void Shuffle()
        {
            Items.Shuffle();
        }
        public void Sort(IComparer<Card> comparer)
        {
            Items.Sort(comparer);
        }
        public Card Peek()
        {
            //change return type and remove excaption?
            try
            {
                return Items.First();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Cannot peek an empty deck");
            }
        }
        public Card[] Draw(int cards)
        {
            var arr = Items.Take(cards).ToArray();
            Items.RemoveRange(0, cards);
            return arr;
        }
        public Card DrawOne()
        {
            var card = Items[0];
            Items.RemoveAt(0);
            return card;
        }
        public void OnTop(Card card)
        {
            Items.Insert(0, card);
        }
        public void OnTop(IEnumerable<Card> cards)
        {
            Items.InsertRange(0, cards);
        }
        public void OnBottom(Card card)
        {
            Items.Add(card);
        }
        public void OnBottom(IEnumerable<Card> cards)
        {
            Items.AddRange(cards);
        }
        public void PlaceAt(int index, Card card)
        {
            Items.Insert(index, card);
        }
        public void InsertRange(int index, IEnumerable<Card> cards)
        {
            Items.InsertRange(index, cards);
        }
        public void Clear()
        {
            Items.Clear();
        }
        public ReadOnlyCollection<Card> GetView()
        {
            return Items.AsReadOnly();
        }
        public class DeckBuilder
        {
            private readonly List<Card> Cards = new List<Card>();
            public DeckBuilder()
            {

            }
            public void Add(int score, int frequency, string text, string? filepath=null)
            {
                Cards.AddRange(Enumerable.Repeat(new Card(score, frequency, text, filepath), frequency));
            }
            public Deck MakeDeck()
            {
                return new Deck(Cards.AsReadOnly());
            }
        }
    }
}