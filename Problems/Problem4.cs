using AdventOfCode.Problems.Model.Problem4;

namespace AdventOfCode.Problems
{
    public class Problem4 : IProblem<long, long>
    {
        public long DoPartA()
        {
            var (withdrawnNumbers, boards) = LoadDataFromInput("4");

            foreach (var number in withdrawnNumbers)
            {
                boards.ForEach(board => board.Mark(number));

                if (boards.Any(board => board.HasWon()))
                {
                    var asd = boards.First(board => board.HasWon());
                    return asd.Score();
                }
            }

            return -1;
        }

        public long DoPartB()
        {
            var (withdrawnNumbers, boards) = LoadDataFromInput("4");
            var lastBoard = default(Board);

            foreach (var number in withdrawnNumbers)
            {
                boards.ForEach(board => board.Mark(number));

                boards.Where(board => board.HasWon())
                    .ToList()
                    .ForEach(board =>
                    {
                        boards.Remove(board);
                        lastBoard = board;
                    });
            }

            return lastBoard!.Score();
        }

        private (IEnumerable<int>, List<Board>) LoadDataFromInput(string inputName)
        {
            var input = Utils.InputToStringArray(inputName);
            var inputUnitSize = 6;
            var withdrawnNumbers = input.First().Split(",").Select(int.Parse);

            return (withdrawnNumbers,
                Enumerable.Range(0, (input.Count() - 1) / inputUnitSize)
                .Select(i => i * inputUnitSize + 2)
                .Select(i => new Board(input.Take(new Range(i, i + inputUnitSize - 1))))
                .ToList());
        }
    }
}
