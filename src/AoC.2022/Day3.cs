using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/3
public sealed class Day3 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var priorityValues = new List<int>();

        foreach (var line in GetLineInput(nameof(Day3)))
        {
            var split = line.Length / 2;
            var left = line[..split];
            var right = line[split..];

            var common = left.Intersect(right).First();

            priorityValues.Add(GetPriorityValue(common));
        }

        return priorityValues.Sum().ToString();
    }

    public string SolvePart2()
    {
        var priorityValues = new List<int>();

        foreach (var group in GetLineInput(nameof(Day3)).Chunk(3))
        {
            var common = group[0]
                .Intersect(group[1])
                .Intersect(group[2])
                .First();

            priorityValues.Add(GetPriorityValue(common));
        }

        return priorityValues.Sum().ToString();
    }

    private static int GetPriorityValue(char input)
    {
        return input is >= 'a' and <= 'z'
            ? input - 96
            : input - 38;
    }
}
