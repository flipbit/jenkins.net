using Hudson.Domain;

namespace Hudson.Factories
{
    internal class JobFactory
    {
        public static Job Create()
        {
            return Create(1);
        }

        public static Job Create(int seed)
        {
            return new Job
                       {
                           LastBuild = BuildFactory.Create(seed),
                           LastStableBuild = BuildFactory.Create(seed)
                       };
        }
    }
}
