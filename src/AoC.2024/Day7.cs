using AoC.Core;

namespace AoC._2024;

// https://adventofcode.com/2024/day/7
public sealed class Day7 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day7));

        return GetTotal(lines, ["+", "*"]).ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day7));

        return GetTotal(lines, ["+", "*", "||"]).ToString();
    }

    private static long GetTotal(string[] lines, string[] operators)
    {
        long total = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(":");

            var expected = long.Parse(parts[0]);
            var values = parts[1].Trim().Split(" ").Select(int.Parse).ToArray();

            var node = new Node { Value = values[0] };

            for (var i = 1; i < values.Length; i++)
            {
                node.Add(values[i], operators);
            }

            if (node.HasTarget(expected))
            {
                total += expected;
            }
        }

        return total;
    }

    private class Node
    {
        public long Value { get; init; }
        private Node? Left { get; set; }
        private Node? Right { get; set; }
        private Node? Middle { get; set; }

        public void Add(long value, string[] operators)
        {
            if (Middle is not null) // Part 2
            {
                Left!.Add(value, operators);
                Right!.Add(value, operators);
                Middle.Add(value, operators);
                return;
            }

            if (Left is not null && Right is not null) // Part 1
            {
                Left.Add(value, operators);
                Right.Add(value, operators);
                return;
            }

            foreach (var op in operators)
            {
                switch (op)
                {
                    case "+":
                        Left = new Node { Value = Value + value };
                        break;
                    case "*":
                        Right = new Node { Value = Value * value };
                        break;
                    case "||":
                        Middle = new Node { Value = long.Parse($"{Value}{value}")};
                        break;
                }
            }
        }

        public bool HasTarget(long target)
        {
            if (Left is null)
            {
                return Value == target;
            }

            return Left.HasTarget(target) ||
                   Right!.HasTarget(target) ||
                   Middle?.HasTarget(target) == true;
        }

    }
}