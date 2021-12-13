using System.Text;

namespace AdventOfCode_2021.Problems
{
    public class Problem13 : IProblem<int, string>
    {
        public int DoPartA()
        {
            var table = ReadTransparentPaper(out List<(char, int)> commands);

            foreach (var command in commands.Take(1))
            {
                table = DoCommand(table, command);
            }

            return table.Where(x => x.Value).ToList().Count;
        }

        public string DoPartB()
        {
            var sb = new StringBuilder();
            var table = ReadTransparentPaper(out List<(char, int)> commands);

            foreach (var command in commands)
            {
                table = DoCommand(table, command);
            }

            for (int i = 0; i <= table.Select(x => x.Key.Item2).Max(); i++)
            {
                for (int j = 0; j <= table.Select(x => x.Key.Item1).Max(); j++)
                {
                    sb.Append(table[(j, i)] ? "#" : ".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private Dictionary<(int, int), bool> ReadTransparentPaper(out List<(char, int)> commands)
        {
            var lines = Utils.InputToStringArray("13").ToArray();

            Dictionary<(int, int), bool> table = new();
            commands = new();
            List<(int, int)> markedPositions = new();
            int maxX = 0;
            int maxY = 0;
            var commandPrefix = "fold along";

            foreach (var item in lines)
            {
                if (item.Contains(","))
                {
                    var line = item.Split(',');
                    var X = Convert.ToInt32(line.First());
                    var Y = Convert.ToInt32(line.Last());

                    if (X > maxX)
                        maxX = X;

                    if (Y > maxY)
                        maxY = Y;

                    markedPositions.Add((X, Y));
                }
                else
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var command = item.Substring(commandPrefix.Length + 1);
                        var commandDetail = command.Split('=');

                        commands.Add((Convert.ToChar(commandDetail.First()), Convert.ToInt32(commandDetail.Last())));
                    }
                }
            }

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    table[(i, j)] = markedPositions.Contains((i, j));
                }
            }

            return table;
        }

        private Dictionary<(int, int), bool> DoCommand(Dictionary<(int, int), bool> table, (char, int) command)
        {
            bool xCommand = command.Item1 == 'x';

            var remainingTable = table.Where(x => (xCommand ? x.Key.Item1 : x.Key.Item2) < command.Item2).ToDictionary(x => x.Key, x => x.Value);
            var lappingTable = table.Where(x => (xCommand ? x.Key.Item1 : x.Key.Item2) > command.Item2 && x.Value).ToList();

            foreach (var item in lappingTable)
            {
                var mirroredPosition = xCommand ? (command.Item2 - (item.Key.Item1 - command.Item2), item.Key.Item2) : (item.Key.Item1, command.Item2 - (item.Key.Item2 - command.Item2));

                remainingTable[mirroredPosition] = true;
            }

            table = remainingTable;

            return table;
        }
    }
}
