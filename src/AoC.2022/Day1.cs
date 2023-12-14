using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/1
public sealed class Day1 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return GetElves()
            .Max()
            .ToString();
    }

    public string SolvePart2()
    {
        return GetElves()
            .OrderByDescending(x => x)
            .Take(3)
            .Sum()
            .ToString();
    }

    private IReadOnlyList<int> GetElves()
    {
        var lines = GetLineInput(nameof(Day1));

        var elves = new List<int>();
        var currentCalorieElf = 0;

        foreach (var line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                currentCalorieElf += int.Parse(line);
            }
            else
            {
                elves.Add(currentCalorieElf);
                currentCalorieElf = 0;
            }
        }

        elves.Add(currentCalorieElf);

        return elves;
    }
}
