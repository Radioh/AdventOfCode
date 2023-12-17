using AoC.Core;

namespace AoC._2023;

public sealed class Day9 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day9));
        var combinedValues = 0;

        foreach (var line in lines)
        {
            var diffIndex = 0;
            var diffs = new List<List<int>>();

            var history = line.Split(" ").Select(int.Parse).ToList();
            diffs.Add(history);

            while (true)
            {
                if (diffs.Last().All(x => x == 0))
                {
                    combinedValues += diffs.Select(x => x.Last()).Sum();
                    break;
                }

                var newDiffs = new List<int>();
                var currentDiffs = diffs[diffIndex];

                for (var i = 0; i < currentDiffs.Count; i++)
                {
                    if (i == currentDiffs.Count - 1)
                        break;

                    var diff = currentDiffs[i + 1] - currentDiffs[i];
                    newDiffs.Add(diff);
                }

                diffs.Add(newDiffs);
                diffIndex++;
            }
        }

        return combinedValues.ToString();
    }
}