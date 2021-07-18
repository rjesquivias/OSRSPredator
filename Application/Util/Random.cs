namespace Application.Util
{
    public class Random
    {
        private static readonly System.Random random = new System.Random(); 
        private static readonly object syncLock = new object(); 
        public static int RandomNumber(int min, int max)
        {
            lock(syncLock) { // synchronize
                return random.Next(min, max);
            }
        }
    }
}