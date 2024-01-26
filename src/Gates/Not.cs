
namespace Gates
{
    public class Not : Gate
	{
        public Pin A => Pins["A"];
        public Pin Q => Pins["Q"];

		public Not(string name) : base("not", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("Q", new Pin());
            Pins["Q"].Level = 5;
            Pins["A"].ConnectTo(this);
		}

        protected override void Refresh(int newLevel)
        {
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromMicroseconds(10));
                bool pinAisHigh = Pins["A"].Level >= 2;
                Pins["Q"].Level = pinAisHigh ? 0 : 5;
            });
		}
    }
}
