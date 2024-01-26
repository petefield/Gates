// See https://aka.ms/new-console-template for more information
using Gates;

public class Probe(string name, Pin pin)
{
    private readonly Pin _pin = pin;

	public string Name { get; } = name;

	public int Level => _pin.Level;

}
