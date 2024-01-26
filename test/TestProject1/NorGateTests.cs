using Gates;
using Xunit;

namespace TestProject1
{
    public class NorGateTests
    {
		readonly Module _sut;
       
        public NorGateTests()
        {
            _sut = new ModuleFactory().Create("nor", "NOR");
        }

        [Theory]
        [InlineData(0 ,0, 5)]
        [InlineData(0, 5, 0)]
        [InlineData(5, 0, 0)]
        [InlineData(5, 5, 0)]

        public void ShouldFollowTruthTable(int A, int B, int Q)
        {
			var testListener = new TestListener(TimeSpan.FromMilliseconds(10));

			_sut.Pins["Q"].ConnectTo(testListener);
			_sut.Pins["A"].Level = A;
			_sut.Pins["B"].Level = B;

			testListener.WaitAndAssertPinLevel(Q, _sut.Pins["Q"]);
		}
    }
}