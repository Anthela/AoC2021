namespace AdventOfCode_2021.Problems
{
    public class Problem15 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("15").ToArray();

            Dictionary<(int, int), int> table = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    table[(i, j)] = Convert.ToInt32(lines[i][j].ToString());
                }
            }

            return CalculateLowestRiskPath(table);
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("15").ToArray();

            Dictionary<(int, int), int> table = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    int value = Convert.ToInt32(lines[i][j].ToString());

                    table[(i, j)] = value;

                    for (int k = 1; k < 5; k++)
                    {
                        table[(i, j + k * lines[i].Length)] = value + k <= 9 ? value + k : value + k - 9;
                    }
                }
            }

            var bottomRight = (table.Keys.MaxBy(x => x.Item1).Item1, table.Keys.MaxBy(x => x.Item2).Item2);

            for (int i = 0; i <= bottomRight.Item1; i++)
            {
                for (int j = 0; j <= bottomRight.Item2; j++)
                {
                    int value = table[(i, j)];

                    for (int k = 1; k < 5; k++)
                    {
                        table[(i + k * (bottomRight.Item1 + 1), j)] = value + k <= 9 ? value + k : value + k - 9;
                    }
                }
            }

            return CalculateLowestRiskPath(table);
        }

        private IEnumerable<(int, int)> Neighbours((int, int) position)
        {
            yield return (position.Item1 - 1, position.Item2);
            yield return (position.Item1 + 1, position.Item2);
            yield return (position.Item1, position.Item2 - 1);
            yield return (position.Item1, position.Item2 + 1);
        }

        private int CalculateLowestRiskPath(Dictionary<(int, int), int> table)
        {
            var bottomRight = (table.Keys.MaxBy(x => x.Item1).Item1, table.Keys.MaxBy(x => x.Item2).Item2);

            var q = new PriorityQueue<(int, int), int>();
            var totalRisk = new Dictionary<(int, int), int>();

            totalRisk[(0, 0)] = 0;
            q.Enqueue((0, 0), 0);

            while (q.Count > 0)
            {
                var position = q.Dequeue();

                foreach (var neighbour in Neighbours(position))
                {
                    if (table.ContainsKey(neighbour) && !totalRisk.ContainsKey(neighbour))
                    {
                        var risk = totalRisk[position] + table[neighbour];

                        totalRisk[neighbour] = risk;

                        if (neighbour == bottomRight)
                        {
                            q.Clear();
                            break;
                        }

                        q.Enqueue(neighbour, risk);
                    }
                }
            }

            return totalRisk[bottomRight];
        }
    }
}