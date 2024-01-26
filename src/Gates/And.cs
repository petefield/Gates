namespace Gates
{
    public class And : Gate
    {
        public And(string name) : base("and", name)
        {
            Pins.Add("A", new Pin());
            Pins.Add("B", new Pin());
            Pins.Add("Q", new Pin());

            Pins["A"].ConnectTo(this);
            Pins["B"].ConnectTo(this);
		}

		protected override void Refresh(int newLevel)
		{
			bool a = Pins["A"].Level > 2;
			bool b = Pins["B"].Level > 2;
			Pins["Q"].Level = (a & b) ? 5 : 0;
		}
	}
}
