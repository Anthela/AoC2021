using AdventOfCode.Problems.Model.Problem8;

namespace AdventOfCode.Problems
{
    public class Problem8 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("8");

            // vonal után 2, 3, 4, 7 hosszúak számolása
            // 2 hosszú - 1
            // 3 hosszú - 7
            // 4 hosszú - 4
            // 7 hosszú - 8

            List<string> afterLine = new List<string>();

            foreach (var item in lines)
            {
                var line = item.Substring(item.IndexOf('|') + 2);
                afterLine.Add(line);
            }

            var sum = 0;

            foreach (var item in afterLine)
            {
                var digits = item.Split(' ');

                foreach (var digit in digits)
                {
                    if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
                        sum++;
                }
            }

            return sum;
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("8");

            Dictionary<string, string> beforeAndAfterLine = new();

            char[] inputs = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            var sum = 0;

            foreach (var item in lines)
            {
                var line = item.Substring(0, item.IndexOf('|') - 1);
                var afterLineString = item.Substring(item.IndexOf('|') + 2);

                beforeAndAfterLine.Add(line, afterLineString);
            }

            foreach (var item in beforeAndAfterLine)
            {
                var digits = item.Key.Split(' ');
                Digits digit = new Digits();

                var one = digits.Where(x => x.Length == 2).Single();
                var seven = digits.Where(x => x.Length == 3).Single();
                var four = digits.Where(x => x.Length == 4).Single();
                var three = digits.Where(x => x.Length == 5 && x.Contains(one[0]) && x.Contains(one[1])).Single();
                var five = digits.Where(x => x.Length == 5 && x != three && x.Intersect(four).ToList().Count == 3).Single();
                var two = digits.Where(x => x.Length == 5 && x != three && x != five).Single();

                digit.Top = seven.Except(one).Single();
                digit.Mid = four.Intersect(five).Intersect(two).Single();
                digit.TopLeft = four.Where(x => x != digit.Mid && !one.Contains(x)).Single();
                digit.Bottom = two.Intersect(five).Where(x => x != digit.Top && x != digit.Mid).Single();
                digit.BottomRight = five.Where(x => x != digit.Top && x != digit.TopLeft && x != digit.Mid && x != digit.Bottom).Single();
                digit.TopRight = one.Where(x => x != digit.BottomRight).Single();
                digit.BottomLeft = inputs.Where(x => x != digit.Top && x != digit.TopLeft && x != digit.TopRight && x != digit.Mid && x != digit.BottomRight && x != digit.Bottom).Single();

                sum += digit.EvaluateValue(item.Value.Split(' '));
            }

            return sum;
        }
    }
}
