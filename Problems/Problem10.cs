namespace AdventOfCode_2021.Problems
{
    public class Problem10 : IProblem<int, long>
    {
        readonly (char, char)[] validPairs = new (char, char)[] { ('(', ')'), ('[', ']'), ('{', '}'), ('<', '>') };
        readonly char[] closingChars = new char[] { ')', ']', '}', '>' };

        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("10");

            (char, int)[] points = new (char, int)[] { (')', 3), (']', 57), ('}', 1197), ('>', 25137) };
            int result = 0;

            foreach (var item in lines)
            {
                var currentLine = item;
                bool exit = false;

                while (currentLine.Any() && !exit)
                {
                    if (closingChars.Contains(currentLine.First()))
                        break;

                    int openCharCount = 1;

                    for (int i = 1; i < currentLine.Length; i++)
                    {
                        if (closingChars.Contains(currentLine[i]))
                        {
                            // Valid pair, cut off from the current line
                            if (validPairs.Contains((currentLine[i - 1], currentLine[i])))
                            {
                                currentLine = currentLine[..(i - 1)] + currentLine[(i + 1)..];
                                break;
                            }
                            else // Found the corrupted closer, evaluate points
                            {
                                result += points.Where(x => x.Item1 == currentLine[i]).Single().Item2;
                                exit = true;
                                break;
                            }
                        }
                        else
                            openCharCount++;
                    }

                    if (openCharCount == currentLine.Length)
                        break;
                }
            }

            return result;
        }

        public long DoPartB()
        {
            var lines = Utils.InputToStringArray("10");

            (char, int)[] points = new (char, int)[] { (')', 1), (']', 2), ('}', 3), ('>', 4) };
            List<long> result = new();

            foreach (var item in lines)
            {
                var currentLine = item;
                bool exit = false;

                while (currentLine.Any() && !exit)
                {
                    if (closingChars.Contains(currentLine.First()))
                        break;

                    int openCharCount = 1;
                    int lineLength = currentLine.Length;

                    for (int i = 1; i < currentLine.Length; i++)
                    {
                        if (closingChars.Contains(currentLine[i]))
                        {
                            // Valid pair, cut off from the current line
                            if (validPairs.Contains((currentLine[i - 1], currentLine[i])))
                            {
                                currentLine = currentLine[..(i - 1)] + currentLine[(i + 1)..];
                                break;
                            }
                            else // Corrupted, we dont care about this line
                            {
                                exit = true;
                                break;
                            }
                        }
                        else
                            openCharCount++;
                    }

                    if (!exit && openCharCount == lineLength)
                    {
                        long score = 0;

                        foreach (var opener in currentLine.Reverse())
                        {
                            var validPair = validPairs.Where(x => x.Item1 == opener).Single();
                            score = score * 5 + points.Where(x => x.Item1 == validPair.Item2).Single().Item2;
                        }

                        result.Add(score);

                        break;
                    }
                }
            }

            return result.OrderBy(x => x).ElementAt((result.Count + 1) / 2 - 1);
        }
    }
}
