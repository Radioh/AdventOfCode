using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/6
public sealed class Day6 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return Solve(4);
    }

    public string SolvePart2()
    {
        return Solve(14);
    }

    private string Solve(int markerLength)
    {
        var input = GetLineInput(nameof(Day6)).First();
        var marker = 0;

        for (var i = 0; i < input.Length - markerLength; i++)
        {
            var sub = input.Substring(i, markerLength);

            if (sub.Distinct().Count() == markerLength)
            {
                marker += markerLength;
                break;
            }

            marker++;
        }

        return marker.ToString();
    }
}
