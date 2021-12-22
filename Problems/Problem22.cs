using AdventOfCode_2021.Problems.Model.Problem22;

namespace AdventOfCode_2021.Problems
{
    public class Problem22 : IProblem<int, long>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("22").Select(line => line.Split(' '));
            List<Command> commands = new();

            foreach (var line in lines)
            {
                var command = line.First();
                var coordinates = line.Last().Split(',').Select(coord => coord.Split("..")).ToArray();

                var x = (Convert.ToInt32(coordinates[0].First().Split("=").Last()), Convert.ToInt32(coordinates[0].Last()));
                var y = (Convert.ToInt32(coordinates[1].First().Split("=").Last()), Convert.ToInt32(coordinates[1].Last()));
                var z = (Convert.ToInt32(coordinates[2].First().Split("=").Last()), Convert.ToInt32(coordinates[2].Last()));

                if (x.Item1 >= -50 && x.Item2 <= 50 && y.Item1 >= -50 && y.Item2 <= 50 && z.Item1 >= -50 && z.Item2 <= 50)
                    commands.Add(new Command() { On = command.Equals("on"), xRange = x, yRange = y, zRange = z });
            }

            Dictionary<(int, int, int), bool> turnedOnCubes = new();

            foreach (var command in commands)
                for (int i = command.xRange.Item1; i <= command.xRange.Item2; i++)
                    for (int j = command.yRange.Item1; j <= command.yRange.Item2; j++)
                        for (int k = command.zRange.Item1; k <= command.zRange.Item2; k++)
                            turnedOnCubes[(i, j, k)] = command.On;

            return turnedOnCubes.Count(x => x.Value);
        }

        public long DoPartB()
        {
            var lines = Utils.InputToStringArray("22_mini").Select(line => line.Split(' '));
            List<Command> commands = new();

            foreach (var line in lines)
            {
                var command = line.First();
                var coordinates = line.Last().Split(',').Select(coord => coord.Split("..")).ToArray();

                var x = (Convert.ToInt32(coordinates[0].First().Split("=").Last()), Convert.ToInt32(coordinates[0].Last()));
                var y = (Convert.ToInt32(coordinates[1].First().Split("=").Last()), Convert.ToInt32(coordinates[1].Last()));
                var z = (Convert.ToInt32(coordinates[2].First().Split("=").Last()), Convert.ToInt32(coordinates[2].Last()));

                commands.Add(new Command() { On = command.Equals("on"), xRange = x, yRange = y, zRange = z });
            }

            Dictionary<(int, int, int), bool> turnedOnCubes = new();

            foreach (var command in commands)
                for (int i = command.xRange.Item1; i <= command.xRange.Item2; i++)
                    for (int j = command.yRange.Item1; j <= command.yRange.Item2; j++)
                        for (int k = command.zRange.Item1; k <= command.zRange.Item2; k++)
                            turnedOnCubes[(i, j, k)] = command.On;

            return turnedOnCubes.Count(x => x.Value);
        }
    }
}
