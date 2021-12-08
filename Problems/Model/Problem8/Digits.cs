namespace AdventOfCode.Problems.Model.Problem8
{
    public class Digits
    {
        public char Top { get; set; } = '*';
        public char TopLeft { get; set; } = '*';
        public char TopRight { get; set; } = '*';
        public char Mid { get; set; } = '*';
        public char BottomLeft { get; set; } = '*';
        public char BottomRight { get; set; } = '*';
        public char Bottom { get; set; } = '*';


        public int EvaluateValue(string[] pattern)
        {
            double result = 0;

            for (int i = 0; i < pattern.Length; i++)
            {
                var multiplier = Math.Pow(10, 3 - i);

                if (pattern[i].Length == 2)
                    result += 1 * multiplier;

                if (pattern[i].Length == 3)
                    result += 7 * multiplier;

                if (pattern[i].Length == 4)
                    result += 4 * multiplier;

                if (pattern[i].Length == 7)
                    result += 8 * multiplier;

                if (pattern[i].Length == 5)
                {
                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopRight) && pattern[i].Any(x => x == Mid) && pattern[i].Any(x => x == BottomLeft) && pattern[i].Any(x => x == Bottom))
                        result += 2 * multiplier;

                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopRight) && pattern[i].Any(x => x == Mid) && pattern[i].Any(x => x == BottomRight) && pattern[i].Any(x => x == Bottom))
                        result += 3 * multiplier;

                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopLeft) && pattern[i].Any(x => x == Mid) && pattern[i].Any(x => x == BottomRight) && pattern[i].Any(x => x == Bottom))
                        result += 5 * multiplier;
                }

                if (pattern[i].Length == 6)
                {
                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopLeft) && pattern[i].Any(x => x == TopRight) && pattern[i].Any(x => x == BottomLeft) && pattern[i].Any(x => x == BottomRight) && pattern[i].Any(x => x == Bottom))
                        result += 0 * multiplier;

                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopLeft) && pattern[i].Any(x => x == Mid) && pattern[i].Any(x => x == BottomLeft) && pattern[i].Any(x => x == BottomRight) && pattern[i].Any(x => x == Bottom))
                        result += 6 * multiplier;

                    if (pattern[i].Any(x => x == Top) && pattern[i].Any(x => x == TopLeft) && pattern[i].Any(x => x == TopRight) && pattern[i].Any(x => x == Mid) && pattern[i].Any(x => x == BottomRight) && pattern[i].Any(x => x == Bottom))
                        result += 9 * multiplier;
                }
            }

            return Convert.ToInt32(result);
        }
    }
}
