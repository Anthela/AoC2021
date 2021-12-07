namespace AdventOfCode.Problems
{
    public class Problem1 : IProblem<int, int>
    {

        public int DoPartA()
        {
            return CountIncreases(GetListOfIntegers());
        }

        public int DoPartB()
        {
            return CountIncreases_B(GetListOfIntegers());
        }

        private int CountIncreases(IList<int> numbers)
        {
            var sum = 0;

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                    sum++;
            }

            return sum;
        }

        private int CountIncreases_B(IList<int> numbers)
        {
            int current;
            int prev = 0;
            int increaseCount = 0;
            List<int> sums = new();
            bool flag = false;
            int a = 0;
            int i;

            for (i = 0; i < numbers.Count; i++)
            {
                int number = numbers[i];

                if (!flag && i > 0 && i % 3 == 2)
                {
                    flag = true;
                }

                if (flag)
                {
                    a += number;
                    sums.Add(a);
                    a = prev + number;
                    prev = number;
                }
                else
                {
                    a += number;
                    prev = number;
                }
            }

            i = 0;
            prev = 0;

            foreach (var item in sums)
            {
                if (i > 0)
                {
                    current = item;

                    if (current > prev)
                    {
                        increaseCount++;
                    }

                    prev = current;
                }
                else
                {
                    prev = item;
                    i++;
                }
            }

            return increaseCount;
        }

        private IList<int> GetListOfIntegers()
        {
            return Utils.StringArrayToIntArray(Utils.InputToStringArray("1")).ToList();
        }
    }
}
