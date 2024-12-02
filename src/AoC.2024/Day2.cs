using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/2
public sealed class Day2 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day2));
        var reports = lines.Select(x => x.Split(" ").Select(int.Parse).ToArray());

        var safeCount = reports.Count(IsSafeReport);

        return safeCount.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day2));
        var reports = lines.Select(x => x.Split(" ").Select(int.Parse).ToArray());
        var safeCount = 0;

        foreach (var report in reports)
        {
            if (IsSafeReport(report))
            {
                safeCount++;
            }
            else
            {
                foreach (var i in Enumerable.Range(0, report.Length))
                {
                    var newReport = report.ToList();
                    newReport.RemoveAt(i);
                    if (!IsSafeReport(newReport.ToArray()))
                        continue;

                    safeCount++;
                    break;
                }
            }
        }

        return safeCount.ToString();
    }

    private static bool IsSafeReport(int[] report)
    {
        var prev = report[0];
        bool? prevIsIncrease = null;

        foreach (var (i, level) in report.Skip(1).Index())
        {
            var isIncrease = level > prev;
            prevIsIncrease ??= isIncrease;

            if (Math.Abs(prev - level) is < 1 or > 3 || prevIsIncrease != isIncrease)
                break;

            if (i == report.Length - 2)
                return true;

            prev = level;
        }

        return false;
    }
}