
namespace Gates;

public interface ILevelChangeListener
{
	public Func<int, Task>? LevelChanged { get;  }
}

public class Pin : ILevelChangeListener
{

	private readonly List<ILevelChangeListener> _listeners = [];

	private int _level;

	public int Level
	{
		get => _level;
		set => LevelChangedHandler(value);
	}

	public Func<int, Task>? LevelChanged => LevelChangedHandler;

	public void ConnectTo(ILevelChangeListener pin)
	{
		_listeners.Add(pin);
	}

	private Task LevelChangedHandler(int newLevel)
	{			
		
		_level = newLevel;

		if (_listeners.Count == 1)
		{
			_listeners.Single().LevelChanged?.Invoke(newLevel);
			return Task.CompletedTask; 
		}
		else
		{
			var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };


			return Parallel.ForEachAsync(_listeners, options, async (listener, c)  =>
			{
				var l = listener.LevelChanged;

				if(l != null) 
					await l.Invoke(newLevel);
			});
		}
	 }

}
