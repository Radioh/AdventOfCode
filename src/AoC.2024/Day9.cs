using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/9
public sealed class Day9 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var line = GetLineInput(nameof(Day9)).Single();

        var expanded = Expand(line);
        var compacted = CompactIndividual(expanded);

        return GetChecksum(compacted).ToString();
    }

    private static List<int> CompactIndividual(List<int?> input)
    {
        for (var i = 0; i < input.Count; i++)
        {
            if (input[i] != null)
                continue;

            for (var j = input.Count - 1; j > i; j--)
            {
                if (input[j] == null)
                    continue;

                // Swap
                (input[i], input[j]) = (input[j], input[i]);
                break;
            }
        }

        return input.Where(x => x.HasValue).Select(x => x!.Value).ToList();
    }

    private static List<int> CompactBlock(List<int?> input)
    {
        for (var i = 0; i < input.Count; i++)
        {
            if (input[i] == null)
                continue;

            for (var j = input.Count - 1; j > i; j--)
            {
                // Find the last block of same values and swap them into first available slot of nulls next to each other
                if (input[j] == null || input[j] != input[i])
                    continue;

                var k = i;
                while (input[k] != null)
                {
                    k++;
                }

                // Swap
                (input[k], input[j]) = (input[j], input[k]);
            }

        }

        return input.Where(x => x.HasValue).Select(x => x!.Value).ToList();
    }

    private static List<int?> Expand(string input)
    {
        var output = new List<int?>();

        var id = 0;
        foreach (var (index, value) in input.Index())
        {
            var digit = int.Parse(value.ToString());

            if (index % 2 == 0)
            {
                output.AddRange(Enumerable.Repeat(id, digit).Select(x => (int?)x));
                id++;
            }
            else
            {
                output.AddRange(Enumerable.Repeat((int?)null, digit));
            }
        }

        return output;
    }

    private static long GetChecksum(List<int> input)
    {
        var sum = 0L;

        foreach (var (id, value) in input.Index())
        {
            sum += id * value;
        }

        return sum;
    }
}