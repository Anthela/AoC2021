using System.Text;

namespace AdventOfCode_2021.Problems
{
    public class Problem14 : IProblem<int, long>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("14").ToArray();
            string init = string.Empty;
            List<(string, char)> rules = new();
            int step = 10;

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains("->"))
                    {
                        var rule = line.Split(" -> ");
                        rules.Add((rule.First(), Convert.ToChar(rule.Last())));
                    }
                    else
                    {
                        init = line;
                    }
                }
            }

            for (int i = 0; i < step; i++)
            {
                var sb = new StringBuilder();

                for (int j = 1; j < init.Length; j++)
                {
                    var rule = rules.Where(x => x.Item1.Equals($"{init[j - 1]}{init[j]}")).Single();

                    sb.Append(init[j - 1]).Append(rule.Item2);
                }

                sb.Append(init.Last());

                init = sb.ToString();
            }

            var groups = init.GroupBy(character => character).Select(group => group.Count());

            return groups.Max() - groups.Min();
        }

        public long DoPartB()
        {
            var lines = Utils.InputToStringArray("14").ToArray();
            List<(string, char)> rules = new();
            int step = 40;

            Dictionary<string, long> pairs = new();
            Dictionary<char, long> occurs = new();

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains("->"))
                    {
                        var rule = line.Split(" -> ");
                        rules.Add((rule.First(), Convert.ToChar(rule.Last())));
                    }
                    else
                    {
                        var init = line;

                        for (int i = 0; i < init.Length; i++)
                        {
                            if (occurs.ContainsKey(init[i]))
                                occurs[init[i]]++;
                            else
                                occurs[init[i]] = 1;

                            if (i > 0)
                            {
                                string pair = $"{init[i - 1]}{init[i]}";
                                if (pairs.ContainsKey(pair))
                                    pairs[pair]++;
                                else
                                    pairs[pair] = 1;
                            }

                        }
                    }
                }
            }

            for (int i = 0; i < step; i++)
            {
                Dictionary<string, long> newPairs = new();

                foreach (var pair in pairs)
                {
                    var rule = rules.Where(x => x.Item1.Equals(pair.Key)).Single();

                    if (newPairs.ContainsKey($"{pair.Key.First()}{rule.Item2}"))
                        newPairs[$"{pair.Key.First()}{rule.Item2}"] += pair.Value;
                    else
                        newPairs[$"{pair.Key.First()}{rule.Item2}"] = pair.Value;

                    if (newPairs.ContainsKey($"{rule.Item2}{pair.Key.Last()}"))
                        newPairs[$"{rule.Item2}{pair.Key.Last()}"] += pair.Value;
                    else
                        newPairs[$"{rule.Item2}{pair.Key.Last()}"] = pair.Value;

                    if (occurs.ContainsKey(rule.Item2))
                        occurs[rule.Item2] += pair.Value;
                    else
                        occurs[rule.Item2] = pair.Value;
                }

                pairs = newPairs;
            }

            var values = occurs.Select(x => x.Value);

            return values.Max() - values.Min();
        }
    }
}
