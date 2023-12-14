using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/5
public sealed class Day5 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        return Solve(reverse: false);
    }

    public string SolvePart2()
    {
        return Solve(reverse: true);
    }

    private string Solve(bool reverse)
    {
        var stacks = InitializeStacks();

        foreach (var procedure in GetLineInput(nameof(Day5)))
        {
            var values = ParseProcedureValues(procedure);

            var popped = new List<char>();

            for (var i = 0; i < values[0]; i++)
                popped.Add(stacks[values[1] - 1].Pop());

            if (reverse)
                popped.Reverse();

            foreach (var pop in popped)
                stacks[values[2] - 1].Push(pop);
        }

        return string.Join("", stacks.Select(x => x.Peek()));
    }

    private static int[] ParseProcedureValues(string procedure)
    {
        return procedure
            .Split(' ')
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .ToArray();
    }

    private static Stack<char>[] InitializeStacks()
    {
        // Part of input
        //     [M]             [Z]     [V]
        //     [Z]     [P]     [L]     [Z] [J]
        // [S] [D]     [W]     [W]     [H] [Q]
        // [P] [V] [N] [D]     [P]     [C] [V]
        // [H] [B] [J] [V] [B] [M]     [N] [P]
        // [V] [F] [L] [Z] [C] [S] [P] [S] [G]
        // [F] [J] [M] [G] [R] [R] [H] [R] [L]
        // [G] [G] [G] [N] [V] [V] [T] [Q] [F]
        //  1   2   3   4   5   6   7   8   9
        return new Stack<char>[]
        {
            new(new []{'S', 'P', 'H', 'V', 'F', 'G', }.Reverse()),
            new(new []{'M', 'Z', 'D', 'V', 'B', 'F', 'J', 'G' }.Reverse()),
            new(new []{'N', 'J', 'L', 'M', 'G' }.Reverse()),
            new(new []{'P', 'W', 'D', 'V', 'Z', 'G', 'N' }.Reverse()),
            new(new []{'B', 'C', 'R', 'V' }.Reverse()),
            new(new []{'Z', 'L', 'W', 'P', 'M', 'S', 'R', 'V' }.Reverse()),
            new(new []{'P', 'H', 'T' }.Reverse()),
            new(new []{'V', 'Z', 'H', 'C', 'N', 'S', 'R', 'Q' }.Reverse()),
            new(new []{'J', 'Q', 'V', 'P', 'G', 'L', 'F' }.Reverse()),
        };
    }
}
