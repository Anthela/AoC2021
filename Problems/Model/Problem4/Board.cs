namespace AdventOfCode.Problems.Model.Problem4
{
    public class Board
    {
        private readonly int boardSize = 5;
        private int score = -1;
        private readonly List<BoardEntry> boardEntries;

        public Board(IEnumerable<string> input)
        {
            boardEntries = new List<BoardEntry>(
                string.Join(" ", input).Split(" ")
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(nStr => new BoardEntry(nStr)));
        }

        public IEnumerable<BoardEntry> GetRow(int rowNumber)
        {
            var initIndex = rowNumber * boardSize;
            return boardEntries.Take(new Range(initIndex, initIndex + boardSize));
        }

        public IEnumerable<BoardEntry> GetColumn(int columnNumber)
        {
            return boardEntries.Where((_, i) => i % boardSize == columnNumber);
        }

        public bool HasWon()
            => HasAllMarkedBy(GetRow) || HasAllMarkedBy(GetColumn);

        public void Mark(int number)
        {
            boardEntries.FirstOrDefault(be => be.GetNumber() == number)?.SetMarked();

            if (HasWon())
            {
                score = boardEntries.Where(be => !be.IsMarked()).Sum(be => be.GetNumber()) * number;
            }
        }

        public int Score() => score;

        private bool HasAllMarkedBy(Func<int, IEnumerable<BoardEntry>> selector)
            => Enumerable.Range(0, boardSize).Select(selector).Any(group => group.All(eb => eb.IsMarked()));
    }
}
