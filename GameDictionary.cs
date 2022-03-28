using System.Collections;

namespace Squiddler.Server
{
    public class GameDictionary : IReadOnlySet<string>
    {
        private readonly HashSet<string> Words = new HashSet<string>();

        public int Count => ((IReadOnlyCollection<string>)Words).Count;

        public GameDictionary()
        {

        }
        public void Load(string filename)
        {
            foreach (string word in File.ReadLines(filename))
            {
                Words.Add(FormatWord(word));
            }
        }
        public void Load(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                Words.Add(FormatWord(word));
            }
        }
        public bool Contains(string word)
        {
            word = FormatWord(word);
            return Words.Contains(word);
        }
        public bool Contains(Word word)
        {
            var word_string = word.AsString();
            word_string = FormatWord(word_string);
            return Contains(word_string);
        }
        private static string FormatWord(string word)
        {
            return word.Trim().ToUpper();
        }

        public bool IsProperSubsetOf(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).Overlaps(other);
        }

        public bool SetEquals(IEnumerable<string> other)
        {
            return ((IReadOnlySet<string>)Words).SetEquals(other);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)Words).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Words).GetEnumerator();
        }
    }
}