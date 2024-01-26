using Gates;
using Xunit;

namespace TestProject1;

internal class TestListener : ILevelChangeListener
{
	private readonly TimeSpan _timeToWait;
	private int gateLevelChangeEvents; 

	public TestListener(TimeSpan timeToWait)
	{
		_timeToWait = timeToWait;
	}

	public Func<int, Task> LevelChanged => Refresh;

	protected async  Task Refresh(int newLevel)
	{
		gateLevelChangeEvents++;
		await Task.Delay(0);


	}

	public void WaitAndAssertPinLevel(int value ,Pin p, int expectedGateLevelChangeEvents = 0)
	{
		Thread.Sleep(_timeToWait);
		if (expectedGateLevelChangeEvents > 0){
			Assert.Equal(expectedGateLevelChangeEvents, gateLevelChangeEvents);
		}

		Assert.Equal(value, p.Level);
	}

}