using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/5
public sealed class Day5 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day5));
        var (beforeOrderingRules, pageUpdates) = ParseInput(lines);

        var result = pageUpdates
            .Where(x => IsValid(beforeOrderingRules, x))
            .Sum(x => x[x.Count / 2]);

        return result.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day5));
        var (beforeOrderingRules, pageUpdates) = ParseInput(lines);

        var result = pageUpdates
            .Where(x => !IsValid(beforeOrderingRules, x))
            .Select(x => Reorder(x, beforeOrderingRules))
            .Sum(x => x[x.Count / 2]);


        return result.ToString();
    }

    private static (Dictionary<int, List<int>> OrderingRules, List<List<int>> PageUpdates) ParseInput(string[] lines)
    {
        Dictionary<int, List<int>> beforeOrderingRules = new();
        List<List<int>> pageUpdates = [];
        var readingPart1 = true;

        foreach (var line in lines)
        {
            if (line == "")
            {
                readingPart1 = false;
                continue;
            }

            if (readingPart1)
            {
                var parts = line.Split('|');
                var before = int.Parse(parts[0]);
                var after = int.Parse(parts[1]);

                if (!beforeOrderingRules.ContainsKey(before))
                    beforeOrderingRules[before] = [];

                beforeOrderingRules[before].Add(after);
            }
            else
            {
                pageUpdates.Add(line.Split(',').Select(int.Parse).ToList());
            }
        }

        return (beforeOrderingRules, pageUpdates);
    }

    private static bool IsValid(Dictionary<int, List<int>> orderingRules, List<int> pageUpdates)
    {
        var seen = new HashSet<int>();

        foreach (var page in pageUpdates)
        {
            if (orderingRules.TryGetValue(page, out var mustComeAfter))
                if (mustComeAfter.Any(after => seen.Contains(after)))
                    return false;

            seen.Add(page);
        }

        return true;
    }

    private static List<int> Reorder(List<int> pageUpdate, Dictionary<int, List<int>> orderingRules)
    {
        return pageUpdate
            .OrderBy(x => orderingRules.TryGetValue(x, out var mustComeAfter) ? mustComeAfter.Count(pageUpdate.Contains) : 0)
            .ToList();
    }
}