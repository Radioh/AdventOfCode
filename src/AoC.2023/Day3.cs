using AoC.Core;

namespace AoC._2023;

public sealed class Day3 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return FindAllNumbers()
            .Where(x => x.IsPartNumber)
            .Sum(x => x.Sum)
            .ToString();
    }

    public string SolvePart2()
    {
        return FindAllNumbers()
            .GroupBy(x => x.GearPosition)
            .Where(x => x.Key is not null && x.Count() > 1)
            .Select(x => x.Aggregate(1, (i, number) => i * number.Sum))
            .Sum()
            .ToString();
    }

    private List<PartNumber> FindAllNumbers()
    {
        var lines = GetLineInput(nameof(Day3));

        var partNumbers = new List<PartNumber>();
        var hasAdded = false;
        var currentPos = (x: 0, y: 0);
        var symbolPositions = new HashSet<(int x, int y)>();
        var gearPositions = new HashSet<(int x, int y)>();

        foreach (var line in lines)
        {
            foreach (var c in line.Trim())
            {
                if (c == '*')
                {
                    gearPositions.Add(currentPos);
                }

                if (!char.IsDigit(c) && c != '.')
                {
                    symbolPositions.Add(currentPos);
                }

                if (char.IsDigit(c))
                {
                    if (!hasAdded)
                    {
                        partNumbers.Add(new PartNumber(currentPos));
                        hasAdded = true;
                    }

                    partNumbers.Last().Nums.Add(c);
                }
                else
                {
                    hasAdded = false;
                }

                currentPos.x++;
            }

            hasAdded = false;
            currentPos.y++;
            currentPos.x = 0;
        }

        foreach (var num in partNumbers)
        {
            num.CheckIsPartNumber(symbolPositions);
            num.SetGearPosition(gearPositions);
        }

        return partNumbers;
    }
}

public class PartNumber((int x, int y) startPosition)
{
    public List<char> Nums { get; } = new();
    private (int x, int y) StartPosition { get; } = startPosition;
    public bool IsPartNumber { get; private set; }
    public int Sum => int.Parse(string.Join("", Nums));
    public (int x, int y)? GearPosition { get; set; }

    public void SetGearPosition(HashSet<(int x, int y)> gearPositions)
    {
        GearPosition = FindAdjacent(gearPositions);
    }

    public void CheckIsPartNumber(HashSet<(int x, int y)> symbolPositions)
    {
        if (FindAdjacent(symbolPositions) is not null)
        {
            IsPartNumber = true;
        }
    }

    private (int x, int y)? FindAdjacent(HashSet<(int x, int y)> symbolPositions)
    {
        foreach (var x in Enumerable.Range(StartPosition.x, Nums.Count))
        {
            var y = StartPosition.y;

            if (symbolPositions.Contains((x - 1, y)))
                return (x - 1, y);

            if (symbolPositions.Contains((x + 1, y)))
                return (x + 1, y);

            if (symbolPositions.Contains((x, y - 1)))
                return (x, y - 1);

            if (symbolPositions.Contains((x, y + 1)))
                return (x, y + 1);

            if (symbolPositions.Contains((x + 1, y + 1)))
                return (x + 1, y + 1);

            if (symbolPositions.Contains((x - 1, y - 1)))
                return (x - 1, y - 1);

            if (symbolPositions.Contains((x - 1, y + 1)))
                return (x - 1, y + 1);

            if (symbolPositions.Contains((x + 1, y - 1)))
                return (x + 1, y - 1);
        }

        return null;
    }
}
