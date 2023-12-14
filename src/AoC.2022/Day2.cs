using AoC.Core;

namespace AoC._2022;

// https://adventofcode.com/2022/day/2
public sealed class Day2 : PuzzleCore, IPuzzle
{
    public string SolvePart1()
    {
        var totalScore = 0;
        foreach (var line in GetLineInput(nameof(Day2)))
        {
            var parts = line.Split(' ');
            totalScore += RockPaperScissorsMoveStrategy(ConvertToMove(parts[1]), ConvertToMove(parts[0]));
        }

        return totalScore.ToString();
    }

    public string SolvePart2()
    {
        var totalScore = 0;
        foreach (var line in GetLineInput(nameof(Day2)))
        {
            var parts = line.Split(' ');
            totalScore += RockPaperScissorsEndStrategy(parts[1], ConvertToMove(parts[0]));
        }

        return totalScore.ToString();
    }

    private static int RockPaperScissorsMoveStrategy(char player, char opponent)
    {
        if (player == opponent)
            return 3 + GetMoveScore(player);

        return player switch
        {
            'R' when opponent == 'S' => 6 + GetMoveScore(player),
            'S' when opponent == 'P' => 6 + GetMoveScore(player),
            'P' when opponent == 'R' => 6 + GetMoveScore(player),
            _ => 0 + GetMoveScore(player)
        };
    }

    private static int RockPaperScissorsEndStrategy(string endResult, char opponent)
    {
        return endResult switch
        {
            "Y" => 3 + GetMoveScore(opponent),
            "Z" => 6 + GetWinningMove(opponent),
            "X" => 0 + GetLosingMove(opponent),
            _ => throw new ArgumentOutOfRangeException(nameof(endResult))
        };
    }

    private static int GetLosingMove(char move)
    {
        return move switch
        {
            'P' => GetMoveScore('R'),
            'R' => GetMoveScore('S'),
            'S' => GetMoveScore('P'),
            _ => throw new ArgumentOutOfRangeException(nameof(move))
        };
    }

    private static int GetWinningMove(char move)
    {
        return move switch
        {
            'P' => GetMoveScore('S'),
            'R' => GetMoveScore('P'),
            'S' => GetMoveScore('R'),
            _ => throw new ArgumentOutOfRangeException(nameof(move))
        };
    }

    private static int GetMoveScore(char player)
    {
        return player switch
        {
            'R' => 1,
            'P' => 2,
            'S' => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(player))
        };
    }

    private static char ConvertToMove(string input)
    {
        return input switch
        {
            "A" or "X" => 'R',
            "B" or "Y" => 'P',
            "C" or "Z" => 'S',
            _ => throw new ArgumentOutOfRangeException(nameof(input))
        };
    }
}
