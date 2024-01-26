using Gates;
using Xunit;

namespace TestProject1
{
	public class NotTests
    {
		readonly Module _sut;
       
        public NotTests()
        {
            _sut = new ModuleFactory().Create("not", "Not");
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 0)]
        public  void ShouldFollowTruthTable(int A, int Q)
        {
            var testListener = new TestListener(TimeSpan.FromMilliseconds(1));

			_sut.Pins["Q"].ConnectTo(testListener);

			_sut.Pins["A"].Level = A;

            testListener.WaitAndAssertPinLevel(Q, _sut.Pins["Q"], expectedGateLevelChangeEvents: 1);

		}
	}
}