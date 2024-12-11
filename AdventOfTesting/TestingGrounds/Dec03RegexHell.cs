using System.Text.RegularExpressions;

namespace AdventOfTesting.TestingGrounds
{
    public partial class Dec03RegexHell
    {
        public static int? ReadGrabageData(string garbage)
        {
            List<int> sumMeup = [];

            RegexOptions options = RegexOptions.Multiline;

            foreach (Match match in AOC03Regex().Matches(garbage))
            {
                sumMeup.Add(int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
            }

            return sumMeup.Sum();
        }

        public static int? StateMachine(string garbage)
        {
            List<string> matchArgs = ValidateMulMatches(garbage);

            List<int?> ints = [];

            foreach (string str in matchArgs)
            {
                int? i = ReadGrabageData(str);

                ints.Add(i);
            }

            return ints.Sum();
        }

        private static List<string> ValidateMulMatches(string input)
        {
            var matches = new List<string>();
            bool enabled = true; // Start with matching enabled
            var regex = MulValidatorRegex();

            var ms = regex.Matches(input);

            foreach (Match match in ms)
            {
                string token = match.Value;

                if (token.Equals("do()"))
                {
                    enabled = true; // Enable matching
                }
                else if (token.Equals("don't()"))
                {
                    enabled = false; // Disable matching
                }
                else if (enabled && token.StartsWith("mul("))
                {
                    matches.Add(token); // Add valid `mul(...)` matches
                }
            }

            return matches;
        }

        [GeneratedRegex(@"(don't\(\))|(do\(\))|(mul\(([0-9]{1,3}),([0-9]{1,3})\))", RegexOptions.Compiled)]
        private static partial Regex MulValidatorRegex();

        [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)")]
        private static partial Regex AOC03Regex();

    }
}
