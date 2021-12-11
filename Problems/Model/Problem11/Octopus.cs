namespace AdventOfCode_2021.Problems.Model.Problem11
{
    public class Octopus
    {
        public int EnergyLevel { get; set; }
        public bool Flashed { get; set; }

        public void Flash()
        {
            EnergyLevel = 0;
            Flashed = true;
        }
    }
}
