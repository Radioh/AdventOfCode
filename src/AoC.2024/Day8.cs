using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/8
public sealed class Day8 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day8));
        var grid = lines.Select(x => x.ToCharArray()).ToArray();

        return GetAntiNodePositions(grid, part2: false).Count.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day8));
        var grid = lines.Select(x => x.ToCharArray()).ToArray();

        return GetAntiNodePositions(grid, part2: true).Count.ToString();
    }

    private static HashSet<(int x, int y)> GetAntiNodePositions(char[][] grid, bool part2)
    {
        var antiNodePositions = new HashSet<(int x, int y)>();

        foreach (var nodeCollection in GetNodePositions(grid))
        {
            var nodes = nodeCollection.Value;

            if (nodes.Count < 1)
                continue;

            foreach (var (i, node) in nodes.Index())
            {
                var otherNodes = nodes.Index().Where(x => x.Index != i).Select(x => x.Item);

                foreach (var nextNode in otherNodes)
                {
                    foreach (var antiNode in GetAntiNodePosition(node, nextNode, part2))
                    {
                        if (antiNode.x < 0 ||
                            antiNode.x >= grid[0].Length ||
                            antiNode.y < 0 ||
                            antiNode.y >= grid.Length)
                            continue;

                        antiNodePositions.Add(antiNode);
                    }
                }
            }
        }

        return antiNodePositions;
    }

    private static List<(int x, int y)> GetAntiNodePosition((int x, int y) node, (int x, int y) nextNode, bool part2)
    {
        var antiNodePositions = new List<(int x, int y)>();

        var deltaX = node.x - nextNode.x;
        var deltaY = node.y - nextNode.y;

        antiNodePositions.Add((node.x + deltaX, node.y + deltaY));

        if (!part2)
            return antiNodePositions;

        antiNodePositions.Add(node);
        for (var i = 0; i < 100; i++)
        {
            antiNodePositions.Add((node.x + deltaX * i, node.y + deltaY * i));
        }

        return antiNodePositions;
    }

    private static Dictionary<char, List<(int x, int y)>> GetNodePositions(char[][] grid)
    {
        var nodePositions = new Dictionary<char, List<(int x, int y)>>();

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                var c = grid[y][x];

                if (c == '.')
                    continue;

                if (!nodePositions.TryGetValue(c, out var value))
                {
                    value = [];
                    nodePositions[c] = value;
                }

                value.Add((x, y));
            }
        }

        return nodePositions;
    }
}