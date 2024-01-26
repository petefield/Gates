namespace Gates
{
    public class Or : Gate
	{
        public Or(string name) : base("or", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("B", new Pin());
            Pins.Add("Q", new Pin());

			Pins["A"].ConnectTo(this);
			Pins["B"].ConnectTo(this);

		}

		protected override void Refresh(int newLevel)
		{
			bool a = Pins["A"].Level >= 2;
			bool b = Pins["B"].Level >= 2;

			var state = a || b;

			Pins["Q"].Level = state ? 5 : 0;
			
		}
	}
}
