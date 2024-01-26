namespace Gates
{
    public class Clock : Module
    {
        public Pin Q => Pins["Q"];

        System.Timers.Timer _timer;
        public Clock(string name) : base("clock", name)
        {
            Pins.Add("Q", new Pin());
            _timer = new System.Timers.Timer();
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Pins["Q"].Level = Pins["Q"].Level == 0 ? 5 : 0;
        }

        public void Start(double interval)
        {
            _timer.Interval = interval;
            _timer.Start();
        }

	}
}
