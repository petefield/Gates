
namespace Gates
{
    public class Not : Gate
	{
        public Pin A => Pins["A"];
        public Pin Q => Pins["Q"];

        private TimeSpan _propogationDelay;
        private bool state; 
		public Not(string name) : base("not", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("Q", new Pin());
            Refresh(0).Wait();
            Pins["A"].ConnectTo(this);
	
		}

        public double PropogationDelay {
            get => _propogationDelay.TotalMicroseconds;
            set { _propogationDelay = TimeSpan.FromMicroseconds(value); }  }

		protected async override Task Refresh(int newLevel)
        {
      
            bool pinAisHigh = Pins["A"].Level >= 2;

            await Task.Delay(_propogationDelay);

            state = pinAisHigh;

            Pins["Q"].Level = !state ? 5 : 0;
        
		}
    }
}
