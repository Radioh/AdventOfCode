using System.Reflection;
using AoC.Core;

var latestPuzzle = GetLatestPuzzle("2024");
Console.WriteLine($"Part 1:\n{latestPuzzle.SolvePart1()}");
Console.WriteLine();
Console.WriteLine($"Part 2:\n{latestPuzzle.SolvePart2()}");

return;

static IPuzzle GetLatestPuzzle(string year)
{
    var puzzleType = typeof(IPuzzle);
    var assembly = AppDomain.CurrentDomain.Load(new AssemblyName($"AoC.{year}"));

    var latestPuzzle = assembly
        .GetTypes()
        .Where(x => puzzleType.IsAssignableFrom(x) && !x.IsInterface)
        .OrderByDescending(x => x.FullName)
        .Select(x => Activator.CreateInstance(x) as IPuzzle)
        .First();

    ArgumentNullException.ThrowIfNull(latestPuzzle);

    return latestPuzzle;
}



