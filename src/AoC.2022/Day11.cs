using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/11
public sealed class Day11 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var monkeys = ParseMonkeys();
        return Solve(monkeys, 20, x => x / 3);
    }

    public string SolvePart2()
    {
        var monkeys = ParseMonkeys();
        var lcm = monkeys.Aggregate(1, (x, y) => x * y.TestValue);

        return Solve(monkeys, 10_000, x => x % lcm);
    }

    private static string Solve(Monkey[] monkeys, int rounds, Func<long, long> worryRelief)
    {
        for (var i = 0; i < rounds; i++)
            foreach (var monkey in monkeys)
                monkey.RunRound(monkeys, worryRelief);

        var monkeyBusiness = monkeys
            .Select(x => x.InspectCount)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((x, y) => x * y);

        return monkeyBusiness.ToString();
    }

    private Monkey[] ParseMonkeys()
    {
        var monkeyStrings = new List<string>();
        var monkeys = new List<Monkey>();

        foreach (var line in GetLineInput(nameof(Day11)))
        {
            if (line.Contains("Monkey"))
                continue;

            if (line.Contains("Starting items:"))
                monkeyStrings.Add(line.Split("Starting items: ").Last());

            if (line.Contains("Operation: new = "))
                monkeyStrings.Add(line.Split("Operation: new = ").Last());

            if (line.Contains("Test: divisible by "))
                monkeyStrings.Add(line.Split("Test: divisible by ").Last());

            if (line.Contains("If "))
                monkeyStrings.Add(line.Split(" ").Last());

            if (monkeyStrings.Count == 5)
            {
                monkeys.Add(CreateMonkey(monkeyStrings));
                monkeyStrings = new List<string>();
            }
        }

        return monkeys.ToArray();
    }

    private static Monkey CreateMonkey(List<string> monkeyStrings)
    {
        return new Monkey(
            items: new Queue<long>(monkeyStrings[0].Split(", ").Select(long.Parse)),
            operation: ParseOperation(monkeyStrings[1]),
            testValue: int.Parse(monkeyStrings[2]),
            testTrueMonkeyIndex: int.Parse(monkeyStrings[3]),
            testFalseMonkeyIndex: int.Parse(monkeyStrings[4]));
    }

    private static Func<long, long> ParseOperation(string operationStr)
    {
        var terms = operationStr.Split(" ");

        if (terms[1] == "+")
        {
            var add = int.Parse(terms[2]);
            return x => x + add;
        }

        if (terms[2] == "old")
            return x => x * x;

        var mul = int.Parse(terms[2]);
        return x => x * mul;
    }

    private class Monkey
    {
        public Monkey(
            Queue<long> items,
            Func<long, long> operation,
            int testValue,
            int testTrueMonkeyIndex,
            int testFalseMonkeyIndex)
        {
            Items = items;
            Operation = operation;
            TestValue = testValue;
            TestTrueMonkeyIndex = testTrueMonkeyIndex;
            TestFalseMonkeyIndex = testFalseMonkeyIndex;
        }

        internal int TestValue { get; }
        internal long InspectCount { get; private set; }
        private Queue<long> Items { get; }
        private Func<long, long> Operation { get; }
        private int TestTrueMonkeyIndex { get; }
        private int TestFalseMonkeyIndex { get; }

        private void AddItem(long item)
        {
            Items.Enqueue(item);
        }

        internal void RunRound(Monkey[] monkeys, Func<long, long> worryRelief)
        {
            while (Items.TryDequeue(out var worryLevel))
            {
                InspectCount++;

                worryLevel = Operation(worryLevel);
                worryLevel = worryRelief(worryLevel);

                var throwTo = worryLevel % TestValue == 0
                    ? TestTrueMonkeyIndex
                    : TestFalseMonkeyIndex;

                monkeys[throwTo].AddItem(worryLevel);
            }
        }
    }
}
