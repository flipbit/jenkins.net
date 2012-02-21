using System;
using System.Threading;

namespace Hudson.Services
{
    public static class Retry
    {
        public static T This<T>(Func<T> action, int numRetries, int retryTimeout)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            do
            {
                try
                {
                    return action.Invoke();
                }
                catch
                {
                    if (numRetries <= 0) throw;  // improved to avoid silent failure

                    Thread.Sleep(retryTimeout);
                }
            }
            while (numRetries-- > 0);

            return default(T);
        }
    }
}
