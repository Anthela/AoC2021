namespace AdventOfCode.Problems.Model.Problem4
{
    public class BoardEntry
    {
        private readonly int number;
        private bool marked;

        public BoardEntry(string number)
        {
            this.number = int.Parse(number);
            this.marked = false;
        }

        public bool IsMarked() => marked;

        public void SetMarked() => marked = true;

        public int GetNumber() => number;
    }
}
