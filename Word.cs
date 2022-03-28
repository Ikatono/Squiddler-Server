using System.Collections;
using System.Collections.ObjectModel;
namespace Squiddler
{
    public class Word : IEnumerable<Card>
    {
        private readonly ReadOnlyCollection<Card> Cards;
        public int Length => AsString().Count();
        public string AsString()
        {
            return string.Concat(Cards.SelectMany(card => card.Text));
        }
        public Word(IEnumerable<Card> cards)
        {
            Cards = cards.ToList().AsReadOnly();
        }
        public int Score()
        {
            return Cards.Select(c => c.Score).Sum();
        }
        public ReadOnlyCollection<Card> GetView()
        {
            return Cards;
        }
        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Cards.GetEnumerator();
        }
    }
}