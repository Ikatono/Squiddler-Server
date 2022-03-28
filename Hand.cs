using System.Collections.ObjectModel;
namespace Squiddler
{
    public class Hand
    {
        private readonly List<Card> Cards = new List<Card>();
        public Hand()
        {

        }
        public void Add(Card card)
        {
            Cards.Add(card);
        }
        public void AddRange(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);
        }
        public void Discard(DiscardPile discard, int index)
        {
            if (index < 0 || index >= Cards.Count)
                throw new ArgumentOutOfRangeException(nameof(index),
                index, "Index out of range");
            Card card = Cards[index];
            Cards.RemoveAt(index);
            discard.Push(card);
        }
        public void DrawFrom(Deck deck, int count)
        {
            if (count < 0)
                throw new ArgumentException("Cannot draw negative cards");
            if (count > deck.Count)
                throw new ArgumentException("Not enough cards in deck");
            var cards = deck.Draw(count);
            AddRange(cards);
        }
        public void Clear()
        {
            Cards.Clear();
        }
        public ReadOnlyCollection<Card> GetView()
        {
            return Cards.AsReadOnly();
        }
    }
}