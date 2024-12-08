﻿using AOCRunner.Attributes;
using Serilog;

namespace _2024
{
    [Solution]
    public class Dec02
    {
        private readonly ILogger logger;

        public Dec02(ILogger logger)
        {
            this.logger = logger;
        }

        public void Part01()
        {
            string[] contents = File.ReadAllLines(Path.Join(Environment.CurrentDirectory, "Resources", "Day02Input.txt"));

            logger.Debug("Read {LineCount} lines.", contents.Length);

            int safeReports = 0;

            foreach (string line in contents)
            {
                int[] lineSplit = line.Split(' ').Select(int.Parse).ToArray();

                if (IncDec(lineSplit))
                {
                    safeReports++;
                }
            }
        }

        public static bool IncDec(int[] arr)
        {
            int difference = arr[1] - arr[0];
            bool returning = false;

            List<int> arrList = arr.ToList();

            for (int i = 0; i <= arrList.Count; i++)
            {
                if (i != 0 && !returning)
                {
                    break;
                }

                int c1 = arrList[i + 1];
                int c2 = arrList[i];

                int currentDifference = c1 - c2;

                if (currentDifference == difference)
                {
                    returning = true;
                }
                else
                {
                    if (currentDifference < difference)
                    {
                        throw new Exception("Difference is smaller!");
                    }

                    for (int j = 0; j <= 3; j++)
                    {
                        if (currentDifference == difference + j)
                        {
                            returning = true;
                            break;
                        }
                        else
                        {
                            returning = false;
                        }
                    }
                }

                difference = currentDifference;
            }

            return returning;
        }
    }
}
