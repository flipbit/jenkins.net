using Hudson.Domain;

namespace Hudson.Factories
{
    internal class BuildFactory
    {
        public static Build Create()
        {
            return Create(1);
        }

        public static Build Create(int seed)
        {
            return new Build
                       {
                           Number = seed
                       };
        }
    }
}
