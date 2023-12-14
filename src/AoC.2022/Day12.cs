using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/12
public sealed class Day12 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var hill = GetHill();
        var shortestPath = FindShortestPath(hill);

        return shortestPath.ToString();
    }

    public string SolvePart2()
    {
        var hill = GetHill();

        // :D
        var aPositions = hill.Map
            .SelectMany((row, y) => row.Select((c, x) => (c, x, y)))
            .Where(t => t.c == 'a')
            .Select(t => (t.x, t.y))
            .ToList();

        aPositions.Reverse();

        var shortestPath = int.MaxValue;

        foreach (var start in aPositions)
        {
            hill = hill with { Start = new Step(start) };
            var stepsToFinish = FindShortestPath(hill);

            if (stepsToFinish < shortestPath && stepsToFinish > 0)
                shortestPath = stepsToFinish;
        }

        return shortestPath.ToString();
    }

    private Hill GetHill()
    {
        var map = GetMap();
        var start = GetPosition(map, 'S');
        var end = GetPosition(map, 'E');

        map[start.Y][start.X] = 'a';
        map[end.Y][end.X] = 'z';

        return new Hill(map, new Step(start), new Step(end));
    }

    private char[][] GetMap()
    {
        return GetLineInput(nameof(Day12))
            .Select(line => line.ToCharArray())
            .ToArray();
    }

    private static (int X, int Y) GetPosition(char[][] map, char c)
    {
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[y].Length; x++)
            if (map[y][x] == c)
                return (x, y);

        throw new InvalidOperationException($"Could not find {c} in map");
    }

    private static int FindShortestPath(Hill hill)
    {
        var activeSteps = new List<Step> { hill.Start };
        var visitedSteps = new List<Step>();

        while (activeSteps.Any())
        {
            var checkStep = activeSteps.OrderBy(x => x.CostDistance).First();

            if (checkStep.Pos == hill.End.Pos)
                return checkStep.CountSteps();

            visitedSteps.Add(checkStep);
            activeSteps.Remove(checkStep);

            var walkableTiles = GetWalkableSteps(hill.Map, checkStep, hill.End);

            var notVisited = walkableTiles.Where(walkableTile => visitedSteps
                .All(x => x.Pos != walkableTile.Pos));

            foreach (var walkableTile in notVisited)
            {
                if (activeSteps.All(x => x.Pos != walkableTile.Pos))
                {
                    activeSteps.Add(walkableTile);
                    continue;
                }

                var existingTile = activeSteps.First(x => x.Pos == walkableTile.Pos);

                if (existingTile.CostDistance > checkStep.CostDistance)
                {
                    activeSteps.Remove(existingTile);
                    activeSteps.Add(walkableTile);
                }
            }
        }

        return -1;
    }

    private static List<Step> GetWalkableSteps(char[][] map, Step current, Step target)
    {
        var possibleSteps = new List<Step>()
        {
            new((current.Pos.X, current.Pos.Y - 1), current, current.Cost + 1),
            new((current.Pos.X, current.Pos.Y + 1), current, current.Cost + 1),
            new((current.Pos.X - 1, current.Pos.Y), current, current.Cost + 1),
            new((current.Pos.X + 1, current.Pos.Y), current, current.Cost + 1),
        };

        foreach (var step in possibleSteps)
            step.SetDistance(target.Pos);

        var maxX = map.First().Length - 1;
        var maxY = map.GetLength(0) - 1;

        var walkable = possibleSteps
            .Where(step => step.Pos.X >= 0 && step.Pos.X <= maxX)
            .Where(step => step.Pos.Y >= 0 && step.Pos.Y <= maxY)
            .Where(step => IsStepAllowed(map, current, step))
            .ToList();

        return walkable;
    }

    private static bool IsStepAllowed(char[][] map, Step current, Step step)
    {
        return map[current.Pos.Y][current.Pos.X] >= map[step.Pos.Y][step.Pos.X] ||
               Math.Abs(map[step.Pos.Y][step.Pos.X] - map[current.Pos.Y][current.Pos.X]) <= 1;
    }

    private record struct Hill(char[][] Map, Step Start, Step End);

    private class Step
    {
        internal Step((int X, int Y) pos)
        {
            Pos = pos;
        }

        internal Step((int X, int Y) pos, Step previous, int cost)
        {
            Pos = pos;
            Previous = previous;
            Cost = cost;
        }

        private int Distance { get; set; }
        private Step? Previous { get; }
        internal int Cost { get; }
        internal (int X, int Y) Pos { get; }
        internal int CostDistance => Cost + Distance;

        internal void SetDistance((int X, int Y) target)
        {
            Distance = Math.Abs(target.X - Pos.X) +
                       Math.Abs(target.Y - Pos.Y);
        }

        internal int CountSteps()
        {
            if (Previous is null)
                return 0;

            return 1 + Previous.CountSteps();
        }
    }
}
