using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/8
public sealed class Day8 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var count = 0;

        LoopGrid((adjacent, value) =>
        {
            if (IsEdge(adjacent))
                count++;

            else if (IsLess(adjacent, value))
                count++;
        });

        return count.ToString();
    }

    public string SolvePart2()
    {
        var highestScore = 0;

        LoopGrid((adjacent, value) =>
        {
            var score = Distance(adjacent.Above, value) *
                        Distance(adjacent.Left, value) *
                        Distance(adjacent.Right, value) *
                        Distance(adjacent.Below, value);

            if (highestScore < score)
                highestScore = score;
        });

        return highestScore.ToString();
    }

    private void LoopGrid(Action<Adjacent, int> solvePart)
    {
        var grid = GetInput();

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                var adjacent = GetAdjacent(grid, i, j);
                var value = grid[i][j];

                solvePart(adjacent, value);
            }
        }
    }

    private static bool IsLess(Adjacent adjacent, int value)
    {
        return adjacent.Above.All(x => x < value)
               || adjacent.Below.All(x => x < value)
               || adjacent.Left.All(x => x < value)
               || adjacent.Right.All(x => x < value);
    }

    private static bool IsEdge(Adjacent adjacent)
    {
        return !adjacent.Above.Any() ||
               !adjacent.Below.Any() ||
               !adjacent.Left.Any() ||
               !adjacent.Right.Any();
    }

    private static int Distance(List<int> adjacent, int max)
    {
        var count = 0;
        foreach (var value in adjacent)
        {
            count++;
            if (value >= max)
                break;
        }

        return count;
    }

    private static Adjacent GetAdjacent(int[][] tiles, int i, int j)
    {
        var above = new List<int>(); // Reverse
        for (var k = i - 1; k >= 0; k--) above.Add(tiles[k][j]);

        var below = new List<int>();
        for (var k = i + 1; k < tiles.Length; k++) below.Add(tiles[k][j]);

        var left = new List<int>(); // Reverse
        for (var k = j - 1; k >= 0; k--) left.Add(tiles[i][k]);

        var right = new List<int>();
        for (var k = j + 1; k < tiles[i].Length; k++) right.Add(tiles[i][k]);

        return new Adjacent(above, below, left, right);
    }

    private int[][] GetInput()
    {
        return GetLineInput(nameof(Day8))
            .Select(line => line
                .Select(x => int.Parse(x.ToString()))
                .ToArray())
            .ToArray();
    }

    private record struct Adjacent(
        List<int> Above,
        List<int> Below,
        List<int> Left,
        List<int> Right);
}
