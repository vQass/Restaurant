using System.Diagnostics;

namespace DiagnosticsHelpers
{
    public static class StopwatchHelper
    {
        private static List<string> _measurementsResults;
        private static Dictionary<string, Stopwatch> _stopwatches;
        static StopwatchHelper()
        {
            _measurementsResults = new List<string>();
            _stopwatches = new Dictionary<string, Stopwatch>();
        }

        public static void Start(string name = "default")
        {
            try
            {
                _stopwatches.Add(name, new Stopwatch());
                _stopwatches[name].Start();
            }
            catch
            {

            }
        }

        public static void Stop(string name = "default")
        {
            _stopwatches[name].Stop();
            var elapsedTime = _stopwatches[name].ElapsedMilliseconds;
            var resultMessage = $"Stopwatch {name} stopped after {elapsedTime} milliseconds";
            _measurementsResults.Add(resultMessage);
            _stopwatches.Remove(name);
        }

        public static void LogMessages()
        {
            _measurementsResults.ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }

        public static void ClearMessages()
        {
            _measurementsResults.Clear();
        }

    }
}