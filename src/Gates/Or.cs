namespace Gates
{
    public class Or : Gate
	{
		private TimeSpan _propogationDelay;

		public Or(string name) : base("or", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("B", new Pin());
            Pins.Add("Q", new Pin());

			Pins["A"].ConnectTo(this);
			Pins["B"].ConnectTo(this);
			_propogationDelay = TimeSpan.FromMicroseconds(new Random().Next(1, 1000));

		}

		protected override async Task Refresh(int newLevel)
		{
			bool a = Pins["A"].Level >= 2;
			bool b = Pins["B"].Level >= 2;
			await Task.Delay(_propogationDelay);
			var state = a || b;

			Pins["Q"].Level = state ? 5 : 0;
			
		}
	}
}
