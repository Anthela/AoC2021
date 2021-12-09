using AdventOfCode_2021.Problems.Model.Problem9;

namespace AdventOfCode.Problems
{
    public class Problem9 : IProblem<int, int>
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

            var lowPoints = table.Where(x => x.Value.Marked).ToArray();

            List<KeyValuePair<(int, int), TableItem>> usedItems = new();

            List<int> result = new();

            foreach (var item in lowPoints)
            {
                List<KeyValuePair<(int, int), TableItem>> currentLowPoints = new();

                currentLowPoints.Add(item);

                usedItems.Clear();

                while (currentLowPoints.Any())
                {
                    var lowPoint = currentLowPoints.First();
                    usedItems.Add(lowPoint);

                    // Left
                    if (lowPoint.Key.Item2 - 1 >= 0)
                    {
                        var position = (lowPoint.Key.Item1, lowPoint.Key.Item2 - 1);
                        var leftItem = table[position];

                        if (leftItem.Value != 9 && leftItem.Value >= lowPoint.Value.Value)
                            currentLowPoints.Add(new KeyValuePair<(int, int), TableItem>(position, leftItem));
                    }

                    // Right
                    if (lowPoint.Key.Item2 + 1 < lines[0].Length)
                    {
                        var position = (lowPoint.Key.Item1, lowPoint.Key.Item2 + 1);
                        var rightItem = table[position];

                        if (rightItem.Value != 9 && rightItem.Value >= lowPoint.Value.Value)
                            currentLowPoints.Add(new KeyValuePair<(int, int), TableItem>(position, rightItem));
                    }

                    // Top
                    if (lowPoint.Key.Item1 - 1 >= 0)
                    {
                        var position = (lowPoint.Key.Item1 - 1, lowPoint.Key.Item2);
                        var topItem = table[position];

                        if (topItem.Value != 9 && topItem.Value >= lowPoint.Value.Value)
                            currentLowPoints.Add(new KeyValuePair<(int, int), TableItem>(position, topItem));
                    }

                    // Bottom
                    if (lowPoint.Key.Item1 + 1 < lines.Length)
                    {
                        var position = (lowPoint.Key.Item1 + 1, lowPoint.Key.Item2);
                        var bottomItem = table[position];

                        if (bottomItem.Value != 9 && bottomItem.Value >= lowPoint.Value.Value)
                            currentLowPoints.Add(new KeyValuePair<(int, int), TableItem>(position, bottomItem));
                    }

                    currentLowPoints = currentLowPoints.Except(usedItems).ToList();
                }

                result.Add(usedItems.Count);
            }

            return result.OrderByDescending(i => i).Take(3).Aggregate(1, (a, b) => a *= b);
        }

        private Dictionary<(int, int), TableItem> CreateTableFromInput(string[] lines)
        {
            Dictionary<(int, int), TableItem> table = new();

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

                    table[(i, j)] = new TableItem() { Value = number, Marked = valid };
                }
            }

            return table;
        }
    }
}
