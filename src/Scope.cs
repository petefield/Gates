using Gates;

namespace Factory
{
    internal class Scope
    {
        private readonly System.Timers.Timer _timer;
		private readonly List<Probe> _probes = [];
		private bool _running = false;
        private readonly Dictionary<string, List<int>> _channelValues;
        private readonly int _top = 5;
		object _lockObj = new object();

		public Scope()
        {
			_timer = new System.Timers.Timer
			{
				AutoReset = true
			};
			_timer.Elapsed += OnTimerElapsed;

			Console.OutputEncoding = System.Text.Encoding.Unicode;
			_channelValues = [];
		}

        public void Start(int interval) {
            _timer.Interval = interval;
            Resume();
        }

        public Boolean Running => _running;

        public void Pause()
		{
            _running = false;
			_timer.Stop();
		}

		public void Resume()
		{
			_timer.Start();
			_running = true;
		}


		private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_lockObj)
            {
                foreach (Probe probe in _probes)
                {
                    _channelValues[probe.Name].Add(probe.Level);
                }

                var i = _top;

                foreach (var channel in _channelValues)
                {
                    var s = channel.Value.TakeLast(100).Select(FormatLevel).ToArray();
                    var plot = new string(s);
                    Console.SetCursorPosition(0, i);
                    Console.Write($"{channel.Key,-15} : {plot}");
                    i += 2;
                }
            }
		}

        private static char FormatLevel(int level)
        {
			return level switch
			{
				0 => '_',
				5 => '‾',
				_ => ' ',
			};
		}



        public void AddProbe(string name, Pin pin)
        {
            _probes.Add(new Probe(name, pin));
            _channelValues.Add(name, new List<int>());
        }
    }
}
