using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/4
public sealed partial class Day4 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day4));
        var grid = lines.Select(x => x.ToCharArray()).ToArray();

        var xmasCount = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                xmasCount += CheckForXMAS(grid, i, j);
            }
        }

        return xmasCount.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day4));
        var grid = lines.Select(x => x.ToCharArray()).ToArray();

        var xmasCount = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (CheckForCrossMAS(grid, i, j))
                {
                    xmasCount++;
                }
            }
        }

        return xmasCount.ToString();
    }

    private static int CheckForXMAS(char[][] grid, int i, int j)
    {
        var count = 0;

        if (grid[i][j] != 'X')
        {
            return count;
        }

        // horizontal right
        if (j + 3 < grid[i].Length)
        {


            if (grid[i][j + 1] == 'M' &&
                grid[i][j + 2] == 'A' &&
                grid[i][j + 3] == 'S')
            {
                count++;
            }
        }

        // horizontal left
        if (j - 3 >= 0)
        {
            if (grid[i][j - 1] == 'M' &&
                grid[i][j - 2] == 'A' &&
                grid[i][j - 3] == 'S')
            {
                count++;
            }
        }

        // vertical down
        if (i + 3 < grid.Length)
        {
            if (grid[i + 1][j] == 'M' &&
                grid[i + 2][j] == 'A' &&
                grid[i + 3][j] == 'S')
            {
                count++;
            }
        }

        // vertical up
        if (i - 3 >= 0)
        {
            if (grid[i - 1][j] == 'M' &&
                grid[i - 2][j] == 'A' &&
                grid[i - 3][j] == 'S')
            {
                count++;
            }
        }

        // diagonal down right
        if (i + 3 < grid.Length && j + 3 < grid[i].Length)
        {
            if (grid[i + 1][j + 1] == 'M' &&
                grid[i + 2][j + 2] == 'A' &&
                grid[i + 3][j + 3] == 'S')
            {
                count++;
            }
        }

        // diagonal down left
        if (i + 3 < grid.Length && j - 3 >= 0)
        {
            if (grid[i + 1][j - 1] == 'M' &&
                grid[i + 2][j - 2] == 'A' &&
                grid[i + 3][j - 3] == 'S')
            {
                count++;
            }
        }

        // diagonal up right
        if (i - 3 >= 0 && j + 3 < grid[i].Length)
        {
            if (grid[i - 1][j + 1] == 'M' &&
                grid[i - 2][j + 2] == 'A' &&
                grid[i - 3][j + 3] == 'S')
            {
                count++;
            }
        }

        // diagonal up left
        if (i - 3 >= 0 && j - 3 >= 0)
        {
            if (grid[i - 1][j - 1] == 'M' &&
                grid[i - 2][j - 2] == 'A' &&
                grid[i - 3][j - 3] == 'S')
            {
                count++;
            }
        }

        return count;
    }

    private static bool CheckForCrossMAS(char[][] grid, int i, int j)
    {
        if (grid[i][j] != 'A')
            return false;

        if (i - 1 < 0 || i + 1 >= grid.Length || j - 1 < 0 || j + 1 >= grid[i].Length)
            return false;

        return new string(grid[i - 1][j + 1], grid[i + 1][j - 1]) is "MS" or "SM" &&
               new string(grid[i - 1][j - 1], grid[i + 1][j + 1]) is "MS" or "SM";
    }
}