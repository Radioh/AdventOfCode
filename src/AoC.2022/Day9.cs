using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/9
public sealed class Day9 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return Solve(ropeCount: 2);
    }
    public string SolvePart2()
    {
        return Solve(ropeCount: 10);
    }

    private string Solve(int ropeCount)
    {
        var ropes = Enumerable.Range(0, ropeCount).Select(x => (X: 0, Y: 0)).ToList();

        var travelled = new HashSet<(int, int)> { ropes.Last() };

        foreach (var line in GetLineInput(nameof(Day9)))
        {
            var instruction = line.Split(" ");

            for (var i = 0; i < int.Parse(instruction[1]); i++)
            {
                ropes[0] = instruction[0] switch
                {
                    "D" => (ropes[0].X, ropes[0].Y - 1),
                    "U" => (ropes[0].X, ropes[0].Y + 1),
                    "L" => (ropes[0].X - 1, ropes[0].Y),
                    "R" => (ropes[0].X + 1, ropes[0].Y),
                    _ => ropes[0]
                };

                for (var j = 0; j < ropes.Count-1; j++)
                {
                    ropes[j+1] = Move(ropes[j], ropes[j+1]);
                }

                travelled.Add(ropes.Last());
            }
        }

        return travelled.Count.ToString();
    }

    private static (int, int) Move((int X, int Y) head, (int X, int Y) tail)
    {
        if (head == tail)
            return tail;

        var xDiff = head.X - tail.X;
        var yDiff = head.Y - tail.Y;

        // If the head is ever two steps directly up, down, left, or right from the tail,
        // the tail must also move one step in that direction

        // check if xDiff or yDiff is >=1 but not both
        if (Math.Abs(xDiff) >= 1 ^ Math.Abs(yDiff) >= 1)
        {
            // move tail one step in the x direction
            if (Math.Abs(xDiff) >= 2)
                return (tail.X + Math.Sign(xDiff), tail.Y);

            // move tail one step in the y direction
            if (Math.Abs(yDiff) >= 2)
                return (tail.X, tail.Y + Math.Sign(yDiff));
        }

        // if the head and tail aren't touching and aren't in the same row or column,
        // the tail always moves one step diagonally

        // if the head and tail has a difference of 1 in both x and y, return the tail
        if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1)
            return tail;

        // move the tail towards the head in the x direction and y direction by 1
        return (tail.X + Math.Sign(xDiff), tail.Y + Math.Sign(yDiff));
    }
}
