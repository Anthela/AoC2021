namespace AdventOfCode_2021.Problems
{
    public class Problem20 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("20_mini");
            var algorithm = lines.First();
            var inputArray = lines.Skip(2).ToArray();
            Dictionary<(int, int), bool> inputImage = new();
            Dictionary<(int, int), bool> outputImage = new();

            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = 0; j < inputArray[i].Length; j++)
                {
                    inputImage[(i, j)] = inputArray[i][j] == '#';
                }
            }

            var minY = -1;
            var maxY = inputArray.Length;
            var minX = -1;
            var maxX = inputArray[0].Length;

            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j);
                }
            }

            for (int i = outputImage.Select(x => x.Key.Item1).Min(); i <= outputImage.Select(x => x.Key.Item1).Max(); i++)
            {
                for (int j = outputImage.Select(x => x.Key.Item2).Min(); j <= outputImage.Select(x => x.Key.Item2).Max(); j++)
                {
                    if (outputImage[(i, j)])
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            
            inputImage = outputImage;
            outputImage = new();

            minY = inputImage.Select(x => x.Key.Item1).Min() - 1;
            maxY = inputImage.Select(x => x.Key.Item1).Max() + 1;
            minX = inputImage.Select(x => x.Key.Item2).Min() - 1;
            maxX = inputImage.Select(x => x.Key.Item2).Max() + 1;

            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j);
                }
            }

            for (int i = outputImage.Select(x => x.Key.Item1).Min(); i <= outputImage.Select(x => x.Key.Item1).Max(); i++)
            {
                for (int j = outputImage.Select(x => x.Key.Item2).Min(); j <= outputImage.Select(x => x.Key.Item2).Max(); j++)
                {
                    if (outputImage[(i, j)])
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }

            return outputImage.Values.Where(x => x).Count();
        }

        public int DoPartB()
        {
            return 0;
        }

        private bool GetPixelAtIndex(Dictionary<(int, int), bool> inputImage, string algorithm, int i, int j)
        {
            string binarystring = string.Empty;

            (int, int)[] neighbours = new (int, int)[]
            {
                (i - 1, j - 1),
                (i - 1, j),
                (i - 1, j + 1),
                (i, j - 1),
                (i, j),
                (i, j + 1),
                (i + 1, j - 1),
                (i + 1, j),
                (i + 1, j + 1)
            };

            foreach (var neighbour in neighbours)
            {
                binarystring += inputImage.GetValueOrDefault(neighbour) ? "1" : "0";
            }

            var index = Convert.ToInt32(binarystring, 2);

            return algorithm[index] == '#';
        }
    }
}
