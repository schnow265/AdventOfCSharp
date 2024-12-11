namespace AdventOfTesting
{
    public class Dec03Tests
    {
        [Theory]
        [InlineData(161, """xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))§""")]
        public void TestRegexTheory(int result, string input)
        {
            Assert.Equal(result, TestingGrounds.Dec03RegexHell.ReadGrabageData(input));
        }

        [Theory]
        [InlineData(48, """xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))""")]
        public void ALitteralStateMachine(int result, string input)
        {
            Assert.Equal(result, TestingGrounds.Dec03RegexHell.StateMachine(input));
        }
    }
}
