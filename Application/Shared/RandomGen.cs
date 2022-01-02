namespace Application.Shared
{
    static class RandomGen
    {
        private static Random gen = new Random();
        public static List<int> GenerateRandomUniqueIntList(int requestedIntTotal, int upperBound)
        {
            if (requestedIntTotal > upperBound - 1) requestedIntTotal = upperBound - 1;
            List<int> intList = new List<int>();
            for (int i = 0; i < requestedIntTotal; i++)
            {
                int num = gen.Next(0, upperBound);
                if (!intList.Contains(num))
                    intList.Add(num);
            }

            return intList;
        }
    }
}
