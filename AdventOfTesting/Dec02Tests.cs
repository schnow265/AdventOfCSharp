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

        [Theory]
        [InlineData(true, "7 6 4 2 1")]
        [InlineData(false, "1 2 7 8 9")]
        [InlineData(false, "9 7 6 2 1")]
        [InlineData(true, "1 3 2 4 5")]
        [InlineData(true, "8 6 4 4 1")]
        [InlineData(true, "1 3 6 7 9")]
        [InlineData(true, "92 94 97 98 97")]
        [InlineData(true, "26 27 28 31 33 34 37 37")]
        [InlineData(true, "56 59 60 61 62 65 69")]
        [InlineData(true, "42 44 46 48 55")]
        [InlineData(true, "15 18 19 22 21 24 26")]
        [InlineData(false, "74 76 77 76 77 78 75")]
        [InlineData(false, "60 62 64 65 64 64")]
        [InlineData(false, "61 64 67 64 67 68 72")]
        [InlineData(false, "71 74 73 76 82")]
        [InlineData(true, "25 26 27 29 29 32")]
        public void EvenWorseArrayLogic(bool expected, string str)
        {
            int[] input = str.Split(" ").Select(int.Parse).ToArray();

            Assert.Equal(expected, TestingGrounds.Dec02ArrayHell.OHMYGOODNESS(input));
        }

        [Theory]
        [InlineData(true, "1 2 3 4 5")]
        [InlineData(true, "5 4 3 2 1")]
        [InlineData(false, "1 3 5 8 2")]
        public void IsArrayMonotonic(bool expected, string array)
        {
            int[] input = array.Split(" ").Select(int.Parse).ToArray();

            Assert.Equal(expected, TestingGrounds.Dec02ArrayHell.IsMonotonic(input));
        }
    }
}
