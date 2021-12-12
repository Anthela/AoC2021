using AdventOfCode_2021.Problems.Model.Problem12;

namespace AdventOfCode_2021.Problems
{
    public class Problem12 : IProblem<int, int>
    {
        List<List<string>> allPaths;
        IEnumerable<Cave> caves;

        public int DoPartA()
        {
            caves = ReadCaves();

            var start = caves.Where(x => x.Name.Equals("start")).Single();
            allPaths = new();
            List<string> path = new();
            path.Add("start");

            GoToNextCave(start, path);

            return allPaths.Where(x => x.Contains("end")).ToArray().Length;
        }

        public int DoPartB()
        {
            caves = ReadCaves();

            var start = caves.Where(x => x.Name.Equals("start")).Single();
            allPaths = new();
            List<string> path = new();
            path.Add("start");

            GoToNextCaveB(start, path);

            return allPaths.Where(x => x.Contains("end")).ToList().Count;
        }

        private void GoToNextCave(Cave cave, List<string> activePath)
        {
            if (!cave.Name.Equals("end"))
            {
                foreach (var connection in cave.ConnectedCaves)
                {
                    if (!activePath.Contains(connection) || caves.Where(x => x.Name.Equals(connection)).Single().Big)
                    {
                        List<string> path = activePath.Append(connection).ToList();
                        allPaths.Add(path);

                        var nextCave = caves.Where(x => x.Name.Equals(connection)).Single();

                        GoToNextCave(nextCave, path);
                    }
                }
            }
        }

        private void GoToNextCaveB(Cave cave, List<string> activePath)
        {
            if (!cave.Name.Equals("end"))
            {
                foreach (var connection in cave.ConnectedCaves)
                {
                    var connectedCave = caves.Where(x => x.Name.Equals(connection)).Single();

                    var miniCaveVisited = activePath.Where(x => x.Equals(x.ToLower()))
                        .GroupBy(cave => cave)
                        .Select(group => group.Count()).ToList();

                    if (!connectedCave.Name.Equals("start") && (!activePath.Contains(connection) || connectedCave.Big || miniCaveVisited.All(x => x < 2)))
                    {
                        List<string> path = activePath.Append(connection).ToList();
                        allPaths.Add(path);

                        var nextCave = caves.Where(x => x.Name.Equals(connection)).Single();

                        GoToNextCaveB(nextCave, path);
                    }
                }
            }
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
