using AoC.Core;

namespace AoC._2023;

public sealed class Day7 : PuzzleCore, IPuzzle, IComparer<(string hand, int bid, int score)>
{
    private static readonly Dictionary<char, int> CardValues = new()
    {
        { '2', 1 }, { '3', 2 }, { '4', 3 }, { '5', 4 }, { '6', 5 }, { '7', 6 }, { '8', 7 }, { '9', 8 },
        { 'T', 9 }, { 'J', 10 }, { 'Q', 11 }, { 'K', 12 }, { 'A', 13 }
    };
    public string SolvePart1()
    {
        return Solve(useJoker: false);
    }

    public string SolvePart2()
    {
        return Solve(useJoker: true);
    }

    private string Solve(bool useJoker)
    {
        if (useJoker)
            CardValues['J'] = 0;

        var lines = GetLineInput(nameof(Day7));
        var handRanks = new List<(string hand, int bid, int score)>();

        foreach (var line in lines)
        {
            var split = line.Split(" ");
            var hand = split[0];
            var bid = int.Parse(split[1]);

            var score = GetScore(hand, useJoker);

            handRanks.Add((hand, bid, score));
        }

        var orderedHands = handRanks
            .OrderByDescending(x => x.score)
            .ThenByDescending(x => x, this);

        var rank = 1;
        var result = orderedHands.Reverse().Sum(hand => hand.bid * rank++);

        return result.ToString();
    }

    private static int GetScore(string hand, bool useJoker)
    {
        var currentScore = GetScore(hand);

        if (!useJoker || hand.All(x => x != 'J'))
            return currentScore;

        return CardValues.Keys
            .Select(potentialCard => GetScore(hand.Replace('J', potentialCard)))
            .Prepend(currentScore)
            .Max();
    }

    private static int GetScore(string hand)
    {
        var groups = hand
            .ToCharArray()
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ToList();

        // 5 of a kind
        if (groups.Count == 1)
            return 7;

        // 4 of a kind
        if (groups.Count == 2 && groups[0].Count() == 4)
            return 6;

        // Full house
        if (groups.Count == 2 && groups[0].Count() == 3)
            return 5;

        // 3 of a kind
        if (groups[0].Count() == 3)
            return 4;

        // 2 pairs
        if (groups[0].Count() == 2 && groups[1].Count() == 2)
            return 3;

        // 1 pair
        if (groups[0].Count() == 2)
            return 2;

        // High card
        return 1;
    }
    private static int CompareCards(char card1, char card2)
    {
        var card1Value = CardValues[card1];
        var card2Value = CardValues[card2];

        return card1Value.CompareTo(card2Value);
    }

    public int Compare(
        (string hand, int bid, int score) x,
        (string hand, int bid, int score) y)
    {
        for (var i = 0; i < x.hand.Length; i++)
        {
            var compare = CompareCards(x.hand[i], y.hand[i]);

            if (compare != 0)
                return compare;
        }

        return 0;
    }
}