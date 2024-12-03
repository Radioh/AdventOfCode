using System.Text.RegularExpressions;
using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/3
public sealed partial class Day3 : PuzzleCore, IPuzzle
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)")]
    private static partial Regex MulDoDontRegex();

    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day3));
        var input = string.Join("\n", lines);

        var matches = MulRegex().Matches(input);

        long sum = 0;

        foreach (Match match in matches)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            sum += x * y;
        }

        return sum.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day3));
        var input = string.Join("\n", lines);

        var matches = MulDoDontRegex().Matches(input);

        long sum = 0;
        var isEnabled = true;

        foreach (Match match in matches)
        {
            switch (match.Groups[0].Value)
            {
                case "do()":
                    isEnabled = true;
                    continue;
                case "don't()":
                    isEnabled = false;
                    continue;
            }

            if (!isEnabled)
            {
                continue;
            }

            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            sum += x * y;
        }

        return sum.ToString();
    }
}