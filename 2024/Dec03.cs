using AOCRunner.Attributes;
using Serilog;
using System.Text.RegularExpressions;

namespace _2024
{
    [Solution]
    public partial class Dec03(ILogger logger)
    {
        private readonly ILogger logger = logger;

        public void Part01()
        {
            string contents = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "Resources", "Day03Input.txt"));

            int? count = ReadGrabageData(contents);

            logger.Information("Result: {Count}", count);
        }

        public void Part02()
        {
            string contents = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "Resources", "Day03Input.txt"));

            int? count = StateMachine(contents);

            logger.Information("Result: {Count}", count);
        }


        private static int? StateMachine(string garbage)
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

        private static int? ReadGrabageData(string garbage)
        {
            List<int> sumMeup = [];

            sumMeup.AddRange(
                from Match match in AOC03Regex().Matches(garbage)
                select int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)
            );

            return sumMeup.Sum();
        }


        [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)")]
        private static partial Regex AOC03Regex();
    }
}
