namespace AdventOfTesting.TestingGrounds
{
    public class Dec02ArrayHell
    {
        public static bool IncDec(int[] arr)
        {
            bool differenceLowerThanThree = false;
            bool numbersChanged = true;
            bool onlyOneAction = true;

            List<int> arrList = arr.ToList();

            for (int i = 0; i < arrList.Count - 1; i++)
            {

                int c1 = arrList[i];
                int c2 = arrList[i + 1];

                if (c1 == c2)
                {
                    numbersChanged = false;
                    break;
                }

                int currentDifference = c1 - c2;



                if (currentDifference <= 3 && currentDifference > 0)
                {
                    differenceLowerThanThree = true;
                }
                else if (currentDifference >= -3 && currentDifference <= 0) { differenceLowerThanThree = true; }
                else
                {
                    differenceLowerThanThree = false;
                    break;
                }

            }
            onlyOneAction = IsMonotonic(arr);

            return differenceLowerThanThree && numbersChanged && onlyOneAction;
        }

        public static bool IsMonotonic(int[] array)
        {
            if (array == null || array.Length < 2)
                return true; // An empty or single-element array is considered monotonic.

            bool isIncreasing = true;
            bool isDecreasing = true;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[i - 1])
                    isDecreasing = false;
                if (array[i] < array[i - 1])
                    isIncreasing = false;

                // If neither increasing nor decreasing, we can stop early.
                if (!isIncreasing && !isDecreasing)
                    return false;
            }

            return isIncreasing || isDecreasing;
        }

        public static bool OHMYGOODNESS(int[] arr)
        {
            List<int> arrList = [.. arr];

            if (IncDec(arr))
            {
                return true;
            }

            for (int i = 0; i < arrList.Count; i++)
            {
                int[] arrayHell = arrList.ToArray();

                List<int> tmp = arrayHell.ToList();

                tmp.RemoveAt(i);

                if (IncDec(tmp.ToArray()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
