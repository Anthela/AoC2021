namespace AdventOfCode_2021.Problems
{
    public class Problem20 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("20");
            var algorithm = lines.First();
            var inputArray = lines.Skip(2).ToArray();
            Dictionary<(int, int), bool> inputImage = new();
            Dictionary<(int, int), bool> outputImage = new();

            bool shine = algorithm.First() == '#';

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
                    outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j, shine, true);
                }
            }

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
                    outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j, shine);
                }
            }

            return outputImage.Values.Where(x => x).Count();
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("20");
            var algorithm = lines.First();
            var inputArray = lines.Skip(2).ToArray();
            Dictionary<(int, int), bool> inputImage = new();
            Dictionary<(int, int), bool> outputImage = new();

            bool shine = algorithm.First() == '#';

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
                    outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j, shine, true);
                }
            }

            inputImage = outputImage;
            outputImage = new();

            for (int k = 0; k < 49; k++)
            {
                minY = inputImage.Select(x => x.Key.Item1).Min() - 1;
                maxY = inputImage.Select(x => x.Key.Item1).Max() + 1;
                minX = inputImage.Select(x => x.Key.Item2).Min() - 1;
                maxX = inputImage.Select(x => x.Key.Item2).Max() + 1;

                for (int i = minY; i <= maxY; i++)
                {
                    for (int j = minX; j <= maxX; j++)
                    {
                        outputImage[(i, j)] = GetPixelAtIndex(inputImage, algorithm, i, j, shine, k % 2 == 1);
                    }
                }

                inputImage = outputImage;
                outputImage = new();
            }

            return inputImage.Values.Where(x => x).Count();
        }

        private bool GetPixelAtIndex(Dictionary<(int, int), bool> inputImage, string algorithm, int i, int j, bool shine, bool firstRound = false)
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
                if (shine)
                {
                    if (firstRound)
                        binarystring += inputImage.GetValueOrDefault(neighbour) ? "1" : "0";
                    else
                    {
                        if (inputImage.ContainsKey(neighbour))
                            binarystring += inputImage[neighbour] ? "1" : "0";
                        else
                            binarystring += "1";
                    }
                }
                else
                    binarystring += inputImage.GetValueOrDefault(neighbour) ? "1" : "0";
            }

            var index = Convert.ToInt32(binarystring, 2);

            return algorithm[index] == '#';
        }
    }
}
