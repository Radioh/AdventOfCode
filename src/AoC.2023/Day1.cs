using AoC.Core;

namespace AoC._2023;

public sealed class Day1 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day1));
        var numbers = lines.Select(x => ParseLine(x));

        return numbers.Sum().ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day1));
        var numbers = lines.Select(x => ParseLine(x, true));

        return numbers.Sum().ToString();
    }

    private static int ParseLine(string line, bool part2 = false)
    {
        if (part2)
        {
            foreach (var digitLookup in Digits)
            {
                line = line.Replace(digitLookup.Key, digitLookup.Value);
            }
        }

        var numbers = line.Where(char.IsDigit).ToArray();

        return int.Parse($"{numbers.First()}{numbers.Last()}");
    }

    private static readonly Dictionary<string, string> Digits = new()
    {
        // Because you might loose numbers which can be created. Example twone should be 21
        { "one", "one1one" },
        { "two", "two2two" },
        { "three", "three3three" },
        { "four", "four4four" },
        { "five", "five5five" },
        { "six", "six6six" },
        { "seven", "seven7seven" },
        { "eight", "eight8eight" },
        { "nine", "nine9nine" }
    };
}