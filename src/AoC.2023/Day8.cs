using AoC.Core;

namespace AoC._2023;

public sealed class Day8 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day8));

        var strategy = lines[0];
        var instructions = GetInstructions(lines);

        var currentInstruction = "AAA";
        var strategyStepIndex = 0;
        var found = false;
        var steps = 0;

        while (!found)
        {
            steps++;

            if (strategyStepIndex == strategy.Length)
                strategyStepIndex = 0;

            var takeLeft = strategy[strategyStepIndex] == 'L';
            strategyStepIndex++;

            currentInstruction = takeLeft
                ? instructions[currentInstruction].left
                : instructions[currentInstruction].right;

            if (currentInstruction == "ZZZ")
                found = true;
        }

        return steps.ToString();
    }

    public string SolvePart2()
    {
        var lines = GetLineInput(nameof(Day8));

        var strategy = lines[0];
        var instructions = GetInstructions(lines);
        var nodes = GetStartingNodes(instructions);

        var steps = 0;
        var found = false;
        var strategyStepIndex = 0;

        while (!found)
        {

            if (strategyStepIndex == strategy.Length)
                strategyStepIndex = 0;

            var takeLeft = strategy[strategyStepIndex] == 'L';
            strategyStepIndex++;

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node.instruction.EndsWith('Z'))
                {
                    if (!node.visited.TryAdd(node.instruction, steps))
                        continue;
                }

                node.instruction = takeLeft
                    ? instructions[node.instruction].left
                    : instructions[node.instruction].right;

                nodes[i] = node;
            }

            steps++;
            found = nodes.TrueForAll(x => x.instruction.EndsWith('Z'));
        }

        var potentialStepCounts = nodes.SelectMany(x => x.visited.Values).ToList();

        return FindLowestCommonMultiple(potentialStepCounts).ToString();
    }

    private static long FindLowestCommonMultiple(List<int> numbers)
    {
        long lcm = numbers[0];

        for (var i = 1; i < numbers.Count; i++)
        {
            lcm = FindLowestCommonMultiple(lcm, numbers[i]);
        }

        return lcm;
    }

    private static long FindLowestCommonMultiple(long a, long b)
    {
        return (a * b) / FindGreatestCommonDenominator(a, b);
    }

    private static long FindGreatestCommonDenominator(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private static List<(string instruction, Dictionary<string, int> visited)> GetStartingNodes(Dictionary<string, (string left, string right)> instructions)
    {
        return instructions.Where(x => x.Key.EndsWith('A')).Select(x => (x.Key, new Dictionary<string, int>())).ToList();
    }

    private static Dictionary<string, (string left, string right)> GetInstructions(string[] lines)
    {
        var instructions = new Dictionary<string, (string left, string right)>();

        for (var i = 2; i < lines.Length; i++)
        {
            var split = lines[i].Split(" = ");
            var label = split[0];
            var leftRight = split[1].Split(", ");

            instructions.Add(label, (leftRight[0].Replace("(", ""), leftRight[1].Replace(")", "")));
        }

        return instructions;
    }
}