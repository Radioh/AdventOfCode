using AoC.Core;

namespace AoC._2023;

public sealed class Day8 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var lines = GetLineInput(nameof(Day8));

        var instructions = new Dictionary<string, (string left, string right)>();

        for (var i = 2; i < lines.Length; i++)
        {
            var split = lines[i].Split(" = ");
            var label = split[0];
            var leftRight = split[1].Split(", ");

            instructions.Add(label, (leftRight[0].Replace("(", ""), leftRight[1].Replace(")", "")));
        }

        var strategy = lines[0];
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


}