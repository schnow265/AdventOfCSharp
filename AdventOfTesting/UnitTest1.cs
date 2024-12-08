namespace AdventOfTesting
{
    public class Dec02Tests
    {
        [Theory]
        [InlineData(true, 7, 6, 4, 2, 1)]
        [InlineData(false, 1, 2, 7, 8, 9)]
        [InlineData(false, 9, 7, 6, 2, 1)]
        [InlineData(false, 1, 3, 2, 4, 5)]
        [InlineData(false, 8, 6, 4, 4, 1)]
        [InlineData(true, 1, 3, 6, 7, 9)]
        public void ArrayLogic(bool expected, params int[] input)
        {
            Assert.Equal(expected, TestingGrounds.Dec02ArrayHell.IncDec(input));
        }
    }
}
