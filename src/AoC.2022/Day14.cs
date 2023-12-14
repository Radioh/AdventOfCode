using AoC.Core;

namespace AoC._2022;

public sealed class Day14 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return GetRestCount(breakOnBottom: true);
    }

    public string SolvePart2()
    {
        return GetRestCount(breakOnBottom: false);
    }

    private string GetRestCount(bool breakOnBottom)
    {
        var structure = GetStructure();

        var highestY = structure.Max(p => p.Y) + 2;
        var dropPoint = (X: 500, Y: 0);
        var sand = dropPoint;
        var atRestCount = 0;

        void Next() => sand = dropPoint;

        while (true)
        {
            if (sand.Y >= highestY)
            {
                if (breakOnBottom)
                    break;

                structure.Add(sand);
                Next();
                continue;
            }

            var below = (X: sand.X, Y: sand.Y + 1);
            if (structure.Contains(below))
            {
                var leftDown = (X: sand.X - 1, Y: sand.Y + 1);
                if (structure.Contains(leftDown))
                {
                    var rightDown = (X: sand.X + 1, Y: sand.Y + 1);
                    if (structure.Contains(rightDown))
                    {
                        atRestCount++;
                        structure.Add(sand);

                        if (sand == dropPoint)
                            break;

                        Next();
                    }
                    else
                    {
                        sand = rightDown;
                    }
                }
                else
                {
                    sand = leftDown;
                }
            }
            else
            {
                sand = below;
            }
        }

        return atRestCount.ToString();
    }

    private HashSet<(int X, int Y)> GetStructure()
    {
        var set = new HashSet<(int X, int Y)>();

        foreach (var line in GetLineInput(nameof(Day14)))
        {
            var coords = line.Split(" -> ");

            for (var i = 0; i < coords.Length; i++)
            {
                if (i == coords.Length - 1)
                    break;

                var left = coords[i].Split(",");
                var right = coords[i + 1].Split(",");

                var leftX = int.Parse(left[0]);
                var leftY = int.Parse(left[1]);
                var rightX = int.Parse(right[0]);
                var rightY = int.Parse(right[1]);

                set.Add((leftX, leftY));
                set.Add((rightX, rightY));

                if (leftX == rightX)
                {
                    if (leftY > rightY)
                        (leftY, rightY) = (rightY, leftY);

                    for (var y = leftY; y <= rightY; y++)
                        set.Add((leftX, y)); }
                else
                {
                    if (leftX > rightX)
                        (leftX, rightX) = (rightX, leftX);

                    for (var x = leftX; x <= rightX; x++)
                        set.Add((x, leftY));
                }
            }
        }

        return set;
    }
}
