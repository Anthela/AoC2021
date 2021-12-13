using AdventOfCode_2021;
using Pastel;
using System.Reflection;

bool runAllProblems = false;

IEnumerable<Type> assemblies;

var problemAssemblies = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => t.FullName!.StartsWith("AdventOfCode_2021.Problems.Problem"));

if (runAllProblems)
    assemblies = problemAssemblies;
else
    assemblies = problemAssemblies.TakeLast(1);

foreach (var type in assemblies)
{
    var problem = Activator.CreateInstance(type) as dynamic;
    var (partA, elapsedA) = Utils.DoAndMeasure(() => problem!.DoPartA());
    var (partB, elapsedB) = Utils.DoAndMeasure(() => problem!.DoPartB());

    Console.WriteLine($"--------------------------------------------------------------------------------".Pastel("#FF8900"));
    Console.WriteLine($"                                   {type.Name.Replace("m", "m ").ToUpper()}");
    Console.WriteLine($"");
    Console.WriteLine($"    {"Part A".Pastel("#F984E5")}: {Indent(Colorize(partA, "#83EEFF"))} - Time elapsed: {Colorize(elapsedA, "#77C66E")} ms");
    Console.WriteLine($"    {"Part B".Pastel("#F5EA92")}: {Indent(Colorize(partB, "#83EEFF"))} - Time elapsed: {Colorize(elapsedB, "#77C66E")} ms");
}

Console.WriteLine($"--------------------------------------------------------------------------------".Pastel("#FF8900"));


string Colorize(dynamic elem, string color) => ((object)elem).ToString().Pastel(color);

string Indent(string line)
{
    if (line.Contains(Environment.NewLine))
    {
        return string.Join(Environment.NewLine, line.Split(Environment.NewLine).Select(s => s.PadLeft(s.Length + 12, ' '))).Substring(12);
    }

    return line;
}