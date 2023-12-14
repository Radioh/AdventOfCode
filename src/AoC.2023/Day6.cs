using AoC.Core;

namespace AoC._2023;

public sealed class Day6 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day6));

        var times = Split(lines, 0, false);
        var distances = Split(lines, 1, false);

        return GetScore(times, distances);
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day6));

        var times = Split(lines, 0, true);
        var distances = Split(lines, 1, true);

        return GetScore(times, distances);
    }

    private static string GetScore(List<long> times, List<long> distances)
    {
        var score = 1;

        for (var i = 0; i < times.Count; i++)
        {
            var waysToBeat = 0;
            var time = times[i];
            var distance = distances[i];

            for (long speed = 0; speed <= time; speed++)
            {
                var totalDistance = speed * (time - speed);
                if (totalDistance > distance)
                {
                    waysToBeat++;
                }
            }

            score *= waysToBeat;
        }

        return score.ToString();
    }

    private static List<long> Split(string[] lines, int index, bool removeSpaces)
    {
        var split = lines[index].Split(":")[1];

        if (!removeSpaces)
            return split.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        return [long.Parse(split.Replace(" ", ""))];
    }
}