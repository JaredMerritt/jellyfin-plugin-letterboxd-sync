using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetterboxdSync
{
    internal static class RateLimiter
    {
        private static readonly SemaphoreSlim Gate = new SemaphoreSlim(1, 1);
        private static DateTime _lastRequestUtc = DateTime.MinValue;

        public static async Task WaitAsync()
        {
            var cfg = Plugin.Instance?.Configuration;
            int rpm = cfg != null ? cfg.RequestsPerMinute : 0;
            if (rpm <= 0)
                return; // disabled

            // Minimum interval between requests in milliseconds
            int minIntervalMs = (int)Math.Ceiling(60000.0 / Math.Max(1, rpm));

            await Gate.WaitAsync().ConfigureAwait(false);
            try
            {
                var now = DateTime.UtcNow;
                var earliest = _lastRequestUtc.AddMilliseconds(minIntervalMs);
                if (earliest > now)
                {
                    var delay = earliest - now;
                    if (delay.TotalMilliseconds > 0)
                        await Task.Delay(delay).ConfigureAwait(false);
                }

                _lastRequestUtc = DateTime.UtcNow;
            }
            finally
            {
                Gate.Release();
            }
        }
    }
}
