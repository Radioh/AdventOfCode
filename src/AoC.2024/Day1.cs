using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/1
public sealed class Day1 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day1));
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var line in lines)
        {
            var split = line.Split("   ");
            list1.Add(int.Parse(split[0]));
            list2.Add(int.Parse(split[1]));
        }

        list1 = list1.OrderBy(x => x).ToList();
        list2 = list2.OrderBy(x => x).ToList();

        var totalDistance = list1.Select((t, i) => Math.Abs(t - list2[i])).Sum();

        return totalDistance.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day1));
        var keys = new List<int>();
        var values = new Dictionary<int, int>();
        var similarityScore = 0;

        foreach (var line in lines)
        {
            var split = line.Split("   ");
            keys.Add(int.Parse(split[0]));

            if (values.ContainsKey(int.Parse(split[1])))
                values[int.Parse(split[1])]++;
            else
                values.Add(int.Parse(split[1]), 1);
        }

        foreach (var key in keys)
        {
            if (values.TryGetValue(key, out var value))
            {
                similarityScore += key * value;
            }
        }

        return similarityScore.ToString();
    }
}