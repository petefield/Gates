
namespace Gates;

public interface ILevelChangeListener
{
	public Action<int>? LevelChanged { get;  }
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

	public Action<int>? LevelChanged => LevelChangedHandler;

	public void ConnectTo(ILevelChangeListener pin)
	{
		_listeners.Add(pin);
	}

	private void LevelChangedHandler(int newLevel)
	{			
		
		_level = newLevel;

		if (_listeners.Count == 1)
		{
			_listeners.Single().LevelChanged?.Invoke(newLevel);
		}
		else
		{
			Parallel.ForEach(_listeners, listener =>
			{
				var l = listener.LevelChanged;
				l?.Invoke(newLevel);
			});
		}
	 }

}
