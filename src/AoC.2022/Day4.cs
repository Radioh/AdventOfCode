using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/4
public sealed class Day4 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var totalCount = 0;

        foreach (var line in GetLineInput(nameof(Day4)))
        {
            var ranges = line.Split(',');

            var left = GetRange(ranges[0].Split('-'));
            var right = GetRange(ranges[1].Split('-'));

            if (left.All(right.Contains) || right.All(left.Contains))
                totalCount++;
        }

        return totalCount.ToString();
    }

    public string SolvePart2()
    {
        var totalCount = 0;

        foreach (var line in GetLineInput(nameof(Day4)))
        {
            var ranges = line.Split(',');

            var left = GetRange(ranges[0].Split('-'));
            var right = GetRange(ranges[1].Split('-'));

            if (left.Any(right.Contains))
                totalCount++;
        }

        return totalCount.ToString();
    }

    private static IReadOnlyCollection<int> GetRange(IReadOnlyList<string> input)
    {
        return Enumerable.Range(
                int.Parse(input[0]),
                int.Parse(input[1]) - int.Parse(input[0]) + 1)
            .ToList();
    }
}
