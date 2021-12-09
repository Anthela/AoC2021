namespace AdventOfCode_2021.Problems
{
    public class Problem10 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("10_mini");

            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }

            return 0;
        }

        public int DoPartB()
        {
            return 0;
        }
    }
}
