using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/10
public sealed class Day10 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var signalStrength = 0;
        var cycle = 0;

        RunInstructions(xReg =>
        {
            cycle++;

            if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
                signalStrength += cycle * xReg;
        });

        return signalStrength.ToString();
    }

    public string SolvePart2()
    {
        var crt = Enumerable.Range(0, 6)
            .Select(_ => Enumerable.Range(0, 40)
                .Select(_ => '.')
                .ToArray())
            .ToArray();

        var row = 0;
        var cycle = 0;

        RunInstructions((xReg) =>
        {
            if (cycle == xReg || cycle == xReg + 1 || cycle == xReg - 1)
                crt[row][cycle] = '#';

            cycle++;

            if (cycle == 40)
            {
                cycle = 0;
                row++;
            }
        });

        return string.Join(Environment.NewLine, crt.Select(chars => new string(chars)));
    }

    private void RunInstructions(Action<int> cycle)
    {
        var x = 1;

        foreach (var line in GetLineInput(nameof(Day10)))
        {
            var parts = line.Split(" ");
            var operation = parts[0];

            if (operation == "addx")
            {
                cycle(x);
                cycle(x);

                x+= int.Parse(parts[1]);
            }
            else
            {
                cycle(x);
            }
        }
    }
}
