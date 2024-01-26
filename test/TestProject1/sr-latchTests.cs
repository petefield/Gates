using Gates;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class sr_latchTests
    {
        Module _sut;
		private readonly ITestOutputHelper output;

		public sr_latchTests(ITestOutputHelper output)
		{
			this.output = output;
		

		_sut = new ModuleFactory().Create("sr-latch", "test");
        }

        [Fact]
        public void ShouldInitialisedCorrectly()
        {
			Assert.NotEqual(_sut.Pins["Q"].Level, _sut.Pins["Q!"].Level);
		}

		[Fact]
		public void ShouldResetCorrectly()
		{
			var testListener = new TestListener(TimeSpan.FromMilliseconds(1));

			output.WriteLine($"Q was {_sut.Pins["Q"].Level}" );

			_sut.Pins["R"].Level = 5; 
			Thread.Sleep(1);
			_sut.Pins["R"].Level = 0;
			Thread.Sleep(1);
			Assert.Equal(0, _sut.Pins["Q"].Level);

			_sut.Pins["S"].Level = 5;
			Thread.Sleep(1);
			Assert.Equal(5, _sut.Pins["Q"].Level);

			_sut.Pins["S"].Level = 0;
			Thread.Sleep(1);
			Assert.Equal(5, _sut.Pins["Q"].Level);

			_sut.Pins["R"].Level = 5;
			Thread.Sleep(1);
			_sut.Pins["R"].Level = 0;
			Thread.Sleep(1);
			Assert.Equal(0, _sut.Pins["Q"].Level);

		}
	}
}