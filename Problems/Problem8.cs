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
            var lines = Utils.InputToStringArray("8_mini");

            // vonal után 2, 3, 4, 7 hosszúak számolása
            // 2 hosszú - 1
            // 3 hosszú - 7
            // 4 hosszú - 4
            // 7 hosszú - 8

            /*
                ****
                *  *
                *  *
                ****
                *  *
                *  *
                ****

            */



            List<string> afterLine = new List<string>();
            List<string> beforeLine = new List<string>();
            List<string> sevenDigits = new();


            List<Digits> codedDigits = new();


            foreach (var item in lines)
            {
                var line = item.Substring(0, item.IndexOf('|') - 1);
                beforeLine.Add(line); 
            }

            char[] inputs = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g'};

            foreach (var item in beforeLine)
            {
                var digits = item.Split(' ');
                Digits digit = new Digits();

                var one = digits.Where(x => x.Length == 2).Single(); // topright, bottomright
                var seven = digits.Where(x => x.Length == 3).Single(); // top
                var four = digits.Where(x => x.Length == 4).Single(); // topleft, mid

                digit.TopRight = one[0];
                digit.BottomRight = one[1];
                digit.Top = seven.Where(x => x != one[0] && x != one[1]).Single();
                digit.TopLeft = four.First(x => x != digit.TopRight && x != digit.BottomRight && x != digit.Top);
                digit.Mid = four.Single(x => x != digit.TopRight && x != digit.BottomRight && x != digit.Top && x != digit.TopLeft);

                var notUsedInputs = inputs.Where(x => x != digit.TopRight && x != digit.BottomRight && x != digit.Top && x != digit.TopLeft && x != digit.Mid).ToArray();


                // TODO


                codedDigits.Add(digit);
            }


            foreach (var item in codedDigits)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(item.Top);
                }
                Console.WriteLine();

                Console.Write(item.TopLeft + "   ");
                Console.WriteLine(item.TopRight);
                Console.Write(item.TopLeft + "   ");
                Console.WriteLine(item.TopRight);
                
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(item.Mid);
                }
                Console.WriteLine();

                Console.Write(item.BottomLeft + "   ");
                Console.WriteLine(item.BottomRight);
                Console.Write(item.BottomLeft + "   ");
                Console.WriteLine(item.BottomRight);

                for (int i = 0; i < 4; i++)
                {
                    Console.Write(item.Bottom);
                }
                Console.WriteLine();

                Console.WriteLine("----------------------------------------------");

            }


            /*
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
            */
            return 0;
        }
    }
}
