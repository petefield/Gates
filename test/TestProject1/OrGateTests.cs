using Gates;
using Xunit;

namespace TestProject1
{
    public class OrGateTests
	{
        readonly Module _sut;
       
        public OrGateTests()
        {
               _sut = new ModuleFactory().Create("or", "OR");
        }

        [Theory]
        [InlineData(0 ,0, 0)]
        [InlineData(0, 5, 5)]
        [InlineData(5, 0, 5)]
        [InlineData(5, 5, 5)]

        public void ShouldFollowTruthTable(int A, int B, int Q)
        {
			var testListener = new TestListener(TimeSpan.FromMilliseconds(1));

			_sut.Pins["Q"].ConnectTo(testListener);
			_sut.Pins["A"].Level = A;
			_sut.Pins["B"].Level = B;

			testListener.WaitAndAssertPinLevel(Q, _sut.Pins["Q"], expectedGateLevelChangeEvents: 2);
		}
    }
}