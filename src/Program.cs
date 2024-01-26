// See https://aka.ms/new-console-template for more information
using Factory;
using Gates;


var factory = new ModuleFactory();

var clock = factory.Create("clock", "clock");
var module = factory.Create("gated-sr-latch", "latch");

var scope = new Scope();

Console.WriteLine("Press any key to start simulation");
Console.ReadKey(true);

scope.AddProbe("Clock", clock.Pins["Q"]);

TogglePin(clock.Pins["A"]);

scope.AddProbe("S", module.Pins["S"]);
scope.AddProbe("E", module.Pins["E"]);
scope.AddProbe("R", module.Pins["R"]);
scope.AddProbe("Q", module.Pins["Q"]);
scope.AddProbe("Q!", module.Pins["Q!"]);

//scope.AddProbe("And1.Q", module.Modules["And1"].Pins["Q"]);
//scope.AddProbe("And2.Q", module.Modules["And2"].Pins["Q"]);


//scope.AddProbe("sr.S", module.Modules["srlatch"].Pins["S"]);

//scope.AddProbe("sr.Q", module.Modules["srlatch"].Pins["Q"]);





scope.Start(100);

do
{
	var keyInfo = Console.ReadKey(true);

	if (keyInfo.Key == ConsoleKey.Spacebar)
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
		var pinId = Char.ToUpper(keyInfo.KeyChar).ToString();

		if (module.Pins.TryGetValue(pinId, out var pin))
		{
			if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
				TogglePin(pin);
			else
				Pulse(pin);
		}
	}
} while (true);

static void TogglePin(Pin pin)
{
	var v = pin.Level;
	if (v >= 2) pin.Level = 0;
	if (v < 2) pin.Level =5;
}

static void Pulse(Pin pin)
{
	var v = pin.Level;
	var newV = (v >= 2) ? 0 : 5;
	pin.Level = newV;
	Thread.Sleep(10);
	pin.Level = v;
}

