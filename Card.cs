using System.Diagnostics.CodeAnalysis;

namespace Squiddler
{
    public readonly struct Card : IEquatable<Card>, IComparable<Card>
    {
        public readonly int Score;
        public readonly int Frequency;
        public readonly string Text;
        public readonly string? ImageName;
        public Card(int score, int frequency, string text, string? imageName=null)
        {
            Score = score;
            Frequency = frequency;
            Text = text;
            ImageName = imageName;
        }
        public int CompareTo(Card other)
        {
            var val = Text.CompareTo(other.Text);
            if (val == 0)
                val = Score.CompareTo(other.Score);
            if (val == 0)
                val = Frequency.CompareTo(other.Frequency);
            return val;
        }
        public bool Equals(Card other)
        {
            return Score == other.Score
                && Frequency == other.Frequency
                && Text == other.Text;
        }
        public static bool operator ==(Card first, Card second)
        {
            return first.Equals(second);
        }
        public static bool operator !=(Card first, Card second)
        {
            return !first.Equals(second);
        }
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Card other)
                return this == other;
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Text, Score, Frequency);
        }
    }
}