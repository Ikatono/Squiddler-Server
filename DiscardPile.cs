using System.Collections.ObjectModel;
namespace Squiddler
{
    public class DiscardPile
    {
        //index 0 is the bottom of the discard
        private readonly List<Card> Cards = new List<Card>();
        public DiscardPile()
        {

        }
        public void Push(Card card)
        {
            Cards.Add(card);
        }
        public Card Pop()
        {
            Card card = Cards[Cards.Count];
            try
            {
                Cards.RemoveAt(Cards.Count);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
            return card;
        }
        public Card Peek()
        {
            return Cards[Cards.Count - 1];
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