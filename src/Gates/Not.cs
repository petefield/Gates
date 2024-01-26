
namespace Gates
{
    public class Not : Gate
	{
        public Pin A => Pins["A"];
        public Pin Q => Pins["Q"];

        private TimeSpan _propogationDelay;

		public Not(string name) : base("not", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("Q", new Pin());
            Pins["Q"].Level = 5;
            Pins["A"].ConnectTo(this);
			_propogationDelay = TimeSpan.FromMicroseconds(new Random().Next(1,10) );
		}

        public double PropogationDelay {
            get => _propogationDelay.TotalMicroseconds;
            set { _propogationDelay = TimeSpan.FromMicroseconds(value); }  }


		protected override void Refresh(int newLevel)
        {
            Task.Run(() =>
            {
                bool pinAisHigh = Pins["A"].Level >= 2;

                if (_propogationDelay.TotalMicroseconds > 0) { 
                                Thread.Sleep(_propogationDelay);

                }

                Pins["Q"].Level = pinAisHigh ? 0 : 5;
            });
		}
    }
}
