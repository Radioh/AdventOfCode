using AoC.Core;

namespace AoC._2023;

public sealed class Day2 : PuzzleCore, IPuzzle
{
    private const int Red = 12;
    private const int Green = 13;
    private const int Blue = 14;

    public string SolvePart1()
    {
        var allLines = GetLineInput(nameof(Day2));

        return allLines
            .Select(x => new Game(x))
            .Where(x => x.IsPossible(Red, Green, Blue))
            .Sum(x => x.Id)
            .ToString();
    }

    public string SolvePart2()
    {
        var allLines = GetLineInput(nameof(Day2));

        return allLines
            .Select(x => new Game(x))
            .Sum(x => x.GetCubePower())
            .ToString();
    }
}

file class Game(string line)
{
    public int Id { get; } = int.Parse(line.Split(":")[0].Split(" ")[1]);
    private string Picks { get; } = line.Split(":")[1];

    public bool IsPossible(int red, int green, int blue)
    {
        return Picks.Split(";").All(x => IsPickPossible(x.Trim(), red, green, blue));
    }

    public int GetCubePower()
    {
        var red = 0;
        var green = 0;
        var blue = 0;

        foreach (var pick in Picks.Replace(";", ",").Split(","))
        {
            var split = pick.Trim().Split(" ");
            var num = int.Parse(split[0]);
            var color = split[1];

            switch (color)
            {
                case "red":
                    if (num > red)
                        red = num;
                    break;
                case "green":
                    if (num > green)
                        green = num;
                    break;
                case "blue":
                    if (num > blue)
                        blue = num;
                    break;
            }
        }


        return red * green * blue;
    }

    private static bool IsPickPossible(string handful, int red, int green, int blue)
    {
        var reds = 0;
        var greens = 0;
        var blues = 0;

        foreach (var pick in handful.Split(","))
        {
            var split = pick.Trim().Split(" ");
            var num = int.Parse(split[0]);
            var color = split[1];

            switch (color)
            {
                case "red":
                    reds += num;
                    break;
                case "green":
                    greens += num;
                    break;
                case "blue":
                    blues += num;
                    break;
            }
        }

        return reds <= red && greens <= green && blues <= blue;
    }
}