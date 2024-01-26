// See https://aka.ms/new-console-template for more information
using Factory;
using Gates;
using System.Runtime.CompilerServices;

var A = "A";
var B = "B";
var Q = "Q";

Console.WriteLine("Press any key to start simulation");
Console.ReadKey();
Console.Clear();
var factory = new ModuleFactory();

var sr_latch = factory.Create("sr-latch", "SR Latch");

var scope = new Scope();


scope.AddProbe("S", sr_latch.Pins["S"]);
scope.AddProbe("R", sr_latch.Pins["R"]);
scope.AddProbe("Q", sr_latch.Pins["Q"]);
scope.AddProbe("Q!", sr_latch.Pins["Q!"]);
sr_latch.Pins["R"].Level = 5;
Thread.Sleep(500);
sr_latch.Pins["R"].Level = 0;
//scope.AddProbe("Nor2.A", sr_latch.Modules["Nor2"].Pins["A"]);
//scope.AddProbe("Nor2.B", sr_latch.Modules["Nor2"].Pins["B"]);
//scope.AddProbe("Nor2.Q", sr_latch.Modules["Nor2"].Pins["Q"]);

//scope.AddProbe("Nor2.Or.A", sr_latch.Modules["Nor2"].Modules["Or"].Pins["A"]);
//scope.AddProbe("Nor2.Or.B", sr_latch.Modules["Nor2"].Modules["Or"].Pins["B"]);
//scope.AddProbe("Nor2.Or.Q", sr_latch.Modules["Nor2"].Modules["Or"].Pins["Q"]);

//scope.AddProbe("Nor2.Not.A", sr_latch.Modules["Nor2"].Modules["Not"].Pins["A"]);
//scope.AddProbe("Nor2.Not.Q", sr_latch.Modules["Nor2"].Modules["Not"].Pins["Q"]);


//scope.AddProbe("Nor1.A", sr_latch.Modules["Nor1"].Pins["A"]); 
//scope.AddProbe("Nor1.B", sr_latch.Modules["Nor1"].Pins["B"]);
//scope.AddProbe("Nor1.Q", sr_latch.Modules["Nor1"].Pins["Q"]);
//scope.AddProbe("Nor1.Or.A", sr_latch.Modules["Nor1"].Modules["Or"].Pins["A"]);
//scope.AddProbe("Nor1.Or.B", sr_latch.Modules["Nor1"].Modules["Or"].Pins["B"]);
//scope.AddProbe("Nor1.Or.Q", sr_latch.Modules["Nor1"].Modules["Or"].Pins["Q"]);
//scope.AddProbe("Nor1.Not.A", sr_latch.Modules["Nor1"].Modules["Not"].Pins["A"]);
//scope.AddProbe("Nor1.Not.Q", sr_latch.Modules["Nor1"].Modules["Not"].Pins["Q"]);



scope.Start(100);

do
{
	var key = Console.ReadKey();

	if (key.Key == ConsoleKey.Spacebar)
	{
		if (scope.Running)
		{
			scope.Pause();
			Console.SetCursorPosition(0,0);
			Console.Write("Paused");
		}
		else
		{
			scope.Resume();
			Console.SetCursorPosition(0,0);

			Console.Write("      ");
		}
	}
	else
	{
		var pinId = Char.ToUpper(key.KeyChar).ToString();

		if (sr_latch.Pins.TryGetValue(pinId, out var pin))
			TogglePin(pin);
	}
} while (true);

static void TogglePin(Pin pin)
{
	var v = pin.Level;
	if (v >= 2) pin.Level = 0;
	if (v < 2) pin.Level =5;
}


