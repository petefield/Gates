// See https://aka.ms/new-console-template for more information
using Factory;
using Gates;


var factory = new ModuleFactory();

var clock = new Clock("123");// factory.Create("clock", "clock");
var module = factory.Create("d-flipflop", "df");

var scope = new Scope();

Console.WriteLine("Press any key to start simulation");

Console.ReadKey(true);
clock.Start(1000);

scope.AddProbe("Clock", clock.Pins["Q"]);

//TogglePin(clock.Pins["A"]);


clock.Pins["Q"].ConnectTo(module.Pins["CLK"]);

scope.AddProbe("d-flipflop.clk", module.Pins["CLK"]);
scope.AddProbe("d-flipflop.d", module.Pins["D"]);
scope.AddProbe("d-flipflop.q", module.Pins["Q"]);


//scope.AddProbe("And1.Q", module.Modules["And1"].Pins["Q"]);
//scope.AddProbe("And2.Q", module.Modules["And2"].Pins["Q"]);


//scope.AddProbe("sr.S", module.Modules["srlatch"].Pins["S"]);

//scope.AddProbe("sr.Q", module.Modules["srlatch"].Pins["Q"]);





scope.Start(10);

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

