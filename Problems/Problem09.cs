namespace AdventOfCode_2021.Problems
{
    public class Problem09 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("9").ToArray();
            List<int> minis = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    bool valid = true;
                    int number = Convert.ToInt32(lines[i][j].ToString());

                    if (i - 1 >= 0)
                        valid = Convert.ToInt32(lines[i - 1][j].ToString()) > number;

                    if (valid)
                        if (i + 1 < lines.Length)
                            valid = Convert.ToInt32(lines[i + 1][j].ToString()) > number;

                    if (valid)
                        if (j - 1 >= 0)
                            valid = Convert.ToInt32(lines[i][j - 1].ToString()) > number;

                    if (valid)
                        if (j + 1 < lines[i].Length)
                            valid = Convert.ToInt32(lines[i][j + 1].ToString()) > number;

                    if (valid)
                        minis.Add(number);
                }
            }

            return minis.Aggregate(0, (a, b) => a += 1 + b);
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("9").ToArray();

            var table = CreateTableFromInput(lines);

            var lowPoints = table.Where(x => x.Value.Item2).ToArray();

            List<((int, int), int)> usedItems = new();

            List<int> result = new();

            foreach (var item in lowPoints)
            {
                List<((int, int), int)> currentLowPoints = new();

                currentLowPoints.Add((item.Key, item.Value.Item1));

                usedItems.Clear();

                while (currentLowPoints.Any())
                {
                    var lowPoint = currentLowPoints.First();
                    usedItems.Add(lowPoint);

                    // Left
                    if (lowPoint.Item1.Item2 - 1 >= 0)
                    {
                        var position = (lowPoint.Item1.Item1, lowPoint.Item1.Item2 - 1);
                        var leftItem = table[position];

                        if (leftItem.Item1 != 9 && leftItem.Item1 >= lowPoint.Item2)
                            currentLowPoints.Add((position, leftItem.Item1));
                    }

                    // Right
                    if (lowPoint.Item1.Item2 + 1 < lines[0].Length)
                    {
                        var position = (lowPoint.Item1.Item1, lowPoint.Item1.Item2 + 1);
                        var rightItem = table[position];

                        if (rightItem.Item1 != 9 && rightItem.Item1 >= lowPoint.Item2)
                            currentLowPoints.Add((position, rightItem.Item1));
                    }

                    // Top
                    if (lowPoint.Item1.Item1 - 1 >= 0)
                    {
                        var position = (lowPoint.Item1.Item1 - 1, lowPoint.Item1.Item2);
                        var topItem = table[position];

                        if (topItem.Item1 != 9 && topItem.Item1 >= lowPoint.Item2)
                            currentLowPoints.Add((position, topItem.Item1));
                    }

                    // Bottom
                    if (lowPoint.Item1.Item1 + 1 < lines.Length)
                    {
                        var position = (lowPoint.Item1.Item1 + 1, lowPoint.Item1.Item2);
                        var bottomItem = table[position];

                        if (bottomItem.Item1 != 9 && bottomItem.Item1 >= lowPoint.Item2)
                            currentLowPoints.Add((position, bottomItem.Item1));
                    }

                    currentLowPoints = currentLowPoints.Except(usedItems).ToList();
                }

                result.Add(usedItems.Count);
            }

            return result.OrderByDescending(i => i).Take(3).Aggregate(1, (a, b) => a *= b);
        }

        private Dictionary<(int, int), (int, bool)> CreateTableFromInput(string[] lines)
        {
            Dictionary<(int, int), (int, bool)> table = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    bool valid = true;
                    int number = Convert.ToInt32(lines[i][j].ToString());

                    if (i - 1 >= 0)
                        valid = Convert.ToInt32(lines[i - 1][j].ToString()) > number;

                    if (valid)
                        if (i + 1 < lines.Length)
                            valid = Convert.ToInt32(lines[i + 1][j].ToString()) > number;

                    if (valid)
                        if (j - 1 >= 0)
                            valid = Convert.ToInt32(lines[i][j - 1].ToString()) > number;

                    if (valid)
                        if (j + 1 < lines[i].Length)
                            valid = Convert.ToInt32(lines[i][j + 1].ToString()) > number;

                    table[(i, j)] = (number, valid);
                }
            }

            return table;
        }
    }
}
