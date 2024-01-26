namespace Gates;

public abstract class Gate: Module, ILevelChangeListener
{
	protected Gate(string type, string name) : base(type, name)
	{
		Modules = [];
	}

	protected abstract void Refresh(int level);

	public Action<int> LevelChanged => Refresh;


}
