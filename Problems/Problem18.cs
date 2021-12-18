using System.Text.RegularExpressions;

namespace AdventOfCode_2021.Problems
{
    public class Problem18 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("18").ToArray();
            string currentNumber = string.Empty;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(currentNumber))
                    currentNumber = line;
                else
                {
                    currentNumber = $"[{currentNumber},{line}]";

                    currentNumber = CompletelyReduce(currentNumber);
                }
            }

            return CalculateMagnitude(currentNumber);
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("18").ToArray();
            var magnitude = 0;

            for (int i = 0; i < lines.Length - 1; i++)
            {
                var firstNumber = lines[i];

                for (int j = i + 1; j < lines.Length; j++)
                {
                    var numberToReduce = $"[{firstNumber},{lines[j]}]";

                    numberToReduce = CompletelyReduce(numberToReduce);

                    var currentMagnitude = CalculateMagnitude(numberToReduce);

                    if (currentMagnitude > magnitude)
                        magnitude = currentMagnitude;
                }
            }

            for (int i = lines.Length - 1; i > 0; i--)
            {
                var firstNumber = lines[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    var numberToReduce = $"[{firstNumber},{lines[j]}]";

                    numberToReduce = CompletelyReduce(numberToReduce);

                    var currentMagnitude = CalculateMagnitude(numberToReduce);

                    if (currentMagnitude > magnitude)
                        magnitude = currentMagnitude;
                }
            }

            return magnitude;
        }

        private string ReduceNumber(string currentNumber)
        {
            var pairCount = 0;
            string leftNumber = string.Empty;
            string rightNumber = string.Empty;
            string lastLeftNumber = string.Empty;
            int lastLeftNumberIndex = 0;
            bool commaOccured = false;
            bool explodable = false;

            for (int i = 0; i < currentNumber.Length; i++)
            {
                if (currentNumber[i] == '[')
                    pairCount++;

                if (currentNumber[i] == ']')
                    pairCount--;

                if (pairCount == 5)
                {
                    explodable = true;
                    pairCount = 0;
                    break;
                }
            }

            for (int i = 0; i < currentNumber.Length; i++)
            {
                switch (currentNumber[i])
                {
                    case '[':
                        pairCount++;

                        if (pairCount == 5)
                        {
                            var leftSide = string.Empty;
                            var mid = string.Empty;
                            var rightSide = string.Empty;

                            var commaIndex = 0;
                            var firstNumberAfterComma = string.Empty;

                            var closerIndex = 0;
                            var firstNumberAfterCloser = string.Empty;

                            if (string.IsNullOrEmpty(lastLeftNumber))
                            {
                                leftSide = $"{currentNumber[..4]}0";

                                commaIndex = currentNumber.IndexOf(',', i);

                                firstNumberAfterComma = Regex.Match(currentNumber[commaIndex..], @"\d+").Value;

                                closerIndex = currentNumber.IndexOf(']', i);

                                firstNumberAfterCloser = Regex.Match(currentNumber[closerIndex..], @"\d+").Value;
                                var firstNumberAfterCloserIndex = Regex.Match(currentNumber[closerIndex..], @"\d+").Index;

                                mid = $"{currentNumber.Substring(closerIndex + 1, firstNumberAfterCloserIndex - 1)}{Convert.ToInt32(firstNumberAfterComma) + Convert.ToInt32(firstNumberAfterCloser)}";

                                rightSide = currentNumber[(closerIndex + firstNumberAfterCloserIndex + firstNumberAfterCloser.Length)..];

                                currentNumber = $"{leftSide}{mid}{rightSide}";

                                i = currentNumber.Length + 1;
                            }
                            else
                            {
                                leftSide = currentNumber[..lastLeftNumberIndex];

                                var firstNumberAfterOpener = Convert.ToInt32(Regex.Match(currentNumber[i..], @"\d+").Value);

                                mid = $"{Convert.ToInt32(lastLeftNumber) + firstNumberAfterOpener}";

                                var firstCloserIndex = currentNumber.IndexOf(']', i);
                                var firstNumberAfterCloserIndex = Regex.Match(currentNumber[firstCloserIndex..], @"\d+").Value;

                                if (string.IsNullOrEmpty(firstNumberAfterCloserIndex))
                                {
                                    rightSide = ",0]]]]";
                                    currentNumber = $"{leftSide}{mid}{rightSide}";
                                    i = currentNumber.Length + 1;
                                }
                                else
                                {
                                    mid += currentNumber.Substring(lastLeftNumberIndex + lastLeftNumber.Length, i - lastLeftNumberIndex - lastLeftNumber.Length);
                                    commaIndex = currentNumber.IndexOf(',', i);
                                    firstNumberAfterComma = Regex.Match(currentNumber[commaIndex..], @"\d+").Value;
                                    var firstNumberAfterCloserIndexIndex = Regex.Match(currentNumber[firstCloserIndex..], @"\d+").Index;
                                    mid += "0" + currentNumber.Substring(firstCloserIndex + 1, firstNumberAfterCloserIndexIndex - 1);

                                    rightSide = $"{Convert.ToInt32(firstNumberAfterCloserIndex) + Convert.ToInt32(firstNumberAfterComma)}{currentNumber[(firstNumberAfterCloserIndexIndex + firstCloserIndex + firstNumberAfterCloserIndex.Length)..]}";

                                    currentNumber = $"{leftSide}{mid}{rightSide}";
                                    i = currentNumber.Length + 1;
                                }
                            }
                        }
                        leftNumber = string.Empty;
                        rightNumber = string.Empty;
                        commaOccured = false;
                        break;
                    case ']':
                        pairCount--;

                        if (!string.IsNullOrEmpty(rightNumber))
                        {
                            int rightValue = Convert.ToInt32(rightNumber);
                            lastLeftNumber = rightNumber;
                            lastLeftNumberIndex = i - lastLeftNumber.Length;

                            if (rightValue >= 10 && !explodable)
                            {
                                currentNumber = $"{currentNumber[..(i - rightNumber.Length)]}[{rightValue / 2},{Math.Ceiling(rightValue / 2.0)}]{currentNumber[i..]}";
                                i = currentNumber.Length + 1;
                            }
                        }

                        leftNumber = string.Empty;
                        rightNumber = string.Empty;
                        commaOccured = false;
                        break;
                    case ',':
                        commaOccured = true;

                        if (!string.IsNullOrEmpty(leftNumber))
                        {
                            int leftValue = Convert.ToInt32(leftNumber);
                            lastLeftNumber = leftNumber;
                            lastLeftNumberIndex = i - lastLeftNumber.Length;

                            if (leftValue >= 10 && !explodable)
                            {
                                currentNumber = $"{currentNumber[..(i - leftNumber.Length)]}[{leftValue / 2},{Math.Ceiling(leftValue / 2.0)}]{currentNumber[i..]}";
                                i = currentNumber.Length + 1;
                            }
                        }
                        break;
                    default:
                        if (commaOccured)
                            rightNumber += currentNumber[i].ToString();
                        else
                            leftNumber += currentNumber[i].ToString();
                        break;
                }
            }

            return currentNumber;
        }

        private int CalculateMagnitude(string number)
        {
            var magnitude = 0;

            while (true)
            {
                var regex = Regex.Match(number, @"\[\d+\,\d+\]");
                if (regex.Success)
                {
                    var pair = regex.Value[1..^1].Split(',').Select(x => Convert.ToInt32(x));

                    magnitude = 3 * pair.First() + 2 * pair.Last();

                    number = $"{number[..regex.Index]}{magnitude}{number[(regex.Index + regex.Value.Length)..]}";
                }
                else
                {
                    return magnitude;
                }
            }
        }

        private string CompletelyReduce(string currentNumber)
        {
            bool exit = false;

            while (!exit)
            {
                var reducedNumber = ReduceNumber(currentNumber);

                if (reducedNumber.Equals(currentNumber))
                    exit = true;
                else
                    currentNumber = reducedNumber;
            }

            return currentNumber;
        }
    }
}
