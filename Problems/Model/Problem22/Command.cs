namespace AdventOfCode_2021.Problems.Model.Problem22
{
    public class Command
    {
        public (int, int) xRange { get; set; }
        public (int, int) yRange { get; set; }
        public (int, int) zRange { get; set; }
        public bool On { get; set; }
    }
}
