namespace AdventOfCode_2021.Problems
{
    public class Problem17 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var y = Math.Abs(Utils.InputToStringArray("17").Select(line => Convert.ToInt32(line.Split(", ").Last().Split('=').Last().Split("..").First())).Single());

            return (int)((y - 1) / 2.0 * y);
        }

        public int DoPartB()
        {
            var targetArea = Utils.InputToStringArray("17").Select(line => line.Split(": ").Last()).Single().Split(", ");

            var xRange = targetArea.First().Split('=').Last().Split("..").Select(coords => Convert.ToInt32(coords));
            var yRange = targetArea.Last().Split('=').Last().Split("..").Select(coords => Convert.ToInt32(coords));
            var left = xRange.First();
            var right = xRange.Last();
            var bottom = yRange.First();
            var top = yRange.Last();

            var maxX = right;
            var minY = bottom;
            var maxY = Math.Abs(bottom) - 1;

            var minX = 1;

            while (minX / 2.0 * (minX + 1) < left)
                minX++;

            var result = 0;

            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    if (CanIShootIntoTheTargetedArea(j, i, left, right, bottom, top))
                        result++;
                }
            }

            return result;
        }

        private bool CanIShootIntoTheTargetedArea(int x, int y, int left, int right, int bottom, int top)
        {
            bool bingo = false;

            var posX = 0;
            var posY = 0;

            while (posX <= right && posY >= bottom && !bingo)
            {
                posX += x;
                posY += y--;

                if (posX >= left && posX <= right && posY >= bottom && posY <= top)
                    bingo = true;

                if (!bingo)
                    if (x > 0) x--;
                    else if (x < 0) x++;
            }

            return bingo;
        }
    }
}
