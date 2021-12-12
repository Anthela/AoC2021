namespace AdventOfCode_2021.Problems.Model.Problem12
{
    public class Cave
    {
        public string Name { get; }
        public bool Big { get; }
        public List<string> ConnectedCaves { get; set; } = new();

        public Cave(string name, string connectedTo)
        {
            Name = name;
            Big = Name.Equals(Name.ToUpper());
            ConnectedCaves.Add(connectedTo);
        }
    }
}
