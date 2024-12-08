namespace AdventOfTesting.TestingGrounds
{
    public class Dec02ArrayHell
    {
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
                    // 5(!) Tests affected by this problem!

                    // if (currentDifference < difference)
                    // {
                    //     throw new Exception("Difference is smaller!");
                    // }

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
