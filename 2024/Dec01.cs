﻿using AOCRunner.Attributes;
using Serilog;

namespace _2024
{
    [Solution]
    public class Dec01
    {
        private readonly ILogger logger;

        public Dec01(ILogger logger)
        {
            this.logger = logger;
        }

        public void Part01()
        {
            var sortedItems = SplitFile();

            List<int> leftCol = sortedItems.Item1;
            List<int> rightCol = sortedItems.Item2;

            List<int> distances = new List<int>();

            for (int i = 0; i < leftCol.Count; i++)
            {
                if (leftCol[i] > rightCol[i])
                {
                    distances.Add(leftCol[i] - rightCol[i]);
                }
                else if (leftCol[i] == rightCol[i])
                {
                    distances.Add(0);
                }
                else
                {
                    distances.Add(rightCol[i] - leftCol[i]);
                }
            }

            logger.Information("The result for Day 1 is: '{Res}'", distances.Sum());
        }

        public void Part02()
        {
            var sortedItems = SplitFile();

            List<int> leftCol = sortedItems.Item1;
            List<int> rightCol = sortedItems.Item2;

            string result = leftCol
                .Select(leftItem => leftItem * rightCol.Count(rightItem => leftItem == rightItem))
                .Sum()
                .ToString();

            logger.Information("The result for Day 1 is: '{Res}'", result);
        }

        private (List<int>, List<int>) SplitFile()
        {
            string path = Path.Join(Environment.CurrentDirectory, "Resources", "Day01Input.txt");

            List<int> leftCol = new List<int>();
            List<int> rightCol = new List<int>();

            foreach (var line in File.ReadLines(path))
            {
                var parts = line.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int left) && int.TryParse(parts[1], out int right))
                    {
                        leftCol.Add(left);
                        rightCol.Add(right);
                    }
                    else
                    {
                        logger.Error("Failed to parse line: {Line}", line);
                    }
                }
                else
                {
                    logger.Error("Invalid format in line: {Line}", line);
                }
            }

            logger.Debug("leftCol has {LeftColElements} and rightCol has {RightColElements}", leftCol.Count, rightCol.Count);

            leftCol.Sort();
            rightCol.Sort();

            return (leftCol, rightCol);
        }
    }
}
