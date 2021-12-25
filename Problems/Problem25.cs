namespace AdventOfCode_2021.Problems
{
    public class Problem25 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("25").ToArray();

            Dictionary<(int, int), char> table = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    table[(i, j)] = lines[i][j];
                }
            }

            int step = 0;

            while (table != null)
            {
                table = moveCucumbers(table);
                step++;
            }

            return step;
        }

        public int DoPartB()
        {
            return 0;
        }

        private Dictionary<(int, int), char> moveCucumbers(Dictionary<(int, int), char> table)
        {
            Dictionary<(int, int), char> newTable = new(table);

            bool moved = false;

            var cucumbersToMove = table.Where(x => x.Value == '>').ToDictionary(x => x.Key, y => y.Value);

            foreach (var cucumber in cucumbersToMove)
            {
                if (canCucumberMove(cucumber, table, out (int, int) positionToMove))
                {
                    moved = true;
                    newTable[positionToMove] = cucumber.Value;
                    newTable[cucumber.Key] = '.';
                }
            }

            if (moved)
            {
                table = newTable;
                newTable = new(table);
            }

            cucumbersToMove = table.Where(x => x.Value == 'v').ToDictionary(x => x.Key, y => y.Value);

            foreach (var cucumber in cucumbersToMove)
            {
                if (canCucumberMove(cucumber, table, out (int, int) positionToMove))
                {
                    moved = true;
                    newTable[positionToMove] = cucumber.Value;
                    newTable[cucumber.Key] = '.';
                }
            }

            if (moved)
                table = newTable;
            else
                table = null;

            return table;
        }

        private bool canCucumberMove(KeyValuePair<(int, int), char> cucumber, Dictionary<(int, int), char> table, out (int, int) positionToMove)
        {
            positionToMove = cucumber.Value == '>' ? (cucumber.Key.Item1, cucumber.Key.Item2 + 1) : (cucumber.Key.Item1 + 1, cucumber.Key.Item2);

            if (!table.ContainsKey(positionToMove))
                positionToMove = cucumber.Value == '>' ? (cucumber.Key.Item1, 0) : (0, cucumber.Key.Item2);

            return table[positionToMove] == '.';
        }
    }
}
