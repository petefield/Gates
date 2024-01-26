namespace Gates;

public class Module(string type, string name) 
{
	public string Type { get; } = type;

	public string Name { get; } = name;

	public Dictionary<string, Pin> Pins { get; set; } = [];

    public Dictionary<string, Module> Modules { get; set; } = [];

	public void Initialise()
	{
		foreach (var pin in Pins.Values)
		{
			pin.Level = 0;
		}
	}

}
