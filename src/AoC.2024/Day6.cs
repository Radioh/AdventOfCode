using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/6
public sealed class Day6 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var grid = GetLineInput(nameof(Day6)).Select(x => x.ToCharArray()).ToArray();

        return GetVisited(grid)!.Count.ToString();
    }

    public string SolvePart2()
    {
        var grid = GetLineInput(nameof(Day6)).Select(x => x.ToCharArray()).ToArray();

        var infiniteLoopCount = 0;

        foreach (var visited in GetVisited(grid)!.Skip(1))
        {
            grid[visited.y][visited.x] = '#';

            var newVisited = GetVisited(grid);

            grid[visited.y][visited.x] = '.';

            if (newVisited is null)
                infiniteLoopCount++;
        }

        return infiniteLoopCount.ToString();
    }

    private static HashSet<(int x, int y)>? GetVisited(char[][] grid)
    {
        var guardPosition = FindGuard(grid);
        var guardDirection = grid[guardPosition.y][guardPosition.x];
        var visitState = new HashSet<(int x, int y, char direction)>();

        while (true)
        {
            if (!visitState.Add((guardPosition.x, guardPosition.y, guardDirection)))
                return null;

            try
            {
                var nextPosition = guardDirection switch
                {
                    '^' => grid[guardPosition.y - 1][guardPosition.x] == '#' ? (guardPosition.x + 1, guardPosition.y, '>') : (guardPosition.x, guardPosition.y - 1, '^'),
                    '>' => grid[guardPosition.y][guardPosition.x + 1] == '#' ? (guardPosition.x, guardPosition.y + 1, 'v') : (guardPosition.x + 1, guardPosition.y, '>'),
                    'v' => grid[guardPosition.y + 1][guardPosition.x] == '#' ? (guardPosition.x - 1, guardPosition.y, '<') : (guardPosition.x, guardPosition.y + 1, 'v'),
                    '<' => grid[guardPosition.y][guardPosition.x - 1] == '#' ? (guardPosition.x, guardPosition.y - 1, '^') : (guardPosition.x - 1, guardPosition.y, '<'),
                    _ => throw new Exception("Invalid guard direction")
                };

                guardDirection = nextPosition.Item3;

                // don't update guard position if it's a wall on the next position
                if (grid[nextPosition.Item2][nextPosition.Item1] != '#')
                    guardPosition = (nextPosition.Item1, nextPosition.Item2);

            }
            catch (IndexOutOfRangeException) // it ain't being greasy in a land full of cleanliness
            {
                break;
            }
        }

        return visitState.Select(x => (x.x, x.y)).ToHashSet();
    }

    private static (int x, int y) FindGuard(char[][] grid)
    {
        for (var i = 0; i < grid.Length; i++)
        for (var j = 0; j < grid[i].Length; j++)
            if (grid[i][j] == '^')
                return (j, i);

        throw new Exception("Guard not found");
    }
}