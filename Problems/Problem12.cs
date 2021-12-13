using AdventOfCode_2021.Problems.Model.Problem12;

namespace AdventOfCode_2021.Problems
{
    public class Problem12 : IProblem<int, int>
    {
        IEnumerable<Cave> caves;

        public int DoPartA()
        {
            caves = ReadCaves();

            var start = caves.Where(x => x.Name.Equals("start")).Single();
            List<string> path = new();
            path.Add("start");

            return GoToNextCave(start, path);
        }

        public int DoPartB()
        {
            caves = ReadCaves();

            var start = caves.Where(x => x.Name.Equals("start")).Single();
            List<string> path = new();
            path.Add("start");

            return GoToNextCaveB(start, path);
        }

        private int GoToNextCave(Cave cave, List<string> activePath)
        {
            int result = 0;

            if (!cave.Name.Equals("end"))
            {
                foreach (var connection in cave.ConnectedCaves)
                {
                    if (!activePath.Contains(connection) || caves.Where(x => x.Name.Equals(connection)).Single().Big)
                    {
                        List<string> path = activePath.Append(connection).ToList();

                        if (connection.Equals("end"))
                            result++;

                        var nextCave = caves.Where(x => x.Name.Equals(connection)).Single();

                        result += GoToNextCave(nextCave, path);
                    }
                }
            }

            return result;
        }

        private int GoToNextCaveB(Cave cave, List<string> activePath)
        {
            int result = 0;
            if (!cave.Name.Equals("end"))
            {
                foreach (var connection in cave.ConnectedCaves)
                {
                    var connectedCave = caves.Where(x => x.Name.Equals(connection)).Single();

                    if (!connectedCave.Name.Equals("start"))
                    {
                        if (!activePath.Contains(connection) ||
                            connectedCave.Big ||
                            activePath.Where(x => x.Equals(x.ToLower()))
                            .GroupBy(cave => cave)
                            .Select(group => group.Count())
                            .All(x => x < 2))
                        {
                            List<string> path = activePath.Append(connection).ToList();

                            if (connection.Equals("end"))
                                result++;

                            var nextCave = caves.Where(x => x.Name.Equals(connection)).Single();

                            result += GoToNextCaveB(nextCave, path);
                        }
                    }
                }
            }

            return result;
        }

        private List<Cave> ReadCaves()
        {
            var lines = Utils.InputToStringArray("12").ToArray();

            List<Cave> caves = new();

            foreach (var line in lines)
            {
                var currentCaves = line.Split('-');

                if (caves.Any(x => x.Name == currentCaves.First()))
                    caves.Where(x => x.Name == currentCaves.First()).Single().ConnectedCaves.Add(currentCaves.Last());
                else
                    caves.Add(new Cave(currentCaves.First(), currentCaves.Last()));

                if (caves.Any(x => x.Name == currentCaves.Last()))
                    caves.Where(x => x.Name == currentCaves.Last()).Single().ConnectedCaves.Add(currentCaves.First());
                else
                    caves.Add(new Cave(currentCaves.Last(), currentCaves.First()));
            }

            return caves;
        }
    }
}
