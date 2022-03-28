namespace Squiddler
{
    public static class Extensions
    {
        //TODO change to act in interface (IList?)
        public static void Shuffle<T>(this List<T> list)
        {
            var rnd = new Random();
            for (int i = list.Count - 1; i > 0; i++)
            {
                var ind = rnd.Next(i + 1);
                var temp = list[i];
                list[i] = list[ind];
                list[ind] = temp;
            }
        }
    }
}