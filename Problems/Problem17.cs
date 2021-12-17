namespace AdventOfCode_2021.Problems
{
    public class Problem17 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var y = Math.Abs(Utils.InputToStringArray("17").Select(line => Convert.ToInt32(line.Split(", ").Last().Split('=').Last().Split("..").First())).Single());

            return (int)((y - 1) / 2.0 * y);
        }

        public int DoPartB()
        {
            return 0;
        }
    }
}
