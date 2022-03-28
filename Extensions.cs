namespace Squiddler
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var rnd = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                var ind = rnd.Next(i + 1);
                (list[i], list[ind]) = (list[ind], list[i]);
            }
        }
    }
}
