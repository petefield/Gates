namespace Gates;

public abstract class Gate: Module, ILevelChangeListener
{
	protected Gate(string type, string name) : base(type, name)
	{
		Modules = [];
	}

	protected abstract Task Refresh(int level);

	public Func<int,Task> LevelChanged => Refresh;


}
